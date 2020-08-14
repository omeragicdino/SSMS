using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json.Linq;
using SSMS2.Infrastructure;

namespace SSMS2.Controllers
{

    [Route("api/library/")]
    [ApiController]
    // GET api/values
    public class ValuesController : ControllerBase
    { 
       
        [HttpGet("books")]
        public JArray getBooks()
        {
            DbConnection dbConnection = new DbConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "dbo.SP_GetBooks";

            return dbConnection.Connection(command);
        }

        
        [HttpGet("students")]
        public JArray getStudents()
        {
            DbConnection dbConnection = new DbConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "dbo.SP_GetAllStudents";

            return dbConnection.Connection(command);
        }

       
        [HttpGet("complaints")]
        public JArray getComplaints()
        {
            DbConnection dbConnection = new DbConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "dbo.SP_GetComplaints";

            return dbConnection.Connection(command);
        }

       
        [HttpGet("issues")]
        public JArray getIssues()
        {
            DbConnection dbConnection = new DbConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "dbo.SP_GetIssues";

            return dbConnection.Connection(command);
        }


       
        [HttpGet("exams")]
        public JArray getExams()
        {
            DbConnection dbConnection = new DbConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "dbo.SP_GetExams";

            return dbConnection.Connection(command);
        }

        
        [HttpGet("exam-schedule")]
        public JArray gewtExamSchedule()
        {
            DbConnection dbConnection = new DbConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "dbo.SP_GetExamSchedule";

            return dbConnection.Connection(command);
        }

        
        [HttpGet("pending-issues")]
        public JArray getPendingIssues()
        {
            DbConnection dbConnection = new DbConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "dbo.SP_GetPendingIssues";

            return dbConnection.Connection(command);
        }

       
        [HttpGet("professor-emails")]
        public JArray getProfessorEmails()
        {
            DbConnection dbConnection = new DbConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "dbo.SP_GetProfessorEmails";

            return dbConnection.Connection(command);
        }

        
        [HttpGet("requests")]
        public JArray getRequests()
        {
            DbConnection dbConnection = new DbConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "dbo.SP_GetRequests";

            return dbConnection.Connection(command);
        }

        
        [HttpGet("return-records")]
        public JArray getReturnRecords()
        {
            DbConnection dbConnection = new DbConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "dbo.SP_GetReturnRecords";

            return dbConnection.Connection(command);
        }

        
        [HttpGet("subjects")]
        public JArray getSubjects()
        {
            DbConnection dbConnection = new DbConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "dbo.SP_GetSubjectsInfo";

            return dbConnection.Connection(command);
        }



        //___________________________________________________________________________________________________________________________________________________


        // GET api/values/5


        [HttpGet("student-info/{id}")]
        public JArray getStudentInfo(int id)
        {
            DbConnection dbConnection = new DbConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "dbo.SP_GetStudentInfo";
            command.Parameters.Add(new SqlParameter("@ID", id));

            return dbConnection.Connection(command); 
        }

        
        [HttpGet("student-exam-registrations/{id}")]
        public JArray getStudentExamRegistrations(int id)
        {
            DbConnection dbConnection = new DbConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "dbo.SP_GetExamRegistrationsByStudentID";
            command.Parameters.Add(new SqlParameter("@ID", id));

            return dbConnection.Connection(command);
        }

       
        [HttpGet("subject-exam-registrations/{id}")]
        public JArray getSubjectExamRegistrations(int id)
        {
            DbConnection dbConnection = new DbConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "dbo.SP_GetExamRegistrationsBySubjectID";
            command.Parameters.Add(new SqlParameter("@ID", id));

            return dbConnection.Connection(command);
        }


        [HttpGet("index/{id}")]
        public JArray getIndex(int id)
        {
            DbConnection dbConnection = new DbConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "dbo.SP_GetIndex";
            command.Parameters.Add(new SqlParameter("@ID", id));

            return dbConnection.Connection(command);
        }

        
        [HttpGet("library-card/{id}")]
        public JArray getLCard(int id)
        {
            DbConnection dbConnection = new DbConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "dbo.SP_GetLCard";
            command.Parameters.Add(new SqlParameter("@ID", id));

            return dbConnection.Connection(command);
        }

       

        //___________________________________________________________________________________________________________________________________________________


        // POST api/values


