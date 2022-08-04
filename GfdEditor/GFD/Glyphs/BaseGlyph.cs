using GfdEditor.ViewModels;
using System;

namespace GfdEditor.GFD.Glyphs
{

    public class BaseGlyph : ICloneable
    {
        private readonly BaseGfdViewModel _gfdViewModel;
        public bool _isClone = false;


        private int _code;
        public int Code
        {
            get => _code;
            set
            {
                _code = value;
                _gfdViewModel.NotifyGlypthUpdate(nameof(Code), _isClone);
            }
        }

        private char _character;
        public char Character
        {
            get => _character;
            set
            {
                _character = value;
                _gfdViewModel.NotifyGlypthUpdate(nameof(Character), _isClone);
            }
        }

        private float _charWidth;
        public float CharWidth
        {
            get => _charWidth;
            set
            {
                _charWidth = value;
                _gfdViewModel.NotifyGlypthUpdate(nameof(CharWidth), _isClone);
            }
        }

        private float _charHeight;
        public float CharHeight
        {
            get => _charHeight;
            set
            {
                _charHeight = value;
                _gfdViewModel.NotifyGlypthUpdate(nameof(CharHeight), _isClone);
            }
        }

        private float _xFix;
        public float XFix
        {
            get => _xFix;
            set
            {
                _xFix = value;
                _gfdViewModel.NotifyGlypthUpdate(nameof(XFix), _isClone);
            }
        }

        private float _yFix;
        public float YFix
        {
            get => _yFix;
            set
            {
                _yFix = value;
                _gfdViewModel.NotifyGlypthUpdate(nameof(YFix), _isClone);
            }
        }

        private float _glyphWidth;
        public float GlyphWidth
        {
            get => _glyphWidth;
            set
            {
                _glyphWidth = value;
                _gfdViewModel.NotifyGlypthUpdate(nameof(GlyphWidth), _isClone);
            }
        }

        private float _glyphMaxWidth;
        public float GlyphMaxWidth
        {
            get => _glyphMaxWidth;
            set
            {
                _glyphMaxWidth = value;
                _gfdViewModel.NotifyGlypthUpdate(nameof(GlyphMaxWidth), _isClone);
            }
        }

        private byte _textureId;
        public byte TextureId
        {
            get => _textureId;
            set
            {
                if (value < _gfdViewModel.TextureCount - 1 && value >= 0)
                {
                    _textureId = value;
                    _gfdViewModel.NotifyGlypthUpdate(nameof(TextureId), _isClone);
                }
            }
        }

        private byte[] _glyphPosition = Array.Empty<byte>();
        public byte[] GlyphPosition
        {
            get => _glyphPosition;
            set
            {
                _glyphPosition = value;
                _gfdViewModel.NotifyGlypthUpdate(nameof(GlyphPosition), _isClone);
            }
        }

        private byte _padding;
        public byte Padding
        {
            get => _padding;
            set
            {
                _padding = value;
                _gfdViewModel.NotifyGlypthUpdate(nameof(Padding), _isClone);
            }
        }

        private byte[] _finalizer = Array.Empty<byte>();
        public byte[] Finalizer
        {
            get => _finalizer;
            set
            {
                _finalizer = value;
                _gfdViewModel.NotifyGlypthUpdate(nameof(Finalizer), _isClone);
            }
        }

        private int _glyphXPosition;
        public int GlyphXPosition
        {
            get => _glyphXPosition;
            set
            {
                _glyphXPosition = value;
                _gfdViewModel.NotifyGlypthUpdate(nameof(GlyphXPosition), _isClone);


            }
        }


        private int _glyphYPosition;
        public int GlyphYPosition
        {
            get => _glyphYPosition;
            set
            {
                _glyphYPosition = value;
                _gfdViewModel.NotifyGlypthUpdate(nameof(GlyphYPosition), _isClone);

            }
        }

        public byte UnknownB { get; set; }


        public BaseGlyph(Glyph0x107 glyph, BaseGfdViewModel model)
        {
            _gfdViewModel = model;
            Code = glyph.Code;
            Character = glyph.Character;
            CharWidth = glyph.CharWidth;
            CharHeight = glyph.CharHeight;
            XFix = glyph.XFix;
            YFix = glyph.YFix;
            GlyphWidth = glyph.GlyphWidth;
            GlyphMaxWidth = glyph.GlyphMaxWidth;
            TextureId = glyph.TextureId;
            Padding = glyph.Padding;
            Finalizer = glyph.Finalizer;
            GlyphXPosition = glyph.GlyphXPosition;
            GlyphYPosition = glyph.GlyphYPosition;

        }

        public BaseGlyph(Glyph0xF06 glyph, BaseGfdViewModel model)
        {
            _gfdViewModel = model;
            Code = glyph.Code;
            Character = glyph.Character;
            CharWidth = glyph.CharWidth;
            CharHeight = glyph.CharHeight;
            XFix = glyph.XFix;
            YFix = glyph.YFix;
            GlyphWidth = glyph.GlyphWidth;
            GlyphMaxWidth = glyph.GlyphHeight;
            TextureId = glyph.TextureId;
            Padding = glyph.Padding;
            Finalizer = glyph.Finalizer;
            GlyphXPosition = glyph.GlyphXPosition;
            GlyphYPosition = glyph.GlyphYPosition;
            UnknownB = glyph.UnknownB;

        }

        public BaseGlyph(Glyph0xC06 glyph, BaseGfdViewModel model)
        {
            _gfdViewModel = model;
            Code = glyph.Code;
            Character = glyph.Character;
            CharWidth = glyph.CharWidth;
            CharHeight = glyph.CharHeight;
            XFix = glyph.XFix;
            YFix = glyph.YFix;
            GlyphWidth = glyph.GlyphWidth;
            GlyphMaxWidth = glyph.GlyphHeight;
            TextureId = glyph.TextureId;
            Padding = glyph.Padding;
            GlyphXPosition = glyph.GlyphXPosition;
            GlyphYPosition = glyph.GlyphYPosition;
            UnknownB = glyph.UnknownB;

        }

        public BaseGlyph(BaseGlyph propriedadesDeGlifo, BaseGfdViewModel model)
        {
            _isClone = true;
            _gfdViewModel = model;
            Code = propriedadesDeGlifo.Code;
            Character = propriedadesDeGlifo.Character;
            CharWidth = propriedadesDeGlifo.CharWidth;
            CharHeight = propriedadesDeGlifo.CharHeight;
            XFix = propriedadesDeGlifo.XFix;
            YFix = propriedadesDeGlifo.YFix;
            GlyphWidth = propriedadesDeGlifo.GlyphWidth;
            GlyphMaxWidth = propriedadesDeGlifo.GlyphMaxWidth;
            TextureId = propriedadesDeGlifo.TextureId;
            GlyphPosition = propriedadesDeGlifo.GlyphPosition;
            Padding = propriedadesDeGlifo.Padding;
            Finalizer = propriedadesDeGlifo.Finalizer;
            GlyphXPosition = propriedadesDeGlifo.GlyphXPosition;
            GlyphYPosition = propriedadesDeGlifo.GlyphYPosition;
            UnknownB = propriedadesDeGlifo.UnknownB;

        }

        public object Clone()
        {
            return new BaseGlyph(this, _gfdViewModel);
        }
    }
}
