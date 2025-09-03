using _Game._Scripts.DataTypes;
using _Game._Scripts.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
namespace _Game._Scripts.Loaders {
 
  public class ResourcesDataLoader : ILoader
  {
    private const string ADDRESS_KEY = "Assets/_Game/SO/SO_Resources/Resources_Database.asset";

    public async UniTask LoadAsync()
    {
      var db = await Addressables.LoadAssetAsync<ResourcesData>(ADDRESS_KEY).Task;
      ResourceUtils.Initialize(db);
    }
  }
}
