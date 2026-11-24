# swagStore-Project-backend
 
A C# shopping application backend, built as a hands-on learning project. It runs as a console application with user and admin menus backed by a JSON mock database and has a RESTful API that a separate frontend uses to manage users, products, shopping carts and payments.

## Features

### Console application

**User menu**
- Login
- View and shop products
- Change password
- Add balance to account

**Admin menu**
- Add new users
- Add new products

**Unit tests**
- xUnit tests to test core method logic

### RESTful API

CRUD controllers and shopping cart logic, communicating with the frontend:

**Users**
- User CRUD (create, read, update, delete)
- Login

**Products**
- Product CRUD (create, read, update, delete)
- Grouping by brand and type
- Receive shopping cart from the frontend (dictionary with username as key and cart as contents) and save in a dictionary
- Return the shopping cart for the currently logged in user to frontend
- Calculate the total price of all items in the cart
- 10% discount applied when the cart contains more than 3 items 
- Payment method for completing purchases
- A ShopRequest class containing parameters to save the shopping cart dictionary as dto

**Admin**
- Update product properties: price, color and brand from frontend input into the JSON lists

## Technologies
- C# / .NET
- ASP.NET Core API
- JSON file database

## Installation
1. Clone the repository from your terminal:
   git clone https://github.com/KalinaGeorgieva312/swagStore-Project-backend.git
2. Open the cloned solution folder.
3. Run the solution by opening 'SwagStore.slnx' in Visual Studio
4. Log in with the test credentials:
   - to test user: Username: Bob
                   Password: 123
   - to test admin: Username: Admin
                    Password: asdf

## Project Structure
API: - Controllers   # Users, Products, Admin
     - Services      # Shopping carts manager
     - JSON files    # Users, Products, Brands + copies
ConsoleApp:          # Presentation layer
Core:                # Core logic, Product and User properties
Tests:               # xUnit tests for core logic
