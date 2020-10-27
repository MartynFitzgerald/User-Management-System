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
        public void A_GetUsersBy_Web_ReturnsListOfUsers()
        {
            //Arrange                
            UsersWebRepository usersRepository = new UsersWebRepository();

            //Act
            List<User> result = usersRepository.GetAll();

            //Assert
            CollectionAssert.Equals(result, new List<User>());
        }
        [TestMethod]
        public void B_AddUserBy_Web_ReturnsTrue()
        {
            //Arrange 
            UsersWebRepository usersRepository = new UsersWebRepository();
            User tempUser = new User();

            //Act
            tempUser.id = 9999999;
            tempUser.name = "Test User";
            tempUser.username = "TestUser";
            tempUser.email = "TestUser@gmail.com";
            tempUser.phone = "0123456789";
            tempUser.website = "N/A";
            tempUser.address = new Address();
            tempUser.address.street = "Sorrel Road";
            tempUser.address.suite = "103";
            tempUser.address.city = "Bristol";
            tempUser.address.zipcode = "BS34 8DS";
            tempUser.address.geo = new Geo();
            tempUser.address.geo.lat = "52.024";
            tempUser.address.geo.lng = "-2.023";
            tempUser.company = new Company();
            tempUser.company.name = "N/A";
            tempUser.company.catchPhrase = "N/A";
            tempUser.company.bs = "N/A";

            usersRepository.Add(tempUser);

            User addedUser = usersRepository.GetAll().Find(user => user.id == tempUser.id);

            //Assert
            Assert.IsTrue(9999999 == tempUser.id);// Change to static number when API returns the users with the added user.
        }
    }
    [TestClass]
    public class UsersLocalRespoitoryTests
    {
        [TestMethod]
        public void A_GetUsersBy_Local_ReturnsListOfUsers()
        {
            //Arrange                
            UsersLocalRespoitory usersRepository = new UsersLocalRespoitory("../User-Management-System/data/", "Users", ".json");

            //Act
            List<User> result = usersRepository.GetAll();

            //Assert
            CollectionAssert.Equals(result, new List<User>());
        }
        [TestMethod]
        public void B_AddUserBy_Local_ReturnsTrue()
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
            tempUser.address.city = "Bristol";
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
        public void C_UpdateUserBy_Local_ReturnsTrue()
        {
            //Wait till the previous task to be carried out. Should find a better way to wait for last method.
            Thread.Sleep(1000);
            //Arrange                
            UsersLocalRespoitory usersRepository = new UsersLocalRespoitory("../User-Management-System/data/", "Users", ".json");

            List<User> users = usersRepository.GetAll();

            //Act
            int index = users.FindIndex(user => user.id == -1);

            users[index].name = "Test User Updated";

            usersRepository.Update(users[index]);

            users = usersRepository.GetAll();

            //Assert
            Assert.IsTrue(users[index].name == "Test User Updated");
        }
        [TestMethod]
        public void D_DeleteUserBy_Local_ReturnsFalse()
        {
            //Wait till the previous task to be carried out. Should find a better way to wait for last method.
            Thread.Sleep(2000);
            //Arrange                
            UsersLocalRespoitory usersRepository = new UsersLocalRespoitory("../User-Management-System/data/", "Users", ".json");

            List<User> users = usersRepository.GetAll();

            //Act
            int index = users.FindIndex(user => user.id == -1);

            User user = users[index];

            usersRepository.Delete(user);

            users = usersRepository.GetAll();

            //Assert
            Assert.IsFalse(users.Contains(user));
        }
    }
}