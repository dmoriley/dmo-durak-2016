/*
 * Authors: David O'Riley, Travis Cowx, Tyler Calderone, Mitchell Hirst
 * Program Name: DurakGame.cs
 * Date Created: 03/14/2017
 * Description: A class file which keeps track of all the rules and actions of 
 *              a game of Durak. Decides which player (human or computer) starts a game, what cards can
 *              be played from both player's hand, and whose turn it is in-game. Also 
 *              handles filling hand when a round ends and deciding which play wins the game.
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;

namespace Durak
{
    public class DurakGame
    {
        #region EVENT HANDLERS

        public event EventHandler ComputerDefendUnsuccessful;
        public event EventHandler CardsDealt;
        public event EventHandler<MultipleEventArgs> CardsDealtToPlayer;
        public event EventHandler RolesSwitched;
        public event EventHandler RoundEnd;
        public event EventHandler ComputerEndAttack;
        public event EventHandler ComputerAddedMoreCards;
        public event EventHandler<Player> GameWon;
        public event EventHandler<PlayingCard> AttackNotAllowed;
        public event EventHandler<PlayingCard> DefenceNotSameSuit;
        public event EventHandler<PlayingCard> DefenceTooSmall;
        public event EventHandler<MultipleEventArgs> CardAddedToTable;

        #endregion


        #region PROPERTIES AND ATTRIBUTES

        /// <summary>
        /// The human player property
        /// </summary>
        private Player myHumanPlayer;
        public Player HumanPlayer
        {
            get { return myHumanPlayer; }
            set { myHumanPlayer = value; }
        }

        /// <summary>
        /// The computer player property
        /// </summary>
        private Player myComputerPlayer;
        public Player ComputerPlayer
        {
            get { return myComputerPlayer; }
            set { myComputerPlayer = value; }
        }

        /// <summary>
        /// The Card Deck property. The deck being used in the game
        /// </summary>
        private Deck myCardDeck;
        public Deck CardDeck
        {
            get { return myCardDeck; }
            set { myCardDeck = value; }
        }

        /// <summary>
        /// The Table Cards property. List of PlayingCards on the field
        /// </summary>
        private List<PlayingCard> myTableCards = new List<PlayingCard>();
        public List<PlayingCard> TableCards
        {
            get { return myTableCards; }
            set { myTableCards = value; }
        }

        /// <summary>
        /// The Trump Card property
        /// </summary>
        private PlayingCard myTrumpCard;
        public PlayingCard TrumpCard
        {
            get { return myTrumpCard; }
            set { myTrumpCard = value; }
        }

        /// <summary>
        /// The defending player property
        /// </summary>
        private Player myDefender;
        public Player Defender
        {
            get { return myDefender; }
            set { myDefender = value; }
        }

        /// <summary>
        /// The attacking player property
        /// </summary>
        private Player myAttacker;       
        public Player Attacker
        {
            get { return myAttacker; }
            set { myAttacker = value; }
        }

        /// <summary>
        /// Property to check if the deck is empty
        /// </summary>
        private bool myDeckEmpty = false;
        public bool DeckEmpty
        {
            get { return myDeckEmpty; }
            set { myDeckEmpty = value; }
        }

        #endregion


        #region CONSTRUCTORS

        /// <summary>
        /// DurakGame - Constructor
        /// Set the human player's name, and the lowest rank in the deck
        /// By default the player is "Player1" and the min rank is Six as the smallest deck
        /// possible in a Durak Game
        /// </summary>
        /// <param name="humanPlayerName">The human player's name</param>
        /// <param name="minRank">The lowest card rank available in the deck</param>
        public DurakGame(string humanPlayerName = "Player1", CardRank minRank = CardRank.Six)
        {
            HumanPlayer = new Player(humanPlayerName);
            ComputerPlayer = new Player("Computer");
            CardDeck = new Cards.Deck(minRank, true);
        }

        #endregion


        #region GAME METHODS

        /// <summary>
        /// StartGame(CardRank?) - Method
        /// Start the game by shuffling the deck, determining the trump card.
        /// Proceed to deal the cards to each player and determine who will attack first
        /// by determining who has the lowest value Trump.
        /// </summary>
        /// <param name="minRank">Make Sure the minRank has a value</param>
        public void StartGame(CardRank? minRank = null)
        {
            //Occurs only when the player restarts the game
            if (minRank.HasValue)
                CardDeck = new Deck(minRank.Value, true);

            //Change the ace value from 1 to 14
            UpdateAceValue();

            //Shuffled the deck twice
            CardDeck.Shuffle();
            CardDeck.Shuffle();

            //Draw the top card to make the trump
            TrumpCard = CardDeck.DrawNextCard();

            //Deal the cards, one by one to each player
            DealInitialCards();

            //Determine which player has the lowest trump
            DetermineFirstRoles();

            if (Attacker.Equals(ComputerPlayer))
                PlayerAttack();
        }

        #region CARD METHODS

        /// <summary>
        /// Update the value of the Aces to 14 due to Aces being high in Durak.
        /// </summary>
        public void UpdateAceValue()
        {
            foreach(PlayingCard card in CardDeck.Cards)
            {
                if (card.Rank == CardRank.Ace)
                    card.Value = 14;
            }
        }

        /// <summary>
        /// Deal Cards to the players in order determined by who
        /// was set to the first and second draw
        /// Called After dealing hands
        /// </summary>
        private void DealCards(Player firstDealt, Player secondDealt)
        {
            DealCardsToPlayer(firstDealt);
            DealCardsToPlayer(secondDealt);

            if (CardsDealt != null) //Raise the cards dealt event
                CardsDealt(this, EventArgs.Empty);
        }

        /// <summary>
        /// DealInitialCards() - Method
        /// Deals the 6 initial cards to the 2 players
        /// </summary>
        private void DealInitialCards()
        {
            //Deal the cards
            for(int i = 0; i < 6; i++)
            {
                HumanPlayer.AddCard(CardDeck.DrawNextCard());
                ComputerPlayer.AddCard(CardDeck.DrawNextCard());
            }
        }

        /// <summary>
        /// DealCardsToPlayer(Player) - Method
        /// Deal cards to the player that is passed until their count is 6
        /// </summary>
        /// <param name="player">The player that the cards are being dealt to</param>
        private void DealCardsToPlayer(Player player)
        {
            //Check to see if there is cards remaining in the deck
            if (CardDeck.CardsRemaining() != 0)
            {
                //Check to see if the player has less than 6 cards
                if(player.CardCount() < 6)
                {
                    //Variable for the loop set to 1 less than the # of cards they own
                    int loopStart = player.CardCount();

                    /* Deal the cards to the player
                     * Checks if the card count of the player isn't more than 6
                     * and that the deck has cards in it to deal
                     */                   
                    for(; loopStart < 6 && CardDeck.CardsRemaining() != 0; loopStart++)
                    {
                        //Draw the next card into the player's hand
                        PlayingCard nextCard = CardDeck.DrawNextCard();
                        player.AddCard(nextCard);
                        player.SortCards();     //Sort the cards

                        //Pass the player and hte card being added to the even as an argument
                        if (CardsDealtToPlayer != null)
                            CardsDealtToPlayer(this, new MultipleEventArgs(player, nextCard));
                    }
                }
            }
        }


        /// <summary>
        /// ClearAllCards() - Method
        /// Remove all the cards from the game
        /// </summary>
        public void ClearAllCards()
        {
            ComputerPlayer.Cards.Clear();
            HumanPlayer.Cards.Clear();
            TableCards.Clear();
        }

        #endregion

        #region PLAYER ROLE METHODS

        /// <summary>
        /// DetermineFirstRoles() - Method
        /// Determine who has the lowest rank trump card. Set the player's isDefending
        /// value to false, meaning they are attacking.
        /// </summary>
        private void DetermineFirstRoles()
        {
            //Cards that will hold the lowest cards the players are holding
            PlayingCard humanCard = LowestCardOfSuit(HumanPlayer.Cards, TrumpCard);
            PlayingCard computerCard = LowestCardOfSuit(ComputerPlayer.Cards, TrumpCard);

            //Make sure there is a card, possible because player may not have the trump
            if((object)humanCard != null)
            {
                //Check if the computer has a trump card
                if((object)computerCard != null)
                {
                    //If both have a trump card. Check who's is higher.
                    //The higher card is defender
                    if(humanCard > computerCard)
                    {
                        //Set attacker and defender
                        Defender = HumanPlayer;
                        Attacker = ComputerPlayer;
                    }
                    else    //Human player has a lower card, they are attacker
                    {
                        //Set attacker and defender
                        Defender = ComputerPlayer;
                        Attacker = HumanPlayer;
                    }
                }
                else    //Human player has a card, but the computer doesn't.
                {
                    //Set attacker and defender
                    Defender = ComputerPlayer;
                    Attacker = HumanPlayer;
                }
            }
            else if((object) computerCard != null) //Check if the computer player has a card
            {
                //Set the computer to the attacker
                Defender = HumanPlayer;
                Attacker = ComputerPlayer;
            }
            else   //Neither players have a trump card
            {
                //Pick the attacker at random
                Random rand = new Random();
                int number = rand.Next(1, 3);   //Get a random number between 1 and 2
                //If the number is 1, human is the defender
                if(number == 1)
                {
                    Defender = HumanPlayer;
                    Attacker = ComputerPlayer;
                }
                else    //It's a 2, so human is attacker
                {
                    Defender = ComputerPlayer;
                    Attacker = HumanPlayer;
                }
            }
        }
        
        /// <summary>
        /// SwitchRoles() - Method
        /// Switch the roles of the defender and the attacker
        /// </summary>
        private void SwitchRoles()
        {
            //If the human player is the defender
            if(Defender.Equals(HumanPlayer))
            {
                Defender = ComputerPlayer;  //Set the computer to the defender
                Attacker = HumanPlayer;
            }
            else    //Else the Computer was the defender
            {
                Defender = HumanPlayer;     //Set the human to the defender
                Attacker = ComputerPlayer;
            }

            //Raise the roles switched event
            if(RolesSwitched != null)
                RolesSwitched(this, EventArgs.Empty);
        }

        #endregion

        //TODO: CHECK THE RULES
        #region ATTACKING AND DEFENDING

        /// <summary>
        /// PlayerAttack(PlayingCard) - Method
        /// Decide which player is attacking. PlayingCard is default to null
        /// due to the computer not using this card.
        /// </summary>
        /// <param name="card">PlayingCard passed to attack</param>
        public void PlayerAttack(PlayingCard card = null)
        {
            //Determine which player is the attacker and call the appropriate method
            //If the computer is attacking
            if(Attacker.Equals(ComputerPlayer))
            {
                ComputerAttack();
            }
            else   //The human player is attacking
            {
                //Make sure the card passed has a value
                if ((object)card == null)
                    throw new ArgumentNullException("Attack Card is null for " + HumanPlayer.Name, "PlayerAttack() Null Error");
                else
                    HumanAttack(card);
            }
        }

        /// <summary>
        /// HumanAttack(PlayingCard, boolean) - Method
        /// Human player picjs a card to play. Returns true if the human
        /// player can play that card
        /// </summary>
        /// <param name="card">The card that the player is playing</param>
        /// <param name="computerAcceptingCards">True of the computer is accepting the cards</param>
        public void HumanAttack(PlayingCard card, bool computerAcceptingCards = false)
        {
            //First make sure the card is playable for attack
            if(TableCards.Count == 0) //No cards on the table, anything can be player
            {
                ConfirmAttack(card);
            }
            else    //There are cards on the table
            {
                List<int> tableCardsValue = GetTableCardsValues();

                //Check if the card has a cooresponding card on the table
                //Thus allowing the card to be played
                if(tableCardsValue.Contains(card.Value))
                {
                    ConfirmAttack(card, computerAcceptingCards);
                }
                else   //Card is not on the table
                {
                    //Raise the attack not allowed event
                    if (AttackNotAllowed != null)
                        AttackNotAllowed(this, card);
                }
            }
        }

        /// <summary>
        /// ComputerAttack() - Method
        /// Computer chooses to play their best card in the scenario while attacking
        /// </summary>
        private void ComputerAttack()
        {
            PlayingCard choice = null;      //Card of the attack

            //Initial attack to start the round
            if(TableCards.Count == 0)
            {
                //Get the lowest card from the computer's deck
                choice = GetLowestCard(ComputerPlayer.Cards);
            }
            else   //Has to play a card from the values on the table
            {
                //Get the values of the cards on the table
                List<PlayingCard> playables = GetPlayableCards(ComputerPlayer.Cards);

                if(playables.Count != 0)    //Computer has a card they can play
                {
                    choice = playables[0];
                }
                else    //Else they don't have any cards to play and end their attack
                {
                    //Raise the ComputerEndAttack event
                    if (ComputerEndAttack != null)
                        ComputerEndAttack(this, EventArgs.Empty);
                }
            }

            //Confirm the attack by the computer
            if ((object)choice != null)
                ConfirmAttack(choice);
        }

        /// <summary>
        /// PlayerDefend(PlayingCard) - Method
        /// Determine which player is defending, call their method
        /// </summary>
        /// <param name="card"></param>
        public void PlayerDefend(PlayingCard card = null)
        {
            //Check if the computer is defending, otherwise it's the human player
            if (Defender.Equals(ComputerPlayer))
                ComputerDefend();
            else
                HumanDefend(card);
        }

        /// <summary>
        /// HumanDefend(PlayingCard) - Method
        /// Determines if the card places is playable
        /// </summary>
        /// <param name="defendingCard">The card the player is playing</param>
        private void HumanDefend(PlayingCard defendingCard)
        {
            //The card the human player has to beat to defend
            PlayingCard cardToBeat = TableCards[(TableCards.Count) - 1];

            //Check if the two cards are of the same suit
            if(PlayingCard.SameSuit(defendingCard, cardToBeat))
            {
                //If the defending card is greater than the card to beat, confirm the defence
                //Otherwise the card can't beat
                if(defendingCard > cardToBeat)
                {
                    ConfirmDefence(defendingCard);
                }
                else
                {
                    //Raise the DefenceTooSmall event
                    if (DefenceTooSmall != null)
                        DefenceTooSmall(this, defendingCard);
                }
            }
            //Otherwise check if the card is a trump card
            else if(PlayingCard.SameSuit(defendingCard, TrumpCard))
            {
                //Was a trump and the other wasn't, so it can be played
                ConfirmDefence(defendingCard);
            }
            //They aren't the same suit, nor is it the trump card
            else
            {
                //Raise the DefenceNotSameSuit event
                if (DefenceNotSameSuit != null)
                    DefenceNotSameSuit(this, defendingCard);
            }
        }

        /// <summary>
        /// ComputerDefend() - Method
        /// Handles the computer player defending against
        /// and attack.
        /// </summary>
        private void ComputerDefend()
        {
            //Get a list of playable cards for defending
            List<PlayingCard> playables = EligibleDefenceCards(ComputerPlayer.Cards, TableCards[(TableCards.Count - 1)]);

            //If there is a playable, play the lowest only
            if(playables.Count != 0)
            {
                ConfirmDefence(playables[0]);
            }
            //There is not a playable card of the same suit & greater value
            else
            {
                playables.Clear();  //Clear the playables, just in case

                //Check if the computer has trump cards usable in defence
                playables = EligibleDefenceCards(ComputerPlayer.Cards);

                //If there is a playbale card, play the lowest
                if (playables.Count != 0)
                    ConfirmDefence(playables[0]);
                else
                {
                    //Raise the ComputerDefendUnsuccessful event
                    if (ComputerDefendUnsuccessful != null)
                        ComputerDefendUnsuccessful(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// ConfirmAttack(PlayingCard, boolean) - Method
        /// Confirm the attack of the attacker by playing that
        /// card to the table cards and removing it from their hand
        /// </summary>
        /// <param name="card">The card that is being played</param>
        /// <param name="computerAcceptingCards">Determines if the computer is accepting the card</param>
        public void ConfirmAttack(PlayingCard card, bool computerAcceptingCards = false)
        {
            TableCards.Add(card);           //Add the card to the table cards
            Attacker.RemoveCard(card);      //Remove that card from the player's hand

            //Raise the CardAddedToTable event
            if (CardAddedToTable != null)
                CardAddedToTable(this, new MultipleEventArgs(Attacker, card));

            //Check to see if the player has any cards left
            if(Attacker.CardCount() != 0)
            {
                //Check to see if the computer is accepting the cards
                //Can't accept cards if they are defending
                if(!computerAcceptingCards)
                {
                    //Making sure if the computer is the defender
                    if (Defender.Equals(ComputerPlayer))
                        ComputerDefend();
                }
            }
            else
            {
                //Raise the GameWon event with the attacker as the winner
                if (GameWon != null)
                    GameWon(this, Attacker);
            }
        }

        /// <summary>
        /// ConfirmDefence(PlayingCard) - Method
        /// Add the defending card to the table, remove the card from the defender's hand
        /// </summary>
        /// <param name="card"></param>
        public void ConfirmDefence(PlayingCard card)
        {
            TableCards.Add(card);       //Add the card to the table
            Defender.RemoveCard(card);  //Remove the card from the player's hand

            //Raise the CardAddedToTable event
            if (CardAddedToTable != null)
                CardAddedToTable(this, new MultipleEventArgs(Defender, card));

            //Make sure there is cards in the defender's hand
            if(Defender.CardCount() != 0)
            {
                //Check if the computer is the attacker
                if (Attacker.Equals(ComputerPlayer))
                    ComputerAttack();
            }
            else    //Defender has no more cards left and has won the game
            {
                if (GameWon != null)
                    GameWon(this, Defender);
            }
        }

        /// <summary>
        /// HumanAcceptCards() - Method
        /// The defender accepts all the cards on the table.
        /// Used when human accepts the cards.
        /// </summary>
        public void HumanAcceptCards()
        {
            //Gets the value of the cards on the table
            List<int> values = GetTableCardsValues();

            //List to hold the cards to be removed from the computer's hand
            List<PlayingCard> cardToRemove = new List<PlayingCard>();

            //Go through each vlue of the card on the table
            foreach(int i in values)
            {
                //Go through each card in the computer player's hand
                foreach(PlayingCard card in ComputerPlayer.Cards)
                {
                    //If they match the same suit and aren't the TrumpSuit because strategy
                    //Add the card to the table
                    if(card.Value == i && !PlayingCard.SameSuit(card, TrumpCard))
                    {
                        //Add the card to the table
                        TableCards.Add(card);
                        cardToRemove.Add(card);

                        //Raise the CardAddedToTable event
                        if (CardAddedToTable != null)
                            CardAddedToTable(this, new MultipleEventArgs(ComputerPlayer, card));
                    }
                }
            }

            //Check to see if there are cards to be removed from the player's hand
            if(cardToRemove.Count != 0)
            {
                //Remove the cards from the computer player's hand
                foreach(PlayingCard card in cardToRemove)
                {
                    ComputerPlayer.Cards.Remove(card);
                }

                //Raise ComputerAddedMoreCards event
                if (ComputerAddedMoreCards != null)
                    ComputerAddedMoreCards(this, EventArgs.Empty);

                //If the computer has no cards left in their hand
                if(ComputerPlayer.Cards.Count == 0)
                {
                    //Raise the GameWon event
                    if (GameWon != null)
                        GameWon(this, ComputerPlayer);
                }
            }
        }

        /// <summary>
        /// EndRound(boolean) - Method
        /// Handles the round being finished. Clears the table, and sends
        /// the cards to the player's hand if they accepted it
        /// </summary>
        /// <param name="cardsToGarbage">True of nobody is accepting the cards</param>
        public void EndRound(bool cardsToGarbage = true)
        {
            //If the cards are being cleared because nobody accepted
            if(cardsToGarbage)
            {
                TableCards.Clear(); //Clear the table
                SwitchRoles();      //Switch the roles of the player
            }
            //A player has accepted the cards
            else
            {
                //For each card in the playing field
                foreach(PlayingCard tableCard in TableCards)
                {
                    //Add the cards to the defender's hand
                    Defender.Cards.Add(tableCard);
                    Defender.Cards.Sort();  //Sort the cards

                    //Pass the player and the card being added to the event as the arguements
                    if (CardsDealtToPlayer != null)
                        CardsDealtToPlayer(this, new MultipleEventArgs(Defender, tableCard));
                }
                TableCards.Clear();     //Clear the table
                Defender.Cards.Sort();  //Sort the cards
            }

            //If the deck still has cards, raise the DealCards event
            if (!DeckEmpty)
                DealCards(Attacker, Defender);

            //Raise the RoundEnd event
            if (RoundEnd != null)
                RoundEnd(this, EventArgs.Empty);

        }
        #endregion

        #region CARD ANALYSIS

        /// <summary>
        /// LowestCardOfSuit(List PlayingCard, PlayingCard) - Method
        /// Compares a list of cards to a card, determines the lowest
        /// of the matching suit
        /// </summary>
        /// <param name="cards">Cards being compared</param>
        /// <param name="compareCard">The card being compared to the suit of</param>
        /// <returns></returns>
        private PlayingCard LowestCardOfSuit(List<PlayingCard> cards, PlayingCard compareCard)
        {
            //List to hold all the cards of the same suit
            List<PlayingCard> cardsOfSuit = new List<PlayingCard>();

            //Loop through the cards and add cards of the same suit to the list
            foreach(PlayingCard card in cards)
            {
                if (PlayingCard.SameSuit(card, compareCard))
                    cardsOfSuit.Add(card);
            }

            //If there are cards in the same suit list
            if(cardsOfSuit.Count != 0)
            {
                cardsOfSuit.OrderBy(x => x.Value);
                return cardsOfSuit[0];
            }

            //Return null if there are no cards of the same suit
            return null;
        }

        /// <summary>
        /// GetTableCardsValues() - Method
        /// Returns a list of integer values for the cards rank values that
        /// are currently in play
        /// </summary>
        /// <returns></returns>
        private List<int> GetTableCardsValues()
        {
            //List to hold the values
            List<int> returnList = new List<int>();

            //Add each value to the list that is on the table
            foreach (PlayingCard card in TableCards)
                returnList.Add(card.Value);

            //Trim down list to return only unique values
            return returnList.Distinct().ToList();
        }

        /// <summary>
        /// SortByRank(List PlayingCard) - Method
        /// Returns a list of cards that has been sorted by rank
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public List<PlayingCard> SortByRank(List<PlayingCard> cards)
        {
            List<PlayingCard> tempDeck = new List<PlayingCard>();

            //Loop through each possible card rank value
            for(int j = 1; j <= 14; j++)
            {
                //Loop through each card in the deck
                foreach (PlayingCard i in cards)
                {
                    //Check to see if the card value is the same as rank being checked
                    if(i.Value == j)
                    {
                        //Add that card to the temp deck
                        tempDeck.Add(i);
                    }
                }
            }

            //Return the sorted deck
            return tempDeck;
        }

        /// <summary>
        /// GetLowestCard(List PlayingCard) - Method
        /// Sorts a list of playingcards by their rank, lowest to highest
        /// </summary>
        /// <param name="cards">PlayingCards to be sorted</param>
        /// <returns>The lowest card in the deck</returns>
        private PlayingCard GetLowestCard(List<PlayingCard> cards)
        {
            List<PlayingCard> tempDeck = SortByRank(cards);     //Sort the cards by their rank values
            return tempDeck[0];     //Return the sorted deck
        }

        /// <summary>
        /// GetPlayableCards(List PlayingCard) - Method
        /// Get the playable cards from a player's hand. Compares the field cards
        /// with their hand
        /// </summary>
        /// <param name="cards">The cards in a player's hand</param>
        /// <returns>A list of playable cards</returns>
        private List<PlayingCard> GetPlayableCards(List<PlayingCard> cards)
        {
            List<int> values = GetTableCardsValues();
            List<PlayingCard> outDeck = new List<PlayingCard>();

            //Go through each playable value
            foreach (int i in values)
            {
                //Check to see if any of the cards match the playable value
                foreach(PlayingCard j in cards)
                {
                    //If they are the same value, add them to the outdeck
                    if (j.Value == i)
                        outDeck.Add(j);
                }
            }

            //Sort the deck and return it if it's not empty
            if (outDeck.Count != 0)
                outDeck = SortByRank(outDeck);

            return outDeck;     //Return the playable cards
        }

        /// <summary>
        /// EligibleDefenceCards(List PlayingCard, PlayingCard) - Method
        /// Get the cards in the player's hand that can beat the card passed
        /// </summary>
        /// <param name="cards">Cards in the player's hand</param>
        /// <param name="cardToBeat">The card that must be beat</param>
        /// <returns>A list of playable cards</returns>
        private List<PlayingCard> EligibleDefenceCards(List<PlayingCard> cards, PlayingCard cardToBeat)
        {
            //List of playable cards
            List<PlayingCard> outCards = new List<PlayingCard>();

            //If the current card beingcompared has a value that is greater then the
            //card to beat and is the same suit, add it to the eligible cards list
            foreach(PlayingCard i in cards)
            {
                if (i > cardToBeat && PlayingCard.SameSuit(i, cardToBeat))
                    outCards.Add(i);
            }

            //If the list has values, sort it by the rank
            if (outCards.Count != 0)
                outCards = SortByRank(outCards);

            return outCards;        //Return a list of eligible defence cards
        }

        /// <summary>
        /// EligibleDefenceCards(List PlayingCard) - Overloaded Method
        /// Checks to see if there are any cards on defence that are trump cards.
        /// Only adds them if the trump card value is lowest than 10
        /// </summary>
        /// <param name="cards">List of cards to be compared</param>
        /// <returns>A list of cards that can be played</returns>
        private List<PlayingCard> EligibleDefenceCards(List<PlayingCard> cards)
        {
            //List of eligible cards for defence
            List<PlayingCard> outCards = new List<PlayingCard>();

            foreach(PlayingCard i in cards)
            {
                //If the value of the card is less than 10 and is the same value of the trump card
                //Add is to the eligible cards list
                if(i.Value < 10 && PlayingCard.SameSuit(i, TrumpCard))
                {
                    //Make sure that the card value is greater than the card being played
                    //if the card is a trump card
                    if(PlayingCard.SameSuit(TableCards[(TableCards.Count - 1)], TrumpCard))
                    {
                        if (i.Value > TableCards[(TableCards.Count - 1)].Value)
                            outCards.Add(i);
                    }
                    else  //Card isn't a trump card to add this card
                    {
                        outCards.Add(i);
                    }
                }
            }

            //Sort by rank if the outcards contains cards
            if (outCards.Count != 0)
                outCards = SortByRank(outCards);

            return outCards;        //Return the list of eligible cards
        }
        #endregion

        #endregion
    }
}
