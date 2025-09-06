using _Game._Scripts.DataTypes.Resources;
using _Game._Scripts.Features.GatheringZone.Shared.Factory.GatherZoneFactory;
using _Game._Scripts.Features.Inventory;
using _Game._Scripts.Features.InventoryStuff;
namespace _Game._Scripts.DataTypes.Zones {
  
  [System.Serializable]
  public class ZoneData {
    public string zoneId;
    public ResourceCost [] costs;
    public ResourceType resourceType;
    public GatherZoneFactoryBase gatherZoneFactory;
      
  }
}
