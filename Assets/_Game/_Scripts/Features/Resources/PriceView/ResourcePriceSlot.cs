using _Game._Scripts.DataTypes.Resources;
using _Game._Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace _Game._Scripts.Features.Resources.PriceView {
  [DisallowMultipleComponent]
  public class ResourcePriceSlot : MonoBehaviour {
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _amountText;
    [SerializeField] private CanvasGroup _canvasGroup;
    public void Set (ResourceType type, int amount) {
      if (_image) {
        var icon = ResourceUtils.GetIcon(type);

        if (icon)
          _image.sprite = icon;
      }

      if (_amountText)
        _amountText.text = $"x{Mathf.Max(0, amount)}";
    }
    public void Show()
      => _canvasGroup.alpha = 1f;
    
    public void Hide()
      => _canvasGroup.alpha = 0f;
    
  }
}
