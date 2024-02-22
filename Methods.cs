using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace GuitarRentalService
{
    public class Methods
    {
        public static string errorMessage = "";
        public static int loggedInId = 0;
        public static bool isAdminLoggedIn = false;
        public static int adminUserIdWorkingWith = 0;

        public static void DrawLogo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            string logo = @"

            Välkommen till

 ███╗   ███╗██╗██╗  ██╗███████╗███████╗
 ████╗ ████║██║██║ ██╔╝██╔════╝██╔════╝
 ██╔████╔██║██║█████╔╝ █████╗  ███████╗
 ██║╚██╔╝██║██║██╔═██╗ ██╔══╝  ╚════██║
 ██║ ╚═╝ ██║██║██║  ██╗███████╗███████║
 ╚═╝     ╚═╝╚═╝╚═╝  ╚═╝╚══════╝╚══════╝

            Gitarruthyrning
";
            Console.WriteLine(logo);
            Console.ResetColor();
        }

        public static void MainMenu()
        {
            DrawLogo();
            string info = "Huvudmeny:\n";
            List<string> options = new() { "Logga in", "Registrera dig", "Visa gitarrer som finns att hyra", "Logga in som Admin (tas bort sen)", "Logga ut", "Avsluta" };
            Menu mainMenu = new Menu(info, options);
            int selectedIndex = mainMenu.DrawMenu();

            switch (selectedIndex)
            {
                case 0:
                    LogIn();
                    break;

                case 1:
                    Register();
                    break;

                case 2:
                    ListGuitars();
                    break;

                case 3:
                    loggedInId = 0;
                    AdminMenu();
                    break;

                case 4:
                    LogOut();
                    break;

                case 5:
                    ExitProgram();
                    break;
            }
        }

        public static void LogIn()
        {
            var accounts = JsonSerializer.Deserialize<List<Account>>(File.ReadAllText("./accounts.json"));
            CheckIfAccountsExist(accounts.Count);

            string emailAddress = "";
            while (emailAddress == "")
            {
                DrawLogo();
                Console.WriteLine(errorMessage);
                Console.WriteLine("Email:");
                emailAddress = InputTest.TestIfEmptyString(Console.ReadLine());
                emailAddress = InputTest.TestEmail(emailAddress);
                if (emailAddress != "") errorMessage = "";
            }

            List<Account> account = accounts.Where(e => e.EmailAddress == emailAddress).ToList();

            if (account.Count < 1)
            {
                DrawLogo();
                Console.WriteLine("Epostadressen finns inte registrerad");
                Console.WriteLine("Tryck enter för att gå tillbaka");
                Console.ReadLine();
                MainMenu();
            }

            string passWord = "";
            while (passWord == "")
            {
                DrawLogo();
                Console.WriteLine(errorMessage);
                Console.WriteLine("Lösenord:");
                passWord = InputTest.TestPassWord(Console.ReadLine(), emailAddress);
                if (passWord != "") errorMessage = "";
            }

            if (account[0].PassWord == passWord)
            {
                loggedInId = account[0].AccountId;
                if (account[0].IsAdmin)
                {
                    AdminMenu();
                }
                else
                {
                    UserMenu();
                }
            }
            else
            {
                Console.WriteLine("Du har angett fel lösenord");
                LogIn();
            }
        }

        public static void LogOut()
        {
            loggedInId = 0;
            isAdminLoggedIn = false;
            adminUserIdWorkingWith = 0;

            DrawLogo();
            Console.WriteLine("Du är uloggad");
            Console.WriteLine("Tryck på enter för att gå tillbaka");
            Console.ReadLine();
            MainMenu();
        }

        public static void Register()
        {
            var accounts = JsonSerializer.Deserialize<List<Account>>(File.ReadAllText("./accounts.json"));
            CheckIfAccountsExist(accounts.Count);

            string fName = "";
            while (fName == "")
            {
                DrawLogo();
                Console.WriteLine(errorMessage);
                Console.WriteLine("Förnamn:");
                fName = InputTest.TestIfEmptyString(Console.ReadLine());
                fName = InputTest.TestName(fName, "Förnamn");
                if (fName != "") errorMessage = "";
            }
            string lName = "";
            while (lName == "")
            {
                DrawLogo();
                Console.WriteLine(errorMessage);
                Console.WriteLine("Efternamn:");
                lName = InputTest.TestIfEmptyString(Console.ReadLine());
                lName = InputTest.TestName(lName, "Efternamn");
                if (lName != "") errorMessage = "";
            }

            string address = "";
            while (address == "")
            {
                DrawLogo();
                Console.WriteLine(errorMessage);
                Console.WriteLine("Adress:");
                address = InputTest.TestIfEmptyString(Console.ReadLine());
                if (address != "") errorMessage = "";
            }

            int zipCode = 0;
            while (zipCode == 0)
            {
                DrawLogo();
                Console.WriteLine(errorMessage);
                Console.WriteLine("Postnummer:");
                zipCode = InputTest.TestInt(Console.ReadLine(), "Postnummer");
                if (zipCode != 0) errorMessage = "";
            }

            string city = "";
            while (city == "")
            {
                DrawLogo();
                Console.WriteLine(errorMessage);
                Console.WriteLine("Stad:");
                city = InputTest.TestIfEmptyString(Console.ReadLine());
                city = InputTest.TestName(city, "Stad");
                if (city != "") errorMessage = "";
            }

            string email = "";
            while (email == "")
            {
                DrawLogo();
                Console.WriteLine(errorMessage);
                Console.WriteLine("Email:");
                email = InputTest.TestIfEmptyString(Console.ReadLine());
                email = InputTest.TestEmail(email);
                if (email != "") errorMessage = "";
            }

            string passWord1 = "";
            while (passWord1 == "")
            {
                DrawLogo();
                Console.WriteLine(errorMessage);
                Console.WriteLine("Lösenord (Minst 6 tecken):");
                passWord1 = InputTest.TestPassWord1(Console.ReadLine());
                if (passWord1 != "") errorMessage = "";
            }

            string passWord2 = "";
            while (passWord2 == "")
            {
                DrawLogo();
                Console.WriteLine(errorMessage);
                Console.WriteLine("Upprepa Lösenord:");
                passWord2 = InputTest.TestPassWord2(Console.ReadLine(), passWord1);
                if (passWord2 != "") errorMessage = "";
            }

            string phoneNumber = "";
            while (phoneNumber == "")
            {
                DrawLogo();
                Console.WriteLine(errorMessage);
                Console.WriteLine("Telefonnummer:");
                phoneNumber = InputTest.TestPhoneNumber(Console.ReadLine());
                if (phoneNumber != "") errorMessage = "";
            }

            int accountId = 0;
            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].AccountId > accountId) accountId = accounts[i].AccountId;
            }
            accountId++;

            Account account = new Account(accountId, fName, lName, address, zipCode, city, email, passWord1, phoneNumber, false);
            accounts.Add(account);
            string jsonString = JsonSerializer.Serialize(accounts);
            File.WriteAllText("accounts.json", jsonString);

            MainMenu();
        }

        public static void ExitProgram()
        {
            Environment.Exit(0);
        }

        public static void AdminMenu()
        {
            isAdminLoggedIn = true;
            string info = "Du är inloggad som admin\n";
            List<string> options = new() { "Lägg till ny gitarr", "Visa gitarrer", "Visa alla bokningar", "Redigera användare", "Huvudmeny" };
            Menu adminMenu = new(info, options);
            int selectedIndex = adminMenu.DrawMenu();

            switch (selectedIndex)
            {
                case 0:
                    AddGuitar();
                    break;

                case 1:
                    ListGuitars();
                    break;

                case 2:
                    AdminViewAllBookings();
                    break;

                case 3:
                    EditUsers();
                    break;

                case 4:
                    MainMenu();
                    break;
            }
        }

        public static void AddGuitar()
        {
            var guitars = JsonSerializer.Deserialize<List<Guitar>>(File.ReadAllText("./guitars.json"));

            string brand = "";
            while (brand == "")
            {
                DrawLogo();
                Console.WriteLine(errorMessage);
                Console.WriteLine("Ange märke:");
                brand = InputTest.TestIfEmptyString(Console.ReadLine());
                if (brand != "") errorMessage = "";
            }

            string model = "";
            while (model == "")
            {
                DrawLogo();
                Console.WriteLine(errorMessage);
                Console.WriteLine("Ange model:");
                model = InputTest.TestIfEmptyString(Console.ReadLine());
                if (model != "") errorMessage = "";
            }

            string colour = "";
            while (colour == "")
            {
                DrawLogo();
                Console.WriteLine(errorMessage);
                Console.WriteLine("Ange färg:");
                colour = InputTest.TestIfEmptyString(Console.ReadLine());
                colour = InputTest.TestName(colour, "Färg");
                if (colour != "") errorMessage = "";
            }

            string body = "";
            while (body == "")
            {
                DrawLogo();
                Console.WriteLine(errorMessage);
                Console.WriteLine("Ange kropp:");
                body = InputTest.TestIfEmptyString(Console.ReadLine());
                body = InputTest.TestName(body, "Kropp");
                if (body != "") errorMessage = "";
            }

            string neck = "";
            while (neck == "")
            {
                DrawLogo();
                Console.WriteLine(errorMessage);
                Console.WriteLine("Ange hals:");
                neck = InputTest.TestIfEmptyString(Console.ReadLine());
                if (neck != "") errorMessage = "";
            }

            int scale = 0;
            while (scale == 0)
            {
                DrawLogo();
                Console.WriteLine(errorMessage);
                Console.WriteLine("Ange skala:");
                scale = InputTest.TestInt(Console.ReadLine(), "Skala");
                if (scale != 0) errorMessage = "";
            }

            int frets = 0;
            while (frets == 0)
            {
                DrawLogo();
                Console.WriteLine(errorMessage);
                Console.WriteLine("Ange antal band:");
                frets = InputTest.TestInt(Console.ReadLine(), "Antal band");
                if (frets != 0) errorMessage = "";
            }

            string pickUps = "";
            while (pickUps == "")
            {
                DrawLogo();
                Console.WriteLine(errorMessage);
                Console.WriteLine("Ange mickar:");
                pickUps = InputTest.TestIfEmptyString(Console.ReadLine());
                if (pickUps != "") errorMessage = "";
            }

            decimal pricePerDay = 0;
            while (pricePerDay == 0)
            {
                DrawLogo();
                Console.WriteLine(errorMessage);
                Console.WriteLine("Ange kostnad att hyra per dag:");
                pricePerDay = InputTest.TestDecimal(Console.ReadLine(), "Pris per dag");
                if (pricePerDay != 0) errorMessage = "";
            }

            int guitarId = 0;
            for (int i = 0; i < guitars.Count; i++)
            {
                if (guitars[i].GuitarId > guitarId) guitarId = guitars[i].GuitarId;
            }
            guitarId++;

            Guitar guitar = new Guitar(guitarId, brand, model, colour, body, neck, scale, frets, pickUps, pricePerDay);
            guitars.Add(guitar);
            string jsonString = JsonSerializer.Serialize(guitars);
            File.WriteAllText("guitars.json", jsonString);

            AdminMenu();
        }

        public static void EditUsers()
        {
            var accounts = JsonSerializer.Deserialize<List<Account>>(File.ReadAllText("./accounts.json"));
            CheckIfAccountsExist(accounts.Count);

            int indexToRemove = 0;
            if (loggedInId != 0)
            {
                for (int i = 0; i < accounts.Count; i++)
                {
                    if (accounts[i].AccountId == loggedInId)
                    {
                        indexToRemove = i;
                    }
                }
                accounts.RemoveAt(indexToRemove);
            }

            string info = "Välj användare att redigera:\n";
            List<string> options = new();

            for (int i = 0; i < accounts.Count; i++)
            {
                options.Add($"{accounts[i].FirstName} {accounts[i].LastName}");
            }
            options.Add("Tillbaka");

            Menu mainMenu = new Menu(info, options);
            int selectedIndex = mainMenu.DrawMenu();

            if (selectedIndex == accounts.Count)
            {
                AdminMenu();
            }
            else
            {
                ViewUser(accounts[selectedIndex].AccountId);
            }
        }

        public static void ViewUser(int userId)
        {
            var accounts = JsonSerializer.Deserialize<List<Account>>(File.ReadAllText("./accounts.json"));
            CheckIfAccountsExist(accounts.Count);

            string info = "";
            List<Account> account = accounts.Where(a => a.AccountId == userId).ToList();
            List<string> selectionInfo = new();

            string menuChangeAdminStatus = "";
            if (account[0].IsAdmin)
            {
                menuChangeAdminStatus = "Ta bort admin-rättigheter för användare";
            }
            else
            {
                menuChangeAdminStatus = "Gör användare till admin";
            }

            List<string> options = new() { "Ta bort användare", menuChangeAdminStatus, "Tillbaka" };

            selectionInfo.Add($"{account[0].FirstName} {account[0].LastName}");
            selectionInfo.Add($"Användar Id: {account[0].AccountId}");
            selectionInfo.Add($"Adress: {account[0].Address}");
            selectionInfo.Add($"Postnummer: {account[0].ZipCode}");
            selectionInfo.Add($"Stad: {account[0].City}");
            selectionInfo.Add($"Epostadress: {account[0].EmailAddress}");
            selectionInfo.Add($"Lösenord: {account[0].PassWord}");
            selectionInfo.Add($"Telefonnummer: {account[0].PhoneNumber}");
            selectionInfo.Add($"Är användaren admin: {account[0].IsAdmin}");

            Menu singleBookingMenu = new(info, options, selectionInfo);
            int selectedIndex = singleBookingMenu.DrawMenu();

            switch (selectedIndex)
            {
                case 0:
                    RemoveUser(userId);
                    break;

                case 1:
                    MakeUserAdmin(userId);
                    break;

                case 2:
                    EditUsers();
                    break;
            }
        }

        public static void RemoveUser(int userId)
        {
            var accounts = JsonSerializer.Deserialize<List<Account>>(File.ReadAllText("./accounts.json"));
            CheckIfAccountsExist(accounts.Count);
            var bookings = JsonSerializer.Deserialize<List<Booking>>(File.ReadAllText("./bookings.json"));
            //CheckIfBookingsExist(bookings.Count);

            List<int> bookingIndexesToRemove = new();
            for (int i = 0; i < bookings.Count; i++)
            {
                if (bookings[i].UserId == userId)
                {
                    bookingIndexesToRemove.Add(i);
                }
            }

            bookingIndexesToRemove.Reverse();
            foreach (var index in bookingIndexesToRemove)
            {
                bookings.RemoveAt(index);
            }

            int idCounter = 1;
            foreach (var booking in bookings)
            {
                booking.BookingId = idCounter;
                idCounter++;
            }

            int accountIndexToRemove = 0;
            foreach (var account in accounts)
            {
                if (account.AccountId == userId)
                {
                    break;
                }
                accountIndexToRemove++;
            }

            accounts.RemoveAt(accountIndexToRemove);

            string jsonString = JsonSerializer.Serialize(accounts);
            File.WriteAllText("accounts.json", jsonString);

            jsonString = JsonSerializer.Serialize(bookings);
            File.WriteAllText("bookings.json", jsonString);

            DrawLogo();
            Console.WriteLine("Användaren har raderats");
            Console.WriteLine("Tryck enter för att gå tillbaka");
            Console.ReadLine();
            EditUsers();
        }

        public static void MakeUserAdmin(int userId)
        {
            var accounts = JsonSerializer.Deserialize<List<Account>>(File.ReadAllText("./accounts.json"));
            CheckIfAccountsExist(accounts.Count);

            int accountIndexToEdit = 0;
            foreach (var account in accounts)
            {
                if (account.AccountId == userId)
                {
                    break;
                }
                accountIndexToEdit++;
            }

            if (accounts[accountIndexToEdit].IsAdmin)
            {
                accounts[accountIndexToEdit].IsAdmin = false;
            }
            else if (!accounts[accountIndexToEdit].IsAdmin)
            {
                accounts[accountIndexToEdit].IsAdmin = true;
            }

            string jsonString = JsonSerializer.Serialize(accounts);
            File.WriteAllText("accounts.json", jsonString);

            DrawLogo();
            if (accounts[accountIndexToEdit].IsAdmin)
            {
                Console.WriteLine("Användaren har fått admin-rättigheter");
            }
            else
            {
                Console.WriteLine("Användaren har blivit av med admin-rättigheter");
            }
            Console.WriteLine("Tryck enter för att gå tillbaka");
            Console.ReadLine();
            EditUsers();
        }

        public static void BookGuitar(int guitarId)
        {
            var guitars = JsonSerializer.Deserialize<List<Guitar>>(File.ReadAllText("./guitars.json"));
            CheckIfGuitarsExist(guitars.Count);
            var bookings = JsonSerializer.Deserialize<List<Booking>>(File.ReadAllText("./bookings.json"));
            List<Guitar> guitar = guitars.Where(g => g.GuitarId == guitarId).ToList();

            foreach (var guitarBooking in bookings)
            {
                if (guitarBooking.GuitarId == guitarId)
                {
                    DrawLogo();
                    Console.WriteLine("Denna gitarr är redan bokad");
                    Console.WriteLine("Tryck enter för att gå tillbaka");
                    Console.ReadLine();
                    ListGuitars();
                }
            }

            int numberOfDays = 0;
            while (numberOfDays == 0)
            {
                DrawLogo();
                Console.WriteLine(errorMessage);
                Console.WriteLine("Hur många dagar vill du boka gitarren?");
                numberOfDays = InputTest.TestInt(Console.ReadLine(), "Antal dagar");
                if (numberOfDays != 0) errorMessage = "";
            }
            decimal bookingCost = guitar[0].PricePerDay * numberOfDays;

            int highestBookingId = 0;
            highestBookingId = bookings.Count + 1;

            Booking booking = new(highestBookingId, loggedInId, guitarId, numberOfDays, bookingCost);
            bookings.Add(booking);

            string jsonString = JsonSerializer.Serialize(bookings);
            File.WriteAllText("bookings.json", jsonString);

            DrawLogo();
            Console.WriteLine($"Du har bokat {guitar[0].Brand} {guitar[0].Model} i {numberOfDays} dagar till en kostnad av {bookingCost} Sek");
            Console.WriteLine("Tryck enter för att gå tillbaka");
            Console.ReadLine();
            UserMenu();
        }

        public static void UserMenu()
        {
            DrawLogo();
            var accounts = JsonSerializer.Deserialize<List<Account>>(File.ReadAllText("./accounts.json"));
            CheckIfAccountsExist(accounts.Count);
            List<Account> account = accounts.Where(e => e.AccountId == loggedInId).ToList();

            string info = $"Välkommen {account[0].FirstName} {account[0].LastName}. Du är inloggad\n";
            List<string> options = new() { "Visa gitarrer som finns att hyra", "Visa mina bokningar", "Huvudmeny" };
            Menu adminMenu = new(info, options);
            int selectedIndex = adminMenu.DrawMenu();

            switch (selectedIndex)
            {
                case 0:
                    ListGuitars();
                    break;

                case 1:
                    ViewBookings();
                    break;

                case 2:
                    MainMenu();
                    break;
            }
        }

        public static void ViewGuitar(int guitarId)
        {
            var guitars = JsonSerializer.Deserialize<List<Guitar>>(File.ReadAllText("./guitars.json"));
            CheckIfGuitarsExist(guitars.Count);
            DrawLogo();

            string info = "Gitarrinfo:\n";
            List<string> selectionInfo = new();
            for (int i = 0; i < guitars.Count; i++)
            {
                if (guitars[i].GuitarId == guitarId)
                {
                    selectionInfo.Add(guitars[i].Brand + " " + guitars[i].Model);
                    selectionInfo.Add($"Färg: {guitars[i].Colour}");
                    selectionInfo.Add($"Kropp: {guitars[i].Body}");
                    selectionInfo.Add($"Hals: {guitars[i].Neck}");
                    selectionInfo.Add($"Skala: {guitars[i].Scale} mm");
                    selectionInfo.Add($"Antal band: {guitars[i].Frets}");
                    selectionInfo.Add($"Mickar: {guitars[i].PickUps}");
                    selectionInfo.Add($"Kostnad att hyra per dag: {guitars[i].PricePerDay} Sek");
                }
            }

            List<string> options = new();
            if (loggedInId != 0)
            {
                options = new() { "Boka gitarr", "Tillbaka" };
            }
            else if (isAdminLoggedIn)
            {
                options = new() { "Ta bort gitarr", "Tillbaka" };
            }
            else
            {
                options = new() { "Tillbaka" };
            }

            Menu guitarMenu = new(info, options, selectionInfo);
            int selectedIndex = guitarMenu.DrawMenu();

            if (isAdminLoggedIn)
            {
                switch (selectedIndex)
                {
                    case 0:
                        RemoveGuitar(guitarId);
                        break;

                    case 1:
                        ListGuitars();
                        break;
                }
            }
            else if (loggedInId != 0)
            {
                switch (selectedIndex)
                {
                    case 0:
                        BookGuitar(guitarId);
                        break;

                    case 1:
                        ListGuitars();
                        break;
                }
            }
            else
            {
                switch (selectedIndex)
                {
                    case 0:
                        ListGuitars();
                        break;
                }
            }
        }

        public static void ListGuitars()
        {
            var guitars = JsonSerializer.Deserialize<List<Guitar>>(File.ReadAllText("./guitars.json"));
            CheckIfGuitarsExist(guitars.Count);

            string info = "Visar gitarrer:\n";
            List<string> options = new();
            List<int> guitarIds = new();
            for (int i = 0; i < guitars.Count; i++)
            {
                options.Add($"{guitars[i].Brand} {guitars[i].Model}");
                guitarIds.Add(guitars[i].GuitarId);
            }
            Console.WriteLine("\n");
            options.Add("Tillbaka");

            Menu guitarMenu = new Menu(info, options);
            int selectedIndex = guitarMenu.DrawMenu();

            if (selectedIndex == guitars.Count)
            {
                if (isAdminLoggedIn)
                {
                    AdminMenu();
                }
                else
                {
                    if (loggedInId == 0)
                    {
                        MainMenu();
                    }
                    else
                    {
                        UserMenu();
                    }
                }
            }
            else
            {
                ViewGuitar(guitarIds[selectedIndex]);
            }
        }

        public static void ViewBookings()
        {
            var accounts = JsonSerializer.Deserialize<List<Account>>(File.ReadAllText("./accounts.json"));
            CheckIfAccountsExist(accounts.Count);
            var bookings = JsonSerializer.Deserialize<List<Booking>>(File.ReadAllText("./bookings.json"));
            CheckIfBookingsExist(bookings.Count);
            var guitars = JsonSerializer.Deserialize<List<Guitar>>(File.ReadAllText("./guitars.json"));
            CheckIfGuitarsExist(guitars.Count);

            string info = "";
            List<Booking> userBookings = new();
            if (isAdminLoggedIn)
            {
                userBookings = bookings.Where(b => b.UserId == adminUserIdWorkingWith).ToList();
                string fName = "";
                string lName = "";
                foreach (var account in accounts)
                {
                    if (account.AccountId == adminUserIdWorkingWith)
                    {
                        fName = account.FirstName;
                        lName = account.LastName;
                    }
                }

                info = $"Gitarrer som {fName} {lName} har bokat:\n";
            }
            else
            {
                userBookings = bookings.Where(b => b.UserId == loggedInId).ToList();
                info = "Dina bokningar:\n";
            }

            List<string> options = new();
            List<int> bookingIds = new();

            if (userBookings.Count < 1)
            {
                DrawLogo();
                Console.WriteLine("Du har inga bokade gitarrer");
                Console.WriteLine("Tryck enter för att gå tillbaka");
                Console.ReadLine();
                if (isAdminLoggedIn)
                {
                    AdminMenu();
                }
                else
                {
                    UserMenu();
                }
            }
            else
            {
                foreach (var booking in userBookings)
                {
                    foreach (var guitar in guitars)
                    {
                        if (guitar.GuitarId == booking.GuitarId)
                        {
                            options.Add($"{guitar.Brand} {guitar.Model}");
                        }
                    }
                    bookingIds.Add(booking.BookingId);
                }
                options.Add("Tillbaka");

                Menu bookingMenu = new Menu(info, options);
                int selectedIndex = bookingMenu.DrawMenu();

                if (selectedIndex == userBookings.Count)
                {
                    if (isAdminLoggedIn)
                    {
                        AdminViewAllBookings();
                    }
                    else
                    {
                        UserMenu();
                    }
                }
                else
                {
                    ViewSingleBooking(bookingIds[selectedIndex]);
                }
            }
        }

        public static void ViewSingleBooking(int bookingId)
        {
            DrawLogo();
            var accounts = JsonSerializer.Deserialize<List<Account>>(File.ReadAllText("./accounts.json"));
            CheckIfAccountsExist(accounts.Count);
            var bookings = JsonSerializer.Deserialize<List<Booking>>(File.ReadAllText("./bookings.json"));
            CheckIfBookingsExist(bookings.Count);
            var guitars = JsonSerializer.Deserialize<List<Guitar>>(File.ReadAllText("./guitars.json"));
            CheckIfGuitarsExist(guitars.Count);

            string fName = "";
            string lName = "";

            foreach (var account in accounts)
            {
                if (account.AccountId == adminUserIdWorkingWith)
                {
                    fName = account.FirstName;
                    lName = account.LastName;
                }
            }

            string info = isAdminLoggedIn ? $"{fName} {lName} har bokat:\n" : "Du har bokat:\n";

            List<Booking> booking = bookings.Where(b => b.BookingId == bookingId).ToList();
            List<Guitar> guitar = guitars.Where(g => g.GuitarId == booking[0].GuitarId).ToList();
            List<string> selectionInfo = new();
            List<string> options = new() { "Ta bort bokning", "Tillbaka" };

            selectionInfo.Add($"{guitar[0].Brand} {guitar[0].Model} i {booking[0].BookedNumberOfDays} dagar");
            selectionInfo.Add($"till en kostnad av {guitar[0].PricePerDay * booking[0].BookedNumberOfDays} Sek");

            Menu singleBookingMenu = new(info, options, selectionInfo);
            int selectedIndex = singleBookingMenu.DrawMenu();

            switch (selectedIndex)
            {
                case 0:
                    RemoveBooking(bookingId);
                    break;

                case 1:
                    ViewBookings();
                    break;
            }
        }

        public static void RemoveBooking(int bookingId)
        {
            DrawLogo();
            var bookings = JsonSerializer.Deserialize<List<Booking>>(File.ReadAllText("./bookings.json"));
            CheckIfBookingsExist(bookings.Count);

            for (int i = 0; i < bookings.Count; i++)
            {
                if (bookings[i].BookingId == bookingId)
                {
                    bookings.RemoveAt(i);
                }
            }

            int idCounter = 1;
            foreach (var booking in bookings)
            {
                booking.BookingId = idCounter;
                idCounter++;
            }

            string jsonString = JsonSerializer.Serialize(bookings);
            File.WriteAllText("bookings.json", jsonString);

            Console.WriteLine("Bokningen är borttagen");
            Console.WriteLine("Tryck enter för att gå tillbaka");
            Console.ReadLine();
            ViewBookings();
        }

        public static void RemoveGuitar(int guitarId)
        {
            DrawLogo();
            var guitars = JsonSerializer.Deserialize<List<Guitar>>(File.ReadAllText("./guitars.json"));
            CheckIfGuitarsExist(guitars.Count);
            var bookings = JsonSerializer.Deserialize<List<Booking>>(File.ReadAllText("./bookings.json"));
            //CheckIfBookingsExist(bookings.Count);

            foreach (var booking in bookings)
            {
                if (booking.GuitarId == guitarId)
                {
                    Console.WriteLine("Gitarren du försöker ta bort är bokad och går inte att ta bort");
                    Console.ReadLine();
                    ListGuitars();
                }
            }
            //guitars.RemoveAt(guitarId - 1);
            for (int i = 0; i < guitars.Count; i++)
            {
                if (guitars[i].GuitarId == guitarId)
                {
                    guitars.RemoveAt(i);
                }
            }

            string jsonString = JsonSerializer.Serialize(guitars);
            File.WriteAllText("guitars.json", jsonString);

            Console.WriteLine("Gitarren har tagits bort");
            Console.WriteLine("Tryck enter för att gå tillbaka.");
            Console.ReadLine();
            ListGuitars();
        }

        public static void AdminViewAllBookings()
        {
            var bookings = JsonSerializer.Deserialize<List<Booking>>(File.ReadAllText("./bookings.json"));
            CheckIfBookingsExist(bookings.Count);
            var guitars = JsonSerializer.Deserialize<List<Guitar>>(File.ReadAllText("./guitars.json"));
            CheckIfGuitarsExist(guitars.Count);
            var accounts = JsonSerializer.Deserialize<List<Account>>(File.ReadAllText("./accounts.json"));
            CheckIfAccountsExist(accounts.Count);

            string info = "Alla bokningar:\n";
            List<string> options = new();
            List<int> bookingIds = new();
            List<int> userIdsInBooking = new();
            List<int> uniqueUserIdsInBooking = new();

            foreach (var booking in bookings)
            {
                foreach (var account in accounts)
                {
                    if (booking.UserId == account.AccountId)
                    {
                        options.Add($"{account.FirstName} {account.LastName}");
                    }
                }
                userIdsInBooking.Add(booking.UserId);
            }

            uniqueUserIdsInBooking = userIdsInBooking.Distinct().ToList();
            options.Add("Tillbaka");
            List<string> optionsUnique = options.Distinct().ToList();
            Menu userBookingMenu = new Menu(info, optionsUnique);
            int selectedIndex = userBookingMenu.DrawMenu();

            if (selectedIndex + 1 == optionsUnique.Count)
            {
                AdminMenu();
            }
            else
            {
                adminUserIdWorkingWith = uniqueUserIdsInBooking[selectedIndex];
                ViewBookings();
            }
        }

        public static void CheckIfGuitarsExist(int numberOfGuitars)
        {
            if (numberOfGuitars < 1)
            {
                DrawLogo();
                Console.WriteLine("Det finns inga gitarrer");
                Console.WriteLine("Tryck enter för att gå tillbaka");
                Console.ReadLine();
                if (isAdminLoggedIn)
                {
                    AdminMenu();
                }
                else
                {
                    UserMenu();
                }
            }
        }

        public static void CheckIfBookingsExist(int numberOfBookings)
        {
            if (numberOfBookings < 1)
            {
                DrawLogo();
                Console.WriteLine("Det finns inga bokningar");
                Console.WriteLine("Tryck enter för att gå tillbaka");
                Console.ReadLine();
                if (isAdminLoggedIn)
                {
                    AdminMenu();
                }
                else
                {
                    UserMenu();
                }
            }
        }

        public static void CheckIfAccountsExist(int numberOfAccounts)
        {
            if (numberOfAccounts < 1)
            {
                DrawLogo();
                Console.WriteLine("Det finns inga användare registrerade");
                Console.WriteLine("Tryck enter för att gå tillbaka");
                Console.ReadLine();
                if (isAdminLoggedIn)
                {
                    AdminMenu();
                }
                else
                {
                    UserMenu();
                }
            }
        }
    }
}