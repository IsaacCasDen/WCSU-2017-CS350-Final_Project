using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Demo
    {

        public class Rootobject
        {
            public Noun noun { get; set; }
            public Verb verb { get; set; }
        }

        public class Noun
        {
            public string[] syn { get; set; }
        }

        public class Verb
        {
            public string[] syn { get; set; }
            public string[] rel { get; set; }
        }

    }
}