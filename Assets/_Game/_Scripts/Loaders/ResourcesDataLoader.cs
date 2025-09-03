using _Game._Scripts.DataTypes;
using _Game._Scripts.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
namespace _Game._Scripts.Loaders {
 
  public class ResourcesDataLoader : ILoader
  {
    private const string ADDRESS_KEY = "ResourcesData";

    public async UniTask LoadAsync()
    {
      var db = await Addressables.LoadAssetAsync<ResourcesData>(ADDRESS_KEY).Task;
      ResourceUtils.Initialize(db);
    }
  }
}
