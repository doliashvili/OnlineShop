using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.UI.Models.User
{
    public class RegisterResponse
    {
        public string Data { get; set; }
        public string Message { get; set; }
        public bool Succeeded { get; set; }
    }
}