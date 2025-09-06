using System.Collections.Generic;
using _Game._Scripts.DataTypes.Resources;
using _Game._Scripts.Features.Inventory;
using _Game._Scripts.Features.InventoryStuff;
using UnityEngine;
namespace _Game._Scripts.Features.Resources.PriceView {
  public class ResourcePriceSlotsView : MonoBehaviour {
    [SerializeField] private ResourcePriceSlot _slotPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private int _initialPool = 8;

    private readonly List<ResourcePriceSlot> _pool = new();
    private int _activeCount = 0;

    private void Awake() {
      if (!_container)
        _container = transform;

      EnsurePool(_initialPool);
      HideAll();
    }

    public void Show (ResourceCost [] price) {
      HideAll();

      if (price == null)
        return;

      EnsurePool(price.Length);

      for (int i = 0; i < price.Length; i++) {
        var p = price[i];
        var slot = _pool[i];
        slot.Enable();
        slot.transform.SetParent(_container, false);
        slot.Set(p.type, p.amount);
      }

      _activeCount = price.Length;
    }

    public void HideAll() {
      for (int i = 0; i < _pool.Count; i++)
        _pool[i].Disable();

      _activeCount = 0;
    }

    public void EnsurePool (int size) {
      while (_pool.Count < size) {
        var slot = Instantiate(_slotPrefab, _container);
        slot.Disable();
        _pool.Add(slot);
      }
    }

    public void SetAtIndex (int index, ResourceType type, int amount) {
      if (index < 0 || index >= _activeCount)
        return;

      _pool[index].Set(type, amount);
    }
  }
}
