using _Game._Scripts.Features.GatheringZone.Shared.Factory;
using UnityEngine;
namespace _Game._Scripts.Features.MiningZones.MinerZone.Factory {
  public class MinerZoneFactory  : GatherZoneFactoryBase, IGatherZoneFactory {
    public GameObject Create(ZoneSpawnArgs args) {
      return base.Create(args);
    }
  }
}
