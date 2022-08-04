using GfdEditor.ViewModels;
using System;
using System.IO;

namespace GfdEditor.GFD.Glyphs
{
    public class Glyph0xF06
    {
       
        public int Code { get; set; }   
        public char Character { get; set; }
        public int CharWidth { get; set; }
        public int CharHeight { get; set; }
        public byte XFix { get; set; }
        public byte YFix { get; set; }
        public int GlyphWidth { get; set; }
        public int GlyphHeight { get; set; }
        public byte TextureId { get; set; }
        public byte[] GlyphPosition { get; set; }
        public byte[] GlyphWidthHeight { get; set; }
        public byte UnknownB { get; set; }
        public byte[] CharWidthHeight { get; set; }
        public byte Padding { get; set; }
        public byte[] Finalizer { get; set; }
        public int GlyphXPosition { get; set; }
        public int GlyphYPosition { get; set; }

        public Glyph0xF06(BinaryReader br)
        {
            Code = br.ReadInt32();
            TextureId = br.ReadByte();
            GlyphPosition = br.ReadBytes(3);
            CharWidthHeight = br.ReadBytes(3);
            Padding = br.ReadByte();
            GlyphWidthHeight = br.ReadBytes(3);
            UnknownB = br.ReadByte();
            XFix = br.ReadByte();
            YFix = br.ReadByte();
            Finalizer = br.ReadBytes(2);
            Character = Convert.ToChar(Code);      
            
            var posicoes = GlyphPosition[2] << 16 | GlyphPosition[1] << 8 | GlyphPosition[0];
            GlyphXPosition = posicoes & 0xFFF;
            GlyphYPosition = posicoes >> 12;
            
            var glyphwidthHeightC = GlyphWidthHeight[2] << 16 | GlyphWidthHeight[1] << 8 | GlyphWidthHeight[0];
            GlyphWidth = glyphwidthHeightC & 0xFFF;
            GlyphHeight = glyphwidthHeightC >> 12;

            var charWidthHeightC = CharWidthHeight[2] << 16 | CharWidthHeight[1] << 8 | CharWidthHeight[0];
            CharWidth = charWidthHeightC & 0xFFF;
            CharHeight = charWidthHeightC >> 12;    

        }

        public Glyph0xF06(BaseGlyph propriedadesDeGlifo)
        {
            Code = propriedadesDeGlifo.Character;
            Character = propriedadesDeGlifo.Character;
            CharWidth = (int)propriedadesDeGlifo.CharWidth;
            CharHeight = (int)propriedadesDeGlifo.CharHeight;
            XFix = (byte)propriedadesDeGlifo.XFix;
            YFix = (byte)propriedadesDeGlifo.YFix;
            GlyphWidth = (int)propriedadesDeGlifo.GlyphWidth;
            GlyphHeight = (int)propriedadesDeGlifo.GlyphMaxWidth;
            TextureId = propriedadesDeGlifo.TextureId;
            Padding = propriedadesDeGlifo.Padding;
            Finalizer = propriedadesDeGlifo.Finalizer;
            GlyphXPosition = propriedadesDeGlifo.GlyphXPosition;
            GlyphYPosition = propriedadesDeGlifo.GlyphYPosition;
            UnknownB = propriedadesDeGlifo.UnknownB;
            SetPosition(GlyphXPosition, GlyphYPosition);
            SetGlyphWidthHeight(GlyphWidth, GlyphHeight);
            SetCharWidthHeight(CharWidth, CharHeight);

        }

        public void EscreverPropriedades(BinaryWriter bw)
        {
            bw.Write(Code);
            bw.Write(TextureId);
            bw.Write(GlyphPosition);
            bw.Write(CharWidthHeight);
            bw.Write(Padding);
            bw.Write(GlyphWidthHeight);
            bw.Write(UnknownB);
            bw.Write(XFix);
            bw.Write(YFix);
            bw.Write(Finalizer);

        }

        public void SetPosition(int valX, int valY)
        {
            GlyphPosition = new byte[3];
            var positon = (valY << 12) | valX;
            var array = BitConverter.GetBytes(positon);
            Array.Copy(array, GlyphPosition, 3);

        }

        public void SetGlyphWidthHeight(int valX, int valY)
        {
            GlyphWidthHeight = new byte[3];
            var widthHeight = (valY << 12) | valX;
            var array = BitConverter.GetBytes(widthHeight);
            Array.Copy(array, GlyphWidthHeight, 3);

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
