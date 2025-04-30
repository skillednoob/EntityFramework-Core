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





	}
}

//The context opens and closes connections as needed
//the First(), selects the first element from the list of elements that satisfies the condition. If the list is empty, an exception is thrown
