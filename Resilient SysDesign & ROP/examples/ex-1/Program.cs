using System;

namespace Program
{
    public class Connection
    {
        public void Open() { }
    }
    public class ConnectionError : Exception { }

    public class Application
    {
        public static void Initialize(Connection c) { }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var dbConnection = new Connection();
            dbConnection.Open();
            // failure not considered
            // application enters invalid state, fails to boot with no information
            Application.Initialize(dbConnection);
        }
    }

    public class ResilientProgram
    {
        public static void Main(string[] args)
        {
            var dbConnection = new Connection();
            try
            {
                // rudimentary fail-safe using try-catch
                dbConnection.Open();
                Application.Initialize(dbConnection);
            }
            catch (ConnectionError e)
            {
                // resiliency - program does not go into invalid state, we can control what happens here
                Console.WriteLine("Sorry, this service is currently unavailable");
            }
        }
    }
}