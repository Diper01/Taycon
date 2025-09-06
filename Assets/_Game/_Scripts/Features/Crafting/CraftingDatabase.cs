using System.Collections.Generic;
using System.Linq;
using _Game._Scripts.DataTypes.Resources;
using UnityEngine;
namespace _Game._Scripts.Features.Crafting {
  [CreateAssetMenu(fileName = "CraftingDatabase", menuName = "Game/Crafting/Database", order = 0)]
  public class CraftingDatabase : ScriptableObject {
    [SerializeField] private List<CraftingRecipe> _recipes = new();

    private Dictionary<ResourceType, CraftingRecipe> _map;

    private void OnEnable()
      => BuildMap();
    private void OnValidate()
      => BuildMap();

    void BuildMap() {
      _map = new Dictionary<ResourceType, CraftingRecipe>();

      foreach (var r in _recipes.Where(r => r != null && r.output != ResourceType.None))
        _map[r.output] = r;
    }

    public bool TryGetRecipe (ResourceType type, out CraftingRecipe recipe) {
      if (_map == null || _map.Count == 0)
        BuildMap();

      return _map.TryGetValue(type, out recipe);
    }

    public IEnumerable<CraftingRecipe> GetAllRecipes() {
      if (_map == null || _map.Count == 0)
        BuildMap();

      return _map.Values;
    }
  }
}