        [HttpPost("book")]
        public  JObject postBook([FromBody] JObject book)
        {
            DbConnection dbConnection = new DbConnection();

            string name = book["Name"] != null ? book["Name"].Value<string>() : string.Empty;
            string author = book["Author"] != null ? book["Author"].Value<string>() : string.Empty;

            if (name != string.Empty && author != string.Empty)
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "dbo.SP_PostBook";
                command.Parameters.Add(new SqlParameter("@Name", name));
                command.Parameters.Add(new SqlParameter("@Author", author));

                var success = dbConnection.Connection(command);

                return JObject.FromObject(new { message = success.Any() == true ? "Nije uspjesno unesena nova knjiga." : "Uspjesno je unesena nova knjiga." });
            }
            else
                return JObject.FromObject( new { message = "Niste unijeli validan Json" });
        }


     
        [HttpPost("complaint")]
        public JObject postComplaint([FromBody] JObject Complaint)
        {
            DbConnection dbConnection = new DbConnection();

            string complaint = Complaint["Complaint"] != null ? Complaint["Complaint"].Value<string>() : string.Empty;
           

            if (complaint != string.Empty)
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "dbo.SP_PostComplaint";
                command.Parameters.Add(new SqlParameter("@Complaint", complaint));
                

                var success = dbConnection.Connection(command);

                return JObject.FromObject(new { message = success.Any() == true ? "Nije uspjesno unesena zalba." : "Uspjesno je unesena zalba." });
            }
            else
                return JObject.FromObject(new { message = "Niste unijeli validan Json" });
        }


        [HttpPost("exam")]
        public JObject postExam([FromBody] JObject exam)
        {
            DbConnection dbConnection = new DbConnection();

            int subject_ID = exam["SubjectID"] != null ? exam["SubjectID"].Value<int>() : 0;
            int professor_ID = exam["ProfessorID"] != null ? exam["ProfessorID"].Value<int>() : 0;
            string date_of_exam = exam["DateOfExam"] != null ? exam["DateOfExam"].Value<string>() : string.Empty;
            string time_of_exam = exam["TimeOfExam"] != null ? exam["TimeOfExam"].Value<string>() : string.Empty;

            if (subject_ID != 0 && professor_ID != 0 && date_of_exam != string.Empty && time_of_exam!= string.Empty)
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "dbo.SP_PostExam";
                command.Parameters.Add(new SqlParameter("@SubjectID", subject_ID));
                command.Parameters.Add(new SqlParameter("@ProfessorID", professor_ID));
                command.Parameters.Add(new SqlParameter("@DateOfExam", date_of_exam));
                command.Parameters.Add(new SqlParameter("@TimeOfExam", time_of_exam));

                var success = dbConnection.Connection(command);

                return JObject.FromObject(new { message = success.Any() == true ? "Nije uspjesno unesen novi ispit." : "Uspjesno je unesen novi ispit." });
            }
            else
                return JObject.FromObject(new { message = "Niste unijeli validan Json" });
        }


        [HttpPost("exam-registration")]
        public JObject postExamRegistration([FromBody] JObject exam_registration)
        {
            DbConnection dbConnection = new DbConnection();

            int exam_ID = exam_registration["ExamID"] != null ? exam_registration["ExamID"].Value<int>() : 0;
            int student_ID = exam_registration["StudentID"] != null ? exam_registration["StudentID"].Value<int>() : 0;

            if (exam_ID != 0 && student_ID != 0)
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "dbo.SP_PostExamRegistration";
                command.Parameters.Add(new SqlParameter("@ExamID", exam_ID));
                command.Parameters.Add(new SqlParameter("@StudentID", student_ID));

                var success = dbConnection.Connection(command);

                return JObject.FromObject(new { message = success.Any() == true ? "Nije uspjesno podnesena prijava za ispit." : "Uspjesno je podnesena prijava za ispit." });
            }
            else
                return JObject.FromObject(new { message = "Niste unijeli validan Json" });
        }



        [HttpPost("grade")]
        public JObject postGrade([FromBody] JObject Grade)
        {
            DbConnection dbConnection = new DbConnection();

            int subject_ID = Grade["SubjectID"] != null ? Grade["SubjectID"].Value<int>() : 0;
            int student_ID = Grade["StudentID"] != null ? Grade["StudentID"].Value<int>() : 0;
            int grade = Grade["Grade"] != null ? Grade["Grade"].Value<int>() : 0;

            if (subject_ID != 0 && student_ID != 0 && grade != 0)
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "dbo.SP_PostGrade";
                command.Parameters.Add(new SqlParameter("@SubjectID", subject_ID));
                command.Parameters.Add(new SqlParameter("@StudentID", student_ID));
                command.Parameters.Add(new SqlParameter("@Grade", grade));

                var success = dbConnection.Connection(command);

                return JObject.FromObject(new { message = success.Any() == true ? "Nije uspjesno unesena ocjena." : "Uspjesno je unesena ocjena." });
            }
            else
                return JObject.FromObject(new { message = "Niste unijeli validan Json" });
        }


        [HttpPost("issue")]
        public JObject postIssue([FromBody] JObject issue)
        {
            DbConnection dbConnection = new DbConnection();

            int book_ID = issue["BookID"] != null ? issue["BookID"].Value<int>() : 0;
            int student_ID = issue["StudentID"] != null ? issue["StudentID"].Value<int>() : 0;

            if (book_ID != 0 && student_ID != 0)
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "dbo.SP_PostIssue";
                command.Parameters.Add(new SqlParameter("@BookID", book_ID));
                command.Parameters.Add(new SqlParameter("@StudentID", student_ID));

                var success = dbConnection.Connection(command);

                return JObject.FromObject(new { message = success.Any() == true ? "Nije uspjesno izdana knjiga." : "Uspjesno je izdana knjiga." });
            }
            else
                return JObject.FromObject(new { message = "Niste unijeli validan Json" });
        }



        [HttpPost("request")]
        public JObject postRequest([FromBody] JObject Request)
        {
            DbConnection dbConnection = new DbConnection();

            int student_ID = Request["StudentID"] != null ? Request["StudentID"].Value<int>() : 0;
            string request = Request["Request"] != null ? Request["Request"].Value<string>() : string.Empty;


            if (student_ID != 0 && request != string.Empty)
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "dbo.SP_PostRequest";
                command.Parameters.Add(new SqlParameter("@StudentID", student_ID));
                command.Parameters.Add(new SqlParameter("@Request", request));


                var success = dbConnection.Connection(command);

                return JObject.FromObject(new { message = success.Any() == true ? "Nije uspjesno podnesen zahtjev." : "Uspjesno je podnesen zahtjev." });
            }
            else
                return JObject.FromObject(new { message = "Niste unijeli validan Json" });
        }



        [HttpPost("return")]
        public JObject postReturn([FromBody] JObject @return)
        {
            DbConnection dbConnection = new DbConnection();

            int issue_ID = @return["IssueID"] != null ? @return["IssueID"].Value<int>() : 0;
            

            if (issue_ID != 0)
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "dbo.SP_PostReturn";
                command.Parameters.Add(new SqlParameter("@IssueID", issue_ID));
               

                var success = dbConnection.Connection(command);

                return JObject.FromObject(new { message = success.Any() == true ? "Nije uspjesno vracena knjiga." : "Uspjesno je vracena knjiga." });
            }
            else
                return JObject.FromObject(new { message = "Niste unijeli validan Json" });
        }


        [HttpPost("student")]
        public JObject postStudent([FromBody] JObject student)
        {
            DbConnection dbConnection = new DbConnection();

            string name = student["Name"] != null ? student["Name"].Value<string>() : string.Empty;
            string city = student["City"] != null ? student["City"].Value<string>() : string.Empty;
            string address = student["Address"] != null ? student["Address"].Value<string>() : string.Empty;
            long phone_number = student["PhoneNumber"] != null ? student["PhoneNumber"].Value<long>() : 0;
            string course = student["Course"] != null ? student["Course"].Value<string>() : string.Empty;
            string email = student["Email"] != null ? student["Email"].Value<string>() : string.Empty;

            if (name != string.Empty && city != string.Empty && address != string.Empty && phone_number != 0 && course != string.Empty && email != string.Empty)
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "dbo.SP_PostStudent";
                command.Parameters.Add(new SqlParameter("@Name", name));
                command.Parameters.Add(new SqlParameter("@City", city));
                command.Parameters.Add(new SqlParameter("@Address", address));
                command.Parameters.Add(new SqlParameter("@PhoneNumber", phone_number));
                command.Parameters.Add(new SqlParameter("@Course", course));
                command.Parameters.Add(new SqlParameter("@Email", email));

                var success = dbConnection.Connection(command);

                return JObject.FromObject(new { message = success.Any() == true ? "Nije uspjesno unesen student u bazu podataka." : "Uspjesno je unesen student u bazu podataka." });
            }
            else
                return JObject.FromObject(new { message = "Niste unijeli validan Json" });
        }







        // DELETE api/values/5
        [HttpDelete("student/{id}")]
        public JObject deleteStudent(int id)
        {
            DbConnection dbConnection = new DbConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "dbo.SP_DeleteStudent";
            command.Parameters.Add(new SqlParameter("@ID", id));

            var success = dbConnection.Connection(command);
            return JObject.FromObject(new { message = success.Any() == true ? "Nije uspjesno izbrisan student." : "Uspjesno je izbrisan student." });
        }


        [HttpDelete("exam/{id}")]
        public JObject deleteExam(int id)
        {
            DbConnection dbConnection = new DbConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "dbo.SP_DeleteExam";
            command.Parameters.Add(new SqlParameter("@ID", id));

            var success = dbConnection.Connection(command);
            return JObject.FromObject(new { message = success.Any() == true ? "Nije uspjesno uklonjen ispit." : "Uspjesno je uklonjen ispit." });
        }


    }
}
