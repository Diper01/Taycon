using System.Collections.Generic;
using System.Linq;
using _Game._Scripts.Core.Save.Service;
using _Game._Scripts.Loaders;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
namespace _Game._Scripts.Boot {
  public class BootstrapRunner : MonoBehaviour {
    private readonly List<ILoader> _loaders = new();
    [SerializeField] private string _nextSceneName = "Game";

    private async void Awake() {
      await InitSave();
      AddLoaders();
      DontDestroyOnLoad(gameObject);
      await Addressables.InitializeAsync().Task;

      var loaders = this._loaders.OfType<ILoader>().ToArray();

      foreach (var l in loaders)
        await l.LoadAsync();

      await SceneManager.LoadSceneAsync(_nextSceneName);
    }
    private void AddLoaders() {
      _loaders.Add(new ResourcesDataLoader());
    }

    private async UniTask InitSave() {
      await SaveService.InitAsync();
#if UNITY_EDITOR
      Debug.Log("[Bootstrap] Save loaded successfully");
#endif
    }

    private void OnDestroy() {
      SaveService.Save();
    }
  }
}
