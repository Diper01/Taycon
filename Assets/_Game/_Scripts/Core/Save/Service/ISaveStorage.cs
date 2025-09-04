using System.Threading.Tasks;
using _Game._Scripts.Core.Save.Data;
using Cysharp.Threading.Tasks;
namespace _Game._Scripts.Core.Save.Service {
  public interface ISaveStorage {
    bool Exists();

    SaveData Load();

    void Save (SaveData data);

    void Delete();

    UniTask<SaveData> LoadAsync();

    UniTask SaveAsync (SaveData data);
  }
}
