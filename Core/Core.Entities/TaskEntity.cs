using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime DeadlineDate { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public string EmployeeFullName { get; set; }
    }
}