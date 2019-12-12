using System.Linq;
using System.Threading.Tasks;
using Common.Models.Distribution;
using Database.DB;
using Microsoft.EntityFrameworkCore;

namespace SupplierApp.Services {
	public class DistributionService {
		internal async Task SaveDistribution(DistributionPostModel model) {
			using (var db = new SupplierbaseContext()) {
				var shopDistribution = await db.ShopDistributions.Where(s => s.ProductId == model.ProductId).ToListAsync();
				db.RemoveRange(shopDistribution);

				var result = model.Shops.SelectMany(s => s.ProductDistribution.Select(d => new ShopDistribution {
					ProductId = model.ProductId,
					ShopId = s.ShopId,
					LimitId = d.LimitId,
					Percentage = d.PercentageDistribution
				})).ToList();

				await db.ShopDistributions.AddRangeAsync(result);
				await db.SaveChangesAsync();
			}
		}
	}
}
