using System;
using System.Collections.Generic;

class NewInstance
{
    /// <summary>
    /// Function takes a 'new' object and either returns the instance or add the instance to the list.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="newObject"></param>
    /// <param name="list"></param>
    /// <returns> Null or Instance of newObject</returns>
    public static T1 Instantiate<T1>(T1 newObject)
    {
        if (!newObject.Equals(null))
        {
           return newObject;
        }
       return default(T1);
    }
}
