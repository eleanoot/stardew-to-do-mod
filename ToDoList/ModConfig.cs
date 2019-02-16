using StardewModdingAPI;

namespace ToDoMod
{
    class ModConfig
    {
        public SButton OpenListKey { get; set; } = SButton.F2;

        public bool UseLargerFont { get; set; }

        public bool OpenAtStartup { get; set; }
    }
}
