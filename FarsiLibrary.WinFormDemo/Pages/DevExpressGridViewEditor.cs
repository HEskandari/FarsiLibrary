using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FarsiLibrary.WinFormDemo.Demo;

namespace FarsiLibrary.WinFormDemo.Pages
{
    public partial class DevExpressGridViewEditor : DemoBase
    {
        public DevExpressGridViewEditor()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            LoadDataSet();
        }

        private void LoadDataSet()
        {
            StringReader sr = new StringReader(FarsiLibrary.WinFormDemo.Properties.Resources.Employee);
            personnelDataSet.ReadXml(sr, XmlReadMode.Auto);
        }
    }
}
