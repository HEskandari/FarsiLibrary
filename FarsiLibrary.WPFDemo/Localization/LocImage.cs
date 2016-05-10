using System;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace FarsiLibrary.WPFDemo.Localization
{
    /// <summary>
    /// <c>LocalizeExtension</c> for image objects
    /// </summary>
    [MarkupExtensionReturnType(typeof(BitmapSource))]
    public class LocImage : LocalizeExtension<BitmapSource>
    {
        /// <summary>
        /// see <c>LocalizeExtension</c>
        /// </summary>
        public LocImage()
        {
        }

        /// <summary>
        /// see <c>LocalizeExtension</c>
        /// </summary>
        public LocImage(string key) : base(key)
        {
        }

        /// <summary>
        /// Provides the Value for the first Binding as <see cref="System.Windows.Media.Imaging.BitmapSource"/>
        /// </summary>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            object obj = base.ProvideValue(serviceProvider);
            if (obj == null) return null;
            if (IsTypeOf(obj.GetType(), typeof(LocalizeExtension<>))) return obj;
            if (obj.GetType().Equals(typeof(System.Drawing.Bitmap)))
            {
                return FormatOutput(obj);
            }

            throw new NotSupportedException(string.Format("ResourceKey '{0}' returns '{1}' which is not type of System.Drawing.Bitmap", Key, obj.GetType().FullName));
        }

        /// <summary>
        /// see <c>LocalizeExtension</c>
        /// </summary>
        protected override void HandleNewValue()
        {
            var obj = LocalizeDictionary.Instance.GetLocalizedObject<object>(Assembly, Dict, Key, GetForcedCultureOrDefault());
            SetNewValue(FormatOutput(obj));
        }

        /// <summary>
        /// free memory of a pointer
        /// </summary>
        /// <param name="o">object to remove from memory</param>
        /// <returns>0 if ok, otherwise another number</returns>
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern int DeleteObject(IntPtr o);

        /// <summary>
        /// Creates a <see cref="System.Windows.Media.Imaging.BitmapSource"/> from a <see cref="System.Drawing.Bitmap"/>.
        /// This extension does NOT support a DesignValue.
        /// </summary>
        /// <param name="input">The <see cref="System.Drawing.Bitmap"/> to convert</param>
        /// <returns>The converted <see cref="System.Windows.Media.Imaging.BitmapSource"/></returns>
        protected override object FormatOutput(object input)
        {
            // allocate the memory for the bitmap
            IntPtr bmpPt = ((System.Drawing.Bitmap)input).GetHbitmap();

            // create the bitmapSource
            BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bmpPt, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            // freeze the bitmap to avoid hooking events to the bitmap
            bitmapSource.Freeze();

            // free memory
            DeleteObject(bmpPt);

            // return bitmapSource
            return bitmapSource;
        }
    }
}