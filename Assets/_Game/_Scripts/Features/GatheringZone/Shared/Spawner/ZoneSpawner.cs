using _Game._Scripts.DataTypes;
using _Game._Scripts.Features.GatheringZone.Shared.Factory;
using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone.Shared.Spawner {
  public class ZoneSpawner : MonoBehaviour {
    [SerializeField] private GatherZoneFactoryBase minerZoneFactory;
    [SerializeField] private Transform parent;

    private void Start() {
      ZoneSpawnArgs args = new ZoneSpawnArgs(ResourceType.Stone, parent, new Vector3(0, 0, 0), Quaternion.identity);

      GameObject zone = minerZoneFactory.Create(args);
    }
  }
}
