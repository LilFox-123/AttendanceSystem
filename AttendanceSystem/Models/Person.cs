using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem
{
    // ==================== 1. PERSON - BASE CLASS ====================

    public class Person
    {
        private string personId;
        private string fullName;
        private DateTime dateOfBirth;
        private string phoneNumber;
        private string address;

        public Person()
        {
            this.personId = "";
            this.fullName = "";
            this.dateOfBirth = DateTime.Now;
            this.phoneNumber = "";
            this.address = "";
        }

        public Person(string personId, string fullName, DateTime dateOfBirth,
                      string phoneNumber, string address)
        {
            this.personId = personId;
            this.fullName = fullName;
            this.dateOfBirth = dateOfBirth;
            this.phoneNumber = phoneNumber;
            this.address = address;
        }

        public string PersonId
        {
            get { return personId; }
            set { personId = value; }
        }

        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Ma: {personId}");
            Console.WriteLine($"Ho ten: {fullName}");
            Console.WriteLine($"Ngay sinh: {dateOfBirth:dd/MM/yyyy}");
            Console.WriteLine($"So dien thoai: {phoneNumber}");
            Console.WriteLine($"Dia chi: {address}");
        }
    }
}
