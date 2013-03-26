using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EF_test1.Models;
using System.Data.Objects.DataClasses;
using System.Data;

namespace EF_test1.Business
{
    public class BusinessController
    {
        public int Insert<T>(T _entity) where T : EntityObject
        {
            int intAffected = 0;
            using (PermissionDBEntities context = new PermissionDBEntities())
            {
                context.AddObject(typeof(T).Name, _entity);
                intAffected = context.SaveChanges();
                if (intAffected > 0)
                    context.AcceptAllChanges();
            }
            return intAffected;
        }

        public int InsertList<T>(List<T> _entities) where T : EntityObject
        {
            int intAffected = 0;

            using (PermissionDBEntities context = new PermissionDBEntities())
            {
                foreach(T entity in _entities)
                    context.AddObject(typeof(T).Name, entity);
                intAffected = context.SaveChanges();
                if (intAffected > 0)
                    context.AcceptAllChanges();
            }
            return intAffected;
        }

        public int Delete<T>(string name,string id) where T : EntityObject
        {
            int intAffected = 0;

            using (PermissionDBEntities context = new PermissionDBEntities())
            {
                EntityKey entityKey = new EntityKey(
                    "PermissionDBEntities." + typeof(T).Name, name, id);
                var mResult = context.GetObjectByKey(entityKey);
                if (mResult != null)
                    context.DeleteObject(mResult);

                intAffected = context.SaveChanges();
                if (intAffected > 0)
                    context.AcceptAllChanges();
            }
            return intAffected;
        }
    }
}
