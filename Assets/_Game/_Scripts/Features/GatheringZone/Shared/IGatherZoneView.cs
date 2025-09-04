using _Game._Scripts.DataTypes;
using _Game._Scripts.DataTypes.Resources;
using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone {
  public interface IGatherZoneView {
    public Transform PointToMain {get;}
    public Transform ParentForResourcePoint {get;}

    void Initialize (ResourceType argsType);
  }
}
