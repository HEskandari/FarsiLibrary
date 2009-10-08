using System;
using System.Windows.Forms;
using FarsiLibrary.Win;
using FarsiLibrary.Win.Enums;
using FarsiLibrary.WinFormDemo.Demo;

namespace FarsiLibrary.WinFormDemo.Pages
{
    public partial class ToolStripIntegration : DemoBase
    {
        #region Ctor

        public ToolStripIntegration()
        {
            InitializeComponent();
        }

        #endregion

        #region EventHandlers

        private void windowsXPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripManager.VisualStylesEnabled = true;
            ToolStripManager.RenderMode = ToolStripManagerRenderMode.System;
            FAThemeManager.Theme = ThemeTypes.WindowsXP;
        }

        private void office2003ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripManager.VisualStylesEnabled = true;
            ToolStripManager.RenderMode = ToolStripManagerRenderMode.Professional;
            FAThemeManager.Theme = ThemeTypes.Office2003;
        }

        private void office2000ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripManager.VisualStylesEnabled = false;
            FAThemeManager.Theme = ThemeTypes.Office2000;
        }

        #endregion
    }
}