using DoodleJump.Classes;
using System.Timers;

namespace DoodleJump
{
    public partial class MainForm : Form
    {
        Player player;
        Rectangle workingArea;
        System.Timers.Timer timer;
        GameState gameState;
        public MainForm()
        {
            InitializeComponent();
            ÑalibrationSize();
            InitializeGameState();
            player = new Player();
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


        private void OnKeyBoardUp(object sender, KeyEventArgs e)
        {
            player.Physics.dx = 0;
            if (e.KeyCode == Keys.Up)
            {
                player.Sprite = GameConfig.Player.Sprites.Right;
            }
        }

        private void OnKeyBoardPressed(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Right":
                case "D":
                    player.Physics.dx = GameConfig.Player.MovingOx;
                    if (player.Sprite != GameConfig.Player.Sprites.Right)
                    {
                        player.Sprite = GameConfig.Player.Sprites.Right;
                    }
                    break;
                case "Left":
                case "A":
                    player.Physics.dx = -GameConfig.Player.MovingOx;
                    if (player.Sprite != GameConfig.Player.Sprites.Left)
                    {
                        player.Sprite = GameConfig.Player.Sprites.Left;
                    }
                    break;
                case "Up":
                case "W":
                    if (player.Sprite != GameConfig.Player.Sprites.Up)
                    {
                        player.Sprite = GameConfig.Player.Sprites.Up;
                    }
                    break;
            }
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
            GameManagerObjectCanvas.MaintainPlatformsCount();
            this.Text = "Doodle Jump: Score - " + GameState.Score;
            GameManagerObjectCanvas.GenerateStartSequence();
            GameManagerObjectCanvas.DeleteTouchObject();
            GameManagerObjectCanvas.ClearObjectCanvas();
            GameManagerObjectCanvas.ApplyPhysicsObject();
            player.Physics.WrapHorizontalPosition(doodleCanvas);
            player.Physics.ApplyPhysics();
            doodleCanvas.Invalidate();
            FollowPlayer();
        }

        private void OnRepaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (GameManagerObjectCanvas.objectCanvas.Count > 0)
            {
                for (int i = 0; i < GameManagerObjectCanvas.objectCanvas.Count; i++)
                {
                    GameManagerObjectCanvas.objectCanvas[i].DrawSprite(g);
                }
            }
            player.DrawSprite(g);
            doodleCanvas.Invalidate();
        }

        private void FollowPlayer()
        {
            float playerY = player.Physics.transform.Position.Y;
            float halfScreenHeight = GameConfig.CanvasParameters.Height / 2f;

            if (playerY < halfScreenHeight)
            {
                float offset = halfScreenHeight - playerY;
                MoveAllObjects(offset);
                player.Physics.transform.Position.Y += offset;
            }
        }

        private void MoveAllObjects(float offset)
        {
            for (int i = GameManagerObjectCanvas.objectCanvas.Count - 1; i >= 0; i--)
            {
                var obj = GameManagerObjectCanvas.objectCanvas[i];
                obj.Transform.Position.Y += offset;
            }
        }
    }
}
