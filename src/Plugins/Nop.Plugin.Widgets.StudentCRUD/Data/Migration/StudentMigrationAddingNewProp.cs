using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.StudentCRUD.Domain;

namespace Nop.Plugin.Widgets.StudentCRUD
{
    [NopMigration("2023/06/12 03:06:45:2696892", "Student. Add some new property", UpdateMigrationType.Data, MigrationProcessType.Update)]
    public class StudentMigrationAddingNewProp : AutoReversingMigration
    {
        /// <summary>Collect the UP migration expressions</summary>
        public override void Up()
        {
            //Create.Column(nameof(Student.Mail))
            //.OnTable(nameof(Student))
            //.AsString(100)
            //.Nullable();

            //Create.Column(nameof(Student.DateOfBirth))
            //.OnTable(nameof(Student))
            //.AsDate()
            //.Nullable();

            Create.Column(nameof(Student.Gender))
            .OnTable(nameof(Student))
            .AsString(10)
            .Nullable();
        }
    }
}