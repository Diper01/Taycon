using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using _Game._Scripts.DataTypes.Resources;
using _Game._Scripts.Features.GatheringZone.СooperationZones.InResource;
using _Game._Scripts.Features.GatheringZone.СooperationZones.OutResource;
using _Game._Scripts.Features.InventoryStuff.WorkerInventory;
using UnityEngine;
using UnityEngine.AI;

namespace _Game._Scripts.Features.Workers {
  public class WorkerController : IDisposable {
    private enum State { MovingToZone, Gathering, MovingToDropOff, Delivering }

    private readonly IWorkerView _view;
    private readonly WorkerInventory _inventory;
    private readonly ZoneView _zone;
    private readonly DropOffPointView _dropOff;
    private readonly int _inventoryCapacity;

    private State _state;
    private const int _maxPerHit = 2;
    private const float _hitInterval = 1.2f;
    private CancellationTokenSource _cts;

    public WorkerController (IWorkerView view, ZoneView zone, DropOffPointView dropOff, int capacity) {
      _view = view;
      _zone = zone;
      _dropOff = dropOff;
      _inventory = new WorkerInventory(capacity);
      _inventoryCapacity = capacity;
      _cts = new CancellationTokenSource();
      _state = State.MovingToZone;
      _ = RunAsync(_cts.Token);
    }

    public void Stop() {
      if (_cts == null)
        return;

      _cts.Cancel();
      _cts.Dispose();
      _cts = null;
    }

    public void Dispose()
      => Stop();

    async UniTaskVoid RunAsync (CancellationToken ct) {
      try {
        while (!ct.IsCancellationRequested) {
          switch (_state) {
            case State.MovingToZone:
              await MoveToAsync(_zone.transform.position, ct);

              if (ct.IsCancellationRequested)
                return;

              await StartGatheringAsync(ct);
              break;
            case State.MovingToDropOff:
              await MoveToAsync(_dropOff.transform.position, ct);

              if (ct.IsCancellationRequested)
                return;

              await DeliverAsync(ct);
              _state = State.MovingToZone;
              break;
            case State.Delivering:
              _state = State.MovingToZone;
              break;
          }

          await UniTask.Yield(PlayerLoopTiming.Update, ct);
        }
      } catch (OperationCanceledException) {} catch (Exception e) {
        Debug.LogException(e);
      } finally {
        SafeStopAgent();
        _view.PlayMove(false);
      }
    }

    private async UniTask MoveToAsync (Vector3 target, CancellationToken ct) {
      NavMeshAgent agent = _view.Agent;
      agent.isStopped = false;
      agent.SetDestination(target);
      _view.PlayMove(true);
      await UniTask.WaitUntil(() => !agent.pathPending, cancellationToken: ct);
      await UniTask.WaitUntil(() => Arrived(agent), cancellationToken: ct);
      agent.isStopped = true;
      _view.PlayMove(false);
    }

    private static bool Arrived (NavMeshAgent agent) {
      if (agent.pathPending)
        return false;

      return agent.remainingDistance <= agent.stoppingDistance + 0.1f;
    }

    private async UniTask StartGatheringAsync (CancellationToken ct) {
      _state = State.Gathering;
      _view.Agent.isStopped = true;
      _view.PlayMove(false);

      while (!_cts.IsCancellationRequested && _zone.Provider.HasResource && _inventory.Get(_zone.Provider.Type) < _inventoryCapacity) {
        _view.PlayHit();
        var stack = _zone.Provider.GatherOnce(_maxPerHit);

        if (stack.Amount > 0)
          _inventory.Add(stack.Type, stack.Amount);

        await UniTask.Delay(TimeSpan.FromSeconds(_hitInterval), cancellationToken: ct);
      }

      _state = State.MovingToDropOff;
    }

    private async UniTask DeliverAsync (CancellationToken ct) {
      foreach (ResourceType type in Enum.GetValues(typeof(ResourceType))) {
        int amount = _inventory.Get(type);

        if (amount > 0) {
          _dropOff.Accept(new ResourceStack(type, amount));
          _inventory.Set(type, 0);
        }
      }

      _view.PlayMove(false);
      await UniTask.Yield(ct);
      _state = State.Delivering;
    }

    private void SafeStopAgent() {
      if (_view.Agent != null) {
        _view.Agent.isStopped = true;
        _view.Agent.ResetPath();
      }
    }
  }
}
