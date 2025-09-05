using _Game._Scripts.Core.Save.Data;
using _Game._Scripts.DataTypes.Resources;
using Cysharp.Threading.Tasks;

namespace _Game._Scripts.Core.Save.Service {
  public static class SaveService {
    private static ISaveStorage _storage;
    private static SaveData _cache;
    static bool _initialized;
    
    
    public static async UniTask SaveAsync() => await _storage.SaveAsync(_cache);
    public static void Save() => _storage.Save(_cache);
    public static async UniTask InitAsync() {
      if (_initialized) return;
      _storage = new JsonFileStorage();
      _cache = await _storage.LoadAsync();
      _initialized = true;
    }
    public static int GetBuiltZonesCount()
      => _cache.BuiltZonesCount;

    public static void IncrementBuiltZonesCount(int delta = 1) { _cache.BuiltZonesCount += delta; }

    public static int GetResource(ResourceType type)
      => _cache.GetAmount(type);

    public static void SetResource (ResourceType type, int amount) {
      _cache.SetAmount(type, amount);
    }
    
    public static SaveData Snapshot()
      => _cache;

    public static void Overwrite(SaveData newData) { _cache = newData ?? new SaveData(); }
  }
}
