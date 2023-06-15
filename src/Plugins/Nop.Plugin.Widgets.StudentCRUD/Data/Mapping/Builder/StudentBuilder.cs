using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.StudentCRUD.Domain;

namespace Nop.Plugin.Widgets.StudentCRUD.Data.Mapping.Builder
{
    public class StudentBuilder : NopEntityBuilder<Student>
    {
        public override void MapEntity(CreateTableExpressionBuilder table) => table
       .WithColumn(nameof(Student.Name)).AsString(100).NotNullable()
       .WithColumn(nameof(Student.Age)).AsInt32().Nullable();
    }
}
