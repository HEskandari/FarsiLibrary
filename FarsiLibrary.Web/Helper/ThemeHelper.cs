using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using FarsiLibrary.Web.Controls;

namespace FarsiLibrary.Web.Helper
{
    public class ThemeHelper
    {
        private ThemeTypes currentTheme;

        public ThemeHelper(ThemeTypes theme)
        {
            currentTheme = theme;
        }

        public Button CreateFooterButton()
        {
            Button button = new Button();

            button.Attributes.CssStyle.Add(HtmlTextWriterStyle.FontSize, "9pt");
            button.Attributes.CssStyle.Add(HtmlTextWriterStyle.Cursor, "pointer");
            button.Attributes.CssStyle.Add(HtmlTextWriterStyle.BackgroundColor, "transparent");
            button.Attributes.CssStyle.Add(HtmlTextWriterStyle.TextDecoration, "underline");
            button.Attributes.CssStyle.Add(HtmlTextWriterStyle.BorderStyle, "none");

            return button;
        }

        public DropDownList CreateFooterDropDownList()
        {
            DropDownList list = new DropDownList();
            list.Attributes.CssStyle.Add(HtmlTextWriterStyle.FontSize, "8pt");
            
            switch (this.currentTheme)
            {
                case ThemeTypes.Office2000:
                    list.Attributes.CssStyle.Add(HtmlTextWriterStyle.BackgroundColor, "#FAFAFA");
                    return list;

                case ThemeTypes.Office2003:
                    list.Attributes.CssStyle.Add(HtmlTextWriterStyle.BackgroundColor, "#ecf3fe");
                    return list;

                case ThemeTypes.Office2007:
                case ThemeTypes.WindowsXP:
                    list.Attributes.CssStyle.Add(HtmlTextWriterStyle.BackgroundColor, "#ecfbec");
                    return list;
            }

            return new DropDownList();
        }

        public Table CreateFooterTable(Page page)
        {
            Table table = new Table();
            table.Attributes.CssStyle.Add(HtmlTextWriterStyle.FontFamily, "Arial");
            table.Attributes.CssStyle.Add("background-position", "bottom");
            table.Attributes.CssStyle.Add("background-repeat", "repeat-x");
            switch (this.currentTheme)
            {
                case ThemeTypes.Office2000:
                    table.Attributes.CssStyle.Add(HtmlTextWriterStyle.BackgroundImage, page.ClientScript.GetWebResourceUrl(typeof(FADatePicker), "FarsiLibrary.Web.Images.SilverGradient.gif"));
                    return table;

                case ThemeTypes.Office2003:
                    table.Attributes.CssStyle.Add(HtmlTextWriterStyle.BackgroundImage, page.ClientScript.GetWebResourceUrl(typeof(FADatePicker), "FarsiLibrary.Web.Images.BlueGradient.gif"));
                    return table;

                case ThemeTypes.Office2007:
                case ThemeTypes.WindowsXP:
                    table.Attributes.CssStyle.Add(HtmlTextWriterStyle.BackgroundImage, page.ClientScript.GetWebResourceUrl(typeof(FADatePicker), "FarsiLibrary.Web.Images.GreenGradient.gif"));
                    return table;
            }
            return new Table();
        }

        public Table CreateTitleTable(Page page)
        {
            Table table = new Table();
            table.Attributes.CssStyle.Add(HtmlTextWriterStyle.FontFamily, "Arial");
            table.Attributes.CssStyle.Add("background-position", "top");
            table.Attributes.CssStyle.Add("background-repeat", "repeat-x");
            switch (this.currentTheme)
            {
                case ThemeTypes.Office2000:
                    table.Attributes.CssStyle.Add(HtmlTextWriterStyle.BackgroundImage, page.ClientScript.GetWebResourceUrl(typeof(FADatePicker), "FarsiLibrary.Web.Images.SilverGradient.gif"));
                    return table;

                case ThemeTypes.Office2003:
                    table.Attributes.CssStyle.Add(HtmlTextWriterStyle.BackgroundImage, page.ClientScript.GetWebResourceUrl(typeof(FADatePicker), "FarsiLibrary.Web.Images.BlueGradient.gif"));
                    return table;

                case ThemeTypes.Office2007:
                case ThemeTypes.WindowsXP:
                    table.Attributes.CssStyle.Add(HtmlTextWriterStyle.BackgroundImage, page.ClientScript.GetWebResourceUrl(typeof(FADatePicker), "FarsiLibrary.Web.Images.GreenGradient.gif"));
                    return table;
            }
            return new Table();
        }

