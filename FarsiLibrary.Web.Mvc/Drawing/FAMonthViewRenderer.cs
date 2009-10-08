using System.IO;
using System.Web.Routing;
using FarsiLibrary.Web.Mvc.Controls;
using FarsiLibrary.Utils.Internals;

namespace FarsiLibrary.Web.Mvc.Drawing
{
    public class FAMonthViewRenderer
    {
        private TextWriter tw;
        private FAMonthView control;

        public FAMonthViewRenderer(FAMonthView control, TextWriter writer)
        {
            this.tw = writer;
            this.control = control;
        }

//        public virtual void Render()
//        {
//            BeginTag("table", new
//            {
//                name=control.Name, 
//                width=control.Width, 
//                height=control.Height,
//                cellpadding=control.CellPadding,
//                cellspacing=control.CellSpacing,
//                @class="FAMonthViewTable"
//            });
//
//            RenderMonthHeader();
//            RenderWeekHeader();
//            RenderBody();
//            RenderFooter();
//
//            EndTag("table");
//        }

        private void RenderFooter()
        {
        }

        private void RenderBody()
        {
            
        }

//        private void RenderMonthHeader()
//        {
//            BeginTag("tr", new { @class = "FAMonthViewHeaderRow" });
//            BeginTag("td", new { colspan = "7" });
//
//            tw.Write(control.ViewDateTime);
//
//            EndTag("td");
//            EndTag("tr");
//        }

        private void RenderWeekHeader()
        {
        }

        #region Helper Methods

        private void BeginTag(string tag, object attributes)
        {
            tw.Write("<{0} ", tag);

            if (attributes != null)
            {
                var attribs = new RouteValueDictionary(attributes);
                attribs.ForEach(x => tw.Write("{0}='{1}' ", x.Key, x.Value));
            }

            tw.Write(">");
        }

        private void BeginTag(string tag)
        {
            tw.WriteLine("<{0}>", tag);
        }

        private void EndTag(string tag)
        {
            tw.WriteLine("</{0}>", tag);
        }

        #endregion
    }
}