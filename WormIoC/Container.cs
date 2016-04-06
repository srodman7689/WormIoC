using System;
using System.Collections.Generic;

namespace WormIoC
{
    public class Container
    {
        private readonly IDictionary<Type, RegisteredObject> _container = new Dictionary<Type, RegisteredObject>();

        public void Register<TInterface, TObject>(Lifecycle lifecycle = Lifecycle.Trasient)
        {
            _container[typeof(TInterface)] = new RegisteredObject(this, typeof(TObject), lifecycle);
        }

        public TInterface Retrieve<TInterface>()
        {
            return (TInterface)Retrieve(typeof(TInterface));
        }

        public object Retrieve(Type interfaceType)
        {
            RegisteredObject resolvedObject;
            if (!_container.TryGetValue(interfaceType, out resolvedObject)) 
            {
                throw new Exception(string.Format("The type {0} has not been registered", interfaceType));
            }

            return resolvedObject.GetInstance();
        }
    }
}
