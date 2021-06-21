
using System.IO;
using Xamarin.Forms;

namespace CookieClicker.Classes.Models
{ 
    public partial class ImageIB
    {
        public int IdImageIB { get; set; }
        public int Version { get; set; }
        public byte[] ImageInByte { get; set; }

        public ImageSource GetImage() =>
              ImageSource.FromStream(() => new MemoryStream(ImageInByte));
    }
}
