using System;
using FarsiLibrary.Utils;
using FarsiLibrary.WinFormDemo.Demo;

namespace FarsiLibrary.Test
{
    public partial class ToStringAndFormatters : DemoBase
    {
        #region Fields

        private PersianDate pd = PersianDate.Now;

        #endregion

        #region Ctor

        public ToStringAndFormatters()
        {
            InitializeComponent();
        }

        #endregion

        #region EventHandlers

        private void frm15_Load(object sender, EventArgs e)
        {
            toStringD.Text = pd.ToString("d");
            toStringDD.Text = pd.ToString("D");
            toStringF.Text = pd.ToString("f");
            toStringFF.Text = pd.ToString("F");
            toStringG.Text = pd.ToString("g");
            toStringGG.Text = pd.ToString("G");
            toStringM.Text = pd.ToString("m");
            toStringMM.Text = pd.ToString("M");
            toStringS.Text = pd.ToString("s");
            toStringT.Text = pd.ToString("t");
            toStringTT.Text = pd.ToString("T");
        }

        #endregion
    }
}