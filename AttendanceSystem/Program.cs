
using AttendanceSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace AttendanceSystem
{


    // ==================== 14. MENU SYSTEM ====================
    class Program
    {
        static AttendanceManager manager;
        static DataManager dataManager;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            // Khởi tạo hệ thống
            manager = AttendanceManager.GetInstance();
            dataManager = new DataManager(manager);

            // Thêm observers
            EmailNotification emailNotif = new EmailNotification("admin@company.com");
            SMSNotification smsNotif = new SMSNotification("0123456789");
            manager.AddObserver(emailNotif);
            manager.AddObserver(smsNotif);

            // Hiển thị màn hình chào
            UIHelper.DrawHeader("HE THONG CHAM CONG VA TINH LUONG");
            Console.WriteLine("Chao mung den voi he thong quan ly nhan vien!");
            UIHelper.PressKeyToContinue();

            // Vào menu chính
            MainMenu();
        }

        static void MainMenu()
        {
            bool exit = false;

            while (!exit)
            {
                UIHelper.DrawHeader("MENU CHINH");

                string[] menuOptions = {
                    "Quan ly Nhan vien",
                    "Cham cong",
                    "Tinh luong",
                    "Bao cao",
                    "Luu du lieu",
                    "Tai lai du lieu",
                    "Tao du lieu mau (Demo)"  // ✅ THÊM MENU NÀY
                };

                UIHelper.DrawMenu("CHUC NANG", menuOptions);

                string choice = UIHelper.GetInput("Chon chuc nang");

                switch (choice)
                {
                    case "1":
                        EmployeeMenu();
                        break;
                    case "2":
                        AttendanceMenu();
                        break;
                    case "3":
                        SalaryMenu();
                        break;
                    case "4":
                        ReportMenu();
                        break;
                    case "5":
                        SaveDataMenu();
                        break;
                    case "6":
                        LoadDataMenu();
                        break;
                    case "7":  // ✅ XỬ LÝ MENU MỚI
                        GenerateSampleDataMenu();
                        break;
                    case "0":
                        if (UIHelper.ConfirmAction("Ban co muon luu du lieu truoc khi thoat?"))
                        {
                            dataManager.SaveAllData();
                        }
                        UIHelper.ShowSuccess("Tam biet!");
                        exit = true;
                        break;
                    default:
                        UIHelper.ShowError("Lua chon khong hop le!");
                        UIHelper.PressKeyToContinue();
                        break;
                }
            }
        }

        // ========== MENU QUẢN LÝ NHÂN VIÊN ==========
        static void EmployeeMenu()
        {
            bool back = false;

            while (!back)
            {
                UIHelper.DrawHeader("QUAN LY NHAN VIEN");

                string[] menuOptions = {
                    "Them nhan vien moi",
                    "Xoa nhan vien",
                    "Hien thi danh sach nhan vien"
                };

                UIHelper.DrawMenu("QUAN LY NHAN VIEN", menuOptions);

                string choice = UIHelper.GetInput("Chon chuc nang");

                switch (choice)
                {
                    case "1":
                        AddEmployee();
                        break;
                    case "2":
                        RemoveEmployee();
                        break;
                    case "3":
                        DisplayAllEmployees();
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        UIHelper.ShowError("Lua chon khong hop le!");
                        UIHelper.PressKeyToContinue();
                        break;
                }
            }
        }

        static void AddEmployee()
        {
            UIHelper.DrawHeader("THEM NHAN VIEN MOI");

            try
            {
                string id = UIHelper.GetInput("Ma nhan vien");
                string name = UIHelper.GetInput("Ho va ten");

                Console.Write("▸ Ngay sinh (dd/MM/yyyy): ");
                Console.ForegroundColor = ConsoleColor.White;
                DateTime dob = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
                Console.ResetColor();

                string phone = UIHelper.GetInput("So dien thoai");
                string address = UIHelper.GetInput("Dia chi");

                UIHelper.ShowInfo("Ma phong ban: IT, HR, ACC, MKT");
                string deptId = UIHelper.GetInput("Ma phong ban").ToUpper();

                string position = UIHelper.GetInput("Chuc vu");

                Console.Write("▸ Luong co ban: ");
                Console.ForegroundColor = ConsoleColor.White;
                decimal salary = decimal.Parse(Console.ReadLine());
                Console.ResetColor();

                Console.Write("▸ Ngay vao lam (dd/MM/yyyy): ");
                Console.ForegroundColor = ConsoleColor.White;
                DateTime hireDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
                Console.ResetColor();

                UIHelper.ShowInfo("Loai nhan vien: Monthly / Hourly");
                string empType = UIHelper.GetInput("Loai nhan vien");

                string isManager = UIHelper.GetInput("La quan ly? (Y/N)").ToUpper();

                if (isManager == "Y")
                {
                    Console.Write("▸ Phu cap quan ly: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    decimal allowance = decimal.Parse(Console.ReadLine());
                    Console.ResetColor();

                    Console.Write("▸ So luong nhan vien quan ly: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    int teamSize = int.Parse(Console.ReadLine());
                    Console.ResetColor();

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

                UIHelper.ShowSuccess("Them nhan vien thanh cong!");
            }
            catch (Exception ex)
            {
                UIHelper.ShowError($"Loi: {ex.Message}");
            }

            UIHelper.PressKeyToContinue();
        }

        static void RemoveEmployee()
        {
            UIHelper.DrawHeader("XOA NHAN VIEN");

            string empId = UIHelper.GetInput("Nhap ma nhan vien can xoa");

            if (UIHelper.ConfirmAction($"Ban co chac chan muon xoa nhan vien {empId}?"))
            {
                manager.RemoveEmployee(empId);
                UIHelper.ShowSuccess("Da xoa nhan vien!");
            }
            else
            {
                UIHelper.ShowInfo("Da huy thao tac xoa.");
            }

            UIHelper.PressKeyToContinue();
        }

        static void DisplayAllEmployees()
        {
            UIHelper.DrawHeader("DANH SACH NHAN VIEN");

            List<Employee> employees = manager.GetAllEmployees();

            if (employees.Count == 0)
            {
                UIHelper.ShowWarning("Chua co nhan vien nao trong he thong!");
                UIHelper.ShowInfo("Vui long them nhan vien hoac chon [7] Tao du lieu mau");
            }
            else
            {
                string[] headers = { "Ma NV", "Ho ten", "Phong", "Chuc vu", "Luong CB", "Loai", "Trang thai" };
                int[] widths = { 8, 20, 8, 15, 15, 8, 12 };

                UIHelper.DrawTableHeader(headers, widths);

                for (int i = 0; i < employees.Count; i++)
                {
                    Employee emp = employees[i];
                    string[] row = {
                        emp.PersonId,
                        emp.FullName,
                        emp.DepartmentId,
                        emp.Position,
                        $"{emp.BaseSalary:N0}",
                        emp.GetEmployeeType(),
                        emp.IsActive ? "Lam viec" : "Nghi viec"
                    };
                    UIHelper.DrawTableRow(row, widths);
                }

                UIHelper.DrawTableFooter(widths);

                UIHelper.ShowInfo($"Tong so: {employees.Count} nhan vien");
            }

            UIHelper.PressKeyToContinue();
        }

        // ========== MENU CHẤM CÔNG ==========
        static void AttendanceMenu()
        {
            bool back = false;

            while (!back)
            {
                UIHelper.DrawHeader("CHAM CONG");

                string[] menuOptions = {
                    "Check-in (Vao lam)",
                    "Check-out (Tan lam)"
                };

                UIHelper.DrawMenu("CHAM CONG", menuOptions);

                string choice = UIHelper.GetInput("Chon chuc nang");

                switch (choice)
                {
                    case "1":
                        CheckIn();
                        break;
                    case "2":
                        CheckOut();
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        UIHelper.ShowError("Lua chon khong hop le!");
                        UIHelper.PressKeyToContinue();
                        break;
                }
            }
        }

        static void CheckIn()
        {
            UIHelper.DrawHeader("CHECK-IN (VAO LAM)");

            string empId = UIHelper.GetInput("Ma nhan vien");

            // Hiển thị ca làm việc
            List<WorkShift> shifts = manager.GetAllWorkShifts();

            UIHelper.ShowInfo($"Co {shifts.Count} ca lam viec:");
            UIHelper.DrawSeparator();

            for (int i = 0; i < shifts.Count; i++)
            {
                WorkShift shift = shifts[i];
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"  [{shift.ShiftId}] ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{shift.ShiftName,-15} ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{shift.StartTime:hh\\:mm} - {shift.EndTime:hh\\:mm}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($" (He so: {shift.ShiftMultiplier})");
                Console.ResetColor();
            }

            UIHelper.DrawSeparator();

            string shiftId = UIHelper.GetInput("Chon ma ca lam (S1/S2/S3)");

            try
            {
                manager.CheckIn(empId, shiftId);
                UIHelper.ShowSuccess("Check-in thanh cong!");
            }
            catch (Exception ex)
            {
                UIHelper.ShowError($"Loi: {ex.Message}");
            }

            UIHelper.PressKeyToContinue();
        }

        static void CheckOut()
        {
            UIHelper.DrawHeader("CHECK-OUT (TAN LAM)");

            string empId = UIHelper.GetInput("Ma nhan vien");

            try
            {
                manager.CheckOut(empId);
                UIHelper.ShowSuccess("Check-out thanh cong!");
            }
            catch (Exception ex)
            {
                UIHelper.ShowError($"Loi: {ex.Message}");
            }

            UIHelper.PressKeyToContinue();
        }

        // ========== MENU TÍNH LƯƠNG ==========
        static void SalaryMenu()
        {
            UIHelper.DrawHeader("TINH LUONG");

            string empId = UIHelper.GetInput("Ma nhan vien");

            Console.Write("▸ Thang (1-12): ");
            Console.ForegroundColor = ConsoleColor.White;
            int month = int.Parse(Console.ReadLine());
            Console.ResetColor();

            Console.Write("▸ Nam: ");
            Console.ForegroundColor = ConsoleColor.White;
            int year = int.Parse(Console.ReadLine());
            Console.ResetColor();

            try
            {
                UIHelper.DrawSeparator();
                manager.CalculateMonthlySalary(empId, month, year);
                UIHelper.DrawSeparator();
            }
            catch (Exception ex)
            {
                UIHelper.ShowError($"Loi: {ex.Message}");
            }

            UIHelper.PressKeyToContinue();
        }

        // ========== MENU BÁO CÁO ==========
        static void ReportMenu()
        {
            bool back = false;

            while (!back)
            {
                UIHelper.DrawHeader("BAO CAO");

                string[] menuOptions = {
                    "Bao cao cham cong ca nhan",
                    "Bao cao phong ban"
                };

                UIHelper.DrawMenu("BAO CAO", menuOptions);

                string choice = UIHelper.GetInput("Chon chuc nang");

                switch (choice)
                {
                    case "1":
                        AttendanceReport();
                        break;
                    case "2":
                        DepartmentReport();
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        UIHelper.ShowError("Lua chon khong hop le!");
                        UIHelper.PressKeyToContinue();
                        break;
                }
            }
        }

        static void AttendanceReport()
        {
            UIHelper.DrawHeader("BAO CAO CHAM CONG CA NHAN");

            string empId = UIHelper.GetInput("Ma nhan vien");

            Console.Write("▸ Thang (1-12): ");
            Console.ForegroundColor = ConsoleColor.White;
            int month = int.Parse(Console.ReadLine());
            Console.ResetColor();

            Console.Write("▸ Nam: ");
            Console.ForegroundColor = ConsoleColor.White;
            int year = int.Parse(Console.ReadLine());
            Console.ResetColor();

            try
            {
                manager.DisplayAttendanceReport(empId, month, year);
            }
            catch (Exception ex)
            {
                UIHelper.ShowError($"Loi: {ex.Message}");
            }

            UIHelper.PressKeyToContinue();
        }

        static void DepartmentReport()
        {
            UIHelper.DrawHeader("BAO CAO PHONG BAN");

            string deptId = UIHelper.GetInput("Ma phong ban (IT/HR/ACC/MKT)").ToUpper();

            List<Employee> deptEmployees = new List<Employee>();
            List<Employee> allEmps = manager.GetAllEmployees();

            for (int i = 0; i < allEmps.Count; i++)
            {
                if (allEmps[i].DepartmentId == deptId && allEmps[i].IsActive)
                {
                    deptEmployees.Add(allEmps[i]);
                }
            }

            if (deptEmployees.Count == 0)
            {
                UIHelper.ShowWarning($"Khong co nhan vien nao trong phong ban {deptId}!");
            }
            else
            {
                string[] headers = { "Ma NV", "Ho ten", "Chuc vu", "Luong CB" };
                int[] widths = { 10, 25, 20, 15 };

                UIHelper.DrawTableHeader(headers, widths);

                decimal totalSalary = 0;
                for (int i = 0; i < deptEmployees.Count; i++)
                {
                    Employee emp = deptEmployees[i];
                    string[] row = {
                        emp.PersonId,
                        emp.FullName,
                        emp.Position,
                        $"{emp.BaseSalary:N0}"
                    };
                    UIHelper.DrawTableRow(row, widths);
                    totalSalary += emp.BaseSalary;
                }

                UIHelper.DrawTableFooter(widths);

                UIHelper.ShowInfo($"Tong so nhan vien: {deptEmployees.Count}");
                UIHelper.ShowInfo($"Tong luong co ban: {totalSalary:N0} VND");
            }

            UIHelper.PressKeyToContinue();
        }

        // ========== LƯU/TẢI DỮ LIỆU ==========
        static void SaveDataMenu()
        {
            UIHelper.DrawHeader("LUU DU LIEU");

            try
            {
                dataManager.SaveAllData();
                UIHelper.ShowSuccess("Luu du lieu thanh cong!");
            }
            catch (Exception ex)
            {
                UIHelper.ShowError($"Loi: {ex.Message}");
            }

            UIHelper.PressKeyToContinue();
        }

        static void LoadDataMenu()
        {
            UIHelper.DrawHeader("TAI LAI DU LIEU");

            if (UIHelper.ConfirmAction("Du lieu hien tai se bi ghi de. Tiep tuc?"))
            {
                try
                {
                    dataManager.LoadAllData();
                    UIHelper.ShowSuccess("Tai du lieu thanh cong!");
                }
                catch (Exception ex)
                {
                    UIHelper.ShowError($"Loi: {ex.Message}");
                }
            }
            else
            {
                UIHelper.ShowInfo("Da huy thao tac.");
            }

            UIHelper.PressKeyToContinue();
        }

        // ========== ✅ TẠO DỮ LIỆU MẪU - MENU MỚI ==========
        static void GenerateSampleDataMenu()
        {
            UIHelper.DrawHeader("TAO DU LIEU MAU CHO DEMO");

            UIHelper.ShowWarning("Chuc nang nay se tao 2 nhan vien mau voi du lieu cham cong day du.");
            UIHelper.ShowInfo("Du lieu mau giup ban test cac chuc nang: Bao cao, Tinh luong.");

            if (!UIHelper.ConfirmAction("Ban co muon tao du lieu mau?"))
            {
                UIHelper.ShowInfo("Da huy thao tac.");
                UIHelper.PressKeyToContinue();
                return;
            }

            Console.Write("\n▸ Thang (1-12): ");
            Console.ForegroundColor = ConsoleColor.White;
            int month = int.Parse(Console.ReadLine());
            Console.ResetColor();

            Console.Write("▸ Nam: ");
            Console.ForegroundColor = ConsoleColor.White;
            int year = int.Parse(Console.ReadLine());
            Console.ResetColor();

            try
            {
                SampleDataGenerator generator = new SampleDataGenerator(manager);
                generator.GenerateFullDemoData(month, year);

                UIHelper.ShowSuccess("Tao du lieu mau thanh cong!");
                UIHelper.ShowInfo("Ban co the xem danh sach nhan vien [1][3] hoac tinh luong [3]");
            }
            catch (Exception ex)
            {
                UIHelper.ShowError($"Loi: {ex.Message}");
            }

            UIHelper.PressKeyToContinue();
        }
    }
}
