using GfdEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace GfdEditor.GFD.Glyphs
{
    public class Glyph0x107
    {
   
        public int Code { get; set; }
        public char Character { get; set; }
        public float CharWidth { get; set; }
        public float CharHeight { get; set; }
        public float XFix { get; set; }
        public float YFix { get; set; }
        public float GlyphWidth { get; set; }
        public float GlyphMaxWidth { get; set; }
        public byte TextureId { get; set; }
        public byte[] GlyphPosition { get; set; }
        public byte Padding { get; set; }
        public byte[] Finalizer { get; set; }
        public int GlyphXPosition { get; set; }
        public int GlyphYPosition { get; set; }


        public Glyph0x107(BinaryReader br)
        {
            Code = br.ReadInt32();
            Character = Convert.ToChar(Code);
            CharWidth = br.ReadSingle();
            CharHeight = br.ReadSingle();
            XFix = br.ReadSingle();
            YFix = br.ReadSingle();
            GlyphWidth = br.ReadSingle();
            GlyphMaxWidth = br.ReadSingle();
            TextureId = br.ReadByte();
            GlyphPosition = br.ReadBytes(3);
            Padding = br.ReadByte();
            Finalizer = br.ReadBytes(3);
            int posicoes = GlyphPosition[2] << 16 | GlyphPosition[1] << 8 | GlyphPosition[0];
            GlyphXPosition = posicoes & 0xFFF;
            GlyphYPosition = posicoes >> 12;

        }

        public Glyph0x107(BaseGlyph propriedadesDeGlifo)
        {
            Code = propriedadesDeGlifo.Character;
            Character = propriedadesDeGlifo.Character;
            CharWidth = propriedadesDeGlifo.CharWidth;
            CharHeight = propriedadesDeGlifo.CharHeight;
            XFix = propriedadesDeGlifo.XFix;
            YFix = propriedadesDeGlifo.YFix;
            GlyphWidth = propriedadesDeGlifo.GlyphWidth;
            GlyphMaxWidth = propriedadesDeGlifo.GlyphMaxWidth;
            TextureId = propriedadesDeGlifo.TextureId;
            Padding = propriedadesDeGlifo.Padding;
            Finalizer = propriedadesDeGlifo.Finalizer;
            GlyphXPosition = propriedadesDeGlifo.GlyphXPosition;
            GlyphYPosition = propriedadesDeGlifo.GlyphYPosition;
            SetPosition(propriedadesDeGlifo.GlyphXPosition, propriedadesDeGlifo.GlyphYPosition);

        }

        public void EscreverPropriedades(BinaryWriter bw)
        {
            bw.Write(Code);
            bw.Write(CharWidth);
            bw.Write(CharHeight);
            bw.Write(XFix);
            bw.Write(YFix);
            bw.Write(GlyphWidth);
            bw.Write(GlyphMaxWidth);
            bw.Write(TextureId);
            bw.Write(GlyphPosition);
            bw.Write(Padding);
            bw.Write(Finalizer);

        }

        public void SetPosition(int valX, int valY)
        {
            GlyphPosition = new byte[3];
            var positon = (valY << 12) | valX;
            var array = BitConverter.GetBytes(positon);
            Array.Copy(array,GlyphPosition, 3);

        }
    }
}
