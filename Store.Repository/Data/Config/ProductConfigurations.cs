using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Data.Config
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name)
                .IsRequired()
                .HasMaxLength(100);


            builder.Property(P => P.Description)
                .IsRequired();

            builder.Property(P => P.PictureUrl)
                .IsRequired();

            builder.Property(P => P.Price)
                .IsRequired()
                .HasColumnType("decimal(19,2)");

            builder.HasOne(P => P.Brand)
                .WithMany()
                .HasForeignKey(P => P.BrandId);

            builder.HasOne(C => C.Category)
                .WithMany()
                .HasForeignKey(P => P.CategoryId);
        }
    }
}
