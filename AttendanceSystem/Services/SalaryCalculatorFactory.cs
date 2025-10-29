using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem
{
    // ==================== 10. FACTORY PATTERN - SALARY CALCULATOR FACTORY ====================

    public class SalaryCalculatorFactory
    {
        public ISalaryCalculator CreateCalculator(string employeeType)
        {
            if (employeeType == "Monthly")
            {
                return new MonthlySalaryCalculator();
            }
            else if (employeeType == "Hourly")
            {
                return new HourlySalaryCalculator();
            }
            else
            {
                return new MonthlySalaryCalculator();
            }
        }
    }
}
