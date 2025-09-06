using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Game._Scripts.DataTypes.Resources;
using _Game._Scripts.Features.GatheringZone.СooperationZones.Provider;
using _Game._Scripts.Utils;
using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone.Shared {
  public class GatherZonePresenter {
    readonly IGatherZoneView _view;
    readonly List<GameObject> _spawned = new();
    IResourceProvider _provider;
    ResourceType _type;

    public GatherZonePresenter (IGatherZoneView view, ResourceType type) {
      _view = view;
      _type = type;

      SpawnResource(_type);
      _view.ZoneView.Provider.SetResourceType(_type);
      _view.WorkerSpawner.SpawnWorker(_view.GetDropOffPointView());

      _provider = _view.ZoneView.Provider;
      _provider.OnDepleted += HandleDepleted;
      _provider.OnRestored += HandleRestored;

      if (!_provider.HasResource)
        StartRespawn();
    }

    void HandleDepleted (IResourceProvider p)
      => StartRespawn();
    void HandleRestored (IResourceProvider p) {
      SpawnResource(_type);
    }

    void StartRespawn() {
      MonoBehaviour host = _view.ZoneView;
      host.StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine() {
      DespawnAll();
      yield return new WaitForSeconds(3f);
      _provider.Restore();
    }

    void SpawnResource (ResourceType type) {
      var points = _view.ParentForResourcePoint.Cast<Transform>().ToList();
      var prefab = ResourceUtils.GetPrefab(type);

      foreach (var point in points) {
        var res = Object.Instantiate(prefab, point.position, point.rotation, point.parent);
        res.transform.localScale = point.localScale;
        res.transform.SetParent(point);
        _spawned.Add(res);
      }
    }

    void DespawnAll() {
      for (int i = 0; i < _spawned.Count; i++) {
        var go = _spawned[i];

        if (go)
          Object.Destroy(go);
      }

      _spawned.Clear();
    }
  }
}
