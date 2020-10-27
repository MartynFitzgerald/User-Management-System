using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace User_Management_System_Classes
{
    /*
        This class contains all the methods for performing actions to the RESTful API that stores the information.
    */
    public class UsersWebRepository : IUsersRepository
    {
        private const string baseUrl = "https://jsonplaceholder.typicode.com";
        private readonly WebClient webClient;

        public UsersWebRepository()
        {
            webClient = new WebClient();
        }
        public List<User> GetAll()
        {
            //Request RESTful API for all the users.
            string rawText = webClient.DownloadString($"{baseUrl}/users");
            //Deserialize JSON into List of users.
            var users = JsonSerializer.Deserialize<List<User>>(rawText);
            return users;
        }
        public void Add(User user)
        {
            //Convert the user object into JSON object.
            string rawText = JsonSerializer.Serialize(user);
            //Send insert a user to RESTful API using POST HTTP method.
            webClient.UploadString($"{baseUrl}/users", "POST", rawText);
        }
        public void Update(User user)
        {
            //Convert the user object into JSON object.
            string rawText = JsonSerializer.Serialize(user);
            //Send updated user to RESTful API using PUT HTTP method.
            webClient.UploadString($"{baseUrl}/users/{user.id}", "PUT", rawText);
        }
        public void Delete(User user)
        {
            //Send the id from the deleted user to RESTful API using DELETE HTTP method.
            webClient.UploadString($"{baseUrl}/users/{user.id}", "DELETE", WebRequestMethods.Http.Connect);
        }
    }
}