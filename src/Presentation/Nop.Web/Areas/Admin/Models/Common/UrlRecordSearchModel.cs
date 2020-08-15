using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Web.Areas.Admin.Models.Common
{
    /// <summary>
    /// Represents an URL record search model
    /// </summary>
    public partial class UrlRecordSearchModel : BaseSearchModel
    {
        #region Properties

        [NopResourceDisplayName("Admin.System.SeNames.Name")]
        public string SeName { get; set; }

        #endregion
    }
}