using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem
{
    // ==================== 9. STRATEGY PATTERN - SALARY CALCULATION ====================

    public interface ISalaryCalculator
    {
        decimal CalculateSalary(decimal baseSalary, int workDays, double totalHours);
        string GetCalculationType();
    }

}
