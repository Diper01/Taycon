using _Game._Scripts.DataTypes;
using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone {
  public interface IGatherZoneView {
    public Transform PointToMain {get;}
    public Transform ParentForResourcePoint {get;}

    void Initialize (ResourceType argsType);
  }
}
