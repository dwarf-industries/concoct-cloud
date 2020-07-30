

using System;

namespace TrasnsportLayerDTOs.DTOModels
{
    [Serializable]
    public class CurrentPossitionDTO
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float Rx { get; set; }
        public float Ry { get; set; }
        public float Rz { get; set; }
        public float W { get; set; }
    }
}
