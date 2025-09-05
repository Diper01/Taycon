using System;
using UnityEngine;
using UnityEngine.UI;
namespace _Game._Scripts.Features.GatheringZone.ClosedZone {
  public class ClosedZoneView : MonoBehaviour, IClosedZoneView {
    [SerializeField] private Button _buyButton;

    public event Action BuyClicked;
    public int Index { get; private set; }

    public void Init(int index) {
      Index = index;

      if (_buyButton != null) {
        _buyButton.onClick.RemoveAllListeners();
        _buyButton.onClick.AddListener(() => BuyClicked?.Invoke());
      }
    }
  }
}
