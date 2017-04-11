using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public abstract class SpeechComponent
    {
        public abstract SpeechPart SpeechPart { get; }
        public string[] ant { get; set; }
        public string[] rel { get; set; }
        public string[] sim { get; set; }
        public string[] syn { get; set; }
    }
}