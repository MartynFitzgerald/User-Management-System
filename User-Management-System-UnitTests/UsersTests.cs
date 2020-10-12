using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using User_Management_System_Classes;

namespace User_Management_System_UnitTests
{
    [TestClass]
    public class UsersWebRepositoryTests
    {
        [TestMethod]
        public void GetUsersBy_Web_ReturnsListOfUsers()
        {
            //Arrange                
            var usersRepository = new UsersWebRepository();

            //Act
            List<User> result = usersRepository.GetAll();

            //Assert
            CollectionAssert.Equals(result, new List<User>());

        }
    }
    [TestClass]
    public class UsersLocalRespoitoryTests
    {
        [TestMethod]
        public void GetUsersBy_Local_ReturnsListOfUsers()
        {
            //Arrange                
            var usersRepository = new UsersLocalRespoitory("../User-Management-System/data/", "Users", ".json");

            //Act
            List<User> result = usersRepository.GetAll();

            //Assert
            CollectionAssert.Equals(result, new List<User>());

        }
        [TestMethod]
        public void AddUserBy_Local_ReturnsUsers()
        {
            //Arrange                
            UsersLocalRespoitory usersRepository = new UsersLocalRespoitory("../User-Management-System/data/", "Users", ".json");
            User tempUser = new User();

            //Act
            tempUser.id = -1;
            tempUser.name = "Test User";
            tempUser.username = "TestUser";
            tempUser.email = "TestUser@gmail.com";
            tempUser.phone = "0123456789";
            tempUser.website = "N/A";
            tempUser.address = new Address();
            tempUser.address.street = "Sorrel Road";
            tempUser.address.suite = "103";
            tempUser.address.suite = "Bristol";
            tempUser.address.zipcode = "BS34 8DS";
            tempUser.address.geo = new Geo();
            tempUser.address.geo.lat = "52.024";
            tempUser.address.geo.lng = "-2.023";
            tempUser.company = new Company();
            tempUser.company.name = "N/A";
            tempUser.company.catchPhrase = "N/A";
            tempUser.company.bs = "N/A";

            usersRepository.Add(tempUser);

            List<User> users = usersRepository.GetAll();

            User addedUser = users.Find(user => user.id == tempUser.id);

            //Assert
            Assert.IsTrue(addedUser.id == tempUser.id);
        }
        [TestMethod]
        public void UpdateUserBy_Local_ReturnsUsers()
        {
            //Wait till the previous task to be carried out. Should find a better way to wait for last method.
            Thread.Sleep(500);
            //Arrange                
            UsersLocalRespoitory usersRepository = new UsersLocalRespoitory("../User-Management-System/data/", "Users", ".json");
            
            List<User> users = usersRepository.GetAll();

            //Act
            var index = users.FindIndex(user => user.id == -1);

            users[index].name = "Test User Updated";

            usersRepository.Update(users[index]);

            users = usersRepository.GetAll();

            //Assert
            Assert.IsTrue(users[index].name == "Test User Updated");
        }
    }
}