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
        public int ExecuteSQL(string commandText)
        {
            using (PermissionDBEntities context = new PermissionDBEntities())
            {
                return context.ExecuteStoreCommand(commandText);
            }
        }
        /// <summary>
        /// 直接执行sql语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public IList<T> Get<T>(string commandText) where T : EntityObject
        {
            using (PermissionDBEntities context = new PermissionDBEntities())
            {
                IList<T> list = context.ExecuteStoreQuery<T>(commandText).ToList();
                return list;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filed"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public T Get<T>(string filed,string where) where T : EntityObject
        {
            using (PermissionDBEntities context = new PermissionDBEntities())
            {
                EntityKey entityKey = new EntityKey(
                    "PermissionDBEntities." + typeof(T).Name, filed, where);
                return context.GetObjectByKey(entityKey) as T;
            }
        }

        public List<T> Get<T>(Func<T,bool> predicate) where T : EntityObject
        {
            using (PermissionDBEntities context = new PermissionDBEntities())
            {
                var returnEntity = context.CreateObjectSet<T>().Where(predicate).ToList();

                return returnEntity as List<T>;
            }
        }
        public List<T> Get<T>(Func<T, bool> predicate,string inculdePath) where T : EntityObject
        {
            using (PermissionDBEntities context = new PermissionDBEntities())
            {
                var returnEntity = context.CreateObjectSet<T>().Include(inculdePath).Where(predicate).ToList();

                return returnEntity as List<T>;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_entity"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_entities"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update<T>(T entity, string name ) 
             where T : EntityObject
         {
             int intAffected = 0;
            Type type = typeof(T);
            using (PermissionDBEntities context = new PermissionDBEntities())
            {
                //获取实体的Id属性
                System.Reflection.PropertyInfo property = type.GetProperty(name);
                object unique_id = property.GetValue(entity, null);
                //根据Id获取上下文中的对应实体
                EntityKey entityKey = new EntityKey("PermissionDBEntities."
                      + type.Name, name, unique_id);
                var objResult = context.GetObjectByKey(entityKey);
                //更新实体属性
                if (objResult != null)
                    context.ApplyCurrentValues<T>(type.Name, entity);

                intAffected = context.SaveChanges();
                if (intAffected > 0)
                    context.AcceptAllChanges();
            }
            return intAffected;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update<T1, T2>(T1 entity)
              where T1 : EntityObject
              where T2 : EntityObject
          {
              int intAffected = 0;
              Type typeT1 = typeof(T1);
              Type typeT2 = typeof(T2);

              using (PermissionDBEntities context = new PermissionDBEntities())
              {
                  System.Reflection.PropertyInfo property = typeT1.GetProperty("Id");
                  object id = property.GetValue(entity, null);

                  //根据软件Id建立EntityKey对象
                  EntityKey entityKey = new EntityKey("PermissionDBEntities."
                       + typeT1.Name, "Id", id);
                  //根据EntityKey查找对应对象
                  T1 objT1 = context.GetObjectByKey(entityKey) as T1;
                  //在上下文中更新当前对象
                  if (objT1 != null)
                      context.ApplyCurrentValues<T1>(typeT1.Name, entity);

                  //获取外键属性
                  System.Reflection.PropertyInfo propertyInfo = typeT1.GetProperty(typeT2.Name);

                  //在一对多关键时更新导航属性
                  var T2List = propertyInfo.GetValue(entity, null)
                         as EntityCollection<T2>;
                  if (T2List != null)
                  {
                      foreach (var obj in T2List.ToList())
                      {
                          var oldEntity = context.GetObjectByKey(obj.EntityKey);
                          if (oldEntity != null)
                              context.ApplyCurrentValues<T2>(typeT2.Name, obj);
                      }
                  }

                  //在多对一，一对一关系时更新导航属性
                  var objT2 = propertyInfo.GetValue(entity, null) as T2;
                  if (objT2 != null)
                  {
                      var oldEntity = context.GetObjectByKey(objT2.EntityKey);
                      if (oldEntity != null)
                          context.ApplyCurrentValues<T2>(typeT2.Name, objT2);
                  }

                  intAffected = context.SaveChanges();
                  if (intAffected > 0)
                      context.AcceptAllChanges();
              }
              return intAffected;
          }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
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
        public int Delete<PrimaryKeyEntity, ForeignKeyEntity>(string name, string id) where PrimaryKeyEntity : EntityObject where ForeignKeyEntity:EntityObject
        {
            int intAffected = 0;

            using (PermissionDBEntities context = new PermissionDBEntities())
            {
                //根据软件Id建立EntityKey对象
                EntityKey entityKey = new EntityKey(
                    "PermissionDBEntities." + typeof(PrimaryKeyEntity).Name, name, id);
                //根据EntityKey查找对应对象
                PrimaryKeyEntity mResult = context.GetObjectByKey(entityKey) as PrimaryKeyEntity;
                //根据FKEntity加载导航属性
                System.Reflection.PropertyInfo proper=typeof(PrimaryKeyEntity).GetProperty(
                    typeof(ForeignKeyEntity).Name);
                EntityCollection<ForeignKeyEntity> FkList = proper.GetValue(
                    entityKey, null
                    ) as EntityCollection<ForeignKeyEntity>;

                //提前加载导航属性,for 级联删除
                if (FkList != null)
                    FkList.Load();

                if (mResult != null)
                    context.DeleteObject(mResult);

                intAffected = context.SaveChanges();
                if (intAffected > 0)
                    context.AcceptAllChanges();
            }
            return intAffected;
        }
        /// <summary>
        /// 
        /// Delete<T>(x=>x.Id==2) 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Delete<T>(Func<T, bool> predicate) where T : EntityObject
        { 
            int intAffected = 0;

            using (PermissionDBEntities context = new PermissionDBEntities())
            {
                //根据输入的委托查找数据
                var delList = context.CreateObjectSet<T>().Where(predicate);
                //若存在数据，删除有关数据
                if (delList.Count() > 0)
                {
                    foreach (var obj in delList)
                        context.DeleteObject(obj);

                    intAffected = context.SaveChanges();

                    if (intAffected > 0)
                        context.AcceptAllChanges();
                }
            }
            return intAffected;
        }
        /// <summary>
        /// 
        /// Delete<PrimaryKeyEntity,ForeignKeyEntity>(x=>x.Id==2)
        /// </summary>
        /// <typeparam name="PrimaryKeyEntity"></typeparam>
        /// <typeparam name="ForeignKeyEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Delete<PrimaryKeyEntity, ForeignKeyEntity>(Func<PrimaryKeyEntity, bool> predicate)
            where PrimaryKeyEntity : EntityObject
            where ForeignKeyEntity : EntityObject
        { 
            int intAffected = 0;

            using (PermissionDBEntities context = new PermissionDBEntities())
            {
                //根据输入的委托查找数据
                var delList = context.CreateObjectSet<PrimaryKeyEntity>().Where(predicate);
                //若存在数据，删除有关数据
                if (delList.Count() > 0)
                {
                    foreach (var obj in delList)
                    {
                        //根据FKEntity加载导航属性
                        System.Reflection.PropertyInfo proper = typeof(PrimaryKeyEntity).GetProperty(
                            typeof(ForeignKeyEntity).Name);
                        //反射获取对象
                        EntityCollection<ForeignKeyEntity> FkList = proper.GetValue(
                            obj, null
                        ) as EntityCollection<ForeignKeyEntity>;

                        //提前加载导航属性,for 级联删除
                        if (FkList != null)
                            FkList.Load();
                        //删除
                        context.DeleteObject(obj);
                    }

                    intAffected = context.SaveChanges();

                    if (intAffected > 0)
                        context.AcceptAllChanges();
                }
            }
            return intAffected;
        }
    }
}
