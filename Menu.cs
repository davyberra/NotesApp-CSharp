using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp
{
    class Menu
    {
        public static string MainMenu => (
            "What would you like to do?\n" +
            "1) Add note\n" +
            "2) Read recent notes\n" +
            "3) Find note by ID\n" +
            "\n" +
            "4) Press 'Enter' to exit."
            );
    }
}
