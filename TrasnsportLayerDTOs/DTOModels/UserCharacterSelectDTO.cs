using System;

namespace TrasnsportLayerDTOs.DTOModels
{
    [Serializable]
    public class UserCharacterSelectDTO
    {
        public int Id { get; set; }
        public string CharacterName { get; set; }
        public int CharacterGender { get; set; }
        public int HeadType { get; set; }
        public int CharacterHeight { get; set; }
        public int CharacterRace { get; set; }
        public int SkinColor { get; set; }
        public int CharacterWeight { get; set; }
    }
}
