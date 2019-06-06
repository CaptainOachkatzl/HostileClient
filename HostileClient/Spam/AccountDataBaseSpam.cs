using System;
using System.Text;
using XSLibrary.Cryptography.AccountManagement;

namespace HostileClient.Spam
{
    class AccountDataBaseSpam : ISpam
    {
        string fileName =  "accounts.txt";
        string directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/";
        FileUserBase dataBase;

        string username = "test123";
        string contact = "contact";
        byte[] password = Encoding.ASCII.GetBytes("password456");
        byte[] wrongPassword = Encoding.ASCII.GetBytes("wrong789");

        public override void Initialize()
        {
            dataBase = new FileUserBase(directory, fileName);
        }

        protected override void SpamAction(int index)
        {
            if (dataBase.AddAccount(new AccountCreationData(username, password, 5, contact)))
                Logger.Log("added user account");
            else
                Logger.Log("failed to add user account");

            if(dataBase.Validate(username, password, 5))
                Logger.Log("verfied user account");
            else
                Logger.Log("failed to verify user account");

            if (dataBase.Validate(username, wrongPassword, 5))
                Logger.Log("verfied user account");
            else
                Logger.Log("failed to verify user account");

            if(dataBase.EraseAccount(username))
                Logger.Log("deleted user account");
            else
                Logger.Log("failed to delete user account");
        }
    }
}
