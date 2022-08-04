using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GfdEditor.GFD.Headers
{
    public class Header0xC06
    {
        public string Magic { get; set; }
        public int Version { get; set; }
        public int Unknown0 { get; set; }
        public int Unknown1 { get; set; }
        public int Unknown2 { get; set; }
        public int FontSize { get; set; }
        public int TextureCount { get; set; }
        public int GlyphCount { get; set; }
        public int ExtraEntriesCount { get; set; }
        public float GlyMaxWidth { get; set; }
        public float GlyphMaxHeight { get; set; }
        public float Baseline { get; set; }
        public float Descender { get; set; }
        public List<float> ExtraEntries { get; set; }
        public int TextureNameLength { get; set; }
        public string TextureName { get; set; } = "";
        public byte StringFinalizer { get; set; }


        public Header0xC06(BinaryReader br)
        {
            ExtraEntries = new List<float>();
            Magic = Encoding.ASCII.GetString(br.ReadBytes(4));
            if (Magic == "GFD\0")
            {

                Version = br.ReadInt32();
                Unknown0 = br.ReadInt32();
                Unknown1 = br.ReadInt32();
                Unknown2 = br.ReadInt32();
                FontSize = br.ReadInt32();
                TextureCount = br.ReadInt32();
                GlyphCount = br.ReadInt32();
                ExtraEntriesCount = br.ReadInt32();
                GlyphMaxHeight = 32;
                Baseline = br.ReadSingle();
                Descender = br.ReadSingle();
                for (int i = 0; i < ExtraEntriesCount; i++)
                {
                    ExtraEntries.Add(br.ReadSingle());
                }
                TextureNameLength = br.ReadInt32();
                TextureName = Encoding.ASCII.GetString(br.ReadBytes(TextureNameLength));
                StringFinalizer = br.ReadByte();
            }

        }

        public Header0xC06(BaseGFDHeader baseGFDHeader)
        {
            Magic = baseGFDHeader.Magic;
            Version = baseGFDHeader.Version;
            Unknown0 = baseGFDHeader.Unknown0;
            Unknown1 = baseGFDHeader.Unknown1;
            Unknown2 = baseGFDHeader.Unknown2;
            FontSize = baseGFDHeader.FontSize;
            GlyphCount = (int)baseGFDHeader.GlyphCount;
            ExtraEntriesCount = baseGFDHeader.ExtraEntriesCount;
            GlyMaxWidth = baseGFDHeader.GlyMaxWidth;
            GlyphMaxHeight = baseGFDHeader.GlyphMaxHeight;
            Baseline = baseGFDHeader.Baseline;
            Descender = baseGFDHeader.Descender;
            ExtraEntries = baseGFDHeader.ExtraEntries;
            TextureName = baseGFDHeader.TextureName;
            StringFinalizer = baseGFDHeader.StringFinalizer;
            TextureCount = baseGFDHeader.TextureCount;
            TextureNameLength = baseGFDHeader.TextureNameLength;
        }

        public void WritePropriedades(BinaryWriter bw)
        {
            bw.Write(Encoding.ASCII.GetBytes(Magic));
            bw.Write(Version);
            bw.Write(Unknown0);
            bw.Write(Unknown1);
            bw.Write(Unknown2);
            bw.Write(FontSize);
            bw.Write(TextureCount);
            bw.Write(GlyphCount);
            bw.Write(ExtraEntriesCount);
            bw.Write(Baseline);
            bw.Write(Descender);
            for (int i = 0; i < ExtraEntries.Count; i++)
            {
                bw.Write(ExtraEntries[i]);
            }
            bw.Write(TextureNameLength);
            bw.Write(Encoding.ASCII.GetBytes(TextureName));
            bw.Write(StringFinalizer);
        }


    }
}
