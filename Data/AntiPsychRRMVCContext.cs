#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AntiPsychRRMVC.Models;

namespace AntiPsychRRMVC.Data
{
    public class AntiPsychRRMVCContext : DbContext
    {
        public AntiPsychRRMVCContext (DbContextOptions<AntiPsychRRMVCContext> options)
            : base(options)
        {
        }

        public DbSet<AntiPsychRRMVC.Models.Drug> Drug { get; set; }
       
    }
}
