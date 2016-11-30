using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FarsiLibrary.Win.Design;
using FarsiLibrary.WinFormDemo.Demo;

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
                         select type)
                         .ToList();

            var pages = new List<IDemoPage>();

            foreach (var demo in demos)
            {
                var demoForm = Activator.CreateInstance(demo) as IDemoPage;
                pages.Add(demoForm);
            }

            RegisterDemos(pages);
        }

        private void RegisterDemos(IList<IDemoPage> demos)
        {
            var ordered = demos.OrderByDescending(x => x.IsNew).ThenBy(x => x.Title).ToList();

            foreach (var page in ordered)
            {
                var item = new DemoItem(page);
                listBoxDemos.Items.Add(item);
            }
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
            var item = listBoxDemos.Items[e.Index] as DemoItem;
            if(item == null)
                return;

            e.DrawBackground();
            DrawNewImage(e, item);
            DrawString(e, item);
        }

        private static void DrawString(DrawItemEventArgs e, DemoItem item)
        {
            var g = e.Graphics;
            var r = e.Bounds;

            using (var brush = new SolidBrush(e.ForeColor))
            {
                var rectangle = item.Page.IsNew ? new Rectangle(r.X + 16, r.Y, r.Width, r.Height) : r; 
                g.DrawString(item.Page.Title, e.Font, brush, rectangle);
            }
        }

        private static void DrawNewImage(DrawItemEventArgs e, DemoItem item)
        {
            var g = e.Graphics;
            var r = e.Bounds;

            if (item.Page.IsNew)
            {
                g.DrawImage(Properties.Resources.NewIcon, new Rectangle(r.X, r.Y, 16, 16));
            }
        }

        #endregion

        private void listBoxDemos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var itemIndex = listBoxDemos.IndexFromPoint(e.Location);
            if(itemIndex < 0) return;

            var item = listBoxDemos.Items[itemIndex] as DemoItem;

            ShowPage(item);
        }
    }
}