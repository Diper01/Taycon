using System;
using System.Collections.Generic;
using UnityEngine;
namespace _Game._Scripts.Features.UISwitcher {
  public class UISwitcher : MonoBehaviour {
    [Serializable]
    private struct UIStateEntry {
      public UIState state;
      public CanvasGroup canvasGroup;
    }

    [SerializeField] private List<UIStateEntry> _states = new();
    [SerializeField] private UIState _initialState = UIState.Game;

    private readonly Dictionary<UIState, CanvasGroup> _map = new();
    private UIState _current = UIState.None;

    private void Awake() {
      _map.Clear();

      foreach (var s in _states) {
        if (s.canvasGroup == null)
          continue;

        _map[s.state] = s.canvasGroup;
        HideCanvas(s.canvasGroup);
      }
    }

    private void Start() {
      if (_initialState != UIState.None)
        Show(_initialState);
    }

    public void Show (UIState state) {
      if (_current == state)
        return;

      if (_map.TryGetValue(_current, out var prev))
        HideCanvas(prev);

      if (_map.TryGetValue(state, out var next))
        ShowCanvas(next);

      _current = state;
    }
    
    public void Toggle (UIState a, UIState b) {
      Show(_current == a ? b : a);
    }

    private void ShowCanvas (CanvasGroup cg) {
      cg.alpha = 1f;
      cg.blocksRaycasts = true;
      cg.interactable = true;
      cg.gameObject.SetActive(true);
    }

    private void HideCanvas (CanvasGroup cg) {
      cg.alpha = 0f;
      cg.blocksRaycasts = false;
      cg.interactable = false;
      cg.gameObject.SetActive(false);
    }
  }
}
