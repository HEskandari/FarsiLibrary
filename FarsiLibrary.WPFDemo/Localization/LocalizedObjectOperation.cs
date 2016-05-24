using System;
using System.Reflection;

namespace FarsiLibrary.WPFDemo.Localization
{
    public static class LocalizedObjectOperation
    {

        public static string GetString(string assembly, string dictionary, string key)
        {

            if (assembly == null) throw new ArgumentNullException("assembly");
            if (assembly == string.Empty) throw new ArgumentException("assembly is empty", "assembly");

            if (dictionary == null) throw new ArgumentNullException("dictionary");
            if (dictionary == string.Empty) throw new ArgumentException("dictionary is empty", "dictionary");

            if (key == null) throw new ArgumentNullException("key");
            if (key == string.Empty) throw new ArgumentException("key is empty", "key");

            try
            {
                return (string)LocalizeDictionary.Instance.GetLocalizedObject<object>(
                                   assembly, dictionary, key, LocalizeDictionary.Instance.Culture);
            }
            catch
            {
                return string.Format("No resource key with name '{0}' in dictionary '{1}' in assembly '{2}' founded! ({2}.{1}.{0})",
                                     key, dictionary, assembly);
            }
        }

        public static string GetString(string dictionary, string key)
        {
            if (dictionary == null) throw new ArgumentNullException("dictionary");
            if (dictionary == string.Empty) throw new ArgumentException("dictionary is empty", "dictionary");

            if (key == null) throw new ArgumentNullException("key");
            if (key == string.Empty) throw new ArgumentException("key is empty", "key");

            string assembly = LocalizeDictionary.GetAssemblyName(Assembly.Load(LocalizeDictionary.AssemblyName));

            try
            {
                return (string)LocalizeDictionary.Instance.GetLocalizedObject<object>(
                                   assembly, dictionary, key, LocalizeDictionary.Instance.Culture);
            }
            catch
            {
                return string.Format("No resource key with name '{0}' in dictionary '{1}' in assembly '{2}' founded! ({2}.{1}.{0})",
                                     key, dictionary, assembly);
            }
        }
    }
}