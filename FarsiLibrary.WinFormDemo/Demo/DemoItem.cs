using System.Windows.Forms;

namespace FarsiLibrary.WinFormDemo.Demo
{
    public class DemoItem
    {
        public DemoItem(IDemoPage demo)
        {
            Page = demo;
        }

        public IDemoPage Page { get; }

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