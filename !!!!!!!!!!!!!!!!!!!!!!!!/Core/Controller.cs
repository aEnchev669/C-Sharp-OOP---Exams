using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories;
using UniversityCompetition.Repositories.Contracts;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Core
{
    public class Controller : IController
    {
        public Controller()
        {
            students = new StudentRepository();
            subjects = new SubjectRepository();
            universities = new UniversityRepository();
        }
        int idStud = 1;
        int idUni = 1;
        int idSubj = 1;
        private IRepository<IStudent> students;
        private IRepository<ISubject> subjects;
        private IRepository<IUniversity> universities;
        Dictionary<IUniversity, int> info = new Dictionary<IUniversity, int>();
        public string AddStudent(string firstName, string lastName)
        {

            string name = firstName + " " + lastName;
            IStudent student = students.FindByName(name);
            if (student != null)
            {
                return string.Format(OutputMessages.AlreadyAddedStudent, firstName, lastName);
            }

            student = new Student(idStud, firstName, lastName);
            idStud++;
            students.AddModel(student);

            return String.Format($"Student {firstName} {lastName} is added to the StudentRepository!");
        }

        public string AddSubject(string subjectName, string subjectType)
        {
            ISubject subject = subjects.FindByName(subjectName);
            if (subject != null)
            {
                return String.Format(OutputMessages.AlreadyAddedSubject, subjectName);
            }


            if (subjectType == "TechnicalSubject")
            {
                subject = new TechnicalSubject(idSubj, subjectName);
            }
            else if (subjectType == "EconomicalSubject")
            {
                subject = new EconomicalSubject(idSubj, subjectName);

            }
            else if (subjectType == "HumanitySubject")
            {
                subject = new HumanitySubject(idSubj, subjectName);

            }
            else
            {
                return String.Format(OutputMessages.SubjectTypeNotSupported, subjectType);
            }
            idSubj++;

            subjects.AddModel(subject);

            return String.Format(OutputMessages.SubjectAddedSuccessfully, subjectType, subjectName, subjects.GetType().Name);


        }

        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {

            IUniversity university = universities.FindByName(universityName);
            if (university != null)
            {
                return String.Format(OutputMessages.AlreadyAddedUniversity, universityName);
            }

            List<int> ints = new List<int>();
            foreach (var item in requiredSubjects)
            {
                ISubject subject = subjects.FindByName(item);
                ints.Add(subject.Id); //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            }

            university = new University(idUni, universityName, category, capacity, ints);
            idUni++;

            universities.AddModel(university);
            info.Add(university, 0);
            return String.Format(OutputMessages.UniversityAddedSuccessfully, universityName, universities.GetType().Name);
        }

        public string ApplyToUniversity(string studentName, string universityName)
        {
            IStudent student = students.FindByName(studentName);
            if (student == null)
            {
                return String.Format(OutputMessages.StudentNotRegitered, student.FirstName, student.LastName);
            }

            IUniversity university = universities.FindByName(universityName);
            if (university == null)
            {
                return String.Format(OutputMessages.UniversityNotRegitered, universityName);
            }
            if (university.Capacity > info[university])
            {


                int[] ints = university.RequiredSubjects.ToArray();
                int[] ints2 = student.CoveredExams.ToArray();
                if (ints.Length == ints2.Length)
                {
                    for (int i = 0; i < ints.Length; i++)
                    {
                        if (ints[i] != ints2[i]) //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        {
                            return String.Format(OutputMessages.StudentHasToCoverExams, studentName, universityName);
                        }
                    }
                }
                else
                {
                    return String.Format(OutputMessages.StudentHasToCoverExams, studentName, universityName);
                }
                if (student.University != null)
                {
                    if (student.University.Name == universityName)
                    {
                        return String.Format(OutputMessages.StudentAlreadyJoined, student.FirstName, student.LastName, universityName);
                    }
                }

                //if (university.Capacity > university.)
                //{

                //}                    !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                student.JoinUniversity(university);
                info[university]++;
                return String.Format(OutputMessages.StudentSuccessfullyJoined, student.FirstName, student.LastName, universityName);
            }
            return null;
        }

        public string TakeExam(int studentId, int subjectId)
        {
            IStudent student = students.FindById(studentId);
            if (student == null)
            {
                return OutputMessages.InvalidStudentId;
            }

            ISubject subject = subjects.FindById(subjectId);
            if (subject == null)
            {
                return OutputMessages.InvalidSubjectId;
            }


            if (student.CoveredExams.Any(e => e == subjectId))
            {
                return String.Format(OutputMessages.StudentAlreadyCoveredThatExam, student.FirstName, student.LastName, subject.Name);
            }

            student.CoverExam(subject);

            return String.Format(OutputMessages.StudentSuccessfullyCoveredExam, student.FirstName, student.LastName, subject.Name);
        }
        public string UniversityReport(int universityId)
        {
            IUniversity university = universities.FindById(universityId);

            int countOfStudents = 0;
            int capacityLeft = 0;
            if (info[university] >= university.Capacity)
            {
                countOfStudents = university.Capacity;
            }
            else
            {
                capacityLeft = university.Capacity - info[university];
                countOfStudents = info[university];
            }

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"*** {university.Name} ***")
                .AppendLine($"Profile: {university.Category}")
                .AppendLine($"Students admitted: {countOfStudents}")
                .AppendLine($"University vacancy: {capacityLeft}");

            return sb.ToString().TrimEnd();
        }
    }
}
