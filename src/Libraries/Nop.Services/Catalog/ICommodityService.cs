using System;
using System.Collections.Generic;
using System.Text;
using Nop.Core;
using Nop.Core.Domain.Catalog;

namespace Nop.Services.Catalog
{
    public partial interface ICommodityService
    {
        void DeleteCommodity(Commodity commodity);
        Commodity GetCommodityById(int commodity);
        IList<Commodity> GetAllCommoditysDisplayedOnHomepage();
        void InsertCommodity(Commodity commodity);
        void UpdateCommodity(Commodity commodity);
        IPagedList<Commodity> SearchCommodity(
            string name = null,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            IList<int> categoryIds = null,
            int manufacturerId = 0,
            int storeId = 0,
            int vendorId = 0,
            int warehouseId = 0,
            ProductType? productType = null,
            bool visibleIndividuallyOnly = false,
            bool markedAsNewOnly = false,
            bool? featuredProducts = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            int productTagId = 0,
            string keywords = null,
            bool searchDescriptions = false,
            bool searchManufacturerPartNumber = true,
            bool searchSku = true,
            bool searchProductTags = false,
            int languageId = 0,
            IList<int> filteredSpecs = null,
            ProductSortingEnum orderBy = ProductSortingEnum.Position,
            bool showHidden = false,
            bool? overridePublished = null);
    }
}
