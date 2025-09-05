using System.Collections.Generic;
using _Game._Scripts.Core.Save.Service;
using _Game._Scripts.DataTypes.Resources;
using _Game._Scripts.DataTypes.Zones;
using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone.Controller {
  public class GatherZoneController : MonoBehaviour {
    [SerializeField] private ZoneDatabase _database;
    [SerializeField] private Transform _spawnRoot;
    [SerializeField] private Vector3 _offset = new Vector3(5, 0, 0);

    private readonly List<GameObject> _spawned = new();

    private int _nextIndex = 0;

    private void Start() {
      SpawnPurchased();
    }

    private void SpawnPurchased() {
      do {
        SpawnNext();
      } while (_nextIndex < SaveService.GetBuiltZonesCount());

      SpawnClosed();
    }

    private void SpawnClosed() {
      while (_nextIndex > SaveService.GetBuiltZonesCount()) {
        if (_nextIndex >= _database.zones.Length)
          return;
        
        var pos = _spawnRoot.position + _offset * _nextIndex;
        var args = new ZoneSpawnArgs(ResourceType.None, _spawnRoot, pos, Quaternion.identity);
        var zone = _database.closedZoneFactory.Create(args );
        _spawned.Add(zone);
        _nextIndex++;
      }
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
