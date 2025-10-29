using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem
{
    // ==================== 4. DEPARTMENT ====================

    public class Department
    {
        private string departmentId;
        private string departmentName;
        private string managerId;
        private List<string> employeeIds; // Quan hệ: Department có nhiều Employee

        public Department()
        {
            this.departmentId = "";
            this.departmentName = "";
            this.managerId = "";
            this.employeeIds = new List<string>();
        }

        public Department(string id, string name, string mgrId)
        {
            this.departmentId = id;
            this.departmentName = name;
            this.managerId = mgrId;
            this.employeeIds = new List<string>();
        }

        public string DepartmentId
        {
            get { return departmentId; }
            set { departmentId = value; }
        }

        public string DepartmentName
        {
            get { return departmentName; }
            set { departmentName = value; }
        }

        public string ManagerId
        {
            get { return managerId; }
            set { managerId = value; }
        }

        public List<string> EmployeeIds
        {
            get { return employeeIds; }
            set { employeeIds = value; }
        }

        public void AddEmployee(string employeeId)
        {
            if (!employeeIds.Contains(employeeId))
            {
                employeeIds.Add(employeeId);
            }
        }

        public void RemoveEmployee(string employeeId)
        {
            if (employeeIds.Contains(employeeId))
            {
                employeeIds.Remove(employeeId);
            }
        }

        public int GetEmployeeCount()
        {
            return employeeIds.Count;
        }
    }
}
