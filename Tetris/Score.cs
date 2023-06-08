using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Score
    {
        // speed*=level*hardLevel

        public int hardLevel=1; //难度等级，和下落倍率相乘

        public int score=0; // score>level
        public int award = 10; // 每消除一行得多少分，随关卡变化   award=10*hardLevel*level
        public int level = 1;

        private int threshold=1; // 进阶阈值  threshold*=2**level
        public int speed = 1000; // 下落速度
        // private int threshold=100; // 进阶阈值  threshold*=2**level

        public void reset()
        {
            score=0;
            award=0;
            level = 1;
            threshold = 1;
            speed = 1000;
        }
        public void setHardLevel(int hardLevel) // 游戏初始化时调用
        {
            if (hardLevel<1)
            {
                this.hardLevel = 1;
                return;
            }
            if (hardLevel>3)
            {
                this.hardLevel = 3;
                return;
            }
            this.hardLevel = hardLevel;
        }


        private void changeLevel() // 当分数到达阈值时，更新阈值、关卡、每次下落得分
        {
            if (score>=this.threshold) 
            {
                this.level += 1;
                this.threshold *= Convert.ToInt32(level*0.8);
                this.award = 10 * hardLevel * level;
                int targetSpeed= 1000 - hardLevel * level * 120;
                this.speed = targetSpeed < 0 ? 10 : targetSpeed;
            }
        }

       
        // 消除一行就调用
        public int addScore()
        {
            score += this.award;
            this.changeLevel();
            return score;
        }

   

    }
}
