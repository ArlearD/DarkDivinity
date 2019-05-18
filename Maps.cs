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
T                           T
T P                      E  T
TTTTTTTTTTTTSSSTTTTTTTTTTTTTT";

        public const string m1 = @"
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
T                           T
T P        TT            E  T
TTTTTTTTSSSSSSSSSTTTTTTTTTTTT";


        public const string m2 = @"
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT
TTTTT   TTTT    TTT    TTTTTT
TTT      TT      T        TTT
T                         TTT
T                         TTT
T                          TT
T                          TT
T                           T
T                           T
T                           T
T        TSSSS              T
T        TTTTT              T
T        TT      T          T
T        TT                 T
T P      TS             E   T
TTTTTSSSTTTSSSSSSSSSSTTTTTTTT";

        public const string m3 = @"
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT
TTT                        TT
TT                         TT
T                          TT
T                          TT
T                          TT
T                          TT
T                          ET
T                       T  TT
T        S          T      TT
T        TT     T           T
T        TTT                T
T        TTTTT              T
T        TTTTTTTTTTTTTT     T
T P                G        T
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT";

        public const string m4 = @"
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT
TTTTT    TTT    TT     TTTTTT
TTT      TT      T          T
T     G                M    T
T           M      G        T
T                           T
T                           T
T                           T
T      G         G          T
T                           T
T                        M  T
TT              M           T
TTT                 G      TT
TTTT          G            TT
TTTTT  P                E TTT
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT";

        public const string m5 = @"
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT
TTTTTTTTTT      TTTTTTTTTTTTT
TTTTTTTT            TTTTTTTTT
TTT                     TTTTT
TTT                        TT
TT                         TT
T                           T
T                           T
T                           T
T                           T
T                           T
T                           T
T                           T
T                           T
T P                      E  T
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT";

        public const string m6 = @"
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
T        G     M        G   T
T                   G       T
T             M             T
T P       G          M   E  T
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT";

        public const string m7 = @"
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT
TTT                         T
TT                          T
T                           T
T               S           T
T          T    T           T
T               S       G   T
T               T           T
T               T           T
T               T           T
T            T  T           T
T               T           T
T               S           T
T               T           T
T P             T          ET
TTTTSSSTSSSTTTTTTTTTTTTTTTTTT";

        public const string m8 = @"
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT
TTT             TTTTTTTTTTTTT
TT                  TTTTTTTTT
T   M                   TTTTT
T       G                  TT
TE                         TT
TTTTTTTTTTTTTT  STS  T     TT
TTTTTTTTTTTT    TTTSS       T
TTTTTTTTT          TTS      T
TTTTTT               TS  TTTT
TTTT                  T     T
TT                          T
T       G                   T
T                           T
T P                   TTTTTTT
TTTTTTSSSSTTTTTTTTTTTTTTTTTTT";

        public const string m9 = @"
TTTTTTTTTTTTTTTTTTTTTTTTTTTTT
TTT             TTTTTTTTTTTTT
TT                  TTTTTTTTT
T       TTTT            TTTTT
T     TT                   TT
T     T                    TT
T     T  E                  T
T     T                     T
T     T                     T
T     TT   T                T
T      TTTTTT               T
T               T           T
T                  T   TTTTTT
T                         TTT
T P                      G TT
TTTTTTTTTTTTTTSSSTTTTTTTTTTTT";

        public static void AddMaps()
        {
            maps.Add(m0);
            maps.Add(m1);
            maps.Add(m2);
            maps.Add(m3);
            maps.Add(m4);
            maps.Add(m5);
            maps.Add(m6);
            maps.Add(m7);
            maps.Add(m8);
            maps.Add(m9);
        }

    }
}
