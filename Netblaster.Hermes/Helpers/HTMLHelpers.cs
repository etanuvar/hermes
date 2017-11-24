using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Netblaster.Hermes.WebUI.Helpers
{
    public static class HtmlHelpers
    {
        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null)
        {
            string cssClass = "active";
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                cssClass : String.Empty;
        }

        public static string RenderRazorViewToString(this Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        public static string IsSelectedWithSubmenu(this HtmlHelper html, string controller = null, string action = null)
        {
            string cssClass = "activewithsubmenu";
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                cssClass : String.Empty;
        }

        public static string MapMimeTypeToIcon(this string mimeType)
        {
            switch (mimeType)
            {
                case "image/png":
                case "image/jpeg":
                    return "fa-file-image-o";
                case "text/plain":
                    return "fa-file-text-o";
                case "application/vnd.ms-excel":
                case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                    return "fa-file-excel-o";
                case "application/msword":
                case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                    return "fa-file-word-o";
                case "application/pdf":
                    return "fa-file-pdf-o";
                case "application/vnd.openxmlformats-officedocument.presentationml.presentation":
                case "application/vnd.ms-powerpoint":
                    return "fa-file-powerpoint-o";
                default:
                    return "fa-file-o";
            }
        }

        public static string PageClass(this HtmlHelper html)
        {
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }

        public static IHtmlString SortIdentifier(this HtmlHelper htmlHelper, string sortOrder, string field)
        {
            if (string.IsNullOrEmpty(sortOrder) || (sortOrder.Trim() != field && sortOrder.Replace("_desc", "").Trim() != field)) return null;
            string glyph;
            if (sortOrder.ToLower().Contains("desc"))
            {
                glyph = "pull-right fa fa-chevron-circle-down fa-grey-dark";
            }
            else
            {
                glyph = "pull-right fa fa-chevron-circle-up fa-grey-dark";
            }

            var span = new TagBuilder("span");
            span.Attributes["class"] = glyph;
            return MvcHtmlString.Create(span.ToString());
        }

        /// <summary>
        /// Sumbits the button.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="label">The label.</param>
        /// <returns>IHtmlString.</returns>
        public static IHtmlString SumbitButton(this HtmlHelper htmlHelper, string label = "Save Changes")
        {
            return MvcHtmlString.Create("<button type=\"submit\" class=\"btn btn-sm btn-primary margin-right-xs\"><i class=\"fa fa-plus-circle margin-right-xs\"></i>" + label + "</button>");
        }

        /// <summary>
        /// Sumbits the button.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="buttonId">The button identifier.</param>
        /// <param name="label">The label.</param>
        /// <returns>IHtmlString.</returns>
        public static IHtmlString SumbitButton(this HtmlHelper htmlHelper, string buttonId = "", string label = "Save Changes")
        {
            return MvcHtmlString.Create("<button id=\"" + buttonId + "\" type=\"submit\" class=\"btn btn-sm btn-primary margin-right-xs\"><i class=\"fa fa-plus-circle margin-right-xs\"></i>" + label + "</button>");
        }


        /// <summary>
        /// Buttons the specified button identifier.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="buttonId">The button identifier.</param>
        /// <param name="label">The label.</param>
        /// <returns>IHtmlString.</returns>
        public static IHtmlString Button(this HtmlHelper htmlHelper, string buttonId = "", string label = "Save Changes")
        {
            return MvcHtmlString.Create("<button id=\"" + buttonId + "\" class=\"btn btn-sm btn-primary margin-right-xs\"><i class=\"fa fa-plus-circle margin-right-xs\"></i>" + label + "</button>");
        }


        /// <summary>
        /// Creates the button.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="url">The URL.</param>
        /// <param name="label">The label.</param>
        /// <returns>IHtmlString.</returns>
        public static IHtmlString CreateButton(this HtmlHelper htmlHelper, string url, string label = "Create")
        {
            return MvcHtmlString.Create("<a href=\"" + url + "\" class=\"btn btn-sm btn-primary margin-right-xs\"><i class=\"fa fa-plus-circle margin-right-xs\"></i>" + label + "</a>");
        }

        /// <summary>
        /// Cancels the button.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="url">The URL.</param>
        /// <param name="label">The label.</param>
        /// <returns>IHtmlString.</returns>
        public static IHtmlString CancelButton(this HtmlHelper htmlHelper, string url, string label = "Back to list")
        {
            return MvcHtmlString.Create("<a href=\"" + url + "\" class=\"btn btn-sm btn-default margin-right-xs\"><i class=\"fa fa-ban margin-right-xs\"></i>" + label + "</a>");
        }

        /// <summary>
        /// Deletes the button.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="url">The URL.</param>
        /// <param name="objectId">The object identifier.</param>
        /// <param name="label">The label.</param>
        /// <returns>IHtmlString.</returns>
        public static IHtmlString DeleteButton(this HtmlHelper htmlHelper, string url, int? objectId, string label = "Delete", string cssClass = null)
        {
            return MvcHtmlString.Create("<a href=\"" + url + (objectId?.ToString() ?? "") + "\" class=\"btn btn-sm btn-default margin-right-xs " + (cssClass ?? "") + "\"><i class=\"fa fa-trash margin-right-xs\"></i>" + label + "</a>");
        }

        /// <summary>
        /// Deletes the button submit.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="label">The label.</param>
        /// <returns>IHtmlString.</returns>
        public static IHtmlString DeleteButtonSubmit(this HtmlHelper htmlHelper, string label = "Delete")
        {
            return MvcHtmlString.Create("<button type=\"submit\" class=\"btn btn-sm btn-orange\"><i class=\"fa fa-trash margin-right-xs\"></i>" + label + "</button>");
        }

        /// <summary>
        /// Edits the button.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="url">The URL.</param>
        /// <param name="objectId">The object identifier.</param>
        /// <param name="label">The label.</param>
        /// <returns>IHtmlString.</returns>
        public static IHtmlString EditButton(this HtmlHelper htmlHelper, string url, int? objectId, string label = "Edit", string attributes = "", string cssClass = null)
        {
            return MvcHtmlString.Create("<a href=\"" + url + (objectId?.ToString() ?? "") + (!string.IsNullOrEmpty(attributes) ? "?" + attributes : "") + "\" class=\"btn btn-sm btn-default margin-right-xs " + (cssClass ?? "") + "\"><i class=\"fa fa-edit margin-right-xs\"></i>" + label + "</a>");
        }




        /// <summary>
        /// Detailses the button.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="url">The URL.</param>
        /// <param name="objectId">The object identifier.</param>
        /// <param name="label">The label.</param>
        /// <returns>IHtmlString.</returns>
        public static IHtmlString DetailsButton(this HtmlHelper htmlHelper, string url, int objectId, string label = "Details")
        {
            return MvcHtmlString.Create("<a href=\"" + url + objectId + "\" class=\"btn btn-sm btn-default margin-right-xs\"><i class=\"fa fa-search margin-right-xs\"></i>" + label + "</a>");
        }



        /// <summary>
        /// Conditionals the disable.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="disabled">if set to <c>true</c> [disabled].</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>RouteValueDictionary.</returns>
        public static RouteValueDictionary ConditionalDisable(
            this HtmlHelper htmlHelper,
            bool disabled,
            object htmlAttributes = null)
        {
            var dictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            if (disabled)
                dictionary.Add("disabled", "disabled");

            return dictionary;
        }

    }
}