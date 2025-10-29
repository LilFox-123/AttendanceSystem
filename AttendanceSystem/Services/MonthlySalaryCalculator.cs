using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem
{
    public class MonthlySalaryCalculator : ISalaryCalculator
    {
        private const int STANDARD_WORK_DAYS = 26;

        public decimal CalculateSalary(decimal baseSalary, int workDays, double totalHours)
        {
            decimal salaryPerDay = baseSalary / STANDARD_WORK_DAYS;
            return salaryPerDay * workDays;
        }

        public string GetCalculationType()
        {
            return "Tinh luong theo thang (26 ngay chuan)";
        }
    }
}
