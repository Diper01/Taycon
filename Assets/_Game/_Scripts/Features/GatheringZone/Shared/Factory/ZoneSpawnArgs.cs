using _Game._Scripts.DataTypes;
using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone.Shared.Factory {
  public class ZoneSpawnArgs {
    private readonly ResourceType _resourceType;
    private readonly Transform _parent;
    private readonly Vector3 _position;
    private readonly Quaternion _rotation;
    
    public ResourceType Type => _resourceType;
    public Transform Parent => _parent;
    public Vector3 Position => _position;
    public Quaternion Rotation => _rotation;
    
    public ZoneSpawnArgs(ResourceType resourceType, Transform parent, Vector3 position, Quaternion rotation) {
      this._resourceType = resourceType;
      this._parent = parent;
      this._position = position;
      this._rotation = rotation;
    }
  }
}
