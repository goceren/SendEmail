using System;
using System.Collections.Generic;
using System.Text;

namespace SendEmail.Model
{
    public class PersonList
    {
        public string ListName { get; set; }
        public List<Person> People { get; set; }
        public PersonList()
        {
            People = new List<Person>();
        }
    }
}
