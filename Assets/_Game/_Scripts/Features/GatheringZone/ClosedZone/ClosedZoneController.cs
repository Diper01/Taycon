using System;
namespace _Game._Scripts.Features.GatheringZone.ClosedZone {
  public class ClosedZoneController {
    private readonly IClosedZoneView _view;
    private readonly Action<int> _onPurchased;

    public ClosedZoneController(IClosedZoneView view, Action<int> onPurchased) {
      _view = view;
      _onPurchased = onPurchased;
      _view.BuyClicked += HandleBuyClick;
    }

    private void HandleBuyClick() {
      _onPurchased?.Invoke(_view.Index);
    }
  }
}
