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
        private int SlashAttackFrames = 0;
        private int HeavyAttackFrames = 0;
        private int JumpFrames = 0;
        private int Count;
        private int Count1;
        private int JumpKd;

        public int LastxMove = 0;
        public CreatureCommand Act(int x, int y)
        {
            JumpKd++;
            Count++;
            Count1++;
            int xvalue = 0;
            int yvalue = 0;
            var pos = Game.GetPosition(this).FirstOrDefault();
            switch (Game.KeyPressed)
            {
                case System.Windows.Forms.Keys.C:
                    RunState = false;
                    if (Count <= 12)
                    {
                        break;
                    }
                    if (SlashAttackFrames != 0)
                    {
                        break;
                    }
                    SlashAttackFrames = 38;
                    if (RightState && pos.X + 1 < Game.MapWidth - 1 && Game.Map[pos.X + 1, pos.Y] == null && Count>12)
                    {
                        Count=0;
                        Game.Map[pos.X, pos.Y] = new Slash(RightState);
                    }
                    else if (pos.X - 1 > 0 && !RightState && Game.Map[pos.X - 1, pos.Y] == null && Count>12)
                    {
                        Count = 0;
                        Game.Map[pos.X, pos.Y] = new Slash(RightState);
                    }
                    break;
                case System.Windows.Forms.Keys.X:
                    if (HeavyAttackFrames != 0)
                    {
                        break;
                    }
                    HeavyAttackFrames = 50;
                    if (RightState && pos.X + 1 < Game.MapWidth - 1 && Game.Map[pos.X + 1, pos.Y] == null && Count1 > 3)
                    {
                        Count1 = 0;
                        Game.Map[pos.X + 1, pos.Y] = new Attack(RightState);
                    }
                    else if (pos.X - 1 > 0 && !RightState && Game.Map[pos.X - 1, pos.Y] == null && Count1 > 3)
                    {
                        Count1 = 0;
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
                    if (JumpKd > 1)
                    {
                        JumpKd = 0;
                        for (int i = 1; i <= 2; i++)
                        {
                            if (y - i >= 0 &&
                                Game.Map[x, y + 1] != null
                                && Game.Map[x, y + 1].ToString() == "Digger.Terrain"
                                && Game.Map[x, y - i] == null)
                            {
                                JumpFrames = 13;
                                if (LastxMove != 0)
                                {
                                    if (Game.Map[x + LastxMove, y - i] == null)
                                    {
                                        xvalue = LastxMove;
                                    }
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
            return true;
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

            if (JumpFrames > 0)
            {
                JumpFrames--;
                if (JumpFrames <= 6)
                {
                    return "PlayerJump" + Fix + "2.png";
                }
                if (JumpFrames <= 12)
                {
                    return "PlayerJump" + Fix + "1.png";
                }
            }

            if (SlashAttackFrames > 0)
            {
                SlashAttackFrames--;
                if (SlashAttackFrames <= 6)
                {
                    return "PlayerSlash" + Fix + "6.png";
                }
                if (SlashAttackFrames <= 12)
                {
                    return "PlayerSlash" + Fix + "5.png";
                }
                if (SlashAttackFrames <= 18)
                {
                    return "PlayerSlash" + Fix + "4.png";
                }
                if (SlashAttackFrames <= 24)
                {
                    return "PlayerSlash" + Fix + "3.png";
                }
                if (SlashAttackFrames <= 30)
                {
                    return "PlayerSlash" + Fix + "2.png";
                }
                if (SlashAttackFrames <= 36)
                {
                    return "PlayerSlash" + Fix + "1.png";
                }
            }

            if (HeavyAttackFrames > 0)
            {
                HeavyAttackFrames--;
                if (HeavyAttackFrames <= 12)
                {
                    return "PlayerHeavy" + Fix + "4.png";
                }
                if (HeavyAttackFrames <= 24)
                {
                    return "PlayerHeavy" + Fix + "3.png";
                }
                if (HeavyAttackFrames <= 36)
                {
                    return "PlayerHeavy" + Fix + "2.png";
                }
                if (HeavyAttackFrames <= 48)
                {
                    return "PlayerHeavy" + Fix + "1.png";
                }
            }

            if (RunState && !Falling)
            {
                if (RunFrame >= 56) RunFrame = 0;
                RunFrame++;
                return "PlayerRun" + Fix + ((RunFrame / 8) + 1) + ".png";
            }
            if (Falling)
            {
                return "PlayerJump" + Fix + "3.png";
            }

            if (!Falling)
            {
                if (StayFrame >= 100)
                {
                    StayFrame = 0;
                }
                StayFrame++;

                if (StayFrame < 25)
                {
                    return "PlayerStay" + Fix + "1.png";
                }
                else if (StayFrame < 50)
                {
                    return "PlayerStay" + Fix + "2.png";
                }
                else if (StayFrame < 75)
                {
                    return "PlayerStay" + Fix + "3.png";
                }
                else
                {
                    return "PlayerStay" + Fix + "4.png";
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
            if (Game.Map[x + moveX, y] !=null && (Game.Map[x + moveX, y].GetImageFileName() == "Slash.png" 
                || Game.Map[x + moveX, y].GetImageFileName() == "SlashL.png" ||
                Game.Map[x + moveX, y].GetImageFileName() == "Attack.png" ||
                    Game.Map[x + moveX, y].GetImageFileName() == "AttackL.png"))
            {
                moveX = 0;
            }
            return new CreatureCommand
            {
                DeltaX = moveX,
                DeltaY = moveY
            };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject.ToString() == "Digger.Player")
            {
                return false;
            }
            else return true;
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
            if (RightState)
            {
                return "Slash.png";
            }
            else
            return "SlashL.png";
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
            if (RightState)
            {
                return "Attack.png";
            }
            else
            return "AttackL.png";
        }
    }

    public class Exit : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand()
            {
                DeltaX = 0,
                DeltaY = 0
            };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject.ToString() == "Digger.Player")
            {
                Program.NextMap();
                return false;
            }
            else
            {
                return false;
            }
        }

        public int GetDrawingPriority()
        {
            return 5;
        }

        public string GetImageFileName()
        {
            return "Exit1.png";
        }
    }
}
