using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace FarsiLibrary.Win.FAPopup
{
    internal class FAShadowManager : IDisposable
    {
        #region Fields

        private Form form;
        private Rectangle creator;
        private bool visible = false;
        private const int rightShadow = 0,
                          bottomShadow = 1;

        private int shadowSize = FAShadow.DefaultShadowSize;
        private Hashtable shadows = null;

        #endregion

        #region Ctor & Dispose

        public FAShadowManager(Form form) 
        {
			creator = Rectangle.Empty;
			this.form = form;
			this.form.VisibleChanged += new EventHandler(OnForm_VisibleChanged);
			this.form.Move += new EventHandler(OnForm_Move);
		}

		public virtual void Dispose() 
        {
			if(Form != null) 
            {
				Hide();
				Form.Move -= new EventHandler(OnForm_Move);
				Form.VisibleChanged -= new EventHandler(OnForm_VisibleChanged);
				DestroyShadows();
				form = null;		
			}
		}

        #endregion

        #region Props

        public int ShadowSize
        {
            get { return shadowSize; } 
            set { shadowSize = value; } 
        }

        protected Hashtable Shadows
        {
            get
            {
                if (shadows == null)
                    shadows = new Hashtable();

                return shadows;
            }
        }

        public Form Form 
        { 
            get { return form; } 
        }

        protected Rectangle CreatorBounds
        {
            get { return creator; }
            set { creator = value; }
        }

        public bool Visible 
        {
            get { return visible; } 
        }

        protected virtual bool CanShowShadow
        {
            get { return Form != null && Form.Visible && !Form.Bounds.IsEmpty && !Form.Disposing; }
        }

        #endregion

        #region Methods

        public void Move() 
        { 
            Move(CreatorBounds); 
        }

        public virtual void Update()
        {
            Move();
        }

        public void Show() 
        {
            Show(Rectangle.Empty); 
        }

        public virtual void Show(Rectangle creatorBounds)
        {
            if (Visible || !CanShowShadow) return;
            visible = true;
            UpdateShadowBounds();
            UpdateShadowRegions();
            foreach (DictionaryEntry entry in Shadows)
            {
                ShowShadow(entry.Value as FAShadow);
            }
        }

        protected virtual void ShowShadow(FAShadow shadow)
        {
            if (Form != null) 
                Form.AddOwnedForm(shadow);
            
            shadow.ShowShadow();
        }

        public virtual void Move(Rectangle creatorBounds)
        {
            creator = creatorBounds;
            if (!Visible) return;
            UpdateShadowBounds();
            UpdateShadowRegions();
            foreach (DictionaryEntry entry in Shadows)
            {
                (entry.Value as FAShadow).MoveShadow();
            }

        }

        protected virtual void UpdateShadowBounds()
        {
            Rectangle bounds = Form.Bounds;

            Rectangle vertRect = new Rectangle(bounds.Left - ShadowSize, bounds.Top + ShadowSize, ShadowSize, bounds.Height);
            Rectangle horzRect = new Rectangle(bounds.X, bounds.Bottom - ShadowSize, bounds.Width - ShadowSize, ShadowSize);

            CreateShadow(rightShadow).RealBounds = vertRect;
            CreateShadow(bottomShadow).RealBounds = horzRect;

            //Rectangle or = CreatorBounds;
            //if (or.IsEmpty)
            //{
            //    DestroyShadow(creatorBottomShadow);
            //    DestroyShadow(creatorRightShadow);
            //}
            //else
            //{
            //CreateShadow(creatorRightShadow).RealBounds = new Rectangle(or.Right, or.Top + ShadowSize, ShadowSize, or.Height);
            //CreateShadow(creatorBottomShadow).RealBounds = new Rectangle(or.X + ShadowSize, or.Bottom, or.Width - ShadowSize, ShadowSize);
            //}

        }

        public virtual void Hide()
        {
            if (!Visible) return;
            foreach (DictionaryEntry entry in Shadows)
            {
                (entry.Value as FAShadow).HideShadow();
            }
            
            visible = false;
        }

        protected virtual void UpdateShadowRegions()
        {
            if (!Visible || shadows == null) return;
            foreach (DictionaryEntry entry in shadows)
            {
                FAShadow shadow = entry.Value as FAShadow;
                shadow.Region = GetShadowRegion(shadow.RealBounds);
            }
        }

        private Region GetShadowRegion(Rectangle shadow)
        {
            if (Form == null) return null;
            Rectangle i1 = CheckShadowIntersects(shadow, Form.Bounds),
                i2 = CheckShadowIntersects(shadow, CreatorBounds);
            if (!i1.IsEmpty || !i2.IsEmpty)
            {
                Region reg = new Region(new Rectangle(Point.Empty, shadow.Size));
                if (!i1.IsEmpty) reg.Exclude(i1);
                if (!i2.IsEmpty) reg.Exclude(i2);
                return reg;
            }
            return null;
        }

        private Rectangle CheckShadowIntersects(Rectangle shadow, Rectangle rect)
        {
            if (rect.IsEmpty || shadow.IsEmpty || !rect.IntersectsWith(shadow)) return Rectangle.Empty;
            Rectangle r = rect;
            r.Offset(-shadow.X, -shadow.Y);
            if (r.X < 0)
            {
                r.Width += r.X;
                r.X = 0;
            }
            if (r.Y < 0)
            {
                r.Height += r.Y;
                r.Y = 0;
            }
            if (r.Width > 0 && r.Height > 0) return r;
            return Rectangle.Empty;
        }

        protected virtual FAShadow CreateShadow(int shadow)
        {
            FAShadow res = GetShadow(shadow);
            if (res == null) Shadows[shadow] = res = new FAShadow(shadow % 2 != 0, 0, Form);
            return res;
        }

        protected FAShadow GetShadow(int shadow)
        {
            if (shadows == null)
                return null;

            return Shadows[shadow] as FAShadow;
        }

        private void OnForm_VisibleChanged(object sender, EventArgs e)
        {
            if (!Form.Visible)
                Hide();
        }

        private void OnForm_Move(object sender, EventArgs e)
        {
            if (Visible)
                Move(CreatorBounds);
        }

        protected virtual void DestroyShadows()
        {
            if (shadows == null) return;
            foreach (DictionaryEntry entry in shadows)
            {
                (entry.Value as FAShadow).Dispose();
            }
            shadows.Clear();
        }

        protected void DestroyShadow(int shadow)
        {
            FAShadow sh = GetShadow(shadow);
            if (sh != null)
            {
                sh.Dispose();
                Shadows.Remove(sh);
            }
        }

        #endregion
    }

}
