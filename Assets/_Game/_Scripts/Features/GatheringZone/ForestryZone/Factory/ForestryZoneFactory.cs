using _Game._Scripts.DataTypes.Zones;
using _Game._Scripts.Features.GatheringZone.Shared;
using _Game._Scripts.Features.GatheringZone.Shared.Factory.GatherZoneFactory;
using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone.ForestryZone.Factory {

  [CreateAssetMenu(fileName = "ForestryZoneFactory", menuName = "Game/Gathering Zone/Forestry Zone Factory", order = 2)]
  public class ForestryZoneFactory : GatherZoneFactoryBase, IGatherZoneFactory {
    public override GameObject Create (ZoneSpawnArgs args) {
      var zone = base.Create(args);
      IGatherZoneView gatherZoneView = zone.GetComponent<IGatherZoneView>();
      gatherZoneView.Initialize(args.Type);
      return zone;
    }
  }
}
