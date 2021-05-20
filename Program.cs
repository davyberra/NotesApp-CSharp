using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace NotesApp
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string fileName = ("D:\\dotnet Projects\\NotesApp\\notes.json");

            while (true)
            {
                Console.WriteLine(Menu.MainMenu);
                string input = Console.ReadLine();
                if (input == "")
                {
                    break;
                }
                else if (input.ToLower() == "1")
                {
                    List<Note> notes = DeserializeNotes(fileName);
                    AddNote(fileName, notes);
                }
                else if (input.ToLower() == "2")
                {
                    ReadRecentNotes(fileName);
                }
                else if (input.ToLower() == "3")
                {
                    ReadNote(fileName);
                }
                
            }

        }
        static void ReadRecentNotes(string fileName)
        {
            List<Note> notes = DeserializeNotes(fileName);
            bool done = false;
            int count = notes.Count - 1;
            while (done != true)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (count < 0)
                    {
                        Console.WriteLine("There are no more entries.");
                        break;
                    }
                    Console.WriteLine(notes[count].Date + "\n");
                    Console.WriteLine("    " + notes[count].Content + "\n");
                    count--;                 
                }
                Console.WriteLine(
                    "Would you like to read more entries?\n" +
                    "1) Yes\n" +
                    "2) No\n"
                    );
                string input = Console.ReadLine();
                if (input == "2")
                {
                    done = true;
                }
            }
        }

        static List<Note> DeserializeNotes(string fileName)
        {
            List<Note> notes = new List<Note>();
            var serializer = new JsonSerializer();
            using (var reader = new StreamReader(fileName))
            using (var jsonReader = new JsonTextReader(reader))
            {
                try
                {
                    notes = serializer.Deserialize<List<Note>>(jsonReader);
                }
                catch (NullReferenceException)
                {
                    notes = new List<Note>();
                }
            }
            return notes;
        }

        static void ReadNote(string fileName)
        {
            Console.WriteLine("Enter the ID of the note you wish to read.");
            string input = Console.ReadLine();
            if (Int32.TryParse(input, out int value))
            {
                List<Note> notes = DeserializeNotes(fileName);                
                Note note;
                try
                {
                    note = notes[value];
                    Console.WriteLine(note.Content);
                }
                catch
                {
                    Console.WriteLine("That note doesn't exist.");
                }
                    
                
            }
            else
            {
                Console.WriteLine("Please enter an integer value.");
            }
            
        }

        static void AddNote(string fileName, List<Note> notes)
        {
            Console.WriteLine("What's on your mind?");
            string input = Console.ReadLine();
            DateTime date = DateTime.Now;
            Note note = new Note();
            string dateString = date.ToString("F");
            note.Date = dateString;
            note.Content = input;
            note.Id = notes.Count;
            notes.Add(note);
            var serializer = new JsonSerializer();
            using (var writer = new StreamWriter(fileName))
            using (var jsonWriter = new JsonTextWriter(writer))
            {

                serializer.Serialize(jsonWriter, notes);
                
            }
        }
    }
}
