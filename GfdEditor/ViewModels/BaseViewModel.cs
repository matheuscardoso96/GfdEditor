using Prism.Commands;
using System.Collections.Generic;

namespace GfdEditor.ViewModels
{
    public class BaseViewModel
    {
        public Dictionary<string, DelegateCommand> Commands { get; set; }
        public BaseViewModel()
        {
            Commands = new();
        }
    }
}