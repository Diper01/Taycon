using _Game._Scripts.Features.GatheringZone.СooperationZones;
using _Game._Scripts.Features.GatheringZone.СooperationZones.OutResource;
using UnityEngine;
namespace _Game._Scripts.Features.Workers {
  public class WorkerSpawner : MonoBehaviour {
    [SerializeField] private WorkerView _workerPrefab;
    [SerializeField] private ZoneView _zone;
    [SerializeField] private DropOffPointView _dropOff;

    private WorkerController _controller;

    private void Start() {
      SpawnWorker();
    }

    private void Update() {
      _controller?.Tick(Time.deltaTime);
    }

    private void SpawnWorker() {
      var view = Instantiate(_workerPrefab, transform.position, Quaternion.identity);
      _controller = new WorkerController(view, _zone, _dropOff, capacity: 10);
    }
  }
}
