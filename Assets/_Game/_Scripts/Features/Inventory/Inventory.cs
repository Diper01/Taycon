using System;
using System.Collections.Generic;
using _Game._Scripts.DataTypes;
using _Game._Scripts.DataTypes.Resources;
using UnityEngine;
namespace _Game._Scripts.Features.Inventory {
  public class Inventory : MonoBehaviour, IInventory {
    [Serializable]
    public class StartEntry {
      public ResourceType type;
      public int amount = 0;
    }

    [Header("Start Resources")]
    [SerializeField] private List<StartEntry> start = new();

    private readonly Dictionary<ResourceType, int> _map = new();

    public event Action<ResourceType, int> OnResourceChanged;

    private void Awake() {
      _map.Clear();

      foreach (var e in start)
        _map[e.type] = Mathf.Max(0, e.amount);

      foreach (var kv in _map)
        OnResourceChanged?.Invoke(kv.Key, kv.Value);
    }

    public int Get (ResourceType type)
      => _map.GetValueOrDefault(type, 0);

    public void Set (ResourceType type, int amount) {
      amount = Mathf.Max(0, amount);
      _map[type] = amount;
      OnResourceChanged?.Invoke(type, amount);
    }

    public int Add (ResourceType type, int amount) {
      if (amount == 0)
        return 0;

      int cur = Get(type);
      long target = (long)cur + amount;
      int newVal = Mathf.Max(0, (int)Mathf.Clamp(target, 0, int.MaxValue));
      int delta = newVal - cur;

      if (delta != 0)
        Set(type, newVal);

      return delta;
    }

    public bool CanSpend (ResourceType type, int amount)
      => amount >= 0 && Get(type) >= amount;

    public bool Spend (ResourceType type, int amount) {
      if (!CanSpend(type, amount))
        return false;

      Set(type, Get(type) - amount);
      return true;
    }

    public bool HasCost (params ResourceCost [] costs) {
      if (costs == null)
        return true;

      foreach (var c in costs)
        if (!CanSpend(c.type, c.amount))
          return false;

      return true;
    }

    public bool TrySpend (params ResourceCost [] costs) {
      if (!HasCost(costs))
        return false;

      foreach (var c in costs)
        Spend(c.type, c.amount);

      return true;
    }
  }
}
