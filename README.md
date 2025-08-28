Pizza Store API

A simple ASP.NET Core 8 Web API project that manages pizzas and toppings.
This API demonstrates RESTful design, Entity Framework Core with InMemory database, and proper HTTP status codes.

🚀 Features

ASP.NET Core 8 Web API (Backend only, no frontend)

Entity Framework Core (InMemory DB for testing)

Many-to-many relationship between Pizzas and Toppings

Input validation with meaningful error messages

Prevents duplicate pizzas and toppings

Follows RESTful API principles

📦 Requirements

.NET 8 SDK

Visual Studio 2022 or VS Code

🛠 Setup & Run

Clone the repository

git clone https://github.com/Arvinfelicen101/PizzaStore.git
cd PizzaStore


Restore dependencies

dotnet restore


Build the project

dotnet build


Run the API

dotnet run


Open Swagger UI at:

https://localhost:5001/swagger

📖 API Endpoints
Pizzas

GET /api/pizzas → Get all pizzas

GET /api/pizzas/{id} → Get pizza by ID

POST /api/pizzas → Create a new pizza

{
  "name": "Pepperoni Pizza",
  "toppingIds": [1, 2]
}


PUT /api/pizzas/{id} → Update a pizza

DELETE /api/pizzas/{id} → Delete a pizza

Toppings

GET /api/toppings → Get all toppings

POST /api/toppings → Create a new topping

{
  "name": "Cheese"
}

✅ Testing the API

You can test using Swagger UI or tools like Postman:

Example with curl:

# Add a topping
curl -X POST "https://localhost:5001/api/toppings" -H "Content-Type: application/json" -d "{ \"name\": \"Cheese\" }"

# Add a pizza with topping
curl -X POST "https://localhost:5001/api/pizzas" -H "Content-Type: application/json" -d "{ \"name\": \"Cheese Pizza\", \"toppingIds\": [1] }"

⚠️ Error Handling

400 Bad Request → Invalid input (e.g., empty pizza name)

404 Not Found → Pizza or topping not found

409 Conflict → Duplicate pizza or topping name

📝 Notes

Uses InMemory DB (data resets when app restarts).

Can be swapped to SQL Server or SQLite easily by updating DbContext.
