using System;

namespace FarsiLibrary.Utils
{
    /// <summary>
    /// Converts a date instance to pleasing-eye format.
    /// For example formatting DateTime.Now.AddDays(-1) would 
    /// return "yesterday" or its equivalant for PersianDate
    /// </summary>
    public interface IPrettyFormatter
    {
        /// <summary>
        /// Formats an instance of the DateTime
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        string Format(DateTime date);

        /// <summary>
        /// Formats an instance of PersianDate
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        string Format(PersianDate date);
    }
}