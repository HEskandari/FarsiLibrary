using System;
using FarsiLibrary.Win.BaseClasses;
using FarsiLibrary.Win.Enums;

namespace FarsiLibrary.Win.Drawing
{
    public class FAPainterFactory
    {
        private static readonly FAPainterOffice2000 PainterOffice2000;
        private static readonly FAPainterOffice2003 PainterOffice2003;
        private static readonly FAPainterOffice2007 PainterOffice2007;
        private static readonly FAPainterWindowsXP PainterWinXP;

        /// <summary>
        /// Ctor. Creates know painter types.
        /// </summary>
        static FAPainterFactory()
        {
            PainterOffice2000 = new FAPainterOffice2000();
            PainterOffice2003 = new FAPainterOffice2003();
            PainterOffice2007 = new FAPainterOffice2007();
            PainterWinXP = new FAPainterWindowsXP();
        }

        /// <summary>
        /// Returns a IFAPainter implementation based on state 
        /// of the control.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static IFAPainter GetPainter(BaseStyledControl control)
        {
            if(control == null)
                throw new InvalidOperationException("Control can not be null");

            if (!control.UseThemes || control.Theme == ThemeTypes.Office2000)
                return PainterOffice2000;
            
            if (control.UseThemes && control.Theme == ThemeTypes.Office2007)
                return PainterOffice2007;
            
            if (control.UseThemes && control.Theme == ThemeTypes.Office2003)
                return PainterOffice2003;

            if (control.UseThemes && control.Theme == ThemeTypes.WindowsXP)
                return PainterWinXP;
            
            return PainterOffice2000;
        }
    }
}