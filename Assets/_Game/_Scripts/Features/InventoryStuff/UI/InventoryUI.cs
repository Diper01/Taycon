using System;
using System.Collections.Generic;
using _Game._Scripts.DataTypes;
using _Game._Scripts.DataTypes.Resources;
using _Game._Scripts.Utils;
using UnityEngine;
namespace _Game._Scripts.Features.Inventory.UI {
 public class InventoryUI : MonoBehaviour {
    [SerializeField] private InventoryStuff.Inventory inventory;
    [SerializeField] private Transform content;
    [SerializeField] private InventoryItemView itemPrefab;

    [SerializeField] private bool hideZeroAmounts = true;

    private readonly Dictionary<ResourceType, InventoryItemView> _views = new();

    private void OnEnable() {
      if (inventory == null)
        inventory = GetComponentInParent<InventoryStuff.Inventory>();

      BuildAllKnownTypes();

      if (inventory != null) {
        inventory.OnResourceChanged += HandleChanged;

        foreach (var type in _views.Keys) {
          HandleChanged(type, inventory.Get(type));
        }
      }
    }

    private void OnDisable() {
      if (inventory != null)
        inventory.OnResourceChanged -= HandleChanged;
    }

    private void BuildAllKnownTypes() {
      if (content == null || itemPrefab == null) return;

      var allTypes = (ResourceType[])Enum.GetValues(typeof(ResourceType));
      foreach (var type in allTypes) {
        if (Convert.ToInt32(type) == 0) continue;
        if (Convert.ToInt32(type) == 0) continue;
        if (_views.ContainsKey(type)) continue;

        var view = Instantiate(itemPrefab, content);
        view.Setup(type, ResourceUtils.GetIcon(type), 0);
        _views[type] = view;

        if (hideZeroAmounts)
          view.gameObject.SetActive(false);
      }
    }

    private void HandleChanged(ResourceType type, int amount) {
      if (!_views.TryGetValue(type, out var view)) {
        if (content == null || itemPrefab == null) return;
        view = Instantiate(itemPrefab, content);
        view.Setup(type, ResourceUtils.GetIcon(type), amount);
        _views[type] = view;
      } else {
        view.SetAmount(amount);
      }

      if (hideZeroAmounts)
        view.gameObject.SetActive(amount > 0);
    }
  }
}
