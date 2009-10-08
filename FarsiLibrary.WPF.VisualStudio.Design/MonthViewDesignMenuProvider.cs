using System.Windows;
using Microsoft.Windows.Design.Interaction;

namespace FarsiLibrary.WPF.VisualStudio.Design
{
    public class MonthViewDesignMenuProvider : PrimarySelectionContextMenuProvider
    {
        private readonly MenuAction aboutMenuAction;

        public MonthViewDesignMenuProvider()
        {
            var grp = new MenuGroup("FarsiLibrary", "Farsi Library");
            aboutMenuAction = new MenuAction("About...");
            aboutMenuAction.Execute += OnAboutActionExecuted;

            grp.Items.Add(aboutMenuAction);
            Items.Add(grp);
        }

        private void OnAboutActionExecuted(object sender, MenuActionEventArgs e)
        {
            var dialog = new AboutUI();
            dialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            dialog.Topmost = true;
            dialog.ShowDialog();
        }
    }
}
