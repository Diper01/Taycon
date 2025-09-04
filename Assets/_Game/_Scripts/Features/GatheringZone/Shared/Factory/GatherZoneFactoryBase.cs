using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone.Shared.Factory {
  [CreateAssetMenu(fileName = "NewGatherZoneFactory", menuName = "Game/Gathering Zone/Factory Base", order = 0)]
  public class GatherZoneFactoryBase : ScriptableObject {
    [SerializeField] GameObject _prefab;
    public GameObject Create (ZoneSpawnArgs args) {
      var zone = Instantiate(_prefab, args.Position, args.Rotation, args.Parent);
      IGatherZoneView gatherZoneView = zone.GetComponent<IGatherZoneView>();
      gatherZoneView.Initialize(args.Type);
      return zone;
    }
  }

}
