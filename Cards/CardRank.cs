/*
 * Authors: David O'Riley, Travis Cowx, Tyler Calderone, Mitchell Hirst
 * Program Name: CardRank.cs
 * Date Created: 02/15/2017
 * Description: Enumeration to hold the thirteen types of card ranks and a joker.
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
    /// CardRank enumeration
    /// Used to represent the 13 ranks of a standard "French" playing card deck, plus Joker.
    /// </summary>
    public enum CardRank : byte
    {
        Ace = 1,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Joker
    }
}
