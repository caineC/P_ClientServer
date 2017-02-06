using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace MyTcpListener
{

    public class Deck
    {
        private static Random r = new Random();
        private int numCards = 52;
        public List<Card> DeckCards = new List<Card>();

        public Deck()
        {
            DeckCards = PopulateDeck();
        }

        public List<Card> PopulateDeck()
        {
            cardsuit[] suitsArray = { cardsuit.Club, cardsuit.Diamond, cardsuit.Heart, cardsuit.Spade };
            List<Card> DeckOfCards = new List<Card>();
            for (int i = 2; i < 15; i++)
                for (int j = 0; j < 4; j++)
                    DeckOfCards.Add(new Card(i, suitsArray[j]));
            return DeckOfCards;
        }
        public void ShowDeck()
        {
            foreach (Card i in DeckCards)
                i.ShowCard();
            WriteLine(DeckCards.Count);
        }
        public void ShuffleDeck()
        {
            Card temp;
            for (int i = 0; i < 100; i++)
            {
                int card = r.Next(52);
                int card1 = r.Next(52);
                temp = DeckCards[card1];
                DeckCards[card1] = DeckCards[card];
                DeckCards[card] = temp;
            }
        }
        public List<Card> DealCards(int numofcards = 2)
        {

            List<Card> Hand = new List<Card>();
            for (int i = 0; i < numofcards; i++)
            {
                int cardindex = r.Next(DeckCards.Count);
                Hand.Add(DeckCards[cardindex]);
                DeckCards.Remove(DeckCards[cardindex]);
            }
            return Hand;

        }
    }
}

