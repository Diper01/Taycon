using _Game._Scripts.Features.GatheringZone;
using _Game._Scripts.Features.GatheringZone.Shared.Factory;
using UnityEngine;
namespace _Game._Scripts.Features.MiningZones.MinerZone.Factory {

  [CreateAssetMenu(fileName = "ForestryZoneFactory", menuName = "Game/Gathering Zone/Forestry Zone Factory", order = 2)]
  public class ForestryZoneFactory : GatherZoneFactoryBase, IGatherZoneFactory {
    public GameObject Create (ZoneSpawnArgs args) {
      var zone = base.Create(args);
      return zone;
    }
  }
}
