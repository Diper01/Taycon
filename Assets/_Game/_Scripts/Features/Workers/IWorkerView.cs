using UnityEngine;
using UnityEngine.AI;
namespace _Game._Scripts.Features.Workers {
  public interface IWorkerView {
    Transform Transform { get; }
    NavMeshAgent Agent { get; }
  
    void PlayMove (bool on);
  
    void PlayHit();
  
    void SetCarry (bool on);
  }
}
