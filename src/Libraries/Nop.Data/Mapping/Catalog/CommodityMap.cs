using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    /// <summary>
    /// Represents a product mapping configuration
    /// </summary>
    public partial class CommodityMap : NopEntityTypeConfiguration<Commodity>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<Commodity> builder)
        {
            builder.ToTable(nameof(Commodity));
            builder.HasKey(commodity => commodity.Id);

            builder.Property(commodity => commodity.Name).HasMaxLength(400).IsRequired();
            builder.Property(commodity => commodity.Price).HasColumnType("decimal(18, 4)");
            builder.Property(commodity => commodity.AllowedQuantities).HasMaxLength(1000);

            builder.Ignore(commodity => commodity.SubjectToAcl);
            base.Configure(builder);
        }

        #endregion
    }
}