using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelHai.CS.ServerAPI.Models
{
    public class Submit
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string ExamId { get; set; }
        public double _grade { get; set; }
        public List<Error> Errors { get; set; }
        public Submit()
        {
            Errors = new List<Error>();
        }
        public override string ToString()
        {
            string ret = StudentId + ", " + StudentName;
            return ret;
        }
    }
}
