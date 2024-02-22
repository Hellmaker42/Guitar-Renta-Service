using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarRentalService
{
    public class Account
    {
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public string EmailAddress { get; set; }
        public string PassWord { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }

        public Account(int accountId, string firstName, string lastName, string address, int zipCode, string city, string emailAddress, string passWord, string phoneNumber, bool isAdmin)
        {
            AccountId = accountId;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            ZipCode = zipCode;
            City = city;
            EmailAddress = emailAddress;
            PassWord = passWord;
            PhoneNumber = phoneNumber;
            IsAdmin = isAdmin;
        }
    }
}