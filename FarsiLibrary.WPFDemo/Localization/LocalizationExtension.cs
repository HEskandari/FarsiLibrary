using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace FarsiLibrary.WPFDemo.Localization
{
    /// <summary>
    /// Represents a LocalizationExtension which provides a localized object of a .resx dictionary
    /// </summary>
    /// <typeparam name="TValue">The type of the provided value.</typeparam>
    /// <remarks>
    /// If a content between two tags in xaml is set, this has the higher priority and will overwrite the setted properties
    /// </remarks>
    [MarkupExtensionReturnType(typeof(object))]
    [ContentProperty("ResourceIdentifierKey")]
    public abstract class LocalizeExtension<TValue> : MarkupExtension, IWeakEventListener, INotifyPropertyChanged
    {
        #region Members

        /// <summary>
        /// Holds the Name of the .resx dictionary.
        /// If it's null, "Resources" will get returned
        /// </summary>
        private string m_Dict;

        /// <summary>
        /// Holds the name of the Assembly where the .resx is located
        /// </summary>
        private string m_Assembly;

        /// <summary>
        /// Holds the Key to a .resx object
        /// </summary>
        private string m_Key;

        /// <summary>
        /// Holds the collection of assigned dependency objects as WeakReferences
        /// </summary>
        private readonly Dictionary<WeakReference, object> m_TargetObjects;

        /// <summary>
        /// The current value
        /// </summary>
        private TValue m_CurrentValue;
        #endregion

        #region Properties
        /// <summary>
        /// Holds the Key that identifies a resource (Assembly:Dictionary:Key)
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string ResourceIdentifierKey
        {
            get { return string.Format("{0}:{1}:{2}", Assembly, Dict, Key ?? "(null)"); }
            set { LocalizeDictionary.ParseKey(value, out m_Assembly, out m_Dict, out m_Key); }
        }

        /// <summary>
        /// Holds the culture to force a fixed localized object
        /// </summary>
        public string ForceCulture { get; set; }

        /// <summary>
        /// Gets or sets the design value.
        /// </summary>
        /// <value>The design value.</value>
        [DesignOnly(true)]
        public object DesignValue { get; set; }

        /// <summary>
        /// Holds the Key to a .resx object
        /// </summary>
        public string Key
        {
            get { return m_Key; }
            set { m_Key = value; }
        }

        /// <summary>
        /// Holds the name of the Assembly where the .resx is located.
        /// If it's null, the executing assembly (where this LocalizeEngine is located at) will get returned
        /// </summary>
        public string Assembly
        {
            get { return m_Assembly ?? LocalizeDictionary.AssemblyName; }
            set { m_Assembly = !string.IsNullOrEmpty(value) ? value : null; }
        }

        /// <summary>
        /// Holds the Name of the .resx dictionary.
        /// If it's null, "Resources" will get returned
        /// </summary>
        public string Dict
        {
            get { return m_Dict ?? LocalizeDictionary.ResourcesName; }
            set { m_Dict = !string.IsNullOrEmpty(value) ? value : null; }
        }

        /// <summary>
        /// Gets or sets the current value.
        /// This property has only a value, if the LocalizeExtension is binded to a target.
        /// </summary>
        /// <value>The current value.</value>
        public TValue CurrentValue
        {
            get
            {
                return m_CurrentValue;
            }
            private set
            {
                m_CurrentValue = value;
                RaiseNotifyPropertyChanged("CurrentValue");
            }
        }
        #endregion

        #region Dependency Properties
        /// <summary>
        /// The collection of <see cref="DependencyObject"/> as WeakReferences and the target property.
        /// </summary>
        public Dictionary<WeakReference, object> TargetObjects { get { return m_TargetObjects; } }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialize the LocalizeExtension
        /// </summary>
        protected LocalizeExtension()
        {
            // initialize the collection of the assigned dependency objects
            m_TargetObjects = new Dictionary<WeakReference, object>();
        }

        /// <summary>
        /// Initialize the LocalizeExtension
        /// </summary>
        /// <remarks>
        /// This constructor register the <see cref="EventHandler"/> <c>OnCultureChanged</c> on <c>LocalizeDictionary</c>
        /// to get an acknowledge of changing the culture
        /// </remarks>
        /// <param name="key">
        /// Three types are supported:
        /// Direct: passed key = key; 
        /// Dict/Key pair: this have to be separated like ResXDictionaryName:ResourceKey
        /// Assembly/Dict/Key pair: this have to be separated like ResXDictionaryName:ResourceKey
        /// </param>
        protected LocalizeExtension(string key)
            : this()
        {
            // parse the key value and split it up if necessary
            LocalizeDictionary.ParseKey(key, out m_Assembly, out m_Dict, out m_Key);
        }
        #endregion

        #region GetForcedCultureOrDefault
        /// <summary>
        /// If Culture property defines a valid <see cref="CultureInfo"/>, a <see cref="CultureInfo"/> instance will get 
        /// created and returned, otherwise <see cref="LocalizeDictionary"/>.Culture will get returned.
        /// </summary>
        /// <returns>The <see cref="CultureInfo"/></returns>
        /// <exception cref="System.ArgumentException">thrown if the parameter Culture don't defines a valid <see cref="CultureInfo"/></exception>
        protected CultureInfo GetForcedCultureOrDefault()
        {
            // define a culture info
            CultureInfo cultureInfo;

            // try to create a valid cultureinfo, if defined
            if (!string.IsNullOrEmpty(ForceCulture))
            {
                try
                {
                    // try to create a specific culture from the forced one
                    cultureInfo = CultureInfo.CreateSpecificCulture(ForceCulture);
                }
                catch (ArgumentException ex)
                {
                    // on error, check if designmode is on
                    if (LocalizeDictionary.Instance.GetIsInDesignMode())
                    {
                        // cultureInfo will be set to the current specific culture
                        cultureInfo = LocalizeDictionary.Instance.SpecificCulture;
                    }
                    else
                    {
                        // tell the customer, that the forced culture cannot be converted propperly
                        throw new ArgumentException("Cannot create a CultureInfo with '" + ForceCulture + "'", ex);
                    }
                }
            }
                // elsewise, take the current specific culture
            else
            {
                cultureInfo = LocalizeDictionary.Instance.SpecificCulture;
            }

            // return the evaluated culture info
            return cultureInfo;
        }
        #endregion

        #region SetNewValue
        /// <summary>
        /// Set the Value of the <see cref="DependencyProperty"/> to the passed Value
        /// </summary>
        /// <param name="newValue">The new Value</param>
        protected void SetNewValue(object newValue)
        {
            // set the new value to the current value, if its the type of TValue
            if (newValue is TValue)
            {
                CurrentValue = (TValue)newValue;
            }

            // if the list of dependency objects is empty or the target property is null, return
            if (m_TargetObjects.Count == 0) return;

            // step through all dependency objects as WeakReference and refresh the value of the dependency property
            foreach (KeyValuePair<WeakReference, object> dpo in m_TargetObjects)
            {
                // set the new value of the target, if the target DependencyTarget is still alive
                if (dpo.Key.IsAlive)
                {
                    SetTargetValue((DependencyObject)dpo.Key.Target, dpo.Value, newValue);
                }
            }
        }
        #endregion

        #region ProvideValue
        /// <summary>
        /// Provides the Value for the first Binding
        /// </summary>
        /// <remarks>
        /// This method register the <see cref="EventHandler"/> <c>OnCultureChanged</c> on <c>LocalizeDictionary</c>
        /// to get an acknowledge of changing the culture, if the passed <see cref="TargetObjects"/> type of <see cref="DependencyObject"/>.
        /// 
        /// !PROOF: On every single <see cref="UserControl"/>, Window, and Page, 
        /// there is a new SparedDP reference, and so there is every time a new <c>LocalizeExtension</c>!
        /// Because of this, we don't need to notify every single DependencyObjects to update their value (for GC).
        /// </remarks>
        /// <param name="serviceProvider">
        /// The <see cref="System.Windows.Markup.IProvideValueTarget"/> provided from the <see cref="MarkupExtension"/>
        /// </param>
        /// <returns>The founded item from the .resx directory or null if not founded</returns>
        /// <exception cref="System.InvalidOperationException">
        /// thrown if <paramref name="serviceProvider"/> is not type of <see cref="System.Windows.Markup.IProvideValueTarget"/>
        /// </exception>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            // try to cast the passed serviceProvider to a IProvideValueTarget
            IProvideValueTarget service = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            // if the cast fails, an exception will be thrown. (should never happen)
            if (service == null)
            {
                throw new InvalidOperationException("IProvideValueTarget service is unavailable");
            }

            // if the service.TargetObject is a Binding, throw an exception
            if (service.TargetObject is System.Windows.Data.Binding)
            {
                throw new InvalidOperationException("Use as binding is not supported!");
            }

            // declare a target property
            object targetProperty = null;

            // check if the service.TargetProperty is a DependencyProperty or a PropertyInfo
            if (service.TargetProperty is DependencyProperty ||
                service.TargetProperty is PropertyInfo)
            {

                // set the target property to the service.TargetProperty
                targetProperty = service.TargetProperty;
            }

            // check if the target property is null
            if (targetProperty == null)
            {
                // return this.
                return this;
            }

            // if the service.TargetObject is System.Windows.SharedDp (= not a DependencyObject), we return "this".
            // the SharedDp will call this instance later again.
            if (!(service.TargetObject is DependencyObject))
            {
                // by returning "this", the provide value will be called later again.
                return this;
            }

            // indicates, if the target object was found
            bool foundInWeakReferences = false;

            // search for the target in the target object list
            foreach (KeyValuePair<WeakReference, object> wr in m_TargetObjects)
            {
                // if the target is the target of the weakreference
                if (wr.Key.Target == service.TargetObject && wr.Value == service.TargetProperty)
                {
                    // set the flag to true and stop searching
                    foundInWeakReferences = true;
                    break;
                }
            }

            // if the target is a dependency object and it's not collected already, collect it
            if (service.TargetObject is DependencyObject && !foundInWeakReferences)
            {

                // if it's the first object, add an event handler too
                if (m_TargetObjects.Count == 0)
                {
                    // add this localize extension to the WeakEventManager on LocalizeDictionary
                    LocalizeDictionary.Instance.AddEventListener(this);
                }

                // add the target as an dependency object as weakreference to the dependency object list
                m_TargetObjects.Add(new WeakReference(service.TargetObject), service.TargetProperty);

                // adds this localize extension to the ObjectDependencyManager to ensure the lifetime along with the targetobject
                ObjectDependencyManager.AddObjectDependency(new WeakReference(service.TargetObject), this);
            }

            // return the new value for the DependencyProperty
            return LocalizeDictionary.Instance.GetLocalizedObject<object>(Assembly, Dict, Key, GetForcedCultureOrDefault());
        }
        #endregion

        #region ResolveLocalizedValue
        /// <summary>
        /// Resolves the localized value of the current Assembly, Dict, Key pair.
        /// </summary>
        /// <param name="resolvedValue">The resolved value.</param>
        /// <returns>
        /// True if the resolve was success, otherwise false.
        /// </returns>
        /// <exception>If the Assembly, Dict, Key pair was not found.</exception>
        public bool ResolveLocalizedValue(out TValue resolvedValue)
        {
            // return the resolved localized value with the current or forced culture.
            return ResolveLocalizedValue(out resolvedValue, GetForcedCultureOrDefault());
        }

        /// <summary>
        /// Resolves the localized value of the current Assembly, Dict, Key pair.
        /// </summary>
        /// <param name="resolvedValue">The resolved value.</param>
        /// <param name="targetCulture">The target culture.</param>
        /// <returns>
        /// True if the resolve was success, otherwise false.
        /// </returns>
        /// <exception>If the Assembly, Dict, Key pair was not found.</exception>
        public bool ResolveLocalizedValue(out TValue resolvedValue, CultureInfo targetCulture)
        {
            // define the default value of the resolved value
            resolvedValue = default(TValue);

            // get the localized object from the dictionary
            object localizedObject = LocalizeDictionary.Instance.GetLocalizedObject<object>(
                Assembly, Dict, Key, targetCulture);

            // check if the found localized object is type of TValue
            if (localizedObject is TValue)
            {
                // format the localized object
                object formattedOutput = FormatOutput(localizedObject);

                // check if the formatted output is not null
                if (formattedOutput != null)
                {
                    // set the content of the resolved value
                    resolvedValue = (TValue)formattedOutput;
                }

                // return true: resolve was successfully
                return true;
            }

            // return false: resulve was not successfully.
            return false;
        }
        #endregion

        #region SetBinding
        /// <summary>
        /// Sets a binding between a <see cref="DependencyObject"/> with its <see cref="DependencyProperty"/> 
        /// or <see cref="PropertyInfo"/> and the LocalizeExtension&lt;&gt;
        /// </summary>
        /// <param name="targetObject">The target dependency object</param>
        /// <param name="targetProperty">The target dependency property</param>
        /// <returns>TRUE if the binding was setup successfully, otherwise FALSE (Binding already exists).</returns>
        /// <exception cref="ArgumentException">If the <paramref name="targetProperty"/> is 
        /// not a <see cref="DependencyProperty"/> or <see cref="PropertyInfo"/>.</exception>
        public bool SetBinding(DependencyObject targetObject, object targetProperty)
        {

            if (!(targetProperty is DependencyProperty ||
                  targetProperty is PropertyInfo))
            {

                throw new ArgumentException("The targetProperty should be a DependencyProperty or PropertyInfo!", "targetProperty");
            }

            // indicates, if the target object was found
            bool foundInWeakReferences = false;
            // search for the target in the target object list
            foreach (KeyValuePair<WeakReference, object> wr in m_TargetObjects)
            {
                // if the target is the target of the weakreference
                if (wr.Key.Target == targetObject && wr.Value == targetProperty)
                {
                    // set the flag to true and stop searching
                    foundInWeakReferences = true;
                    break;
                }
            }

            // if the target it's not collected already, collect it
            if (!foundInWeakReferences)
            {

                // if it's the first object, add an event handler too
                if (m_TargetObjects.Count == 0)
                {
                    // add this localize extension to the WeakEventManager on LocalizeDictionary
                    LocalizeDictionary.Instance.AddEventListener(this);
                }

                // add the target as an dependency object as weakreference to the dependency object list
                m_TargetObjects.Add(new WeakReference(targetObject), targetProperty);

                // adds this localize extension to the ObjectDependencyManager to ensure the lifetime along with the targetobject
                ObjectDependencyManager.AddObjectDependency(new WeakReference(targetObject), this);

                // set the initial value of the dependency property
                SetTargetValue(targetObject,
                               targetProperty,
                               FormatOutput(LocalizeDictionary.Instance.GetLocalizedObject<object>(
                                                Assembly, Dict, Key, GetForcedCultureOrDefault())));

                // return true, the binding was successfully
                return true;
            }

            // return false, the binding already exists
            return false;
        }
        #endregion

        #region SetTargetValue
        /// <summary>
        /// Sets the target value.
        /// </summary>
        /// <param name="targetObject">The target object.</param>
        /// <param name="targetProperty">The target property.</param>
        /// <param name="value">The value.</param>
        private void SetTargetValue(DependencyObject targetObject, object targetProperty, object value)
        {
            // check if the target property is a DependencyProperty
            if (targetProperty is DependencyProperty)
            {
                SetTargetValue(targetObject, (DependencyProperty)targetProperty, value);
            }
                // check if the target property is a PropertyInfo
            else if (targetProperty is PropertyInfo)
            {
                SetTargetValue(targetObject, (PropertyInfo)targetProperty, value);
            }
        }

        /// <summary>
        /// Sets the target value.
        /// </summary>
        /// <param name="targetObject">The target object.</param>
        /// <param name="targetProperty">The target property.</param>
        /// <param name="value">The value.</param>
        private void SetTargetValue(DependencyObject targetObject, DependencyProperty targetProperty, object value)
        {
            targetObject.SetValue(targetProperty, value);
        }

        /// <summary>
        /// Sets the target value.
        /// </summary>
        /// <param name="targetObject">The target object.</param>
        /// <param name="targetProperty">The target property.</param>
        /// <param name="value">The value.</param>
        private void SetTargetValue(DependencyObject targetObject, PropertyInfo targetProperty, object value)
        {
            targetProperty.SetValue(targetObject, value, null);
        }
        #endregion

        #region Abstract FormatOutput
        /// <summary>
        /// This method is used to modify the passed object into the target format
        /// </summary>
        /// <param name="input">The object that will be modified</param>
        /// <returns>Returns the modified object</returns>
        protected abstract object FormatOutput(object input);
        #endregion

        #region HandleNewValue
        /// <summary>
        /// This method gets the new value for the target property and call <see cref="SetNewValue"/>.
        /// </summary>
        protected virtual void HandleNewValue()
        {
            // gets the new value and set it to the dependency property on the dependency object
            SetNewValue(LocalizeDictionary.Instance.GetLocalizedObject<object>(Assembly, Dict, Key, GetForcedCultureOrDefault()));
        }
        #endregion

        #region IsTypeOf
        /// <summary>
        /// Determines whether if the <paramref name="checkType"/> is the <paramref name="targetType"/>.
        /// </summary>
        /// <param name="checkType">Type of the check.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <returns>
        /// 	<c>true</c> if the <paramref name="checkType"/> is type of the <paramref name="targetType"/>; otherwise, <c>false</c>.
        /// </returns>
        internal bool IsTypeOf(Type checkType, Type targetType)
        {
            // if the checkType is null (possible base type), return false
            if (checkType == null) return false;
            // if the targetType (wrong call) is null, return false
            if (targetType == null) return false;

            // if we search a generic type
            if (targetType.IsGenericType)
            {
                // and the checkType is a generic (BaseType)
                if (checkType.IsGenericType)
                {
                    // and the signature is the same
                    if (checkType.GetGenericTypeDefinition() == targetType)
                    {
                        // return true
                        return true;
                    }
                }
                // otherwise call the same method again with the base type
                return IsTypeOf(checkType.BaseType, targetType);
            }

            // if we search a non generic type and its equal
            if (checkType.Equals(targetType))
            {
                // return true
                return true;
            }
            // otherwise call the same method again with the base type
            return IsTypeOf(checkType.BaseType, targetType);
        }
        #endregion

        #region IWeakEventListener Members

        /// <summary>
        /// This method will be called through the interface, passed to the 
        /// <see cref="LocalizeDictionary"/>.<see cref="LocalizeDictionary.WeakCultureChangedEventManager"/> to get notified on culture changed
        /// </summary>
        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            // if the passed handler is type of LocalizeDictionary.WeakCultureChangedEventManager, handle it
            if (managerType == typeof(LocalizeDictionary.WeakCultureChangedEventManager))
            {
                // call to handle the new value
                HandleNewValue();
                // return true, to notify the event was processed
                return true;
            }
            // return false, to notify the event was not processed
            return false;
        }

        #endregion

        #region RaiseNotifyPropertyChanged
        /// <summary>
        /// Raises the notify property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void RaiseNotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region INotifyPropertyChanged Members
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Destructor
        ///// <summary>
        ///// This destructor only exists fr debug reasons
        ///// </summary>
        //~LocalizeExtension() {
        //    //#if DEBUG
        //    //            System.Diagnostics.Debug.WriteLine("LocExtension destructed!");
        //    //#endif
        //}
        #endregion

        #region ToString()
        /// <summary>
        /// Returns the Key that identifies a resource (Assembly:Dictionary:Key)
        /// </summary>
        /// <returns>Format: Assembly:Dictionary:Key</returns>
        public sealed override string ToString()
        {
            return base.ToString() + " -> " + ResourceIdentifierKey;
        }
        #endregion
    }
}