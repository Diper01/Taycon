using _Game._Scripts.Features.GatheringZone.СooperationZones.Provider;
using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone.СooperationZones.OutResource {
  public class ZoneView : MonoBehaviour, IZone {
    
    [SerializeField] private Transform _workSpot;
    [SerializeField] private Transform _spawnSpot;
    [SerializeField] private ResourceNodeView _provider;
    
    private bool _occupied;

    public bool TryReserve(out Transform spot) {
      if (_occupied) { spot = null; return false; }
      _occupied = true;
      spot = _workSpot;
      return true;
    }
    public void Release() => _occupied = false;

    public Transform WorkSpot => _workSpot;
    public Transform SpawnSpot => _spawnSpot;
    public IResourceProvider Provider => _provider;
  }
}
