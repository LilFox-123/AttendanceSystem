using System;

namespace AttendanceSystem
{
    // ==================== EMPLOYEE - INHERITANCE + IDISPOSABLE ====================
    // Kế thừa từ Person, implement IDisposable để quản lý tài nguyên

    public class Employee : Person, IDisposable
    {
        private string departmentId;
        private string position;
        private decimal baseSalary;
        private DateTime hireDate;
        private string employeeType;
        private bool isActive;
        private bool disposed = false;

        public Employee() : base()
        {
            this.departmentId = "";
            this.position = "";
            this.baseSalary = 0;
            this.hireDate = DateTime.Now;
            this.employeeType = "Monthly";
            this.isActive = true;
        }

        public Employee(string id, string name, DateTime dob, string phone, string address,
                       string deptId, string pos, decimal salary, DateTime hire, string empType)
            : base(id, name, dob, phone, address)
        {
            this.departmentId = deptId;
            this.position = pos;
            this.baseSalary = salary;
            this.hireDate = hire;
            this.employeeType = empType;
            this.isActive = true;
        }

        public string DepartmentId
        {
            get { return departmentId; }
            set { departmentId = value; }
        }

        public string Position
        {
            get { return position; }
            set { position = value; }
        }

        public decimal BaseSalary
        {
            get { return baseSalary; }
            set { baseSalary = value; }
        }

        public DateTime HireDate
        {
            get { return hireDate; }
            set { hireDate = value; }
        }

        public string EmployeeType
        {
            get { return employeeType; }
            set { employeeType = value; }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public void DisplayEmployeeInfo()
        {
            DisplayInfo();
            Console.WriteLine($"Phong ban: {departmentId}");
            Console.WriteLine($"Chuc vu: {position}");
            Console.WriteLine($"Luong co ban: {baseSalary:N0} VND");
            Console.WriteLine($"Ngay vao lam: {hireDate:dd/MM/yyyy}");
            Console.WriteLine($"Loai nhan vien: {employeeType}");
            Console.WriteLine($"Trang thai: {(isActive ? "Dang lam viec" : "Da nghi viec")}");
        }

        public decimal GetBaseSalary()
        {
            return baseSalary;
        }

        public string GetEmployeeType()
        {
            return employeeType;
        }

        // IDisposable implementation cho chức năng xóa nhân viên
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Console.WriteLine($"\n=== DANG XOA NHAN VIEN ===");
                    Console.WriteLine($"Giai phong tai nguyen cho: {FullName}");
                    Console.WriteLine($"Ma nhan vien: {PersonId}");
                    isActive = false;
                    Console.WriteLine("Hoan thanh xoa nhan vien!");
                }
                disposed = true;
            }
        }

        ~Employee()
        {
            Dispose(false);
        }
    }
}