using DBFirstCore.DataAccessLayer.Models;
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


	}
}

//The context opens and closes connections as needed
//the First(), selects the first element from the list of elements that satisfies the condition. If the list is empty, an exception is thrown
