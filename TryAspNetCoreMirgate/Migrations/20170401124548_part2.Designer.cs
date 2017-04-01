using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TryAspNetCoreMirgate.EfCore;

namespace TryAspNetCoreMirgate.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20170401124548_part2")]
    partial class part2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TryAspNetCoreMirgate.EfCore.MyEntity", b =>
                {
                    b.Property<int>("MyEntityId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Part1");

                    b.Property<int>("Part2");

                    b.HasKey("MyEntityId");

                    b.ToTable("MyEntities");
                });
        }
    }
}
