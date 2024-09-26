using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;    // [ViewContext] attribute
using Microsoft.AspNetCore.Mvc.Rendering;       // ViewContext data type
using Bookstore.Models;

namespace Bookstore.TagHelpers
{
    [HtmlTargetElement("my-sorting-link")]
    public class SortingLinkTagHelper : TagHelper
    {
        private LinkGenerator linkBuilder;
        public SortingLinkTagHelper(LinkGenerator lg) => linkBuilder = lg;

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewCtx { get; set; } = null!;

        public GridData Current { get; set; } = null!;
        public string SortField { get; set; } = string.Empty;

        public override void Process(TagHelperContext context,
        TagHelperOutput output)
        {
            var routes = Current.Clone();
            routes.SetSortAndDirection(SortField, Current);

            string ctlr = ViewCtx.RouteData.Values["controller"]?.ToString() ?? "";
            string action = ViewCtx.RouteData.Values["action"]?.ToString() ?? "";
            string url = linkBuilder.GetPathByAction(
                action, ctlr, routes.ToDictionary()) ?? "";

            output.BuildLink(url, "text-white");
        }
    }
}
