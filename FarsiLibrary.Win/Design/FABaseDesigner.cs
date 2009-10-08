using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;
using System.Windows.Forms.Design;
using FarsiLibrary.Win.BaseClasses;

namespace FarsiLibrary.Win.Design
{
    /// <summary>
    /// Base designer for all designer classes.
    /// </summary>
    internal class FABaseDesigner : ControlDesigner
    {
        #region Fields

        protected static ArrayList Designers;
        protected DesignerVerb ShowAbout;
        protected DesignerVerbCollection designerVerbs;
        private IComponentChangeService changeService;

        #endregion

        #region Ctor

        static FABaseDesigner()
        {
            Designers = new ArrayList();
        }

        public FABaseDesigner()
        {
            changeService = null;
            designerVerbs = new DesignerVerbCollection();

            ShowAbout = new DesignerVerb("About Farsi Libraries", OnShowAbout);
            ShowAbout.Checked = false;
            designerVerbs.Add(ShowAbout);

            Designers.Add(this);
        }

        #endregion

        #region Verbs

        private void OnShowAbout(object sender, EventArgs e)
        {
            About frm = new About();
            frm.Show();
        }

        #endregion

        #region Component Changing

        protected void OnComponentChanging()
        {
            ChangeService.OnComponentChanging(Control, null);
        }

        protected void OnComponentChanged()
        {
            ChangeService.OnComponentChanged(Control, null, null, null);
        }

        #endregion

        #region Overrides

        protected override void Dispose(bool disposing)
        {
            Designers.Remove(this);

            if (disposing)
            {
                if (changeService != null)
                {
                    changeService.ComponentRename -= OnComponentRename;
                }

                changeService = null;
            }
            base.Dispose(disposing);
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);

            if (component.Site != null)
            {
                changeService = component.Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                changeService.ComponentRename += OnComponentRename;
            }
        }

        protected void OnComponentRename(object sender, ComponentRenameEventArgs e)
        {
            if (e.Component == Editor)
            {
                ResetReferenceName();
            }
        }

        protected virtual void ResetReferenceName()
        {
            IReferenceService svc = GetService(typeof(IReferenceService)) as IReferenceService;
            if (svc != null)
            {
                FieldInfo fi = svc.GetType().GetField("referenceList", BindingFlags.GetField | (BindingFlags.NonPublic | BindingFlags.Instance));
                if (fi != null)
                {
                    ArrayList values = fi.GetValue(svc) as ArrayList;
                    if (values != null)
                    {
                        foreach (object val in values)
                        {
                            PropertyInfo pi = val.GetType().GetProperty("SitedComponent", BindingFlags.GetProperty | (BindingFlags.Public | BindingFlags.Instance));
                            if (pi != null)
                            {
                                object obj = pi.GetValue(val, null);
                                if (obj == Editor)
                                {
                                    MethodInfo mi = val.GetType().GetMethod("ResetName", BindingFlags.InvokeMethod | (BindingFlags.NonPublic | BindingFlags.Instance));
                                    if (mi != null)
                                    {
                                        mi.Invoke(val, null);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Props

        public IComponentChangeService ChangeService
        {
            get { return changeService; }
        }

        public virtual Component Editor
        {
            get { return (Component as BaseControl); }
        }

        protected virtual bool IsSetTextProperty
        {
            get { return true; }
        }

        public override DesignerVerbCollection Verbs
        {
            get { return designerVerbs; }
        }

        #endregion
    }
}
