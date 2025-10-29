using System;
using System.Xml.Serialization;

namespace AttendanceSystem
{
    // ==================== WORK SHIFT ====================
    // Quản lý ca làm việc (sáng, chiều, tối)

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
}