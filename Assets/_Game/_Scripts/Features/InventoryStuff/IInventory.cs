using System;
using _Game._Scripts.DataTypes.Resources;
namespace _Game._Scripts.Features.InventoryStuff {
  public interface IInventory {
    event Action<ResourceType, int> OnResourceChanged;

    int Get (ResourceType type);

    void Set (ResourceType type, int amount);

    int Add (ResourceType type, int amount);

    bool CanSpend (ResourceType type, int amount);

    bool Spend (ResourceType type, int amount);

    bool HasCost (params ResourceCost [] costs);

    bool TrySpend (params ResourceCost [] costs);
  }
  
  
  [Serializable]
  public struct ResourceCost {
    public ResourceType type;
    public int amount;

    public ResourceCost (ResourceType type, int amount) {
      this.type = type;
      this.amount = amount;
    }

  }
}
