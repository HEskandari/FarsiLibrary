using System;

namespace FarsiLibrary.WebDemo
{
    public partial class Index : System.Web.UI.Page
    {
        protected void btnCultures_Clicked(object sender, EventArgs e)
        {
            Server.Transfer("Cultures.aspx");
        }

        protected void btnStyles_Clicked(object sender, EventArgs e)
        {
            Server.Transfer("Styles.aspx");
        }

        protected void btnCustomRendering_Clicked(object sender, EventArgs e)
        {
            Server.Transfer("CustomRendering.aspx");
        }
    }
}
