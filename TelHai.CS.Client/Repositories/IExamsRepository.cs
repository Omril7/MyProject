using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TelHai.CS.Client.Models;

namespace TelHai.CS.Client.Repositories
{
    public interface IExamsRepository
    {
        void AddExam(Exam exam);
        void UpdateExam(Exam exam);
        void DeleteExam(string id);
        List<Exam> GetAllExams();
        List<Exam> GetExam(string name);
        void Save();
        void Load(string path);
    }
}
