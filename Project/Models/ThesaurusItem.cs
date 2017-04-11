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

        SpeechPart SpeechPart { get; set; }
        WordRel WordRelation { get; set; }
        String Word { get; set; }

        public ThesaurusItem(String word,SpeechPart speechPart, WordRel wordRel)
        {
            this.Word = textInfo.ToTitleCase(word);
            this.SpeechPart = speechPart;
            this.WordRelation = wordRel;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1}) {2}.", Word, SpeechPart, WordRelation.ToString().ToLower().Substring(0,3));
        }
    }
}