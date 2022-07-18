using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class User
{
    public string Name { get; set; }
    public string Surname { get; set; }

    public User(string name, string surName)
    {
        Name = name;
        Surname = surName;
    }
}
