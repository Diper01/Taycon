using System.Collections.Generic;
using _Game._Scripts.DataTypes.Resources;
using _Game._Scripts.DataTypes.Zones;
using _Game._Scripts.Features.GatheringZone.ClosedZone;
using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone.Controller {
  public class ZoneSpawner : IZoneSpawner {
    private readonly ZoneDatabase _db;
    private readonly InventoryStuff.Inventory _inventory;
    private readonly Transform _root;
    private readonly Vector3 _offset;
    private readonly List<GameObject> _spawned;

    public ZoneSpawner (ZoneDatabase db, InventoryStuff.Inventory inventory, Transform root, Vector3 offset, int capacity) {
      _db = db;
      _inventory = inventory;
      _root = root;
      _offset = offset;
      _spawned = new List<GameObject>(capacity);

      for (int i = 0; i < capacity; i++)
        _spawned.Add(null);
    }

    public int Count
      => _spawned.Count;

    public Vector3 GetPosByIndex (int index)
      => _root.position + _offset * index;

    public GameObject GetAt (int index)
      => _spawned[index];

    public void DestroyAt (int index) {
      if (_spawned[index] != null)
        Object.Destroy(_spawned[index]);

      _spawned[index] = null;
    }

    public GameObject SpawnOpen (int index) {
      var data = _db.zones[index];
      var args = new ZoneSpawnArgs(data.resourceType, _root, GetPosByIndex(index), Quaternion.identity);
      var zone = data.gatherZoneFactory.Create(args);
      zone.name = $"Zone_{index:D2}_{data.resourceType}";
      _spawned[index] = zone;
      return zone;
    }

    public GameObject SpawnClosed (int index, System.Action<int> onPurchased) {
      var args = new ZoneSpawnArgs(ResourceType.None, _root, GetPosByIndex(index), Quaternion.identity);
      var go = _db.closedZoneFactory.Create(args);
      go.name = $"ClosedZone_{index:D2}";

      var view = go.GetComponent<ClosedZoneView>();
      view.Init(index);

      var price = _db.zones[index].costs;
      var ctrl = new ClosedZoneController(view, _inventory, price, onPurchased);

      _spawned[index] = go;
      return go;
    }

    public void ReplaceWithOpen (int index) {
      DestroyAt(index);
      SpawnOpen(index);
    }
  }
}
