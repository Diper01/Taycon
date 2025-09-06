using _Game._Scripts.DataTypes;
using _Game._Scripts.DataTypes.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace _Game._Scripts.Features.Inventory.UI {
  public class InventoryItemView : MonoBehaviour {
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text amountText;

    public ResourceType Type { get; private set; }

    public void Setup(ResourceType type, Sprite sprite, int amount) {
      Type = type;
      if (icon != null) {
        icon.sprite = sprite;
        icon.enabled = sprite != null;
        icon.preserveAspect = true;
      }
      SetAmount(amount);
      gameObject.name = $"Item_{type}";
    }

    public void SetAmount(int amount) {
      if (amountText != null)
        amountText.text = amount.ToString();
    }
  }
}
