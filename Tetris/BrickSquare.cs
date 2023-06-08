using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    public class BrickSquare : Brick
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="canvas_data"></param>
        public BrickSquare (int[,] canvas_data):base(canvas_data)
        {
            // 初始化方形砖块数据
            brick_data = new int[,] { { 1, 1 },
                                      { 1, 1 }};

            // 定义方形砖块初始位置
            location = new Point((canvas_data.GetLength(1) - 2) / 2, 0);
        }
    }
}