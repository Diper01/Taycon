using _Game._Scripts.DataTypes.Zones;
using _Game._Scripts.Features.GatheringZone.Shared.Factory;
using _Game._Scripts.Features.GatheringZone.Shared.Factory.GatherZoneFactory;
using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone.MinerZone.Factory {
  [CreateAssetMenu(fileName = "MinerZoneFactory", menuName = "Game/Gathering Zone/Miner Zone Factory", order = 1)]
  public class MinerZoneFactory : GatherZoneFactoryBase, IGatherZoneFactory {
    public GameObject Create (ZoneSpawnArgs args) {
      var zone = base.Create(args);
      return zone;
    }
  }
}
