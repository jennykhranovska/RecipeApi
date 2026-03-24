using RecipeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApi.Repositories;

public class RecipeRepository : IRecipeRepository
{
    private static readonly List<Recipe> _recipes = new();
    private static int _nextId = 1;

    public Task<List<Recipe>> GetAllAsync()
    {
        return Task.FromResult(_recipes);
    }

    public Task<Recipe?> GetByIdAsync(int id)
    {
        var recipe = _recipes.FirstOrDefault(r => r.Id == id);
        return Task.FromResult(recipe);
    }

    public Task<Recipe> CreateAsync(Recipe recipe)
    {
        recipe.Id = _nextId++;
        recipe.CreatedAt = DateTime.UtcNow;

        if (recipe.Ingredients != null)
        {
            for (int i = 0; i < recipe.Ingredients.Count; i++)
            {
                recipe.Ingredients[i].Id = i + 1;
            }
        }

        _recipes.Add(recipe);
        return Task.FromResult(recipe);
    }

    public Task<bool> UpdateAsync(Recipe recipe)
    {
        var existingRecipe = _recipes.FirstOrDefault(r => r.Id == recipe.Id);

        if (existingRecipe is null)
        {
            return Task.FromResult(false);
        }

        existingRecipe.Name = recipe.Name;
        existingRecipe.Description = recipe.Description;
        existingRecipe.PrepTimeMinutes = recipe.PrepTimeMinutes;
        existingRecipe.CookTimeMinutes = recipe.CookTimeMinutes;
        existingRecipe.Servings = recipe.Servings;
        existingRecipe.Difficulty = recipe.Difficulty;
        existingRecipe.Ingredients = recipe.Ingredients;
        existingRecipe.Instructions = recipe.Instructions;

        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var recipe = _recipes.FirstOrDefault(r => r.Id == id);

        if (recipe is null)
        {
            return Task.FromResult(false);
        }

        _recipes.Remove(recipe);
        return Task.FromResult(true);
    }

    public Task<List<Recipe>> SearchAsync(string term)
    {
        term = term.ToLower();

        var results = _recipes
            .Where(r =>
                r.Name.ToLower().Contains(term) ||
                r.Description.ToLower().Contains(term))
            .ToList();

        return Task.FromResult(results);
    }

    public Task<List<Recipe>> GetByDifficultyAsync(string difficulty)
    {
        var results = _recipes
            .Where(r => r.Difficulty.Equals(difficulty, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Task.FromResult(results);
    }
}