using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone.Controller {
  public interface IZoneSpawner {
    GameObject SpawnOpen (int index);

    GameObject SpawnClosed (int index, System.Action<int> onPurchased);

    void ReplaceWithOpen (int index);

    GameObject GetAt (int index);

    void DestroyAt (int index);

    Vector3 GetPosByIndex (int index);

    int Count { get; }
  }
}
