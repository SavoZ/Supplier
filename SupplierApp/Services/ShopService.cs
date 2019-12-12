using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models.Distribution;
using Common.Models.Shop;
using Database.DB;
using Microsoft.EntityFrameworkCore;

namespace SupplierApp.Services {
	public class ShopService {
		internal async Task SaveShop(ShopPostModel model) {
			using (var db = new SupplierbaseContext()) {
				var entity = await db.Shops.FirstOrDefaultAsync(p => p.Id == model.Id);

				if (entity == null) {
					entity = new Shop {
						Name = model.Name,
					};
					await db.Shops.AddAsync(entity);
				} else {
					entity.Name = model.Name;
					db.Shops.Update(entity);
				}

				await db.SaveChangesAsync();
				model.Id = entity.Id;

				await AddShopProducts(model);

			}
		}

		internal async Task<List<ShopDistributionModel>> GetShopsByProductId(int productId) {
			var result = new List<ShopDistributionModel>();

			using (var db = new SupplierbaseContext()) {
				var shops = await (from s in db.Shops
								   join sp in db.ShopProducts on s.Id equals sp.ShopId
								   where sp.ProductId == productId
								   select s).ToListAsync();

				foreach (var shop in shops) {
					var model = new ShopDistributionModel {
						ShopId = shop.Id,
						ShopName = shop.Name,
						ProductDistribution = await GetProductDistribution(shop.Id, productId)
					};
					result.Add(model);
				}

				return result;
			}
		}

		internal async Task DeleteShop(int shopId) {
			using (var db = new SupplierbaseContext()) {
				var shopProducts = await db.ShopProducts.Where(s => s.ShopId == shopId).ToListAsync();
				db.RemoveRange(shopProducts);
				var shopDistributions = await db.ShopDistributions.Where(s => s.ShopId == shopId).ToListAsync();
				db.RemoveRange(shopDistributions);

				var shops = await db.Shops.Where(s => s.Id == shopId).ToListAsync();
				db.RemoveRange(shops);

				await db.SaveChangesAsync();
			}
		}

		private async Task<List<DistributionModel>> GetProductDistribution(int shopId, int productId) {
			using (var db = new SupplierbaseContext()) {

				var result = await (from sd in db.ShopDistributions
									join l in db.Limits on sd.LimitId equals l.Id
									where sd.ProductId == productId && sd.ShopId == shopId
									select new DistributionModel {
										LimitId = l.Id,
										LimitValue = l.LimitValue,
										PercentageDistribution = sd.Percentage
									}).ToListAsync();

				if (!result.Any()) {
					result = await (from l in db.Limits
									where l.ProductId == productId
									select new DistributionModel {
										LimitId = l.Id,
										LimitValue = l.LimitValue,
										PercentageDistribution = 0
									}).ToListAsync();
				}

				return result;
			}
		}

		private async Task AddShopProducts(ShopPostModel model) {
			using (var db = new SupplierbaseContext()) {
				var shopProducts = await db.ShopProducts.Where(s => s.ShopId == model.Id).ToListAsync();
				db.RemoveRange(shopProducts);

				var models = model.Products.Select(productId => new ShopProduct {
					ShopId = model.Id,
					ProductId = productId,
				}).ToList();

				await db.ShopProducts.AddRangeAsync(models);
				await db.SaveChangesAsync();
			}
		}

		public async Task<List<ShopViewModel>> GetShops() {
			using (var db = new SupplierbaseContext()) {
				var shops = await (from s in db.Shops
								   let products = (from p in db.Products
												   join sp in db.ShopProducts on p.Id equals sp.ProductId
												   where sp.ShopId == s.Id
												   select p.Name).ToList()
								   select new ShopViewModel {
									   Id = s.Id,
									   Name = s.Name,
									   Products = string.Join(", ", products)
								   }).ToListAsync();

				return shops;
			}
		}
	}
}
