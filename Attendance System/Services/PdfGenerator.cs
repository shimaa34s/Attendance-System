using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Attendance_System.Models;
using Attendance_System.Services;

namespace Attendance_System.Services
{
    public class PdfGenerator
    {
        private readonly IStudentService _studentService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<PdfGenerator> _logger;

        // Constructor to inject dependencies
        public PdfGenerator(IStudentService studentService, IWebHostEnvironment webHostEnvironment, ILogger<PdfGenerator> logger)
        {
            _studentService = studentService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        // Method to generate PDF for a student card
        public void GenerateStudentCardPdf(int studentId)
        {
            // Retrieve student data from the database
            var student = _studentService.GetById(studentId);

            // If the student is not found, throw an exception
            if (student == null)
            {
                throw new ArgumentException("Student not found.");
            }

            // Define the file path for the PDF
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, $"Student_{student.Id}_Card.pdf");

            try
            {
                // Initialize PDF writer and document
                using (var writer = new PdfWriter(filePath))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        var document = new Document(pdf);

                        // Add title to the document
                        document.Add(new Paragraph("Student Card")
                            .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                            .SetFontSize(24)
                            .SetBold());

                        // Check if the student has an image and if the file exists
                        if (!string.IsNullOrEmpty(student.img))
                        {
                            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", student.img);

                            if (File.Exists(imagePath))
                            {
                                var imageData = iText.IO.Image.ImageDataFactory.Create(imagePath);
                                var image = new Image(imageData)
                                    .SetAutoScale(true)
                                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                                document.Add(image);
                            }
                            else
                            {
                                _logger.LogWarning("Image not found at path: " + imagePath);
                            }
                        }

                        // Add student details to the document
                        document.Add(new Paragraph($"Name: {student.Name}"));
                        document.Add(new Paragraph($"Email: {student.Email}"));
                        document.Add(new Paragraph($"Age: {student.Age}"));
                        document.Add(new Paragraph($"Department: {student.Department?.DeptName ?? "N/A"}"));

                        // Add student attendance days

                        document.Add(new Paragraph($"Attendance Day (Saturday): {(student.StudentAttendece.Saturday ? "true" : "false")}"));
                        document.Add(new Paragraph($"Attendance Day (Sunday): {(student.StudentAttendece.Sunday ? "true" : "false")}"));
                        document.Add(new Paragraph($"Attendance Day (Monday): {(student.StudentAttendece.Monday ? "true" : "false")}"));
                        document.Add(new Paragraph($"Attendance Day (Tuesday): {(student.StudentAttendece.Tuesday ? "true" : "false")}"));
                        document.Add(new Paragraph($"Attendance Day (Wednesday): {(student.StudentAttendece.Wednesday ? "true" : "false")}"));
                        document.Add(new Paragraph($"Attendance Day (Thursday): {(student.StudentAttendece.Thursday ? "true" : "false")}"));
                        document.Add(new Paragraph($"Attendance Day (Friday): {(student.StudentAttendece.Friday ? "true" : "false")}"));
                    
                    // Close the document
                    document.Close();
                    }
                }

                _logger.LogInformation($"PDF file created at: {filePath}");
            }
            catch (Exception ex)
            {
                // Log and rethrow the exception
                _logger.LogError(ex, "An error occurred while generating the PDF.");
                throw;
            }
        }
    }
}
