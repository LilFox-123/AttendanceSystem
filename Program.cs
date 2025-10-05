using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace AttendanceSystem
{
    // ==================== 1. ABSTRACT CLASS & BASE CLASS ====================

    public abstract class Person : ISerializable
    {
        private string id;
        private string name;
        private DateTime dateOfBirth;
        private string phoneNumber;
        private string address;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public Person()
        {
        }

        public Person(string id, string name, DateTime dob, string phone, string address)
        {
            this.id = id;
            this.name = name;
            this.dateOfBirth = dob;
            this.phoneNumber = phone;
            this.address = address;
        }

        protected Person(SerializationInfo info, StreamingContext context)
        {
            this.id = info.GetString("Id");
            this.name = info.GetString("Name");
            this.dateOfBirth = info.GetDateTime("DateOfBirth");
            this.phoneNumber = info.GetString("PhoneNumber");
            this.address = info.GetString("Address");
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", id);
            info.AddValue("Name", name);
            info.AddValue("DateOfBirth", dateOfBirth);
            info.AddValue("PhoneNumber", phoneNumber);
            info.AddValue("Address", address);
        }

        public abstract void DisplayInfo();

        public virtual int GetAge()
        {
            return DateTime.Now.Year - dateOfBirth.Year;
        }
    }

    // ==================== 2. EMPLOYEE CLASS ====================

    public class Employee : Person
    {
        private string departmentId;
        private string position;
        private decimal baseSalary;
        private DateTime hireDate;
        private string employeeType;

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
            set
            {
                if (value >= 0)
                    baseSalary = value;
            }
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

        public Employee() : base()
        {
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
        }

        protected Employee(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.departmentId = info.GetString("DepartmentId");
            this.position = info.GetString("Position");
            this.baseSalary = info.GetDecimal("BaseSalary");
            this.hireDate = info.GetDateTime("HireDate");
            this.employeeType = info.GetString("EmployeeType");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("DepartmentId", departmentId);
            info.AddValue("Position", position);
            info.AddValue("BaseSalary", baseSalary);
            info.AddValue("HireDate", hireDate);
            info.AddValue("EmployeeType", employeeType);
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"ID: {Id} | Ten: {Name} | Chuc vu: {Position}");
            Console.WriteLine($"Phong ban: {DepartmentId} | Luong co ban: {BaseSalary:N0} VND");
            Console.WriteLine($"Loai nhan vien: {EmployeeType} | Ngay vao lam: {HireDate:dd/MM/yyyy}");
        }

        public virtual decimal CalculateBonus(int workDays)
        {
            if (workDays >= 26)
                return BaseSalary * 0.1m;
            return 0;
        }
    }

    // ==================== 3. MANAGER CLASS ====================

    public class Manager : Employee
    {
        private decimal managementAllowance;
        private int teamSize;

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

        public Manager() : base()
        {
        }

        public Manager(string id, string name, DateTime dob, string phone, string address,
                      string deptId, string pos, decimal salary, DateTime hire, string empType,
                      decimal allowance, int team)
            : base(id, name, dob, phone, address, deptId, pos, salary, hire, empType)
        {
            this.managementAllowance = allowance;
            this.teamSize = team;
        }

        protected Manager(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.managementAllowance = info.GetDecimal("ManagementAllowance");
            this.teamSize = info.GetInt32("TeamSize");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("ManagementAllowance", managementAllowance);
            info.AddValue("TeamSize", teamSize);
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Phu cap quan ly: {ManagementAllowance:N0} VND | So luong nhan vien: {TeamSize}");
        }

        public override decimal CalculateBonus(int workDays)
        {
            decimal baseBonus = base.CalculateBonus(workDays);
            decimal teamBonus = TeamSize * 500000m;
            return baseBonus + teamBonus;
        }
    }

    // ==================== 4. DEPARTMENT CLASS ====================

    public class Department : ISerializable
    {
        private string departmentId;
        private string departmentName;
        private string managerId;
        private List<string> employeeIds;

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

        public Department()
        {
            employeeIds = new List<string>();
        }

        public Department(string id, string name, string managerId)
        {
            this.departmentId = id;
            this.departmentName = name;
            this.managerId = managerId;
            this.employeeIds = new List<string>();
        }

        protected Department(SerializationInfo info, StreamingContext context)
        {
            this.departmentId = info.GetString("DepartmentId");
            this.departmentName = info.GetString("DepartmentName");
            this.managerId = info.GetString("ManagerId");
            this.employeeIds = (List<string>)info.GetValue("EmployeeIds", typeof(List<string>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("DepartmentId", departmentId);
            info.AddValue("DepartmentName", departmentName);
            info.AddValue("ManagerId", managerId);
            info.AddValue("EmployeeIds", employeeIds);
        }

        public void AddEmployee(string empId)
        {
            if (!employeeIds.Contains(empId))
            {
                employeeIds.Add(empId);
            }
        }

        public void RemoveEmployee(string empId)
        {
            employeeIds.Remove(empId);
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Ma phong ban: {DepartmentId}");
            Console.WriteLine($"Ten phong ban: {DepartmentName}");
            Console.WriteLine($"Truong phong: {ManagerId}");
            Console.WriteLine($"So nhan vien: {EmployeeIds.Count}");
        }
    }

    // ==================== 5. WORKSHIFT CLASS ====================

    public class WorkShift : ISerializable
    {
        private string shiftId;
        private string shiftName;
        private TimeSpan startTime;
        private TimeSpan endTime;
        private decimal hourlyRate;

        public string ShiftId
        {
            get { return shiftId; }
            set { shiftId = value; }
        }

        public string ShiftName
        {
            get { return shiftName; }
            set { shiftName = value; }
        }

        public TimeSpan StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        public TimeSpan EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        public decimal HourlyRate
        {
            get { return hourlyRate; }
            set { hourlyRate = value; }
        }

        public WorkShift()
        {
        }

        public WorkShift(string id, string name, TimeSpan start, TimeSpan end, decimal rate)
        {
            this.shiftId = id;
            this.shiftName = name;
            this.startTime = start;
            this.endTime = end;
            this.hourlyRate = rate;
        }

        protected WorkShift(SerializationInfo info, StreamingContext context)
        {
            this.shiftId = info.GetString("ShiftId");
            this.shiftName = info.GetString("ShiftName");
            this.startTime = new TimeSpan(info.GetInt64("StartTimeTicks"));
            this.endTime = new TimeSpan(info.GetInt64("EndTimeTicks"));
            this.hourlyRate = info.GetDecimal("HourlyRate");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ShiftId", shiftId);
            info.AddValue("ShiftName", shiftName);
            info.AddValue("StartTimeTicks", startTime.Ticks);
            info.AddValue("EndTimeTicks", endTime.Ticks);
            info.AddValue("HourlyRate", hourlyRate);
        }

        public double CalculateShiftHours()
        {
            TimeSpan duration = endTime - startTime;
            if (duration.TotalHours < 0)
            {
                duration = duration.Add(TimeSpan.FromHours(24));
            }
            return duration.TotalHours;
        }
    }

    // ==================== 6. TIMERECORD CLASS ====================

    public class TimeRecord : ISerializable
    {
        private string recordId;
        private string employeeId;
        private DateTime checkInTime;
        private DateTime checkOutTime;
        private bool isCheckedOut;
        private string shiftId;

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

        public bool IsCheckedOut
        {
            get { return isCheckedOut; }
            set { isCheckedOut = value; }
        }

        public string ShiftId
        {
            get { return shiftId; }
            set { shiftId = value; }
        }

        public TimeRecord()
        {
        }

        public TimeRecord(string recId, string empId, DateTime checkIn, string shift)
        {
            this.recordId = recId;
            this.employeeId = empId;
            this.checkInTime = checkIn;
            this.isCheckedOut = false;
            this.shiftId = shift;
        }

        protected TimeRecord(SerializationInfo info, StreamingContext context)
        {
            this.recordId = info.GetString("RecordId");
            this.employeeId = info.GetString("EmployeeId");
            this.checkInTime = info.GetDateTime("CheckInTime");
            this.checkOutTime = info.GetDateTime("CheckOutTime");
            this.isCheckedOut = info.GetBoolean("IsCheckedOut");
            this.shiftId = info.GetString("ShiftId");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("RecordId", recordId);
            info.AddValue("EmployeeId", employeeId);
            info.AddValue("CheckInTime", checkInTime);
            info.AddValue("CheckOutTime", checkOutTime);
            info.AddValue("IsCheckedOut", isCheckedOut);
            info.AddValue("ShiftId", shiftId);
        }

        public void CheckOut(DateTime time)
        {
            if (!this.isCheckedOut)
            {
                this.checkOutTime = time;
                this.isCheckedOut = true;
            }
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
            Console.WriteLine($"Ma: {RecordId} | NV: {EmployeeId}");
            Console.WriteLine($"Gio vao: {CheckInTime:dd/MM/yyyy HH:mm}");
            if (isCheckedOut)
            {
                Console.WriteLine($"Gio ra: {CheckOutTime:dd/MM/yyyy HH:mm}");
                Console.WriteLine($"Tong gio: {CalculateWorkHours():F2} gio");
            }
            else
            {
                Console.WriteLine("Chua check out");
            }
        }
    }

    // ==================== 7. ATTENDANCE CLASS ====================

    public class Attendance : ISerializable
    {
        private string attendanceId;
        private string employeeId;
        private DateTime date;
        private double totalHours;
        private bool isPresent;
        private string note;

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

        public double TotalHours
        {
            get { return totalHours; }
            set { totalHours = value; }
        }

        public bool IsPresent
        {
            get { return isPresent; }
            set { isPresent = value; }
        }

        public string Note
        {
            get { return note; }
            set { note = value; }
        }

        public Attendance()
        {
        }

        public Attendance(string attId, string empId, DateTime dt, double hours, bool present, string nt)
        {
            this.attendanceId = attId;
            this.employeeId = empId;
            this.date = dt;
            this.totalHours = hours;
            this.isPresent = present;
            this.note = nt;
        }

        protected Attendance(SerializationInfo info, StreamingContext context)
        {
            this.attendanceId = info.GetString("AttendanceId");
            this.employeeId = info.GetString("EmployeeId");
            this.date = info.GetDateTime("Date");
            this.totalHours = info.GetDouble("TotalHours");
            this.isPresent = info.GetBoolean("IsPresent");
            this.note = info.GetString("Note");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("AttendanceId", attendanceId);
            info.AddValue("EmployeeId", employeeId);
            info.AddValue("Date", date);
            info.AddValue("TotalHours", totalHours);
            info.AddValue("IsPresent", isPresent);
            info.AddValue("Note", note);
        }

        public void DisplayAttendance()
        {
            Console.WriteLine($"Ma cham cong: {AttendanceId} | NV: {EmployeeId}");
            Console.WriteLine($"Ngay: {Date:dd/MM/yyyy} | Gio lam: {TotalHours:F2}h");
            Console.WriteLine($"Trang thai: {(IsPresent ? "Co mat" : "Vang")} | Ghi chu: {Note}");
        }
    }

    // ==================== 8. SALARY CLASS ====================

    public class Salary : ISerializable
    {
        private string salaryId;
        private string employeeId;
        private int month;
        private int year;
        private decimal baseSalary;
        private decimal bonus;
        private decimal deduction;
        private decimal totalSalary;
        private int workDays;

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

        public decimal TotalSalary
        {
            get { return totalSalary; }
            set { totalSalary = value; }
        }

        public int WorkDays
        {
            get { return workDays; }
            set { workDays = value; }
        }

        public Salary()
        {
        }

        public Salary(string salId, string empId, int m, int y, decimal baseSal, decimal bon, decimal ded, int days)
        {
            this.salaryId = salId;
            this.employeeId = empId;
            this.month = m;
            this.year = y;
            this.baseSalary = baseSal;
            this.bonus = bon;
            this.deduction = ded;
            this.workDays = days;
            CalculateTotalSalary();
        }

        protected Salary(SerializationInfo info, StreamingContext context)
        {
            this.salaryId = info.GetString("SalaryId");
            this.employeeId = info.GetString("EmployeeId");
            this.month = info.GetInt32("Month");
            this.year = info.GetInt32("Year");
            this.baseSalary = info.GetDecimal("BaseSalary");
            this.bonus = info.GetDecimal("Bonus");
            this.deduction = info.GetDecimal("Deduction");
            this.totalSalary = info.GetDecimal("TotalSalary");
            this.workDays = info.GetInt32("WorkDays");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("SalaryId", salaryId);
            info.AddValue("EmployeeId", employeeId);
            info.AddValue("Month", month);
            info.AddValue("Year", year);
            info.AddValue("BaseSalary", baseSalary);
            info.AddValue("Bonus", bonus);
            info.AddValue("Deduction", deduction);
            info.AddValue("TotalSalary", totalSalary);
            info.AddValue("WorkDays", workDays);
        }

        public void CalculateTotalSalary()
        {
            this.totalSalary = baseSalary + bonus - deduction;
        }

        public void DisplaySalary()
        {
            Console.WriteLine($"Ma luong: {SalaryId} | NV: {EmployeeId}");
            Console.WriteLine($"Thang: {Month}/{Year} | So ngay lam: {WorkDays}");
            Console.WriteLine($"Luong co ban: {BaseSalary:N0} VND");
            Console.WriteLine($"Thuong: {Bonus:N0} VND | Khau tru: {Deduction:N0} VND");
            Console.WriteLine($"TONG LUONG: {TotalSalary:N0} VND");
        }
    }

    // ==================== 9. STRATEGY PATTERN - INTERFACE ====================

    public interface ISalaryCalculator
    {
        decimal Calculate(Employee employee, int workDays, double totalHours);
        string GetCalculationType();
    }

    // ==================== 10. MONTHLY SALARY CALCULATOR ====================

    public class MonthlySalaryCalculator : ISalaryCalculator
    {
        public decimal Calculate(Employee employee, int workDays, double totalHours)
        {
            decimal dailySalary = employee.BaseSalary / 26;
            decimal calculatedSalary = dailySalary * workDays;
            return calculatedSalary;
        }

        public string GetCalculationType()
        {
            return "Luong Thang";
        }
    }

    // ==================== 11. HOURLY SALARY CALCULATOR ====================

    public class HourlySalaryCalculator : ISalaryCalculator
    {
        private decimal hourlyRate;

        public HourlySalaryCalculator(decimal rate)
        {
            this.hourlyRate = rate;
        }

        public decimal Calculate(Employee employee, int workDays, double totalHours)
        {
            decimal calculatedSalary = (decimal)totalHours * hourlyRate;
            return calculatedSalary;
        }

        public string GetCalculationType()
        {
            return "Luong Theo Gio";
        }
    }

    // ==================== 12. FACTORY PATTERN ====================

    public class SalaryCalculatorFactory
    {
        public static ISalaryCalculator CreateCalculator(string employeeType)
        {
            if (employeeType == "Monthly")
            {
                return new MonthlySalaryCalculator();
            }
            else if (employeeType == "Hourly")
            {
                return new HourlySalaryCalculator(50000);
            }
            else
            {
                return new MonthlySalaryCalculator();
            }
        }
    }

    // ==================== 13. SINGLETON PATTERN - ATTENDANCE MANAGER ====================

    public class AttendanceManager
    {
        private static AttendanceManager instance;
        private List<Employee> employees;
        private List<TimeRecord> timeRecords;
        private List<Attendance> attendances;
        private List<Department> departments;
        private List<Salary> salaries;
        private List<WorkShift> workShifts;

        private AttendanceManager()
        {
            employees = new List<Employee>();
            timeRecords = new List<TimeRecord>();
            attendances = new List<Attendance>();
            departments = new List<Department>();
            salaries = new List<Salary>();
            workShifts = new List<WorkShift>();
            InitializeDefaultData();
        }

        public static AttendanceManager GetInstance()
        {
            if (instance == null)
            {
                instance = new AttendanceManager();
            }
            return instance;
        }

        private void InitializeDefaultData()
        {
            // Default WorkShifts
            WorkShift morning = new WorkShift("SH001", "Ca Sang", new TimeSpan(8, 0, 0), new TimeSpan(17, 0, 0), 50000);
            WorkShift evening = new WorkShift("SH002", "Ca Chieu", new TimeSpan(13, 0, 0), new TimeSpan(22, 0, 0), 60000);
            workShifts.Add(morning);
            workShifts.Add(evening);

            // Default Departments
            Department dept1 = new Department("DEPT001", "Phong Ky Thuat", "EMP003");
            Department dept2 = new Department("DEPT002", "Phong Kinh Doanh", "");
            departments.Add(dept1);
            departments.Add(dept2);

            // Default Employees
            Employee emp1 = new Employee("EMP001", "Nguyen Van A", new DateTime(1990, 1, 1), "0123456789", "Ha Noi",
                                         "DEPT001", "Nhan vien", 10000000, new DateTime(2020, 1, 1), "Monthly");
            Employee emp2 = new Employee("EMP002", "Tran Thi B", new DateTime(1995, 5, 5), "0987654321", "TP HCM",
                                         "DEPT002", "Nhan vien", 8000000, new DateTime(2021, 6, 1), "Hourly");
            Manager mgr1 = new Manager("EMP003", "Le Van C", new DateTime(1985, 3, 3), "0112233445", "Da Nang",
                                       "DEPT001", "Truong phong", 15000000, new DateTime(2018, 1, 1), "Monthly",
                                       5000000, 5);
            employees.Add(emp1);
            employees.Add(emp2);
            employees.Add(mgr1);

            // Add employees to departments
            dept1.AddEmployee("EMP001");
            dept1.AddEmployee("EMP003");
            dept2.AddEmployee("EMP002");

            // Default TimeRecords and Attendances for demo
            TimeRecord rec1 = new TimeRecord("REC001", "EMP001", new DateTime(2023, 10, 1, 8, 0, 0), "SH001");
            rec1.CheckOut(new DateTime(2023, 10, 1, 17, 0, 0));
            timeRecords.Add(rec1);
            Attendance att1 = new Attendance("ATT001", "EMP001", new DateTime(2023, 10, 1), rec1.CalculateWorkHours(), true, "Binh thuong");
            attendances.Add(att1);
        }

        public void AddEmployee(Employee emp)
        {
            employees.Add(emp);
            Department dept = FindDepartmentById(emp.DepartmentId);
            if (dept != null)
            {
                dept.AddEmployee(emp.Id);
                if (emp is Manager)
                {
                    dept.ManagerId = emp.Id;
                }
            }
            Console.WriteLine("Them nhan vien thanh cong!");
        }

        public void RemoveEmployee(string empId)
        {
            Employee emp = FindEmployeeById(empId);
            if (emp != null)
            {
                employees.Remove(emp);
                Department dept = FindDepartmentById(emp.DepartmentId);
                if (dept != null)
                {
                    dept.RemoveEmployee(empId);
                    if (dept.ManagerId == empId)
                    {
                        dept.ManagerId = "";
                    }
                }
                Console.WriteLine("Xoa nhan vien thanh cong!");
            }
            else
            {
                Console.WriteLine("Khong tim thay nhan vien!");
            }
        }

        public void UpdateEmployee(string empId, string newName, string newPhone)
        {
            Employee emp = FindEmployeeById(empId);
            if (emp != null)
            {
                emp.Name = newName;
                emp.PhoneNumber = newPhone;
                Console.WriteLine("Cap nhat nhan vien thanh cong!");
            }
            else
            {
                Console.WriteLine("Khong tim thay nhan vien!");
            }
        }

        public Employee FindEmployeeById(string id)
        {
            for (int i = 0; i < employees.Count; i++)
            {
                if (employees[i].Id == id)
                {
                    return employees[i];
                }
            }
            return null;
        }

        public Department FindDepartmentById(string deptId)
        {
            for (int i = 0; i < departments.Count; i++)
            {
                if (departments[i].DepartmentId == deptId)
                {
                    return departments[i];
                }
            }
            return null;
        }

        public void DisplayAllEmployees()
        {
            if (employees.Count == 0)
            {
                Console.WriteLine("Khong co nhan vien nao!");
                return;
            }

            Console.WriteLine("\n========== DANH SACH NHAN VIEN ==========");
            DrawEmployeeTableHeader();
            for (int i = 0; i < employees.Count; i++)
            {
                DrawEmployeeTableRow(employees[i]);
            }
            DrawTableFooter();
        }

        private void DrawEmployeeTableHeader()
        {
            Console.WriteLine("+----------+----------------------+----------------+--------------+------------+");
            Console.WriteLine("| ID       | Ten                  | Chuc vu        | Phong ban    | Luong CB   |");
            Console.WriteLine("+----------+----------------------+----------------+--------------+------------+");
        }

        private void DrawEmployeeTableRow(Employee emp)
        {
            string id = emp.Id.PadRight(8);
            string name = emp.Name.Length > 20 ? emp.Name.Substring(0, 17) + "..." : emp.Name.PadRight(20);
            string position = emp.Position.PadRight(14);
            string dept = emp.DepartmentId.PadRight(12);
            string salary = (emp.BaseSalary.ToString("N0") + " VND").PadRight(10);
            Console.WriteLine($"| {id} | {name} | {position} | {dept} | {salary} |");
        }

        private void DrawTableFooter()
        {
            Console.WriteLine("+----------+----------------------+----------------+--------------+------------+");
        }

        public void CheckIn(string empId, string shiftId)
        {
            Employee emp = FindEmployeeById(empId);
            if (emp == null)
            {
                Console.WriteLine("Khong tim thay nhan vien!");
                return;
            }

            if (FindWorkShiftById(shiftId) == null)
            {
                Console.WriteLine("Khong tim thay ca lam!");
                return;
            }

            string recordId = "REC" + DateTime.Now.ToString("yyyyMMddHHmmss");
            TimeRecord record = new TimeRecord(recordId, empId, DateTime.Now, shiftId);
            timeRecords.Add(record);
            Console.WriteLine($"Check in thanh cong luc {DateTime.Now:HH:mm:ss dd/MM/yyyy}");
        }

        public void CheckOut(string empId)
        {
            TimeRecord lastRecord = null;
            for (int i = timeRecords.Count - 1; i >= 0; i--)
            {
                if (timeRecords[i].EmployeeId == empId && !timeRecords[i].IsCheckedOut)
                {
                    lastRecord = timeRecords[i];
                    break;
                }
            }

            if (lastRecord == null)
            {
                Console.WriteLine("Khong tim thay ban ghi check in!");
                return;
            }

            lastRecord.CheckOut(DateTime.Now);
            Console.WriteLine($"Check out thanh cong luc {DateTime.Now:HH:mm:ss dd/MM/yyyy}");
            Console.WriteLine($"Tong thoi gian lam viec: {lastRecord.CalculateWorkHours():F2} gio");

            string attId = "ATT" + DateTime.Now.ToString("yyyyMMdd") + empId;
            Attendance att = new Attendance(attId, empId, DateTime.Now.Date, lastRecord.CalculateWorkHours(), true, "Binh thuong");
            attendances.Add(att);
        }

        public void DisplayTimeRecords(string empId)
        {
            Console.WriteLine($"\n========== LICH SU CHAM CONG CUA {empId} ==========");
            bool found = false;
            for (int i = 0; i < timeRecords.Count; i++)
            {
                if (timeRecords[i].EmployeeId == empId)
                {
                    Console.WriteLine($"\n--- Ban ghi {i + 1} ---");
                    timeRecords[i].DisplayRecord();
                    found = true;
                }
            }
            if (!found)
            {
                Console.WriteLine("Khong co ban ghi nao!");
            }
        }

        public void CalculateMonthlySalary(string empId, int month, int year)
        {
            Employee emp = FindEmployeeById(empId);
            if (emp == null)
            {
                Console.WriteLine("Khong tim thay nhan vien!");
                return;
            }

            int workDays = 0;
            double totalHours = 0;

            for (int i = 0; i < attendances.Count; i++)
            {
                if (attendances[i].EmployeeId == empId &&
                    attendances[i].Date.Month == month &&
                    attendances[i].Date.Year == year &&
                    attendances[i].IsPresent)
                {
                    workDays++;
                    totalHours += attendances[i].TotalHours;
                }
            }

            ISalaryCalculator calculator = SalaryCalculatorFactory.CreateCalculator(emp.EmployeeType);
            decimal baseSalary = calculator.Calculate(emp, workDays, totalHours);
            decimal bonus = emp.CalculateBonus(workDays);
            decimal deduction = 0;

            if (workDays < 22)
            {
                deduction = emp.BaseSalary * 0.05m;
            }

            string salaryId = "SAL" + month.ToString("D2") + year + empId;
            Salary salary = new Salary(salaryId, empId, month, year, baseSalary, bonus, deduction, workDays);
            salaries.Add(salary);

            Console.WriteLine("\n========== BANG LUONG ==========");
            salary.DisplaySalary();
            Console.WriteLine($"Loai tinh luong: {calculator.GetCalculationType()}");
        }

        public void DisplayAllSalaries()
        {
            if (salaries.Count == 0)
            {
                Console.WriteLine("Chua co bang luong nao!");
                return;
            }

            Console.WriteLine("\n========== TAT CA BANG LUONG ==========");
            for (int i = 0; i < salaries.Count; i++)
            {
                Console.WriteLine($"\n--- Bang luong {i + 1} ---");
                salaries[i].DisplaySalary();
            }
        }

        public void DisplayAttendanceReport(string empId, int month, int year)
        {
            Console.WriteLine($"\n========== BAO CAO CHAM CONG THANG {month}/{year} ==========");
            bool found = false;
            int presentDays = 0;
            double totalHours = 0;

            for (int i = 0; i < attendances.Count; i++)
            {
                if (attendances[i].EmployeeId == empId &&
                    attendances[i].Date.Month == month &&
                    attendances[i].Date.Year == year)
                {
                    Console.WriteLine($"\n--- Ngay {attendances[i].Date:dd/MM/yyyy} ---");
                    attendances[i].DisplayAttendance();
                    if (attendances[i].IsPresent)
                    {
                        presentDays++;
                        totalHours += attendances[i].TotalHours;
                    }
                    found = true;
                }
            }

            if (found)
            {
                Console.WriteLine("\n========== TONG KET ==========");
                Console.WriteLine($"Tong so ngay lam viec: {presentDays}");
                Console.WriteLine($"Tong so gio lam viec: {totalHours:F2} gio");
            }
            else
            {
                Console.WriteLine("Khong co du lieu cham cong!");
            }
        }

        public void SaveData(string filePath)
        {
            try
            {
                FileStream stream = new FileStream(filePath, FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();

                AttendanceData data = new AttendanceData();
                data.Employees = this.employees;
                data.TimeRecords = this.timeRecords;
                data.Attendances = this.attendances;
                data.Departments = this.departments;
                data.Salaries = this.salaries;
                data.WorkShifts = this.workShifts;

                formatter.Serialize(stream, data);
                stream.Close();
                Console.WriteLine("Luu du lieu thanh cong!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi khi luu du lieu: " + ex.Message);
            }
        }

        public void LoadData(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("File khong ton tai!");
                    return;
                }

                FileStream stream = new FileStream(filePath, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                AttendanceData data = (AttendanceData)formatter.Deserialize(stream);
                stream.Close();

                this.employees = data.Employees;
                this.timeRecords = data.TimeRecords;
                this.attendances = data.Attendances;
                this.departments = data.Departments;
                this.salaries = data.Salaries;
                this.workShifts = data.WorkShifts;

                Console.WriteLine("Tai du lieu thanh cong!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi khi tai du lieu: " + ex.Message);
            }
        }

        public List<WorkShift> GetWorkShifts()
        {
            return workShifts;
        }

        public WorkShift FindWorkShiftById(string shiftId)
        {
            for (int i = 0; i < workShifts.Count; i++)
            {
                if (workShifts[i].ShiftId == shiftId)
                {
                    return workShifts[i];
                }
            }
            return null;
        }

        public List<Department> GetDepartments()
        {
            return departments;
        }
    }

    // ==================== 14. DATA CONTAINER FOR SERIALIZATION ====================

    public class AttendanceData : ISerializable
    {
        private List<Employee> employees;
        private List<TimeRecord> timeRecords;
        private List<Attendance> attendances;
        private List<Department> departments;
        private List<Salary> salaries;
        private List<WorkShift> workShifts;

        public List<Employee> Employees
        {
            get { return employees; }
            set { employees = value; }
        }

        public List<TimeRecord> TimeRecords
        {
            get { return timeRecords; }
            set { timeRecords = value; }
        }

        public List<Attendance> Attendances
        {
            get { return attendances; }
            set { attendances = value; }
        }

        public List<Department> Departments
        {
            get { return departments; }
            set { departments = value; }
        }

        public List<Salary> Salaries
        {
            get { return salaries; }
            set { salaries = value; }
        }

        public List<WorkShift> WorkShifts
        {
            get { return workShifts; }
            set { workShifts = value; }
        }

        public AttendanceData()
        {
            employees = new List<Employee>();
            timeRecords = new List<TimeRecord>();
            attendances = new List<Attendance>();
            departments = new List<Department>();
            salaries = new List<Salary>();
            workShifts = new List<WorkShift>();
        }

        protected AttendanceData(SerializationInfo info, StreamingContext context)
        {
            employees = (List<Employee>)info.GetValue("Employees", typeof(List<Employee>));
            timeRecords = (List<TimeRecord>)info.GetValue("TimeRecords", typeof(List<TimeRecord>));
            attendances = (List<Attendance>)info.GetValue("Attendances", typeof(List<Attendance>));
            departments = (List<Department>)info.GetValue("Departments", typeof(List<Department>));
            salaries = (List<Salary>)info.GetValue("Salaries", typeof(List<Salary>));
            workShifts = (List<WorkShift>)info.GetValue("WorkShifts", typeof(List<WorkShift>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Employees", employees);
            info.AddValue("TimeRecords", timeRecords);
            info.AddValue("Attendances", attendances);
            info.AddValue("Departments", departments);
            info.AddValue("Salaries", salaries);
            info.AddValue("WorkShifts", workShifts);
        }
    }

    // ==================== 15. MAIN PROGRAM ====================

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            AttendanceManager manager = AttendanceManager.GetInstance();

            bool running = true;
            while (running)
            {
                DisplayMenu();
                Console.Write("Chon chuc nang: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddEmployeeMenu(manager);
                        break;
                    case "2":
                        manager.DisplayAllEmployees();
                        break;
                    case "3":
                        CheckInMenu(manager);
                        break;
                    case "4":
                        CheckOutMenu(manager);
                        break;
                    case "5":
                        ViewTimeRecordsMenu(manager);
                        break;
                    case "6":
                        CalculateSalaryMenu(manager);
                        break;
                    case "7":
                        manager.DisplayAllSalaries();
                        break;
                    case "8":
                        ViewAttendanceReportMenu(manager);
                        break;
                    case "9":
                        SaveDataMenu(manager);
                        break;
                    case "10":
                        LoadDataMenu(manager);
                        break;
                    case "11":
                        RemoveEmployeeMenu(manager);
                        break;
                    case "12":
                        UpdateEmployeeMenu(manager);
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("Tam biet!");
                        break;
                    default:
                        Console.WriteLine("Lua chon khong hop le!");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nNhan phim bat ky de tiep tuc...");
                    Console.ReadKey();
                }
            }
        }

        static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║     HE THONG QUAN LY CHAM CONG - TINH LUONG    ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.WriteLine("║  1. Them nhan vien moi                         ║");
            Console.WriteLine("║  2. Xem danh sach nhan vien                    ║");
            Console.WriteLine("║  3. Check in                                   ║");
            Console.WriteLine("║  4. Check out                                  ║");
            Console.WriteLine("║  5. Xem lich su cham cong                      ║");
            Console.WriteLine("║  6. Tinh luong thang                           ║");
            Console.WriteLine("║  7. Xem tat ca bang luong                      ║");
            Console.WriteLine("║  8. Bao cao cham cong                          ║");
            Console.WriteLine("║  9. Luu du lieu                                ║");
            Console.WriteLine("║ 10. Tai du lieu                                ║");
            Console.WriteLine("║ 11. Xoa nhan vien                              ║");
            Console.WriteLine("║ 12. Cap nhat nhan vien                         ║");
            Console.WriteLine("║  0. Thoat                                      ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
        }

        static void AddEmployeeMenu(AttendanceManager manager)
        {
            Console.Clear();
            Console.WriteLine("========== THEM NHAN VIEN MOI ==========");

            try
            {
                Console.Write("Ma nhan vien: ");
                string id = Console.ReadLine();

                Console.Write("Ho ten: ");
                string name = Console.ReadLine();

                Console.Write("Ngay sinh (dd/MM/yyyy): ");
                DateTime dob = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

                Console.Write("So dien thoai: ");
                string phone = Console.ReadLine();

                Console.Write("Dia chi: ");
                string address = Console.ReadLine();

                Console.WriteLine("\nDanh sach phong ban:");
                List<Department> departments = manager.GetDepartments();
                for (int i = 0; i < departments.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {departments[i].DepartmentId} - {departments[i].DepartmentName}");
                }
                Console.Write("Chon phong ban (nhap ma): ");
                string deptId = Console.ReadLine();

                if (manager.FindDepartmentById(deptId) == null)
                {
                    Console.WriteLine("Phong ban khong ton tai!");
                    return;
                }

                Console.Write("Chuc vu: ");
                string position = Console.ReadLine();

                Console.Write("Luong co ban: ");
                decimal baseSalary = decimal.Parse(Console.ReadLine());

                Console.Write("Ngay vao lam (dd/MM/yyyy): ");
                DateTime hireDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

                Console.Write("Loai nhan vien (Monthly/Hourly): ");
                string empType = Console.ReadLine();

                Console.Write("La quan ly? (Y/N): ");
                string isManager = Console.ReadLine().ToUpper();

                if (isManager == "Y")
                {
                    Console.Write("Phu cap quan ly: ");
                    decimal allowance = decimal.Parse(Console.ReadLine());

                    Console.Write("So luong nhan vien quan ly: ");
                    int teamSize = int.Parse(Console.ReadLine());

                    Manager mgr = new Manager(id, name, dob, phone, address, deptId, position,
                                             baseSalary, hireDate, empType, allowance, teamSize);
                    manager.AddEmployee(mgr);
                }
                else
                {
                    Employee emp = new Employee(id, name, dob, phone, address, deptId, position,
                                               baseSalary, hireDate, empType);
                    manager.AddEmployee(emp);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi nhap lieu: " + ex.Message);
            }
        }

        static void RemoveEmployeeMenu(AttendanceManager manager)
        {
            Console.Clear();
            Console.WriteLine("========== XOA NHAN VIEN ==========");
            Console.Write("Ma nhan vien: ");
            string empId = Console.ReadLine();
            manager.RemoveEmployee(empId);
        }

        static void UpdateEmployeeMenu(AttendanceManager manager)
        {
            Console.Clear();
            Console.WriteLine("========== CAP NHAT NHAN VIEN ==========");
            Console.Write("Ma nhan vien: ");
            string empId = Console.ReadLine();

            try
            {
                Console.Write("Ten moi: ");
                string newName = Console.ReadLine();

                Console.Write("So dien thoai moi: ");
                string newPhone = Console.ReadLine();

                manager.UpdateEmployee(empId, newName, newPhone);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi nhap lieu: " + ex.Message);
            }
        }

        static void CheckInMenu(AttendanceManager manager)
        {
            Console.Clear();
            Console.WriteLine("========== CHECK IN ==========");
            Console.Write("Ma nhan vien: ");
            string empId = Console.ReadLine();

            Console.WriteLine("\nDanh sach ca lam viec:");
            List<WorkShift> shifts = manager.GetWorkShifts();
            for (int i = 0; i < shifts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {shifts[i].ShiftId} - {shifts[i].ShiftName} ({shifts[i].StartTime} - {shifts[i].EndTime})");
            }
            Console.Write("Chon ca lam (nhap ma): ");
            string shiftId = Console.ReadLine();

            manager.CheckIn(empId, shiftId);
        }

        static void CheckOutMenu(AttendanceManager manager)
        {
            Console.Clear();
            Console.WriteLine("========== CHECK OUT ==========");
            Console.Write("Ma nhan vien: ");
            string empId = Console.ReadLine();
            manager.CheckOut(empId);
        }

        static void ViewTimeRecordsMenu(AttendanceManager manager)
        {
            Console.Clear();
            Console.Write("Nhap ma nhan vien: ");
            string empId = Console.ReadLine();
            manager.DisplayTimeRecords(empId);
        }

        static void CalculateSalaryMenu(AttendanceManager manager)
        {
            Console.Clear();
            Console.WriteLine("========== TINH LUONG THANG ==========");
            Console.Write("Ma nhan vien: ");
            string empId = Console.ReadLine();

            try
            {
                Console.Write("Thang: ");
                int month = int.Parse(Console.ReadLine());

                Console.Write("Nam: ");
                int year = int.Parse(Console.ReadLine());

                manager.CalculateMonthlySalary(empId, month, year);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi nhap lieu: " + ex.Message);
            }
        }

        static void ViewAttendanceReportMenu(AttendanceManager manager)
        {
            Console.Clear();
            Console.Write("Ma nhan vien: ");
            string empId = Console.ReadLine();

            try
            {
                Console.Write("Thang: ");
                int month = int.Parse(Console.ReadLine());

                Console.Write("Nam: ");
                int year = int.Parse(Console.ReadLine());

                manager.DisplayAttendanceReport(empId, month, year);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi nhap lieu: " + ex.Message);
            }
        }

        static void SaveDataMenu(AttendanceManager manager)
        {
            Console.Clear();
            Console.Write("Nhap duong dan file (mac dinh: attendance_data.dat): ");
            string path = Console.ReadLine();
            if (string.IsNullOrEmpty(path))
            {
                path = "attendance_data.dat";
            }
            manager.SaveData(path);
        }

        static void LoadDataMenu(AttendanceManager manager)
        {
            Console.Clear();
            Console.Write("Nhap duong dan file (mac dinh: attendance_data.dat): ");
            string path = Console.ReadLine();
            if (string.IsNullOrEmpty(path))
            {
                path = "attendance_data.dat";
            }
            manager.LoadData(path);
        }
    }
}