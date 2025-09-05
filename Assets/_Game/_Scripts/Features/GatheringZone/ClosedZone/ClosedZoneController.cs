using System;
using _Game._Scripts.DataTypes.Resources;
using _Game._Scripts.Features.Inventory;
namespace _Game._Scripts.Features.GatheringZone.ClosedZone {
  public class ClosedZoneController {
    private readonly IClosedZoneView _view;
    private readonly IInventory _inventory;
    private readonly ResourceCost [] _price;
    private readonly Action<int> _onPurchased;

    public ClosedZoneController (IClosedZoneView view, IInventory inventory, ResourceCost [] price, Action<int> onPurchased) {
      _view = view;
      _inventory = inventory;
      _price = price;
      _onPurchased = onPurchased;
      _view.BuyClicked += HandleBuyClick;
    }

    private void HandleBuyClick() {
      if (!_inventory.HasCost(_price)) {
        return;
      }

      if (!_inventory.TrySpend(_price))
        return;

      _onPurchased?.Invoke(_view.Index);
    }
  }
}
