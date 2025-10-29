using System;
using System.Collections.Generic;

namespace AttendanceSystem
{
    // ==================== MANAGER - INHERITANCE FROM EMPLOYEE ====================
    // Kế thừa từ Employee, quản lý một nhóm nhân viên

    public class Manager : Employee
    {
        private decimal managementAllowance;
        private int teamSize;
        private List<string> teamMemberIds; // Quan hệ: Manager quản lý nhiều Employee

        public Manager() : base()
        {
            this.managementAllowance = 0;
            this.teamSize = 0;
            this.teamMemberIds = new List<string>();
        }

        public Manager(string id, string name, DateTime dob, string phone, string address,
                      string deptId, string pos, decimal salary, DateTime hire, string empType,
                      decimal allowance, int team)
            : base(id, name, dob, phone, address, deptId, pos, salary, hire, empType)
        {
            this.managementAllowance = allowance;
            this.teamSize = team;
            this.teamMemberIds = new List<string>();
        }

        public decimal ManagementAllowance
        {
            get { return managementAllowance; }
            set { managementAllowance = value; }
        }

        public int TeamSize
        {
            get { return teamSize; }
            set { teamSize = value; }
        }

        public List<string> TeamMemberIds
        {
            get { return teamMemberIds; }
            set { teamMemberIds = value; }
        }

        public void AddTeamMember(string employeeId)
        {
            if (!teamMemberIds.Contains(employeeId))
            {
                teamMemberIds.Add(employeeId);
                Console.WriteLine($"Da them nhan vien {employeeId} vao nhom quan ly");
            }
        }

        public void RemoveTeamMember(string employeeId)
        {
            if (teamMemberIds.Contains(employeeId))
            {
                teamMemberIds.Remove(employeeId);
                Console.WriteLine($"Da xoa nhan vien {employeeId} khoi nhom quan ly");
            }
        }

        public void DisplayManagerInfo()
        {
            DisplayEmployeeInfo();
            Console.WriteLine($"Phu cap quan ly: {managementAllowance:N0} VND");
            Console.WriteLine($"So luong nhan vien quan ly: {teamSize}");
            Console.WriteLine($"Danh sach nhan vien trong nhom: {teamMemberIds.Count}");
        }

        public decimal GetTotalBaseSalary()
        {
            return BaseSalary + managementAllowance;
        }
    }
}