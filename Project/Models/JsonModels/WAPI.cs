namespace Project.Models.JsonModels
{
    public class WAPI
    {
        public class Rootobject
        {
            public float frequency { get; set; }
            public Pronunciation pronunciation { get; set; }
            public Result[] results { get; set; }
            public Syllables syllables { get; set; }
            public string word { get; set; }
        }

        public class Syllables
        {
            public int count { get; set; }
            public string[] list { get; set; }
        }

        public class Pronunciation
        {
            public string all { get; set; }
        }

        public class Result
        {
            public string[] also { get; set; }
            public string[] antonyms { get; set; }
            public string[] cause { get; set; }
            public string definition { get; set; }
            public string[] derivation { get; set; }
            public string[] entails { get; set; }
            public string[] examples { get; set; }
            public string[] hasMembers { get; set; }
            public string[] hasParts { get; set; }
            public string[] hasTypes { get; set; }
            public string[] inCategory { get; set; }
            public string[] instanceOf { get; set; }
            public string[] partOf { get; set; }
            public string partOfSpeech { get; set; }
            public string[] similarTo { get; set; }
            public string[] synonyms { get; set; }
            public string[] typeOf { get; set; }
            public string[] verbGroup { get; set; }
        }
    }
}