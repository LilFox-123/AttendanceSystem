using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace AttendanceSystem
{
    // ==================== 1. PERSON - BASE CLASS ====================

    public class Person
    {
        private string personId;
        private string fullName;
        private DateTime dateOfBirth;
        private string phoneNumber;
        private string address;

        public Person()
        {
            this.personId = "";
            this.fullName = "";
            this.dateOfBirth = DateTime.Now;
            this.phoneNumber = "";
            this.address = "";
        }

        public Person(string personId, string fullName, DateTime dateOfBirth,
                      string phoneNumber, string address)
        {
            this.personId = personId;
            this.fullName = fullName;
            this.dateOfBirth = dateOfBirth;
            this.phoneNumber = phoneNumber;
            this.address = address;
        }

        public string PersonId
        {
            get { return personId; }
            set { personId = value; }
        }

        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
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

        public void DisplayInfo()
        {
            Console.WriteLine($"Ma: {personId}");
            Console.WriteLine($"Ho ten: {fullName}");
            Console.WriteLine($"Ngay sinh: {dateOfBirth:dd/MM/yyyy}");
            Console.WriteLine($"So dien thoai: {phoneNumber}");
            Console.WriteLine($"Dia chi: {address}");
        }
    }

    // ==================== 2. EMPLOYEE - INHERITANCE + IDISPOSABLE ====================

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

    // ==================== 3. MANAGER - INHERITANCE FROM EMPLOYEE ====================

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

    // ==================== 5. WORK SHIFT ====================

    public class WorkShift
    {
        private string shiftId;
        private string shiftName;
        private TimeSpan startTime;
        private TimeSpan endTime;
        private decimal shiftMultiplier;

        public WorkShift()
        {
            this.shiftId = "";
            this.shiftName = "";
            this.startTime = new TimeSpan(8, 0, 0);
            this.endTime = new TimeSpan(17, 0, 0);
            this.shiftMultiplier = 1.0m;
        }

        public WorkShift(string id, string name, TimeSpan start, TimeSpan end, decimal multiplier)
        {
            this.shiftId = id;
            this.shiftName = name;
            this.startTime = start;
            this.endTime = end;
            this.shiftMultiplier = multiplier;
        }

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

        [XmlIgnore]
        public TimeSpan StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        [XmlElement("StartTime")]
        public long StartTimeTicks
        {
            get { return startTime.Ticks; }
            set { startTime = new TimeSpan(value); }
        }

        [XmlIgnore]
        public TimeSpan EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        [XmlElement("EndTime")]
        public long EndTimeTicks
        {
            get { return endTime.Ticks; }
            set { endTime = new TimeSpan(value); }
        }

        public decimal ShiftMultiplier
        {
            get { return shiftMultiplier; }
            set { shiftMultiplier = value; }
        }

        public double GetWorkHours()
        {
            return (endTime - startTime).TotalHours;
        }
    }

    // ==================== 6. TIME RECORD ====================

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

    // ==================== 7. ATTENDANCE ====================

    public class Attendance
    {
        private string attendanceId;
        private string employeeId;
        private DateTime date;
        private bool isPresent;
        private double totalHours;
        private string note;

        public Attendance()
        {
            this.attendanceId = "";
            this.employeeId = "";
            this.date = DateTime.Now;
            this.isPresent = false;
            this.totalHours = 0;
            this.note = "";
        }

        public Attendance(string attId, string empId, DateTime dt, bool present, double hours, string nt)
        {
            this.attendanceId = attId;
            this.employeeId = empId;
            this.date = dt;
            this.isPresent = present;
            this.totalHours = hours;
            this.note = nt;
        }

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

        public bool IsPresent
        {
            get { return isPresent; }
            set { isPresent = value; }
        }

        public double TotalHours
        {
            get { return totalHours; }
            set { totalHours = value; }
        }

        public string Note
        {
            get { return note; }
            set { note = value; }
        }

        public void DisplayAttendance()
        {
            Console.WriteLine($"Ngay: {date:dd/MM/yyyy}");
            Console.WriteLine($"Trang thai: {(isPresent ? "Co mat" : "Vang mat")}");
            Console.WriteLine($"Tong gio: {totalHours:F2}");
            if (!string.IsNullOrEmpty(note))
            {
                Console.WriteLine($"Ghi chu: {note}");
            }
        }
    }

    // ==================== 8. SALARY ====================

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

    // ==================== 9. STRATEGY PATTERN - SALARY CALCULATION ====================

    public interface ISalaryCalculator
    {
        decimal CalculateSalary(decimal baseSalary, int workDays, double totalHours);
        string GetCalculationType();
    }

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

    // ==================== 11. OBSERVER PATTERN - NOTIFICATION SYSTEM ====================

    public interface IObserver
    {
        void Update(string message);
    }

    public interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify(string message);
    }

    public class AttendanceNotifier : ISubject
    {
        private List<IObserver> observers;

        public AttendanceNotifier()
        {
            observers = new List<IObserver>();
        }

        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify(string message)
        {
            for (int i = 0; i < observers.Count; i++)
            {
                observers[i].Update(message);
            }
        }
    }

    public class EmailNotification : IObserver
    {
        private string recipientEmail;

        public EmailNotification(string email)
        {
            this.recipientEmail = email;
        }

        public void Update(string message)
        {
            Console.WriteLine($"[EMAIL] Gui den {recipientEmail}: {message}");
        }
    }

    public class SMSNotification : IObserver
    {
        private string phoneNumber;

        public SMSNotification(string phone)
        {
            this.phoneNumber = phone;
        }

        public void Update(string message)
        {
            Console.WriteLine($"[SMS] Gui den {phoneNumber}: {message}");
        }
    }

    // ==================== 12. SINGLETON PATTERN - ATTENDANCE MANAGER ====================

    public class AttendanceManager
    {
        private static AttendanceManager instance;
        private static object lockObject = new object();

        private List<Employee> employees;
        private List<TimeRecord> timeRecords;
        private List<Attendance> attendances;
        private List<Department> departments;
        private List<Salary> salaries;
        private List<WorkShift> workShifts;
        private AttendanceNotifier notifier;

        private AttendanceManager()
        {
            employees = new List<Employee>();
            timeRecords = new List<TimeRecord>();
            attendances = new List<Attendance>();
            departments = new List<Department>();
            salaries = new List<Salary>();
            workShifts = new List<WorkShift>();
            notifier = new AttendanceNotifier();

            InitializeDefaultData();
        }

        public static AttendanceManager GetInstance()
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new AttendanceManager();
                    }
                }
            }
            return instance;
        }

        private void InitializeDefaultData()
        {
            // Khởi tạo phòng ban mặc định
            departments.Add(new Department("IT", "Cong nghe thong tin", ""));
            departments.Add(new Department("HR", "Nhan su", ""));
            departments.Add(new Department("ACC", "Ke toan", ""));
            departments.Add(new Department("MKT", "Marketing", ""));

            // Khởi tạo ca làm việc mặc định
            workShifts.Add(new WorkShift("S1", "Ca sang", new TimeSpan(8, 0, 0), new TimeSpan(17, 0, 0), 1.0m));
            workShifts.Add(new WorkShift("S2", "Ca chieu", new TimeSpan(13, 0, 0), new TimeSpan(22, 0, 0), 1.2m));
            workShifts.Add(new WorkShift("S3", "Ca toi", new TimeSpan(22, 0, 0), new TimeSpan(6, 0, 0), 1.5m));
        }

        public void AddEmployee(Employee emp)
        {
            // Kiểm tra trùng mã
            for (int i = 0; i < employees.Count; i++)
            {
                if (employees[i].PersonId == emp.PersonId)
                {
                    Console.WriteLine("Ma nhan vien da ton tai!");
                    return;
                }
            }

            employees.Add(emp);

            // Thêm vào phòng ban
            for (int i = 0; i < departments.Count; i++)
            {
                if (departments[i].DepartmentId == emp.DepartmentId)
                {
                    departments[i].AddEmployee(emp.PersonId);
                    break;
                }
            }

            notifier.Notify($"Nhan vien moi {emp.FullName} da duoc them vao he thong");
            Console.WriteLine("Them nhan vien thanh cong!");
        }

        public void RemoveEmployee(string employeeId)
        {
            Employee empToRemove = null;
            int removeIndex = -1;

            // Tìm nhân viên cần xóa
            for (int i = 0; i < employees.Count; i++)
            {
                if (employees[i].PersonId == employeeId)
                {
                    empToRemove = employees[i];
                    removeIndex = i;
                    break;
                }
            }

            if (empToRemove == null)
            {
                Console.WriteLine("Khong tim thay nhan vien!");
                return;
            }

            // Sử dụng Dispose để dọn dẹp tài nguyên
            empToRemove.Dispose();

            // Xóa khỏi danh sách nhân viên
            employees.RemoveAt(removeIndex);

            // Xóa khỏi phòng ban
            for (int i = 0; i < departments.Count; i++)
            {
                departments[i].RemoveEmployee(employeeId);
            }

            // Xóa khỏi nhóm quản lý (nếu là thành viên)
            for (int i = 0; i < employees.Count; i++)
            {
                Manager mgr = employees[i] as Manager;
                if (mgr != null)
                {
                    mgr.RemoveTeamMember(employeeId);
                }
            }

            notifier.Notify($"Nhan vien {empToRemove.FullName} da bi xoa khoi he thong");
            Console.WriteLine("Xoa nhan vien thanh cong!");
        }

        public void DisplayAllEmployees()
        {
            if (employees.Count == 0)
            {
                Console.WriteLine("Chua co nhan vien nao!");
                return;
            }

            Console.WriteLine("\n========== DANH SACH NHAN VIEN ==========");
            for (int i = 0; i < employees.Count; i++)
            {
                Console.WriteLine($"\n--- Nhan vien {i + 1} ---");

                Manager mgr = employees[i] as Manager;
                if (mgr != null)
                {
                    mgr.DisplayManagerInfo();
                }
                else
                {
                    employees[i].DisplayEmployeeInfo();
                }
            }
        }

        public void CheckIn(string employeeId, string shiftId)
        {
            Employee emp = FindEmployee(employeeId);
            if (emp == null)
            {
                Console.WriteLine("Khong tim thay nhan vien!");
                return;
            }

            if (!emp.IsActive)
            {
                Console.WriteLine("Nhan vien da nghi viec!");
                return;
            }

            // Kiểm tra xem đã check-in trong ngày chưa
            DateTime today = DateTime.Now.Date;
            for (int i = 0; i < timeRecords.Count; i++)
            {
                if (timeRecords[i].EmployeeId == employeeId &&
                    timeRecords[i].CheckInTime.Date == today &&
                    !timeRecords[i].IsCheckedOut)
                {
                    Console.WriteLine("Nhan vien da check-in trong ngay hom nay!");
                    return;
                }
            }

            string recordId = "REC" + DateTime.Now.ToString("yyyyMMddHHmmss");
            TimeRecord record = new TimeRecord(recordId, employeeId, DateTime.Now, shiftId);
            timeRecords.Add(record);

            notifier.Notify($"Nhan vien {emp.FullName} da check-in luc {DateTime.Now:HH:mm:ss}");
            Console.WriteLine($"\nCheck-in thanh cong!");
            Console.WriteLine($"Thoi gian: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            Console.WriteLine($"Ca lam: {shiftId}");
        }

        // ==================== TIẾP TỤC ATTENDANCE MANAGER ====================

        public void CheckOut(string employeeId)
        {
            Employee emp = FindEmployee(employeeId);
            if (emp == null)
            {
                Console.WriteLine("Khong tim thay nhan vien!");
                return;
            }

            // Tìm bản ghi check-in chưa check-out
            TimeRecord latestRecord = null;
            for (int i = timeRecords.Count - 1; i >= 0; i--)
            {
                if (timeRecords[i].EmployeeId == employeeId && !timeRecords[i].IsCheckedOut)
                {
                    latestRecord = timeRecords[i];
                    break;
                }
            }

            if (latestRecord == null)
            {
                Console.WriteLine("Khong tim thay ban ghi check-in!");
                return;
            }

            latestRecord.CheckOut(DateTime.Now);
            double workHours = latestRecord.CalculateWorkHours();

            // Cập nhật attendance
            UpdateAttendance(employeeId, latestRecord.CheckInTime.Date, true, workHours);

            notifier.Notify($"Nhan vien {emp.FullName} da check-out luc {DateTime.Now:HH:mm:ss}");
            Console.WriteLine($"\nCheck-out thanh cong!");
            Console.WriteLine($"Thoi gian: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            Console.WriteLine($"So gio lam viec: {workHours:F2} gio");
        }

        private void UpdateAttendance(string employeeId, DateTime date, bool present, double hours)
        {
            Attendance att = null;
            for (int i = 0; i < attendances.Count; i++)
            {
                if (attendances[i].EmployeeId == employeeId &&
                    attendances[i].Date.Date == date.Date)
                {
                    att = attendances[i];
                    break;
                }
            }

            if (att == null)
            {
                string attId = "ATT" + DateTime.Now.ToString("yyyyMMddHHmmss");
                att = new Attendance(attId, employeeId, date, present, hours, "");
                attendances.Add(att);
            }
            else
            {
                att.IsPresent = present;
                att.TotalHours += hours;
            }
        }

        public void CalculateMonthlySalary(string employeeId, int month, int year)
        {
            Employee emp = FindEmployee(employeeId);
            if (emp == null)
            {
                Console.WriteLine("Khong tim thay nhan vien!");
                return;
            }

            // Đếm số ngày làm việc và tổng giờ
            int workDays = 0;
            double totalHours = 0;

            for (int i = 0; i < attendances.Count; i++)
            {
                Attendance att = attendances[i];
                if (att.EmployeeId == employeeId &&
                    att.Date.Month == month &&
                    att.Date.Year == year &&
                    att.IsPresent)
                {
                    workDays++;
                    totalHours += att.TotalHours;
                }
            }

            // Sử dụng Factory Pattern để tạo calculator
            SalaryCalculatorFactory factory = new SalaryCalculatorFactory();
            ISalaryCalculator calculator = factory.CreateCalculator(emp.GetEmployeeType());

            decimal calculatedSalary = calculator.CalculateSalary(emp.GetBaseSalary(), workDays, totalHours);

            // Tính bonus và deduction
            decimal bonus = 0;
            Manager mgr = emp as Manager;
            if (mgr != null)
            {
                bonus = mgr.ManagementAllowance;
            }

            if (workDays >= 26)
            {
                bonus += emp.GetBaseSalary() * 0.1m; // Thưởng 10% nếu đi làm đủ
            }

            decimal deduction = 0;
            if (workDays < 20)
            {
                deduction = emp.GetBaseSalary() * 0.05m; // Trừ 5% nếu thiếu nhiều
            }

            // Tạo bảng lương
            string salaryId = "SAL" + employeeId + month.ToString("D2") + year.ToString();
            Salary salary = new Salary(salaryId, employeeId, month, year,
                                      calculatedSalary, bonus, deduction, workDays);

            // Kiểm tra xem đã có bảng lương này chưa
            bool exists = false;
            for (int i = 0; i < salaries.Count; i++)
            {
                if (salaries[i].EmployeeId == employeeId &&
                    salaries[i].Month == month &&
                    salaries[i].Year == year)
                {
                    salaries[i] = salary;
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                salaries.Add(salary);
            }

            Console.WriteLine("\n========== BANG LUONG ==========");
            salary.DisplaySalary();
            Console.WriteLine($"\nPhuong phap tinh: {calculator.GetCalculationType()}");
        }

        public void DisplayAttendanceReport(string employeeId, int month, int year)
        {
            Employee emp = FindEmployee(employeeId);
            if (emp == null)
            {
                Console.WriteLine("Khong tim thay nhan vien!");
                return;
            }

            Console.WriteLine("\n========== BAO CAO CHAM CONG ==========");
            Console.WriteLine($"Nhan vien: {emp.FullName} ({emp.PersonId})");
            Console.WriteLine($"Thang: {month}/{year}");
            Console.WriteLine("=======================================");

            int workDays = 0;
            double totalHours = 0;

            for (int i = 0; i < attendances.Count; i++)
            {
                Attendance att = attendances[i];
                if (att.EmployeeId == employeeId &&
                    att.Date.Month == month &&
                    att.Date.Year == year)
                {
                    Console.WriteLine($"\nNgay: {att.Date:dd/MM/yyyy}");
                    att.DisplayAttendance();

                    if (att.IsPresent)
                    {
                        workDays++;
                        totalHours += att.TotalHours;
                    }
                }
            }

            Console.WriteLine("\n=======================================");
            Console.WriteLine($"Tong so ngay lam viec: {workDays}");
            Console.WriteLine($"Tong so gio lam viec: {totalHours:F2}");
        }

        public void AddObserver(IObserver observer)
        {
            notifier.Attach(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            notifier.Detach(observer);
        }

        private Employee FindEmployee(string employeeId)
        {
            for (int i = 0; i < employees.Count; i++)
            {
                if (employees[i].PersonId == employeeId)
                {
                    return employees[i];
                }
            }
            return null;
        }

        public List<Employee> GetAllEmployees()
        {
            return employees;
        }

        public List<Department> GetAllDepartments()
        {
            return departments;
        }

        public List<WorkShift> GetAllWorkShifts()
        {
            return workShifts;
        }

        public List<TimeRecord> GetAllTimeRecords()
        {
            return timeRecords;
        }

        public List<Attendance> GetAllAttendances()
        {
            return attendances;
        }

        public List<Salary> GetAllSalaries()
        {
            return salaries;
        }

        public void SetEmployees(List<Employee> emps)
        {
            employees = emps;
        }

        public void SetDepartments(List<Department> depts)
        {
            departments = depts;
        }

        public void SetWorkShifts(List<WorkShift> shifts)
        {
            workShifts = shifts;
        }

        public void SetTimeRecords(List<TimeRecord> records)
        {
            timeRecords = records;
        }

        public void SetAttendances(List<Attendance> atts)
        {
            attendances = atts;
        }

        public void SetSalaries(List<Salary> sals)
        {
            salaries = sals;
        }
    }

    // ==================== 13. DATA MANAGER - XML SERIALIZATION ====================

    public class DataManager
    {
        private string dataDirectory;

        public DataManager()
        {
            dataDirectory = "Data";
            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }
        }

        public void SaveEmployees(List<Employee> employees)
        {
            string filePath = Path.Combine(dataDirectory, "Employees.xml");
            SaveToXml(employees, filePath);
        }

        public List<Employee> LoadEmployees()
        {
            string filePath = Path.Combine(dataDirectory, "Employees.xml");
            return LoadFromXml<List<Employee>>(filePath);
        }

        public void SaveDepartments(List<Department> departments)
        {
            string filePath = Path.Combine(dataDirectory, "Departments.xml");
            SaveToXml(departments, filePath);
        }

        public List<Department> LoadDepartments()
        {
            string filePath = Path.Combine(dataDirectory, "Departments.xml");
            return LoadFromXml<List<Department>>(filePath);
        }

        public void SaveWorkShifts(List<WorkShift> shifts)
        {
            string filePath = Path.Combine(dataDirectory, "WorkShifts.xml");
            SaveToXml(shifts, filePath);
        }

        public List<WorkShift> LoadWorkShifts()
        {
            string filePath = Path.Combine(dataDirectory, "WorkShifts.xml");
            return LoadFromXml<List<WorkShift>>(filePath);
        }

        public void SaveTimeRecords(List<TimeRecord> records)
        {
            string filePath = Path.Combine(dataDirectory, "TimeRecords.xml");
            SaveToXml(records, filePath);
        }

        public List<TimeRecord> LoadTimeRecords()
        {
            string filePath = Path.Combine(dataDirectory, "TimeRecords.xml");
            return LoadFromXml<List<TimeRecord>>(filePath);
        }

        public void SaveAttendances(List<Attendance> attendances)
        {
            string filePath = Path.Combine(dataDirectory, "Attendances.xml");
            SaveToXml(attendances, filePath);
        }

        public List<Attendance> LoadAttendances()
        {
            string filePath = Path.Combine(dataDirectory, "Attendances.xml");
            return LoadFromXml<List<Attendance>>(filePath);
        }

        public void SaveSalaries(List<Salary> salaries)
        {
            string filePath = Path.Combine(dataDirectory, "Salaries.xml");
            SaveToXml(salaries, filePath);
        }

        public List<Salary> LoadSalaries()
        {
            string filePath = Path.Combine(dataDirectory, "Salaries.xml");
            return LoadFromXml<List<Salary>>(filePath);
        }

        private void SaveToXml<T>(T data, string filePath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    serializer.Serialize(writer, data);
                }
                Console.WriteLine($"Luu du lieu thanh cong: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loi khi luu du lieu: {ex.Message}");
            }
        }

        private T LoadFromXml<T>(string filePath) where T : new()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File khong ton tai: {filePath}");
                    return new T();
                }

                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
                {
                    T result = (T)serializer.Deserialize(reader);
                    Console.WriteLine($"Tai du lieu thanh cong: {filePath}");
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loi khi tai du lieu: {ex.Message}");
                return new T();
            }
        }

        public void SaveAllData()
        {
            AttendanceManager manager = AttendanceManager.GetInstance();

            SaveEmployees(manager.GetAllEmployees());
            SaveDepartments(manager.GetAllDepartments());
            SaveWorkShifts(manager.GetAllWorkShifts());
            SaveTimeRecords(manager.GetAllTimeRecords());
            SaveAttendances(manager.GetAllAttendances());
            SaveSalaries(manager.GetAllSalaries());

            Console.WriteLine("\n=== LUU TAT CA DU LIEU THANH CONG ===");
        }

        public void LoadAllData()
        {
            AttendanceManager manager = AttendanceManager.GetInstance();

            List<Employee> employees = LoadEmployees();
            List<Department> departments = LoadDepartments();
            List<WorkShift> shifts = LoadWorkShifts();
            List<TimeRecord> records = LoadTimeRecords();
            List<Attendance> attendances = LoadAttendances();
            List<Salary> salaries = LoadSalaries();

            if (employees != null) manager.SetEmployees(employees);
            if (departments != null) manager.SetDepartments(departments);
            if (shifts != null) manager.SetWorkShifts(shifts);
            if (records != null) manager.SetTimeRecords(records);
            if (attendances != null) manager.SetAttendances(attendances);
            if (salaries != null) manager.SetSalaries(salaries);

            Console.WriteLine("\n=== TAI TAT CA DU LIEU THANH CONG ===");
        }
    }

    // ==================== 14. MENU SYSTEM ====================

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            AttendanceManager manager = AttendanceManager.GetInstance();
            DataManager dataManager = new DataManager();

            // Thêm observers
            EmailNotification emailNotif = new EmailNotification("admin@company.com");
            SMSNotification smsNotif = new SMSNotification("0123456789");
            manager.AddObserver(emailNotif);
            manager.AddObserver(smsNotif);

            // Tải dữ liệu từ file
            Console.WriteLine("=== TAI DU LIEU TU FILE ===");
            dataManager.LoadAllData();

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n╔════════════════════════════════════════════╗");
                Console.WriteLine("║   HE THONG QUAN LY CHAM CONG & LUONG      ║");
                Console.WriteLine("╠════════════════════════════════════════════╣");
                Console.WriteLine("║ 1. Quan ly nhan vien                      ║");
                Console.WriteLine("║ 2. Cham cong                              ║");
                Console.WriteLine("║ 3. Tinh luong                             ║");
                Console.WriteLine("║ 4. Bao cao                                ║");
                Console.WriteLine("║ 5. Luu du lieu                            ║");
                Console.WriteLine("║ 6. Tai du lieu                            ║");
                Console.WriteLine("║ 0. Thoat                                  ║");
                Console.WriteLine("╚════════════════════════════════════════════╝");
                Console.Write("Chon chuc nang: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        EmployeeMenu(manager);
                        break;
                    case "2":
                        AttendanceMenu(manager);
                        break;
                    case "3":
                        SalaryMenu(manager);
                        break;
                    case "4":
                        ReportMenu(manager);
                        break;
                    case "5":
                        dataManager.SaveAllData();
                        break;
                    case "6":
                        dataManager.LoadAllData();
                        break;
                    case "0":
                        Console.Write("\nBan co muon luu du lieu truoc khi thoat? (Y/N): ");
                        string saveChoice = Console.ReadLine();
                        if (saveChoice.ToUpper() == "Y")
                        {
                            dataManager.SaveAllData();
                        }
                        running = false;
                        Console.WriteLine("Tam biet!");
                        break;
                    default:
                        Console.WriteLine("Lua chon khong hop le!");
                        break;
                }
            }
        }

        static void EmployeeMenu(AttendanceManager manager)
        {
            Console.WriteLine("\n=== QUAN LY NHAN VIEN ===");
            Console.WriteLine("1. Them nhan vien");
            Console.WriteLine("2. Xoa nhan vien");
            Console.WriteLine("3. Hien thi danh sach");
            Console.WriteLine("0. Quay lai");
            Console.Write("Chon: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddEmployeeInput(manager);
                    break;
                case "2":
                    RemoveEmployeeInput(manager);
                    break;
                case "3":
                    manager.DisplayAllEmployees();
                    break;
            }
        }

        static void AddEmployeeInput(AttendanceManager manager)
        {
            Console.WriteLine("\n=== THEM NHAN VIEN MOI ===");

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

            Console.Write("Ma phong ban (IT/HR/ACC/MKT): ");
            string deptId = Console.ReadLine();

            Console.Write("Chuc vu: ");
            string position = Console.ReadLine();

            Console.Write("Luong co ban: ");
            decimal salary = decimal.Parse(Console.ReadLine());

            Console.Write("Ngay vao lam (dd/MM/yyyy): ");
            DateTime hireDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

            Console.Write("Loai nhan vien (Monthly/Hourly): ");
            string empType = Console.ReadLine();

            Console.Write("La quan ly? (Y/N): ");
            string isManager = Console.ReadLine();

            if (isManager.ToUpper() == "Y")
            {
                Console.Write("Phu cap quan ly: ");
                decimal allowance = decimal.Parse(Console.ReadLine());

                Console.Write("So luong nhan vien quan ly: ");
                int teamSize = int.Parse(Console.ReadLine());

                Manager mgr = new Manager(id, name, dob, phone, address, deptId,
                                         position, salary, hireDate, empType, allowance, teamSize);
                manager.AddEmployee(mgr);
            }
            else
            {
                Employee emp = new Employee(id, name, dob, phone, address, deptId,
                                           position, salary, hireDate, empType);
                manager.AddEmployee(emp);
            }
        }

        static void RemoveEmployeeInput(AttendanceManager manager)
        {
            Console.Write("\nNhap ma nhan vien can xoa: ");
            string empId = Console.ReadLine();
            manager.RemoveEmployee(empId);
        }

        static void AttendanceMenu(AttendanceManager manager)
        {
            Console.WriteLine("\n=== CHAM CONG ===");
            Console.WriteLine("1. Check-in");
            Console.WriteLine("2. Check-out");
            Console.WriteLine("0. Quay lai");
            Console.Write("Chon: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CheckInInput(manager);
                    break;
                case "2":
                    CheckOutInput(manager);
                    break;
            }
        }

        static void CheckInInput(AttendanceManager manager)
        {
            Console.Write("\nMa nhan vien: ");
            string empId = Console.ReadLine();

            Console.WriteLine("\nCac ca lam viec:");
            List<WorkShift> shifts = manager.GetAllWorkShifts();
            for (int i = 0; i < shifts.Count; i++)
            {
                Console.WriteLine($"{shifts[i].ShiftId} - {shifts[i].ShiftName}");
            }

            Console.Write("Chon ca lam: ");
            string shiftId = Console.ReadLine();

            manager.CheckIn(empId, shiftId);
        }

        static void CheckOutInput(AttendanceManager manager)
        {
            Console.Write("\nMa nhan vien: ");
            string empId = Console.ReadLine();
            manager.CheckOut(empId);
        }

        static void SalaryMenu(AttendanceManager manager)
        {
            Console.Write("\nMa nhan vien: ");
            string empId = Console.ReadLine();

            Console.Write("Thang: ");
            int month = int.Parse(Console.ReadLine());

            Console.Write("Nam: ");
            int year = int.Parse(Console.ReadLine());

            manager.CalculateMonthlySalary(empId, month, year);
        }

        static void ReportMenu(AttendanceManager manager)
        {
            Console.Write("\nMa nhan vien: ");
            string empId = Console.ReadLine();

            Console.Write("Thang: ");
            int month = int.Parse(Console.ReadLine());

            Console.Write("Nam: ");
            int year = int.Parse(Console.ReadLine());

            manager.DisplayAttendanceReport(empId, month, year);
        }
    }
}