        public string GetNextImage(Page page)
        {
            string str = string.Empty;
            switch (this.currentTheme)
            {
                case ThemeTypes.Office2000:
                    return string.Format("<img src='{0}' />", page.ClientScript.GetWebResourceUrl(typeof(FADatePicker), "FarsiLibrary.Web.Images.SilverNext.gif"));

                case ThemeTypes.Office2003:
                    return string.Format("<img src='{0}' />", page.ClientScript.GetWebResourceUrl(typeof(FADatePicker), "FarsiLibrary.Web.Images.BlueNext.gif"));

                case ThemeTypes.Office2007:
                case ThemeTypes.WindowsXP:
                    return string.Format("<img src='{0}' />", page.ClientScript.GetWebResourceUrl(typeof(FADatePicker), "FarsiLibrary.Web.Images.GreenNext.gif"));
            }
            return str;
        }

        public string GetPrevImage(Page page)
        {
            string str = string.Empty;
            switch (this.currentTheme)
            {
                case ThemeTypes.Office2000:
                    return string.Format("<img src='{0}' />", page.ClientScript.GetWebResourceUrl(typeof(FADatePicker), "FarsiLibrary.Web.Images.SilverPrev.gif"));

                case ThemeTypes.Office2003:
                    return string.Format("<img src='{0}' />", page.ClientScript.GetWebResourceUrl(typeof(FADatePicker), "FarsiLibrary.Web.Images.BluePrev.gif"));

                case ThemeTypes.Office2007:
                case ThemeTypes.WindowsXP:
                    return string.Format("<img src='{0}' />", page.ClientScript.GetWebResourceUrl(typeof(FADatePicker), "FarsiLibrary.Web.Images.GreenPrev.gif"));
            }
            return str;
        }

        public Style DayStyle
        {
            get
            {
                Style style = new Style();
                switch (this.currentTheme)
                {
                    case ThemeTypes.Office2000:
                        style.BackColor = Color.FromArgb(250, 250, 250);
                        return style;

                    case ThemeTypes.Office2003:
                        style.BackColor = Color.FromArgb(236, 243, 254);
                        return style;

                    case ThemeTypes.Office2007:
                    case ThemeTypes.WindowsXP:
                        style.BackColor = Color.FromArgb(236, 251, 236);
                        return style;
                }
                return new Style();
            }
        }

        public Style SelectedDayStyle
        {
            get
            {
                Style style = new Style();
                switch (this.currentTheme)
                {
                    case ThemeTypes.Office2000:
                        style.BackColor = Color.FromArgb(100, 100, 100);
                        return style;

                    case ThemeTypes.Office2003:
                        style.BackColor = Color.FromArgb(153, 190, 251);
                        return style;

                    case ThemeTypes.Office2007:
                    case ThemeTypes.WindowsXP:
                        style.BackColor = Color.FromArgb(154, 219, 134);
                        return style;
                }
                return new Style();
            }
        }

        public Style TodayDayStyle
        {
            get
            {
                Style style = new Style
                {
                    Font = { Bold = true }
                };

                switch (this.currentTheme)
                {
                    case ThemeTypes.Office2000:
                        style.BackColor = Color.FromArgb(250, 250, 250);
                        return style;

                    case ThemeTypes.Office2003:
                        style.BackColor = Color.FromArgb(236, 243, 254);
                        return style;

                    case ThemeTypes.Office2007:
                    case ThemeTypes.WindowsXP:
                        style.BackColor = Color.FromArgb(236, 251, 236);
                        return style;
                }
                return new Style();
            }
        }
    }
}
