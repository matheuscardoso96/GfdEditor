using GfdEditor.GFD.Glyphs;
using GfdEditor.GFD.Headers;
using GfdEditor.ViewModels;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;


namespace GfdEditor.GFD
{

    public class BaseGfdFile
    {
        public uint Version { get; set; }
        public List<string> LoadErrors { get; set; }
        public BaseGFDHeader Header { get; set; }
        public List<BaseGlyph> Glyphs { get; set; } = new();
        public List<GlypthImage> GlypthImages { get; set; } = new();
        public string GfdPath { get; set; }
        public string GfdName { get; set; }
        public Bitmap BaseLineInGamePreview { get; set; } = null!;
        public Bitmap BaseIndividualInGamePreview { get; set; } = null!;

        public BaseGfdFile(string diretorioGfd, BaseGfdViewModel gfdViewModel)
        {
            LoadErrors = new();
            GfdName = Path.GetFileName(diretorioGfd);
            GfdPath = diretorioGfd;
            BinaryReader br = new(File.OpenRead(diretorioGfd));
            LoadGfdVersion(br, gfdViewModel);
            if (LoadErrors.Count > 0)
                return;
            
            GlypthImages = new List<GlypthImage>();
            for (int i = 0; i < Header.TextureCount; i++)
            {
                var name = $"{Path.GetFileName(Header.TextureName)}_{i:00}.png";
                GlypthImages.Add(new GlypthImage() { Name = name, Dir = $"{diretorioGfd.Replace(Path.GetFileName(diretorioGfd), "")}\\{name}" });
            }

            br.Close();
            BaseLineInGamePreview = GerarImagensDePrevia(new Bitmap(400, 100));
            BaseIndividualInGamePreview = GerarImagensDePrevia(new Bitmap(150, 150));
            LoadImagens();

        }

        private void LoadGfdVersion(BinaryReader br, BaseGfdViewModel gfdViewModel) 
        {
            br.BaseStream.Position = 0x4;
            Version = br.ReadUInt32();
            br.BaseStream.Position = 0x0;

            switch (Version)
            {
                case 0x11107:
                    LoadTGACC(br, gfdViewModel);
                    break;
                case 0x10F06:
                    LoadAA5Android(br, gfdViewModel);
                    break;
                case 0x10C06:
                    LoadAA5DGS3DS(br, gfdViewModel);
                    break;
                default:
                    LoadErrors.Add("Invalid gfd file.");
                    break;
            }
        }
        private void LoadTGACC(BinaryReader br, BaseGfdViewModel gfdViewModel) 
        {
            var header = new HeaderV0x107(br);
            Header = new BaseGFDHeader(header);
            gfdViewModel.TextureCount = header.TextureCount;
            var glyphs = new List<Glyph0x107>();
            for (int i = 0; i < header.GlyphCount; i++)
                glyphs.Add(new Glyph0x107(br));
            
            glyphs = glyphs.OrderBy(c => c.Code).ToList();
            glyphs.ForEach(glyph => Glyphs.Add( new BaseGlyph(glyph, gfdViewModel)));
            gfdViewModel.TextureCount = Header.TextureCount;


        }
        private void LoadAA5Android(BinaryReader br, BaseGfdViewModel gfdViewModel) 
        {
            var header = new HeaderV0x107(br);
            Header = new BaseGFDHeader(header);
            gfdViewModel.TextureCount = header.TextureCount;
            var glyphs = new List<Glyph0xF06>();
            for (int i = 0; i < header.GlyphCount; i++)
                glyphs.Add(new Glyph0xF06(br));

            glyphs = glyphs.OrderBy(c => c.Code).ToList();
            glyphs.ForEach(glyph => Glyphs.Add(new BaseGlyph(glyph, gfdViewModel)));
            gfdViewModel.TextureCount = Header.TextureCount;
        }
        private void LoadAA5DGS3DS(BinaryReader br, BaseGfdViewModel gfdViewModel) 
        {
            var header = new Header0xC06(br);
            Header = new BaseGFDHeader(header);
            gfdViewModel.TextureCount = header.TextureCount;
            var glyphs = new List<Glyph0xC06>();
            for (int i = 0; i < header.GlyphCount; i++)
                glyphs.Add(new Glyph0xC06(br));

            glyphs = glyphs.OrderBy(c => c.Code).ToList();
            glyphs.ForEach(glyph => Glyphs.Add(new BaseGlyph(glyph, gfdViewModel)));
            gfdViewModel.TextureCount = Header.TextureCount;
        }

