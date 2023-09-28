﻿namespace VHUX.API.Model.Customer
{ 
    public class CustomerLoginModel
    {
        public string username { set; get; }
        public string password { set; get; }
    }
    public class CustomerTokenModel
    {
        public long id { get; set; }
        public string username { get; set; }
        public string token { get; set; }
        public string full_name { get; set; }


    }
}
