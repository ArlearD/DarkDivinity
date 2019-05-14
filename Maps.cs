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
TTTTT    TTT    TT     TTTTTT
TTT      TT      T          T
T                           T
T      M                    T
T                           T
T            M      M       T
T                           T
T                           T
T                           T
TT                          T
TTT                  M     TT
TTTT                       TT
TTTTT  P                E TTT
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT";

        public const string m1 = @"
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT
T                           T
T                           T
T                           T
T                           T
T                 T         T
T      M            M       T
T                   T       T
T                     M     T
T                     T     T
T                         E T
T                       T TTT
T            TTT   TTTT     T
T P       T      M          T
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT";

        public const string m2 = @"
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT
TTT                         T
TT                          T
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
T P               E         T
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT";

        public const string m3 = @"
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT
TTT                         T
TT                          T
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
T P               E         T
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT";

        public static void AddMaps()
        {
            maps.Add(m0);
            maps.Add(m1);
            maps.Add(m2);
            maps.Add(m3);
        }

    }
}
