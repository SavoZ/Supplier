using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Extensions;
using Common.Models.Distribution;
using Common.Models.Supplier;
using Database.DB;
using Microsoft.EntityFrameworkCore;

namespace SupplierApp.Services {
	public class SupplierService {
		internal async Task<List<OrderViewModel>> CalculateOrder(OrderModel model) {
			using (var db = new SupplierbaseContext()) {
				var result = new List<OrderViewModel>();

				var limits = await (from sd in db.ShopDistributions
									join l in db.Limits on sd.LimitId equals l.Id
									join s in db.Shops on sd.ShopId equals s.Id
									where sd.ProductId == model.ProductId && sd.Percentage > 0
									select new DistributionModel {
										LimitValue = l.LimitValue,
										RangeCapacity = l.RangeCapacity,
										PercentageDistribution = sd.Percentage,
										ShopId = sd.ShopId,
										ShopName = s.Name
									}).GroupBy(l => new { l.ShopId, l.ShopName })
									  .ToListAsync();

				foreach (var item in limits) {
					var shop = new OrderViewModel {
						ShopId = item.Key.ShopId,
						ShopName = item.Key.ShopName,
						Quantity = item.ToList().CalculateQuantity(model.Quantity.Value)
					};
					result.Add(shop);
				}

				return result;
			}
		}
	}
}
