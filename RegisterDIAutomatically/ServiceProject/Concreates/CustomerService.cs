using ServiceProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceProject.Concreates
{
    public class CustomerService: Servicebase, ICustomerService
    {
        public void Testmethod()
        {
            var a = 10;
        }
    }
}
