using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Course { get; set; } = "";

        [Range(0, 100)]
        public int Score { get; set; }

        public string Status
        {
            get
            {
                return Score >= 50 ? "Pass" : "Fail";
            }
        }
    }
}
