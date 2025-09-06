using _Game._Scripts.DataTypes.Resources;
using _Game._Scripts.Features.GatheringZone.СooperationZones.InResource;
using _Game._Scripts.Features.GatheringZone.СooperationZones.OutResource;
using _Game._Scripts.Utils;
using UnityEngine;
namespace _Game._Scripts.Features.Workers {
  public class WorkerSpawner : MonoBehaviour {
    [SerializeField] private ZoneView _zone;
    [SerializeField] private int _workerInventoryCapacity = 2;
    private DropOffPointView _dropOff;
    private WorkerController _controller;
    
    public void SpawnWorker(DropOffPointView dropOff,ResourceType resourceType) {
      _dropOff = dropOff;
      var workerPrefab = WorkerUtils.GetPrefabForResource(resourceType);
      var view = Instantiate(workerPrefab, _zone.SpawnSpot.position, Quaternion.identity);
      
      IWorkerView workerView = view.GetComponent<IWorkerView>();
      _controller = new WorkerController(workerView, _zone, _dropOff, _workerInventoryCapacity);
    }
  }
}
