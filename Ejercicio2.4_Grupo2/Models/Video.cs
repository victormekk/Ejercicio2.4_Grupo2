using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio2._4_Grupo2.Models
{
    [Table("Videos")]
    public class Video
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string VideoData { get; set; }
    }
}
