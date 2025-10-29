using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem
{
   
    public class HourlySalaryCalculator : ISalaryCalculator
    {
        private const double STANDARD_HOURS_PER_DAY = 8.0;
        private const int STANDARD_WORK_DAYS = 26;

        public decimal CalculateSalary(decimal baseSalary, int workDays, double totalHours)
        {
            decimal hourlyRate = baseSalary / (STANDARD_WORK_DAYS * (decimal)STANDARD_HOURS_PER_DAY);
            return hourlyRate * (decimal)totalHours;
        }

        public string GetCalculationType()
        {
            return "Tinh luong theo gio";
        }
    }
}