        public string Save() 
        {
            var novoGfd = new MemoryStream();
            var bw = new BinaryWriter(novoGfd);
            switch (Version)
            {
                case 0x10F06:
                    SerializeAA5Android(bw);
                    break;
                case 0x10C06:
                    SerializeAA5DGS3DS(bw);
                    break;
                default:
                    SerializeTGAAC(bw);
                    break;
            }

            File.WriteAllBytes(GfdPath, novoGfd.ToArray());
            return GfdPath;
        }

        public void SerializeTGAAC(BinaryWriter bw)
        {
            var header = new HeaderV0x107(Header);
            header.GlyphCount = Glyphs.Count;
            header.WritePropriedades(bw);
            //É preciso reordenar a lista pelo código, se não o gfd não funciona como deveria
            var glyphs = Glyphs.Select(g => new Glyph0x107(g)).OrderBy(x => x.Code).ToList();
            glyphs.ForEach(g => g.EscreverPropriedades(bw));
        }

        public void SerializeAA5Android(BinaryWriter bw)
        {
            var header = new HeaderV0x107(Header);
            header.GlyphCount = Glyphs.Count;
            header.WritePropriedades(bw);
            var glyphs = Glyphs.Select(g => new Glyph0xF06(g)).OrderBy(x => x.Code).ToList();
            glyphs.ForEach(g => g.EscreverPropriedades(bw));

        }
        
        public void SerializeAA5DGS3DS(BinaryWriter bw)
        {
            
            var header = new Header0xC06(Header);
            header.GlyphCount = Glyphs.Count;
            header.WritePropriedades(bw);
            var glyphs = Glyphs.Select(g => new Glyph0xC06(g)).OrderBy(x => x.Code).ToList();
            glyphs.ForEach(g => g.EscreverPropriedades(bw));

        }


