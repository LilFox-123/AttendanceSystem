using System;

namespace AttendanceSystem
{
    // ==================== ATTENDANCE ====================
    // Ghi nhận trạng thái chấm công hàng ngày của nhân viên

    public class Attendance
    {
        private string attendanceId;
        private string employeeId;
        private DateTime date;
        private bool isPresent;
        private double totalHours;
        private string note;

        public Attendance()
        {
            this.attendanceId = "";
            this.employeeId = "";
            this.date = DateTime.Now;
            this.isPresent = false;
            this.totalHours = 0;
            this.note = "";
        }

        public Attendance(string attId, string empId, DateTime dt, bool present, double hours, string nt)
        {
            this.attendanceId = attId;
            this.employeeId = empId;
            this.date = dt;
            this.isPresent = present;
            this.totalHours = hours;
            this.note = nt;
        }

        public string AttendanceId
        {
            get { return attendanceId; }
            set { attendanceId = value; }
        }

        public string EmployeeId
        {
            get { return employeeId; }
            set { employeeId = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public bool IsPresent
        {
            get { return isPresent; }
            set { isPresent = value; }
        }

        public double TotalHours
        {
            get { return totalHours; }
            set { totalHours = value; }
        }

        public string Note
        {
            get { return note; }
            set { note = value; }
        }

        public void DisplayAttendance()
        {
            Console.WriteLine($"Ngay: {date:dd/MM/yyyy}");
            Console.WriteLine($"Trang thai: {(isPresent ? "Co mat" : "Vang mat")}");
            Console.WriteLine($"Tong gio: {totalHours:F2}");
            if (!string.IsNullOrEmpty(note))
            {
                Console.WriteLine($"Ghi chu: {note}");
            }
        }
    }
}