using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataEntities
{
    public class Portfolio
    {
        public string Name { get; set; }
        public string Language { get; private set; }
        public DateTime LatesCommit { get; private set; }
        public int StarsNumber { get; private set; }
        public int PullRequestsNumber { get; private set; }
        public string Link { get; private set; }
        
        public Portfolio(string name, string language, DateTime latesCommit, int starsNumber, int pullRequestsNumber, string link)
        {
            Name = name;
            Language = language;
            LatesCommit = latesCommit;
            StarsNumber = starsNumber;
            PullRequestsNumber = pullRequestsNumber;
            Link = link;
        }
    }
}