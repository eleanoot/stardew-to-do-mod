using Microsoft.Xna.Framework.Input;

namespace ToDoMod
{
    class ModConfig
    {
        public string OpenListKey { get; set; } = Keys.F2.ToString();

        public bool UseLargerFont { get; set; }

        public bool OpenAtStartup { get; set; }
    }
}
