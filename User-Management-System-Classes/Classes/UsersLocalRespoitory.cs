using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace User_Management_System_Classes
{
    /*
        This class contains all the methods for performing actions to the file that stores the information.
    */
    public class UsersLocalRespoitory : IUsersRepository
    {
        private readonly string defaultPath = System.IO.Path.GetFullPath(@"..\..\");
        private readonly string fileLocation = "";
        private readonly string fileName = "";
        private readonly string fileExtension = "";

        public UsersLocalRespoitory(string fileLocation, string fileName, string fileExtension)
        {
            this.fileLocation = fileLocation;
            this.fileName = fileName;
            this.fileExtension = fileExtension;
        }
        public List<User> GetAll()
        {
            //Read the contents of the JSON file and storing them as a string.
            string rawText = File.ReadAllText(@$"{defaultPath}{fileLocation}{fileName}{fileExtension}");
            //Return the list of users by deserializing the JSON object.
            return JsonSerializer.Deserialize<List<User>>(rawText);
        }
        public void Add(User user)
        {
            //Get current users and add a new user to the list.
            List<User> users = GetAll();
            //Add user from the list of users.
            users.Add(user);
            //Update file Stored
            SaveFile(users);
        }
        public void Update(User user)
        {
            //Get current users and add a new user to the list.
            List<User> users = GetAll();
            //Search for index of user to remove.
            int index = users.FindIndex(item => item.id == user.id);
            //Remove old user from the list of users.
            users.Remove(users[index]);
            //Add new user from the list of users.
            users.Add(user);
            //Update file Stored
            SaveFile(users);
        }
        public void Delete(User user)
        {
            //Get current users and add a new user to the list.
            List<User> users = GetAll();
            //Remove user from the list of users.
            users.Remove(user);
            //Update file Stored
            SaveFile(users);
        }
        private void SaveFile(List<User> users)
        {
            //Serialize the users object to JSON.
            string rawText = JsonSerializer.Serialize(users);
            //Create file that will be a copy of the original but it will have the current date in the filename.
            File.Copy(@$"{defaultPath}{fileLocation}{fileName}{fileExtension}", @$"{defaultPath}{fileLocation}{fileName}{DateTime.Now.ToString("@dd-MM-yyyy--HH-mm-ss")}{fileExtension}");
            //Create new file that will replace original file with the new user.
            File.WriteAllText(@$"{defaultPath}{fileLocation}{fileName}{fileExtension}", rawText);
        }
    }
}