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
        IPagedList<Commodity> SearchCommodities(int pageIndex = 0,
                                                     int pageSize = int.MaxValue,
                                                      decimal? priceMin = null,
                                                      decimal? priceMax = null,
                                                      string keywords = null,
                                                      IList<int> filteredSpecs = null,
                                                      bool showHidden = false);
        IPagedList<Commodity> SearchCommodities(
            out IList<int> filterableSpecificationAttributeOptionIds,
            bool loadFilterableSpecificationAttributeOptionIds = false,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            decimal? priceMin = null,
            decimal? priceMax = null,
            string keywords = null,
            IList<int> filteredSpecs = null,
            bool showHidden = false);
    }
}
