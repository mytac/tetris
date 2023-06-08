using System.Windows.Forms;
using static Tetris.Brick;

namespace Tetris
{
    public partial class FormMain : Form
    {
        Canvas canvas;
        private Score score;
        private int speed = 1000;
        public FormMain()
        {
            score=new Score();
            InitializeComponent();
            game_panel.Image = new Bitmap(game_panel.Width, game_panel.Height);
            canvas = new Canvas(game_panel.Image, new Size(20, 25), score);
            levelText.Text = score.level.ToString();
            timer.Interval = speed;
        }


        private void FormMain_Load(object sender, EventArgs e)
        {
            StartGame();
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {

           // game_panel.Image = new Bitmap(game_panel.Width, game_panel.Height);
        }

        private void renderLeft()
        {
            textBox1.Text = score.score.ToString(); // 渲染分数
            levelText.Text = score.level.ToString(); // 渲染关卡
            if (!score.speed.Equals(this.speed))
            {
                timer.Stop();
                timer.Interval = score.speed;
                timer.Start();

            }
            Console.WriteLine(score.speed.ToString());
        }

     
        private void timer_Tick(object sender, EventArgs e)
        {
            if (!canvas.Tick())
            {
                StopGame();
                StartGame();
            }

            game_panel.Refresh();
            renderLeft();
        }

       
        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (canvas.BrickRotate())
                        game_panel.Refresh();
                    break;
                case Keys.Left:
                    canvas.BrickMove(DIRECTION.Left);
                    game_panel.Refresh();
                    break;
                case Keys.Right:
                    canvas.BrickMove(DIRECTION.Right);
                    game_panel.Refresh();
                    break;
                case Keys.Down:
                    if (!canvas.BrickMove(DIRECTION.Down))
                    {
                        StopGame();
                        StartGame();
                    }
                    game_panel.Refresh();
                    break;
            }
        }


        private void StartGame()
        {
            canvas.StartGame();

            timer.Start();
        }


        private void StopGame()
        {
            // ֹͣ��ʱ��
            timer.Stop();

            // ������ʾ��Ϸ����
            MessageBox.Show("游戏结束", "��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

    }
}