using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Services.Events;
using Nop.Core.Data.Extensions;
using Nop.Data;

namespace Nop.Services.Catalog
{
    public partial class CommodityService : ICommodityService
    {
        #region Fields
        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<Commodity> _commodityRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        #endregion

        public CommodityService(
            IEventPublisher eventPublisher,
            IRepository<Commodity> commodityRepository,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext)
        {
            _eventPublisher = eventPublisher;
            _commodityRepository = commodityRepository;
            _cacheManager = cacheManager;
            _dataProvider = dataProvider;
            _dbContext = dbContext;
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

        public IPagedList<Commodity> SearchCommodities(int pageIndex = 0,
                                                       int pageSize = int.MaxValue,  
                                                       decimal? priceMin = null, 
                                                       decimal? priceMax = null, 
                                                       string keywords = null,   
                                                       IList<int> filteredSpecs = null,  
                                                       bool showHidden = false)
        {
            return SearchCommodities(out var _, false,
                pageIndex, pageSize, 
                priceMin, priceMax,keywords, filteredSpecs, showHidden);

        }

        public virtual IPagedList<Commodity> SearchCommodities(
            out IList<int> filterableSpecificationAttributeOptionIds,
            bool loadFilterableSpecificationAttributeOptionIds = false,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            decimal? priceMin = null,
            decimal? priceMax = null,
            string keywords = null,
            IList<int> filteredSpecs = null,
            bool showHidden = false)
        {
            filterableSpecificationAttributeOptionIds = new List<int>();

            //search by keyword
            var searchLocalizedValue = false;

            //pass specification identifiers as comma-delimited string
            var commaSeparatedSpecIds = string.Empty;
            if (filteredSpecs != null)
            {
                ((List<int>)filteredSpecs).Sort();
                commaSeparatedSpecIds = string.Join(",", filteredSpecs);
            }

            //some databases don't support int.MaxValue
            if (pageSize == int.MaxValue)
                pageSize = int.MaxValue - 1;

            //prepare input parameters
            var pPriceMin = _dataProvider.GetDecimalParameter("PriceMin", priceMin);
            var pPriceMax = _dataProvider.GetDecimalParameter("PriceMax", priceMax);
            var pKeywords = _dataProvider.GetStringParameter("Keywords", keywords);
            var pFilteredSpecs = _dataProvider.GetStringParameter("FilteredSpecs", commaSeparatedSpecIds);
            var pPageIndex = _dataProvider.GetInt32Parameter("PageIndex", pageIndex);
            var pPageSize = _dataProvider.GetInt32Parameter("PageSize", pageSize);
            var pShowHidden = _dataProvider.GetBooleanParameter("ShowHidden", showHidden);
            var pLoadFilterableSpecificationAttributeOptionIds = _dataProvider.GetBooleanParameter("LoadFilterableSpecificationAttributeOptionIds", loadFilterableSpecificationAttributeOptionIds);
            // 
            //prepare output parameters questionable
            var pFilterableSpecificationAttributeOptionIds = _dataProvider.GetOutputStringParameter("FilterableSpecificationAttributeOptionIds");
            pFilterableSpecificationAttributeOptionIds.Size = int.MaxValue - 1;
            var pTotalRecords = _dataProvider.GetOutputInt32Parameter("TotalRecords");

            //invoke stored procedure questionable
            var commodities = _dbContext.EntityFromSql<Commodity>("CommodityLoadAllPaged",
                pPriceMin,
                pPriceMax,
                pKeywords,
                pFilteredSpecs,
                pPageIndex,
                pPageSize,
                pShowHidden,
                pLoadFilterableSpecificationAttributeOptionIds,
                pFilterableSpecificationAttributeOptionIds,
                pTotalRecords).ToList();
            //get filterable specification attribute option identifier
            var filterableSpecificationAttributeOptionIdsStr =
                pFilterableSpecificationAttributeOptionIds.Value != DBNull.Value
                    ? (string)pFilterableSpecificationAttributeOptionIds.Value
                    : string.Empty;

            if (loadFilterableSpecificationAttributeOptionIds &&
                !string.IsNullOrWhiteSpace(filterableSpecificationAttributeOptionIdsStr))
            {
                filterableSpecificationAttributeOptionIds = filterableSpecificationAttributeOptionIdsStr
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x.Trim()))
                    .ToList();
            }
            //return products
            var totalRecords = pTotalRecords.Value != DBNull.Value ? Convert.ToInt32(pTotalRecords.Value) : 0;
            return new PagedList<Commodity>(commodities, pageIndex, pageSize, totalRecords);
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
