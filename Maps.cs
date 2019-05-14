using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkDivinity
{
    public static class Maps
    {

        public static List<string> maps = new List<string>();

        public const string m0 = @"
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT
TTT             TTTTTTTTTTTTT
TT                  TTTTTTTTT
T                       TTTTT
T                          TT
T                          TT
T                           T
T                           T
T                           T
T                           T
T                           T
T                           T
T                           T
T P                      E  T
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT";

        public const string m1 = @"
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT
TTTTT    TTT    TT     TTTTTT
TTT      TT      T          T
T     M                M    T
T           M      M        T
T                           T
T                           T
T                           T
T                           T
T                           T
TT                          T
TTT                        TT
TTTT                       TT
TTTTT  P                E TTT
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT";

        public const string m2 = @"
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT
TTTTT   TTTT    TTT    TTTTTT
TTT      TT      T        TTT
T                         TTT
T                         TTT
T                           T
T                           T
T                           T
T                           T
T                           T
T                           T
T                           T
T P            M         E  T
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT";

        public const string m3 = @"
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT
TTTTT   TTTT    TTT    TTTTTT
TTT      TT      T        TTT
T                         TTT
T                         TTT
T                 T        TT
T                   M      TT
T                   T       T
T                     M     T
T                     T     T
T                         E T
T                       T TTT
T            TTT   TTTT     T
T P       T                 T
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT";

        public const string m4 = @"
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT
TTT                        TT
TT                         TT
T                          TT
T                          TT
T                          TT
T                          TT
T                          TT
T                          TT
T                           T
T                   E       T
T              T  TTT       T
T           T               T
T P                   M     T
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT";

        public const string m5 = @"
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT
T                           T
T                           T
T                           T
T                           T
T                           T
T                           T
T                           T
T                 E         T
TT   TTTTTTTTTTTTTTTT  TTT  T
T                    T      T
T   T     M           T     T
TTT T TTTTTTTTTTTTTTTTTTT TTT
T P T             M        MT
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT";

        public const string m6 = @"
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
            maps.Add(m4);
            maps.Add(m5);
            maps.Add(m6);
        }

    }
}
