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



	}
}

//The context opens and closes connections as needed
//the First(), selects the first element from the list of elements that satisfies the condition. If the list is empty, an exception is thrown
