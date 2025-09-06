using System.Collections.Generic;
using UnityEngine;
namespace _Game._Scripts.DataTypes.Workers {
  [CreateAssetMenu(fileName = "Workers_Database", menuName = "Game/Workers/Database")]
  public class WorkersData : ScriptableObject {
    public List<WorkerEntry> workers;
  }
}
