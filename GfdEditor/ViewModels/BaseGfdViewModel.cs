using GfdEditor.Extensions;
using GfdEditor.GFD;
using GfdEditor.GFD.Glyphs;
using Microsoft.Win32;
using Prism.Commands;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Text;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace GfdEditor.ViewModels
{

    public class BaseGfdViewModel :  INotifyPropertyChanged
    {
        public Dictionary<string, DelegateCommand> Commands { get; set; }
        public int TextureCount { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void NotifyGlypthUpdate(string propertyName, bool isClone)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if (GfdFile is not null && SelectedGlyph is not null && !isClone)
            {
                FontImage = GfdFile.GeneratePreview(SelectedGlyph).ToBitmapSource();
                GyphImage = GfdFile.GerarImagemComTratamento(SelectedGlyph).ToBitmapSource();
                GyphImageWithBaseLine = GfdFile.GerarPreviaComLinhaBaseIndividual(SelectedGlyph).ToBitmapSource();
                if (TestString is not null)
                    InGamePreview = GfdFile.GerarPreviaInGame(TestString).ToBitmapSource();
            }


        }
        private bool _cancelEnabled = false;
        public bool CancelEnabled
        {
            get => _cancelEnabled;
            set
            {

                _cancelEnabled = value;
                NotifyPropertyChanged(nameof(CancelEnabled));
            }
        }

        private string? _mainWindowTitle;
        public string? MainWindowTitle
        {
            get => _mainWindowTitle;
            set
            {
                _mainWindowTitle = value;
                NotifyPropertyChanged(nameof(MainWindowTitle));
            }
        }

        private bool _openEnabled = true;
        public bool OpenEnabled
        {
            get => _openEnabled;
            set
            {

                _openEnabled = value;
                NotifyPropertyChanged(nameof(OpenEnabled));
            }
        }

        private bool _closeEnabled = false;
        public bool CloseEnabled
        {
            get => _closeEnabled;
            set
            {

                _closeEnabled = value;
                NotifyPropertyChanged(nameof(CloseEnabled));
            }
        }

        private BaseGfdFile? _gfd = null!;
        public BaseGfdFile? GfdFile
        {
            get => _gfd;
            set
            {
                _gfd = value;
                NotifyPropertyChanged(nameof(GfdFile));
            }
        }

        private BaseGlyph? _glyph = null!;
        public BaseGlyph? SelectedGlyph
        {
            get => _glyph;
            set
            {
                _glyph = value;
                NotifyPropertyChanged(nameof(SelectedGlyph));
                if (SelectedGlyph is not null && GfdFile != null)
                {
                    FontImage = GfdFile.GeneratePreview(SelectedGlyph).ToBitmapSource();
                    GyphImage = GfdFile.GerarImagemComTratamento(SelectedGlyph).ToBitmapSource();
                    GyphImageWithBaseLine = GfdFile.GerarPreviaComLinhaBaseIndividual(SelectedGlyph).ToBitmapSource();
                    InsertButtons = true;
                }


            }
        }

        private BaseGlyph? _editableGlypy = null!;
        public BaseGlyph? EditableGlyph
        {
            get => _editableGlypy;
            set
            {
                _editableGlypy = value;
                NotifyPropertyChanged(nameof(EditableGlyph));
            }
        }


        private ImageSource? _fontImage = null!;
        public ImageSource? FontImage
        {
            get => _fontImage;
            set
            {
                _fontImage = value;
                NotifyPropertyChanged(nameof(FontImage));
            }
        }

        private ImageSource? _gyphImage = null!;
        public ImageSource? GyphImage
        {
            get => _gyphImage;
            set
            {
                _gyphImage = value;
                NotifyPropertyChanged(nameof(GyphImage));
            }
        }

        private ImageSource? _gyphImageWithBaseLine = null!;
        public ImageSource? GyphImageWithBaseLine
        {
            get => _gyphImageWithBaseLine;
            set
            {
                _gyphImageWithBaseLine = value;
                NotifyPropertyChanged(nameof(GyphImageWithBaseLine));
            }
        }

        private ImageSource? _inGamePreview = null!;
        public ImageSource? InGamePreview
        {
            get => _inGamePreview;
            set
            {
                _inGamePreview = value;
                NotifyPropertyChanged(nameof(InGamePreview));
            }
        }

        private List<System.Drawing.FontFamily> _fonts = null!;
        public List<System.Drawing.FontFamily> Fonts
        {
            get => _fonts;
            set
            {
                _fonts = value;
                NotifyPropertyChanged(nameof(Fonts));
            }
        }

        private System.Drawing.FontFamily _font = null!;
        public System.Drawing.FontFamily Font
        {
            get => _font;
            set
            {
                _font = value;
                NotifyPropertyChanged(nameof(Font));
            }
        }

        private string _testString = null!;
        public string TestString
        {
            get => _testString;
            set
            {

                _testString = value;
                NotifyPropertyChanged(nameof(TestString));

                if (GfdFile != null)
                    InGamePreview = GfdFile.GerarPreviaInGame(TestString).ToBitmapSource();
            }
        }

        private bool _saveEnabled = false;
        public bool SaveEnabled
        {
            get => _saveEnabled;
            set
            {

                _saveEnabled = value;
                NotifyPropertyChanged(nameof(SaveEnabled));
            }
        }


        private bool _insertFromFontEnabled = false;
        public bool InsertFromFontEnabled
        {
            get => _insertFromFontEnabled;
            set
            {

                _insertFromFontEnabled = value;
                NotifyPropertyChanged(nameof(InsertFromFontEnabled));
            }
        }

        private bool _insertEnabled = false;
        public bool InsertEnabled
        {
            get => _insertEnabled;
            set
            {

                _insertEnabled = value;
                NotifyPropertyChanged(nameof(InsertEnabled));
            }
        }

        private bool _insertButtons = false;
        public bool InsertButtons
        {
            get => _insertButtons;
            set
            {

                _insertButtons = value;
                NotifyPropertyChanged(nameof(InsertButtons));
            }
        }

        private bool _isDataGridEnabled = true;
        public bool IsDataGridEnabled
        {
            get => _isDataGridEnabled;
            set
            {

                _isDataGridEnabled = value;
                NotifyPropertyChanged(nameof(IsDataGridEnabled));
            }
        }

        private string _charToAdd = "";
        public string CharToAdd
        {
            get => _charToAdd;
            set
            {
                _charToAdd = value;
                NotifyPropertyChanged(nameof(CharToAdd));
                if (EditableGlyph != null && !string.IsNullOrEmpty(CharToAdd))
                {
                    EditableGlyph.Character = CharToAdd[0];
                    EditableGlyph.Code = CharToAdd[0];
                }


            }
        }

        private int _xPosGlyph;
        public int X
        {
            get => _xPosGlyph;
            set
            {

                _xPosGlyph = value;
                NotifyPropertyChanged(nameof(X));
                if (EditableGlyph != null)
                {
                    EditableGlyph.GlyphXPosition = X;
                    SetEditablePreview();
                }

            }
        }

        private int _yPosGlyph;
        public int Y
        {
            get => _yPosGlyph;
            set
            {

                _yPosGlyph = value;
                NotifyPropertyChanged(nameof(Y));
                if (EditableGlyph != null)
                {
                    EditableGlyph.GlyphYPosition = Y;
                    SetEditablePreview();
                }
            }
        }




        private float _widthChar;
        public float Width
        {
            get => _widthChar;
            set
            {

                _widthChar = value;
                NotifyPropertyChanged(nameof(Width));
                if (EditableGlyph != null)
                {
                    EditableGlyph.CharWidth = Width;
                    SetEditablePreview();
                }

            }
        }


        private float _heightChar;
        public float Height
        {
            get => _heightChar;
            set
            {

                _heightChar = value;
                NotifyPropertyChanged(nameof(Height));
                if (EditableGlyph != null)
                {
                    EditableGlyph.CharHeight = Height;
                    SetEditablePreview();
                }
            }
        }


        private float _xFix;
        public float XFix
        {
            get => _xFix;
            set
            {

                _xFix = value;
                NotifyPropertyChanged(nameof(XFix));
                if (EditableGlyph != null)
                {
                    EditableGlyph.XFix = XFix;
                    SetEditablePreview();
                }
            }
        }

        private float _yFix;
        public float YFix
        {
            get => _yFix;
            set
            {

                _yFix = value;
                NotifyPropertyChanged(nameof(YFix));
                if (EditableGlyph != null)
                {
                    EditableGlyph.YFix = YFix;
                    SetEditablePreview();
                }
            }
        }

        private float _glyphWidth;
        public float GlyphWidth
        {
            get => _glyphWidth;
            set
            {

                _glyphWidth = value;
                NotifyPropertyChanged(nameof(GlyphWidth));
                if (EditableGlyph != null)
                {
                    EditableGlyph.GlyphWidth = GlyphWidth;
                    SetEditablePreview();
                }
            }
        }

        private byte _textureIdNewGlyth;
        public byte TextureIdNewGlyth
        {
            get => _textureIdNewGlyth;
            set
            {

                if (value < TextureCount - 1 && value >= 0)
                {
                    _textureIdNewGlyth = value;
                    NotifyPropertyChanged(nameof(TextureIdNewGlyth));
                    if (EditableGlyph != null)
                    {
                        EditableGlyph.TextureId = TextureIdNewGlyth;
                        SetEditablePreview();
                    }
                }
            }
        }

        private readonly MainWindow _main;
        public BaseGfdViewModel(MainWindow main) : base()
        {
            _main = main;
            MainWindowTitle = "Gfd Editor";
            Commands = new();
            Commands.Add("GetGfdPath", new DelegateCommand(GetGfdPath));
            Commands.Add("SaveGfd", new DelegateCommand(SaveGfd));
            Commands.Add("InsertGlyph", new DelegateCommand(InsertGlyph));
            Commands.Add("EnableInsertGlyph", new DelegateCommand(EnableInsertGlyph));
            Commands.Add("CloseGfd", new DelegateCommand(CloseGfd));
            Commands.Add("CancelInsertGlyph", new DelegateCommand(CancelInsertGlyph));

            LoadInstalledFonts();


        }

        public void GetGfdPath()
        {
            var dlg = new OpenFileDialog
            {
                DefaultExt = ".gfd",
                Filter = "files .gfd|*.gfd"
            };
            var result = dlg.ShowDialog();

            if (result == true)
                LoadGfd(dlg.FileName);
        }

        public void LoadGfd(string gfdPath)
        {
            var gfd = new BaseGfdFile(gfdPath, this);
            if (gfd.LoadErrors.Count == 0)
            {
                GfdFile = gfd;
                SaveEnabled = true;
                MainWindowTitle = GfdFile.NomeDoGfd;
                OpenEnabled = false;
                CloseEnabled = true;
                InsertButtons = true;
            }
            else
                MessageBox.Show(string.Join("\r\n", gfd.LoadErrors));


        }

        public void SaveGfd()
        {
            GfdFile?.Save();
            MessageBox.Show($"{GfdFile?.NomeDoGfd} saved successfully.");
        }


        public void LoadInstalledFonts()
        {
            var fonts = new List<System.Drawing.FontFamily>();
            var installedFontCollection = new InstalledFontCollection();

            var fontFamilies = installedFontCollection.Families;

            foreach (var fontFamily in fontFamilies)
            {
                var mfont = new System.Drawing.FontFamily(fontFamily.Name);
                fonts.Add(mfont);
            }

            Fonts = fonts;
        }

        public void CloseGfd()
        {
            GfdFile = null;
            SelectedGlyph = null;
            FontImage = null;
            GyphImage = null;
            GyphImageWithBaseLine = null;
            InGamePreview = null;
            SaveEnabled = false;
            OpenEnabled = true;
            CloseEnabled = false;
            MainWindowTitle = "Gfd Editor";



        }

        public void EnableInsertGlyph()
        {
            CharToAdd = "";
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
            XFix = 0;
            YFix = 0;
            GlyphWidth = 0;

            if (GfdFile != null && SelectedGlyph is not null)
            {
                if (InsertEnabled)
                {
                    CancelInsertGlyph();

                }

                else
                {

                    GyphImage = null;
                    GyphImageWithBaseLine = null;
                    CloseEnabled = false;
                    IsDataGridEnabled = false;
                    InsertEnabled = true;
                    InsertButtons = false;
                    CancelEnabled = true;
                    EditableGlyph = GfdFile.Glyphs.First().Clone() as BaseGlyph;


                    if (EditableGlyph != null)
                        SetEditableGlyph(EditableGlyph);


                }

            }



        }

        public void CancelInsertGlyph()
        {
            if (InsertEnabled)
            {
                EditableGlyph = null;
                InsertEnabled = false;
                CancelEnabled = false;
                IsDataGridEnabled = true;
                InsertButtons = true;
                CloseEnabled = true;

            }
        }

        public void InsertGlyph()
        {

            if (GfdFile != null && EditableGlyph != null)
            {

                var (success, message) = GfdFile.AddGlyph(EditableGlyph);

                if (success)
                {
                    EditableGlyph = null;
                    EnableInsertGlyph();
                    CollectionViewSource.GetDefaultView(_main.DgChars.ItemsSource).Refresh();
                }
                else
                {
                    MessageBox.Show(message);
                }


            }

        }

        private void SetEditablePreview()
        {
            if (EditableGlyph is not null && GfdFile is not null)
            {
                GyphImage = GfdFile.GerarImagemComTratamento(EditableGlyph).ToBitmapSource();
                FontImage = GfdFile.GeneratePreview(EditableGlyph).ToBitmapSource();
            }

        }

        private static void SetEditableGlyph(BaseGlyph glyph)
        {
            if (glyph != null)
            {
                glyph._isClone = true;
                glyph.Character = ' ';
                glyph.GlyphXPosition = 0;
                glyph.GlyphYPosition = 0;
                glyph.CharWidth = 0;
                glyph.CharHeight = 0;
                glyph.XFix = 0;
                glyph.YFix = 0;
                glyph.GlyphWidth = 0;
            }
        }


    }
}
