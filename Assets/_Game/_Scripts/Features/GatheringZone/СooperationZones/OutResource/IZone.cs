using _Game._Scripts.Features.GatheringZone.СooperationZones.Provider;
using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone.СooperationZones.OutResource {
  public interface IZone {
    bool TryReserve(out Transform workSpot);

    void Release();
    Transform Transform { get; }
    
    IResourceProvider Provider { get; }
  }
}
