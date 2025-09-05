using System;
using UnityEngine;
using UnityEngine.UI;
namespace _Game._Scripts.Features.GatheringZone.ClosedZone {
  public class ClosedZoneView : MonoBehaviour {
    [SerializeField] private Button _buyButton;

    private Action<int> _onPurchased;
    private int _index;

    public void Init (int index, Action<int> onPurchased) {
      _index = index;
      _onPurchased = onPurchased;

      if (_buyButton != null) {
        _buyButton.onClick.RemoveAllListeners();
        _buyButton.onClick.AddListener(() => _onPurchased?.Invoke(_index));
      }
    }
  }
}
