using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class JsonModel
    {
        public class Rootobject
        {
            public Adjective adjective { get; set; }
            public Adverb adverb { get; set; }
            public Noun noun { get; set; }
            public Verb verb { get; set; }
        }
        public class Adjective: SpeechComponent
        { 
            public override SpeechPart SpeechPart { get { return SpeechPart.Adjective; } }
        }
        public class Adverb: SpeechComponent
        {
            public override SpeechPart SpeechPart { get { return SpeechPart.Adverb; } }
        }
        public class Verb: SpeechComponent
        {
            public override SpeechPart SpeechPart { get { return SpeechPart.Verb; } }
        }
        public class Noun: SpeechComponent
        {
            public override SpeechPart SpeechPart { get { return SpeechPart.Noun; } }
        }
    }
}