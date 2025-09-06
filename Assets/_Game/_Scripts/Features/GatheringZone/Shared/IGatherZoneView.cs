using _Game._Scripts.DataTypes.Resources;
using _Game._Scripts.Features.GatheringZone.СooperationZones;
using _Game._Scripts.Features.GatheringZone.СooperationZones.InResource;
using _Game._Scripts.Features.GatheringZone.СooperationZones.OutResource;
using _Game._Scripts.Features.Workers;
using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone.Shared {
  public interface IGatherZoneView {
    public Transform ParentForResourcePoint {get;}
    void Initialize (ResourceType argsType);
    
    public WorkerSpawner WorkerSpawner {get;}

    public DropOffPointView GetDropOffPointView();
    
    public ZoneView ZoneView {get;}
  }
}
