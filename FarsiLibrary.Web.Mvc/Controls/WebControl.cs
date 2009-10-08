using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.Routing;
using FarsiLibrary.Utils.Internals;

namespace FarsiLibrary.Web.Mvc.Controls
{
    public class WebControl
    {
        private string name;

        public WebControl(ViewContext viewContext)
        {
            ViewContext = viewContext;
            HtmlAttributes = new RouteValueDictionary();
        }

        /// <summary>
        /// Gets the HTML attributes.
        /// </summary>
        /// <value>The HTML attributes.</value>
        public IDictionary<string, object> HtmlAttributes
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the view context to rendering a view.
        /// </summary>
        /// <value>The view context.</value>
        protected ViewContext ViewContext
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the text writer that is used to render the view.
        /// </summary>
        protected TextWriter Writer
        {
            get { return ViewContext.HttpContext.Response.Output; }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return name; }
            set
            {
                Guard.Against(string.IsNullOrEmpty(value), "value can not be null or empty.");
                name = value;
            }
        }

        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>The id.</value>
        public string Id
        {
            get
            {
                return HtmlAttributes.ContainsKey("id") ?
                       HtmlAttributes["id"].ToString() :
                       !string.IsNullOrEmpty(Name) ? Name.Replace(".", HtmlHelper.IdAttributeDotReplacement) : null;
            }
        }

        /// <summary>
        /// Renders the component.
        /// </summary>
        public void Render()
        {
            EnsureRequired();
            WriteHtml();
        }

        /// <summary>
        /// Ensures the required settings.
        /// </summary>
        protected virtual void EnsureRequired()
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new InvalidOperationException("requried property is not set.");
            }
        }

        /// <summary>
        /// Writes the HTML.
        /// </summary>
        protected virtual void WriteHtml()
        {
        }
    }
}