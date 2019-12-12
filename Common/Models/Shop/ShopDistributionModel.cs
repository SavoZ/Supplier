using Common.Models.Distribution;
using System.Collections.Generic;

namespace Common.Models.Shop {
	public class ShopDistributionModel {
		public int ShopId { get; set; }
		public string ShopName { get; set; }
		public List<DistributionModel> ProductDistribution { get; set; }

		public ShopDistributionModel() {
			ProductDistribution = new List<DistributionModel>();
		}
	}
}
