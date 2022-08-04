using GfdEditor.ViewModels;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace GfdEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
	{
		
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new BaseGfdViewModel(this);
		}

		public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
		{
			var regex = new Regex("[^0-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}

	}
}
