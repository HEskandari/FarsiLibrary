using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Windows;

namespace FarsiLibrary.WPFDemo.Localization
{
    /// <summary>
    /// Represents the culture interface for localization
    /// </summary>
    public sealed class LocalizeDictionary : DependencyObject
    {

        #region Singleton Members / Properties
        /// <summary>
        /// Holds the instance of singleton
        /// </summary>
        private static LocalizeDictionary m_Instance;
        /// <summary>
        /// Holds a SyncRoot to be thread safe
        /// </summary>
        private readonly static object m_SyncRoot = new object();

        /// <summary>
        /// Provides the singleton.
        /// If the underlying instance is null, a instance will be created.
        /// </summary>
        public static LocalizeDictionary Instance
        {
            get
            {
                // check if the underlying instance is null
                if (m_Instance == null)
                {
                    // if it is null, lock the syncroot.
                    // if another thread is accessing this too, 
                    // it have to wait until the syncroot is released
                    lock (m_SyncRoot)
                    {
                        // check again, if the underlying instance is null
                        if (m_Instance == null)
                        {
                            // create a new instance
                            m_Instance = new LocalizeDictionary();
                        }
                    }
                }
                // return the existing/new instance
                return m_Instance;
            }
        }
        #endregion

        #region Attatched DependencyProperty Culture
        /// <summary>
        /// Getter of DependencyProperty Culture.
        /// Only supported at DesignTime.
        /// If its in Runtime, LocalizeDictionary.Culture will be returned.
        /// </summary>
        [System.ComponentModel.DesignOnly(true)]
        public static string GetDesignCulture(DependencyObject obj)
        {
            if (LocalizeDictionary.Instance.GetIsInDesignMode())
            {
                return (string)obj.GetValue(DesignCultureProperty);
            }
            else
            {
                return LocalizeDictionary.Instance.Culture.ToString();
            }
        }

        /// <summary>
        /// Setter of DependencyProperty Culture.
        /// Only supported at DesignTime.
        /// </summary>
        [System.ComponentModel.DesignOnly(true)]
        public static void SetDesignCulture(DependencyObject obj, string value)
        {
            if (LocalizeDictionary.Instance.GetIsInDesignMode())
            {
                obj.SetValue(DesignCultureProperty, value);
            }
        }

        /// <summary>
        /// Dependencyproperty DesignCulture to set the Culture.
        /// Only supported at DesignTime.
        /// </summary>
        [System.ComponentModel.DesignOnly(true)]
        public static readonly DependencyProperty DesignCultureProperty =
            DependencyProperty.RegisterAttached("DesignCulture", typeof(string), typeof(LocalizeDictionary),
                                                new PropertyMetadata(SetCultureFromDependencyProperty));

        /// <summary>
        /// Callback function. Used to set the LocalizeDictionary.Culture if set in Xaml.
        /// Only supported at DesignTime.
        /// </summary>
        [System.ComponentModel.DesignOnly(true)]
        private static void SetCultureFromDependencyProperty(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (!LocalizeDictionary.Instance.GetIsInDesignMode())
            {
                return;
            }

            CultureInfo culture = null;

            try
            {
                culture = CultureInfo.GetCultureInfo((string)args.NewValue);
            }
            catch
            {
                if (LocalizeDictionary.Instance.GetIsInDesignMode())
                {
                    culture = DefaultCultureInfo;
                }
                else
                {
                    throw;
                }
            }

            if (culture != null)
            {
                LocalizeDictionary.Instance.Culture = culture;
            }
        }
        #endregion

        #region Constants
        /// <summary>
        /// Holds the default ResourceDictionary name
        /// </summary>
        public static string ResourcesName = "Resources";

        public static string AssemblyName = "";

        #endregion

        #region Members
        /// <summary>
        /// Holds the current choosen CultureInfo
        /// </summary>
        private CultureInfo m_Culture;

        #endregion

        #region Readonly Properties
        /// <summary>
        /// Holds the default CultureInfo to initialize the LocalizeDictionary.CultureInfo
        /// </summary>
        public static CultureInfo DefaultCultureInfo { get { return CultureInfo.InvariantCulture; } }
        #endregion

