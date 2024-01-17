using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebServer.Shared
{
    public class SubElement
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Window")]
        public int WindowId { get; set; }

        public int Element { get; set; }

        public string Type { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}
