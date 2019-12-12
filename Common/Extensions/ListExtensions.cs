using Common.Models.Distribution;
using Common.Models.Limit;
using System.Collections.Generic;

namespace Common.Extensions {
	public static class ListExtensions {
		public static List<LimitModel> CalculateRangeCapacity(this List<int> limits) {
			var result = new List<LimitModel>();
			limits.Sort();

			for (int i = 0; i < limits.Count; i++) {
				if (i == 0) {
					result.Add(new LimitModel {
						LimitValue = limits[i],
						RangeCapacity = limits[i]
					});
				} else {
					result.Add(new LimitModel {
						LimitValue = limits[i],
						RangeCapacity = limits[i] - limits[i - 1]
					});
				}
			}
			result.Add(new LimitModel { LimitValue = null, RangeCapacity = null });

			return result;
		}

		public static decimal CalculateQuantity(this List<DistributionModel> limits, int quantity) {
			decimal total = 0;

			foreach (var limit in limits) {
				if (limit.LimitValue == null) {
					total += (quantity * limit.PercentageDistribution.Value) / 100;
				} else {
					var difference = quantity > limit.RangeCapacity ? limit.RangeCapacity.Value : quantity;
					total += (difference * limit.PercentageDistribution.Value) / 100;
					quantity -= difference;
				}
			}

			return total;
		}
	}
}
