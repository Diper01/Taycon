using System.Linq;
using _Game._Scripts.Features.Crafting.ItemView;
using UnityEngine;
namespace _Game._Scripts.Features.Crafting {
  public class CraftingPanel : MonoBehaviour {
    [SerializeField] private CraftingDatabase database;
    [SerializeField] private InventoryStuff.Inventory inventory;
    [SerializeField] private Transform content;
    [SerializeField] private CraftingRecipeItemView itemPrefab;

    private CraftingSystem _crafting;

    private void Awake() {
      _crafting = new CraftingSystem(inventory, database);
      Build();
    }

    private void Build() {
      foreach (Transform c in content) Destroy(c.gameObject);
      var recipes = database.GetAllRecipes().ToList();
      for (int i = 0; i < recipes.Count; i++) {
        var view = Instantiate(itemPrefab, content);
        view.name = $"Recipe_{recipes[i].output}";
        view.Setup(recipes[i], inventory, _crafting);
      }
    }
  }
 
}
