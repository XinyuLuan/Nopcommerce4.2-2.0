using System;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Seo;
using Nop.Core.Domain.Stores;

namespace Nop.Core.Domain.Catalog
{
    public class Commodity : BaseEntity, ILocalizedEntity, ISlugSupported, IAclSupported
    {
        public string Name { get; set; }
        public string AllowedQuantities { get; set; }
        public decimal Price { get; set; }
        public bool Deleted { get; set; }
        public bool SubjectToAcl { get; set; }
    }
}
