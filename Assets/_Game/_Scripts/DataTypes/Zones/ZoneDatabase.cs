using _Game._Scripts.Features.GatheringZone.Shared.Factory.GatherZoneFactory;
using UnityEngine;
namespace _Game._Scripts.DataTypes.Zones {
  
  [CreateAssetMenu(menuName = "Game/Zones/ZoneDatabase", fileName = "ZoneDatabase")]
  public class ZoneDatabase : ScriptableObject {
    public ZoneData [] zones;
    
    public GatherZoneFactoryBase closedZoneFactory;
  }
}
