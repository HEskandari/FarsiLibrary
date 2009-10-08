using System.ComponentModel;
using FarsiLibrary.Utils.Internals;
using FarsiLibrary.Web.Mvc.Controls;
using FarsiLibrary.Web.Mvc.Helpers;

namespace FarsiLibrary.Web.Mvc.Builder
{
    public abstract class ControlBuilder<TControl, TBuilder> : IHideMembers 
        where TControl : WebControl
        where TBuilder : ControlBuilder<TControl, TBuilder>
    {
        private readonly TControl control;

        protected ControlBuilder(TControl control)
        {
            Guard.Against(control == null, "control should not be null");

            this.control = control;
        }

        protected TControl Control
        {
            get { return control; }
        }

        public static implicit operator TControl(ControlBuilder<TControl, TBuilder> builder)
        {
            Guard.Against(builder == null, "builder should not be null");

            return builder.ToControl();
        }

        /// <summary>
        /// Returns the internal view component.
        /// </summary>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public TControl ToControl()
        {
            return Control;
        }

        /// <summary>
        /// Sets the name of the component.
        /// </summary>
        /// <param name="controlName">The name.</param>
        /// <returns></returns>
        public virtual TBuilder Named(string controlName)
        {
            Control.Name = controlName;

            return this as TBuilder;
        }

        /// <summary>
        /// Sets the HTML attributes.
        /// </summary>
        /// <param name="attributes">The HTML attributes.</param>
        /// <returns></returns>
        public virtual TBuilder HtmlAttributes(object attributes)
        {
            Guard.Against(attributes == null, "attributes should not be null");

            Control.HtmlAttributes.Clear();
            Control.HtmlAttributes.Merge(attributes);

            return this as TBuilder;
        }

        /// <summary>
        /// Renders the component.
        /// </summary>
        public virtual void Render()
        {
            Control.Render();
        }
    }
}