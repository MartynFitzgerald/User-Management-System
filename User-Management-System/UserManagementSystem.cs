using System;
using System.Collections.Generic;
using User_Management_System_Classes;

namespace User_Management_System
{
    /*
        This class contains the initial main function for this main menu system
        and other interfaces used to retreive information from the user.
    */
    class UserManagementSystem
    {
        static void Main(string[] args)
        {
            IUsersRepository usersRepository;
            //Storing choice of storage method.
            string storageMethod = "";
            //Recursive check to ensure the menu is visible.
            bool isMenuVisible = true;

            while (storageMethod != "web" && storageMethod != "local")
            {
                Console.Clear();
                Console.WriteLine("User Management System\n");
                Console.WriteLine("This system will allow you to edit users' data either locally or web stored.");
                Console.WriteLine("Please note that changes to the RESTful API will not affect the data.\n");
                Console.WriteLine("Would you like to use 'Web' or 'Local' storage?");
                storageMethod = Console.ReadLine().ToLower();
            }

            if (storageMethod == "web")
            {
                usersRepository = new UsersWebRepository();
            }
            else
            {
                usersRepository = new UsersLocalRespoitory("../data/", "Users", ".json");
            }

            List<User> users = usersRepository.GetAll();

            //This while loop allows the menu to keep displaying till the user selected the 'Exit' option.
            while (isMenuVisible) 
            {
                Console.Clear();
                Console.WriteLine("User Management System\n");
                Console.WriteLine("1) View users.");
                Console.WriteLine("2) Insert user.");
                Console.WriteLine("3) Modify user.");
                Console.WriteLine("4) Delete user.");
                Console.WriteLine("5) Exit.\n");

                string userInput = RequestInput("Please select an option: ");
                Console.Clear();

                switch (userInput)
                {
                    case "1":
                        ViewUsersUI(users);
                        break;
                    case "2":
                        var userAdd = AddUserUI(users);
                        usersRepository.Add(userAdd);
                        users.Add(userAdd);
                        break;
                    case "3":
                        var userSelected = SelectUserUI(users);
                        var userModify = ModifyUserUI(userSelected);
                        usersRepository.Update(userModify);
                        break;
                    case "4":
                        var userDelete = DeleteUserUI(SelectUserUI(users));
                        usersRepository.Delete(userDelete);
                        users.Remove(userDelete);
                        break;
                    case "5":
                        isMenuVisible = false;
                        break;
                    default:
                        Console.Write("Invalid Input. Please press enter to retry...");
                        Console.ReadLine();
                        break;
                }
            }
        }
        private static string RequestInput(string text)
        {
            Console.Write(text);
            return Console.ReadLine();
        }
        private static void ReturnToMainMenu()
        {
            RequestInput("Please press enter to return to the main menu...");
        }
        private static void ViewUsersUI(List<User> users)
        {
            foreach (var User in users)
            {
                Console.WriteLine(
                    $"Id: {User.id}\n" +
                    $"Name: {User.name}\n" +
                    $"Username: {User.username}\n" +
                    $"Email: {User.email}\n" +
                    $"Address: {User.address.suite}, {User.address.street}, {User.address.city}, {User.address.zipcode}\n" +
                    $"Coordinates: {User.address.geo.lat}, {User.address.geo.lng}\n" +
                    $"Phone Number: {User.phone}\n" +
                    $"Website: {User.website}\n" +
                    $"Company: {User.company.name}, {User.company.catchPhrase}, { User.company.bs}\n"
                );
            }
            ReturnToMainMenu();
        }
        //This collects data for a new user to be inserted.
        private static User AddUserUI(List<User> users)
        {
            //Requesting all the details for the new user.
            Console.WriteLine("To add a user, please fill out the fields below.");
            User user = new User();
            user.id = users.Count + 1; ;
            user.name = RequestInput("Name: ");
            user.username = RequestInput("Username: ");
            user.email = RequestInput("Email Address: ");
            user.phone = RequestInput("Phone Number: ");
            user.website = RequestInput("Website: ");
            user.address = new Address();
            user.address.street = RequestInput("Street: ");
            user.address.suite = RequestInput("Suite: ");
            user.address.city = RequestInput("City: ");
            user.address.zipcode = RequestInput("Zipcode (Postcode): ");
            user.address.geo = new Geo();
            user.address.geo.lat = RequestInput("Latitude: ");
            user.address.geo.lng = RequestInput("Longitude: ");
            user.company = new Company();
            user.company.name = RequestInput("Company name: ");
            user.company.catchPhrase = RequestInput("Company catch phrase: ");
            user.company.bs = RequestInput("Company bs (Keywords): ");

            Console.WriteLine($"The user with the id {user.id} has been added to the local and external storage.");
            ReturnToMainMenu();
            return user;
        }
        private static User ModifyUserUI(User user)
        {
            if (user != null)
            {
                Console.Clear();
                Console.WriteLine($"Please choose the data you would like to change on the user with ID: {user.id}...");
                Console.WriteLine("1) Id\n" +
                                  "2) Name\n" +
                                  "3) Username\n" +
                                  "4) Email Address\n" +
                                  "5) Phone Number\n" +
                                  "6) Website\n" +
                                  "7) Street\n" +
                                  "8) Suite\n" +
                                  "9) City\n" +
                                  "10) Zipcode (Postcode)\n" +
                                  "11) Latitude\n" +
                                  "12) Longitude\n" +
                                  "13) Company Name\n" +
                                  "14) Company catch phrase\n" +
                                  "15) Company bs (Keywords)");
                string selectedOption = RequestInput("Please select an option: ");
                switch (selectedOption)
                {
                    case "1":
                        int id = Int32.Parse(RequestInput("Please enter the new id: "));
                        user.id = id;
                        return user;
                    case "2":
                        string name = RequestInput("Please enter the new name: ");
                        user.name = name;
                        return user;
                    case "3":
                        string username = RequestInput("Please enter the new username: ");
                        user.username = username;
                        return user;
                    case "4":
                        string email = RequestInput("Please enter the new email: ");
                        user.email = email;
                        return user;
                    case "5":
                        string phone = RequestInput("Please enter the new phone: ");
                        user.phone = phone;
                        return user;
                    case "6":
                        string website = RequestInput("Please enter the new website: ");
                        user.website = website;
                        return user;
                    case "7":
                        string street = RequestInput("Please enter the new street: ");
                        user.address.street = street;
                        return user;
                    case "8":
                        string suite = RequestInput("Please enter the new suite: ");
                        user.address.suite = suite;
                        return user;
                    case "9":
                        string city = RequestInput("Please enter the new city: ");
                        user.address.city = city;
                        return user;
                    case "10":
                        string zipcode = RequestInput("Please enter the new zipcode (postcode): ");
                        user.address.zipcode = zipcode;
                        return user;
                    case "11":
                        string latitude = RequestInput("Please enter the new latitude: ");
                        user.address.geo.lat = latitude;
                        return user;
                    case "12":
                        string longitude = RequestInput("Please enter the new longitude: ");
                        user.address.geo.lng = longitude;
                        return user;
                    case "13":
                        string cName = RequestInput("Please enter the new company name: ");
                        user.company.name = cName;
                        return user;
                    case "14":
                        string cCatchPhrase = RequestInput("Please enter the new company catchphrase: ");
                        user.company.catchPhrase = cCatchPhrase;
                        return user;
                    case "15":
                        string cBS = RequestInput("Please enter the new company bs (Keywords): ");
                        user.company.bs = cBS;
                        return user;
                    default:
                        Console.Write("Invalid Input.");
                        ReturnToMainMenu();
                        break;
                }
            }
            else
            {
                Console.WriteLine($"No user with the ID was found.");
                ReturnToMainMenu();
            }
            return user;
        }
        private static User DeleteUserUI(User user)
        {
            if (user != null)
            {
                Console.WriteLine($"The user with the id {user.id} has been removed from the storage.");
                ReturnToMainMenu();
                return user;
            }

            Console.WriteLine($"No user with the ID was found.");
            ReturnToMainMenu();
            return null;
        }
        private static User SelectUserUI(List<User> users)
        {
            Console.WriteLine("Please enter an the ID of the user in the field below.");
            int userInput = Int32.Parse(RequestInput("Id: "));

            //Search for a user with the id that equals userInput.
            User user = users.Find(User => User.id == userInput);

            return user;
        }
    }
}