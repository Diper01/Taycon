using _Game._Scripts.DataTypes.Resources;
using _Game._Scripts.Features.InventoryStuff;
namespace _Game._Scripts.Features.Crafting {
  public class CraftingSystem {
    private readonly IInventory _inventory;
    private readonly CraftingDatabase _db;

    public CraftingSystem (IInventory inventory, CraftingDatabase db) {
      _inventory = inventory;
      _db = db;
    }

    public bool CanCraft (ResourceType type) {
      if (!_db.TryGetRecipe(type, out var recipe))
        return false;

      return _inventory.HasCost(recipe.inputs);
    }

    public bool TryCraft (ResourceType type) {
      if (!_db.TryGetRecipe(type, out var recipe))
        return false;

      if (!_inventory.TrySpend(recipe.inputs))
        return false;

      _inventory.Add(recipe.output, recipe.outputAmount);
      return true;
    }
  }
}
