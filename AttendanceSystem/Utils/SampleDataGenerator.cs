using System;
using System.Collections.Generic;

namespace AttendanceSystem
{
    public class SampleDataGenerator
    {
        private AttendanceManager manager;
        private Random random;

        public SampleDataGenerator(AttendanceManager manager)
        {
            this.manager = manager;
            this.random = new Random();
        }

        // ========== TẠO NHÂN VIÊN MẪU ==========
        public void GenerateSampleEmployees()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════╗");
            Console.WriteLine("║          TAO NHAN VIEN MAU                     ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            // Nhân viên 1: Monthly
            Employee emp1 = new Employee(
                "NV001",
                "Nguyen Van An",
                new DateTime(1990, 3, 10),
                "0901234567",
                "123 Duong ABC, Quan 1, TP.HCM",
                "IT",
                "Developer",
                15000000,
                new DateTime(2024, 1, 1),
                "Monthly"
            );
            manager.AddEmployee(emp1);
            Console.WriteLine("  [✓] NV001 - Nguyen Van An (Developer - Monthly)");

            // Nhân viên 2: Hourly
            Employee emp2 = new Employee(
                "NV002",
                "Tran Thi Binh",
                new DateTime(1992, 5, 15),
                "0912345678",
                "456 Duong XYZ, Quan 3, TP.HCM",
                "HR",
                "HR Specialist",
                12000000,
                new DateTime(2024, 1, 1),
                "Hourly"
            );
            manager.AddEmployee(emp2);
            Console.WriteLine("  [✓] NV002 - Tran Thi Binh (HR Specialist - Hourly)");

            Console.WriteLine($"\n→ Tong cong: {manager.GetAllEmployees().Count} nhan vien\n");
        }

        // ========== TẠO CHẤM CÔNG CHO 1 NHÂN VIÊN TRONG 1 THÁNG ==========
        public void GenerateMonthlyAttendance(string employeeId, int month, int year)
        {
            Console.WriteLine($"\n=== TAO CHAM CONG: {employeeId} - THANG {month}/{year} ===");

            Employee emp = manager.FindEmployee(employeeId);
            if (emp == null)
            {
                Console.WriteLine($"[ERROR] Khong tim thay nhan vien {employeeId}!");
                return;
            }

            int daysInMonth = DateTime.DaysInMonth(year, month);
            int workDaysGenerated = 0;
            double totalHoursGenerated = 0;

            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime date = new DateTime(year, month, day);

                // Bỏ qua cuối tuần
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    continue;
                }

                // Random 10% nghỉ
                int skipChance = random.Next(0, 100);
                if (skipChance < 10)
                {
                    Console.WriteLine($"  {date:dd/MM} ({GetDayName(date.DayOfWeek)}): NGHI");
                    continue;
                }

                // Tạo giờ check-in và check-out ngẫu nhiên
                TimeSpan checkInTime = GenerateCheckInTime();
                TimeSpan checkOutTime = GenerateCheckOutTime();

                DateTime checkIn = new DateTime(year, month, day, checkInTime.Hours, checkInTime.Minutes, 0);
                DateTime checkOut = new DateTime(year, month, day, checkOutTime.Hours, checkOutTime.Minutes, 0);

                // Tạo TimeRecord
                string recordId = "REC" + checkIn.ToString("yyyyMMddHHmmss") + employeeId;
                TimeRecord record = new TimeRecord(recordId, employeeId, checkIn, "S1");
                record.CheckOut(checkOut);

                double workHours = record.CalculateWorkHours();

                // ✅ FIX: Gọi method Direct thay vì add vào copy
                manager.AddTimeRecordDirect(record);

                // Tạo Attendance
                string attId = "ATT" + checkIn.ToString("yyyyMMddHHmmss") + employeeId;
                Attendance att = new Attendance(attId, employeeId, date, true, workHours, "");

                // ✅ FIX: Gọi method Direct thay vì add vào copy
                manager.AddAttendanceDirect(att);

                workDaysGenerated++;
                totalHoursGenerated += workHours;

                Console.WriteLine($"  {date:dd/MM} ({GetDayName(date.DayOfWeek)}): {checkIn:HH:mm}-{checkOut:HH:mm} = {workHours:F1}h");
            }

            Console.WriteLine($"\n→ Tong ket: {workDaysGenerated} ngay, {totalHoursGenerated:F1} gio\n");
        }

        // ========== TẠO DỮ LIỆU ĐẦY ĐỦ CHO DEMO ==========
        public void GenerateFullDemoData(int month, int year)
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════╗");
            Console.WriteLine("║     TAO DU LIEU MAU DAY DU CHO DEMO            ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");

            // Bước 1: Tạo nhân viên
            GenerateSampleEmployees();

            // Verify
            List<Employee> allEmps = manager.GetAllEmployees();
            if (allEmps.Count == 0)
            {
                Console.WriteLine("\n[ERROR] Khong co nhan vien nao duoc tao!");
                return;
            }

            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║          TAO CHAM CONG THANG 9/2025            ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");

            // Bước 2: Tạo chấm công cho từng nhân viên
            for (int i = 0; i < allEmps.Count; i++)
            {
                GenerateMonthlyAttendance(allEmps[i].PersonId, month, year);
            }

            // Verify kết quả
            List<Attendance> allAtt = manager.GetAllAttendances();

            Console.WriteLine("\n╔════════════════════════════════════════════════╗");
            Console.WriteLine("║          HOAN THANH TAO DU LIEU!               ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");

            Console.WriteLine($"\n✓ Tong so nhan vien: {allEmps.Count}");
            Console.WriteLine($"✓ Tong so ban ghi cham cong: {allAtt.Count}");

            Console.WriteLine("\n═══════════════════════════════════════════════");
            Console.WriteLine("BAN CO THE:");
            Console.WriteLine($"  [1] Xem danh sach nhan vien");
            Console.WriteLine($"  [4] Xem bao cao cham cong thang {month}/{year}");
            Console.WriteLine($"  [3] Tinh luong thang {month}/{year}");
            Console.WriteLine($"  [5] Luu du lieu de su dung lan sau");
            Console.WriteLine("═══════════════════════════════════════════════\n");
        }

        // ========== HELPER METHODS ==========

        private TimeSpan GenerateCheckInTime()
        {
            // Random từ 7:45 đến 8:30
            int hour = 8;
            int minute = random.Next(-15, 31);

            if (minute < 0)
            {
                hour = 7;
                minute = 60 + minute;
            }

            return new TimeSpan(hour, minute, 0);
        }

        private TimeSpan GenerateCheckOutTime()
        {
            // Random từ 17:00 đến 18:30
            int hour = 17;
            int minute = random.Next(0, 91);

            if (minute >= 60)
            {
                hour = 18;
                minute = minute - 60;
            }

            return new TimeSpan(hour, minute, 0);
        }

        private string GetDayName(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday: return "T2";
                case DayOfWeek.Tuesday: return "T3";
                case DayOfWeek.Wednesday: return "T4";
                case DayOfWeek.Thursday: return "T5";
                case DayOfWeek.Friday: return "T6";
                case DayOfWeek.Saturday: return "T7";
                case DayOfWeek.Sunday: return "CN";
                default: return "";
            }
        }
    }
}