using System;
using System.Configuration;

namespace lab3_CLR
{
    public class AuthenticationService
    {
        public bool IsLogged
        {
            get;
            private set;
        }

        public string UserLogin
        {
            get;
            private set;
        }

        public void Authenticate(string login, string password)
        {
            string _password = ConfigurationManager.AppSettings.Get(login);

            if (_password != null && _password == password)
            {
                IsLogged = true;
                UserLogin = login;
                Console.WriteLine("You are logged");
            }
            else
            {
                Console.WriteLine("Invalid login or password");
            }
        }
    }
}
