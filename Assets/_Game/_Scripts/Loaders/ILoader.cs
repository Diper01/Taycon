using Cysharp.Threading.Tasks;
namespace _Game._Scripts.Loaders {
  public interface ILoader
  {
    UniTask LoadAsync();
  }
}
