using _Game._Scripts.DataTypes;
using _Game._Scripts.DataTypes.Resources;
using _Game._Scripts.DataTypes.Zones;
using _Game._Scripts.Features.GatheringZone.Shared.Factory;
using _Game._Scripts.Features.GatheringZone.Shared.Factory.GatherZoneFactory;
using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone.Shared.Spawner {
  public class ZoneSpawner : MonoBehaviour {
    [SerializeField] private GatherZoneFactoryBase minerZoneFactory;
    [SerializeField] private Transform parent;

    private void Start() {
      ZoneSpawnArgs args = new ZoneSpawnArgs(ResourceType.Copper, parent, new Vector3(0, 0, 0), Quaternion.identity);
      GameObject zone = minerZoneFactory.Create(args);
    }
  }
}
