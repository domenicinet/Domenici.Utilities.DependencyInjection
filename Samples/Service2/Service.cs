using BusinessLogic;
using System;

namespace Service2
{
    public class Service : IService
    {
        public string GetMessage()
        {
            return "Hello from Service 2.";
        }
    }
}
