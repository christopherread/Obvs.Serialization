using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Obvs.Configuration;

namespace Obvs.Serialization.NetJson
{
    public class NetJsonMessageDeserializerFactory : IMessageDeserializerFactory
    {
        public IEnumerable<IMessageDeserializer<TMessage>> Create<TMessage, TServiceMessage>(Func<Assembly, bool> assemblyFilter = null, Func<Type, bool> typeFilter = null)
            where TMessage : class
            where TServiceMessage : class
        {
            return MessageTypes.Get<TMessage, TServiceMessage>(assemblyFilter, typeFilter)
                .Select(type => typeof(NetJsonMessageDeserializer<>).MakeGenericType(type))
                .Select(deserializerGeneric => Activator.CreateInstance(deserializerGeneric) as IMessageDeserializer<TMessage>)
                .ToArray();
        }
    }
}