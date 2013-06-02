using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DiContainer
{
    public class Container
    {
        public Container()
        {
            Types = new Dictionary<Type, Type>();
        }

        protected readonly IDictionary<Type, Type> Types;

        public void Register<T>()
        {
            Register(typeof(T), typeof(T));
        }

        public void Register<T, U>()
        {
            Register(typeof(T), typeof(U));
        }

        public void Register(Type baseType, Type implementationType)
        {
            Types[baseType] = implementationType;
        }

        public T Resolve<T>() where T : class
        {
            return Resolve(typeof(T)) as T;
        }

        public object Resolve(Type type)
        {
            if (!Types.ContainsKey(type))
            {
                throw new Exception("Type not registered: " + type.Name);
            }

            var resultType = Types[type];
            var constructors = resultType.GetConstructors();

            foreach (var c in constructors)
            {
                var parameters = c.GetParameters();
                if(parameters.Length == 0 || c.GetParameters().All(a => Types.ContainsKey(a.ParameterType)))
                {
                    var parameterInstances = new List<object>();
                    foreach (var p in parameters)
                    {
                        var instance = Resolve(p.ParameterType);
                        parameterInstances.Add(instance);
                    }
                    return Activator.CreateInstance(resultType, parameterInstances.ToArray());
                }
            }

            throw new Exception("Could not resolve dependencies for " + resultType.Name);
        }

        public void Release()
        {
            // Nothing as of yet to do
        }
    }
}
