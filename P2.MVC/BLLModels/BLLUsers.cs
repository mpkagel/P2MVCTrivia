using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2.MVC.BLLModels
{
    public class BLLUsers
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pw { get; set; }
        public string Username { get; set; }
        public int PointTotal { get; set; }
        public bool? AccountType { get; set; }
    }
}
