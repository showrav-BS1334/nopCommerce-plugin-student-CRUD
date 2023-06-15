using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DocumentFormat.OpenXml.EMMA;
using Nop.Core;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Vendors;
using Nop.Plugin.Widgets.StudentCRUD.Domain;
using Nop.Plugin.Widgets.StudentCRUD.Models;
using Nop.Plugin.Widgets.StudentCRUD.Service;
using Nop.Services.Catalog;
using Nop.Services.Localization;
using Nop.Services.Seo;
using Nop.Web.Areas.Admin.Models.Vendors;
using Nop.Web.Framework.Factories;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Plugin.Widgets.StudentCRUD.Factory
{
    public class StudentModelFactory : IStudentModelFactory
    {
        private readonly IStudentService _studentService;
        private readonly IUrlRecordService _urlRecordService;
        private IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedModelFactory _localizedModelFactory;

        public StudentModelFactory(
            IStudentService studentService,
            IUrlRecordService urlRecordService,
            IMapper mapper,
            ILocalizationService localizationService,
            ILocalizedModelFactory localizedModelFactory
            )
        {
            _studentService = studentService;
            _urlRecordService = urlRecordService;
            _mapper = mapper;
            _localizationService = localizationService;
            _localizedModelFactory = localizedModelFactory;
        }

        public async Task<IEnumerable<StudentModel>> PrepareStudentListModelAsync()
        {
            var students = await _studentService.GetAllStudentsAsync();
            var model = _mapper.Map<IEnumerable<StudentModel>>(students);
            return model;
        }

        public async Task<StudentModel> PrepareStudentModelAsync(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
                return new StudentModel();
            var model = _mapper.Map<StudentModel>(student);
            model.SeName = await _urlRecordService.GetActiveSlugAsync(student.Id, student.Name, 1);
            return model;
        }

        public async Task<StudentModel> PrepareStudentModelAsync(StudentModel model, Student student, bool excludeProperties = false)
        {
            Func<StudentLocalizedModel, int, Task> localizedModelConfiguration = null;

            if (student != null)
            {
                if(model == null)
                {
                    model = _mapper.Map<StudentModel>(student);
                    model.SeName = await _urlRecordService.GetActiveSlugAsync(student.Id, student.Name, 1);
                }

                localizedModelConfiguration = async (locale, languageId) =>
                {
                    locale.Name = await _localizationService.GetLocalizedAsync(student, entity => entity.Name, languageId, false, false);
                    //locale.Age = await _localizationService.GetLocalizedAsync(student, entity => entity.Age.ToString(), languageId, false, false);
                };
            }

            //prepare localized models
            if (!excludeProperties)
                model.Locales = await _localizedModelFactory.PrepareLocalizedModelsAsync(localizedModelConfiguration);

            return model;
        }

        public async Task<Student> PrepareStudentDomainAsync(StudentModel model)
        {
            if (model == null)
                return new Student();
            Student student = _mapper.Map<Student>(model);
            return student;
        }

        public async Task<IPagedList<Student>> PrepareStudentPagedListModelAsync()
        {
            var studentList = await _studentService.GetAllStudentsPagedAsync();

            return studentList;
        }

        public async Task<StudentListModel> PrepareStudentListModelAsync(StudentSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            //get categories
            var students = await _studentService.GetAllStudentsPagedAsync(
                studentName: searchModel.SearchStudentName,
                studentMail: searchModel.SearchStudentMail,
                //showHidden: true,
                pageIndex: searchModel.Page - 1,
                pageSize: searchModel.PageSize
                //overridePublished: searchModel.SearchPublishedId == 0 ? null : (bool?)(searchModel.SearchPublishedId == 1)
                );
            //var students = await _studentService.GetAllStudentsPagedAsync();

            //prepare grid model
            var model = await new StudentListModel().PrepareToGridAsync(searchModel, students, () =>
            {
                return students.SelectAwait(async student =>
                {
                    //fill in model values from the entity
                    //var studentModel = student.ToModel<StudentModel>();
                    var studentModel = _mapper.Map<StudentModel>(student);

                    //fill in additional values (not existing in the entity)
                    //studentModel.Breadcrumb = await _studentService.GetFormattedBreadCrumbAsync(student);
                    studentModel.SeName = await _urlRecordService.GetSeNameAsync(student, 1, true, false);

                    return studentModel;
                });
            });

            return model;
        }

        public async Task<StudentSearchModel> PrepareStudentSearchModelAsync(StudentSearchModel searchModel)
        {
            if (searchModel == null)
                return new StudentSearchModel();
            searchModel.SetGridPageSize();
            return searchModel;
        }
    }
}
