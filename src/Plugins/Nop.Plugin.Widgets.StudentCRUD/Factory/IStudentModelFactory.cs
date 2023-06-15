using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Plugin.Widgets.StudentCRUD.Domain;
using Nop.Plugin.Widgets.StudentCRUD.Models;

namespace Nop.Plugin.Widgets.StudentCRUD.Factory
{
    public interface IStudentModelFactory
    {
        // interface er method gula always public and abstract!
        Task<IEnumerable<StudentModel>> PrepareStudentListModelAsync();
        Task<IPagedList<Student>> PrepareStudentPagedListModelAsync();
        Task<StudentListModel> PrepareStudentListModelAsync(StudentSearchModel searchModel);
        Task<StudentModel> PrepareStudentModelAsync(int id);
        Task<StudentModel> PrepareStudentModelAsync(StudentModel model, Student student, bool excludeProperties = false);

        Task<Student> PrepareStudentDomainAsync(StudentModel model);
        Task<StudentSearchModel> PrepareStudentSearchModelAsync(StudentSearchModel searchModel);
    }
}
