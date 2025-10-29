using AttendanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem
{
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

            empToRemove.Dispose();
            employees.RemoveAt(removeIndex);

            for (int i = 0; i < departments.Count; i++)
            {
                departments[i].RemoveEmployee(employeeId);
            }

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

        public void CheckOut(string employeeId)
        {
            Employee emp = FindEmployee(employeeId);
            if (emp == null)
            {
                Console.WriteLine("Khong tim thay nhan vien!");
                return;
            }

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

        // ✅ THÊM 2 METHOD NÀY - QUAN TRỌNG CHO SAMPLEDATAGENERATOR
        public void AddTimeRecordDirect(TimeRecord record)
        {
            timeRecords.Add(record);
        }

        public void AddAttendanceDirect(Attendance att)
        {
            // Kiểm tra trùng theo ngày
            for (int i = 0; i < attendances.Count; i++)
            {
                if (attendances[i].EmployeeId == att.EmployeeId &&
                    attendances[i].Date.Date == att.Date.Date)
                {
                    // Đã tồn tại, cộng dồn giờ
                    attendances[i].IsPresent = att.IsPresent;
                    attendances[i].TotalHours += att.TotalHours;
                    return;
                }
            }
            // Chưa tồn tại, thêm mới
            attendances.Add(att);
        }

        public void CalculateMonthlySalary(string employeeId, int month, int year)
        {
            Employee emp = FindEmployee(employeeId);
            if (emp == null)
            {
                Console.WriteLine("Khong tim thay nhan vien!");
                return;
            }

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

            SalaryCalculatorFactory factory = new SalaryCalculatorFactory();
            ISalaryCalculator calculator = factory.CreateCalculator(emp.GetEmployeeType());

            decimal calculatedSalary = calculator.CalculateSalary(emp.GetBaseSalary(), workDays, totalHours);

            decimal bonus = 0;
            Manager mgr = emp as Manager;
            if (mgr != null)
            {
                bonus = mgr.ManagementAllowance;
            }

            if (workDays >= 26)
            {
                bonus += emp.GetBaseSalary() * 0.1m;
            }

            decimal deduction = 0;
            if (workDays < 20)
            {
                deduction = emp.GetBaseSalary() * 0.05m;
            }

            string salaryId = "SAL" + employeeId + month.ToString("D2") + year.ToString();
            Salary salary = new Salary(salaryId, employeeId, month, year,
                                      calculatedSalary, bonus, deduction, workDays);

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

        public Employee FindEmployee(string employeeId)
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
}