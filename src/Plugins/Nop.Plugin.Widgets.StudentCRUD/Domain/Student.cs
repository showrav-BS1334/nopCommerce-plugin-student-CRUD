using Nop.Core;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Seo;

namespace Nop.Plugin.Widgets.StudentCRUD.Domain
{
    public class Student : BaseEntity, ISlugSupported, ILocalizedEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Mail { get; set; }
        public string Department { get; set; }
        public string Gender { get; set; }
        public bool IsActive { get; set; }
    }
}
