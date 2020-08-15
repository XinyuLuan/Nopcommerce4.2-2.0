using System;
using System.Collections.Generic;
using Nop.Core.Domain.Catalog;
using Nop.Web.Framework.Models;
using Nop.Web.Models.Media;

namespace Nop.Web.Models.Catalog
{
    public partial class CommodityOverviewModel : BaseNopEntityModel
    {
        public CommodityOverviewModel()
        {
        }
        public string Name { get; set; }

        public string AllowedQuantities { get; set; }

        public decimal Price { get; set; }

       
    }
}