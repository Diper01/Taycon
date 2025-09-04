using System.Collections.Generic;
using System.Linq;
using _Game._Scripts.DataTypes;
using _Game._Scripts.DataTypes.Resources;
using _Game._Scripts.Utils;
using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone.Shared {
  public class GatherZonePresenter {
    private IGatherZoneView _view;
    public GatherZonePresenter (IGatherZoneView view, ResourceType type) {
      _view = view;
      SpawnResource(type);
    }

    private void SpawnResource(ResourceType type) {
      List<Transform> points = _view.ParentForResourcePoint.Cast<Transform>().ToList();
      var prefab = ResourceUtils.GetPrefab(type);

      foreach (var point in points) {
        Object.Instantiate(prefab, point.position, point.rotation, point.parent);
      }
    }
    
  }
}
