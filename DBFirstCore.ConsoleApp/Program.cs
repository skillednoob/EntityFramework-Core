using DBFirstCore.DataAccessLayer.Models;
using DBFirstCore.DataAccessLayer;

namespace DBFirstCore.ConsoleApp
{
	public class Program
	{
		static QuickKartDbContext context;
		static QuickKartRepository repository;

		static Program()
		{
			context = new QuickKartDbContext();
			repository = new QuickKartRepository(context);
		}
		static void Main(string[] args)
		{
			//var categories = repository.GetAllCategories();
			//Console.WriteLine("----------------------------------");
			//Console.WriteLine("CategoryId\tCategoryName");
			//Console.WriteLine("----------------------------------");
			//foreach (var category in categories)
			//{
			//	Console.WriteLine("{0}\t\t{1}", category.CategoryId, category.CategoryName);
			//}


			//byte categoryId = 2;
			//List<Product> lstProducts = repository.GetProductsOnCategoryId(categoryId);
			//if (lstProducts.Count == 0)
			//{
			//	Console.WriteLine("No products available under the category = " + categoryId);
			//}
			//else
			//{
			//	Console.WriteLine("{0,-15}{1,-30}{2,-15}{3,-10}{4}", "ProductId", "ProductName", "CategoryId", "Price", "QuantityAvailable");
			//	Console.WriteLine("---------------------------------------------------------------------------------------");
			//	foreach (var product in lstProducts)
			//	{
			//		Console.WriteLine("{0,-15}{1,-30}{2,-15}{3,-10}{4}", product.ProductId, product.ProductName, product.CategoryId, product.Price, product.QuantityAvailable);
			//	}
			//}

			//byte categoryId = 1;
			//Product product = repository.FilterProducts(categoryId);
			//if (product == null)
			//{
			//	Console.WriteLine("No product details available");
			//}
			//else
			//{
			//	Console.WriteLine("{0,-15}{1,-30}{2,-15}{3,-10}{4}", "ProductId", "ProductName", "CategoryId", "Price", "QuantityAvailable");
			//	Console.WriteLine("---------------------------------------------------------------------------------------");
			//	Console.WriteLine("{0,-15}{1,-30}{2,-15}{3,-10}{4}", product.ProductId, product.ProductName, product.CategoryId, product.Price, product.QuantityAvailable);
			//}
			//Console.WriteLine();

			//string pattern = "BMW%"; //Only the products with the ProductName matching the pattern 'BMW%' are displayed.
			//List<Product> lstProducts = repository.FilterProductsUsingLike(pattern);
			//if (lstProducts.Count == 0)
			//{
			//	Console.WriteLine("No products available with the = " + pattern);
			//}
			//else
			//{
			//	Console.WriteLine("{0,-15}{1,-30}{2,-15}{3,-10}{4}", "ProductId", "ProductName", "CategoryId", "Price", "QuantityAvailable");
			//	Console.WriteLine("---------------------------------------------------------------------------------------");
			//	foreach (var product in lstProducts)
			//	{
			//		Console.WriteLine("{0,-15}{1,-30}{2,-15}{3,-10}{4}", product.ProductId, product.ProductName, product.CategoryId, product.Price, product.QuantityAvailable);
			//	}
			//}
			//Console.WriteLine();

			//CREATE------------------------------------------------------------------------------------------------------------------------------------------------------------------
			//bool result = repository.AddCategory("Books");
			//if (result)
			//{
			//	Console.WriteLine("New category added successfully");
			//}
			//else
			//{
			//	Console.WriteLine("Something went wrong. Try again!");
			//}

			//Product productOne = new Product();
			//productOne.ProductId = "P158";
			//productOne.ProductName = "The Ship of Secrets - Geronimo Stilton";
			//productOne.CategoryId = 8;
			//productOne.Price = 450;
			//productOne.QuantityAvailable = 10;

			//Product productTwo = new Product();
			//productTwo.ProductId = "P159";
			//productTwo.ProductName = "101 Nursery Rhymes";
			//productTwo.CategoryId = 8;
			//productTwo.Price = 700;
			//productTwo.QuantityAvailable = 10;
			//bool result = repository.AddProductsUsingAddRange(productOne, productTwo);
			//if (result)
			//{
			//	Console.WriteLine("Product details added successfully!");
			//}
			//else
			//{
			//	Console.WriteLine("Some error occurred. Try again!!");
			//}

			//bool result = repository.RegisterUser("GEAHN@123", "F", "Meghan@gmail.com", new DateTime(1978, 02, 12), "New Avenue, Robster");
			//if(result)
			//{
			//	Console.WriteLine("User added succesfully");
			//}
			//else
			//{
			//	Console.WriteLine("Some error occured");
			//}


		}
	}
}
