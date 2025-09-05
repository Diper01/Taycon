using System.Collections.Generic;
using _Game._Scripts.Core.Save.Service;
using _Game._Scripts.DataTypes.Resources;
using _Game._Scripts.DataTypes.Zones;
using _Game._Scripts.Features.GatheringZone.ClosedZone;
using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone.Controller {
  public class GatherZoneController : MonoBehaviour {
    [SerializeField] private Inventory.Inventory _inventory;
    [SerializeField] private ZoneDatabase _database;
    [SerializeField] private Transform _spawnRoot;
    [SerializeField] private Vector3 _offset = new Vector3(5, 0, 0);
    private readonly List<GameObject> _spawned = new();

    private int _nextIndex = 0;

    private void Start() {
      SpawnAllAccordingToSave();
    }

    private void SpawnAllAccordingToSave() {
      _spawned.Clear();
      _spawned.Add(SpawnOpen(0));
      int built = Mathf.Clamp(SaveService.GetBuiltZonesCount(), 0, _database.zones.Length);

      for (int i = 1; i < _database.zones.Length; i++) {
        if (i < built) {
          _spawned.Add(SpawnOpen(i));
        } else {
          _spawned.Add(SpawnClosed(i));
        }
      }
    }

    private GameObject SpawnClosed (int index) {
      var pos = _spawnRoot.position + _offset * index;
      var args = new ZoneSpawnArgs(ResourceType.None, _spawnRoot, pos, Quaternion.identity);
      var go = _database.closedZoneFactory.Create(args);

      var view = go.GetComponent<ClosedZoneView>();
      view.Init(index);

      var ctrl = new ClosedZoneController(view,_inventory,_database.zones[index].costs, OnPurchased);

      go.name = $"ClosedZone_{index:D2}";
      return go;
    }

    private void OnPurchased (int index) {
      SaveService.IncrementBuiltZonesCount();

      if (index >= 0 && index < _spawned.Count && _spawned[index] != null) {
        Destroy(_spawned[index]);
      }

      _spawned[index] = SpawnOpen(index);
    }
    private Vector3 GetPosByIndex (int index)
      => _spawnRoot.position + _offset * index;

    private GameObject SpawnOpen (int index) {
      ZoneData data = _database.zones[index];
      var pos = GetPosByIndex(index);
      var args = new ZoneSpawnArgs(data.resourceType, _spawnRoot, pos, Quaternion.identity);
      var zone = _database.zones[index].gatherZoneFactory.Create(args);
      zone.name = $"Zone_{index:D2}_{data.resourceType}";
      return zone;
    }



    private void SpawnAll() {
      while (_nextIndex < _database.zones.Length) {
        SpawnNext();
      }
    }

    private void SpawnNext() {
      if (_nextIndex >= _database.zones.Length)
        return;

      ZoneData data = _database.zones[_nextIndex];

      var pos = _spawnRoot.position + _offset * _nextIndex;

      var args = new ZoneSpawnArgs(data.resourceType, _spawnRoot, pos, Quaternion.identity);

      var zone = data.gatherZoneFactory.Create(args);
      _spawned.Add(zone);

      _nextIndex++;
    }
  }
}
