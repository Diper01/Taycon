using System;
using _Game._Scripts.DataTypes.Resources;
namespace _Game._Scripts.Features.GatheringZone.СooperationZones.Provider {
  public interface IResourceProvider {
    bool HasResource { get; }
    ResourceType Type { get; }

    ResourceStack GatherOnce (int maxAmount);

    void SetResourceType (ResourceType type);

    event Action<IResourceProvider> OnDepleted;
    event Action<IResourceProvider> OnRestored;

    void Restore();
  }
}
