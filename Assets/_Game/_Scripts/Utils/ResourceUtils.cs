using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Game._Scripts.DataTypes;
using UnityEngine;
namespace _Game._Scripts.Utils {
  public static class ResourceUtils {
    static Dictionary<ResourceType, ResourceEntry> _map;
    static TaskCompletionSource<bool> _readyTcs = new();

    public static Task Ready
      => _readyTcs.Task;

    public static void Initialize (ResourcesData db) {
      _map = db.resources?.ToDictionary(r => r.type) ?? new Dictionary<ResourceType, ResourceEntry>();

      if (!_readyTcs.Task.IsCompleted)
        _readyTcs.SetResult(true);
    }

    public static ResourceEntry Get (ResourceType type) {
      if (_map != null && _map.TryGetValue(type, out var entry))
        return entry;

      return null;
    }

    public static string GetName (ResourceType type)
      => Get(type)?.resourceName ?? type.ToString();
    public static Sprite GetIcon (ResourceType type)
      => Get(type)?.icon;
    public static GameObject GetPrefab (ResourceType type)
      => Get(type)?.prefab;
  }
}
