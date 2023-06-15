using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.StudentCRUD.Factory;
//using Nop.Plugin.Widgets.NivoSlider.Infrastructure.Cache;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.StudentCRUD.Components
{
    public class WidgetsStudentCRUDViewComponent : NopViewComponent
    {
        private readonly IStoreContext _storeContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly ISettingService _settingService;
        private readonly IPictureService _pictureService;
        private readonly IWebHelper _webHelper;
        private readonly IStudentModelFactory _studentModelFactory;
        public WidgetsStudentCRUDViewComponent(IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,
            ISettingService settingService,
            IPictureService pictureService,
            IWebHelper webHelper,
            IStudentModelFactory studentFactory)
        {
            _storeContext = storeContext;
            _staticCacheManager = staticCacheManager;
            _settingService = settingService;
            _pictureService = pictureService;
            _webHelper = webHelper;
            _studentModelFactory = studentFactory;
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            var model = await _studentModelFactory.PrepareStudentListModelAsync();
            return View("~/Plugins/Widgets.StudentCRUD/Views/PublicInfo.cshtml", model);
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        //protected async Task<string> GetPictureUrlAsync(int pictureId)
        //{
        //    var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(ModelCacheEventConsumer.PICTURE_URL_MODEL_KEY, 
        //        pictureId, _webHelper.IsCurrentConnectionSecured() ? Uri.UriSchemeHttps : Uri.UriSchemeHttp);

        //    return await _staticCacheManager.GetAsync(cacheKey, async () =>
        //    {
        //        //little hack here. nulls aren't cacheable so set it to ""
        //        var url = await _pictureService.GetPictureUrlAsync(pictureId, showDefaultPicture: false) ?? "";
        //        return url;
        //    });
        //}
    }
}
