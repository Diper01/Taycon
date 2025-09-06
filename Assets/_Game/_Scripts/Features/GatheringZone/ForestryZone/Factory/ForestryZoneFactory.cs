using _Game._Scripts.DataTypes.Zones;
using _Game._Scripts.Features.GatheringZone.Shared;
using _Game._Scripts.Features.GatheringZone.Shared.Factory.GatherZoneFactory;
using _Game._Scripts.Features.GatheringZone.СooperationZones.Provider;
using UnityEngine;

namespace _Game._Scripts.Features.GatheringZone.ForestryZone.Factory {

  [CreateAssetMenu(fileName = "ForestryZoneFactory", menuName = "Game/Gathering Zone/Forestry Zone Factory", order = 2)]
  public class ForestryZoneFactory : GatherZoneFactoryBase, IGatherZoneFactory {
    [SerializeField] private float _respawnDelay = 3f;

    public override GameObject Create (ZoneSpawnArgs args) {
      var zone = base.Create(args);
      var view = zone.GetComponent<IGatherZoneView>();
      view.Initialize(args.Type);

      var provider = view.ZoneView.Provider;
      provider.OnDepleted += Handler;

      if (!provider.HasResource)
        Handler(provider);

      return zone;

      void Handler (IResourceProvider _) {
        provider.OnDepleted -= Handler;
      }
    }
  }
}
