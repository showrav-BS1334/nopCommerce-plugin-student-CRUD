using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.StudentCRUD.Domain;

namespace Nop.Plugin.Widgets.StudentCRUD.Data.Migration
{
    //[NopMigration("2023/06/04 01:03:00896432", "Migration for student model", MigrationProcessType.Installation)]
    [NopMigration("2023/06/04 12:00:00:8531778", "Migration for student model", UpdateMigrationType.Data, MigrationProcessType.Installation)]

    public class StudentMigration : AutoReversingMigration
    {
        public override void Up()
        {
            Create.TableFor<Student>();
        }
    }
}
