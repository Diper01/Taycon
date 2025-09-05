using _Game._Scripts.DataTypes.Resources;
using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone.Shared {
  public class GatherZoneViewBase : MonoBehaviour, IGatherZoneView {

    [SerializeField] private Transform _pointToMain;
    [SerializeField] private Transform _parentForResourcePoint;
    private GatherZonePresenter _presenter;
    
    public Transform PointToMain => _pointToMain;
    public Transform ParentForResourcePoint => _parentForResourcePoint;

    public void Initialize (ResourceType type) {
      _presenter ??= new GatherZonePresenter(this, type);
    }
  }
}
