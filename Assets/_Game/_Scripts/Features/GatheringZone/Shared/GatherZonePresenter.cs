using System.Collections.Generic;
using System.Linq;
using _Game._Scripts.DataTypes;
using _Game._Scripts.Utils;
using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone {
  public class GatherZonePresenter {
    private IGatherZoneView _view;
    public GatherZonePresenter (IGatherZoneView view, ResourceType type) {
      _view = view;
    }

    private void SpawnResource(ResourceType type) {
      List<Transform> points = _view.ParentForResourcePoint.Cast<Transform>().ToList();
      var prefab = ResourceUtils.GetPrefab(type);
    }
    
  }
}
