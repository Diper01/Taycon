using System.Collections.Generic;
using System.Linq;
using _Game._Scripts.Loaders;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
namespace _Game._Scripts.Boot {
  public class BootstrapRunner : MonoBehaviour
  {
    private readonly List<ILoader> loaders;
    [SerializeField] private string nextSceneName = "Game";

    private async void Awake() {

      AddLoaders();
      DontDestroyOnLoad(gameObject);
      await Addressables.InitializeAsync().Task;

      var loaders = this.loaders.OfType<ILoader>().ToArray();
      foreach (var l in loaders)
        await l.LoadAsync();

      await SceneManager.LoadSceneAsync(nextSceneName);
    }
    private void AddLoaders () {
      loaders.Add(new ResourcesDataLoader());
    }
  }
}
