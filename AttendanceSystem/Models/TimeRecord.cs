using System;

namespace AttendanceSystem
{
    // ==================== TIME RECORD ====================
    // Ghi nhận thời gian check-in và check-out của nhân viên

    public class TimeRecord
    {
        private string recordId;
        private string employeeId;
        private DateTime checkInTime;
        private DateTime checkOutTime;
        private string shiftId;
        private bool isCheckedOut;

        public TimeRecord()
        {
            this.recordId = "";
            this.employeeId = "";
            this.checkInTime = DateTime.Now;
            this.checkOutTime = DateTime.Now;
            this.shiftId = "";
            this.isCheckedOut = false;
        }

        public TimeRecord(string recId, string empId, DateTime checkIn, string shift)
        {
            this.recordId = recId;
            this.employeeId = empId;
            this.checkInTime = checkIn;
            this.checkOutTime = DateTime.MinValue;
            this.shiftId = shift;
            this.isCheckedOut = false;
        }

        public string RecordId
        {
            get { return recordId; }
            set { recordId = value; }
        }

        public string EmployeeId
        {
            get { return employeeId; }
            set { employeeId = value; }
        }

        public DateTime CheckInTime
        {
            get { return checkInTime; }
            set { checkInTime = value; }
        }

        public DateTime CheckOutTime
        {
            get { return checkOutTime; }
            set { checkOutTime = value; }
        }

        public string ShiftId
        {
            get { return shiftId; }
            set { shiftId = value; }
        }

        public bool IsCheckedOut
        {
            get { return isCheckedOut; }
            set { isCheckedOut = value; }
        }

        public void CheckOut(DateTime time)
        {
            this.checkOutTime = time;
            this.isCheckedOut = true;
        }

        public double CalculateWorkHours()
        {
            if (!isCheckedOut)
                return 0;

            TimeSpan duration = checkOutTime - checkInTime;
            return duration.TotalHours;
        }

        public void DisplayRecord()
        {
            Console.WriteLine($"Ma ban ghi: {recordId}");
            Console.WriteLine($"Check in: {checkInTime:dd/MM/yyyy HH:mm:ss}");
            if (isCheckedOut)
            {
                Console.WriteLine($"Check out: {checkOutTime:dd/MM/yyyy HH:mm:ss}");
                Console.WriteLine($"So gio lam: {CalculateWorkHours():F2}");
            }
            else
            {
                Console.WriteLine("Chua check out");
            }
        }
    }
}