using System;
using System.Collections.Generic;
using System.Collections;
using FarsiLibrary.Win.Events;
using FarsiLibrary.Win.Enums;

namespace FarsiLibrary.Win.BaseClasses
{
    public class DateTimeCollection : IList<DateTime>
    {
        #region Events

        /// <summary>
        /// Raised when the collection is changed.
        /// </summary>
        public event EventHandler<CollectionChangedEventArgs> CollectionChanged;

        #endregion

        #region Fields

        private readonly List<DateTime> data = new List<DateTime>(); 

        #endregion

        #region Protected Methods
        
        /// <summary>
        /// Fires CollectionChanged event.
        /// </summary>
        protected virtual void OnCollectionChanged(CollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, e);
        }

        #endregion

        #region ICollection<DateTime> Members

        public void Add(DateTime item)
        {
            data.Add(item);
            OnCollectionChanged(new CollectionChangedEventArgs(CollectionChangeType.Add));
        }

        public void AddRange(DateTime[] items)
        {
            data.AddRange(items);
            OnCollectionChanged(new CollectionChangedEventArgs(CollectionChangeType.Add));
        }

        public void Clear()
        {
            data.Clear();
            OnCollectionChanged(new CollectionChangedEventArgs(CollectionChangeType.Clear));
        }

        public bool Contains(DateTime item)
        {
            return data.Contains(item);
        }

        public void CopyTo(DateTime[] array, int arrayIndex)
        {
            foreach (DateTime dt in this)
            {
                array.SetValue(dt, arrayIndex);
                arrayIndex++;
            }
        }

        public int Count
        {
            get { return data.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(DateTime item)
        {
            bool result = data.Remove(item);
            if (result)
                OnCollectionChanged(new CollectionChangedEventArgs(CollectionChangeType.Remove));

            return result;
        }

        public void RemoveAt(int index)
        {
            data.RemoveAt(index);
            OnCollectionChanged(new CollectionChangedEventArgs(CollectionChangeType.Remove));
        }

        public void RemoveAll(Predicate<DateTime> match)
        {
            data.RemoveAll(match);
            OnCollectionChanged(new CollectionChangedEventArgs(CollectionChangeType.Remove));
        }

        public DateTime this[int index]
        {
            get { return data[index]; }
            set 
            { 
                data[index] = value;
                OnCollectionChanged(new CollectionChangedEventArgs(CollectionChangeType.Other));
            }
        }

        #endregion

        #region IEnumerable<DateTime> Members

        public IEnumerator<DateTime> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        #endregion

        #region IList<DateTime> Members

        public int IndexOf(DateTime item)
        {
            return data.IndexOf(item);
        }

        public void Insert(int index, DateTime item)
        {
            data.Insert(index, item);
            OnCollectionChanged(new CollectionChangedEventArgs(CollectionChangeType.Add));
        }

        #endregion
    }
}
