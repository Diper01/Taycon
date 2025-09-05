using System.Collections.Generic;
using _Game._Scripts.DataTypes.Resources;
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

    public void Show (List<ResourceStack> stacks) {
      HideAll();

      if (stacks == null || stacks.Count == 0)
        return;

      EnsurePool(stacks.Count);

      for (int i = 0; i < stacks.Count; i++) {
        var s = stacks[i];
        var slot = _pool[i];
        slot.Show();
        slot.transform.SetParent(_container, false);

        slot.Set(s.Type, s.Amount);
      }

      _activeCount = stacks.Count;
    }

    public void HideAll() {
      for (int i = 0; i < _pool.Count; i++)
        _pool[i].Hide();

      _activeCount = 0;
    }

    public void EnsurePool (int size) {
      while (_pool.Count < size) {
        var slot = Instantiate(_slotPrefab, _container);
        slot.Hide();
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
