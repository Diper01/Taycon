using System;
using System.Collections.Generic;
using _Game._Scripts.DataTypes.Resources;
namespace _Game._Scripts.Features.Inventory.WorkerInventory {
  public class WorkerInventory : IInventory {
    private readonly Dictionary<ResourceType, int> _resources = new();
    private readonly int _capacity;
    private IInventory _inventoryImplementation;

    public event Action<ResourceType, int> OnResourceChanged;

    public WorkerInventory (int capacity = 10) {
      _capacity = capacity;
    }

    public int Get (ResourceType type)
      => _resources.TryGetValue(type, out var value) ? value : 0;

    void IInventory.Set (ResourceType type, int amount) {
      _inventoryImplementation.Set(type, amount);
    }

    int IInventory.Add (ResourceType type, int amount) {
      return _inventoryImplementation.Add(type, amount);
    }

    bool IInventory.CanSpend (ResourceType type, int amount) {
      return _inventoryImplementation.CanSpend(type, amount);
    }

    bool IInventory.Spend (ResourceType type, int amount) {
      return _inventoryImplementation.Spend(type, amount);
    }

    int IInventory.Get (ResourceType type) {
      return _inventoryImplementation.Get(type);
    }

    public void Set (ResourceType type, int amount) {
      _resources[type] = amount;
      OnResourceChanged?.Invoke(type, amount);
    }

    public int Add (ResourceType type, int amount) {
      int current = Get(type);
      int total = CountAll();

      if (total >= _capacity)
        return 0;

      int free = _capacity - total;
      int added = Math.Min(amount, free);
      _resources[type] = current + added;
      OnResourceChanged?.Invoke(type, _resources[type]);
      return added;
    }

    public bool CanSpend (ResourceType type, int amount)
      => Get(type) >= amount;

    public bool Spend (ResourceType type, int amount) {
      if (!CanSpend(type, amount))
        return false;

      _resources[type] = Get(type) - amount;
      OnResourceChanged?.Invoke(type, _resources[type]);
      return true;
    }

    event Action<ResourceType, int> IInventory.OnResourceChanged {
      add {
        _inventoryImplementation.OnResourceChanged += value;
      }
      remove {
        _inventoryImplementation.OnResourceChanged -= value;
      }
    }
    public bool HasCost (params ResourceCost [] costs) {
      foreach (var cost in costs)
        if (Get(cost.type) < cost.amount)
          return false;

      return true;
    }

    public bool TrySpend (params ResourceCost [] costs) {
      if (!HasCost(costs))
        return false;

      foreach (var cost in costs)
        Spend(cost.type, cost.amount);

      return true;
    }

    private int CountAll() {
      int sum = 0;

      foreach (var kvp in _resources)
        sum += kvp.Value;

      return sum;
    }
  }
}