        #region Culture Properties
        /// <summary>
        /// Gets / sets the CultureInfo for localization.
        /// On set, OnCultureChanged is raised.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// You have to set LocDictionary.Culture first or 
        /// wait until System.Windows.Application.Current.MainWindow is created.
        /// Otherwise you will get an Exception.</exception>
        /// <exception cref="System.ArgumentNullException">thrown if Culture will be set to null</exception>
        public CultureInfo Culture
        {
            get
            {
                if (m_Culture == null)
                {
                    m_Culture = DefaultCultureInfo;
                    //try {
                    //    m_Culture = System.Windows.Application.Current.MainWindow.Language.GetEquivalentCulture();
                    //}
                    //catch (InvalidOperationException) {
                    //    throw;
                    //}
                    //catch (Exception ex) {
                    //    throw new InvalidOperationException("Cannot read CultureInfo from " +
                    //        "System.Windows.Application.Current.MainWindow.Language. " +
                    //        "You have to set LocDictionary.Culture first or wait until " + 
                    //        "System.Windows.Application.Current.MainWindow is created.", ex);
                    //}
                }
                return m_Culture;
            }
            set
            {
                // the cultureinfo cannot contains a null reference
                if (value == null) throw new ArgumentNullException("value");

                // Set the CultureInfo
                m_Culture = value;

                // Raise the OnCultureChanged event
                if (OnCultureChanged != null)
                {
                    OnCultureChanged();
                }
            }
        }

        /// <summary>
        /// Gets the Specific CultureInfo of the current culture.
        /// This can be used for format manners.
        /// If the Culture is an invariant CultureInfo, SpecificCulture will also return an invalriant CultureInfo.
        /// </summary>
        public CultureInfo SpecificCulture { get { return CultureInfo.CreateSpecificCulture(Culture.ToString()); } }
        #endregion

        #region Static Constructor
        /// <summary>
        /// Static Constructor
        /// </summary>
        private LocalizeDictionary()
        {
            if (string.IsNullOrEmpty(AssemblyName))
            {
                AssemblyName = GetAssemblyName(Assembly.GetExecutingAssembly());
            }

            ResourceManagerList = new Dictionary<string, ResourceManager>();
        }
        #endregion

        #region Event OnCultureChanged
        /// <summary>
        /// Get raised if the LocDictionary.Culture is changed.
        /// </summary>
        internal event Action OnCultureChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Holds the used ResourceManagers with their coresponding namespaces
        /// </summary>
        public Dictionary<string, ResourceManager> ResourceManagerList { get; private set; }
        #endregion

        #region GetAssemblyName
        /// <summary>
        /// Returns the AssemblyName of the passed assembly instance
        /// </summary>
        /// <param name="assembly">The Assembly where to get the name from</param>
        /// <returns>The Assembly name</returns>
        public static string GetAssemblyName(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            if (assembly.FullName == null) throw new NullReferenceException("assembly.FullName is null");
            return assembly.FullName.Split(',')[0];
        }
        #endregion

        #region GetIsInDesignMode
        /// <summary>
        /// Gets the status of the Application.Current.MainWindow designmode
        /// </summary>
        /// <returns>TRUE if in design mode, else FALSE</returns>
        public bool GetIsInDesignMode()
        {
            return System.ComponentModel.DesignerProperties.GetIsInDesignMode(this);
        }
        #endregion

        #region Static ParseKey
        /// <summary>
        /// Parses a key ([[Assembly:]Dict:]Key and return the parts of it.
        /// </summary>
        /// <param name="inKey">The key to parse.</param>
        /// <param name="outAssembly">The found or default assembly.</param>
        /// <param name="outDict">The found or default dictionary.</param>
        /// <param name="outKey">The found or default key.</param>
        public static void ParseKey(string inKey, out string outAssembly, out string outDict, out string outKey)
        {
            // reset the vars to null
            outAssembly = null;
            outDict = null;
            outKey = null;

            // its a assembly/dict/key pair
            if (!string.IsNullOrEmpty(inKey))
            {

                string[] split = inKey.Trim().Split(":".ToCharArray(), 3);

                // assembly:dict:key
                if (split.Length == 3)
                {
                    outAssembly = !string.IsNullOrEmpty(split[0]) ? split[0] : null;
                    outDict = !string.IsNullOrEmpty(split[1]) ? split[1] : null;
                    outKey = split[2];
                }
                    // dict:key
                    // assembly = ExecutingAssembly
                else if (split.Length == 2)
                {
                    outDict = !string.IsNullOrEmpty(split[0]) ? split[0] : null;
                    outKey = split[1];
                }
                    // key
                    // assembly = ExecutingAssembly
                    // dict = standard resourcedictionary
                else if (split.Length == 1)
                {
                    outKey = split[0];
                }
            }
            else
            {
                // if the passed value is null pr empty, throw an exception if in runtime
                if (!Instance.GetIsInDesignMode())
                {
                    throw new ArgumentNullException("inKey");
                }
            }
        }
        #endregion

