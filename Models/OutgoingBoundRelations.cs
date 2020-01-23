using System.Collections.Generic;
using Rokono_Control.Models;

namespace RokonoControl.Models
{
    public class OutgoingBoundRelations
    {
        public string UmlData {get; set;}
        public List<BindingWorkItemRelation> WorkItems {get; set;}
    }
}