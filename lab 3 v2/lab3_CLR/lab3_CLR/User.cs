using System;

namespace lab3_CLR
{
    class User
    {
        private string login;
        private string password;
        private long maxRepositorySize;
        private long repositorySize;
        private DateTime creationDate;

        public User()
        {
        }

        public User(string login, string password, long maxRepositorySize, long repositorySize, DateTime creationDate)
        {
            this.login = login;
            this.password = password;
            this.maxRepositorySize = maxRepositorySize;
            this.repositorySize = repositorySize;
            this.creationDate = creationDate;
        }

        public string Login
        {
            get { return this.login; }
        }

        public string Password
        {
            get { return this.password; }
        }

        public long MaxRepositorySize
        {
            get { return this.maxRepositorySize; }
        }

        public long RepositorySize
        {
            get { return this.repositorySize; }
            set { this.repositorySize = value; }
        }

        public DateTime CreationDate
        {
            get { return this.creationDate; }
        }



    }
}
