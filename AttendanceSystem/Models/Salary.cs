using System;

namespace AttendanceSystem
{
    // ==================== SALARY ====================
    // Lưu trữ thông tin bảng lương hàng tháng

    public class Salary
    {
        private string salaryId;
        private string employeeId;
        private int month;
        private int year;
        private decimal baseSalary;
        private decimal bonus;
        private decimal deduction;
        private int workDays;
        private decimal totalSalary;

        public Salary()
        {
            this.salaryId = "";
            this.employeeId = "";
            this.month = 1;
            this.year = 2024;
            this.baseSalary = 0;
            this.bonus = 0;
            this.deduction = 0;
            this.workDays = 0;
            this.totalSalary = 0;
        }

        public Salary(string salId, string empId, int m, int y, decimal base_sal,
                     decimal bon, decimal ded, int days)
        {
            this.salaryId = salId;
            this.employeeId = empId;
            this.month = m;
            this.year = y;
            this.baseSalary = base_sal;
            this.bonus = bon;
            this.deduction = ded;
            this.workDays = days;
            CalculateTotalSalary();
        }

        public string SalaryId
        {
            get { return salaryId; }
            set { salaryId = value; }
        }

        public string EmployeeId
        {
            get { return employeeId; }
            set { employeeId = value; }
        }

        public int Month
        {
            get { return month; }
            set { month = value; }
        }

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        public decimal BaseSalary
        {
            get { return baseSalary; }
            set { baseSalary = value; }
        }

        public decimal Bonus
        {
            get { return bonus; }
            set { bonus = value; }
        }

        public decimal Deduction
        {
            get { return deduction; }
            set { deduction = value; }
        }

        public int WorkDays
        {
            get { return workDays; }
            set { workDays = value; }
        }

        public decimal TotalSalary
        {
            get { return totalSalary; }
            set { totalSalary = value; }
        }

        public void CalculateTotalSalary()
        {
            totalSalary = baseSalary + bonus - deduction;
        }

        public decimal GetTotalSalary()
        {
            return totalSalary;
        }

        public void DisplaySalary()
        {
            Console.WriteLine($"Ma bang luong: {salaryId}");
            Console.WriteLine($"Ma nhan vien: {employeeId}");
            Console.WriteLine($"Thang: {month}/{year}");
            Console.WriteLine($"Luong co ban: {baseSalary:N0} VND");
            Console.WriteLine($"Thuong: {bonus:N0} VND");
            Console.WriteLine($"Khau tru: {deduction:N0} VND");
            Console.WriteLine($"So ngay lam viec: {workDays}");
            Console.WriteLine($"TONG LUONG: {totalSalary:N0} VND");
        }
    }
}