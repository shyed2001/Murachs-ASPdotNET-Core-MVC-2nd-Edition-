using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;    // [ViewContext] attribute
using Microsoft.AspNetCore.Mvc.Rendering;       // ViewContext data type

namespace ClassSchedule.TagHelpers
{
    public class MyLinkButtonTagHelper : TagHelper
    {
        private LinkGenerator linkBuilder;
        public MyLinkButtonTagHelper(LinkGenerator lg) => linkBuilder = lg;

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewCtx { get; set; } = null!;

        public string Action { get; set; } = string.Empty;
        public string Controller { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // build the url
            string action = Action ?? ViewCtx.RouteData.Values["action"]?.ToString() ?? "";
            string controller = Controller ?? ViewCtx.RouteData.Values["controller"]?.ToString() ?? "";
            var id = new { Id };
            string url = linkBuilder.GetPathByAction(action, controller, id) ?? "";

            // set the Bootstrap class based on whether Id matches current id route segment
            string current = ViewCtx.RouteData.Values["id"]?.ToString() ?? "";
            string css = (current == Id) ? "btn btn-dark" : "btn btn-outline-dark";

            output.BuildLink(url, css);
        }
    }
}
