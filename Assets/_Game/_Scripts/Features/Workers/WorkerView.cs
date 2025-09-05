using UnityEngine;
using UnityEngine.AI;
namespace _Game._Scripts.Features.Workers {
  [DisallowMultipleComponent]
  public class WorkerView : MonoBehaviour, IWorkerView {
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;

    public Transform Transform
      => transform;
    public NavMeshAgent Agent
      => _agent;

    private void Awake() {
      if (!_agent)
        _agent = GetComponent<NavMeshAgent>();
    }

    public void PlayMove (bool on) {
      if (_animator)
        _animator.SetFloat("MoveSpeed", on ? _agent.speed : 0f);
    }

    public void PlayHit() {
      if (_animator)
        _animator.SetTrigger("Hit");
    }

    public void SetCarry (bool on) {
      if (_animator)
        _animator.SetBool("Carry", on);
    }
  }

  public interface IWorkerView {
    Transform Transform { get; }
    NavMeshAgent Agent { get; }

    void PlayMove (bool on);

    void PlayHit();

    void SetCarry (bool on);
  }
}
