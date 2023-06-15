using System;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Nop.Core.Domain.Seo;
using Nop.Plugin.Widgets.StudentCRUD.Domain;
using Nop.Services.Events;
using Nop.Web.Framework.Events;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Widgets.StudentCRUD.Infrastructure
{
    public class GenericUrlRouteEventConsumer : IConsumer<GenericRoutingEvent>
    {
        public async Task HandleEventAsync(GenericRoutingEvent eventMessage)
        {
            if (eventMessage.UrlRecord.EntityName.Equals(nameof(Student), StringComparison.InvariantCultureIgnoreCase))
            {
                RouteToAction(eventMessage.RouteValues, "Student", "Details", eventMessage.UrlRecord.Slug, ("studentId", eventMessage.UrlRecord.EntityId));
            }
            return;
        }
        protected void RouteToAction(RouteValueDictionary values, string controller, string action, string slug, params (string Key, object Value)[] parameters)
        {
            values["controller"] = controller;
            values["action"] = action;
            values["area"] = "admin";
            foreach (var (key, value) in parameters)
                values[key] = value;
        }
    }
}



//namespace Nop.Plugin.Widgets.StudentCRUD.Infrastructure
//{
//    public class RouteProvider : IConsumer<GenericRoutingEvent>
//    {
//        public async Task HandleEventAsync(GenericRoutingEvent eventMessage)
//        {
//            if (eventMessage.UrlRecord.EntityName.Equals(nameof(Student), StringComparison.InvariantCultureIgnoreCase))
//            {
//                //RouteToAction(eventMessage.RouteValues, "Student", "Details", eventMessage.UrlRecord.Slug, (NopRoutingDefaults.RouteValue.SeName, eventMessage.UrlRecord.EntityId));
//                eventMessage.RouteValues["controller"] = "Student";
//                eventMessage.RouteValues["action"] = "Details";
//                eventMessage.RouteValues[NopRoutingDefaults.RouteValue.SeName] = eventMessage.UrlRecord.Slug;
//            }
//            return;
//        }
//    }
//}
