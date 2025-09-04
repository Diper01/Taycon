#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEditor.SceneManagement;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Editor {
  [EditorToolbarElement(Id, typeof(SceneView))]
  public class ScenesDropdown : EditorToolbarDropdown {
    public const string Id = "MK/ScenesDropdown";

    public ScenesDropdown() {
      text = "Scenes";
      tooltip = "Open scene from Build Settings";
      clicked += ShowMenu;
    }

    void ShowMenu() {
      var menu = new GenericMenu();
      int count = SceneManager.sceneCountInBuildSettings;

      for (int i = 0; i < count; i++) {
        string path = SceneUtility.GetScenePathByBuildIndex(i);
        string name = Path.GetFileNameWithoutExtension(path);

        menu.AddItem(new GUIContent(name), false, () => {
          if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            EditorSceneManager.OpenScene(path);
        });
      }

      if (count == 0)
        menu.AddDisabledItem(new GUIContent("No scenes in Build Settings"));

      menu.ShowAsContext();
    }
  }

  [Overlay(typeof(SceneView), "Scenes Switcher")]
  public class SceneSwitcherOverlay : ToolbarOverlay {
    public SceneSwitcherOverlay()
      : base(ScenesDropdown.Id) {}
  }
}
#endif
