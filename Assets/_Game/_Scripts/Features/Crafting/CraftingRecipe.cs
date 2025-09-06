using System;
using _Game._Scripts.DataTypes.Resources;
using _Game._Scripts.Features.InventoryStuff;
namespace _Game._Scripts.Features.Crafting {
  [Serializable]
  public class CraftingRecipe {
    public ResourceType output;
    public int outputAmount = 1;
    public ResourceCost[] inputs;
  }
}
