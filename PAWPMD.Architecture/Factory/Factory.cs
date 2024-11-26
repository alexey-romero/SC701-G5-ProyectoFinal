using PAWPMD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAWPMD.Architecture.Factory
{
    /// <summary> ///
    /// Defines a generic factory interface that mandates the implementation of a creation method.
    /// </summary> /// 
    /// <typeparam name="T">The type of object the factory creates, constrained to class types.</typeparam>
    public interface IFactory<T> where T : class
    {
        /// <summary> /// 
        /// Creates an instance of type T based on the specified type. 
        /// </summary> 
        /// <param name="type">A string representing the type of object to create.</param> /// <returns>An instance of type T.</returns>
        T Create(string type);
    }

 /// <summary> 
 /// Abstract base class for a factory that implements the IFactory interface. 
 /// </summary> 
 /// <typeparam name="T">The type of object the factory creates, constrained to class types.</typeparam>
public abstract class Factory<T> : IFactory<T> where T : class
    {
        public abstract T Create(string type);
    }
}
