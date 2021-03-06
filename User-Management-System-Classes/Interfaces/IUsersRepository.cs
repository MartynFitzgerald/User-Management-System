﻿using System;
using System.Collections.Generic;

namespace User_Management_System_Classes
{
    public interface IUsersRepository
    {
        List<User> GetAll();
        void Add(User user);
        void Update(User user);
        void Delete(User user);
    }
}
 