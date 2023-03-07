using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelHai.CS.ServerAPI.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string ExamId { get; set; }
        public double _grade { get; set; }
        public List<Error> Errors { get; set; }
        public Grade()
        {
            Errors = new List<Error>();
        }
        public override string ToString()
        {
            return _grade.ToString();
        }
    }
}
