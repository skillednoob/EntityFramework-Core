using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFirstCore.DataAccessLayer.Models
{
	public class ProductCategoryName
	{
		[Key]
		public string ProductId { get; set; }
		public string ProductName { get; set; }
		public decimal Price { get; set; }
		public string CategoryName { get; set; }
		public int QuantityAvailable { get; set; }
	}
}
//In case an entity requires a composite key (with more than one property in a primary key), then data annotation [Key] attribute cannot be used. You need to use ONLY Fluent API to configure composite key in the entity.