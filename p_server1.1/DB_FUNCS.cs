using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTcpListener
{
    public class DB_FUNCS
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=c:\P_DecisionSoft\p_server1.1\p_server1.1\db_MTL.mdf;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
        public static void InsertHand(List<string> cards, int score)
        {
            string insertStatement = "INSERT INTO CardsDB (HandCard1,HandCard2,Score) VALUES (@HandCard1,@HandCard2,@Score)";
            SqlConnection connection = DB_FUNCS.GetConnection();
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@HandCard1", cards[0]);
            insertCommand.Parameters.AddWithValue("@HandCard2", cards[1]);
            insertCommand.Parameters.AddWithValue("@Score", score);
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

        }
        public static void InsertHandFlop(List<string> cards, int score)
        {
            string insertStatement = "INSERT INTO CardsDB (HandCard1,HandCard2,FlopCard1,FlopCard2,FlopCard3,Score) VALUES (@HandCard1,@HandCard2,@FlopCard1,@FlopCard2,@FlopCard3,@Score)";
            SqlConnection connection = DB_FUNCS.GetConnection();
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@HandCard1", cards[0]);
            insertCommand.Parameters.AddWithValue("@HandCard2", cards[1]);
            insertCommand.Parameters.AddWithValue("@FlopCard1", cards[2]);
            insertCommand.Parameters.AddWithValue("@FlopCard2", cards[3]);
            insertCommand.Parameters.AddWithValue("@FlopCard3", cards[4]);
            insertCommand.Parameters.AddWithValue("@Score", score);
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

        }
        public static void InsertHandTurn(List<string> cards, int score)
        {
            string insertStatement = "INSERT INTO CardsDB (HandCard1,HandCard2,FlopCard1,FlopCard2,FlopCard3,Turn,Score) VALUES (@HandCard1,@HandCard2,@FlopCard1,@FlopCard2,@FlopCard3,@Turn,@Score)";
            SqlConnection connection = DB_FUNCS.GetConnection();
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@HandCard1", cards[0]);
            insertCommand.Parameters.AddWithValue("@HandCard2", cards[1]);
            insertCommand.Parameters.AddWithValue("@FlopCard1", cards[2]);
            insertCommand.Parameters.AddWithValue("@FlopCard2", cards[3]);
            insertCommand.Parameters.AddWithValue("@FlopCard3", cards[4]);
            insertCommand.Parameters.AddWithValue("@Turn", cards[5]);
            insertCommand.Parameters.AddWithValue("@Score", score);
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
        public static void InsertHandRiver(List<string> cards, int score)
        {
            string insertStatement = "INSERT INTO CardsDB (HandCard1,HandCard2,FlopCard1,FlopCard2,FlopCard3,Turn,River,Score) VALUES (@HandCard1,@HandCard2,@FlopCard1,@FlopCard2,@FlopCard3,@Turn,@River,@Score)";
            SqlConnection connection = DB_FUNCS.GetConnection();
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@HandCard1", cards[0]);
            insertCommand.Parameters.AddWithValue("@HandCard2", cards[1]);
            insertCommand.Parameters.AddWithValue("@FlopCard1", cards[2]);
            insertCommand.Parameters.AddWithValue("@FlopCard2", cards[3]);
            insertCommand.Parameters.AddWithValue("@FlopCard3", cards[4]);
            insertCommand.Parameters.AddWithValue("@Turn", cards[5]);
            insertCommand.Parameters.AddWithValue("@River", cards[6]);
            insertCommand.Parameters.AddWithValue("@Score", score);
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
        public static int Insert(List<string> cards, int score)
        {
            int return_code = 0;
            int cardsNum = cards.Count;
            switch(cardsNum)
            {
                case 2:
                    InsertHand(cards,score);
                    return_code = 1;
                    break;
                case 5:
                    InsertHandFlop(cards, score);
                    return_code = 1;
                    break;
                case 6:
                    InsertHandTurn(cards, score);
                    return_code = 1;
                    break;
                case 7:
                    InsertHandRiver(cards, score);
                    return_code = 1;
                    break;
                default:
                    Console.WriteLine("Insert nie wypalil");
                    break;
            }
          
            Console.WriteLine("DONE");
            return return_code;
        }
    }
}
