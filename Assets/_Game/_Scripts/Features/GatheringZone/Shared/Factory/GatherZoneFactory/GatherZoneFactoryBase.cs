using _Game._Scripts.DataTypes.Zones;
using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone.Shared.Factory.GatherZoneFactory {
  [CreateAssetMenu(fileName = "NewGatherZoneFactory", menuName = "Game/Gathering Zone/Factory Base", order = 0)]
  public class GatherZoneFactoryBase : ScriptableObject {
    [SerializeField] private GameObject _prefab;
    public virtual GameObject Create (ZoneSpawnArgs args) {
      var zone = Instantiate(_prefab, args.Position, args.Rotation, args.Parent);
      return zone;
    }
  }

}
