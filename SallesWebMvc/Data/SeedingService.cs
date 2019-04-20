using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SallesWebMvc.Models;
using SallesWebMvc.Models.Enums;

namespace SallesWebMvc.Data
{
    public class SeedingService
    {
        private SallesWebMvcContext _context;

        public SeedingService(SallesWebMvcContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if(_context.Departament.Any() || _context.Seller.Any() || _context.SalesRecords.Any())
            {
                return;
            }

            //Seeding Departaments
            Departament d1 = new Departament(1, "Electronics");
            Departament d2 = new Departament(2, "House");
            Departament d3 = new Departament(3, "Clotches");
            Departament d4 = new Departament(4, "Gardeen");

            //Seeding Sellers
            Seller s1 = new Seller(1, "Milton", "milton@seller.com", new DateTime(1974,01,03), 1000.0,d1);
            Seller s2 = new Seller(2, "Roberta", "roberta@seller.com", new DateTime(1979, 04, 10), 1500.0, d1);
            Seller s3 = new Seller(3, "Arthur", "arthur@seller.com", new DateTime(1999, 02, 15), 2000.0, d2);
            Seller s4 = new Seller(4, "Esther", "esther@seller.com", new DateTime(2000, 07, 25), 1500.0, d2);
            Seller s5 = new Seller(5, "Victor Hugo", "hogo@seller.com", new DateTime(2001, 09, 20), 2000.0, d3);
            Seller s6 = new Seller(6, "Daniela", "daniela@seller.com", new DateTime(1976, 09, 17), 1500.0, d3);
            Seller s7 = new Seller(7, "Denise", "denise@seller.com", new DateTime(1980, 05, 21), 2000.0, d4);
            Seller s8 = new Seller(8, "Eduardo", "eduardo@seller.com", new DateTime(1974, 04, 26), 1500.0, d4);

            //Seeding Sales Records
            SalesRecord r1 = new SalesRecord(1, new DateTime(2019, 04, 15), 3500.0, SalesStatus.Billed, s1);
            SalesRecord r2 = new SalesRecord(2, new DateTime(2019, 04, 16), 1650.0, SalesStatus.Pending, s1);
            SalesRecord r3 = new SalesRecord(3, new DateTime(2019, 04, 15), 4850.0, SalesStatus.Billed, s2);
            SalesRecord r4 = new SalesRecord(4, new DateTime(2019, 04, 17), 789.0, SalesStatus.Billed, s2);
            SalesRecord r5 = new SalesRecord(5, new DateTime(2019, 04, 15), 680.0, SalesStatus.Pending, s3);
            SalesRecord r6 = new SalesRecord(6, new DateTime(2019, 04, 15), 1180.5, SalesStatus.Pending, s3);
            SalesRecord r7 = new SalesRecord(7, new DateTime(2019, 04, 15), 185.2, SalesStatus.Billed, s4);
            SalesRecord r8 = new SalesRecord(8, new DateTime(2019, 04, 19), 796.0, SalesStatus.Billed, s4);
            SalesRecord r9 = new SalesRecord(9, new DateTime(2019, 04, 16), 810.9, SalesStatus.Canceled, s5);
            SalesRecord r10 = new SalesRecord(10, new DateTime(2019, 04, 19), 1259.1, SalesStatus.Billed, s5);
            SalesRecord r11 = new SalesRecord(11, new DateTime(2019, 04, 15), 254.3, SalesStatus.Canceled, s6);
            SalesRecord r12 = new SalesRecord(12, new DateTime(2019, 04, 18), 1187.0, SalesStatus.Canceled, s6);
            SalesRecord r13 = new SalesRecord(13, new DateTime(2019, 04, 15), 351.25, SalesStatus.Pending, s7);
            SalesRecord r14 = new SalesRecord(14, new DateTime(2019, 04, 17), 925.21, SalesStatus.Pending, s7);
            SalesRecord r15 = new SalesRecord(15, new DateTime(2019, 04, 17), 687.28, SalesStatus.Billed, s8);
            SalesRecord r16 = new SalesRecord(16, new DateTime(2019, 04, 19), 463.44, SalesStatus.Billed, s8);

            //Update entities
            _context.Departament.AddRange(d1, d2, d3, d4);
            _context.Seller.AddRange(s1, s2, s3, s4, s5, s6, s7, s8);
            _context.SalesRecords.AddRange(r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11, r12, r13, r14, r15, r16);

            //Commit changes 
            _context.SaveChanges();

        }
    }
}
