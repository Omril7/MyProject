﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TelHai.CS.ServerAPI.Models;

namespace TelHai.CS.ServerAPI.Controllers
{
    [Route("API/Exam")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly ExamContext _context;

        public ExamsController(ExamContext context)
        {
            _context = context;
        }

        // GET: API/Exam
        [HttpGet]
        public async Task<ActionResult<List<Exam>>> GetExams()
        {
            return await _context.Exams.Include(e => e.Questions)
                                       .ThenInclude(q => q.Answers)
                                       .Select(e => new Exam
                                       {
                                           Id = e.Id,
                                           Name = e.Name,
                                           _id = e._id,
                                           DateDay = e.DateDay,
                                           DateHour = e.DateHour,
                                           DateMinute = e.DateMinute,
                                           DateMonth = e.DateMonth,
                                           DateYear = e.DateYear,
                                           TeacherName = e.TeacherName,
                                           TotalTime = e.TotalTime,
                                           IsOrderRandom = e.IsOrderRandom,
                                           Questions = e.Questions.Select(q => new Question
                                           {
                                               Id = q.Id,
                                               Text = q.Text,
                                               Answers = q.Answers
                                           }).ToList()
                                       }).ToListAsync();
        }

        // GET: API/Exam/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Exam>> GetExam(int id)
        {
            var exam = await _context.Exams.Include(e => e.Questions).ThenInclude(q => q.Answers).FirstOrDefaultAsync(e => e.Id == id);

            if (exam == null)
            {
                return NotFound();
            }

            return exam;
        }

        // PUT: API/Exam/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExam(int id, Exam exam)
        {
            if (id != exam.Id)
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
                if (!ExamExists(id))
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

        // POST: API/Exam
        [HttpPost]
        public async Task<ActionResult<Exam>> PostExam(Exam exam)
        {
            _context.Exams.Add(exam);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExam", new { id = exam.Id }, exam);
        }

        // DELETE: API/Exam/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(int id)
        {
            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }

            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExamExists(int id)
        {
            return _context.Exams.Any(e => e.Id == id);
        }
    }
}