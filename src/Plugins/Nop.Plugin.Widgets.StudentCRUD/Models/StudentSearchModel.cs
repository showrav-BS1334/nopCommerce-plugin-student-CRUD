using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.StudentCRUD.Models
{
    public partial record StudentSearchModel : BaseSearchModel
    {
        //public StudentSearchModel()
        //{
        //    //AvailableStores = new List<SelectListItem>();
        //    //AvailablePublishedOptions = new List<SelectListItem>();
        //}

        //[NopResourceDisplayName("Admin.Catalog.Categories.List.SearchCategoryName")]
        [NopResourceDisplayName("Plugins.Widgets.StudentCRUD.Fields.SearchStudentName")]
        public string SearchStudentName { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.StudentCRUD.Fields.SearchStudentMail")]
        public string SearchStudentMail { get; set; }
    }
}
