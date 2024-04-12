using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationLoan.Models
{
    public class Loan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoanId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public double PrincipalAmount { get; set; }
        [Required]
        public int NoOfYears { get; set; }
        public double InterestRate { get; set; }
        public double InterestAmount { get; set; }
    }
}
