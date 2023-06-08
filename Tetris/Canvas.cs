using static Tetris.Brick;


namespace Tetris
{
    public class Canvas
    {
        private Brick brick_current = default!;         // 当前砖块
        private Brick brick_next = default!;            // 下一个砖块

        private Image panel_image;                      // 游戏显示面板绘图实例

        private int[,] canvas_data;                     // 画布抽象状态数组（不含当前砖块），数组中1表示该位置有方块，0表示该位置为空

        private int cube_width;                         // 组成砖块的方块宽度
        private Random random = new Random();           // 用于生成随机砖块
        private SolidBrush brushColor;
        public int score = 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="game_panel"></param>
        /// <param name="canvas_size"></param>
        public Canvas (Image game_panel, Size canvas_size)
        {
            // 保存游戏绘图面板实例
            this.panel_image = game_panel;

            // 初始化画布抽象状态数组
            canvas_data = new int[canvas_size.Height, canvas_size.Width];

            // 计算组成砖块的方块宽度
            this.cube_width = game_panel.Width / canvas_size.Width;
            this.brushColor = this.GetRandomBrush();

    }

    /// <summary>
    /// 方法——完成一个游戏节拍
    /// </summary>
    /// <returns></returns>
    public bool Tick()
        {
            return BrickMove(DIRECTION.Down);
        }

        /// <summary>
        /// 方法——启动新游戏
        /// </summary>
        public void StartGame()
        {
            // 生成当前砖块
            brick_current = GetNewBrick();

            // 生成下一个砖块
            brick_next = GetNewBrick();

            // 清除画布状态
            for (int i = 0; i < canvas_data.GetLength(0); i++)
            {
                for (int j = 0; j < canvas_data.GetLength(1); j++)
                    canvas_data[i, j] = 0;
            }

            // 重绘画布
            Paint();
        }

        /// <summary>
        /// 方法——按指定方向移动砖块
        /// </summary>
        /// <returns>如果砖块向下移动到底后，无空间生成新砖块，则返回false，否则返回true</returns>
        public bool BrickMove(DIRECTION direction)
        {
            if (brick_current.Move(direction))
                // 重绘画布
                Paint();
            else
            {
                if (direction == DIRECTION.Down)
                {
                    // 无法向下移动，则处理到底的砖块，并生成新砖块
                    if (!NextBrick())
                        return false;

                    // 重绘画布
                    Paint();
                }
            }

            return true;
        }

        /// <summary>
        /// 方法——旋转砖块
        /// </summary>
        /// <returns></returns>
        public bool BrickRotate()
        {
            if (brick_current.Rotate())
            {
                // 重绘画布
                Paint();

                return true;
            }
            else
                return false;
        }

        private SolidBrush GetRandomBrush()
        {
            Random random = new Random();
            Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            return new SolidBrush(randomColor);
        }

        /// <summary>
        /// 函数——画布重绘
        /// </summary>
        private void Paint()
        {
            // 获取游戏面板的绘图句柄
            Graphics graphics_panel = Graphics.FromImage(panel_image);

            // 清除画布已有内容
            graphics_panel.Clear(Color.White);

            // 绘制画布底部的方块
            for (int i = 0;i < canvas_data.GetLength(0); i++)
            {
                for (int j = 0;j < canvas_data.GetLength(1);j++)
                {
                    if (canvas_data[i, j] != 0)
                        graphics_panel.FillRectangle(Brushes.Gray, j * cube_width + 1, i * cube_width + 1, cube_width - 2, cube_width - 2);
                }
            }

            // 绘制当前砖块
            for (int i = 0; i < brick_current.Data.GetLength(0); i++)
            {
                for (int j = 0; j < brick_current.Data.GetLength (1); j++)
                {
                    if (brick_current.Data[i, j] != 0)
                        // graphics_panel.FillRectangle(Brushes.SlateBlue, 
                        graphics_panel.FillRectangle(this.brushColor, 
                            (j + brick_current.Location.X) * cube_width + 1, 
                            (i + brick_current.Location.Y) * cube_width + 1, 
                            cube_width - 2, cube_width - 2);
                }
            }
        }

        public int addScore()
        {
            score += 10;
            return score;
        }

        /// <summary>
        /// 函数——清除已经填满的行
        /// </summary>
        private void RemoveFilledRows()
        {
            // 从上至下进行行遍历，检查该行是否已满
            for (int i = 0; i < canvas_data.GetLength(0); i++)
            {
                // 检查该行是否已满
                bool isFilled = true;
                for (int j = 0;j < canvas_data.GetLength (1);j++)
                {
                    if (canvas_data[i, j] == 0)
                    {
                        isFilled = false;
                        break;
                    }
                }

                // 如该行已满，则该行之上所有行方块下降
                if (isFilled)
                {
                    for (int m = i; m > 0; m--)
                    {
                        for (int n = 0; n < canvas_data.GetLength(1); n++)
                            canvas_data[m, n] = canvas_data[m - 1, n];
                    }

                    // 行满就加分
                    this.addScore();
                    // 清除顶行数据
                    for (int n = 0; n < canvas_data.GetLength(1); n++)
                        canvas_data[0, n] = 0;
                }
            }
        }

        /// <summary>
        /// 函数——返回随机生成的新砖块
        /// </summary>
        /// <returns></returns>
        private Brick GetNewBrick()
        {
            this.brushColor = this.GetRandomBrush();
            switch (random.Next(7))
            {
                case 0:
                    // 产生方形砖块
                    return new BrickSquare(canvas_data);
                case 1:
                    // 产生长条形砖块
                    return new BrickOneType(canvas_data);
                case 2:
                    // 产生T字形砖块
                    return new BrickTType(canvas_data);
                case 3:
                    // 产生Z字形砖块
                    return new BrickLType(canvas_data);
                case 4:
                    // 产生反Z字形砖块
                    return new BrickLRType(canvas_data);
                case 5:
                    // 产生L字形砖块
                    return new BrickZType(canvas_data);
                case 6:
                    // 产生反L字形砖块
                    return new BrickZRType(canvas_data);
                default:
                    return null!;
            }
        }

        /// <summary>
        /// 函数——砖块到底后，生成新砖块
        /// </summary>
        /// <returns>如果没有空间生成新砖块，则返回false，否则返回true</returns>
        private bool NextBrick()
        {
            // 将砖块合并到画布
            for (int i = 0; i < brick_current.Data.GetLength(0); i++)
            {
                for (int j = 0; j < brick_current.Data.GetLength(1); j++)
                {
                    if (brick_current.Location.Y + i < 0 ||
                        brick_current.Location.Y + i > canvas_data.GetLength(0) - 1 ||
                        brick_current.Location.X + j < 0 ||
                        brick_current.Location.X + j > canvas_data.GetLength(1) - 1)
                        continue;

                    canvas_data[brick_current.Location.Y + i, brick_current.Location.X + j] += brick_current.Data[i, j];
                }
            }

            // 清除已填满的行
            RemoveFilledRows();

            // 下一个砖块替补到当前砖块，重新生成下一个砖块
            brick_current = brick_next;
            brick_next = GetNewBrick();

            return brick_current.Init();
        }
    }
}