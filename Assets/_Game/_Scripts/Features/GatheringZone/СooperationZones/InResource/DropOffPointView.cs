using System;
using _Game._Scripts.DataTypes.Resources;
using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone.СooperationZones {
  public class DropOffPointView : MonoBehaviour {
    public event Action<ResourceStack> OnDelivered;

    public void Accept (ResourceStack stack) {
      if (stack.Amount <= 0)
        return;

      Debug.Log($"[DropOff] +{stack.Amount} {stack.Type} at {name}");
      OnDelivered?.Invoke(stack);
    }
  }
}
