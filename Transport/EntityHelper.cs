using System;
using System.Linq;
using System.Reflection;

namespace TkMiddleware.Helpers
{    
    public class EntityHelper
    {
        private const string @namespace = "TkMiddleware.DataObjects";
        private const string suffix = "Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

        public EntityHelper() {}

        public string GetEntityTypeName(int version, string entityName, bool dynamicVersion = true)
        {
            bool found = false;
            string fqdn = string.Empty;

            while (!found && version > 0)
            {
                string className = string.Format("{0}.{1}_v{2}", @namespace, entityName, version);
                fqdn = string.Format("{0}, {1}, {2}", className, @namespace, suffix);
                try
                {
                    var obj = Activator.CreateInstance(Type.GetType(fqdn));
                    found = true;
                }
                catch
                {
                    if (dynamicVersion)
                        version--;
                    else
                        version = 0;
                }
            }

            if (!found)
                return string.Empty;            
            else            
                return fqdn;            
        } 

        public dynamic GetEntityInstance(string entityTypeName)
        {
            Type entityType = Type.GetType(entityTypeName);
            dynamic entityObject = Activator.CreateInstance(entityType);

            return entityObject;
        }

        public dynamic MapJsonToObject(dynamic jsonObject, string entityTypeName)
        {
            Type entityType = Type.GetType(entityTypeName);
            dynamic destObj = Activator.CreateInstance(entityType);
            dynamic data = System.Web.Helpers.Json.Decode(jsonObject.ToString(), entityType);

            var sourceType = data.GetType();
            var destType = destObj.GetType();
            var destProperties = destType.GetProperties();
            var sourceProperties = entityType.GetProperties();
            sourceProperties = sourceType.GetProperties();

            foreach (PropertyInfo propertyInfo in destProperties)
            {
                if (propertyInfo.CanRead && sourceProperties.Any(m =>
                    m.CanRead == true && m.CanWrite && m.Name == propertyInfo.Name))
                {
                    var sourceValue = sourceProperties.Single(m =>
                        m.Name == propertyInfo.Name).GetValue(data);
                    propertyInfo.SetValue(destObj, sourceValue);
                }
            }

            return destObj;
        }
    }
}