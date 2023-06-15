using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Plugin.Widgets.StudentCRUD.Domain;

namespace Nop.Plugin.Widgets.StudentCRUD.Service
{
    public interface IStudentService
    {
        Task<Student> GetStudentByIdAsync(int studentId);
        Task DeleteStudentAsync(Student student);
        Task InsertStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task<IList<Student>> GetAllStudentsAsync();
        Task<IPagedList<Student>> GetAllStudentsPagedAsync();

        // like the GetAllCategoriesAsync method of CategoryService
        Task<IPagedList<Student>> GetAllStudentsPagedAsync(string studentName, string studentMail,
            int pageIndex = 0, int pageSize = int.MaxValue/*, bool showHidden = false, bool? overridePublished = null*/);
    }
}
