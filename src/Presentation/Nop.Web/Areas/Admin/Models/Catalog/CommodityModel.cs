using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Areas.Admin.Models.Settings;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Web.Areas.Admin.Models.Catalog
{
    /// <summary>
    /// Represents a product model
    /// </summary>
    public partial class CommodityModel : BaseNopEntityModel
    {
        #region Ctor

        public CommodityModel()
        {
        }

        #endregion

        #region Properties

        [NopResourceDisplayName("Admin.Catalog.Commodity.Fields.Name.Required")]
        public string Name { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Commodity.Fields.AllowedQuantities")]
        public string AllowedQuantities { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Commodity.Fields.Price")]
        public decimal Price { get; set; }
        public string SeName { get; internal set; }

        #endregion
    }

    public partial class CommodityLocalizedModel : ILocalizedLocaleModel
    {
        public int LanguageId { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.Fields.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.Fields.ShortDescription")]
        public string ShortDescription { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.Fields.FullDescription")]
        public string FullDescription { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.Fields.MetaKeywords")]
        public string MetaKeywords { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.Fields.MetaDescription")]
        public string MetaDescription { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.Fields.MetaTitle")]
        public string MetaTitle { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.Fields.SeName")]
        public string SeName { get; set; }
    }
}