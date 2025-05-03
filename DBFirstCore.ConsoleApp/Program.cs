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
			//To Run All Methods Make Sure to Re-Run The Sql Script

			////READ Operations
			//GetAllCategories();
			//GetProductsOnCategoryId();   
			//FilterProducts();			
			//FilterProductsUsingLike();  

			////CREATE Operations
			//AddCategory();
			//AddProductsUsingAddRange();
			//RegisterUser();

			////UPDATE Operations
			//UpdateCategory();
			//UpdateProductPrice();
			//UpdateProductUsingAddRange();
			//UpdateUserPassword();


			////DELETE Operations
			//DeleteProduct();
			//DeleteProductsUsingRemoveRange();
			//DeleteUserDetails();

			////STORED PROCEDURE  Operations
			//AddCategoryDetailsUsingUSP();		 
			//InsertPurchaseDetails();  
			//RegisterNewUser();


			////TABLE VALUED FUNCTION Operations
			//GetProductsUsinfTVF();
			//GetProductDetails();


			////SCALAR VALUED FUNCTION Operations
			//GetNewProductId();
			//CheckEmailId();
			//GetRole();			
		}

		#region GetAllCategories
		public static void GetAllCategories()
		{
			var categories = repository.GetAllCategories();
			Console.WriteLine("----------------------------------");
			Console.WriteLine("CategoryId\tCategoryName");
			Console.WriteLine("----------------------------------");
			foreach (var category in categories)
			{
				Console.WriteLine("{0}\t\t{1}", category.CategoryId, category.CategoryName);
			}
		}
		#endregion

		#region GetProductsOnCategoryId
		public static void GetProductsOnCategoryId()
		{
			byte categoryId = 1; //make sure categorid exsist
			List<Product> lstProducts = repository.GetProductsOnCategoryId(categoryId);
			if (lstProducts.Count == 0)
			{
				Console.WriteLine("No products available under the category = " + categoryId);
			}
			else
			{
				Console.WriteLine("{0,-15}{1,-30}{2,-15}{3,-10}{4}", "ProductId", "ProductName", "CategoryId", "Price", "QuantityAvailable");
				Console.WriteLine("---------------------------------------------------------------------------------------");
				foreach (var product in lstProducts)
				{
					Console.WriteLine("{0,-15}{1,-30}{2,-15}{3,-10}{4}", product.ProductId, product.ProductName, product.CategoryId, product.Price, product.QuantityAvailable);
				}
			}
		}
		#endregion

		#region FilterProducts
		public static void FilterProducts()
		{
			byte categoryId = 1;
			Product product = repository.FilterProducts(categoryId);
			if (product == null)
			{
				Console.WriteLine("No product details available");
			}
			else
			{
				Console.WriteLine("{0,-15}{1,-30}{2,-15}{3,-10}{4}", "ProductId", "ProductName", "CategoryId", "Price", "QuantityAvailable");
				Console.WriteLine("---------------------------------------------------------------------------------------");
				Console.WriteLine("{0,-15}{1,-30}{2,-15}{3,-10}{4}", product.ProductId, product.ProductName, product.CategoryId, product.Price, product.QuantityAvailable);
			}
			Console.WriteLine();
		}
		#endregion

		#region FilterProductsUsingLike
		public static void FilterProductsUsingLike()
		{
			string pattern = "BMW%"; //Only the products with the ProductName matching the pattern 'BMW%' are displayed.
			List<Product> lstProducts = repository.FilterProductsUsingLike(pattern);
			if (lstProducts.Count == 0)
			{
				Console.WriteLine("No products available with the = " + pattern);
			}
			else
			{
				Console.WriteLine("{0,-15}{1,-30}{2,-15}{3,-10}{4}", "ProductId", "ProductName", "CategoryId", "Price", "QuantityAvailable");
				Console.WriteLine("---------------------------------------------------------------------------------------");
				foreach (var product in lstProducts)
				{
					Console.WriteLine("{0,-15}{1,-30}{2,-15}{3,-10}{4}", product.ProductId, product.ProductName, product.CategoryId, product.Price, product.QuantityAvailable);
				}
			}
			Console.WriteLine();
		}
		#endregion

		#region AddCategory
		public static void AddCategory()
		{
			bool result = repository.AddCategory("Books");
			if (result)
			{
				Console.WriteLine("New category added successfully");
			}
			else
			{
				Console.WriteLine("Something went wrong. Try again!");
			}
		}
		#endregion

		#region  AddProductsUsingAddRange
		public static void AddProductsUsingAddRange()
		{
			Product productOne = new Product();
			productOne.ProductId = "P158";
			productOne.ProductName = "The Ship of Secrets - Geronimo Stilton";
			productOne.CategoryId = 8;
			productOne.Price = 450;
			productOne.QuantityAvailable = 10;

			Product productTwo = new Product();
			productTwo.ProductId = "P159";
			productTwo.ProductName = "101 Nursery Rhymes";
			productTwo.CategoryId = 8;
			productTwo.Price = 700;
			productTwo.QuantityAvailable = 10;
			bool result = repository.AddProductsUsingAddRange(productOne, productTwo);
			if (result)
			{
				Console.WriteLine("Product details added successfully!");
			}
			else
			{
				Console.WriteLine("Some error occurred. Try again!!");
			}

		}
		#endregion

		#region RegisterUser
		public static void RegisterUser()
		{
			bool result = repository.RegisterUser("GEAHN@123", "F", "Meghan@gmail.com", new DateTime(1978, 02, 12), "New Avenue, Robster");
			if (result)
			{
				Console.WriteLine("User added succesfully");
			}
			else
			{
				Console.WriteLine("Some error occured");
			}
		}
		#endregion

		#region UpdateCatgeory
		public static void UpdateCategory()
		{
			bool result = repository.UpdateCategory(8, "Stationery");
			if (result)
			{
				Console.WriteLine("Category details updated successfully");
			}
			else
			{
				Console.WriteLine("Something went wrong. Try again!");
			}
		}
		#endregion

		#region UpdateProductPrice()
		public static void UpdateProductPrice()
		{
			int status = repository.UpdateProduct("P159", 1000);
			if (status == 1)
			{
				Console.WriteLine("Product price updated successfully!");
			}
			else
			{
				Console.WriteLine("Some error occurred. Try again!!");
			}
		}
		#endregion

		#region UpdateProductUsingAddRange
		public static void UpdateProductUsingAddRange()
		{
			int status = repository.UpdateProductsUsingUpdateRange(8, 10);
			if (status == 1)
			{
				Console.WriteLine("Products updated successfully!");
			}
			else
			{
				Console.WriteLine("Some error occurred. Try again!!");
			}
		}
		#endregion

		#region UpdateUserPassword
		public static void UpdateUserPassword()
		{
			bool result = repository.UpdateUserPassword("Meghan@gmail.com", "GEAHN@1234");
			if (result)
			{
				Console.WriteLine("Passwrod Updated Succesfully");
			}
			else
			{
				Console.WriteLine("Some error Occured");
			}
		}
		#endregion

		#region DeleteProduct
		public static void DeleteProduct()
		{
			bool status = repository.DeleteProduct("P159");
			if (status)
			{
				Console.WriteLine("Product details deleted successfully!");
			}
			else
			{
				Console.WriteLine("Some error occurred. Try again!!");
			}
		}
		#endregion

		#region DeleteProductsUsingRemoveRange
		public static void DeleteProductsUsingRemoveRange()
		{
			bool status = repository.DeleteProductsUsingRemoveRange("BMW");
			if (status)
			{
				Console.WriteLine("Products deleted successfully");
			}
			else
			{
				Console.WriteLine("Some error occurred.Try again!!");
			}
		}
		#endregion

		#region DeleteUserDetails
		public static void DeleteUserDetails()
		{
			bool ans = repository.DeleteUserDetails("Meghan@gmail.com");
			if (ans)
			{
				Console.WriteLine("User Details Deleted Successfully");
			}
			else
			{
				Console.WriteLine("Some Error Occured");
			}
		}
		#endregion

        # region AddCategoryDetailsUsingUSP

		public static void AddCategoryDetailsUsingUSP()
		{
			byte categoryId = 0;
			int returnResult = repository.AddCategoryDetailsUsingUSP("Footwear", out categoryId);
			if (returnResult > 0)
			{
				Console.WriteLine("Category details added successfully with CategoryId = " + categoryId);
			}
			else
			{
				Console.WriteLine("Some error occurred. Try again!");
			}
		}
		#endregion

		#region RegisterNewUser
		public static void RegisterNewUser()
		{
			int rr = repository.RegisterNewUser("Swsw@1234", "M", "veer@gmail.com", new DateTime(1978, 02, 12), "Palghar", 2);
			if (rr > 0)
			{
				Console.WriteLine("Added/Regitser user successfully");
			}
			else
			{
				Console.WriteLine("Some Error Occured");
			}
		}
		#endregion

		#region InsertPurchaseDetails
		public static void InsertPurchaseDetails()
		{
			long purchaseId = 0;
			int rr = repository.InsertPurchaseDetails("veer@gmail.com", "P104", 3, out purchaseId);    //make sure quantitypurchased is less than quantity available
			if (rr > 0)
			{
				Console.WriteLine("Inserted Succesfully with PurchaseId = " + purchaseId);
			}
			else
			{
				Console.WriteLine("Some Error Occured");
			}
		}
		#endregion

		#region GetProductsUsinfTVF
		public static void GetProductsUsinfTVF()
		{
			byte categoryId = 3;
			var products = repository.GetProductsUsingTVF(categoryId);
			Console.WriteLine("{0, -12}{1, -30}{2}", "ProductId", "ProductName", "CategoryName");
			Console.WriteLine("------------------------------------------------------");
			if (products == null || products.Count == 0)
			{
				Console.WriteLine("No products available under the given category!");
			}
			else
			{
				foreach (var product in products)
				{
					Console.WriteLine("{0, -12}{1, -30}{2}", product.ProductId, product.ProductName, product.CategoryName);
				}
			}
		}
		#endregion

		#region  GetProductDetails

		public static void GetProductDetails()
		{
			byte cId = 1;
			var prods = repository.GetProductDetails(cId);
			Console.WriteLine("{0, -12}{1, -30}{2, -30}{3, -30}", "ProductId", "ProductName", "Price", "QuantityAvailabe", "CategoryId");
			Console.WriteLine("-----------------------------------------------------------------------------------------------------");
			if (prods != null)
			{
				foreach (var pro in prods)
				{
					Console.WriteLine("{0, -12}{1, -30}{2, -30}{3, -30}", pro.ProductId, pro.ProductName, pro.Price, pro.QuantityAvailable, pro.CategoryId);
				}
			}
			else
			{
				Console.WriteLine("No Products for given categoryId");
			}
		}
		#endregion

		#region GetNewProductId
		public static void GetNewProductId()
		{
			string productId = repository.GetNewProductId();
			Console.WriteLine("New ProductId = " + productId);
			Console.WriteLine();
		}
		#endregion

		#region CheckEmailId

		public static void CheckEmailId()
		{
			bool result = repository.CheckEmailId("Ivy@gmail.com");
			if (result)
			{
				Console.WriteLine("EMailId can be used to register new user!");
			}
			else
			{
				Console.WriteLine("EmailId exists! Please use a new EmailId!!");
			}
		}
		#endregion

		#region GetRole
		public static void GetRole()
		{
			string emailId = "MeetRoda@yahoo.co.in";
			string password = "ChristaRocks";
			int roleid = repository.GetRoleId(emailId, password);
			if (roleid == 0)
			{
				Console.WriteLine("wrong credentails");
			}
			else if (roleid == 1)
			{
				Console.WriteLine("Admin Type");
			}
			else
			{
				Console.WriteLine("User Type");
			}
		}
		#endregion
	}
}
