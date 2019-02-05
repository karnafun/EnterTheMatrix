using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnterTheMatrix
{
    public class GithubResponse
    {
        public int total_count { get; set; }
        public List<GithubRepo> items { get; set; }
    }
}