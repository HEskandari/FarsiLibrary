using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.Design;

namespace FarsiLibrary.Win.Design
{
    public class DateTimeCollectionEditor : CollectionEditor
    {
        public DateTimeCollectionEditor(Type type)
            : base(type)
        {
        }

        protected override Type CreateCollectionItemType()
        {
            return typeof(DateTime);
        }

        protected override Type[] CreateNewItemTypes()
        {
            return new Type[] { typeof(DateTime) };
        }

        protected override object CreateInstance(Type itemType)
        {
            DateTime dt = (DateTime)base.CreateInstance(itemType);
            return dt;
        }
    }
}
