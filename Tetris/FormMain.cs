using System.Windows.Forms;
using static Tetris.Brick;

namespace Tetris
{
    public partial class FormMain : Form
    {
        Canvas canvas;      // ��Ϸ����ʵ��

        /// <summary>
        /// ���캯��
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            // ��ʼ����Ϸ������������Ϸ�����й���
            game_panel.Image = new Bitmap(game_panel.Width, game_panel.Height);
            canvas = new Canvas(game_panel.Image, new Size(20, 25));
            //canvas = new Canvas(game_panel.Image, new Size(20, 25));
        }

        /// <summary>
        /// �¼���������������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            // ������Ϸ
            StartGame();
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {

           // game_panel.Image = new Bitmap(game_panel.Width, game_panel.Height);
        }

        /// <summary>
        /// �¼�������ʱ����ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            // ��Ϸ���һ������
            if (!canvas.Tick())
            {
                // ��Ϸ������������ʾ��������Ϸ
                StopGame();
                StartGame();
            }

            // ˢ����Ϸ�����ʾ
            game_panel.Refresh();
            textBox1.Text=canvas.score.ToString();
        }

        /// <summary>
        /// �¼�������Ӧ���̲���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        // ��Ϸ������������ʾ��������Ϸ
                        StopGame();
                        StartGame();
                    }
                    game_panel.Refresh();
                    break;
            }
        }

        /// <summary>
        /// ��������������Ϸ
        /// </summary>
        private void StartGame()
        {
            // ������Ϸ
            canvas.StartGame();

            // ������ʱ��
            timer.Start();
        }

        /// <summary>
        /// ����������Ϸ����
        /// </summary>
        private void StopGame()
        {
            // ֹͣ��ʱ��
            timer.Stop();

            // ������ʾ��Ϸ����
            MessageBox.Show("��Ϸ������������ȷ������ť���¿�ʼ��Ϸ��", "��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}