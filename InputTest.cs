using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace GuitarRentalService
{
    public static class InputTest
    {
        public static string TestIfEmptyString(string stringToTest)
        {
            if (stringToTest.Trim() == "")
            {
                Methods.errorMessage = "Du måste skriva något";
                stringToTest = "";
            }
            return stringToTest.Trim();
        }

        public static string TestName(string stringToTest, string type)
        {
            if (stringToTest.Any(c => char.IsDigit(c)))
            {
                Methods.errorMessage = $"{type} får inte innehålla siffror";
                stringToTest = "";
            }
            return stringToTest;
        }

        public static int TestInt(string intToTest, string type)
        {
            int output = 0;
            Regex regex = new Regex(@"^\d+$");
            if (intToTest == "")
            {
                Methods.errorMessage = "Du måste skriva något";
            }
            else if (regex.IsMatch(intToTest))
            {
                output = Convert.ToInt32(intToTest);
            }
            else
            {
                Methods.errorMessage = $"{type} får bara innehålla siffror";
            }

            return output;
        }

        public static decimal TestDecimal(string intToTest, string type)
        {
            decimal output = 0;
            Regex regex = new Regex(@"^\d+$");
            if (intToTest == "")
            {
                Methods.errorMessage = "Du måste skriva något";
            }
            else if (regex.IsMatch(intToTest))
            {
                output = Convert.ToDecimal(intToTest);
            }
            else
            {
                Methods.errorMessage = $"{type} får bara innehålla siffror";
            }

            return output;
        }

        public static string TestPhoneNumber(string stringToTest)
        {
            Regex regex = new Regex(@"^\d+$");
            if (stringToTest == "")
            {
                Methods.errorMessage = "Du måste skriva något";
            }
            else if (!regex.IsMatch(stringToTest))
            {
                Methods.errorMessage = "Telefonnummer får bara innehålla siffror";
                stringToTest = "";
            }
            return stringToTest;
        }

        public static string TestEmail(string stringToTest)
        {
            string output = "";
            if (stringToTest.Contains('@') && stringToTest.Contains('.'))
            {
                output = stringToTest.Trim().ToLower();
            }
            else
            {
                Methods.errorMessage = "Epostadress är ej gilltig";
                output = "";
            }
            return output;
        }

        public static string TestPassWord1(string stringToTest)
        {
            string output = stringToTest.Trim();
            if (output.Length < 6)
            {
                Methods.errorMessage = "Du måste ange minst 6 tecken";
                output = "";
            }
            return output;
        }

        public static string TestPassWord2(string stringToTest, string firstPassWord)
        {
            string output = stringToTest.Trim();
            if (output != firstPassWord)
            {
                Methods.errorMessage = "Lösenordet matchar ej";
                output = "";
            }
            return output;
        }

        public static string TestPassWord(string stringToTest, string email)
        {
            var accounts = JsonSerializer.Deserialize<List<Account>>(File.ReadAllText("./accounts.json"));
            Methods.CheckIfAccountsExist(accounts.Count);

            string output = stringToTest.Trim();
            foreach (var account in accounts)
            {
                if (account.EmailAddress == email)
                {
                    if (account.PassWord != output)
                    {
                        Methods.errorMessage = "Lösenordet matchar ej";
                        output = "";
                    }
                }
            }
            return output;
        }
    }
}