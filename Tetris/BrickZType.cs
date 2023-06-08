using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    public class BrickZType : Brick
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="canvas_data"></param>
        public BrickZType(int[,] canvas_data) : base(canvas_data)
        {
            // 初始化Z字形砖块数据
            brick_data = new int[,] { { 1, 1, 0 },
                                      { 0, 1, 1 },
                                      { 0, 0, 0} };

            // 定义Z字形砖块初始位置
            location = new Point((canvas_data.GetLength(1) - 3) / 2, 0);
        }
    }
}