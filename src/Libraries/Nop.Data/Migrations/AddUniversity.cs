using FluentMigrator;
using Nop.Core.Domain.Catalog;

namespace Nop.Data.Migrations
{
    [NopMigration("2022/01/01 12:00:00:2551785", "Category. Add some new property", UpdateMigrationType.Data, MigrationProcessType.Update)]
    public class AddUniversity : AutoReversingMigration
    {
        /// <summary>Collect the UP migration expressions</summary>
        public override void Up()
        {
   
            Create.Column(nameof(Category.BSID))
            .OnTable(nameof(Category))
            .AsInt32()
            .Nullable();
        }
    }
}