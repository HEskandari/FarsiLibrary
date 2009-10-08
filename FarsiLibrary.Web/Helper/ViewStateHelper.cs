using System.Web.UI;

namespace FarsiLibrary.Web.Helper
{
    public static class ViewStateHelper
    {
        /// <summary>
        /// Returns the property value stored in ViewSatate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="viewState"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static T GetValue<T>(this StateBag viewState, string propertyName)
        {
            return GetValue<T>(viewState, propertyName, default(T));
        }

        /// <summary>
        /// Returns the property value stored in ViewSatate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="viewState"></param>
        /// <param name="propertyName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetValue<T>(this StateBag viewState, string propertyName, T defaultValue)
        {
            object o = viewState[propertyName] as object;
            if (o != null)
            {
                return (T)o;
            }

            return defaultValue;
        }

        /// <summary>
        /// Stores the value on ViewState
        /// </summary>
        /// <param name="viewState"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void SetValue(this StateBag viewState, string propertyName, object value)
        {
            viewState[propertyName] = value;
        }
    }
}
