using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Tax;
using Nop.Core.Domain.Vendors;
using Nop.Services.Catalog;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Discounts;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Orders;
using Nop.Services.Seo;
using Nop.Services.Shipping;
using Nop.Services.Stores;
using Nop.Web.Areas.Admin.Infrastructure.Cache;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Areas.Admin.Models.Orders;
using Nop.Web.Framework.Extensions;
using Nop.Web.Framework.Factories;
using Nop.Web.Framework.Models.Extensions;

using Nop.Web.Areas.Admin.Factories;

namespace Nop.Web.Areas.Admin.Factories
{
    /// <summary>
    /// Represents the product model factory implementation
    /// </summary>
    public partial class CommodityModelFactory : ICommodityModelFactory
    {
        #region Fields

        private readonly CatalogSettings _catalogSettings;
        private readonly CurrencySettings _currencySettings;
        private readonly IAclSupportedModelFactory _aclSupportedModelFactory;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        private readonly ICategoryService _categoryService;
        private readonly ICurrencyService _currencyService;
        private readonly ICustomerService _customerService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IDiscountService _discountService;
        private readonly IDiscountSupportedModelFactory _discountSupportedModelFactory;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedModelFactory _localizedModelFactory;
        private readonly IManufacturerService _manufacturerService;
        private readonly IMeasureService _measureService;
        private readonly IOrderService _orderService;
        private readonly IPictureService _pictureService;
        private readonly IProductAttributeFormatter _productAttributeFormatter;
        private readonly IProductAttributeParser _productAttributeParser;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IProductService _productService;
        private readonly IProductTagService _productTagService;
        private readonly IProductTemplateService _productTemplateService;
        private readonly ISettingModelFactory _settingModelFactory;
        private readonly IShipmentService _shipmentService;
        private readonly IShippingService _shippingService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ISpecificationAttributeService _specificationAttributeService;
        private readonly IStaticCacheManager _cacheManager;
        private readonly IStoreMappingSupportedModelFactory _storeMappingSupportedModelFactory;
        private readonly IStoreService _storeService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IWorkContext _workContext;
        private readonly MeasureSettings _measureSettings;
        private readonly TaxSettings _taxSettings;
        private readonly VendorSettings _vendorSettings;

        private readonly ICommodityService _commodityService;

        #endregion

        #region Ctor

