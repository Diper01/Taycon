using _Game._Scripts.DataTypes;
using _Game._Scripts.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
namespace _Game._Scripts.Loaders {
 
  public class ResourcesDataLoader : ILoader
  {
    private const string addressKey = "ResourcesData";

    public async UniTask LoadAsync()
    {
      var db = await Addressables.LoadAssetAsync<ResourcesData>(addressKey).Task;
      ResourceUtils.Initialize(db);
    }
  }
}
