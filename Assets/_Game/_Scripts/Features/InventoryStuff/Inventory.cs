using System;
using System.Collections.Generic;
using _Game._Scripts.Core.Save.Service;
using _Game._Scripts.DataTypes.Resources;
using _Game._Scripts.Features.GatheringZone.СooperationZones.InResource;
using UnityEngine;

namespace _Game._Scripts.Features.InventoryStuff {
  public class Inventory : MonoBehaviour, IInventory {
    [SerializeField] private DropOffPointView _dropOffPointView;

    private readonly Dictionary<ResourceType, int> _map = new();
    public event Action<ResourceType, int> OnResourceChanged;

    private void Awake() {
      _map.Clear();

      LoadFromSave();

      foreach (var kv in _map)
        OnResourceChanged?.Invoke(kv.Key, kv.Value);

      Subscribe();
    }

    private void LoadFromSave() {
      foreach (ResourceType type in Enum.GetValues(typeof(ResourceType))) {
        if (type == ResourceType.None)
          continue;

        int saved = SaveService.GetResource(type);
        _map[type] = Mathf.Max(0, saved);
      }
    }

    public int Get (ResourceType type)
      => _map.GetValueOrDefault(type, 0);

    public void Set (ResourceType type, int amount) {
      amount = Mathf.Max(0, amount);
      _map[type] = amount;
      SaveService.SetResource(type, amount);
      OnResourceChanged?.Invoke(type, amount);
    }

    public int Add (ResourceType type, int amount) {
      if (amount == 0)
        return 0;

      int cur = Get(type);
      long target = (long)cur + amount;
      int newVal = Mathf.Clamp((int)target, 0, int.MaxValue);
      int delta = newVal - cur;

      if (delta != 0)
        Set(type, newVal);

      return delta;
    }

    private void Add (ResourceStack stack) {
      if (stack.Amount > 0)
        Add(stack.Type, stack.Amount);
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

    private void Subscribe() {
      _dropOffPointView.OnDelivered += Add;
    }

    private void UnSubscribe() {
      _dropOffPointView.OnDelivered -= Add;
    }

    private void OnDestroy() {
      UnSubscribe();
    }
  }
}
