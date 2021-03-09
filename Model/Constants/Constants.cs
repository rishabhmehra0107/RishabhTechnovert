using System;
namespace Bank.Model
{
    public static class Constants
    {
        public static double InitialBalance = 1000;
        public static int SameBankRTGS = 0;
        public static int SameBankIMPS = 5;
        public static int DiffBankRTGS = 2;
        public static int DiffBankIMPS = 6;

        public enum UserType
        {
            AccountHolder,
            Admin,
            Staff
        }

        public enum AccountType
        {
            Savings,
            Current
        }

        public enum TransactionType
        {
            Deposit,
            Withdraw,
            Transfer
        }

        public enum AccessOptions
        {
            GetDetails=1,
            Update=2,
            Delete=3,
            Exit=4
        }

        public enum BankType
        {
            Same,
            Different
        }

        public enum TransactionStatus
        {
            InsufficientBalance = -1,
            Null = -2
        }

        public enum MenuOption
        {
            Setup = 1,
            Login = 2,
            Exit = 3,
            Database=4
        }

        public enum DatabaseOption
        {
            BanksTable = 1,
            AccountHoldersTable = 2,
            EmployeesTable = 3,
            BranchesTable = 4,
            CurrenciesTable=5,
            Exit = 6
        }

        public enum AdminOption
        {
            AddStaff = 1,
            AddAccountHolder = 2,
            DisplayUsers = 3,
            UpdateCharges = 4,
            NewCurrency = 5,
            UpdateAccount = 6,
            DeleteAccount = 7,
            Logout = 8
        }

        public enum StaffOption
        {
            AddAccountHolder = 1,
            DisplayUsers = 2,
            UpdateCharges = 3,
            NewCurrency = 4,
            Logout = 5
        }

        public enum AccountHolderOption
        {
            Withdraw = 1,
            Deposit = 2,
            TransactionHistory = 3,
            Balance = 4,
            TransferFund = 5,
            RevertTransaction = 6,
            Logout = 7,
        }
    }
}
