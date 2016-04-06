using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WormIoC
{
    class RegisteredObject
    {
        private Type concreteType;
        private readonly Container container;
        private Lifecycle lifecycle;
        private TypeExtensions.Activator compiledCtor;
        public object Instance { get; private set; }

        public RegisteredObject(Container container, Type concreteType, Lifecycle lifecycle)
        {
            this.container = container;
            this.concreteType = concreteType;
            this.lifecycle = lifecycle;
            this.compiledCtor = null;
        }

		public object GetInstance()
		{
			if (lifecycle == Lifecycle.Singleton && Instance != null)
				return Instance;

			var ctorParams = ResolveConstructorParameters(concreteType);

		    if (compiledCtor == null)
		    {
		        compiledCtor = concreteType.GetActivator();
		    }
		    
		    var instance = compiledCtor(ctorParams.ToArray());

			if (lifecycle == Lifecycle.Singleton)
				Instance = instance;

			return instance;
		}

        public IEnumerable<object> ResolveConstructorParameters(Type resolvedType)
        {
            var constructorInfo = resolvedType.GetConstructors()[0];
            foreach (var parameter in constructorInfo.GetParameters())
            {
                yield return container.Retrieve(parameter.ParameterType);
            }
        }
    }
}
