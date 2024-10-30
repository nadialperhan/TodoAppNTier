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
    public class ParameterGroupConfiguration : IEntityTypeConfiguration<ParameterGroup>
    {
        public void Configure(EntityTypeBuilder<ParameterGroup> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ParameterGroupName).IsRequired();
        }
    }
}
