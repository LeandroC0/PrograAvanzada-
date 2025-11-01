using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Usuario.Helpers
{
    public static class CustomHtmlHelpers
    {
    public static MvcHtmlString CustomTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
        Expression<Func<TModel, TValue>> expression,
        object htmlAttributes)
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            if (attributes.ContainsKey("class"))
            {
                attributes["class"] += " form-control";
            }
            else
            {
                attributes.Add("class", "form-control");
            }

            return htmlHelper.TextBoxFor(expression, attributes);
        }
    }
}