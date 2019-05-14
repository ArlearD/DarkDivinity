using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digger
{
    public static class Maps
    {

        public static List<string> maps = new List<string>();

        public const string m0 = @"
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT
T                           T
T                           T
T                           T
T                           T
T                           T
T                           T
T                           T
T                           T
T                           T
T                           T
T                           T
T                           T
T P          E           M  T
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT";

        public const string m1 = @"
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT
T                           T
T                           T
T                           T
T                           T
T                 T         T
T                           T
T                   T       T
T                           T
T                     T     T
T                           T
T                       T TTT
T         E  TTT   TTTT     T
T P       T      M          T
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT";

        public static void AddMaps()
        {
            maps.Add(m0);
            maps.Add(m1);
        }

    }
}
