using System;
using System.ComponentModel.DataAnnotations;

namespace CookieClicker.ClassLibrary.Models
{ 
    public partial class ImageIB
    {
        [Key]
        public int IdImageIB { get; set; }
        public int Version { get; set; }
        public byte[] ImageInByte { get; set; }
    }
}
