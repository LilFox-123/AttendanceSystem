using AttendanceSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AttendanceSystem
{
    // ==================== 13. DATA MANAGER - XML SERIALIZATION ====================

    public class DataManager
    {
        private AttendanceManager manager;
        private string dataDirectory = "Data";

        // ✅ CONSTRUCTOR - FIX LỖI CS1729
        public DataManager(AttendanceManager manager)
        {
            this.manager = manager;

            // Tạo thư mục Data nếu chưa tồn tại
            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }
        }

        // ========== SAVE ALL DATA ==========
        public void SaveAllData()
        {
            SaveEmployees();
            SaveDepartments();
            SaveWorkShifts();
            SaveTimeRecords();
            SaveAttendances();
            SaveSalaries();

            Console.WriteLine("\n=== LUU TAT CA DU LIEU THANH CONG ===");
        }

        // ========== SAVE INDIVIDUAL ==========
        private void SaveEmployees()
        {
            string filePath = Path.Combine(dataDirectory, "Employees.xml");
            List<Employee> employees = manager.GetAllEmployees();

            XmlSerializer serializer = new XmlSerializer(typeof(List<Employee>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, employees);
            }

            Console.WriteLine($"Luu du lieu thanh cong: {filePath}");
        }

        private void SaveDepartments()
        {
            string filePath = Path.Combine(dataDirectory, "Departments.xml");
            List<Department> departments = manager.GetAllDepartments();

            XmlSerializer serializer = new XmlSerializer(typeof(List<Department>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, departments);
            }

            Console.WriteLine($"Luu du lieu thanh cong: {filePath}");
        }

        private void SaveWorkShifts()
        {
            string filePath = Path.Combine(dataDirectory, "WorkShifts.xml");
            List<WorkShift> shifts = manager.GetAllWorkShifts();

            XmlSerializer serializer = new XmlSerializer(typeof(List<WorkShift>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, shifts);
            }

            Console.WriteLine($"Luu du lieu thanh cong: {filePath}");
        }

        private void SaveTimeRecords()
        {
            string filePath = Path.Combine(dataDirectory, "TimeRecords.xml");
            List<TimeRecord> records = manager.GetAllTimeRecords();

            XmlSerializer serializer = new XmlSerializer(typeof(List<TimeRecord>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, records);
            }

            Console.WriteLine($"Luu du lieu thanh cong: {filePath}");
        }

        private void SaveAttendances()
        {
            string filePath = Path.Combine(dataDirectory, "Attendances.xml");
            List<Attendance> attendances = manager.GetAllAttendances();

            XmlSerializer serializer = new XmlSerializer(typeof(List<Attendance>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, attendances);
            }

            Console.WriteLine($"Luu du lieu thanh cong: {filePath}");
        }

        private void SaveSalaries()
        {
            string filePath = Path.Combine(dataDirectory, "Salaries.xml");
            List<Salary> salaries = manager.GetAllSalaries();

            XmlSerializer serializer = new XmlSerializer(typeof(List<Salary>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, salaries);
            }

            Console.WriteLine($"Luu du lieu thanh cong: {filePath}");
        }

        // ========== LOAD ALL DATA ==========
        public void LoadAllData()
        {
            LoadEmployees();
            LoadDepartments();
            LoadWorkShifts();
            LoadTimeRecords();
            LoadAttendances();
            LoadSalaries();

            Console.WriteLine("\n=== TAI TAT CA DU LIEU THANH CONG ===");
        }

        // ========== LOAD INDIVIDUAL ==========
        private void LoadEmployees()
        {
            string filePath = Path.Combine(dataDirectory, "Employees.xml");

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Khong tim thay file Employees.xml");
                return;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<Employee>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                List<Employee> employees = (List<Employee>)serializer.Deserialize(reader);
                manager.SetEmployees(employees);
            }

            Console.WriteLine($"Tai du lieu thanh cong: {filePath}");
        }

        private void LoadDepartments()
        {
            string filePath = Path.Combine(dataDirectory, "Departments.xml");

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Khong tim thay file Departments.xml");
                return;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<Department>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                List<Department> departments = (List<Department>)serializer.Deserialize(reader);
                manager.SetDepartments(departments);
            }

            Console.WriteLine($"Tai du lieu thanh cong: {filePath}");
        }

        private void LoadWorkShifts()
        {
            string filePath = Path.Combine(dataDirectory, "WorkShifts.xml");

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Khong tim thay file WorkShifts.xml");
                return;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<WorkShift>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                List<WorkShift> shifts = (List<WorkShift>)serializer.Deserialize(reader);
                manager.SetWorkShifts(shifts);
            }

            Console.WriteLine($"Tai du lieu thanh cong: {filePath}");
        }

        private void LoadTimeRecords()
        {
            string filePath = Path.Combine(dataDirectory, "TimeRecords.xml");

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Khong tim thay file TimeRecords.xml");
                return;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<TimeRecord>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                List<TimeRecord> records = (List<TimeRecord>)serializer.Deserialize(reader);
                manager.SetTimeRecords(records);
            }

            Console.WriteLine($"Tai du lieu thanh cong: {filePath}");
        }

        private void LoadAttendances()
        {
            string filePath = Path.Combine(dataDirectory, "Attendances.xml");

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Khong tim thay file Attendances.xml");
                return;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<Attendance>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                List<Attendance> attendances = (List<Attendance>)serializer.Deserialize(reader);
                manager.SetAttendances(attendances);
            }

            Console.WriteLine($"Tai du lieu thanh cong: {filePath}");
        }

        private void LoadSalaries()
        {
            string filePath = Path.Combine(dataDirectory, "Salaries.xml");

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Khong tim thay file Salaries.xml");
                return;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<Salary>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                List<Salary> salaries = (List<Salary>)serializer.Deserialize(reader);
                manager.SetSalaries(salaries);
            }

            Console.WriteLine($"Tai du lieu thanh cong: {filePath}");
        }
    }
}
