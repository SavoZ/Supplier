using System.Collections.Generic;
using Common.Models.Limit;

namespace Common.Models.Product {
	public class ProductViewModel {
		public int Id { get; set; }
		public string Name { get; set; }
		public List<LimitModel> Limits { get; set; }

		public ProductViewModel() {
			Limits = new List<LimitModel>();
		}
	}
}
