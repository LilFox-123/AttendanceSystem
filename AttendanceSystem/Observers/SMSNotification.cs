
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem
{
    public class SMSNotification : IObserver
    {
        private string phoneNumber;

        public SMSNotification(string phone)
        {
            this.phoneNumber = phone;
        }

        public void Update(string message)
        {
            Console.WriteLine($"[SMS] Gui den {phoneNumber}: {message}");
        }
    }
}
