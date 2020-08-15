using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Services.Events;

namespace Nop.Services.Catalog
{
    public partial class CommodityService : ICommodityService
    {
        #region Fields
        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<Commodity> _commodityRepository;
        private readonly ICacheManager _cacheManager;
        #endregion

        public CommodityService(
            IEventPublisher eventPublisher,
            IRepository<Commodity> commodityRepository,
            ICacheManager cacheManager)
        {
            _eventPublisher = eventPublisher;
            _commodityRepository = commodityRepository;
            _cacheManager = cacheManager;
        }
        public void DeleteCommodity(Commodity commodity)
        {
            if (commodity == null)
                throw new ArgumentNullException(nameof(commodity));
            commodity.Deleted = true;
            //delete commodity
            UpdateCommodity(commodity);

            //event notification
            _eventPublisher.EntityDeleted(commodity);
        }

        public IList<Commodity> GetAllCommoditysDisplayedOnHomepage()
        {
            var query = from p in _commodityRepository.Table
                        orderby p.Id
                        where !p.Deleted
                        select p;
            var commodities = query.ToList();
            return commodities;
        }

        public Commodity GetCommodityById(int commodityId)
        {
            if (commodityId == 0)
                return null;
            var key = string.Format(NopCatalogDefaults.ProductsByIdCacheKey, commodityId);
            return _cacheManager.Get(key, () => _commodityRepository.GetById(commodityId));
        }

        public void InsertCommodity(Commodity commodity)
        {
            if (commodity == null)
                throw new ArgumentNullException(nameof(commodity));

            //insert
            _commodityRepository.Insert(commodity);

            //clear cache
            _cacheManager.RemoveByPrefix(NopCatalogDefaults.ProductsPrefixCacheKey);

            //event notification
            _eventPublisher.EntityInserted(commodity);
        }

        public IPagedList<Commodity> SearchCommodity(int pageIndex = 0, int pageSize = int.MaxValue, IList<int> categoryIds = null, int manufacturerId = 0, int storeId = 0, int vendorId = 0, int warehouseId = 0, ProductType? productType = null, bool visibleIndividuallyOnly = false, bool markedAsNewOnly = false, bool? featuredProducts = null, decimal? priceMin = null, decimal? priceMax = null, int productTagId = 0, string keywords = null, bool searchDescriptions = false, bool searchManufacturerPartNumber = true, bool searchSku = true, bool searchProductTags = false, int languageId = 0, IList<int> filteredSpecs = null, ProductSortingEnum orderBy = ProductSortingEnum.Position, bool showHidden = false, bool? overridePublished = null)
        {
            throw new NotImplementedException();
        }

        public IPagedList<Commodity> SearchCommodity(
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
            bool? overridePublished = null)
        {
            throw new NotImplementedException();
        }

        public void UpdateCommodity(Commodity commodity)
        {
            if (commodity == null)
                throw new ArgumentNullException(nameof(commodity));

            //update
            _commodityRepository.Update(commodity);

            //cache
            _cacheManager.RemoveByPrefix(NopCatalogDefaults.ProductsPrefixCacheKey);

            //event notification
            _eventPublisher.EntityUpdated(commodity);
        }
    }
}
