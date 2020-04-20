using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel_california.Models
{
    public class EmpModel
    {
        /// <summary>  
        /// DOB datetime data type property   
        /// to display date type control  
        /// </summary>  
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }
    }
}