        private Bitmap GerarImagensDePrevia(Bitmap bitmap)
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                Pen penLinhaBase = new(Color.Green);
                Pen penLinhaDescendente = new(Color.Purple);
                g.DrawLine(penLinhaBase, new Point(0, (int)Header.Baseline), new Point(bitmap.Width, (int)Header.Baseline));
                g.DrawLine(penLinhaDescendente, new Point(0, (int)Header.Baseline + (int)Header.Descender), new Point(bitmap.Width, (int)Header.Baseline + (int)Header.Descender));

            }
            return bitmap;
        }

        private void LoadImagens()
        {

            foreach (var tex in GlypthImages)
            {
                if (File.Exists(tex.Dir))
                {
                    var img = new Bitmap(tex.Dir);
                    tex.Image = img;

                }
                else
                    LoadErrors.Add($"Image not found: {tex.Name}");

            }

        }

        public Bitmap GeneratePreview(BaseGlyph propriedades)
        {
            Rectangle cloneRect = new(propriedades.GlyphXPosition, propriedades.GlyphYPosition, (int)propriedades.CharWidth, (int)propriedades.CharHeight);
            Rectangle cloneRect2 = new(0, 0, GlypthImages[propriedades.TextureId].Image.Width, GlypthImages[propriedades.TextureId].Image.Height);
            var bitmap = new Bitmap(GlypthImages[propriedades.TextureId].Image);
            using var g = Graphics.FromImage(bitmap);
            Pen blackPen = new(Color.Red, 2);
            g.DrawRectangle(blackPen, cloneRect);
            g.DrawRectangle(blackPen, cloneRect2);
            return bitmap;

        }

        public Bitmap GenerateAllPreview(BaseGlyph propriedades)
        {
            var bitmap = new Bitmap(GlypthImages[propriedades.TextureId].Image);
            var selection = Glyphs.Where(x => x.TextureId == propriedades.TextureId);

            using var g = Graphics.FromImage(bitmap);
            Pen blackPen = new(Color.Red, 2);

            foreach (var glypth in selection)
            {
                Rectangle cloneRect = new(glypth.GlyphXPosition, glypth.GlyphYPosition, (int)glypth.CharWidth, (int)glypth.CharHeight);
                g.DrawRectangle(blackPen, cloneRect);

            }

            Rectangle cloneRect2 = new(0, 0, GlypthImages[propriedades.TextureId].Image.Width, GlypthImages[propriedades.TextureId].Image.Height);
            g.DrawRectangle(blackPen, cloneRect2);

            return bitmap;

        }

        public Bitmap GetGlyphImage(BaseGlyph properties)
        {

            Rectangle cloneRect = new(properties.GlyphXPosition, properties.GlyphYPosition, (int)properties.CharWidth, (int)properties.CharHeight);
            PixelFormat format = GlypthImages[properties.TextureId].Image.PixelFormat;
            var bitmap = new Bitmap(100, (int)Header.GlyphMaxHeight + 1, format);
            using var g = Graphics.FromImage(bitmap);
            g.DrawImage(GlypthImages[properties.TextureId].Image, 0, 0, cloneRect, GraphicsUnit.Pixel);
            return bitmap;


        }

        public Bitmap GeneratePreviewWithGlypthProperties(BaseGlyph properties)
        {
            return GeneratePreviewWithProperties((int)properties.CharWidth, (int)properties.CharHeight, (int)properties.GlyphWidth, GetGlyphImage(properties));

        }

        private static Bitmap GeneratePreviewWithProperties(int larguraCaractere, int alturaCaractere, int larguraDoGlifo, Bitmap charImage)
        {
            Bitmap fonte = new(charImage);

            if (charImage != null)
            {

                using Graphics g = Graphics.FromImage(fonte);
                Pen penLargCarac = new(Color.Red);
                Pen penalturaCarac = new(Color.Orange);
                Pen penLargGlifo = new(Color.Aquamarine);
                g.DrawLine(penLargCarac, new Point(0, alturaCaractere), new Point(larguraCaractere, alturaCaractere));
                g.DrawLine(penalturaCarac, new Point(larguraCaractere, 0), new Point(larguraCaractere, alturaCaractere));
                g.DrawLine(penLargGlifo, new Point(larguraDoGlifo, 0), new Point(larguraDoGlifo, alturaCaractere));

            }

            return fonte;
        }

        public Bitmap GenerateInGamePreviewFromText(string text)
        {

            Bitmap previa = new(BaseLineInGamePreview);

            using (Graphics g = Graphics.FromImage(previa))
            {
                int valorX = 4;

                foreach (char letra in text)
                {
                    BaseGlyph? pg = Glyphs?.FirstOrDefault(p => p?.Character == letra);

                    if (pg != null)
                    {
                        g.DrawImage(GetGlyphImage(pg), valorX + pg.XFix, pg.YFix);
                        valorX += (int)pg.GlyphWidth;
                    }
                }

            }


            return previa;
        }

        public Bitmap GeneratePreviewBaseline(BaseGlyph propiedade)
        {
            Bitmap previa = new(BaseIndividualInGamePreview);

            using (Graphics g = Graphics.FromImage(previa))
            {
                g.DrawImage(GetGlyphImage(propiedade), propiedade.XFix, propiedade.YFix);
            }

            return previa;
        }

        public Bitmap CreateAddFontPreview(CharViewModel model, FontFamily fontFamily, BaseGlyph propriedades)
        {

            var preview = GenerateAllPreview(propriedades);

            if (model.Pt > 0)
            {
                var font = new Font(fontFamily.GetName(0), model.Pt);

                using Graphics e = Graphics.FromImage(preview);
                SolidBrush drawBrush = new(Color.White);
                e.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                Point origin = new(model.X, model.Y);
                e.DrawString(model.CharToAdd, font, drawBrush, origin);
            }



            return preview;

        }

        public (bool success, string message) AddGlyph(BaseGlyph editedGlyph)
        {
            var isValidGlyph = ValidateGlypth(editedGlyph);


            if (isValidGlyph.success)
            {
                editedGlyph._isClone = false;
                Glyphs.Add(editedGlyph);
            }

            return isValidGlyph;

        }

        private (bool success, string message) ValidateGlypth(BaseGlyph editedGlyph)
        {
            if (char.IsWhiteSpace(editedGlyph.Character))
                return (success: false, message: "Char cannot be empty.");

            if (Glyphs.Any(x => x?.Character == editedGlyph.Character))
                return (success: false, message: "Char already in table.");

            return (success: true, $"\"{editedGlyph.Character}\" added.");

        }


    }
}
