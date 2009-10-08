using System.Windows.Forms;

namespace FarsiLibrary.WinFormDemo.Demo
{
    public class DemoItem
    {
        public IDemoPage Page { get; set; }

        public Control Control
        {
            get { return Page as UserControl; }
        }

        public override string ToString()
        {
            return Page.Title;
        }
    }
}