using System.Drawing;
using System.Windows.Media.Imaging;

namespace GfdEditor.GFD
{
    public class GlypthImage
    {
        public string Name { get; set; } = "";
        public string Dir { get; set; } = "";
        public Bitmap Image { get; set; } = null!;
        public BitmapSource ImageSource { get; set; } = null!;

    }

    
}
