using System;

namespace BradSeven.Data
{
    public class User
    {
        public int Id { get; set; }

        public string First { get; set; }
        public string Last { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }

        public string Name
        {
            get { return First + " " + Last; }
        }
    }
}
