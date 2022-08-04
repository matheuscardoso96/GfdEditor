using System.Collections.Generic;
using System.IO;
using System.Text;


namespace GfdEditor.GFD.Headers
{
    public class BaseGFDHeader
    {
        //https://github.com/onepiecefreak3/GFDRenderer/blob/master/GFDv1v2.txt
        public string Magic { get; set; }
        public int Version { get; set; }
        public int Unknown0 { get; set; }
        public int Unknown1 { get; set; }
        public int Unknown2 { get; set; }
        public int FontSize { get; set; }
        public int TextureCount { get; set; }
        public long GlyphCount { get; set; }
        public int ExtraEntriesCount { get; set; }
        public float GlyMaxWidth { get; set; }
        public float GlyphMaxHeight { get; set; }
        public float Baseline { get; set; }
        public float Descender { get; set; }
        public List<float> ExtraEntries { get; set; }
        public int TextureNameLength { get; set; }
        public string TextureName { get; set; } = "";
        public byte StringFinalizer { get; set; }

        public BaseGFDHeader(HeaderV0x107 header)
        {
           
            Magic = header.Magic;
            Version = header.Version;
            Unknown0 = header.Unknown0;
            Unknown1 = header.Unknown1;
            Unknown2 = header.Unknown2;
            FontSize = header.FontSize;
            TextureCount = header.TextureCount;
            GlyphCount = header.GlyphCount;
            ExtraEntriesCount = header.ExtraEntriesCount;
            GlyMaxWidth = header.GlyMaxWidth;
            GlyphMaxHeight = header.GlyphMaxHeight;
            Baseline = header.Baseline;
            Descender = header.Descender;
            ExtraEntries = header.ExtraEntries;
            TextureNameLength = header.TextureNameLength;
            TextureName = header.TextureName;
            StringFinalizer = header.StringFinalizer;

        }

        public BaseGFDHeader(Header0xC06 header)
        {

            Magic = header.Magic;
            Version = header.Version;
            Unknown0 = header.Unknown0;
            Unknown1 = header.Unknown1;
            Unknown2 = header.Unknown2;
            FontSize = header.FontSize;
            TextureCount = header.TextureCount;
            GlyphCount = header.GlyphCount;
            ExtraEntriesCount = header.ExtraEntriesCount;
            GlyMaxWidth = header.GlyMaxWidth;
            GlyphMaxHeight = header.GlyphMaxHeight;
            Baseline = header.Baseline;
            Descender = header.Descender;
            ExtraEntries = header.ExtraEntries;
            TextureNameLength = header.TextureNameLength;
            TextureName = header.TextureName;
            StringFinalizer = header.StringFinalizer;

        }
    }
}
