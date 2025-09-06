using System.Collections.Generic;
using System.Linq;
using _Game._Scripts.DataTypes.Resources;
using _Game._Scripts.Utils;
using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone.Shared {
  public class GatherZonePresenter {
    private readonly IGatherZoneView _view;
    public GatherZonePresenter (IGatherZoneView view, ResourceType type) {
      _view = view;
      SpawnResource(type);
      _view.ZoneView.Provider.SetResourceType(type);
      _view.WorkerSpawner.SpawnWorker(_view.GetDropOffPointView());
    }
    private void SpawnResource (ResourceType type) {
      List<Transform> points = _view.ParentForResourcePoint.Cast<Transform>().ToList();
      GameObject prefab = ResourceUtils.GetPrefab(type);

      foreach (Transform point in points) {
        var res = Object.Instantiate(prefab, point.position, point.rotation, point.parent);
        res.transform.localScale = point.localScale;
        res.transform.SetParent(point);
      }
    }
  }
}
