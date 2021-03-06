﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

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

        public CommoditySearchModel(string name)
        {
           SearchCommodityName = name;
        }
        #endregion

        #region Properties

        [NopResourceDisplayName("Admin.Catalog.Products.List.SearchCommodityName")]
        public string SearchCommodityName { get; set; }

        #endregion
    }
}