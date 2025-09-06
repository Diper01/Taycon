using _Game._Scripts.DataTypes.Workers;
using _Game._Scripts.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
namespace _Game._Scripts.Loaders {
  public class WorkersDataLoader : ILoader {
    private const string ADDRESS_KEY = "Assets/_Game/SO/Workers/Workers_Database.asset";

    public async UniTask LoadAsync() {
      var db = await Addressables.LoadAssetAsync<WorkersData>(ADDRESS_KEY).Task;
      WorkerUtils.Initialize(db);
    }
  }
}
