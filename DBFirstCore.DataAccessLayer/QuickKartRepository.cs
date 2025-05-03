using DBFirstCore.DataAccessLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFirstCore.DataAccessLayer
{
	public class QuickKartRepository
	{
		private QuickKartDbContext context;
		public QuickKartRepository(QuickKartDbContext context)
		{
			this.context = context;
		}


		//READ
		public List<Category> GetAllCategories()
		{
			var categoriesList = context.Categories.OrderBy(c => c.CategoryId).ToList();
			return categoriesList;
		}

		public List<Product> GetProductsOnCategoryId(byte categoryId)
		{
			List<Product> lstProducts = new List<Product>();
			try
			{
				lstProducts = context.Products.Where(p => p.CategoryId == categoryId).ToList();
			}
			catch (Exception ex)
			{
				lstProducts = null;
			}
			return lstProducts;
		}

		public Product FilterProducts(byte categoryId)
		{
			Product product = new Product();
			try
			{
				//product = context.Products.Where(p => p.CategoryId == categoryId).FirstOrDefault();
				product = context.Products.Where(p => p.CategoryId == categoryId).OrderByDescending(p => p.Price).LastOrDefault();
			}
			catch (Exception ex)
			{
				product = null;
			}
			return product;
		}

		public List<Product> FilterProductsUsingLike(string pattern)
		{
			List<Product> lstProduct = new List<Product>();
			try
			{
				lstProduct = context.Products.Where(p => EF.Functions.Like(p.ProductName, pattern)).ToList();
			}
			catch (Exception ex)
			{
				lstProduct = null;
			}
			return lstProduct;
		}

		//CREATE
		public bool AddCategory(string categoryName)
		{
			bool ans=false;
			try
			{
				Category category = new Category();
				category.CategoryName=categoryName;
				context.Categories.Add(category);
				context.SaveChanges();
				ans=true;
			}
			catch (Exception)
			{

				ans=false;
			}
			return ans;
		}

		public bool AddProductsUsingAddRange(params Product[] products)
		{
			bool ans = false;
			try
			{
				context.Products.AddRange(products);
				context.SaveChanges();
				ans = true;
			}
			catch (Exception)
			{

				ans=false;
			}
			return ans;
		}

		public bool RegisterUser(string userPassword, string gender, string emailId, DateTime dateOfBirth, string address)
		{
			bool ans=false;
			try
			{
				User user = new User();
				user.UserPassword = userPassword;
				user.Gender = gender;
				user.EmailId = emailId;
				user.DateOfBirth = DateOnly.FromDateTime(dateOfBirth);
				user.Address = address;
				context.Users.Add(user);
				context.SaveChanges ();
				ans=true;
			}
			catch (Exception)
			{

				ans=false;
			}
			return ans;
		}

		//UPDATE
		public bool UpdateCategory(byte categoryId, string newCategoryName)
		{
			bool ans=false;
			Category category = new Category();
			category = context.Categories.Find(categoryId);
			try
			{
				if(category!=null)
				{
					category.CategoryName = newCategoryName;
					context.Categories.Update(category);
					context.SaveChanges();
					ans = true;
				}

			}
			catch (Exception)
			{

				ans=false;
			}
			return ans;
		}

		public int UpdateProduct(string productId, decimal price)
		{
			int ans=0;
			Product product=context.Products.Find(productId);
			try
			{
				if (product != null)
				{
					product.Price= price;
					context.SaveChanges();
					ans = 1;
				}

			}
			catch (Exception)
			{

				ans=-99;
			}
			return ans;
		}

		public int UpdateProductsUsingUpdateRange(int categoryId, int quantityProcured)
		{
			int ans = 0;
			List<Product> plst=new List<Product> ();
			plst = context.Products.Where(p => p.CategoryId==categoryId).ToList();
			try
			{
				foreach(Product p in plst)
				{
					p.QuantityAvailable += quantityProcured;
				}
				context.Products.UpdateRange(plst);
				context.SaveChanges();
				ans = 1;

			}
			catch (Exception)
			{

				ans=-99;
			}
			return ans;
		}

		public bool UpdateUserPassword(string EmailId, string newUserPassword)
		{
			bool ans = false;
			User user = new User();
			user = context.Users.Find(EmailId);
			try
			{
				if (user != null)
				{
					user.UserPassword = newUserPassword;
					context.SaveChanges();
					ans = true;
				}
			}
			catch (Exception)
			{

				ans=false;
			}
			return ans;
		}

		//DELETE
		public bool DeleteProduct(string productId)
		{
			bool ans=false;
			Product product=context.Products.Find(productId);
			try
			{
				if (product != null)
				{
					context.Products.Remove(product);
					context.SaveChanges();
					ans = true;
				}
			}
			catch (Exception)
			{

				ans=false;
			}
			return ans;
		}

		public bool DeleteProductsUsingRemoveRange(string subString)
		{
			bool ans = false;
			List<Product> products = new List<Product>();
			products=context.Products.Where(p=>p.ProductName.Contains(subString)).ToList();
			try
			{
				if(products != null)
				{
					context.Products.RemoveRange(products);
					context.SaveChanges();
					ans = true;
				}

			}
			catch (Exception)
			{

				ans=false;
			}
			return ans;
		}

		public bool DeleteUserDetails(string emailID)
		{
			bool ans=false;
			User user = context.Users.Find(emailID);
			try
			{
				if(user != null)
				{
					context.Users.Remove(user);
					context.SaveChanges();
					ans = true;
				}
			}
			catch (Exception)
			{

				ans=false;
			}
			return ans;
		}

		//STORED PROCEDURE
		public int AddCategoryDetailsUsingUSP(string categoryName, out byte categoryId)
		{
			categoryId = 0;
			int noOfRowsAffected = 0;
			int returnResult = 0;


			SqlParameter prmCategoryName = new SqlParameter("@CategoryName", categoryName);

			SqlParameter prmCategoryId = new SqlParameter("@CategoryId", System.Data.SqlDbType.TinyInt);
			prmCategoryId.Direction = System.Data.ParameterDirection.Output;

			SqlParameter prmReturnResult = new SqlParameter("@ReturnResult", System.Data.SqlDbType.Int);
			prmReturnResult.Direction = System.Data.ParameterDirection.Output;
			try
			{
				noOfRowsAffected=context.Database.ExecuteSqlRaw("EXEC @ReturnResult = usp_AddCategory @CategoryName, @CategoryId OUT",prmReturnResult, prmCategoryName, prmCategoryId);

				returnResult = Convert.ToInt32(prmReturnResult.Value);
				categoryId = Convert.ToByte(prmCategoryId.Value);
			}
			catch (Exception ex)
			{
				categoryId = 0;
				noOfRowsAffected = -1;
				returnResult = -99;
			}
			return returnResult;
		}

		public int RegisterNewUser(string userPassword, string gender, string emailId, DateTime dateOfBirth, string address, int roleId)
		{
			int noOfRowsAffected = 0;
			int returnResult = 0;

			SqlParameter prmUserPassword = new SqlParameter("@UserPassword", userPassword);
			SqlParameter prmGender = new SqlParameter("@Gender", gender);
			SqlParameter prmEmailId = new SqlParameter("@EmailId", emailId);
			SqlParameter prmDOB = new SqlParameter("@DateOfBirth", dateOfBirth);
			SqlParameter prmAddress = new SqlParameter("@Address", address);

			SqlParameter prmReturnResult = new SqlParameter("@ReturnResult", System.Data.SqlDbType.Int);
			prmReturnResult.Direction = System.Data.ParameterDirection.Output;

			try
			{
				noOfRowsAffected = context.Database.ExecuteSqlRaw("EXEC @ReturnResult = usp_RegisterUser @UserPassword, @Gender, @EmailId, @DateOfBirth, @Address", prmReturnResult, prmUserPassword, prmGender, prmEmailId, prmDOB, prmAddress);
				if(noOfRowsAffected>0)
				{
					returnResult = Convert.ToInt32(noOfRowsAffected);
				}
			}
			catch (Exception)
			{

				noOfRowsAffected=-1;
				returnResult = -99;
			}
			return returnResult;
		}

		public int InsertPurchaseDetails(string emailId, string productId, int quantityPurchased, out long purchaseId)
		{
			int returnResult = 0;
			purchaseId = 0;
			int noOfRowsAffected = 0;

			SqlParameter prmEmailId = new SqlParameter("@EmailId", emailId);
			SqlParameter prmProductId = new SqlParameter("@ProductId", productId);
			SqlParameter prmqp = new SqlParameter("@QuantityPurchased", quantityPurchased);

			SqlParameter prmPId = new SqlParameter("@PurchaseId", System.Data.SqlDbType.BigInt);
			prmPId.Direction = System.Data.ParameterDirection.Output;

			SqlParameter prmReturnResult = new SqlParameter("@ReturnResult", System.Data.SqlDbType.Int);
			prmReturnResult.Direction = System.Data.ParameterDirection.Output;

			try
			{
				//noOfRowsAffected = context.Database.ExecuteSqlRaw("Exec @ReturnResult = usp_InsertPurchaseDetails @EmailId,@ProductId,@QuantityPurchased,@PurchaseId OUT", prmReturnResult, prmEmailId, prmProductId, prmqp, prmPId);
				// Corrected parameter order - return value parameter comes first in the collection
				noOfRowsAffected = context.Database.ExecuteSqlRaw("EXEC @ReturnResult = usp_InsertPurchaseDetails @EmailId, @ProductId, @QuantityPurchased, @PurchaseId OUTPUT", prmReturnResult, prmEmailId, prmProductId, prmqp, prmPId);

				if (noOfRowsAffected>0)
				{
					returnResult = (prmReturnResult.Value != DBNull.Value) ? Convert.ToInt32(prmReturnResult.Value) : -99;
					purchaseId = (prmPId.Value != DBNull.Value) ? Convert.ToInt64(prmPId.Value) : 0;
					//returnResult =Convert.ToInt32(prmReturnResult.Value);
					//purchaseId=Convert.ToInt64(prmPId.Value);
				}

			}
			catch (Exception)
			{

				returnResult=-1;
				noOfRowsAffected = 0;
				purchaseId = -99;
			}
			return returnResult;
		}

		//TABLE VALUED FUNCTIONS
		public List<ProductCategoryName> GetProductsUsingTVF(byte categoryId)
		{
			List<ProductCategoryName> lstProduct;
			try
			{
				SqlParameter prmCategoryId = new SqlParameter("@CategoryId", categoryId);
				lstProduct = context.ProductCategoryNames.FromSqlRaw("SELECT * FROM ufn_GetProductCategoryDetails(@CategoryId)", prmCategoryId).ToList();
					 //context.ProductCategoryNames.FromSqlRaw("SELECT * FROM ufn_GetProductCategoryDetails(@CategoryId)",prmCategoryId).ToList();
			}
			catch (Exception)
			{
				lstProduct=null;
			}
			return lstProduct;
		}

		public List<Product> GetProductDetails(byte categoryId)
		{
			List<Product> lstProduct;
			try
			{
				SqlParameter prmCId = new SqlParameter("@CategoryId", categoryId);
				lstProduct=context.Products.FromSqlRaw("SELECT * FROM ufn_GetProductDetails(@CategoryId)",prmCId).ToList();

			}
			catch (Exception)
			{

				lstProduct=null;
			}
			return lstProduct;
		}

		//SCALAR VALUED FUNCTIONS
		public string GetNewProductId()
		{
			string productId = string.Empty;
			try
			{
				productId = (from s in context.Products
							 select QuickKartDbContext.ufn_GenerateNewProductId())
							 .FirstOrDefault();
			}
			catch (Exception ex)
			{
				productId = null;
			}
			return productId;
		}

		public bool CheckEmailId(string emailId)
		{
			bool result;
			try
			{
				result = (from p in context.Users
						  select QuickKartDbContext.ufn_CheckEmailId(emailId))
						  .FirstOrDefault();
			}
			catch (Exception ex)
			{
				result = false;
			}

			return result;
		}

		public int GetRoleId(string emailId,string password)
		{
			int roleId = 0;
			try
			{
				roleId = (from s in context.Users
						  select QuickKartDbContext.ufn_ValidateUserCredentials(emailId, password)).FirstOrDefault();

			}
			catch (Exception)
			{

				roleId=0;
			}
			return roleId;
		}

	}
}

//The context opens and closes connections as needed
//the First(), selects the first element from the list of elements that satisfies the condition. If the list is empty, an exception is thrown
