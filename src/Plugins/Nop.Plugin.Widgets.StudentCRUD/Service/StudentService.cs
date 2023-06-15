using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Data;
using Nop.Plugin.Widgets.StudentCRUD.Domain;

namespace Nop.Plugin.Widgets.StudentCRUD.Service
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _studentRepository;
        public StudentService(IRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public virtual async Task<Student> GetStudentByIdAsync(int studentId)
        {
            return await _studentRepository.GetByIdAsync(studentId);
        }

        public virtual async Task DeleteStudentAsync(Student student)
        {
            await _studentRepository.DeleteAsync(student);
        }

        public virtual async Task InsertStudentAsync(Student student)
        {
            await _studentRepository.InsertAsync(student);
        }

        public virtual async Task UpdateStudentAsync(Student student)
        {
            await _studentRepository.UpdateAsync(student);
        }

        public virtual async Task<IList<Student>> GetAllStudentsAsync()
        {
            return await _studentRepository.GetAllAsync(query =>
            {
                query = query.Where(x => x.Id > 0);
                return query;
            });
        }

        public async Task<IPagedList<Student>> GetAllStudentsPagedAsync()
        {
            return await _studentRepository.GetAllPagedAsync(query =>
            {
                query = query.Where(x => x.Id > 0);
                return query;
            });
        }

        public async Task<IPagedList<Student>> GetAllStudentsPagedAsync(string studentName, string studentMail, int pageIndex = 0, int pageSize = int.MaxValue/*, bool showHidden = false, bool? overridePublished = null*/)
        {
            var unsortedStudents = await _studentRepository.GetAllAsync(async query =>
            {
                //if (!showHidden)
                //    query = query.Where(c => c.Published);
                //else if (overridePublished.HasValue)
                //    query = query.Where(c => c.Published == overridePublished.Value);

                //if (!showHidden)
                //{
                //    //apply store mapping constraints
                //    query = await _storeMappingService.ApplyStoreMapping(query, storeId);

                //    //apply ACL constraints
                //    var customer = await _workContext.GetCurrentCustomerAsync();
                //    query = await _aclService.ApplyAcl(query, customer);
                //}
                
                if (!string.IsNullOrWhiteSpace(studentName))
                    query = query.Where(c => c.Name.Contains(studentName));

                if (!string.IsNullOrWhiteSpace(studentMail))
                    query = query.Where(c => c.Mail.Contains(studentMail));

                return query;

                //query = query.Where(c => !c.Deleted);

                //return query.OrderBy(c => c.ParentCategoryId).ThenBy(c => c.DisplayOrder).ThenBy(c => c.Id);
            });

            //sort categories
            //var sortedCategories = await SortCategoriesForTreeAsync(unsortedCategories);

            //paging
            return new PagedList<Student>(unsortedStudents, pageIndex, pageSize);
        }
    }
}
