using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class ThesaurusItem
    {
        private static TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        public SpeechPart SpeechPart { get; protected set; }
        public WordRel WordRelation { get; protected set; }
        public String Word { get; protected set; }

        public ThesaurusItem(String word,SpeechPart speechPart, WordRel wordRel)
        {
            this.Word = textInfo.ToTitleCase(word);
            this.SpeechPart = speechPart;
            this.WordRelation = wordRel;
        }

        public string ToJson()
        {
            return string.Format("{0} ({1}) {2}.", Word, SpeechPart, WordRelation.ToString().ToLower().Substring(0,3));
        }
    }
}