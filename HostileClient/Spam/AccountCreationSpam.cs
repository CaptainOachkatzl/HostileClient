using System;
using System.Diagnostics;
using System.Text;
using XSLibrary.Cryptography.AccountManagement;

namespace HostileClient.Spam
{
    class AccountCreationSpam : ISpam
    {
        string fileName = "accounts.txt";
        string directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/";
        FileUserBase dataBase;

        string username = "username";
        string contact = "contact";
        byte[] password = Encoding.ASCII.GetBytes("password456");
        byte[] wrongPassword = Encoding.ASCII.GetBytes("wrong789");

        public override void Initialize()
        {
            dataBase = new FileUserBase(directory, fileName);
            dataBase.EraseAllAccounts();
        }

        protected override void SpamAction(int index)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Restart();
            dataBase.AddAccount(new AccountCreationData(username + index, password, 5, contact + index));
            stopwatch.Stop();
            Logger.Log("Added account #" + index + " - duration: " + stopwatch.ElapsedMilliseconds + "ms");
        }
    }
}
