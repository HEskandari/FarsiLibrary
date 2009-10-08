using System.ComponentModel;

namespace FarsiLibrary.WPF.VisualStudio.Design
{
    public class DesignerPropertyChangedEventArgs : PropertyChangedEventArgs
    {
        private readonly object value;

        public DesignerPropertyChangedEventArgs(string property, object value) : base(property)
        {
            this.value = value;
        }

        public object Value
        {
            get { return value; }
        }
    }
}
