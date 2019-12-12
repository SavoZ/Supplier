using Common.Models.Shop;
using System.Collections.Generic;

namespace Common.Models.Distribution {
	public class DistributionPostModel {
		public int ProductId { get; set; }
		public List<ShopDistributionModel> Shops { get; set; }

		public DistributionPostModel() {
			Shops = new List<ShopDistributionModel>();
		}
	}
}
