using _Game._Scripts.DataTypes.Resources;
using _Game._Scripts.Features.GatheringZone.СooperationZones;
using _Game._Scripts.Features.Workers;
using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone.Shared {
  public class GatherZoneViewBase : MonoBehaviour, IGatherZoneView {

    private GatherZonePresenter _presenter;
    private IGatherZoneView _gatherZoneViewImplementation;
    private DropOffPointView _dropOffPointView;
    
    
    [SerializeField] private Transform _parentForResourcePoint;
    [SerializeField] private WorkerSpawner _workerSpawner;
    public Transform ParentForResourcePoint
      => _parentForResourcePoint;
    
    public WorkerSpawner WorkerSpawner {
      get {
        return _workerSpawner;
      }
    }

    public DropOffPointView GetDropOffPointView() {
      return _dropOffPointView ?? FindObjectOfType<DropOffPointView>();
    }

    public void Initialize (ResourceType type) {
      _presenter ??= new GatherZonePresenter(this, type);
    }

  }
}
