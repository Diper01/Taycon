using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Game._Scripts.DataTypes.Resources;
using _Game._Scripts.DataTypes.Workers;
using UnityEngine;
namespace _Game._Scripts.Utils {
  public static class WorkerUtils {
    private static Dictionary<WorkerType, WorkerEntry> _map;
    static readonly TaskCompletionSource<bool> _readyTcs = new();

    public static Task Ready
      => _readyTcs.Task;

    public static void Initialize (WorkersData db) {
      _map = db.workers?.ToDictionary(w => w.type) ?? new Dictionary<WorkerType, WorkerEntry>();

      if (!_readyTcs.Task.IsCompleted)
        _readyTcs.SetResult(true);
    }

    public static WorkerEntry Get (WorkerType type) {
      if (_map != null && _map.TryGetValue(type, out var entry))
        return entry;

      return null;
    }

    public static string GetName (WorkerType type)
      => Get(type)?.workerName ?? type.ToString();

    public static GameObject GetPrefab (WorkerType type)
      => Get(type)?.prefab;

    public static GameObject GetPrefabForResource (ResourceType resourceType) {
      switch (resourceType) {
        case ResourceType.Coal:
        case ResourceType.Copper:
        case ResourceType.Gold:
        case ResourceType.Diamond:
        case ResourceType.Emerald:
          return GetPrefab(WorkerType.Miner);

        case ResourceType.Wood:
        case ResourceType.Hardwood:
          return GetPrefab(WorkerType.Lumberjack);

        default:
          return GetPrefab(WorkerType.Miner);
      }
    }
  }

}
