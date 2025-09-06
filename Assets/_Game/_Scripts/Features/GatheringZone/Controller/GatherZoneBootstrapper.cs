using _Game._Scripts.Core.Save.Service;
using _Game._Scripts.DataTypes.Zones;
using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone.Controller {
  public class GatherZoneBootstrapper : MonoBehaviour {
    [SerializeField] private InventoryStuff.Inventory _inventory;
    [SerializeField] private ZoneDatabase _database;
    [SerializeField] private Transform _spawnRoot;
    [SerializeField] private Vector3 _offset = new Vector3(0, 0, 5);

    private IZoneSpawner _spawner;

    private void Start() {
      _spawner = new ZoneSpawner(_database, _inventory, _spawnRoot, _offset, _database.zones.Length);
      BuildInitialLayout();
    }

    private void BuildInitialLayout() {
      _spawner.SpawnOpen(0);

      int built = Mathf.Clamp(SaveService.GetBuiltZonesCount(), 0, _database.zones.Length);

      for (int i = 1; i < _database.zones.Length; i++) {
        if (i < built) {
          _spawner.SpawnOpen(i);
        } else {
          _spawner.SpawnClosed(i, OnPurchased);
        }
      }
    }

    private void OnPurchased (int index) {
      SaveService.IncrementBuiltZonesCount();

      _spawner.ReplaceWithOpen(index);
    }
  }
}
