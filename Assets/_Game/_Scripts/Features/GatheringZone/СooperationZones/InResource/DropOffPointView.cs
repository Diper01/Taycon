using System;
using _Game._Scripts.DataTypes.Resources;

using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone.СooperationZones.InResource {
  public class DropOffPointView : MonoBehaviour {
    
    [SerializeField] private InventoryStuff.Inventory _inventory;

    public event Action<ResourceStack> OnDelivered;

    public void Accept (ResourceStack stack) {
      if (stack.Amount <= 0)
        return;

      OnDelivered?.Invoke(stack);
    }
  }
}
