using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace MyTcpListener
{
    public class Game
    {
        public int CurrentPlayer { get; set; }
        public int NumOfPlayers { get; set; }
        public uint GameStack { get; set; }
        public uint CurrentTurn { get; set; }
        public uint TempRaise { get; set; }
        public List<Player> Players = new List<Player>();
        public List<Card> CardsOnTable = new List<Card>();
        public Deck DeckA = new Deck();
        public Game() { }
        public Game(int nop)
        {
            CurrentPlayer = 0;
            NumOfPlayers = nop;
            CurrentTurn = 0;
            GameStack = 0;
        }
        public Game(List<Card> cards, List<Card> hand)
        {
            NumOfPlayers = 1;
            CardsOnTable = cards;
            Players.Add(new Player(0));
            Players[0].Hand = hand;
            
        }
        public Game(List<Card> hand)
        {
            NumOfPlayers = 1;
            Players.Add(new Player(0));
            Players[0].Hand = hand;
        }
        public void CreatePlayers()
        {
            for (int i = 0; i < NumOfPlayers; i++)
            {
                Players.Add(new Player(i));
            }

        }
        public void InitGame()
        {

            DealCards();
           // foreach (Player p in Players)
               // p.HandEval();
            List<Card> flop = Flop(ref DeckA);
            List<Card> turn = Turn(ref DeckA);
            List<Card> river = River(ref DeckA);

            CardsOnTable.AddRange(flop);
            CardsOnTable.AddRange(turn);
            CardsOnTable.AddRange(river);

        }
        public void GameTurn()
        {

            int[] tempArr = new int[NumOfPlayers]; // Temp array, breaks turn-loop after correct configuration is set
            for (int i = 0; i < NumOfPlayers; i++)
                tempArr[i] = 1;
            bool flag = false;
            //while (flag != true)
            //{
            GameInfo();
            CurrentPlayer = (CurrentTurn < NumOfPlayers) ? (int)CurrentTurn : 0;
            int temp = 0;
            //while (temp < NumOfPlayers)
            while (tempArr.Sum() > 0) // checking for correct config of temp array
            {
                WriteLine("\nGameStack: " + GameStack + "\n");
                // WriteLine("\nROUND: " + CurrentTurn + "\n");
                WriteLine("Player " + CurrentPlayer);
                WriteLine("Your Stack: " + Players[CurrentPlayer].PlayerStack);
                WriteLine("What do you do?");

                WriteLine("1- Check, 2- Fold, 3- Call,4- Raise");

                int decision = Convert.ToInt16(ReadLine());
                Players[CurrentPlayer].ToCall = TempRaise;
                Players[CurrentPlayer].MyTurn(decision);
                /* if (decision == 4)
                 {
                     GameStack += Players[CurrentPlayer].CurrentRaise;
                     tempArr[Players[CurrentPlayer].PlayerID] = 1;
                 }
                 */
                switch (decision)
                {
                    case 2:
                        tempArr[Players[CurrentPlayer].PlayerID] = -1;
                        break;
                    case 4:
                        GameStack += Players[CurrentPlayer].CurrentRaise;
                        tempArr[Players[CurrentPlayer].PlayerID] = 1;
                        TempRaise = Players[CurrentPlayer].CurrentRaise;
                        break;
                    default:
                        tempArr[Players[CurrentPlayer].PlayerID] = 0;
                        flag = true;
                        GameStack += Players[CurrentPlayer].ToCall;
                        break;
                }
                CurrentPlayer++;
                CurrentPlayer = (CurrentPlayer < NumOfPlayers) ? CurrentPlayer : 0;

                temp++;
                // }

                foreach (Player p in Players)
                    if (p.PlayerStack == 0)
                    {
                        flag = true;
                    }

                CurrentTurn++;

            }
            WriteLine("\nGameStack: " + GameStack + "\n");
            WriteLine("The End");


        }
        public void DealCards()
        {
            // Deal cards to players
            //Give Cards to players
            for (int i = 0; i < NumOfPlayers; i++)
            {
                Players[i].Hand = DeckA.DealCards();
            }

        }
        public void ResetDeck()
        {
            // ResetDECK
            DeckA.DeckCards.Clear();
            CardsOnTable.Clear();
            DeckA.DeckCards = DeckA.PopulateDeck();
            CurrentTurn += 1;


        }
        private List<Card> Flop(ref Deck deckrest)
        {
            //Select 3 cards from deck and add to table
            List<Card> flop = deckrest.DealCards(3);
            return flop;
        }
        public List<Card> River(ref Deck deckrest)
        {
            //Selec 1 card from deck and add to table
            List<Card> river = deckrest.DealCards(1);
            return river;
        }
        public List<Card> Turn(ref Deck deckrest)
        {
            // Select 1 card from deck and add to table
            List<Card> turn = deckrest.DealCards(1);
            return turn;
        }

        public void Showdown()
        {
            //Calculate winning hand
            //Take cards on the table and cards from players
            //score every single player and return winning player id
            //then assign to the winning player stack the gamestack



        }

        public void CheckWhoseTurnItIs()
        {

        }
        public void GameInfo()
        {
            WriteLine("\tCurrent round: " + CurrentTurn + "\nNumber of Players: " + NumOfPlayers + "\nGame Stack: " + GameStack);
            WriteLine("Cards on Table: ");
            foreach (Card i in CardsOnTable)
                i.ShowCard();
        }
        public void HandFlopEval(int stage = 3) // stage oznacza flop = 3, turn = 4 lub river=5
        {

            int ctnV = 0, ctnC = 0;
            foreach (Player p in Players)
            {
                if (p.Hand[0].Val == p.Hand[1].Val)
                    ctnV++;
                if (p.Hand[0].Suit == p.Hand[1].Suit)
                    ctnC++;
                foreach (Card c in p.Hand)
                {
                    for (int i = 0; i < stage; i++)
                    {
                        if (c.Val == CardsOnTable[i].Val)
                            ctnV++;
                        if (c.Suit == CardsOnTable[i].Suit)
                            ctnC++;
                    }
                }
                //Includes pair,set,quad
                p.ScoreAfterFlop = (p.Hand[0].Val + p.Hand[1].Val) * (ctnV + ctnC);
                switch (ctnC) // Includes flush
                {
                    case 3:
                        p.ScoreAfterFlop = (p.ScoreAfterFlop == 0) ? (p.Hand[0].Val+p.Hand[1].Val) : p.ScoreAfterFlop *= 1.25;
                        break;
                    case 4:
                        p.ScoreAfterFlop = (p.ScoreAfterFlop == 0) ? (p.Hand[0].Val + p.Hand[1].Val) : p.ScoreAfterFlop *= 2;
                        break;
                    case 5:
                        p.ScoreAfterFlop = (p.ScoreAfterFlop == 0) ? (p.Hand[0].Val + p.Hand[1].Val) : p.ScoreAfterFlop *= 5;
                        break;
                }


            }


        }
        public void HandTurnEval()
        {

        }
        public void HandRiverEval()
        {

        }


    }
}
