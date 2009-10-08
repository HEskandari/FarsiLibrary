using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading;

namespace FarsiLibrary.WPF.VisualStudio.Design
{
    /// <summary>
    /// Interaction logic for AboutUI.xaml
    /// </summary>
    public partial class AboutUI
    {
        private static ObservableCollection<string> AssemblyNames;

        public AboutUI()
        {
            InitializeComponent();

            this.DataContext = this;
            Assemblies = GetLoadedAssemblies();
        }

        protected ObservableCollection<string> GetLoadedAssemblies()
        {
            if (AssemblyNames == null)
            {
                AssemblyNames = new ObservableCollection<string>();
                Assembly[] assemblies = Thread.GetDomain().GetAssemblies();

                if (assemblies != null && assemblies.Length > 0)
                {
                    foreach (var asm in assemblies)
                    {
                        if (asm.GetName().Name.StartsWith("FarsiLibrary"))
                        {
                            string itemName = asm.GetName().Name + " " + asm.GetName().Version;
                            AssemblyNames.Add(itemName);
                        }
                    }
                }
            }

            return AssemblyNames;
        }

        public ObservableCollection<string> Assemblies
        {
            get;
            private set;
        }
    }
}
