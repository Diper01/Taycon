using System;
using _Game._Scripts.DataTypes.Resources;
using UnityEngine;

namespace _Game._Scripts.Features.GatheringZone.СooperationZones.Provider {

  public class ResourceNodeView : MonoBehaviour, IResourceProvider {
    [SerializeField] private ResourceType _type;
    [SerializeField] private int _totalUnits = 5;
    [SerializeField] private int _unitsPerHit = 1;
    [SerializeField] private bool _infinite = true;

    int _initialUnits;
    public bool HasResource
      => _infinite || _totalUnits > 0;
    public ResourceType Type
      => _type;

    public event Action<IResourceProvider> OnDepleted;
    public event Action<IResourceProvider> OnRestored;

    void Awake() {
      _initialUnits = Mathf.Max(0, _totalUnits);
    }

    public void SetResourceType (ResourceType type)
      => _type = type;

    public ResourceStack GatherOnce (int maxAmount) {
      if (!_infinite && _totalUnits <= 0)
        return new ResourceStack(_type, 0);

      int amount = Mathf.Min(_unitsPerHit, maxAmount, _infinite ? maxAmount : _totalUnits);

      if (!_infinite) {
        _totalUnits -= amount;

        if (_totalUnits <= 0)
          OnDepleted?.Invoke(this);
      }

      return new ResourceStack(_type, amount);
    }

    public void Restore() {
      if (_infinite) {
        OnRestored?.Invoke(this);
        return;
      }

      _totalUnits = _initialUnits;
      OnRestored?.Invoke(this);
    }
  }
}
