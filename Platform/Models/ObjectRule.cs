using System;
using System.Collections.Generic;

namespace Platform.Models 
{
    public class ObjectRule 
    { 
        public string field { get; set; }
        public string Operator {get; set;}
        public string label { get; set; }
        public string type { get; set; }
        public object value { get; set; }

      
    }
}