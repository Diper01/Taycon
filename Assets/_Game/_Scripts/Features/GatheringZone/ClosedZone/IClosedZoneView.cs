using System;
using _Game._Scripts.Features.Resources.PriceView;
namespace _Game._Scripts.Features.GatheringZone.ClosedZone {
  public interface IClosedZoneView {
    event Action BuyClicked;
    int Index { get; }

    ResourcePriceSlotsView ResourcePriceSlotsView {get;}
  }
}
