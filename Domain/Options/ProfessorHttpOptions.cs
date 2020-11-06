using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Options
{
    public class ProfessorHttpOptions
    {
        public Uri ApiBaseUrl { get; set; }
        public string ProfessorPath { get; set; }
        public string PostPath { get; set; }
        public string Name { get; set; }
        public int Timeout { get; set; }
    }
}