using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidShort.Domain.Entities
{
    [Table("url")]
    public class Urls
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("url")]
        public string Url { get; set; }
        [Column("shorturl")]
        public string ShortURL { get; set; }
        [Column("hits")]
        public int Hits { get; set; }
    }
}
