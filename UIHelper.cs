using System;

namespace AttendanceSystem
{
    // ==================== UI HELPER - QUAN LY MAU SAC VA GIAO DIEN ====================
    public class UIHelper
    {
        // Màu sắc chủ đề
        public static ConsoleColor PRIMARY_COLOR = ConsoleColor.Cyan;
        public static ConsoleColor SUCCESS_COLOR = ConsoleColor.Green;
        public static ConsoleColor ERROR_COLOR = ConsoleColor.Red;
        public static ConsoleColor WARNING_COLOR = ConsoleColor.Yellow;
        public static ConsoleColor INFO_COLOR = ConsoleColor.White;
        public static ConsoleColor HEADER_COLOR = ConsoleColor.Magenta;

        // Vẽ tiêu đề lớn
        public static void DrawHeader(string title)
        {
            Console.Clear();
            Console.ForegroundColor = HEADER_COLOR;
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║{CenterText(title, 67)}║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        // Vẽ menu với màu sắc
        public static void DrawMenu(string title, string[] options)
        {
            Console.ForegroundColor = PRIMARY_COLOR;
            Console.WriteLine($"\n┌─── {title} ───┐");
            Console.ResetColor();

            for (int i = 0; i < options.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("│ ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{i + 1}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($". {options[i]}");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("│ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("0");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(". Quay lại / Thoát");

            Console.ForegroundColor = PRIMARY_COLOR;
            Console.WriteLine("└" + new string('─', title.Length + 10) + "┘");
            Console.ResetColor();
        }

        // Hiển thị thông báo thành công
        public static void ShowSuccess(string message)
        {
            Console.ForegroundColor = SUCCESS_COLOR;
            Console.WriteLine($"\n✓ {message}");
            Console.ResetColor();
        }

        // Hiển thị thông báo lỗi
        public static void ShowError(string message)
        {
            Console.ForegroundColor = ERROR_COLOR;
            Console.WriteLine($"\n✗ {message}");
            Console.ResetColor();
        }

        // Hiển thị thông báo cảnh báo
        public static void ShowWarning(string message)
        {
            Console.ForegroundColor = WARNING_COLOR;
            Console.WriteLine($"\n⚠ {message}");
            Console.ResetColor();
        }

        // Hiển thị thông tin
        public static void ShowInfo(string message)
        {
            Console.ForegroundColor = INFO_COLOR;
            Console.WriteLine($"ℹ {message}");
            Console.ResetColor();
        }

        // Nhập dữ liệu với prompt màu
        public static string GetInput(string prompt)
        {
            Console.ForegroundColor = PRIMARY_COLOR;
            Console.Write($"▸ {prompt}: ");
            Console.ForegroundColor = ConsoleColor.White;
            string input = Console.ReadLine();
            Console.ResetColor();
            return input;
        }

        // Vẽ đường phân cách
        public static void DrawSeparator()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(new string('─', 67));
            Console.ResetColor();
        }

        // Căn giữa text
        private static string CenterText(string text, int width)
        {
            if (text.Length >= width)
                return text;
            int leftPadding = (width - text.Length) / 2;
            int rightPadding = width - text.Length - leftPadding;
            return new string(' ', leftPadding) + text + new string(' ', rightPadding);
        }

        // Hiển thị thông tin với label màu
        public static void DisplayField(string label, string value)
        {
            Console.ForegroundColor = PRIMARY_COLOR;
            Console.Write($"{label,-20}: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        // Nhấn phím để tiếp tục
        public static void PressKeyToContinue()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("\nNhấn phím bất kỳ để tiếp tục...");
            Console.ResetColor();
            Console.ReadKey();
        }

        // Vẽ box thông tin
        public static void DrawInfoBox(string title, string[] content)
        {
            int maxLength = title.Length;
            foreach (string line in content)
            {
                if (line.Length > maxLength)
                    maxLength = line.Length;
            }
            maxLength += 4;

            Console.ForegroundColor = PRIMARY_COLOR;
            Console.WriteLine("┌─" + new string('─', maxLength) + "─┐");
            Console.WriteLine($"│ {title.PadRight(maxLength)} │");
            Console.WriteLine("├─" + new string('─', maxLength) + "─┤");

            Console.ForegroundColor = ConsoleColor.White;
            foreach (string line in content)
            {
                Console.ForegroundColor = PRIMARY_COLOR;
                Console.Write("│ ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(line.PadRight(maxLength));
                Console.ForegroundColor = PRIMARY_COLOR;
                Console.WriteLine(" │");
            }

            Console.WriteLine("└─" + new string('─', maxLength) + "─┘");
            Console.ResetColor();
        }

        // Vẽ bảng dữ liệu
        public static void DrawTableHeader(string[] headers, int[] columnWidths)
        {
            Console.ForegroundColor = HEADER_COLOR;
            Console.Write("┌");
            for (int i = 0; i < headers.Length; i++)
            {
                Console.Write(new string('─', columnWidths[i] + 2));
                if (i < headers.Length - 1)
                    Console.Write("┬");
            }
            Console.WriteLine("┐");

            Console.Write("│");
            for (int i = 0; i < headers.Length; i++)
            {
                Console.Write($" {headers[i].PadRight(columnWidths[i])} ");
                if (i < headers.Length - 1)
                    Console.Write("│");
            }
            Console.WriteLine("│");

            Console.Write("├");
            for (int i = 0; i < headers.Length; i++)
            {
                Console.Write(new string('─', columnWidths[i] + 2));
                if (i < headers.Length - 1)
                    Console.Write("┼");
            }
            Console.WriteLine("┤");
            Console.ResetColor();
        }

        public static void DrawTableRow(string[] values, int[] columnWidths)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("│");
            for (int i = 0; i < values.Length; i++)
            {
                Console.Write($" {values[i].PadRight(columnWidths[i])} ");
                if (i < values.Length - 1)
                    Console.Write("│");
            }
            Console.WriteLine("│");
            Console.ResetColor();
        }

        public static void DrawTableFooter(int[] columnWidths)
        {
            Console.ForegroundColor = HEADER_COLOR;
            Console.Write("└");
            for (int i = 0; i < columnWidths.Length; i++)
            {
                Console.Write(new string('─', columnWidths[i] + 2));
                if (i < columnWidths.Length - 1)
                    Console.Write("┴");
            }
            Console.WriteLine("┘");
            Console.ResetColor();
        }

        // Xác nhận hành động
        public static bool ConfirmAction(string message)
        {
            Console.ForegroundColor = WARNING_COLOR;
            Console.Write($"\n⚠ {message} (Y/N): ");
            Console.ForegroundColor = ConsoleColor.White;
            string response = Console.ReadLine();
            Console.ResetColor();
            return response.ToUpper() == "Y";
        }
    }
}
