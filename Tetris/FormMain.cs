using System.Windows.Forms;
using static Tetris.Brick;

namespace Tetris
{
    public partial class FormMain : Form
    {
        Canvas canvas;      

        /// <summary>
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            game_panel.Image = new Bitmap(game_panel.Width, game_panel.Height);
            canvas = new Canvas(game_panel.Image, new Size(20, 25));
        }


        private void FormMain_Load(object sender, EventArgs e)
        {
            StartGame();
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {

           // game_panel.Image = new Bitmap(game_panel.Width, game_panel.Height);
        }

     
        private void timer_Tick(object sender, EventArgs e)
        {
            if (!canvas.Tick())
            {
                StopGame();
                StartGame();
            }

            game_panel.Refresh();
            textBox1.Text=canvas.score.ToString();
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

        private void radioButton1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.Handled = true; // 禁止自动选择
            }
        }
        private void radioButton2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.Handled = true; // 禁止自动选择
            }
        }
        private void radioButton3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.Handled = true; // 禁止自动选择
            }
        }
    }
}