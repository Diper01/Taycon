using _Game._Scripts.DataTypes.Resources;
using _Game._Scripts.Features.InventoryStuff;
using _Game._Scripts.Features.Resources.PriceView;
using _Game._Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace _Game._Scripts.Features.Crafting.ItemView {
  public class CraftingRecipeItemView : MonoBehaviour {
    [SerializeField] private ResourcePriceSlotsView _inputsView;
    [SerializeField] private Image _outputIcon;
    [SerializeField] private TMP_Text _outputAmountText;
    [SerializeField] private Button _craftButton;

    private IInventory _inventory;
    private CraftingSystem _crafting;
    private CraftingRecipe _recipe;

    public void Setup (CraftingRecipe recipe, IInventory inventory, CraftingSystem crafting) {
      _recipe = recipe;
      _inventory = inventory;
      _crafting = crafting;

      _inputsView.HideAll();
      _inputsView.Show(recipe.inputs);

      _outputIcon.sprite = ResourceUtils.GetIcon(recipe.output);
      _outputIcon.enabled = _outputIcon.sprite != null;

      if (_outputAmountText)
        _outputAmountText.text = "x" + recipe.outputAmount;

      if (_inventory != null)
        _inventory.OnResourceChanged += OnInvChanged;

      if (_craftButton != null) {
        _craftButton.onClick.RemoveAllListeners();
        _craftButton.onClick.AddListener(DoCraft);
      }

      RefreshState();
    }

    private void OnDestroy() {
      if (_inventory != null)
        _inventory.OnResourceChanged -= OnInvChanged;

      if (_craftButton != null)
        _craftButton.onClick.RemoveAllListeners();
    }

    private void OnInvChanged (ResourceType type, int amount) {
      RefreshState();
    }

    private void RefreshState() {
      bool can = _crafting != null && _crafting.CanCraft(_recipe.output);

      if (_craftButton)
        _craftButton.interactable = can;
    }

    private void DoCraft() {
      if (_crafting.TryCraft(_recipe.output)) {
        RefreshState();
      }
    }
  }
}
