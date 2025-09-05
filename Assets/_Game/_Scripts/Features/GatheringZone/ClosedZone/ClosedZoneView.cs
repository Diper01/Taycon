using System;
using _Game._Scripts.Features.Resources.PriceView;
using UnityEngine;
using UnityEngine.UI;
namespace _Game._Scripts.Features.GatheringZone.ClosedZone {
  public class ClosedZoneView : MonoBehaviour, IClosedZoneView {
    
    [SerializeField] private ResourcePriceSlotsView _resourcePriceSlotsView;
    public ResourcePriceSlotsView ResourcePriceSlotsView => _resourcePriceSlotsView;

    
    [SerializeField] private Button _buyButton;
    private IClosedZoneView _closedZoneViewImplementation;

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
