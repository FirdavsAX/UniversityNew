﻿namespace University.Models.InstructionViewModels
{
    public class InstructorDisplayViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
    }
}