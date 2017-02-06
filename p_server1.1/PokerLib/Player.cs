using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace MyTcpListener
{
    public class Player
    {
        public uint PlayerID { get; set; }
        public uint PlayerStack { get; set; }
        public uint CurrentRaise { get; set; }
        public uint ToCall { get; set; }
        public uint Investment { get; set; }
        public List<Card> Hand = new List<Card>();
        public double Score { get; set; }
        public double ScoreAfterFlop { get; set; }
        public double ScoreAfterTurn { get; set; }
        public double ScoreAfterRiver { get; set; }
        public Player(int id, uint stack = 1000)
        {
            PlayerID = (uint)id;
            PlayerStack = stack;
            Investment = 0;
        }
        public List<Card> GetCards(List<Card> hand)
        {
            return hand;
        }
        public void MyTurn(int move)
        {
            switch (move)
            {
                case 1:
                    Check();
                    break;
                case 2:
                    Fold();
                    break;
                case 3:
                    Write("x");
                    ToCall = ToCall - Investment;
                    Call(ToCall);
                    break;
                case 4:
                    WriteLine("How much?");
                    uint raiseValue = Convert.ToUInt16(ReadLine());
                    Raise(raiseValue);
                    break;
            }
        }
        public void Check()
        {

        }
        public void Fold()
        {
            Hand.Clear();
        }
        public uint Call(uint value)
        {
            PlayerStack -= value;
            Investment += value;
            return value;
        }
        public uint Raise(uint value)
        {
            PlayerStack -= value;
            CurrentRaise = value;
            Investment += value;
            return value;
        }
        public void ShowHand()
        {
            foreach (Card c in Hand)
                c.ShowCard();
        }
        public void ShowInfo()
        {
            WriteLine("\nPlayer_id: " + PlayerID + "\nStack: " + PlayerStack);
            ShowHand();
            WriteLine("HandScore: " + Score + "\n");
        }
        public void HandEval()
        {
            int pair = 2;

            if (Hand[0].Val == Hand[1].Val)
                Score = (Hand[0].Val * 2) * pair;
            else if ((Hand[0].Val == Hand[1].Val - 1) || Hand[0].Val - 1 == Hand[1].Val) // Check if cards are consecutive, possible straight
            {
                Score = (Hand[0].Val + Hand[1].Val) * 1.25;
                if (Hand[0].Suit == Hand[1].Suit)
                    Score *= 1.5; // Chance for flush and straight
            }
            else if (Hand[0].Suit == Hand[1].Suit)
            {
                Score = 1.5 * (Hand[0].Val + Hand[1].Val); // Chance for flush
            }
            else
            {
                Score = Hand[0].Val + Hand[1].Val;
            }

        }
    }

}
