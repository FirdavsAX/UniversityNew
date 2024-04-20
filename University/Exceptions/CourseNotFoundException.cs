using System.CodeDom;

namespace University.Exceptions
{
    public class CourseNotFoundException:Exception
    {
        public CourseNotFoundException()
        {
            
        }
        public CourseNotFoundException(string message) : base(message)
        {
            
        }
    }
}
