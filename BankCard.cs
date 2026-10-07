using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomat
{
    class BankCard
    {
        private int COUNT_OF_FAILED_ATTEMPTS = 3;
        private decimal balance = 0;
        private string cardNumber = "";
        private int failedPinAttempts = 0;
        private bool isBlocked = false;
        private string ownerName = "";
        private string pinCode = "";

        public BankCard(string cardNumber, string pinCode, decimal balance, string ownerName)
        {
            this.cardNumber = cardNumber;
            this.pinCode = pinCode;
            this.balance = balance;
            this.ownerName = ownerName;
        }

        private void BlockedCard()
        {
            this.isBlocked = true;
            throw new Exception("Card is blocked due to too many failed attempts.");
        }

        public string GetCardNumber() => this.cardNumber;
        public string GetOwnerName() => this.ownerName;
        public bool IsBlocked() => this.isBlocked;
        public decimal GetBalance(string pin)
        {
            if (this.CheckPin(pin))
                return this.balance;
            else
                throw new Exception("Invalid PIN.");
        }

        public bool CheckPin(string pin)
        {
            if (this.isBlocked)
                throw new Exception("Card is blocked.");

            if (this.failedPinAttempts >= COUNT_OF_FAILED_ATTEMPTS)
                this.BlockedCard();


            if (this.pinCode != pin)
            {
                this.failedPinAttempts++;

                if (this.failedPinAttempts >= COUNT_OF_FAILED_ATTEMPTS)
                    this.BlockedCard();

                return false;

            }
            else
            {
                return true;
            }
        }

        public void Withdraw(string pin, decimal amount)
        {
            if (this.CheckPin(pin))
            {
                if (amount > this.balance)
                    throw new Exception("Insufficient funds.");
                this.balance -= amount;
            }
            else
            {
                throw new Exception("Invalid PIN.");
            }
        }
    }

}
