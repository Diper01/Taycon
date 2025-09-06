using UnityEngine;
namespace _Game._Scripts.DataTypes.Workers {
  [System.Serializable]
  public class WorkerEntry {
    public string workerName;
    public GameObject prefab;
    public WorkerType type;
  }
}
