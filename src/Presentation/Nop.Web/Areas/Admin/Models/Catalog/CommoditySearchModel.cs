using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Web.Areas.Admin.Models.Catalog
{
    /// <summary>
    /// Represents a product search model
    /// </summary>
    public partial class CommoditySearchModel : BaseSearchModel
    {
        #region Ctor

        public CommoditySearchModel()
        {
        }

        #endregion

        #region Properties

        [NopResourceDisplayName("Admin.Catalog.Products.List.SearchCommodityName")]
        public string SearchCommodityName { get; set; }

        #endregion
    }
}