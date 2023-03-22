using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace TelHai.CS.Client.Models
{
    public class Exam
    {
        private int? minute, hour, day, month, year;
        private double? totalTime;

        public int Id { get; set; }
        public string Name { get; set; }
        public string _id { get; set; }
        public int? DateMinute
        {
            get { return minute; }
            set
            {
                if (value < 0 || value > 59)
                    minute = 0;
                else
                    minute = value;
            }
        }
        public int? DateHour
        {
            get { return hour; }
            set
            {
                if (value < 0 || value > 23)
                    hour = 0;
                else
                    hour = value;
            }
        }
        public int? DateDay
        {
            get { return day; }
            set
            {
                if (value < 1)
                {
                    day = 1;
                }
                else if ((DateMonth == 1 || DateMonth == 3 || DateMonth == 5 || DateMonth == 7 || DateMonth == 8 || DateMonth == 10 || DateMonth == 12) && value > 31)
                {
                    day = 31;
                }
                else if ((DateMonth == 4 || DateMonth == 6 || DateMonth == 9 || DateMonth == 11) && value > 30)
                {
                    day = 30;
                }
                else if (DateMonth == 2 && (DateYear % 4 == 0 && value > 29))
                {
                    day = 29;
                }
                else if (DateMonth == 2 && (DateYear % 4 != 0 && value > 28))
                {
                    day = 28;
                }
                else
                    day = value;
            }
        }
        public int? DateMonth
        {
            get { return month; }
            set
            {
                if (value < 1 || value > 12)
                    month = 1;
                else
                    month = value;
            }
        }
        public int? DateYear
        {
            get { return year; }
            set
            {
                if (value < 2023)
                    year = 2023;
                else
                    year = value;
            }
        }
        public string? TeacherName { get; set; }
        public double? TotalTime
        {
            get { return totalTime; }
            set
            {
                if (value <= 0)
                    totalTime = 1;
                else
                    totalTime = value;
            }
        }
        public bool? IsOrderRandom { get; set; }
        public List<Question> Questions { get; set; }
        public List<Submit> Submissions { get; set; }

        public Exam() : this("Exam from API", DateTime.Now.Minute, DateTime.Now.Hour, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, "", 1, false, new List<Question>()) { }
        public Exam(string name, int minute, int hour, int day, int month, int year, string teacherName, double totalTime, bool isOrderRandom, List<Question> questions)
        {
            Name = name;
            _id = string.Empty;
            DateMinute = minute;
            DateHour = hour;
            DateDay = day;
            DateMonth = month;
            DateYear = year;
            TeacherName = teacherName;
            TotalTime = totalTime;
            IsOrderRandom = isOrderRandom;
            Questions = questions;
            Submissions = new List<Submit>();
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(Name);
            string dd, dm, dh, dmin;
            if (DateDay < 10) dd = 0 + DateDay.ToString();
            else dd = DateDay.ToString();

            if (DateMonth < 10) dm = 0 + DateMonth.ToString();
            else dm = DateMonth.ToString();

            if (DateHour < 10) dh = 0 + DateHour.ToString();
            else dh = DateHour.ToString();

            if (DateMinute < 10) dmin = 0 + DateMinute.ToString();
            else dmin = DateMinute.ToString();


            sb.AppendFormat(", {0}/{1}/{2}, {3}:{4}", dd, dm, DateYear, dh, dmin);
            return sb.ToString();
        }
    }
}
