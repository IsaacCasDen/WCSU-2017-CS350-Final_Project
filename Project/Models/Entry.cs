using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Project.Models
{
    public class Entry
    {
        public ThesaurusItem Item { get; set; }
        public Dictionary<SpeechPart,PartOfSpeech> SpeechParts { get { return _SpeechParts; } }
        Dictionary<SpeechPart, PartOfSpeech> _SpeechParts = new Dictionary<SpeechPart, PartOfSpeech>();

        public Entry(ThesaurusItem Item)
        {
            this.Item = Item;
        }
        public Entry(ThesaurusItem Item,List<ThesaurusItem> Items)
        {
            this.Item = Item;
            AddItems(Items);
        }

        public void AddItems(List<ThesaurusItem> Items)
        {
            foreach (ThesaurusItem Item in Items)
            {
                AddItem(Item);
            }
        }
        public void AddItem(ThesaurusItem Item)
        {
            if (Item != null)
            {
                if (!SpeechParts.ContainsKey(Item.SpeechPart))
                {
                    SpeechParts.Add(
                        Item.SpeechPart,
                        new PartOfSpeech(Item.SpeechPart)
                        );
                }
                SpeechParts[Item.SpeechPart].AddItem(Item);
            }
        }
        public void Merge(Entry Entry)
        {

            foreach(KeyValuePair<SpeechPart,PartOfSpeech> Item in Entry.SpeechParts)
            {
                if (!SpeechParts.ContainsKey(Item.Key))
                {
                    SpeechParts.Add(Item.Key, Item.Value);
                } else
                {
                    SpeechParts[Item.Key].Merge(Item.Value);
                }
            }
        }

        public String ToJson()
        {
            String value = "{{ \"name\": \"{0}\", \"size\": 50, \"isOuterLevel\": true, \"children\": [ {1} ] }}";
            StringBuilder values = new StringBuilder();

            foreach (KeyValuePair<SpeechPart,PartOfSpeech> Item in SpeechParts)
            {
                if (values.Length > 0) { values.Append(", "); }
                values.Append(String.Format("{0}", Item.Value.ToJson()));
            }

            value = String.Format(value,Item.Word, values.ToString());
            return value;
        }

        public class PartOfSpeech
        {
            public SpeechPart Part { get; private set; }
            public Dictionary<WordRel, WordRelation> Relations { get { return _Relations; } }
            Dictionary<WordRel, WordRelation> _Relations = new Dictionary<WordRel, WordRelation>();

            public PartOfSpeech(SpeechPart Part)
            {
                this.Part = Part;
            }
            public PartOfSpeech(SpeechPart Part, List<ThesaurusItem> Items)
            {
                this.Part = Part;
                AddItems(Items);
            }

            public void AddItems(List<ThesaurusItem> Items)
            {
                foreach (ThesaurusItem Item in Items)
                {
                    AddItem(Item);
                }
            }
            public void AddItem(ThesaurusItem Item)
            {
                if (Item != null && Item.SpeechPart == Part)
                {
                    if (!Relations.ContainsKey(Item.WordRelation))
                    {
                        Relations.Add(
                            Item.WordRelation,
                            new WordRelation(Item.WordRelation)
                            );
                    }
                    Relations[Item.WordRelation].Add(Item);
                }
            }
            internal void Merge(PartOfSpeech Part)
            {
                if (Part!=null)
                {
                    foreach (KeyValuePair<WordRel,WordRelation> Item in Part.Relations)
                    {
                        if (!Relations.ContainsKey(Item.Key))
                        {
                            Relations.Add(Item.Key, Item.Value);
                        } else
                        {
                            Relations[Item.Key].Merge(Item.Value);
                        }
                    }
                }
            }

            public String ToJson()
            {
                String value = "{{ \"name\": \"{0}\", \"size\": 50, \"children\": [ {1} ] }}";
                StringBuilder values = new StringBuilder();

                foreach (KeyValuePair<WordRel, WordRelation> Item in Relations)
                {
                    if (values.Length > 0) { values.Append(", "); }
                    values.Append(String.Format("{0}", Item.Value.ToJson()));
                }

                value = String.Format(value, Part, values.ToString());
                return value;
            }

            public class WordRelation
            {
                public WordRel Relationship { get; private set; }
                public Dictionary<String, WordItem> Items { get { return _Items; } }
                Dictionary<String, WordItem> _Items = new Dictionary<string, WordItem>();

                internal WordRelation(WordRel Relationship)
                {
                    this.Relationship = Relationship;
                }

                internal void AddAll(List<ThesaurusItem> Items)
                {
                    if (Items!=null)
                    {
                        foreach(ThesaurusItem Item in Items)
                        {
                            Add(Item);
                        }
                    }
                }
                internal void Add(ThesaurusItem Item)
                {
                    if (Item.WordRelation == this.Relationship)
                    {
                        WordItem item = new WordItem(Item.Word);
                        Add(item);
                    }
                }
                internal void Add(WordItem Item)
                {
                    if (Item!=null)
                    {
                        if (!Items.ContainsKey(Item.Name))
                        {
                            Items.Add(Item.Name, Item);
                        }
                        else
                        {
                            Items[Item.Name] = Item;
                        }
                    }
                }
                internal void Merge(WordRelation Relationship)
                {
                    if (Relationship!=null)
                    {
                        foreach (KeyValuePair<String,WordItem> Item in Relationship.Items)
                        {
                            Add(Item.Value);
                        }
                    }
                }

                public String ToJson()
                {
                    String value = "{{ \"name\": \"{0}\", \"size\": 50, \"children\": [ {1} ] }}";
                    StringBuilder values = new StringBuilder();

                    foreach (KeyValuePair<String, WordItem> Item in Items)
                    {
                        if (values.Length > 0) { values.Append(", "); }
                        values.Append(String.Format("{0}",Item.Value.ToJson()));
                    }

                    value = String.Format(value, Relationship, values.ToString());
                    return value;
                }

                public class WordItem
                {
                    public String Name { get; private set; }
                    public bool IsWord { get { return true; } }

                    internal WordItem(String Name)
                    {
                        this.Name = Name;
                    }

                    public String ToJson()
                    {
                        String value = "{{ \"name\": \"{0}\", \"size\": 1000, \"isWord\": true }}";
                        value = String.Format(value, Name);
                        return value;
                    }
                }
            }
        }
    }
}