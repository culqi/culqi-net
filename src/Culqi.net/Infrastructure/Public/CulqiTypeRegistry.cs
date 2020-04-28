using Culqi.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;

namespace Culqi.Infrastructure.Public
{
    public static class CulqiTypeRegistry
    {
        public static readonly IReadOnlyDictionary<string, Type> ObjectsToTypes = new ReadOnlyDictionary<string, Type>(new Dictionary<string, Type>
        {
            { "card", typeof(Card) },
            { "charge", typeof(Charge) },
            { "customer", typeof(Customer) },
            { "event", typeof(Event) },            
            { "order", typeof(Order) },
            { "plan", typeof(Plan) },
            { "refund", typeof(Refund) },
            { "subscription", typeof(Subscription) },
            { "token", typeof(Token) }            
        });

        public static Type GetConcreteType(Type potentialType, string objectValue)
        {
            if (potentialType != null && !potentialType.GetTypeInfo().IsInterface)
            {
                // Potential type is already a concrete type, return it.
                return potentialType;
            }

            Type concreteType = null;

            if (!string.IsNullOrEmpty(objectValue) &&
                ObjectsToTypes.TryGetValue(objectValue, out concreteType))
            {
                // Found a concrete type matching the value of the `object` key, check if it's
                // compatible with the interface.
                if (potentialType.GetTypeInfo().IsAssignableFrom(concreteType.GetTypeInfo()))
                {
                    return concreteType;
                }
            }

            return null;
        }
    }
}
