using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone.Shared.Factory {
  public class GatherZoneFactoryBase : ScriptableObject{
    [SerializeField] GameObject _prefab;
    public GameObject Create(ZoneSpawnArgs args) {
      //var zonePrefab = ResourceUtils.GetPrefab(args.Type);
      var go = Instantiate(_prefab, args.Position, args.Rotation, args.Parent);
      return go;
    }
  }
  
}
