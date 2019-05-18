using System;
using System.Linq;

namespace DarkDivinity
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
            return "Terrain4.png";
        }
    }

    public class TerrainBroken : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 3;
        }

        public string GetImageFileName()
        {
            return "Terrain3.png";
        }
    }

    public class Player : ICreature
    {
        private bool RunState = false;
        private bool RightState = false;
        private bool Falling = false;
        private bool Sitting = false;
        private int SlashAttackFrames = 0;
        private int HeavyAttackFrames = 0;
        private int HeavenAttackFrames = 0;
        private int StayFrame = 0;
        private int RunFrame = 0;
        private int SittingFrame = 0;
        private int JumpFrames = 0;
        private int SlachCooldown;
        private int HeavyCooldown;
        private int HeavenCooldown;

        public int LastxMove = 0;
        public CreatureCommand Act(int x, int y)
        {
            SlachCooldown++;
            HeavyCooldown++;
            HeavenCooldown++;
            int xvalue = 0;
            int yvalue = 0;
            var pos = Game.GetPosition("DarkDivinity.Player").FirstOrDefault();
            switch (Game.KeyPressed)
            {
                case System.Windows.Forms.Keys.V:
                    Sitting = false;
                    RunState = false;
                    if (HeavenCooldown <= 8)
                    {
                        break;
                    }
                    if (HeavyAttackFrames != 0)
                    {
                        break;
                    }
                    HeavenAttackFrames = 82;
                    if (pos.X + 1 < Game.MapWidth - 1 && Game.Map[pos.X, pos.Y - 1] == null && HeavenCooldown > 8)
                    {
                        HeavenCooldown = 0;
                        Game.Map[pos.X, pos.Y] = new Heaven();
                    }


                    break;
                case System.Windows.Forms.Keys.C:
                    Sitting = false;
                    RunState = false;
                    if (SlachCooldown <= 8)
                    {
                        break;
                    }
                    if (SlashAttackFrames != 0)
                    {
                        break;
                    }
                    SlashAttackFrames = 61;
                    if (RightState && pos.X + 1 < Game.MapWidth - 1 && Game.Map[pos.X + 1, pos.Y] == null && SlachCooldown > 8)
                    {
                        SlachCooldown = 0;
                        Game.Map[pos.X, pos.Y] = new Slash(RightState);
                    }
                    else if (pos.X - 1 > 0 && !RightState && Game.Map[pos.X - 1, pos.Y] == null && SlachCooldown > 8)
                    {
                        SlachCooldown = 0;
                        Game.Map[pos.X, pos.Y] = new Slash(RightState);
                    }
                    break;

                case System.Windows.Forms.Keys.X:
                    Sitting = false;
                    if (HeavyAttackFrames != 0)
                    {
                        break;
                    }
                    HeavyAttackFrames = 50;
                    if (RightState && pos.X + 1 < Game.MapWidth - 1 && Game.Map[pos.X + 1, pos.Y] == null && HeavyCooldown > 3)
                    {
                        HeavyCooldown = 0;
                        Game.Map[pos.X + 1, pos.Y] = new Attack(RightState);
                    }
                    else if (pos.X - 1 > 0 && !RightState && Game.Map[pos.X - 1, pos.Y] == null && HeavyCooldown > 3)
                    {
                        HeavyCooldown = 0;
                        Game.Map[pos.X - 1, pos.Y] = new Attack(RightState);
                    }
                    break;

                case System.Windows.Forms.Keys.Left:
                    if (!Sitting)
                    {
                        if (x - 1 >= 0)
                            xvalue = -1;
                        LastxMove = xvalue;
                        RightState = false;
                    }
                    else
                    {
                        RightState = false;
                        Sitting = false;
                    }
                    break;

                case System.Windows.Forms.Keys.Right:
                    if (!Sitting)
                    {
                        if (x + 1 < Game.MapWidth)
                            xvalue = 1;
                        LastxMove = xvalue;
                        RightState = true;
                        Sitting = false;
                    }
                    else
                    {
                        RightState = true;
                        Sitting = false;
                    }
                    break;

                case System.Windows.Forms.Keys.Z:
                    RunState = false;
                    break;

                case System.Windows.Forms.Keys.Down:
                    RunState = false;
                    if (!Falling) Sitting = true;
                    break;

                case System.Windows.Forms.Keys.Up:
                    RunState = false;
                    if (Sitting)
                    {
                        Sitting = false;
                        RunState = false;
                        for (int t = 1; t <= 6; t++)
                        {
                            if (Game.Map[x, y - t] == null)
                            {
                                yvalue = -t;
                            }
                            else
                            {
                                JumpFrames = 13;
                                Falling = true;
                                return new CreatureCommand
                                {
                                    DeltaX = 0,
                                    DeltaY = yvalue
                                };
                            }
                        }
                        JumpFrames = 13;
                        Falling = true;
                        return new CreatureCommand
                        {
                            DeltaX = 0,
                            DeltaY = yvalue
                        };
                    }
                    if (!Falling)
                    {
                        if (Game.Map[x, y - 1] != null)
                        {
                            return new CreatureCommand
                            {
                                DeltaX = 0,
                                DeltaY = 0
                            };
                        }
                        for (int i = 1; i <= 3; i++)
                        {
                            if (!RightState)
                            {
                                if (Game.Map[x - i, y - i] != null
                                    && Game.Map[x - 1, y] == null
                                    && Game.Map[x, y - 1] == null)
                                {
                                    for (int t = 1; t <= 3; t++)
                                    {
                                        if (Game.Map[x, y - t] == null)
                                        {
                                            yvalue = -t;
                                        }
                                        else
                                        {
                                            JumpFrames = 13;
                                            Falling = true;
                                            return new CreatureCommand
                                            {
                                                DeltaX = 0,
                                                DeltaY = yvalue
                                            };
                                        }
                                    }
                                    JumpFrames = 13;
                                    Falling = true;
                                    return new CreatureCommand
                                    {
                                        DeltaX = 0,
                                        DeltaY = yvalue
                                    };
                                }
                                if (Game.Map[x - 1, y] != null)
                                {
                                    for (int t = 1; t <= 3; t++)
                                    {
                                        if (Game.Map[x, y - t] == null)
                                        {
                                            yvalue = -t;
                                        }
                                        else
                                        {
                                            JumpFrames = 13;
                                            Falling = true;
                                            return new CreatureCommand
                                            {
                                                DeltaX = 0,
                                                DeltaY = yvalue
                                            };
                                        }
                                    }
                                    JumpFrames = 13;
                                    Falling = true;
                                    return new CreatureCommand
                                    {
                                        DeltaX = 0,
                                        DeltaY = yvalue
                                    };
                                }
                                if (Game.Map[x - i, y - i] == null)
                                {
                                    if (Game.Map[x - i - 1, y - i] != null)
                                    {
                                        xvalue = -i;
                                        for (int t = 0; t + i <= 3; t++)
                                        {
                                            if (Game.Map[x - i, y - i - t] == null)
                                            {
                                                yvalue = -t - i;
                                            }
                                        }
                                        JumpFrames = 13;
                                        Falling = true;
                                        return new CreatureCommand
                                        {
                                            DeltaX = xvalue,
                                            DeltaY = yvalue
                                        };
                                    }
                                    xvalue = -i;
                                    yvalue = -i;
                                }
                                else
                                {
                                    return new CreatureCommand
                                    {
                                        DeltaX = 0,
                                        DeltaY = 0
                                    };
                                }
                            }
                            else
                            {
                                if (Game.Map[x + i, y - i] != null
                                    && Game.Map[x + 1, y] == null
                                    && Game.Map[x, y - 1] == null)
                                {
                                    for (int t = 1; t <= 3; t++)
                                    {
                                        if (Game.Map[x, y - t] == null)
                                        {
                                            yvalue = -t;
                                        }
                                        else
                                        {
                                            JumpFrames = 13;
                                            Falling = true;
                                            return new CreatureCommand
                                            {
                                                DeltaX = 0,
                                                DeltaY = yvalue
                                            };
                                        }
                                    }
                                    JumpFrames = 13;
                                    Falling = true;
                                    return new CreatureCommand
                                    {
                                        DeltaX = 0,
                                        DeltaY = yvalue
                                    };
                                }
                                if (Game.Map[x + 1, y] != null)
                                {
                                    for (int t = 1; t <= 3; t++)
                                    {
                                        if (Game.Map[x, y - t] == null)
                                        {
                                            yvalue = -t;
                                        }
                                        else
                                        {
                                            JumpFrames = 13;
                                            Falling = true;
                                            return new CreatureCommand
                                            {
                                                DeltaX = 0,
                                                DeltaY = yvalue
                                            };
                                        }
                                    }
                                    JumpFrames = 13;
                                    Falling = true;
                                    return new CreatureCommand
                                    {
                                        DeltaX = 0,
                                        DeltaY = yvalue
                                    };
                                }
                                if (Game.Map[x + i, y - i] == null)
                                {
                                    if (Game.Map[x + i + 1, y - i] != null)
                                    {
                                        xvalue = +i;
                                        for (int t = 0; t + i <= 3; t++)
                                        {
                                            if (Game.Map[x + i, y - i - t] == null)
                                            {
                                                yvalue = -t - i;
                                            }
                                        }
                                        JumpFrames = 13;
                                        Falling = true;
                                        return new CreatureCommand
                                        {
                                            DeltaX = xvalue,
                                            DeltaY = yvalue
                                        };
                                    }
                                    xvalue = +i;
                                    yvalue = -i;
                                }
                                else
                                {
                                    return new CreatureCommand
                                    {
                                        DeltaX = 0,
                                        DeltaY = 0
                                    };
                                }
                            }
                        }
                        JumpFrames = 13;
                        Falling = true;
                    }
                    break;
                default:
                    RunState = false;
                    Falling = false;
                    LastxMove = 0;
                    break;
            }
            if (!(y + 1 < Game.MapHeight && Game.Map[x, y + 1] != null && Game.Map[x, y + 1].ToString() == "DarkDivinity.Terrain"))
                yvalue = 1;

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
                if (Game.Map[x, y + yvalue].ToString() == "DarkDivinity.Terrain")
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
                if (Game.Map[x + xvalue, y].ToString() == "DarkDivinity.Terrain")
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
                if (Game.Map[x + xvalue, y + yvalue].ToString() == "DarkDivinity.Terrain")
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

            if (Sitting)
            {
                if (SittingFrame >= 90) SittingFrame = 0;
                SittingFrame++;
                return "PlayerSitting" + Fix + ((SittingFrame / 30) + 1) + ".png";
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

            if (HeavenAttackFrames > 0)
            {
                HeavenAttackFrames--;
                if (HeavenAttackFrames <= 20)
                {
                    return "PlayerHeaven" + Fix + "4.png";
                }
                if (HeavenAttackFrames <= 50)
                {
                    return "PlayerHeaven" + Fix + "3.png";
                }
                if (HeavenAttackFrames <= 70)
                {
                    return "PlayerHeaven" + Fix + "2.png";
                }
                if (HeavenAttackFrames <= 80)
                {
                    return "PlayerHeaven" + Fix + "1.png";
                }
            }

            if (SlashAttackFrames > 0)
            {
                SlashAttackFrames--;
                if (SlashAttackFrames <= 10)
                {
                    return "PlayerSlash" + Fix + "6.png";
                }
                if (SlashAttackFrames <= 20)
                {
                    return "PlayerSlash" + Fix + "5.png";
                }
                if (SlashAttackFrames <= 30)
                {
                    return "PlayerSlash" + Fix + "4.png";
                }
                if (SlashAttackFrames <= 40)
                {
                    return "PlayerSlash" + Fix + "3.png";
                }
                if (SlashAttackFrames <= 50)
                {
                    return "PlayerSlash" + Fix + "2.png";
                }
                if (SlashAttackFrames <= 60)
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

    public class Spike : ICreature
    {
        int hight_of_falling = 0;
        public CreatureCommand Act(int x, int y)
        {
            if (y + 1 < Game.MapHeight)
            {
                var element = Game.Map[x, y + 1];
                if (element == null || ((element.ToString() == "DarkDivinity.Player" ||
                    element.ToString() == "DarkDivinity.Monster") && hight_of_falling > 0))
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
            return "Spike.png";
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
            if (conflictedObject.GetImageFileName().Remove(6) == "Player")
            {
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
    public class Monster : ICreature
    {
        private int MoveFrames = 0;
        private bool RightState = false;
        private bool MoveSlower = true;
        public CreatureCommand Act(int x, int y)
        {
            int moveX = 0;
            int moveY = 0;

            var pos = Game.GetPosition("DarkDivinity.Player").FirstOrDefault();
            if (Math.Abs(x - pos.X) >= 6 || Math.Abs(y - pos.Y) >= 6)
                return new CreatureCommand
                {
                    DeltaX = 0,
                    DeltaY = 0
                };
            else
                for (int xOfPlayer = 0; xOfPlayer < Game.MapWidth; xOfPlayer++)
                    for (int yOfPlayer = 0; yOfPlayer < Game.MapHeight; yOfPlayer++)
                        if (Game.Map[xOfPlayer, yOfPlayer] != null &&
                            Game.Map[xOfPlayer, yOfPlayer].ToString() == "DarkDivinity.Player")
                        {
                            if (xOfPlayer - x > 0)
                                if (Game.Check(x, y, 1, 0))
                                    moveX = 0;
                                else moveX = 1;
                            if (xOfPlayer - x < 0)
                                if (Game.Check(x, y, -1, 0))
                                    moveX = 0;
                                else moveX = -1;
                            if (yOfPlayer - y > 0)
                                if (Game.Check(x, y, 0, 1))
                                    moveY = 0;
                                else moveY = 1;
                            if (yOfPlayer - y < 0)
                                if (Game.Check(x, y, 0, -1))
                                    moveY = 0;
                                else moveY = -1;
                        }
            if (Game.Map[x + moveX, y + moveY] != null &&
                (Game.Map[x + moveX, y + moveY].GetImageFileName().Remove(5) == "Slash"
                || Game.Map[x + moveX, y + moveY].GetImageFileName().Remove(5) == "Attac"
                || Game.Map[x + moveX, y + moveY].GetImageFileName().Remove(6) == "Heaven"))
            {
                moveX = 0;
                moveY = 0;
            }
            if (Game.Map[x + moveX, y + moveY] != null && (Game.Map[x + moveX, y + moveY].GetImageFileName().Remove(7) == "Terrain"
                || Game.Map[x + moveX, y + moveY].GetImageFileName().Remove(5) == "Spike"))
            {
                moveX = 0;
                moveY = 0;
            }
            if (moveX > 0)
            {
                RightState = true;
            }
            else RightState = false;
            if (MoveSlower)
            {
                MoveSlower = !MoveSlower;
                return new CreatureCommand
                {
                    DeltaX = moveX,
                    DeltaY = moveY
                };
            }
            else
            {
                MoveSlower = !MoveSlower;
                return new CreatureCommand
                {
                    DeltaX = 0,
                    DeltaY = 0
                };
            }
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject.ToString() == "DarkDivinity.Player")
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
            var Fix = "";
            if (!RightState)
            {
                Fix = "L";
            }
            if (MoveFrames >= 74)
            {
                MoveFrames = 0;
            }

            MoveFrames++;
            if (MoveFrames <= 0)
            {
                return "Monster" + Fix + "1.png";
            }
            if (MoveFrames <= 25)
            {
                return "Monster" + Fix + "2.png";
            }
            if (MoveFrames <= 50)
            {
                return "Monster" + Fix + "3.png";
            }
            if (MoveFrames <= 75)
            {
                return "Monster" + Fix + "4.png";
            }
            return "";
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

    public class Heaven : ICreature
    {
        public int Count;
        public CreatureCommand Act(int x, int y)
        {
            Count++;

            return new CreatureCommand
            {
                DeltaX = 0,
                DeltaY = -1
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
            return "Heaven.png";
        }
    }

    public class MonsterGhul : ICreature
    {
        private int MoveFrames = 0;
        private bool RightState = false;
        private bool MoveSlower = true;
        public CreatureCommand Act(int x, int y)
        {
            int moveX = 0;
            int moveY = 0;

            var pos = Game.GetPosition("DarkDivinity.Player").FirstOrDefault();
            if (Math.Abs(x - pos.X) >= 6 || Math.Abs(y - pos.Y) >= 6)
                return new CreatureCommand
                {
                    DeltaX = 0,
                    DeltaY = 0
                };
            else
                for (int xOfPlayer = 0; xOfPlayer < Game.MapWidth; xOfPlayer++)
                    for (int yOfPlayer = 0; yOfPlayer < Game.MapHeight; yOfPlayer++)
                        if (Game.Map[xOfPlayer, yOfPlayer] != null &&
                            Game.Map[xOfPlayer, yOfPlayer].ToString() == "DarkDivinity.Player")
                        {
                            if (xOfPlayer - x > 0)
                                if (Game.Check(x, y, 1, 0))
                                    moveX = 0;
                                else moveX = 1;
                            if (xOfPlayer - x < 0)
                                if (Game.Check(x, y, -1, 0))
                                    moveX = 0;
                                else moveX = -1;
                            if (yOfPlayer - y > 0)
                                if (Game.Check(x, y, 0, 1))
                                    moveY = 0;
                                else moveY = 1;
                            if (yOfPlayer - y < 0)
                                if (Game.Check(x, y, 0, -1))
                                    moveY = 0;
                                else moveY = -1;
                        }
            if (Game.Map[x + moveX, y + moveY] != null &&
                (Game.Map[x + moveX, y + moveY].GetImageFileName().Remove(5) == "Slash"
                || Game.Map[x + moveX, y + moveY].GetImageFileName().Remove(5) == "Attac"
                || Game.Map[x + moveX, y + moveY].GetImageFileName().Remove(6) == "Heaven"))
            {
                moveX = 0;
                moveY = 0;
            }
            if (Game.Map[x + moveX, y + moveY] != null && (Game.Map[x + moveX, y + moveY].GetImageFileName().Remove(7) == "Terrain"
                || Game.Map[x + moveX, y + moveY].GetImageFileName().Remove(5) == "Spike"))
            {
                moveX = 0;
                moveY = 0;
            }
            if (moveX > 0)
            {
                RightState = true;
            }
            else RightState = false;
            if (MoveSlower)
            {
                MoveSlower = !MoveSlower;
                return new CreatureCommand
                {
                    DeltaX = moveX,
                    DeltaY = moveY
                };
            }
            else
            {
                MoveSlower = !MoveSlower;
                return new CreatureCommand
                {
                    DeltaX = 0,
                    DeltaY = 0
                };
            }
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject.ToString() == "DarkDivinity.Player")
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
            var Fix = "";
            if (!RightState)
            {
                Fix = "L";
            }
            if (MoveFrames >= 100)
            {
                MoveFrames = 0;
            }

            MoveFrames++;
            if (MoveFrames <= 0)
            {
                return "MonsterGhul" + Fix + "1.png";
            }
            if (MoveFrames <= 20)
            {
                return "MonsterGhul" + Fix + "2.png";
            }
            if (MoveFrames <= 40)
            {
                return "MonsterGhul" + Fix + "3.png";
            }
            if (MoveFrames <= 60)
            {
                return "MonsterGhul" + Fix + "4.png";
            }
            if (MoveFrames <= 80)
            {
                return "MonsterGhul" + Fix + "5.png";
            }
            if (MoveFrames <= 100)
            {
                return "MonsterGhul" + Fix + "6.png";
            }
            return "";
        }
    }

    public class Exit : ICreature
    {
        private int Frame = 0;
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
            if (conflictedObject.ToString() == "DarkDivinity.Player")
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
            var scal = 30;
            if (Frame >= 5 * scal)
            {
                Frame = 0;
            }
            Frame++;
            if (Frame <= 1 * scal)
            {
                return "Exit1.png";
            }
            if (Frame <= 2 * scal)
            {
                return "Exit2.png";
            }
            if (Frame <= 3 * scal)
            {
                return "Exit3.png";
            }
            if (Frame <= 4 * scal)
            {
                return "Exit4.png";
            }
            if (Frame <= 5 * scal)
            {
                return "Exit5.png";
            }
            return "";
        }
    }
}
