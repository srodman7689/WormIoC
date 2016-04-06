using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace WormIoC {

	public static class TypeExtensions
	{
		public delegate object Activator(params object[] args);

		private static TDelegate DoGetActivator<TDelegate>(ConstructorInfo ctor)
		where TDelegate : class
		{
			var ctorParams = ctor.GetParameters();
			var paramExp = Expression.Parameter(typeof(object[]), "args");

			var expArr = new Expression[ctorParams.Length];

			for (var i = 0; i < ctorParams.Length; i++)
			{
				var ctorType = ctorParams[i].ParameterType;
				var argExp = Expression.ArrayIndex(paramExp, Expression.Constant(i));
				var argExpConverted = Expression.Convert(argExp, ctorType);
				expArr[i] = argExpConverted;
			}

			var newExp = Expression.New(ctor, expArr);
			var lambda = Expression.Lambda<TDelegate>(newExp, paramExp);
			return lambda.Compile();
		}
		
		public static Activator GetActivator(this ConstructorInfo ctor) 
		{
			return DoGetActivator<Activator>(ctor);
		}

		public static Activator GetActivator(this Type type)
		{
			var ci = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault();
			return ci.GetActivator();
		}
	}
}
