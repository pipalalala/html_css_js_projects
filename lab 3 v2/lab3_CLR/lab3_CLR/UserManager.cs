using System.Collections.Generic;

namespace lab3_CLR
{
    public class UserManager
    {
        public string CurrentUserLogin
        {
            get;
            private set;
        }

        public bool IsUserLogged
        {
            get;
            private set;
        }

        private AuthenticationService authenticationService = new AuthenticationService();

        public void Authenticate(List<string> listOfSubCommands)
        {
            authenticationService.Authenticate(listOfSubCommands[1], listOfSubCommands[3]);

            IsUserLogged = authenticationService.IsLogged;
            CurrentUserLogin = authenticationService.UserLogin;
        }
    }
}