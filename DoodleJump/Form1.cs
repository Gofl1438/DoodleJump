using DoodleJump.Classes;
using System.Timers;

namespace DoodleJump
{
    public partial class Form1 : Form
    {
        Rectangle workingArea;
        System.Timers.Timer timer;
        GameState gameState;
        public Form1()
        {
            InitializeComponent();
            InitializeGameState();
            timer = new System.Timers.Timer(15);
            timer.AutoReset = true;
            timer.SynchronizingObject = this;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Update(this, EventArgs.Empty);
        }

        private void ÑalibrationSize()
        {
            workingArea = Screen.FromControl(this).WorkingArea;
            this.Height = workingArea.Height;
            this.Width = workingArea.Width;
            this.MinimumSize = new Size(workingArea.Width, workingArea.Height);
            GameConfig.Initialize(new Size(doodleCanvas.Size.Width, doodleCanvas.Size.Height));
        }

        private void InitializeGameState()
        {
            gameState = new GameState();
        }

        private void Update(object sender, EventArgs e)
        {

        }
    }
}
