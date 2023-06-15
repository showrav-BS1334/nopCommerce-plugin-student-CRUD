using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Vendors;
using Nop.Plugin.Widgets.StudentCRUD.Domain;
using Nop.Plugin.Widgets.StudentCRUD.Factory;
using Nop.Plugin.Widgets.StudentCRUD.Models;
using Nop.Plugin.Widgets.StudentCRUD.Service;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.StudentCRUD.Controller
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class StudentController : BasePluginController
    {
        private readonly IStudentService _studentService;
        private readonly IStudentModelFactory _studentModelFactory;
        private readonly IPermissionService _permissionService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly ILocalizedEntityService _localizedEntityService;

        public StudentController(
            IStudentService studentService, 
            IStudentModelFactory studentFactory, 
            IPermissionService permissionService, 
            IUrlRecordService urlRecordService,
            ILocalizedEntityService localizedEntityService
            )
        {
            _studentService = studentService;
            _studentModelFactory = studentFactory;
            _permissionService = permissionService;
            _urlRecordService = urlRecordService;
            _localizedEntityService = localizedEntityService;
        }
        public async Task<IActionResult> List()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManagePlugins))
                return AccessDeniedView();

            //var model = await _studentModelFactory.PrepareStudentPagedListModelAsync();
            var model = await _studentModelFactory.PrepareStudentSearchModelAsync(new StudentSearchModel());
            return View("~/Plugins/Widgets.StudentCRUD/Views/Student/List.cshtml", model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> List(StudentSearchModel searchModel)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageCategories))
                return await AccessDeniedDataTablesJson();

            //prepare model
            var model = await _studentModelFactory.PrepareStudentListModelAsync(searchModel);

            return Json(model);
        }



        protected virtual async Task UpdateLocalesAsync(Student student, StudentModel model)
        {
            foreach (var localized in model.Locales)
            {
                await _localizedEntityService.SaveLocalizedValueAsync(student,
                    x => x.Name,
                    localized.Name,
                    localized.LanguageId);

                //await _localizedEntityService.SaveLocalizedValueAsync(student,
                //    x => x.Age,
                //    localized.Age,
                //    localized.LanguageId);

                //search engine name
                //var seName = await _urlRecordService.ValidateSeNameAsync(vendor, localized.SeName, localized.Name, false);
                //await _urlRecordService.SaveSlugAsync(vendor, seName, localized.LanguageId);
            }
        }

        
        public async Task<IActionResult> Create()
        {
            var model = await _studentModelFactory.PrepareStudentModelAsync(new StudentModel(), null);
            return View("~/Plugins/Widgets.StudentCRUD/Views/Student/Create.cshtml", model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(StudentModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageCategories))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                //var category = model.ToEntity<Category>();
                //category.CreatedOnUtc = DateTime.UtcNow;
                //category.UpdatedOnUtc = DateTime.UtcNow;
                //await _categoryService.InsertCategoryAsync(category);

                //search engine name
                var studentDomain = await _studentModelFactory.PrepareStudentDomainAsync(model);


                await _studentService.InsertStudentAsync(studentDomain);

                model.SeName = await _urlRecordService.ValidateSeNameAsync(studentDomain, model.Name, "student", true);
                await _urlRecordService.SaveSlugAsync(studentDomain, model.SeName, 0);


                await UpdateLocalesAsync(studentDomain, model);

                ////locales
                //await UpdateLocalesAsync(category, model);

                ////discounts
                //var allDiscounts = await _discountService.GetAllDiscountsAsync(DiscountType.AssignedToCategories, showHidden: true, isActive: null);
                //foreach (var discount in allDiscounts)
                //{
                //    if (model.SelectedDiscountIds != null && model.SelectedDiscountIds.Contains(discount.Id))
                //        await _categoryService.InsertDiscountCategoryMappingAsync(new DiscountCategoryMapping { DiscountId = discount.Id, EntityId = category.Id });
                //}

                //await _categoryService.UpdateCategoryAsync(category);

                ////update picture seo file name
                //await UpdatePictureSeoNamesAsync(category);

                ////ACL (customer roles)
                //await SaveCategoryAclAsync(category, model);

                ////stores
                //await SaveStoreMappingsAsync(category, model);

                ////activity log
                //await _customerActivityService.InsertActivityAsync("AddNewCategory",
                //    string.Format(await _localizationService.GetResourceAsync("ActivityLog.AddNewCategory"), category.Name), category);

                //_notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Catalog.Categories.Added"));

                //if (!continueEditing)
                //    return RedirectToAction("List");
                //return RedirectToAction("Edit", new { id = category.Id });



                ViewBag.RefreshPage = true;

                return View("~/Plugins/Widgets.StudentCRUD/Views/Student/Create.cshtml", model);
            }

            //prepare model
            //model = await _categoryModelFactory.PrepareCategoryModelAsync(model, null, true);

            //ViewBag.RefreshPage = true;

            //return View("~/Plugins/Widgets.StudentCRUD/Views/Student/Create.cshtml", model);

            //if we got this far, something failed, redisplay form
            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            //if(id == null) return
            var student = await _studentService.GetStudentByIdAsync(id);
            var model = await _studentModelFactory.PrepareStudentModelAsync(null, student);
            return View("~/Plugins/Widgets.StudentCRUD/Views/Student/Edit.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StudentModel model)
        {
            if (ModelState.IsValid || model != null)
            {
                var student = await _studentModelFactory.PrepareStudentDomainAsync(model);
                await _studentService.UpdateStudentAsync(student);
                return RedirectToAction("List", "Student");
            }
            return RedirectToAction("List", "Student");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid || id > 0)
            {
                var student = await _studentService.GetStudentByIdAsync(id);
                await _studentService.DeleteStudentAsync(student);
                return RedirectToAction("List", "Student");
            }
            return RedirectToAction("List", "Student");
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _studentModelFactory.PrepareStudentModelAsync(id);
            model.SeName = model.Name;
            return View("~/Plugins/Widgets.StudentCRUD/Views/Student/Details.cshtml", model);
        }
    }
}