using System.ComponentModel;
using System.Windows.Forms;

namespace FarsiLibrary.WinFormDemo.Demo
{
    public partial class DemoBase : UserControl, IDemoPage
    {
        public DemoBase()
        {
            InitializeComponent();
        }

        [DefaultValue("")]
        public string Title
        {
            get;
            set;
        }

        [DefaultValue(true)]
        public bool IsNew
        {
            get; 
            set;
        }

        public bool ShouldSerializeIsNew()
        {
            return IsNew;
        }

        public bool ShouldSerializeTitle()
        {
            return !string.IsNullOrEmpty(Title);
        }
    }
}
