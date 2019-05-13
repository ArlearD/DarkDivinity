using System;
using System.Linq;
namespace Digger
{
    public class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority()
        {
            return 3;
        }

        public string GetImageFileName()
        {
            return "Terrain.png";
        }
    }

    public class Player : ICreature
    {
        private bool RunState = false;
        private int StayFrame = 0;
        private int RunFrame = 1;
        private bool RightState = false;
        private bool Falling = false;
        private bool Rush = false;
        private int Count;

        public int LastxMove = 0;
        public CreatureCommand Act(int x, int y)
        {
            Count++;
            int xvalue = 0;
            int yvalue = 0;
            var pos = Game.GetPosition(this).FirstOrDefault();
            switch (Game.KeyPressed)
            {
                case System.Windows.Forms.Keys.C:
                    
                    if (RightState && pos.X + 1 < Game.MapWidth - 1 && Game.Map[pos.X + 1, pos.Y] == null && Count>12)
                    {
                        Count=0;
                        Game.Map[pos.X + 1, pos.Y] = new Slash(RightState);
                    }
                    else if (pos.X - 1 > 0 && !RightState && Game.Map[pos.X - 1, pos.Y] == null && Count>12)
                    {
                        Count = 0;
                        Game.Map[pos.X - 1, pos.Y] = new Slash(RightState);
                    }
                    break;
                case System.Windows.Forms.Keys.X:
                    
                    if (RightState && pos.X + 1 < Game.MapWidth - 1 && Game.Map[pos.X + 1, pos.Y] == null && Count > 3)
                    {
                        Count = 0;
                        Game.Map[pos.X + 1, pos.Y] = new Attack(RightState);
                    }
                    else if (pos.X - 1 > 0 && !RightState && Game.Map[pos.X - 1, pos.Y] == null && Count > 3)
                    {
                        Count = 0;
                        Game.Map[pos.X - 1, pos.Y] = new Attack(RightState);
                    }
                    break;
                case System.Windows.Forms.Keys.Left:
                    if (x - 1 >= 0)
                        xvalue = -1;
                    LastxMove = xvalue;
                    RightState = false;
                    break;
                case System.Windows.Forms.Keys.Right:
                    if (x + 1 < Game.MapWidth)
                        xvalue = 1;
                    LastxMove = xvalue;
                    RightState = true;
                    break;
                case System.Windows.Forms.Keys.Up:
                    RunState = false;
                    for (int i = 1; i <= 2; i++)
                    {
                        if (y - i >= 0 &&
                            Game.Map[x, y + 1] != null
                            && Game.Map[x, y + 1].ToString() == "Digger.Terrain"
                            && Game.Map[x, y - i] == null)
                        {
                            if (LastxMove != 0)
                            {
                                xvalue = LastxMove;
                                yvalue = -i;
                            }
                            else
                            {
                                yvalue = -i;
                            }
                        }
                        else
                        {
                            break;
                        }
                        Falling = true;
                    }
                    break;
                case System.Windows.Forms.Keys.Z:
                    for (int i = 1; i <= 2; i++)
                    {
                        if (x - i >= 0 && x + i <= Game.MapWidth &&
                            Game.Map[x + i, y] == null
                            && Game.Map[x - i, y] == null)
                        {
                            if (LastxMove != 0)
                            {
                                if (RightState)
                                {
                                    xvalue = i;
                                }
                                else
                                    xvalue = -i;
                                Rush = true;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;
                default:
                    RunState = false;
                    Falling = false;
                    LastxMove = 0;
                    break;
            }
            if (y + 1 < Game.MapHeight && Game.Map[x, y + 1] == null || Game.Map[x, y + 1].ToString() == "Digger.Sack")
            {
                yvalue = 1;
            }

            if (xvalue != 0)
            {
                RunState = true;
            }
            if (yvalue != 0)
            {
                Falling = true;
            }
            else Falling = false;

            if (Game.Map[x, y + yvalue] != null)
            {
                if (Game.Map[x, y + yvalue].ToString() == "Digger.Terrain")
                {
                    return new CreatureCommand
                    {
                        DeltaX = xvalue,
                        DeltaY = 0
                    };
                }
            }

            if (Game.Map[x + xvalue, y] != null)
            {
                if (Game.Map[x + xvalue, y].ToString() == "Digger.Terrain")
                {
                    RunState = false;
                    return new CreatureCommand
                    {
                        DeltaX = 0,
                        DeltaY = yvalue
                    };
                }
            }
            if (Game.Map[x + xvalue, y + yvalue] != null)
            {
                if (Game.Map[x + xvalue, y + yvalue].ToString() == "Digger.Terrain")
                {
                    return new CreatureCommand
                    {
                        DeltaX = xvalue,
                        DeltaY = 0
                    };
                }
            }
            return new CreatureCommand
            {
                DeltaX = xvalue,
                DeltaY = yvalue
            };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject.GetImageFileName() == "Sack.png" ||
                conflictedObject.GetImageFileName() == "Monster.png")
            {
                return true;
            }
            return false;
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public string GetImageFileName()
        {
            string Fix = "";
            if (!RightState)
            {
                Fix = "L";
            }
            if (Rush)
            {
                Rush = false;
                return "PlayerRush" + Fix + ".png";
            }

            if (RunState && !Falling)
            {
                var currentFrame = RunFrame;
                if (RunFrame >= 8) RunFrame = 1;
                RunFrame++;
                return "PlayerRun" + Fix + currentFrame + ".png";
            }
            if (Falling)
            {
                return "PlayerJump" + Fix + 3 + ".png";
            }

            if (!Falling)
            {
                if (StayFrame >= 100)
                {
                    StayFrame = 0;
                }
                StayFrame++;

                if (StayFrame < 33)
                {
                    return "Player" + Fix + "1.png";
                }
                else if (StayFrame < 66)
                {
                    return "Player" + Fix + "2.png";
                }
                else
                {
                    return "Player" + Fix + "3.png";
                }
            }
            return "";
        }
    }

    public class Sack : ICreature
    {
        int hight_of_falling = 0;
        public CreatureCommand Act(int x, int y)
        {
            if (y + 1 < Game.MapHeight)
            {
                var element = Game.Map[x, y + 1];
                if (element == null || ((element.ToString() == "Digger.Player" ||
                    element.ToString() == "Digger.Monster") && hight_of_falling > 0))
                {
                    hight_of_falling++;
                    return new CreatureCommand
                    {
                        DeltaX = 0,
                        DeltaY = 1
                    };
                }
            }
            if (hight_of_falling > 1)
            {
                return new CreatureCommand
                {
                    DeltaX = 0,
                    DeltaY = 0,
                    TransformTo = new Gold()
                };
            }
            if (hight_of_falling == 1)
            {
                hight_of_falling = 0;
            }
            return new CreatureCommand
            {
                DeltaX = 0,
                DeltaY = 0
            };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority()
        {
            return 3;
        }

        public string GetImageFileName()
        {
            return "Sack.png";
        }
    }

    public class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject.GetImageFileName() == "Digger.png" ||
                conflictedObject.GetImageFileName() == "Monster.png")
            {
                Game.Scores += 10;
                return true;
            }
            return false;
        }

        public int GetDrawingPriority()
        {
            return 3;
        }

        public string GetImageFileName()
        {
            return "Gold.png";
        }
    }

    public static class Checker
    {
        public static bool Check(int x, int y, int moveX, int moveY)
        {
            return Game.Map[x + moveX, y + moveY] != null &&
                (Game.Map[x + moveX, y + moveY].GetImageFileName() == "Sack.png"
                || Game.Map[x + moveX, y + moveY].GetImageFileName() == "Terrain.png"
                || Game.Map[x + moveX, y + moveY].GetImageFileName() == "Monster.png");
        }
    }

    public class Monster : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            int moveX = 0;
            int moveY = 0;
            for (int xOfPlayer = 0; xOfPlayer < Game.MapWidth; xOfPlayer++)
                for (int yOfPlayer = 0; yOfPlayer < Game.MapHeight; yOfPlayer++)
                    if (Game.Map[xOfPlayer, yOfPlayer] != null &&
                        Game.Map[xOfPlayer, yOfPlayer].ToString() == "Digger.Player")
                    {
                        if (xOfPlayer - x > 0)
                            if (Checker.Check(x, y, 1, 0))
                                moveX = 0;
                            else moveX = 1;
                        if (xOfPlayer - x < 0)
                            if (Checker.Check(x, y, -1, 0))
                                moveX = 0;
                            else moveX = -1;
                        if (yOfPlayer - y > 0)
                            if (Checker.Check(x, y, 0, 1))
                                moveY = 0;
                            else moveY = 1;
                        if (yOfPlayer - y < 0)
                            if (Checker.Check(x, y, 0, -1))
                                moveY = 0;
                            else moveY = -1;
                    }
            return new CreatureCommand
            {
                DeltaX = moveX,
                DeltaY = moveY
            };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject.GetImageFileName() == "Monster.png"
                || conflictedObject.GetImageFileName() == "Sack.png")
            {
                return true;
            }
            else return false;
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public string GetImageFileName()
        {
            return "Monster.png";
        }
    }
    public class Slash : ICreature
    {
        private bool RightState;
        public int Count;

        public Slash(bool RightState)
        {
            this.RightState = RightState;
        }
        public CreatureCommand Act(int x, int y)
        {
            Count++;

            int xMove;

            if (RightState)
                xMove = 1;
            else
                xMove = -1;

            return new CreatureCommand
            {
                DeltaX = xMove,
                DeltaY = 0
            };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }


        public int GetDrawingPriority()
        {
            return 2;
        }

        public string GetImageFileName()
        {
            return "Sack.png";
        }
    }

    public class Attack : ICreature
    {
        private bool RightState;
        public int Count;

        public Attack(bool RightState)
        {
            this.RightState = RightState;
        }
        public CreatureCommand Act(int x, int y)
        {
            Count++;
            return new CreatureCommand
            {
                DeltaX = 0,
                DeltaY = 0
            };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }


        public int GetDrawingPriority()
        {
            return 2;
        }

        public string GetImageFileName()
        {
            return "Sack.png";
        }
    }
}
