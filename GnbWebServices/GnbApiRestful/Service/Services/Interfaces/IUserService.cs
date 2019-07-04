using System;
using System.Collections.Generic;
using Service.Model;

namespace Service
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
    }
}