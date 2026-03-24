# RecipeApi

`RecipeApi` is a simple ASP.NET Core Web API for managing recipes. The project exposes endpoints for listing recipes, fetching a recipe by id, creating new recipes, updating existing recipes, deleting recipes, searching by text, and filtering by difficulty.

The current implementation uses an in-memory repository, which means data is stored only while the application is running.

## Tech Stack

- .NET 10
- ASP.NET Core Web API
- Swagger / Swashbuckle
- xUnit
- Moq

## Project Structure

- `RecipeApi/Controllers` contains the API endpoints.
- `RecipeApi/Services` contains business logic.
- `RecipeApi/Repositories` contains data access logic.
- `RecipeApi/Models` contains entities and DTOs.
- `RecipeApi.Tests` contains unit tests.

## Run The Project

1. Open a terminal in the solution folder.
2. Run:

```bash
dotnet run --project RecipeApi/RecipeApi.csproj --launch-profile http
```

The API starts on:

```text
http://localhost:5099
```

Swagger UI is available at:

```text
http://localhost:5099/swagger/index.html
```

## Run Tests

Run all tests in the solution:

```bash
dotnet test RecipeApi.slnx
```

Run only the test project:

```bash
dotnet test RecipeApi.Tests/RecipeApi.Tests.csproj
```

## API Endpoints

Base route:

```text
/api/recipes
```

Available endpoints:

- `GET /api/recipes` returns all recipes.
- `GET /api/recipes/{id}` returns a recipe by id.
- `POST /api/recipes` creates a new recipe.
- `PUT /api/recipes/{id}` updates an existing recipe.
- `DELETE /api/recipes/{id}` deletes a recipe.
- `GET /api/recipes/search?q=value` searches recipes by name or description.
- `GET /api/recipes/difficulty/{level}` filters recipes by difficulty.

## Example Request Body

Use this JSON when creating or updating a recipe:

```json
{
  "name": "Pancakes",
  "description": "Simple homemade pancakes",
  "prepTimeMinutes": 10,
  "cookTimeMinutes": 15,
  "servings": 4,
  "difficulty": "Easy",
  "ingredients": [
    {
      "name": "Flour",
      "quantity": 3,
      "unit": "dl"
    }
  ],
  "instructions": [
    "Mix all ingredients",
    "Cook in a pan"
  ]
}
```

## Validation Rules

The API validates incoming recipe data through DTOs:

- `Name` is required and must be between 3 and 100 characters.
- `Description` can contain up to 500 characters.
- `PrepTimeMinutes` must be between 1 and 480.
- `CookTimeMinutes` must be between 0 and 480.
- `Servings` must be between 1 and 100.
- `Ingredients` is required.
- `Instructions` is required and must contain at least one step.

## Notes

- The repository is in-memory only, so all data is reset when the application stops.
- Swagger is enabled in the development environment.
- The included `RecipeApi.http` file can be expanded with calls to the recipe endpoints for quick local testing.
