using UnityEngine;
using UnityEngine.UI;
namespace _Game._Scripts.Features.UISwitcher {
  [RequireComponent(typeof(Button))]
  public class UIShowButton : MonoBehaviour {
    [SerializeField] private UISwitcher switcher;
    [SerializeField] private UIState target;

    private void Awake() {
      var btn = GetComponent<Button>();
      btn.onClick.AddListener(() => switcher.Show(target));
    }
  }
}
