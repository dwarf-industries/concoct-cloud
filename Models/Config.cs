using System.Collections.Generic;

namespace RokonoControl.Models
{
    public class Config
    {
        public List<ConfigBindingData> ShellScripts { get; set; }
        public string OS { get; set; }
        public string LocalRepo { get; set; }
    }
}