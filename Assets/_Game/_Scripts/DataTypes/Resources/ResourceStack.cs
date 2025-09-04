using System;
namespace _Game._Scripts.DataTypes.Resources {
  [Serializable]
  public class ResourceStack {
    public ResourceType Type;
    public int Amount;

    public ResourceStack() {}

    public ResourceStack (ResourceType type, int amount) {
      Type = type;
      Amount = amount;
    }
  }
}
