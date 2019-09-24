using System;
using System.Collections.Generic;
using System.Text;

namespace Functional_Samples
{
    public class User
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public Option<string> Role { get; set; }
        public IEnumerable<string> EmailAddresses { get; set; }
    }
}
