using Common.Models.Product;
using Database.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Extensions;
using Common.Models;
using Common.Models.Limit;

namespace SupplierApp.Services {
	public class ProductService {

		public async Task SaveProduct(ProductPostModel model) {
			using (var db = new SupplierbaseContext()) {
				try {
					var entity = await db.Products.FirstOrDefaultAsync(p => p.Id == model.Id);

					if (entity == null) {
						entity = new Product {
							Name = model.Name,
						};
						await db.Products.AddAsync(entity);
					} else {
						entity.Name = model.Name;
						db.Products.Update(entity);
					}

					await db.SaveChangesAsync();
					model.Id = entity.Id;

					await AddProductLimits(model);
				} catch (Exception ex) {
					throw ex;
				}
			}
		}

		internal async Task<List<DropdownModel>> GetDropdownProducts() {
			using (var db = new SupplierbaseContext()) {
				var result = await db.Products.Select(p => new DropdownModel {
					Id = p.Id,
					Name = p.Name
				}).ToListAsync();

				return result;
			}
		}

		internal async Task<List<ProductViewModel>> GetProducts() {
			using (var db = new SupplierbaseContext()) {
				var result = await (from p in db.Products
									join l in db.Limits on p.Id equals l.ProductId into limits
									select new ProductViewModel {
										Id = p.Id,
										Name = p.Name,
										Limits = limits.Select(l => new LimitModel {
											LimitValue = l.LimitValue,
											RangeCapacity = l.RangeCapacity
										}).ToList()
									}).ToListAsync();

				return result;
			}

		}

		internal async Task<ProductViewModel> GetProductById(int productId) {
			using (var db = new SupplierbaseContext()) {
				var model = await db.Products.FirstOrDefaultAsync(p => p.Id == productId);

				if (model != null) {
					var result = new ProductViewModel {
						Id = model.Id,
						Name = model.Name,
						Limits = db.Limits.Where(l => l.ProductId == productId)
										  .Select(l => new LimitModel {
											  LimitValue = l.LimitValue,
											  RangeCapacity = l.RangeCapacity
										  }).ToList()
					};

					return result;
				} else
					throw new Exception($"Product with ID:{productId} not exist");
			}
		}

		private async Task AddProductLimits(ProductPostModel model) {
			using (var db = new SupplierbaseContext()) {
				var productLimits = await db.Limits.Where(c => c.ProductId == model.Id).ToListAsync();
				db.RemoveRange(productLimits);
				var limits = model.Limits.CalculateRangeCapacity();

				foreach (var limit in limits) {
					var entity = new Limit {
						ProductId = model.Id.Value,
						LimitValue = limit.LimitValue,
						RangeCapacity = limit.RangeCapacity,
					};
					db.Limits.Add(entity);
				}
				await db.SaveChangesAsync();
			}
		}
	}
}
