using System;
using System.Collections.Generic;
using _Game._Scripts.DataTypes.Resources;
namespace _Game._Scripts.Core.Save.Data {

  [Serializable]
  public class SaveData {
    public int BuiltZonesCount;
    public List<ResourceStack> Resources = new List<ResourceStack>();

    public int GetAmount (ResourceType type) {
      for (int i = 0; i < Resources.Count; i++)
        if (Resources[i].Type == type)
          return Resources[i].Amount;

      return 0;
    }

    public void SetAmount (ResourceType type, int amount) {
      for (int i = 0; i < Resources.Count; i++) {
        if (Resources[i].Type == type) {
          Resources[i].Amount = amount;
          return;
        }
      }

      Resources.Add(new ResourceStack(type, amount));
    }

    public void AddAmount (ResourceType type, int delta) {
      SetAmount(type, GetAmount(type) + delta);
    }
  }
}
