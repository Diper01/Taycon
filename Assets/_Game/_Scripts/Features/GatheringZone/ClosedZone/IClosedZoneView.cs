using System;
namespace _Game._Scripts.Features.GatheringZone.ClosedZone {
  public interface IClosedZoneView {
    event Action BuyClicked;
    int Index { get; }
  }
}
