using System;
using System.Collections.Generic;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.StudentCRUD.Models
{
    public partial record StudentModel : BaseNopEntityModel, ILocalizedModel<StudentLocalizedModel>
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Age { get; set; }
        public string Mail { get; set; }
        public string Department { get; set; }
        public string Gender { get; set; }
        public string SeName { get; set; }
        public bool IsActive { get; set; }
        public IList<StudentLocalizedModel> Locales {get; set; }
    }

    public partial record StudentLocalizedModel : ILocalizedLocaleModel
    {
        public int LanguageId { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.StudentCRUD.Fields.Age")]
        public string Age { get; set; }
    }
}
