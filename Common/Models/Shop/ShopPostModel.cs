using System.Collections.Generic;

namespace Common.Models.Shop {
	public class ShopPostModel {
		public int Id { get; set; }
		public string Name { get; set; }
		public List<int> Products { get; set; }

		public ShopPostModel() {
			Products = new List<int>();
		}
	}
}
