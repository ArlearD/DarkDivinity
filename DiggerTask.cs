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
            if (conflictedObject.GetImageFileName() == "Digger.png" ||
                conflictedObject.GetImageFileName() == "Monster.png")
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
            return "Terrain.png";
        }
    }

    public class Player : ICreature
    {
        public bool RunState = false;
        public int RunFrame = 1;
        public CreatureCommand Act(int x, int y)
        {
            int xvalue = 0;
            int yvalue = 0;
            switch (Game.KeyPressed)
            {
                case System.Windows.Forms.Keys.Left:
                    if (x - 1 >= 0)
                        xvalue = -1;
                    break;
                case System.Windows.Forms.Keys.Right:
                    if (x + 1 < Game.MapWidth)
                        xvalue = 1;
                    break;
                case System.Windows.Forms.Keys.Up:
                    for (int i = 0; i <= 2; i++)
                    {
                        if (y - i >= 0 
                            && Game.Map[x, y + 1] != null 
                            && Game.Map[x, y + 1].ToString() == "Digger.Terrain")
                            yvalue = -i;
                    }
                        break;
                case System.Windows.Forms.Keys.Down:
                    if (y + 1 < Game.MapHeight)
                        yvalue = 1;
                    break;
            }
            if (y + 1 < Game.MapHeight && Game.Map[x, y + 1] == null)
            {
                yvalue++;
            }

            if (Game.Map[x + xvalue, y + yvalue] != null)
            {
                if (Game.Map[x + xvalue, y + yvalue].ToString() == "Digger.Sack"
                    || Game.Map[x + xvalue, y + yvalue].ToString() == "Digger.Terrain")
                {
                    RunState = false;
                    return new CreatureCommand
                    {
                        DeltaX = 0,
                        DeltaY = 0
                    };
                }
            }
            else
            {
                if (xvalue != 0)
                {
                    RunState = true;
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
            if (RunState)
            {
                if (RunFrame >= 8) RunFrame = 1;
                var currentFrame = RunFrame;
                RunFrame++;
                return "DiggerRun" + currentFrame + ".png";
            }
            else
            {
                return "Digger.png";
            }
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

    public class Checker
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

}