//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace EF_test1.Models
{
    public partial class users
    {
        #region Primitive Properties
    
        public virtual string userid
        {
            get;
            set;
        }
    
        public virtual string username
        {
            get;
            set;
        }
    
        public virtual string passwordword
        {
            get;
            set;
        }
    
        public virtual string name
        {
            get;
            set;
        }
    
        public virtual string sex
        {
            get;
            set;
        }
    
        public virtual string birthday
        {
            get;
            set;
        }
    
        public virtual string dept
        {
            get;
            set;
        }
    
        public virtual string postion
        {
            get;
            set;
        }
    
        public virtual string postiondetail
        {
            get;
            set;
        }
    
        public virtual string createuser
        {
            get;
            set;
        }
    
        public virtual string createuserid
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> createdate
        {
            get;
            set;
        }
    
        public virtual Nullable<int> sortnum
        {
            get;
            set;
        }
    
        public virtual string isvalid
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<userrole> userrole
        {
            get
            {
                if (_userrole == null)
                {
                    var newCollection = new FixupCollection<userrole>();
                    newCollection.CollectionChanged += Fixupuserrole;
                    _userrole = newCollection;
                }
                return _userrole;
            }
            set
            {
                if (!ReferenceEquals(_userrole, value))
                {
                    var previousValue = _userrole as FixupCollection<userrole>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixupuserrole;
                    }
                    _userrole = value;
                    var newValue = value as FixupCollection<userrole>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixupuserrole;
                    }
                }
            }
        }
        private ICollection<userrole> _userrole;

        #endregion
        #region Association Fixup
    
        private void Fixupuserrole(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (userrole item in e.NewItems)
                {
                    item.users = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (userrole item in e.OldItems)
                {
                    if (ReferenceEquals(item.users, this))
                    {
                        item.users = null;
                    }
                }
            }
        }

        #endregion
    }
}