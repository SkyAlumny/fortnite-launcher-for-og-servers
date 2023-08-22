using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Auth;


namespace Auth
{
 

    class AuthenticationInput
    {
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static string AccessToken { get; set; }
    }
}