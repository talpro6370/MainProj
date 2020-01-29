using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MainProject;
using MainProject.Business_Logic;

namespace WebApiPart.Models
{
    public class LoginHelpClass
    {
       
        public  LoginToken<Administrator> getLoginToken()
        {
            LoginToken<Administrator> adminToken = new LoginToken<Administrator>();
            adminToken.User = new Administrator();
            adminToken.User.AdminUserName = "admin";
            adminToken.User.Password = "9999";
            return adminToken;
        }
        public LoginToken<AirlineCompany> GetAirlineToken()
        {
            LoginToken<AirlineCompany> airlineToken = new LoginToken<AirlineCompany>();
            airlineToken.User = new AirlineCompany();
            airlineToken.User.USER_NAME = "rdt";
            airlineToken.User.PASSWORD = "6370";
            return airlineToken;
        }
        public LoginToken<Customer> GetCustomerToken()
        {
            LoginToken<Customer> customerToken = new LoginToken<Customer>();
            customerToken.User = new Customer();
            customerToken.User.USER_NAME = "avi";
            customerToken.User.PASSWORD = "456789";
            return customerToken;
        }
    }
}