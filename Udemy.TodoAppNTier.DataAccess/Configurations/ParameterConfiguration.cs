using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Entities.Domains;

namespace Udemy.TodoAppNTier.DataAccess.Configurations
{
    public class ParameterConfiguration : IEntityTypeConfiguration<Parameter>
    {
        public void Configure(EntityTypeBuilder<Parameter> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Text).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Value).IsRequired().HasMaxLength(200);

            builder.HasOne(x => x.ParameterGroup).WithMany(x => x.Parameter).HasForeignKey(x => x.ParameterGroupId);
        }
    }
}
