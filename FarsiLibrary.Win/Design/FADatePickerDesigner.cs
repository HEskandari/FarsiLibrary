using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;
using FarsiLibrary.Win.Controls;

namespace FarsiLibrary.Win.Design
{
    /// <summary>
    /// Design behavior of FADatePicker control
    /// </summary>
    internal class FADatePickerDesigner : FABaseDesigner
    {
        #region Initialize

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            Verbs.Add(new DesignerVerb("Multiline", (sender, e) => OnMultilineChange()));
        }

        #endregion

        #region Methods

        private void OnMultilineChange()
        {
            OnComponentChanging();
            Control.Multiline = !Control.Multiline;
            OnComponentChanged();
        }

        #endregion

        #region Props

        public new virtual FAContainerComboBox Control
        {
            get
            {
                return base.Control as FAContainerComboBox;
            }
        }

        #endregion

        #region Overrides

        public override SelectionRules SelectionRules
        {
            get
            {
                if (Control.Multiline)
                {
                    return SelectionRules.AllSizeable | SelectionRules.Moveable | SelectionRules.Visible;
                }
                else
                {
                    return SelectionRules.RightSizeable | SelectionRules.LeftSizeable | SelectionRules.Visible | SelectionRules.Moveable;
                }
            }
        }

        protected override void PreFilterProperties(IDictionary properties)
        {
            base.PreFilterProperties(properties);

            properties.Remove("Text");
            properties.Remove("AutoScroll");
            properties.Remove("AutoScrollMargin");
            properties.Remove("AutoScrollMinSize");
            properties.Remove("DockPadding");
            properties.Remove("DrawGrid");
            properties.Remove("MinimumSize");
            properties.Remove("MaximumSize");
            properties.Remove("Margin");
            properties.Remove("ForeColor");
            properties.Remove("BackColor");
            properties.Remove("BackgroundImage");
            properties.Remove("BackgroundImageLayout");
        }

        #endregion
    }
}
