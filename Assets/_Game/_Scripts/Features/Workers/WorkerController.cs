using _Game._Scripts.DataTypes.Resources;
using _Game._Scripts.Features.GatheringZone.СooperationZones;
using _Game._Scripts.Features.GatheringZone.СooperationZones.OutResource;
using _Game._Scripts.Features.Inventory.WorkerInventory;
using UnityEngine;
namespace _Game._Scripts.Features.Workers {
  public class WorkerController {
    public enum State { MovingToZone, Gathering, MovingToDropOff, Delivering }

    private readonly IWorkerView _view;
    private readonly WorkerInventory _inventory;
    private readonly ZoneView _zone;
    private readonly DropOffPointView _dropOff;

    private State _state;
    private float _hitTimer;
    private readonly int _maxPerHit = 2;
    private readonly float _hitInterval = 0.6f;

    public WorkerController (IWorkerView view, ZoneView zone, DropOffPointView dropOff, int capacity = 2) {
      _view = view;
      _zone = zone;
      _dropOff = dropOff;
      _inventory = new WorkerInventory(capacity);

      _state = State.MovingToZone;
      MoveToZone();
    }

    public void Tick (float deltaTime) {
      switch (_state) {
        case State.MovingToZone:
          if (Arrived(_zone.transform.position))
            StartGathering();

          break;
        case State.Gathering:
          _hitTimer += deltaTime;

          if (_hitTimer >= _hitInterval) {
            _hitTimer = 0f;
            _view.PlayHit();
            var stack = _zone.Provider.GatherOnce(_maxPerHit);

            if (stack.Amount > 0)
              _inventory.Add(stack.Type, stack.Amount);
          }

          if (!_zone.Provider.HasResource || _inventory.Get(_zone.Provider.Type) >= _inventoryCapacity) {
            _state = State.MovingToDropOff;
            MoveTo(_dropOff.transform.position);
          }

          break;
        case State.MovingToDropOff:
          if (Arrived(_dropOff.transform.position))
            Deliver();

          break;
        case State.Delivering:
          _state = State.MovingToZone;
          MoveToZone();
          break;
      }
    }

    private void MoveToZone() {
      MoveTo(_zone.transform.position);
      _view.PlayMove(true);
    }

    private void MoveTo (Vector3 target) {
      _view.Agent.isStopped = false;
      _view.Agent.SetDestination(target);
      _view.PlayMove(true);
    }

    private bool Arrived (Vector3 target) {
      var agent = _view.Agent;

      if (agent.pathPending)
        return false;

      return agent.remainingDistance <= agent.stoppingDistance + 0.1f;
    }

    private void StartGathering() {
      _view.Agent.isStopped = true;
      _view.PlayMove(false);
      _hitTimer = 0f;
      _state = State.Gathering;
    }

    private void Deliver() {
      foreach (ResourceType type in System.Enum.GetValues(typeof(ResourceType))) {
        int amount = _inventory.Get(type);

        if (amount > 0) {
          _dropOff.Accept(new ResourceStack(type, amount));
          _inventory.Set(type, 0);
        }
      }

      _view.PlayMove(false);
      _state = State.Delivering;
    }

    private int _inventoryCapacity
      => 10;
  }
}
