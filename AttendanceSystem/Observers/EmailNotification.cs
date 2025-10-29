using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem
{
    public class EmailNotification : IObserver
    {
        private string recipientEmail;

        public EmailNotification(string email)
        {
            this.recipientEmail = email;
        }

        public void Update(string message)
        {
            Console.WriteLine($"[EMAIL] Gui den {recipientEmail}: {message}");
        }
    }
}
