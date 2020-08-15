using System.Collections.Generic;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Orders;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Models.Catalog;

namespace Nop.Web.Areas.Admin.Factories
{
    /// <summary>
    /// Represents the interface of the product model factory
    /// </summary>
    public partial interface ICommodityModelFactory
    {
        CommoditySearchModel PrepareCommoditySearchModel(CommoditySearchModel searchModel);
        CommodityListModel PrepareCommodityListModel(CommoditySearchModel searchModel);
    }
}
