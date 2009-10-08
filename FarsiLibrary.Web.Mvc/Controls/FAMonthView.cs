using System;
using System.Web.Mvc;
using FarsiLibrary.Utils.Internals;

namespace FarsiLibrary.Web.Mvc.Controls
{
    public class FAMonthView : WebControl
    {
        public FAMonthView(ViewContext context) : base(context)
        {
        }

        public DateTime ViewDateTime
        {
            get; 
            set;
        }

        public DateTime? SelectedDateTime
        {
            get; set;
        }

        public string SelectionEmpty
        {
            get; set;
        }

        protected override void WriteHtml()
        {
            var tagBuilder = new TagBuilder("input");

            tagBuilder.MergeAttributes(HtmlAttributes);
            tagBuilder.MergeAttribute("type", HtmlHelper.GetInputTypeString(InputType.Text));
            tagBuilder.MergeAttribute("name", Name, true);
            tagBuilder.MergeAttribute("value", GetSelectedValue());

            tagBuilder.GenerateId(Name);

            Writer.Write(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }

        private string GetSelectedValue()
        {
            if(SelectedDateTime.HasValue)
            {
                return SelectedDateTime.Value.ToString(CultureHelper.CurrentCulture);
            }
            else
            {
                return SelectionEmpty;
            }
        }
    }
}
