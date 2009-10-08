using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using FarsiLibrary.Resources;
using FarsiLibrary.Win.Design;
using FarsiLibrary.WinFormDemo.Demo;
using FarsiLibrary.WinFormDemo.Pages;

namespace FarsiLibrary.WinFormDemo
{
    public partial class MainWinForm : Form
    {
        #region Ctor & Initialization

        public MainWinForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if(!DesignMode)
            {
                StartPosition = FormStartPosition.CenterScreen;
                FindAllDemos();
            }
        }

        private void FindAllDemos()
        {
            var demos = (from type in this.GetType().Assembly.GetExportedTypes()
                         where typeof(IDemoPage).IsAssignableFrom(type) && 
                               type.IsAbstract == false &&
                               type != typeof(DemoBase)
                         select type).ToList();

            foreach (var demo in demos)
            {
                var demoForm = Activator.CreateInstance(demo) as IDemoPage;
                RegisterDemos(demoForm);
            }
        }

        private void RegisterDemos(IDemoPage demo)
        {
            if(demo.Title == null)
                throw new ApplicationException(string.Format("page has no title : {0}", demo.GetType()));

            var item = new DemoItem() {Page = demo};
            listBoxDemos.Items.Add(item);
        }

        #endregion

        #region Show Pages

        public void ShowPage(DemoItem item)
        {
            item.Control.Dock = DockStyle.Fill;

            splitContainerView.Panel2.Controls.Clear();
            splitContainerView.Panel2.Controls.Add(item.Control);
        }

        #endregion

        #region EventHandlers

        private void btnAbout_Click(object sender, EventArgs e)
        {
        }

        private void btnBindingBusinessObj_Click(object sender, EventArgs e)
        {
            CultureInfo oldValue = Thread.CurrentThread.CurrentUICulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fa-IR");
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            
            BindingToList form = new BindingToList();
            //form.ShowDialog(this);

            Thread.CurrentThread.CurrentCulture = oldValue;
            Thread.CurrentThread.CurrentUICulture = oldValue;
        }

        private void btnValidation_Click(object sender, EventArgs e)
        {
            CultureInfo oldValue = Thread.CurrentThread.CurrentUICulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fa-IR");
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            DateValidation form = new DateValidation();
            //form.ShowDialog(this);

            Thread.CurrentThread.CurrentCulture = oldValue;
            Thread.CurrentThread.CurrentUICulture = oldValue;
        }

        private void btnGridView_Click(object sender, EventArgs e)
        {
            FALocalizeManager.Instance.CustomCulture = FALocalizeManager.Instance.FarsiCulture;
            
            GridViewColumnEditor form = new GridViewColumnEditor();
            //form.ShowDialog(this);

            FALocalizeManager.Instance.CustomCulture = null;
        }

        #endregion

        #region Toolbar Commands

        private void toolStripShow_Click(object sender, EventArgs e)
        {
            if (listBoxDemos.SelectedItem != null)
            {
                var demo = listBoxDemos.SelectedItem as DemoItem;

                ShowPage(demo);
            }
        }

        private void toolStripButtonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButtonAbout_Click(object sender, EventArgs e)
        {
            var form = new About(false);
            form.ShowDialog(this);
        }

        #endregion

        #region Draw Demo Item

        private void listBoxDemos_DrawItem(object sender, DrawItemEventArgs e)
        {
            var g = e.Graphics;
            var r = e.Bounds;
            var item = listBoxDemos.Items[e.Index] as DemoItem;

            if(item == null)
                return;

            e.DrawBackground();
            
            if(item.Page.IsNew)
            {
                g.DrawImage(Properties.Resources.NewIcon, new Rectangle(r.X, r.Y, 16, 16));
                //r.Width -= 16;
                r.X += 16;
            }

            using(var brush = new SolidBrush(e.ForeColor))
            {
                g.DrawString(item.Page.Title, e.Font, brush, r);
            }
        }

        #endregion
    }
}