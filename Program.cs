using System;
using System.IO;
using Newtonsoft.Json;

namespace NotesApp
{
    class Program
    {
        
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("What's on your mind?");
                string input = Console.ReadLine();
                if (input == "")
                {
                    break;
                }
                string fileName = ("D:\\dotnet Projects\\NotesApp\\notes.json");
                AddNote(input, fileName);
            }

        }

        static void AddNote(string input, string fileName)
        {
            DateTime date = DateTime.Now;
            Note note = new Note();
            string dateString = date.ToString("F");
            note.Date = dateString;
            note.Content = input;
            var serializer = new JsonSerializer();
            using (var writer = new StreamWriter(fileName, true))
            using (var jsonWriter = new JsonTextWriter(writer))
            {

                serializer.Serialize(jsonWriter, note);
                
            }
        }
    }
}
