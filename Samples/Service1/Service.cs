using BusinessLogic;
using System;

namespace Service1
{
    public class Service : IService
    {
        public string GetMessage()
        {
            return "Hello from Service 1.";
        }
    }
}
