using System.Collections;
using System.Collections.Generic;

namespace FarsiLibrary.Resources
{
    internal static class AssemblyNames
    {
        public const string Description = "Library containing farsi controls, which has correct Right-To-Left drawing. Also contains classes to work with Jalali Dates";
        public const string AssemblyTradeMark = "Farsi Library and Controls by Hadi Eskandari (H.Eskandari@Gmail.com). All rights are reserved for the author. Contact the author to obtain license to use this product.";
        public const string AssemblyCopyright = "Copyright (c) 2005-2009";
        public const string AssemblyGenericTitle = "Farsi Library";
        public const string Version = "2.5.0.0";
        public const string ShortVersion = "2.5";

        public static Product WinForms = new Product {Name = "FarsiLibrary.Win"};
        public static Product WebForms = new Product {Name = "FarsiLibrary.Web"};
        public static Product Wpf = new Product {Name = "FarsiLibrary.WPF"};
        public static Product Utils = new Product {Name = "FarsiLibrary.Utils"};
        public static Product Resources = new Product {Name = "FarsiLibrary.Resources"};
        
        public static List<Product> Products = new List<Product> { WinForms, Wpf, WebForms, Utils, Resources };

        internal class Product
        {
            public string Name
            { 
                get; set;
            }
        }
    }
}
