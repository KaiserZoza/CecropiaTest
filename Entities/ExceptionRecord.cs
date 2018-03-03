
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class ExceptionRecord
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ExceptionRecordId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime ExceptionRecordDate { get; set; }
        [Display(Name = "Message")]
        public string ExceptionMessage { get; set; }
        [Display(Name = "StackTrace")]
        public string ExceptionStackTrace { get; set; }
        [Display(Name = "Source")]
        public string Source { get; set; }
        [Display(Name = "InnerMessage")]
        public string InnerExceptionMessage { get; set; }
        [Display(Name = "InnerST")]
        public string InnerExceptionStackTrace { get; set; }
        [Display(Name = "InnerSource")]
        public string InnerExceptionSource { get; set; }
        [Display(Name = "TypeName")]
        public string ExceptionTypeName { get; set; }

        public ExceptionRecord()
        {
            this.ExceptionRecordDate = DateTime.UtcNow;
        }
    }
}
