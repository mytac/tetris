namespace Tetris
{
    public abstract class Brick
    {
        protected int[,] brick_data = default!;        // 保存砖块结构数据
        protected int[,] canvas_data;                  // 保存画布结构数据
        protected Point location;                      // 保存砖块左上角在画布中的坐标

        /// <summary>
        /// 枚举——移动方向
        /// </summary>
        public enum DIRECTION { Left, Right, Down };

        /// <summary>
        /// 属性——获取砖块结构数据
        /// </summary>
        public int[,] Data
        {
            get { return brick_data; }
        }

        /// <summary>
        /// 属性——获取砖块左上角在画布中的坐标
        /// </summary>
        public Point Location
        {
            get { return location; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="canvas_data"></param>
        public Brick(int[,] canvas_data) 
        {
            this.canvas_data = canvas_data;
        }

        /// <summary>
        /// 方法——旋转砖块
        /// </summary>
        /// <returns>返回是否旋转成功</returns>
        public bool Rotate ()
        {
            // 判断砖块变形区域必须为方阵
            if (brick_data.GetLength(0) != brick_data.GetLength (1))
                return false;

            // 旋转砖块
            int[,] new_data = new int[brick_data.GetLength(0), brick_data.GetLength(1)];
            int len = new_data.GetLength(0);
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                    new_data[i, j] = brick_data[len - 1 - j, i];
            }

            // 判断旋转后是否发生碰撞
            if (CollisionDetect(new_data, location))
                return false;
            else
            {
                brick_data = new_data;
                return true;
            }
        }

        /// <summary>
        /// 方法——控制砖块向下移动一个位置
        /// </summary>
        /// <param name="direction"></param>
        /// <returns>移动成功返回true，因空间不足移动失败返回false</returns>
        public bool Move (DIRECTION direction) 
        {
            switch (direction)
            {
                case DIRECTION.Left:
                    if (CollisionDetect(brick_data, new Point(location.X - 1, location.Y)))
                        return false;
                    else
                    {
                        location.X = location.X - 1;
                        return true;
                    }
                case DIRECTION.Right:
                    if (CollisionDetect(brick_data, new Point(location.X + 1, location.Y)))
                        return false;
                    else
                    {
                        location.X = location.X + 1;
                        return true;
                    }
                case DIRECTION.Down:
                    if (CollisionDetect(brick_data, new Point(location.X, location.Y + 1)))
                        return false;
                    else
                    {
                        location.Y = location.Y + 1;
                        return true;
                    }
                default: return false;
            }
        }

        /// <summary>
        /// 函数——检测给定的砖块是否与墙壁或底部元素发生碰撞
        /// </summary>
        /// <param name="brick_data"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        private bool CollisionDetect(int[,] brick_data, Point location)
        {
            
            for (int i = 0; i < brick_data.GetLength(0); i++)
            {
                for (int j = 0; j < brick_data.GetLength(1); j++)
                {
                    if (brick_data[i, j] != 0)
                    {
                        // 判断砖块是否超出边界
                        if (j + location.X < 0 ||
                            j + location.X > canvas_data.GetLength(1) - 1 ||
                            i + location.Y < 0 ||
                            i + location.Y > canvas_data.GetLength(0) - 1)
                            return true;

                        // 判断砖块是否与底部方块碰撞
                        if (canvas_data[i + location.Y, j + location.X] != 0)
                            return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 方法——初始化砖块
        /// </summary>
        /// <returns></returns>
        public bool Init()
        {
            // 如果已经没有空间放置初始化砖块，则返回false
            return !CollisionDetect(brick_data, location);
        }
    }
}