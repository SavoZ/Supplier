using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models.Distribution {
	public class DistributionModel {
		public int? ShopId { get; set; }
		public int LimitId { get; set; }
		public int? LimitValue { get; set; }
		public int? RangeCapacity { get; set; }
		public decimal? PercentageDistribution { get; set; }
		public string ShopName { get; set; }
	}
}
