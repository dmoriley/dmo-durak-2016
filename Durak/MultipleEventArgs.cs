/*
 * Authors: David O'Riley, Travis Cowx, Tyler Calderone, Mitchell Hirst
 * Program Name: MultipleEventArgs.cs
 * Date Created: 02/02/2017
 * Description: A class used with any event to be able to pass multiple objects to an event.
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public class MultipleEventArgs : EventArgs
    {
        /// <summary>
        /// automatic property for an array of objects the is used
        /// as the event generic type to be able to pass multiple objects
        /// in an event.
        /// </summary>
        public object[] EventObjects { get; set; }

        /// <summary>
        /// constructor that accepts an array of objects
        /// </summary>
        /// <param name="args"></param>
        public MultipleEventArgs(params object[] args)
        {
            EventObjects = args;
        }
    }
}
