using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone {
  public class GatherZoneViewBase : MonoBehaviour, IGatherZoneView {

    [SerializeField] private Transform _pointToMain;
    [SerializeField] private Transform _parentForResourcePoint;
    
    
    public Transform PointToMain => _pointToMain;
    public Transform ParentForResourcePoint => _parentForResourcePoint;

  }
}
