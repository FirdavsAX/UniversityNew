namespace University.Models.CourseViewModels
{
    public class CourseInActionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Room { get; set; }
        public decimal Price { get; set; }
        public int Hours { get; set; }
        public int InstructorId { get; set; }
        public string Instructor { get; set; }
        
    }
}
