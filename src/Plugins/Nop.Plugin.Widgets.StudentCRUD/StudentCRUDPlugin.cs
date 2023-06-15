using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Core.Infrastructure;
using Nop.Plugin.Widgets.StudentCRUD.Components;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Plugins;
using Nop.Web.Framework;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Widgets.StudentCRUD
{
    /// <summary>
    /// PLugin
    /// </summary>
    public class StudentCRUDPlugin : BasePlugin, IWidgetPlugin, IAdminMenuPlugin
    {
        private readonly ILocalizationService _localizationService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;
        private readonly INopFileProvider _fileProvider;

        public StudentCRUDPlugin(ILocalizationService localizationService,
            IPictureService pictureService,
            ISettingService settingService,
            IWebHelper webHelper,
            INopFileProvider fileProvider)
        {
            _localizationService = localizationService;
            _pictureService = pictureService;
            _settingService = settingService;
            _webHelper = webHelper;
            _fileProvider = fileProvider;
        }

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the widget zones
        /// </returns>
        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HomepageTop });
        }

        /// <summary>
        /// Gets a name of a view component for displaying widget
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <returns>View component name</returns>
        public Type GetWidgetViewComponent(string widgetZone)
        {
            return typeof(WidgetsStudentCRUDViewComponent);
        }

        // admin panel e baam er dike ekta button er jnno
        public async Task ManageSiteMapAsync(SiteMapNode rootNode)
        {
            //var configurationItem = rootNode.ChildNodes.FirstOrDefault(node => node.SystemName.Equals("Configuration"));
            //if (configurationItem is null)
            //    return;

            //var widgetsItem = configurationItem.ChildNodes.FirstOrDefault(node => node.SystemName.Equals("Widgets"));
            //var index = configurationItem is not null ? configurationItem.ChildNodes.IndexOf(widgetsItem) : -1;

            var totalChilds = rootNode.ChildNodes.Count;
            rootNode.ChildNodes.Insert(totalChilds, new SiteMapNode
            {
                Visible = true,
                SystemName = "Widgets.StudentCRUD",
                Title = "Student CRUD",
                ControllerName = "Student",
                ActionName = "List",
                IconClass = "far fa-smile",
                RouteValues = new RouteValueDictionary { { "area", AreaNames.Admin } }
            });
        }

        public override async Task InstallAsync()
        {
            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Plugins.Widgets.StudentCRUD.ListPageTitle"] = "Student List Page",
                ["Plugins.Widgets.StudentCRUD.EditPageTitle"] = "Edit Student Info",

                ["Plugins.Widgets.StudentCRUD.Students"] = "Students",

                ["Plugins.Widgets.StudentCRUD.Fields.Name"] = "Name",
                ["Plugins.Widgets.StudentCRUD.Fields.Age"] = "Age",
                ["Plugins.Widgets.StudentCRUD.Fields.Mail"] = "Mail",
                ["Plugins.Widgets.StudentCRUD.Fields.Department"] = "Department",
                ["Plugins.Widgets.StudentCRUD.Fields.Gender"] = "Gender",

                ["Plugins.Widgets.StudentCRUD.Fields.SearchStudentName"] = "Name",
                ["Plugins.Widgets.StudentCRUD.Fields.SearchStudentMail"] = "Mail address",
            });
            await base.InstallAsync();
        }

        public override async Task UpdateAsync(string currentVersion, string targetVersion)
        {
            //await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            //{
            //    ["Plugins.Widgets.StudentCRUD.ListPageTitle"] = "Student List Page",
            //    ["Plugins.Widgets.StudentCRUD.EditPageTitle"] = "Edit Student Info",

            //    ["Plugins.Widgets.StudentCRUD.Students"] = "Students",

            //    ["Plugins.Widgets.StudentCRUD.Fields.Name"] = "Name",
            //    ["Plugins.Widgets.StudentCRUD.Fields.Age"] = "Age",
            //    ["Plugins.Widgets.StudentCRUD.Fields.Mail"] = "Mail",
            //    ["Plugins.Widgets.StudentCRUD.Fields.Department"] = "Department",
            //    ["Plugins.Widgets.StudentCRUD.Fields.Gender"] = "Gender",

            //    ["Plugins.Widgets.StudentCRUD.Fields.SearchStudentName"] = "Name",
            //    ["Plugins.Widgets.StudentCRUD.Fields.SearchStudentMail"] = "Mail address",
            //});
            //await base.UpdateAsync("1.51", "1.52");
        }

        public override async Task UninstallAsync()
        {
            await _localizationService.DeleteLocaleResourcesAsync("Plugins.Widgets.StudentCRUD");
            await base.UninstallAsync();
        }

        /// <summary>
        /// Gets a value indicating whether to hide this plugin on the widget list page in the admin area
        /// </summary>
        public bool HideInWidgetList => false;
    }
}