        #region ResourceKeyExists
        /// <summary>
        /// Looks up the ResourceManagers for the searched resourceKey in the ResourceDictionary in the ResourceAssembly
        /// with an Invariant Culture.
        /// </summary>
        /// <param name="resourceAssembly">The resource assembly (e.g.: LocalizeExtension). NULL = Name of the executing assembly</param>
        /// <param name="resourceDictionary">The dictionary to look up (e.g.: ResHelp, Resources, ...). NULL = Name of the default resource file (Resources)</param>
        /// <param name="resourceKey">The key of the searched entry (e.g.: btnHelp, Cancel, ...). NULL = Exception</param>
        /// <returns>TRUE if the searched one is found, otherwise FALSE</returns>
        /// <exception cref="System.InvalidOperationException">If the ResourceManagers cannot be looked up</exception>
        /// <exception cref="System.ArgumentException">If the searched ResourceManager wasnt found (only in runtime)</exception>
        /// <exception cref="System.ArgumentException">If the resourceKey is null or empty</exception>
        public bool ResourceKeyExists(string resourceAssembly, string resourceDictionary, string resourceKey)
        {
            return ResourceKeyExists(resourceAssembly, resourceDictionary, resourceKey, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Looks up the ResourceManagers for the searched resourceKey in the ResourceDictionary in the ResourceAssembly
        /// with the passed culture. If the searched one does not exists with the passed culture, is will searched 
        /// until the invariant culture is used.
        /// </summary>
        /// <param name="resourceAssembly">The resource assembly (e.g.: LocalizeExtension). NULL = Name of the executing assembly</param>
        /// <param name="resourceDictionary">The dictionary to look up (e.g.: ResHelp, Resources, ...). NULL = Name of the default resource file (Resources)</param>
        /// <param name="resourceKey">The key of the searched entry (e.g.: btnHelp, Cancel, ...). NULL = Exception</param>
        /// <param name="culture"></param>
        /// <returns>TRUE if the searched one is found, otherwise FALSE</returns>
        /// <exception cref="System.InvalidOperationException">If the ResourceManagers cannot be looked up</exception>
        /// <exception cref="System.ArgumentException">If the searched ResourceManager wasnt found (only in runtime)</exception>
        /// <exception cref="System.ArgumentException">If the resourceKey is null or empty</exception>
        public bool ResourceKeyExists(string resourceAssembly, string resourceDictionary, string resourceKey, CultureInfo culture)
        {
            try
            {
                return GetResourceManager(resourceAssembly, resourceDictionary, resourceKey).GetObject(resourceKey, culture) != null;
            }
            catch
            {
                if (GetIsInDesignMode())
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }
        #endregion

        #region GetResourceManager
        /// <summary>
        /// Looks up in the cached ResourceManager list for the searched ResourceManager.
        /// </summary>
        /// <param name="resourceAssembly">The resource assembly (e.g.: LocalizeExtension). NULL = Name of the executing assembly</param>
        /// <param name="resourceDictionary">The dictionary to look up (e.g.: ResHelp, Resources, ...). NULL = Name of the default resource file (Resources)</param>
        /// <param name="resourceKey">The key of the searched entry (e.g.: btnHelp, Cancel, ...). NULL = Exception</param>
        /// <returns>The founded ResourceManager</returns>
        /// <exception cref="System.InvalidOperationException">If the ResourceManagers cannot be looked up</exception>
        /// <exception cref="System.ArgumentException">If the searched ResourceManager wasnt found</exception>
        /// <exception cref="System.ArgumentException">If the resourceKey is null or empty</exception>
        private ResourceManager GetResourceManager(string resourceAssembly, string resourceDictionary, string resourceKey)
        {

            if (resourceAssembly == null) resourceAssembly = GetAssemblyName(Assembly.GetExecutingAssembly());
            if (resourceDictionary == null) resourceDictionary = LocalizeDictionary.ResourcesName;
            if (string.IsNullOrEmpty(resourceKey))
            {
                throw new ArgumentNullException("resourceKey");
            }

            const string resManName = "ResourceManager";
            const string resConst = ".resources";
            PropertyInfo propInfo = null;
            MethodInfo methodInfo = null;
            Assembly assembly = null;
            ResourceManager resManager = null;
            string foundedResource = null;
            string resManagerNameToSearch = "." + resourceDictionary + resConst;
            string[] availableResources = null;
            const BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;

            if (ResourceManagerList.ContainsKey(resourceAssembly + resManagerNameToSearch))
            {
                resManager = ResourceManagerList[resourceAssembly + resManagerNameToSearch];
            }
            else
            {
                // if the assembly cannot be loaded, throw an exception
                try
                {
                    assembly = Assembly.Load(new AssemblyName(resourceAssembly));
                }
                catch
                {
                    throw;
                }

                // get all available resourcenames
                //availableResources = Assembly.GetExecutingAssembly().GetManifestResourceNames();
                availableResources = assembly.GetManifestResourceNames();

                // search for the best fitting resourcefile. pattern: ".{NAME}.resources"
                for (int i = 0; i < availableResources.Length; i++)
                {

                    if (availableResources[i].StartsWith(resourceAssembly + ".") &&
                        availableResources[i].EndsWith(resManagerNameToSearch))
                    {

                        // if a second one was found, exception
                        if (foundedResource != null) throw new ArgumentException(
                            string.Format("Ambiguous resourcename '{0}'!", resourceDictionary));
                        foundedResource = availableResources[i];
                        break;
                    }
                }

                // if no one was found, exception
                if (foundedResource == null)
                {
                    throw new ArgumentException(string.Format(
                                                    "No resource key with name '{0}' in dictionary '{1}' in assembly '{2}' founded! ({2}.{1}.{0})",
                                                    resourceKey, resourceDictionary, resourceAssembly));
                }

                // remove ".resources" from the end
                foundedResource = foundedResource.Substring(0, foundedResource.Length - resConst.Length);

                // Resources.{foundedResource}.ResourceManager.GetObject()
                //           ^^ prop-info      ^^ method get

                try
                {

                    // get the propertyinfo from resManager over the type from foundedResource
                    propInfo = assembly.GetType(foundedResource).GetProperty(resManName, bindingFlags);

                    // get the GET-method from the methodinfo
                    methodInfo = propInfo.GetGetMethod(true);

                    // get the static ResourceManager property
                    object resManObject = methodInfo.Invoke(null, null);

                    // cast it to a ResourceManager for better working with
                    resManager = ((ResourceManager)resManObject);

                }
                catch (Exception ex)
                {
                    // this error has to get thrown because this has to work
                    throw new InvalidOperationException("Cannot reslove the ResourceManager!", ex);
                }

                // Add the ResourceManager to the cachelist
                ResourceManagerList.Add(resourceAssembly + resManagerNameToSearch, resManager);
            }

            // return the founded ResourceManager
            return resManager;
        }
        #endregion

        #region GetLocalizedObject
        /// <summary>
        /// Returns an object from the passed dictionary with the given name.
        /// If a wrong TType is passed, no exception will get thrown (return obj as TType).
        /// </summary>
        /// <typeparam name="TType">Type of resulttype. Have to be a class.</typeparam>
        /// <param name="resourceAssembly">The Assembly where the Resource is located at</param>
        /// <param name="resourceDictionary">Name of the resource directory</param>
        /// <param name="resourceKey">The key for the resource</param>
        /// <param name="culture">The culture to get the right content</param>
        /// <returns>The founded object or NULL if not found or wront TType is given</returns>
        /// <exception cref="System.ArgumentNullException">resourceDictionary is null</exception>
        /// <exception cref="System.ArgumentException">resourceDictionary is empty</exception>
        /// <exception cref="System.ArgumentNullException">resourceName is null</exception>
        /// <exception cref="System.ArgumentException">resourceName is empty</exception>
        /// <exception cref="System.ArgumentException">Ambiguous resourcename {resourceDictionary}</exception>
        /// <exception cref="System.ArgumentException">No resource with name '{resourceDictionary}' founded</exception>
        public TType GetLocalizedObject<TType>(string resourceAssembly, string resourceDictionary,
                                               string resourceKey, CultureInfo culture) where TType : class
        {

            // Validation
            if (resourceAssembly == null) throw new ArgumentNullException("resourceAssembly");
            if (resourceAssembly == string.Empty) throw new ArgumentException("resourceAssembly is empty", "resourceAssembly");
            if (resourceDictionary == null) throw new ArgumentNullException("resourceDictionary");
            if (resourceDictionary == string.Empty) throw new ArgumentException("resourceDictionary is empty", "resourceDictionary");

            if (string.IsNullOrEmpty(resourceKey))
            {
                if (GetIsInDesignMode())
                {
                    return null;
                }
                else
                {
                    if (resourceKey == null)
                    {
                        throw new ArgumentNullException("resourceKey");
                    }
                    else if (resourceKey == string.Empty)
                    {
                        throw new ArgumentException("resourceKey is empty", "resourceKey");
                    }
                }
            }

            // declaring local ResourceManager
            ResourceManager resManager = null;

            // try to get the resouce manager
            try
            {
                resManager = GetResourceManager(resourceAssembly, resourceDictionary, resourceKey);
            }
            catch
            {
                // if an error occour, throw exception, if in runtime
                if (GetIsInDesignMode())
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            // gets the resourceobject with the choosen localization
            object retVal = resManager.GetObject(resourceKey, culture) as TType;

            // if the retVal is null, throw exception, if in runtime
            if (retVal == null)
            {
                if (GetIsInDesignMode())
                {
                    retVal = default(TType);
                }
                else
                {
                    throw new ArgumentException(string.Format(
                                                    "No resource key with name '{0}' in dictionary '{1}' in assembly '{2}' founded! ({2}.{1}.{0})",
                                                    resourceKey, resourceDictionary, resourceAssembly));
                }
            }

            // finally, return the searched object as type of the generic type
            return retVal as TType;
        }
        #endregion

        #region GetTopMostParent
        /// <summary>
        /// Gets the top most parent of an DependencyObject and returns it.
        /// </summary>
        /// <param name="dependencyTarget">The <c>DependencyObject</c> to get the top most parent from</param>
        /// <returns>The top most parent of the passed <c>DependencyObject</c></returns>
        [Obsolete]
        private DependencyObject GetTopMostParent(DependencyObject dependencyTarget)
        {
            // holds the current target to get the parent from
            DependencyObject oldParent = dependencyTarget;
            // holds the recived parent of the target (maybe null)
            DependencyObject newParent = null;

            // loop
            while (true)
            {
                // try to get the parent of the target
                newParent = LogicalTreeHelper.GetParent(oldParent);

                // if the new parent is null and the the current parent is an FrameworkElement,
                // try to get the templated parent of the FrameworkElement
                if (newParent == null && oldParent is FrameworkElement)
                {
                    // cast the current parent to a FrameworkElement
                    FrameworkElement fwElement = (FrameworkElement)oldParent;

                    // set the new parent to the templated parent (may be null)
                    newParent = fwElement.TemplatedParent;
                }

                // if the new parent is not null (DependencyObject parent or FrameworkElement templated parent),
                // continue the whole process to find the top most parent
                if (newParent != null)
                {
                    // set the old parent to the new parent
                    oldParent = newParent;
                    // set the new parent to null
                    newParent = null;
                    // loop
                    continue;
                }

                // break the loop if new parent is null
                break;
            }

            // return the old parent (new parent is null)
            return oldParent;
        }
        #endregion

        #region Expose Add/Remove EventListener
        /// <summary>
        /// Attach an WeakEventListener to the LocalizeDictionary
        /// </summary>
        /// <param name="listener">The listener to attach</param>
        public void AddEventListener(IWeakEventListener listener)
        {
            // calls AddListener from the inline WeakCultureChangedEventManager
            WeakCultureChangedEventManager.AddListener(listener);
        }

        /// <summary>
        /// Detach an WeakEventListener to the LocalizeDictionary
        /// </summary>
        /// <param name="listener">The listener to detach</param>
        public void RemoveEventListener(IWeakEventListener listener)
        {
            // calls RemoveListener from the inline WeakCultureChangedEventManager
            WeakCultureChangedEventManager.RemoveListener(listener);
        }
        #endregion

        #region Inline Class WeakCultureChangedEventManager
        /// <summary>
        /// This inline class is used to handle weak events to avoid memory leaks
        /// </summary>
        internal sealed class WeakCultureChangedEventManager : WeakEventManager
        {

            #region Static CurrentManager
            /// <summary>
            /// Holds the singleton instance of WeakCultureChangedEventManager
            /// </summary>
            private static WeakCultureChangedEventManager CurrentManager
            {
                get
                {
                    // store the type of this WeakEventManager
                    Type managerType = typeof(WeakCultureChangedEventManager);
                    // try to retrieve an existing instance of the stored type
                    WeakCultureChangedEventManager manager = (WeakCultureChangedEventManager)WeakEventManager.GetCurrentManager(managerType);
                    // if the manager does not exists
                    if (manager == null)
                    {
                        // create a new instance of WeakCultureChangedEventManager
                        manager = new WeakCultureChangedEventManager();
                        // add the new instance to the WeakEventManager manager-store
                        WeakEventManager.SetCurrentManager(managerType, manager);
                    }
                    // return the new / existing WeakCultureChangedEventManager instance
                    return manager;
                }
            }
            #endregion

            #region Static AddListener
            /// <summary>
            /// Adds an listener to the inner list of listeners
            /// </summary>
            /// <param name="listener">The listener to add</param>
            internal static void AddListener(IWeakEventListener listener)
            {
                // add the listener to the inner list of listeners
                WeakCultureChangedEventManager.CurrentManager.Listeners.Add(listener);
                // start / stop the listening process
                WeakCultureChangedEventManager.CurrentManager.StartStopListening();
            }
            #endregion

            #region Static RemoveListener
            /// <summary>
            /// Removes an listener from the inner list of listeners
            /// </summary>
            /// <param name="listener">The listener to remove</param>
            internal static void RemoveListener(IWeakEventListener listener)
            {
                // removes the listener from the inner list of listeners
                WeakCultureChangedEventManager.CurrentManager.Listeners.Remove(listener);
                // start / stop the listening process
                WeakCultureChangedEventManager.CurrentManager.StartStopListening();
            }
            #endregion

            #region Members
            /// <summary>
            /// Holds the inner list of listeners
            /// </summary>
            private WeakEventManager.ListenerList Listeners;
            /// <summary>
            /// Indicates, if the current instance is listening on the source event
            /// </summary>
            private bool IsListening = false;
            #endregion

            #region Constructor
            /// <summary>
            /// Creates a new instance of WeakCultureChangedEventManager
            /// </summary>
            private WeakCultureChangedEventManager()
            {
                // creates a new list and assign it to Listeners
                Listeners = new WeakEventManager.ListenerList();
            }
            #endregion

            #region StartStopListening
            /// <summary>
            /// This method starts and stops the listening process by attaching/detaching on the source event
            /// </summary>
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
            private void StartStopListening()
            {
                // if listeners are available and the listening process is stopped, start it
                if (Listeners.Count != 0)
                {
                    if (!IsListening)
                    {
                        StartListening(null);
                    }
                }
                    // otherwise if no listeners are available and the listening process is started, stop it
                else
                {
                    if (IsListening)
                    {
                        StopListening(null);
                    }
                }
            }
            #endregion

            #region StartListening
            /// <summary>
            /// This method starts the listening process by attaching on the source event
            /// </summary>
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
            protected override void StartListening(Object source)
            {
                if (!IsListening)
                {
                    Instance.OnCultureChanged += Instance_OnCultureChanged;
                    IsListening = true;
                }
            }
            #endregion

            #region StopListening
            /// <summary>
            /// This method stops the listening process by detaching on the source event
            /// </summary>
            /// <param name="source"></param>
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
            protected override void StopListening(Object source)
            {
                if (IsListening)
                {
                    Instance.OnCultureChanged -= Instance_OnCultureChanged;
                    IsListening = false;
                }
            }
            #endregion

            #region Instance_OnCultureChanged
            /// <summary>
            /// This method is called if the LocalizeDictionary.OnCultureChanged
            /// is called and the listening process is enabled
            /// </summary>
            private void Instance_OnCultureChanged()
            {
                // tells every listener in the list that the event is occurred
                base.DeliverEventToList(LocalizeDictionary.Instance, EventArgs.Empty, Listeners);
            }
            #endregion
        }
        #endregion
    }
}