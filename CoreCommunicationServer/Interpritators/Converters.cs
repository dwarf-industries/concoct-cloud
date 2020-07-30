using DbScaffold.Models;

namespace WWServer.Interpritators
{
    class Converters
    {
        public static float CastDoubleToFloat(double num)
        {
            var result = default(float);
            if (float.TryParse(num.ToString(), out result))
                return result;
            return result;
        }
        public static Characters GetCharacterRecepie(Characters character)
        {
            if (character.CharacterLevel < 20 && character.CharacterLevel < 40)
                character.CharacterRecepie = character.ActiveRecepieNavigation.Teenager;
            else if (character.CharacterLevel < 40 && character.CharacterLevel < 70)
                character.CharacterRecepie = character.ActiveRecepieNavigation.Adult;
            else if (character.CharacterLevel < 70)
                character.CharacterRecepie = character.ActiveRecepieNavigation.Grown;
            return character;
        }

    }
}
