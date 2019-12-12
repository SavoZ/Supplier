using System.Collections.Generic;

namespace Common.Models.Product {
	public class ProductPostModel {
		public int? Id { get; set; }
		public string Name { get; set; }
		public List<int> Limits { get; set; }

		public ProductPostModel() {
			Limits = new List<int>();
		}
	}
}
