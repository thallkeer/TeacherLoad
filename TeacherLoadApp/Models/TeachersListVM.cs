namespace TeacherLoadApp.Models
{
    public class TeachersListVM
    {
        public int TeacherID { get; set; }
        /// <summary>
        /// Full name
        /// </summary>
        public string TeacherName { get; set; } 
        /// <summary>
        /// Is teacher selected
        /// </summary>
        public bool Selected { get; set; } 
    }
}
