using System;

namespace Lundgren.Game
{
    public static class GameData
    {
        private static readonly string[] _stageIds =
        {
            "Princess Peach's Castle",
            "Rainbow Cruise",
            "Kongo Jungle",
            "Jungle Japes",
            "Termina Great Bay",
            "Hyrule Temple",
            "Yoshi's Story",
            "Yoshi's Island",
            "Fountain of Dreams",
            "Green Greens",
            "Corneria",
            "Venom",
            "Brinstar",
            "Brinstar Depths",
            "Onett",
            "Fourside",
            "Mute City",
            "Big Blue",
            "Pokemon Stadium",
            "Poke Floats",
            "Mushroom Kingdom",
            "Mushroom Kingdom 2",
            "Icicle Mountain",
            "Flat Zone",
            "Battlefield",
            "Final Destination",
            "Dream Land",
            "Yoshi's Island - Past",
            "Kongo Jungle - Past",
            "Random",
            "??"

        };

        // https://code.google.com/p/vgce/wiki/ssbmCharID
        private static readonly string[] _externalCharIds =
        {
            "Captain Falcon",
            "Donkey Kong",
            "Fox",
            "Mr. GameData & Watch",
            "Kirby",
            "Bowser",
            "Link",
            "Luigi",
            "Mario",
            "Marth",
            "Mewtwo",
            "Ness",
            "Peach",
            "Pikachu",
            "Ice Climbers",
            "Jigglypuff",
            "Samus",
            "Yoshi",
            "Zelda",
            "Sheik",
            "Falco",
            "Young Link",
            "Dr. Mario",
            "Roy",
            "Pichu",
            "Ganondorf",
            "Master Hand",
            "Wireframe Male (Boy)",
            "Wireframe Female (Girl)",
            "Giga Bowser",
            "Crazy Hand",
            "Sandbag",
            "Popo",
            "None"
        };

        private static readonly string[] _interalCharIds =
        {
            "Donkey Kong",
            "Kirby",
            "Bowser",
            "Link",
            "Sheik",
            "Ness",
            "Peach",
            "Popo",
            "Nana",
            "Pikachu",
            "Samus",
            "Yoshi",
            "JigglyPuff",
            "Mewtwo",
            "Luigi",
            "Marth",
            "Zelda",
            "Young Link",
            "Dr. Mario",
            "Falco",
            "Pichu",
            "Mr. Game & Watch",
            "Ganondorf",
            "Roy",
            "Master Hand",
            "Crazy Hand",
            "Wireframe Male (Boy)",
            "Wireframe Female (Girl)",
            "Giga Bowser",
            "Sandbag"
        };

        public static string Stage(int stageNum)
        {
            return _stageIds[stageNum];
        }

        public static string ExternalChar(byte charNum)
        {
            return _externalCharIds[charNum];
        }
    }
}