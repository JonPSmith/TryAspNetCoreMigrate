using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TryAspNetCoreMirgate.EfCore;

namespace TryAspNetCoreMirgate.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TryAspNetCoreMirgate.EfCore.MyEntity", b =>
                {
                    b.Property<int>("MyEntityId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("MyEntityId");

                    b.ToTable("MyEntities");
                });
        }
    }
}
