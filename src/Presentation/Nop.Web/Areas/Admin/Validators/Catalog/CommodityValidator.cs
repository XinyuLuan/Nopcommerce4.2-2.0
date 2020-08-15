using FluentValidation;
using Nop.Core.Domain.Catalog;
using Nop.Data;
using Nop.Services.Localization;
using Nop.Services.Seo;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Framework.Validators;

namespace Nop.Web.Areas.Admin.Validators.Catalog
{
    public partial class CommodityValidator : BaseNopValidator<CommodityModel> 
    {
        public CommodityValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(localizationService.GetResource("Admin.Catalog.Commodity.Fields.Name.Required"));

            SetDatabaseValidationRules<Commodity>(dbContext);
        }
    }
}
