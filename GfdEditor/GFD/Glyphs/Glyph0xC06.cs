using GfdEditor.ViewModels;
using System;
using System.IO;
using System.Linq;

namespace GfdEditor.GFD.Glyphs
{
    public class Glyph0xC06
    {
        public int Code { get; set; }
        public char Character { get; set; }
        public int CharWidth { get; set; }
        public int CharHeight { get; set; }
        public byte XFix { get; set; }
        public byte YFix { get; set; }
        public byte GlyphWidth { get; set; }
        public int GlyphHeight { get; set; }
        public byte TextureId { get; set; }
        public byte[] GlyphPosition { get; set; }
        public byte UnknownB { get; set; }
        public byte[] CharWidthHeight { get; set; }
        public byte Padding { get; set; }
        public int GlyphXPosition { get; set; }
        public int GlyphYPosition { get; set; }

        public Glyph0xC06(BinaryReader br)
        {
            Code = br.ReadInt32();
            TextureId = br.ReadByte();
            GlyphPosition = br.ReadBytes(3);
            UnknownB = br.ReadByte();
            CharWidthHeight = br.ReadBytes(3);
            GlyphWidth = br.ReadByte();
            XFix = br.ReadByte();
            YFix = br.ReadByte();
            Padding = br.ReadByte();


            Character = Convert.ToChar(Code);

            var posicoes = GlyphPosition[2] << 16 | GlyphPosition[1] << 8 | GlyphPosition[0];
            GlyphXPosition = posicoes & 0xFFF;
            GlyphYPosition = posicoes >> 12;

            var charWidthHeightC = CharWidthHeight[2] << 16 | CharWidthHeight[1] << 8 | CharWidthHeight[0];
            CharWidth = charWidthHeightC & 0xFFF;
            CharHeight = charWidthHeightC >> 12;

        }

        public Glyph0xC06(BaseGlyph propriedadesDeGlifo)
        {

            Code = propriedadesDeGlifo.Character;
            Character = propriedadesDeGlifo.Character;
            CharWidth = (int)propriedadesDeGlifo.CharWidth;
            CharHeight = (int)propriedadesDeGlifo.CharHeight;
            XFix = (byte)propriedadesDeGlifo.XFix;
            YFix = (byte)propriedadesDeGlifo.YFix;
            GlyphWidth = (byte)propriedadesDeGlifo.GlyphWidth;
            GlyphHeight = (byte)propriedadesDeGlifo.GlyphMaxWidth;
            TextureId = propriedadesDeGlifo.TextureId;
            Padding = propriedadesDeGlifo.Padding;
            GlyphXPosition = propriedadesDeGlifo.GlyphXPosition;
            GlyphYPosition = propriedadesDeGlifo.GlyphYPosition;
            UnknownB = propriedadesDeGlifo.UnknownB;
            SetPosition(GlyphXPosition, GlyphYPosition);
            SetCharWidthHeight(CharWidth, CharHeight);

        }

        public void EscreverPropriedades(BinaryWriter bw)
        {
            bw.Write(Code);
            bw.Write(TextureId);
            bw.Write(GlyphPosition);
            bw.Write(UnknownB);
            bw.Write(CharWidthHeight);
            bw.Write(GlyphWidth);
            bw.Write(XFix);
            bw.Write(YFix);
            bw.Write(Padding);
           
            

        }

        public void SetPosition(int valX, int valY)
        {
            GlyphPosition = new byte[3];
            var positon = (valY << 12) | valX;
            var array = BitConverter.GetBytes(positon);
            Array.Copy(array, GlyphPosition, 3);

        }

        public void SetCharWidthHeight(int valX, int valY)
        {
            CharWidthHeight = new byte[3];
            var charWidthHeight = (valY << 12) | valX;
            var array = BitConverter.GetBytes(charWidthHeight);
            Array.Copy(array, CharWidthHeight, 3);

        }

    }
}
