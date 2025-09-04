using System.Collections.Generic;
using UnityEngine;
namespace _Game._Scripts.DataTypes.Resources {
  [CreateAssetMenu(fileName = "ResourcesDatabase", menuName = "Game/Resources Database", order = 1)]
  public class ResourcesData : ScriptableObject
  {
    public List<ResourceEntry> resources;
  }

  [System.Serializable]
  public class ResourceEntry
  {
    public string resourceName;
    public Sprite icon;
    public GameObject prefab;
    public ResourceType type;
  }
}
