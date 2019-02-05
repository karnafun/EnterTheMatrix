using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnterTheMatrix
{
    public class GithubRepo
    {
        public int id { get; set; }
        public string name { get; set; }
        public GithubUser owner { get; set; }
    }
}