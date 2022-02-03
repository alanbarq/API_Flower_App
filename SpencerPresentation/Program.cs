using System;
using SpencerService;

namespace SpencerPresentation
{
    class Program
    {
        static void Main(string[] args)
        {
            UsersSL users = new UsersSL();
            users.ListOfUsers();
        }
    }
}
