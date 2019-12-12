export class DistributionModel {
    productId: number = null;
    shops = Array<DistributionShopsModel>();
}

export class DistributionShopsModel {
    shopId: number;
    ShopName: string;
    PercentageDistribution: number;
}
