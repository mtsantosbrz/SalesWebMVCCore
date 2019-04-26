using SallesWebMvc.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SallesWebMvc.Models
{
    public class SalesRecord
    {
        public SalesRecord()
        {

        }
        public SalesRecord(int id, DateTime date, Double amount, SalesStatus status, Seller seller)
        {
            ID = id;
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }
        public int ID { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        public Double Amount { get; set; }

        public SalesStatus Status { get; set; }

        public Seller Seller { get; set; }
    }
}
