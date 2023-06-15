using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.StudentCRUD.Domain;

namespace Nop.Plugin.Widgets.StudentCRUD
{
    [NopMigration("2023/06/13 12:41:45:2696892", "Student. Add some new property", UpdateMigrationType.Data, MigrationProcessType.Update)]
    public class StudentMigrationAddingIsActive : AutoReversingMigration
    {
        /// <summary>Collect the UP migration expressions</summary>
        public override void Up()
        {
            Create.Column(nameof(Student.IsActive))
            .OnTable(nameof(Student))
            .AsBoolean()
            .Nullable();
        }
    }
}