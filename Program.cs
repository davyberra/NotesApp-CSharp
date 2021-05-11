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
                Console.WriteLine("Would you like to write a note or read a note?");
                string input = Console.ReadLine();
                if (input == "")
                {
                    break;
                }
                else if (input.ToLower() == "write")
                {
                    List<Note> notes = DeserializeNotes(fileName);
                    AddNote(fileName, notes);
                }
                else if (input.ToLower() == "read")
                {
                    ReadNote(fileName);
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
                List<Note> notes;
                var serializer = new JsonSerializer();
                using (var reader = new StreamReader(fileName))
                using (var jsonReader = new JsonTextReader(reader))
                {
                    notes = serializer.Deserialize<List<Note>>(jsonReader);
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
