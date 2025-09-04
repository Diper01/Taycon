using _Game._Scripts.Features.GatheringZone;
using _Game._Scripts.Features.GatheringZone.Shared.Factory;
using UnityEngine;
namespace _Game._Scripts.Features.MiningZones.MinerZone.Factory {
  public class ForestryZoneFactory : GatherZoneFactoryBase, IGatherZoneFactory {
    public GameObject Create(ZoneSpawnArgs args) {
      var zone = base.Create(args);
      IGatherZoneView gatherZoneView = zone.GetComponent<IGatherZoneView>();
      gatherZoneView.Initialize(args.Type);
      return zone;
    }
  }
}
