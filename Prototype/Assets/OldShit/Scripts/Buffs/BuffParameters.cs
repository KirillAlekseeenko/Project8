using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BuffParameters
{
    public BuffParameters()
    {
    }

    public BuffParameters(int damage)
    {
        Damage = damage;
    }

    public int Damage { get; }
}