        public CommodityModelFactory(CatalogSettings catalogSettings,
            CurrencySettings currencySettings,
            IAclSupportedModelFactory aclSupportedModelFactory,
            IBaseAdminModelFactory baseAdminModelFactory,
            ICategoryService categoryService,
            ICurrencyService currencyService,
            ICustomerService customerService,
            IDateTimeHelper dateTimeHelper,
            IDiscountService discountService,
            IDiscountSupportedModelFactory discountSupportedModelFactory,
            ILocalizationService localizationService,
            ILocalizedModelFactory localizedModelFactory,
            IManufacturerService manufacturerService,
            IMeasureService measureService,
            IOrderService orderService,
            IPictureService pictureService,
            IProductAttributeFormatter productAttributeFormatter,
            IProductAttributeParser productAttributeParser,
            IProductAttributeService productAttributeService,
            IProductService productService,
            IProductTagService productTagService,
            IProductTemplateService productTemplateService,
            ISettingModelFactory settingModelFactory,
            IShipmentService shipmentService,
            IShippingService shippingService,
            IShoppingCartService shoppingCartService,
            ISpecificationAttributeService specificationAttributeService,
            IStaticCacheManager cacheManager,
            IStoreMappingSupportedModelFactory storeMappingSupportedModelFactory,
            IStoreService storeService,
            IUrlRecordService urlRecordService,
            IWorkContext workContext,
            MeasureSettings measureSettings,
            TaxSettings taxSettings,
            VendorSettings vendorSettings,

            ICommodityService commodityService
            )
        {
            _catalogSettings = catalogSettings;
            _currencySettings = currencySettings;
            _aclSupportedModelFactory = aclSupportedModelFactory;
            _baseAdminModelFactory = baseAdminModelFactory;
            _cacheManager = cacheManager;
            _categoryService = categoryService;
            _currencyService = currencyService;
            _customerService = customerService;
            _dateTimeHelper = dateTimeHelper;
            _discountService = discountService;
            _discountSupportedModelFactory = discountSupportedModelFactory;
            _localizationService = localizationService;
            _localizedModelFactory = localizedModelFactory;
            _manufacturerService = manufacturerService;
            _measureService = measureService;
            _measureSettings = measureSettings;
            _orderService = orderService;
            _pictureService = pictureService;
            _productAttributeFormatter = productAttributeFormatter;
            _productAttributeParser = productAttributeParser;
            _productAttributeService = productAttributeService;
            _productService = productService;
            _productTagService = productTagService;
            _productTemplateService = productTemplateService;
            _settingModelFactory = settingModelFactory;
            _shipmentService = shipmentService;
            _shippingService = shippingService;
            _shoppingCartService = shoppingCartService;
            _specificationAttributeService = specificationAttributeService;
            _storeMappingSupportedModelFactory = storeMappingSupportedModelFactory;
            _storeService = storeService;
            _urlRecordService = urlRecordService;
            _workContext = workContext;
            _taxSettings = taxSettings;
            _vendorSettings = vendorSettings;

            _commodityService = commodityService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepare product search model
        /// </summary>
        /// <param name="searchModel">Product search model</param>
        /// <returns>Product search model</returns>
        public virtual CommoditySearchModel PrepareCommoditySearchModel(CommoditySearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //prepare grid
            searchModel.SetGridPageSize();

            return searchModel;
        }

        /// <summary>
        /// Prepare paged product list model
        /// </summary>
        /// <param name="searchModel">Product search model</param>
        /// <returns>Product list model</returns>
        public virtual CommodityListModel PrepareCommodityListModel(CommoditySearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get commodities

            var commodities = _commodityService.SearchCommodities(
                keywords: searchModel.SearchCommodityName,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize
            );

            //prepare list model
            var model = new CommodityListModel().PrepareToGrid(searchModel, commodities, () =>
            {
                return commodities.Select(commodity =>
                {
                    //fill in model values from the entity
                    var commodityModel = commodity.ToModel<CommodityModel>();

                    //fill in additional values (not existing in the entity)
                    commodityModel.SeName = _urlRecordService.GetSeName(commodity, 0, true, false);

                    return commodityModel;
                });
            });

            return model;
        }

        /*
/// <summary>
/// Prepare product model
/// </summary>
/// <param name="model">Product model</param>
/// <param name="product">Product</param>
/// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
/// <returns>Product model</returns>
public virtual ProductModel PrepareProductModel(ProductModel model, Product product, bool excludeProperties = false)
{
Action<ProductLocalizedModel, int> localizedModelConfiguration = null;

if (product != null)
{
//fill in model values from the entity
if (model == null)
{
model = product.ToModel<ProductModel>();
model.SeName = _urlRecordService.GetSeName(product, 0, true, false);
}

var parentGroupedProduct = _productService.GetProductById(product.ParentGroupedProductId);
if (parentGroupedProduct != null)
{
model.AssociatedToProductId = product.ParentGroupedProductId;
model.AssociatedToProductName = parentGroupedProduct.Name;
}

model.LastStockQuantity = product.StockQuantity;
model.ProductTags = string.Join(", ", _productTagService.GetAllProductTagsByProductId(product.Id).Select(tag => tag.Name));
model.ProductAttributesExist = _productAttributeService.GetAllProductAttributes().Any();

if (!excludeProperties)
{
model.SelectedCategoryIds = _categoryService.GetProductCategoriesByProductId(product.Id, true)
.Select(productCategory => productCategory.CategoryId).ToList();
model.SelectedManufacturerIds = _manufacturerService.GetProductManufacturersByProductId(product.Id, true)
.Select(productManufacturer => productManufacturer.ManufacturerId).ToList();
}

//prepare copy product model
PrepareCopyProductModel(model.CopyProductModel, product);

//prepare nested search model
PrepareRelatedProductSearchModel(model.RelatedProductSearchModel, product);
PrepareCrossSellProductSearchModel(model.CrossSellProductSearchModel, product);
PrepareAssociatedProductSearchModel(model.AssociatedProductSearchModel, product);
PrepareProductPictureSearchModel(model.ProductPictureSearchModel, product);
PrepareProductSpecificationAttributeSearchModel(model.ProductSpecificationAttributeSearchModel, product);
PrepareProductOrderSearchModel(model.ProductOrderSearchModel, product);
PrepareTierPriceSearchModel(model.TierPriceSearchModel, product);
PrepareStockQuantityHistorySearchModel(model.StockQuantityHistorySearchModel, product);
PrepareProductAttributeMappingSearchModel(model.ProductAttributeMappingSearchModel, product);
PrepareProductAttributeCombinationSearchModel(model.ProductAttributeCombinationSearchModel, product);

//define localized model configuration action
localizedModelConfiguration = (locale, languageId) =>
{
locale.Name = _localizationService.GetLocalized(product, entity => entity.Name, languageId, false, false);
locale.FullDescription = _localizationService.GetLocalized(product, entity => entity.FullDescription, languageId, false, false);
locale.ShortDescription = _localizationService.GetLocalized(product, entity => entity.ShortDescription, languageId, false, false);
locale.MetaKeywords = _localizationService.GetLocalized(product, entity => entity.MetaKeywords, languageId, false, false);
locale.MetaDescription = _localizationService.GetLocalized(product, entity => entity.MetaDescription, languageId, false, false);
locale.MetaTitle = _localizationService.GetLocalized(product, entity => entity.MetaTitle, languageId, false, false);
locale.SeName = _urlRecordService.GetSeName(product, languageId, false, false);
};
}

//set default values for the new model
if (product == null)
{
model.MaximumCustomerEnteredPrice = 1000;
model.MaxNumberOfDownloads = 10;
model.RecurringCycleLength = 100;
model.RecurringTotalCycles = 10;
model.RentalPriceLength = 1;
model.StockQuantity = 10000;
model.NotifyAdminForQuantityBelow = 1;
model.OrderMinimumQuantity = 1;
model.OrderMaximumQuantity = 10000;
model.TaxCategoryId = _taxSettings.DefaultTaxCategoryId;
model.UnlimitedDownloads = true;
model.IsShipEnabled = true;
model.AllowCustomerReviews = true;
model.Published = true;
model.VisibleIndividually = true;
}

model.PrimaryStoreCurrencyCode = _currencyService.GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId).CurrencyCode;
model.BaseWeightIn = _measureService.GetMeasureWeightById(_measureSettings.BaseWeightId).Name;
model.BaseDimensionIn = _measureService.GetMeasureDimensionById(_measureSettings.BaseDimensionId).Name;
model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;
model.HasAvailableSpecificationAttributes = _cacheManager.Get(NopModelCacheDefaults.SpecAttributesModelKey, () =>
{
return _specificationAttributeService.GetSpecificationAttributesWithOptions()
.Select(attributeWithOption => new SelectListItem(attributeWithOption.Name, attributeWithOption.Id.ToString()))
.ToList();
}).Any();

//prepare localized models
if (!excludeProperties)
model.Locales = _localizedModelFactory.PrepareLocalizedModels(localizedModelConfiguration);

//prepare editor settings
model.ProductEditorSettingsModel = _settingModelFactory.PrepareProductEditorSettingsModel();

//prepare available product templates
_baseAdminModelFactory.PrepareProductTemplates(model.AvailableProductTemplates, false);

//prepare available product types
var productTemplates = _productTemplateService.GetAllProductTemplates();
foreach (var productType in Enum.GetValues(typeof(ProductType)).OfType<ProductType>())
{
model.ProductsTypesSupportedByProductTemplates.Add((int)productType, new List<SelectListItem>());
foreach (var template in productTemplates)
{
var list = (IList<int>)TypeDescriptor.GetConverter(typeof(List<int>)).ConvertFrom(template.IgnoredProductTypes) ?? new List<int>();
if (string.IsNullOrEmpty(template.IgnoredProductTypes) || !list.Contains((int)productType))
{
model.ProductsTypesSupportedByProductTemplates[(int)productType].Add(new SelectListItem
{
 Text = template.Name,
 Value = template.Id.ToString()
});
}
}
}

//prepare available delivery dates
_baseAdminModelFactory.PrepareDeliveryDates(model.AvailableDeliveryDates,
defaultItemText: _localizationService.GetResource("Admin.Catalog.Products.Fields.DeliveryDate.None"));

//prepare available product availability ranges
_baseAdminModelFactory.PrepareProductAvailabilityRanges(model.AvailableProductAvailabilityRanges,
defaultItemText: _localizationService.GetResource("Admin.Catalog.Products.Fields.ProductAvailabilityRange.None"));

//prepare available vendors
_baseAdminModelFactory.PrepareVendors(model.AvailableVendors,
defaultItemText: _localizationService.GetResource("Admin.Catalog.Products.Fields.Vendor.None"));

//prepare available tax categories
_baseAdminModelFactory.PrepareTaxCategories(model.AvailableTaxCategories);

//prepare available warehouses
_baseAdminModelFactory.PrepareWarehouses(model.AvailableWarehouses,
defaultItemText: _localizationService.GetResource("Admin.Catalog.Products.Fields.Warehouse.None"));
PrepareProductWarehouseInventoryModels(model.ProductWarehouseInventoryModels, product);

//prepare available base price units
var availableMeasureWeights = _measureService.GetAllMeasureWeights()
.Select(weight => new SelectListItem { Text = weight.Name, Value = weight.Id.ToString() }).ToList();
model.AvailableBasepriceUnits = availableMeasureWeights;
model.AvailableBasepriceBaseUnits = availableMeasureWeights;

//prepare model categories
_baseAdminModelFactory.PrepareCategories(model.AvailableCategories, false);
foreach (var categoryItem in model.AvailableCategories)
{
categoryItem.Selected = int.TryParse(categoryItem.Value, out var categoryId)
&& model.SelectedCategoryIds.Contains(categoryId);
}

//prepare model manufacturers
_baseAdminModelFactory.PrepareManufacturers(model.AvailableManufacturers, false);
foreach (var manufacturerItem in model.AvailableManufacturers)
{
manufacturerItem.Selected = int.TryParse(manufacturerItem.Value, out var manufacturerId)
&& model.SelectedManufacturerIds.Contains(manufacturerId);
}

//prepare model discounts
var availableDiscounts = _discountService.GetAllDiscounts(DiscountType.AssignedToSkus, showHidden: true);
_discountSupportedModelFactory.PrepareModelDiscounts(model, product, availableDiscounts, excludeProperties);

//prepare model customer roles
_aclSupportedModelFactory.PrepareModelCustomerRoles(model, product, excludeProperties);

//prepare model stores
_storeMappingSupportedModelFactory.PrepareModelStores(model, product, excludeProperties);

return model;
}
*/
        #endregion
    }
}