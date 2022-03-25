﻿using System;

namespace Scrutor.Decoration
{
    internal static class DecorationFactory
    {
        public static Decoration Create(Type serviceType, Type? decoratorType, Func<object, IServiceProvider, object>? decoratorFactory)
        {
            IDecorationStrategy strategy;

            if (serviceType.IsOpenGeneric() && decoratorType is not null &&  decoratorType.IsOpenGeneric())
            {
                strategy = new OpenGenericDecorationStrategy(serviceType, decoratorType);
            }
            else
            {
                strategy = new ClosedTypeDecorationStrategy(serviceType, decoratorType, decoratorFactory);
            }

            return new Decoration(strategy);
        }

        public static Decoration Create<TService>(Type? decoratorType, Func<object, IServiceProvider, object>? decoratorFactory)
            => new(new ClosedTypeDecorationStrategy(typeof(TService), decoratorType, decoratorFactory)); 
    }
}