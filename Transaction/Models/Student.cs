using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.Models
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public bool IsStudent { get; set; }
        public DateTime Birthday { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
    public class Address
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int AddressId { get; set; }
        [StringLength(6)]
        public string PochtaIndex { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
