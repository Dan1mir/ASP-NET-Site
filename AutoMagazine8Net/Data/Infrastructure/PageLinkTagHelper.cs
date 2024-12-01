using Microsoft.AspNetCore.Mvc;
using AutoMagazine8Net.Models;

namespace AutoMagazine8Net.Data.Infrastucture
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory factory)
        {
            _urlHelperFactory = factory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public required ViewContext ViewContext { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

        public string? PageAction { get; set; }
        public PageInfo? PageModel { get; set; }
        public bool PageClassesEnabled { get; set; } = false;
        public string? PageClass { get; set; }
        public string? PageClassNormal { get; set; }
        public string? PageClassSelected { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");

            for (int i = 1; i <= PageModel.TotalPages; i++)
            {
                TagBuilder aTag = new TagBuilder("a");
                PageUrlValues["page"] = i;
                aTag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);

                if (PageClassesEnabled)
                {
                    aTag.AddCssClass(PageClass);
                    aTag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                }

                aTag.InnerHtml.Append(i.ToString());
                result.InnerHtml.AppendHtml(aTag);
            }

            output.Content.AppendHtml(result.InnerHtml);
        }
    }

}
