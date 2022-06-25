using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Teknorix_project.Models
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage ="First name is required")]
        public string  FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [MaxLength(10)]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Mobile number is required")]
        [MaxLength(10)]
        public string MobileNumber { get; set; }
        //public string DesgnID { get; set; }

        [ForeignKey("Department")]
        [Required(ErrorMessage = "Phone number is required")]
        public int DeptID { get; set; }
        public virtual Departments Department { get; set; }
    }
}
