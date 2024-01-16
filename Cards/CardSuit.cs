/*
 * Authors: David O'Riley, Travis Cowx, Tyler Calderone, Mitchell Hirst
 * Program Name: CardSuit.cs
 * Date Created: 03/15/2017
 * Description: Enumeration to store the 4 suits of a card.
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    /// <summary>
    /// CardSuit enumeration
    /// Used to represent the 4 "French" suits of a standard playing card deck
    /// </summary>
    public enum CardSuit : byte
    {
        Spades,
        Hearts,
        Diamonds,
        Clubs
    }
}
