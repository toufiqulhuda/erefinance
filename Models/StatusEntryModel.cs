using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erefinance.Models
{
    public class StatusEntryModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage= "The District Name is requred.")]
        [Display(Name = "District Name")]
        public String DistrictName { get; set; }

        [Required(ErrorMessage = "The Fund Name is requred.")]
        [Display(Name = "Fund Name")]
        public String FundName { get; set; }

        [Required(ErrorMessage = "The SME Category is requred.")]
        [Display(Name = "SME Category")]
        public String SMECategory { get; set; }

        [Required(ErrorMessage = "The Type of Sector is requred.")]
        [Display(Name = "Types of Sector")]
        public String TypesofSector { get; set; }

        [Required(ErrorMessage = "The Disbursed Female Enterprise is requred.")]
        [Display(Name = "Disbursed Female Enterprise")]
        public long DisbursedFemaleEnterprise { get; set; }

        [Required(ErrorMessage = "The Disbursed Female Amount is requred.")]
        [Display(Name = "Disbursed Female Amount")]
        public long DisbursedFemaleAmount { get; set; }

        [Required(ErrorMessage = "The Disbursed Male Enterprise is requred.")]
        [Display(Name = "Disbursed Male Enterprise")]
        public long DisbursedMaleEnterprise { get; set; }

        [Required(ErrorMessage = "The Disbursed Male Amount is requred.")]
        [Display(Name = "Disbursed Male Amount")]
        public long DisbursedMaleAmount { get; set; }

        [Required(ErrorMessage = "The Approved Female Enterprise is requred.")]
        [Display(Name = "Approved Female Enterprise")]
        public long ApprovedFemaleEnterprise { get; set; }

        [Required(ErrorMessage = "The Approved Female Amount is requred.")]
        [Display(Name = "Approved Female Amount")]
        public long ApprovedFemaleAmount { get; set; }

        [Required(ErrorMessage = "The Approved Male Enterprise is requred.")]
        [Display(Name = "Approved Male Enterprise")]
        public long ApprovedMaleEnterprise { get; set; }

        [Required(ErrorMessage = "The Approved Male Amount is requred.")]
        [Display(Name = "Approved Male Amount")]
        public long ApprovedMaleAmount { get; set; }

        [Required(ErrorMessage = "The Report Date is requred.")]
        [Display(Name = "Report Date")]
        public DateTime ReportDate { get; set; }

        //[Required]
        public string? BranchName { get; set; }

        //[Required]
        public int? BranchCode { get; set; }

        //[Required]
        public string? EntryBy { get; set; }

        [Required]
        public DateTime EntryDate { get; set; }


        public string UpdateBy { get; set; }


        public DateTime UpdateDate { get; set; }



    }
}
