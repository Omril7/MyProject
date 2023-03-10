using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TelHai.CS.ServerAPI.Models;

namespace TelHai.CS.ServerAPI.Controllers
{
    [Route("API/Exams")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly ExamContext _context;

        public ExamsController(ExamContext context)
        {
            _context = context;
        }

        /*
         * Exams
        */
        // GET: API/Exams
        [HttpGet]
        public async Task<ActionResult<List<Exam>>> GetExams()
        {
            return await _context.Exams.Include(e => e.Questions)
                                       .ThenInclude(q => q.Answers)
                                       .ToListAsync();
        }

        // GET: API/Exams/{examId}
        [HttpGet("{examId}")]
        public async Task<ActionResult<Exam>> GetExam(int examId)
        {
            var exam = await _context.Exams.Include(e => e.Questions)
                                           .ThenInclude(q => q.Answers)
                                           .Include(e => e.Grades)
                                           .ThenInclude(g => g.Errors)
                                           .FirstOrDefaultAsync(e => e.Id == examId);
            if (exam == null)
            {
                return NotFound();
            }

            return exam;
        }

        // POST: API/Exams
        [HttpPost]
        public async Task<ActionResult<Exam>> PostExam(Exam exam)
        {
            _context.Exams.Add(exam);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExam", new { id = exam.Id }, exam);
        }

        // PUT: API/Exams/{examId}
        [HttpPut("{examId}")]
        public async Task<IActionResult> PutExam(int examId, Exam exam)
        {
            if (examId != exam.Id)
            {
                return BadRequest();
            }

            _context.Entry(exam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamExists(examId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: API/Exams/{examId}
        [HttpDelete("{examId}")]
        public async Task<IActionResult> DeleteExam(int examId)
        {
            var exam = await _context.Exams.FindAsync(examId);
            if (exam == null)
            {
                return NotFound();
            }

            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /*
         * Questions
        */
        // POST: API/Exams/{examId}/Questions
        [HttpPost("{examId}/Questions")]
        public async Task<ActionResult<Question>> PostQuestion(int examId, Question question)
        {
            var exam = await _context.Exams.FindAsync(examId);
            if (exam == null)
            {
                return NotFound();
            }
            exam.Questions.Add(question);

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestion", new { id = question.Id }, question);
        }

        // DELETE: API/Exams/{examId}/Questions/{questionId}
        [HttpDelete("{examId}/Questions/{questionId}")]
        public async Task<IActionResult> DeleteQuestion(int examId, int questionId)
        {
            var exam = await _context.Exams.Include(e => e.Questions).FirstOrDefaultAsync(e => e.Id == examId);
            if (exam == null)
            {
                return NotFound();
            }

            var question = exam.Questions.FirstOrDefault(q => q.Id == questionId);
            if (question == null)
            {
                return NotFound();
            }

            exam.Questions.Remove(question);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /*
         * Grades
        */
        // POST: API/Exams/{examId}/Grades
        [HttpPost("{examId}/Grades")]
        public async Task<ActionResult<Grade>> PostGrade(int examId, Grade grade)
        {
            var exam = await _context.Exams.FindAsync(examId);
            if (exam == null)
            {
                return NotFound();
            }
            exam.Grades.Add(grade);

            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGrade", new { id = grade.Id }, grade);
        }

        private bool ExamExists(int id)
        {
            return _context.Exams.Any(e => e.Id == id);
        }
    }
}
