using System.IO;
using System.Text;
using Cysharp.Threading.Tasks;
using _Game._Scripts.Core.Save.Data;
using UnityEngine;

namespace _Game._Scripts.Core.Save.Service {
  public class JsonFileStorage : ISaveStorage {
    private const string FILE_NAME = "GameSave.json";
    private readonly string _fullPath;

    public JsonFileStorage() {
      string dir = Application.persistentDataPath;
      _fullPath = Path.Combine(dir, FILE_NAME);
    }

    public bool Exists() => File.Exists(_fullPath);

    public async UniTask<SaveData> LoadAsync() {
      if (!Exists()) return new SaveData();

      try {
        await using var stream = new FileStream(
          _fullPath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
        using var reader = new StreamReader(stream, Encoding.UTF8);
        string json = await reader.ReadToEndAsync();
        if (string.IsNullOrWhiteSpace(json)) return new SaveData();
        return JsonUtility.FromJson<SaveData>(json) ?? new SaveData();
      }
      catch {
        return new SaveData();
      }
    }

    public async UniTask SaveAsync(SaveData data) {
      string json = JsonUtility.ToJson(data, true);
      await using var stream = new FileStream(
        _fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
      using var writer = new StreamWriter(stream, Encoding.UTF8);
      await writer.WriteAsync(json);
      await writer.FlushAsync();
      await stream.FlushAsync();
    }

    public SaveData Load() {
      try {
        if (!Exists()) return new SaveData();
        string json = File.ReadAllText(_fullPath, Encoding.UTF8);
        if (string.IsNullOrWhiteSpace(json)) return new SaveData();
        return JsonUtility.FromJson<SaveData>(json) ?? new SaveData();
      }
      catch {
        return new SaveData();
      }
    }

    public void Save(SaveData data) {
      string json = JsonUtility.ToJson(data, true);
      File.WriteAllText(_fullPath, json, Encoding.UTF8);
    }

    public void Delete() {
      if (Exists()) File.Delete(_fullPath);
    }
  }
}
