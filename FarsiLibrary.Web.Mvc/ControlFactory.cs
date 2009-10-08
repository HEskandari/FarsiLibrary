using System;
using System.Web.Mvc;
using FarsiLibrary.Utils.Internals;
using FarsiLibrary.Web.Mvc.Builder;
using FarsiLibrary.Web.Mvc.Controls;

namespace FarsiLibrary.Web.Mvc
{
    public class ControlFactory : IHideMembers
    {
        private readonly HtmlHelper htmlHelper;

        public ControlFactory(HtmlHelper htmlHelper)
        {
            Guard.Against(htmlHelper == null, "htmlHelper should not be null");

            this.htmlHelper = htmlHelper;
        }

        private ViewContext ViewContext
        {
            get
            {
                return htmlHelper.ViewContext;
            }
        }

        public virtual FAMonthViewBuilder MonthView()
        {
            return new FAMonthViewBuilder(Create(() => new FAMonthView(ViewContext)));
        }

        private TControl Create<TControl>(Func<TControl> factory) where TControl : WebControl
        {
            TControl control = factory();

            return control;
        }
    }
}