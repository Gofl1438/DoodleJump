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
            СalibrationSize();
            InitializeGameState();
            GameManagerUI.AppendElementInterface();
            timer = new System.Timers.Timer(15);
            timer.AutoReset = true;
            timer.SynchronizingObject = this;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }
        private void СalibrationSize()
        {
            workingArea = Screen.FromControl(this).WorkingArea;
            this.Height = workingArea.Height;
            this.Width = workingArea.Width;
            this.MinimumSize = new Size(workingArea.Width, workingArea.Height);
            GameConfig.Initialize(new Size(doodleCanvas.Size.Width, doodleCanvas.Size.Height));
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
                    AddBallList();
                    break;
            }
        }
        private void Update(object sender, EventArgs e)
        {
            if (!GameState.IsSceneMenu)
            {
                if (GameState.IsMonsterDeath)
                {
                    FollowMonsterDeath();
                    player.Physics.MoveOx();
                }
                else if (GameState.IsFallDeath)
                {
                    FollowFallDeath();
                    player.Physics.MoveOx();
                }
                else
                {
                    GameManagerObjectCanvas.MaintainPlatformsCount();
                    this.Text = "Doodle Jump: Score - " + GameState.Score;
                    GameManagerObjectCanvas.DeleteTouchObject();
                    GameManagerObjectCanvas.ClearObjectCanvas();
                    player.Physics.ApplyPhysics();
                    FollowPlayer();
                }
                GameManagerObjectCanvas.ClearBall();
                GameManagerObjectCanvas.ApplyPhysicsObject();
                GameManagerObjectCanvas.ApplyPhysicsBall();
                player.Physics.WrapHorizontalPosition(doodleCanvas);
                doodleCanvas.Invalidate();
            }
        }


        private void InitializeGameState()
        {
            gameState = new GameState();
        }

        private void OnRepaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (GameState.IsSceneMenu)
            {
                for (int i = 0; i < GameManagerUI.elementStartMenu.Count; i++)
                {
                    GameManagerUI.elementStartMenu[i].DrawSprite(g);
                }
            }
            else
            {
                if (GameManagerObjectCanvas.objectCanvas.Count > 0)
                {
                    for (int i = 0; i < GameManagerObjectCanvas.objectCanvas.Count; i++)
                    {
                        GameManagerObjectCanvas.objectCanvas[i].DrawSprite(g);
                    }
                }
                if (GameManagerObjectCanvas.ball.Count > 0)
                {
                    for (int i = 0; i < GameManagerObjectCanvas.ball.Count; i++)
                    {
                        GameManagerObjectCanvas.ball[i].DrawSprite(g);
                    }
                }
                player.DrawSprite(g);
                if (GameManagerUI.elementInterface.Count > 0)
                {
                    for (int i = 0; i < GameManagerUI.elementInterface.Count; i++)
                    {
                        GameManagerUI.elementInterface[i].DrawSprite(g);
                    }
                }
                if (GameState.isDoodleFrozen)
                {
                    for (int i = 0; i < GameManagerUI.elementPause.Count; i++)
                    {
                        GameManagerUI.elementPause[i].DrawSprite(g);
                    }
                    timer.Stop();
                    GameState.IsScenePause = true;
                }
                if (GameState.IsSceneGameOver)
                {
                    GameManagerObjectCanvas.AllClearObject();
                    doodleCanvas.Invalidate();
                    for (int i = 0; i < GameManagerUI.elementGameOver.Count; i++)
                    {
                        GameManagerUI.elementGameOver[i].DrawSprite(g);
                    }
                }
            }
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

        /// <summary>
        /// Перемещение камеры при столкновении с чудищем
        /// </summary>
        private void FollowMonsterDeath()
        {
            if (player.Physics.transform.Position.Y < GameConfig.CanvasParameters.Height)
            {
                int offset = GameConfig.CameraSpeeds.MonsterDeath;
                for (int i = 0; i < GameManagerObjectCanvas.objectCanvas.Count; i++)
                {
                    var plaform = GameManagerObjectCanvas.objectCanvas[i];
                    plaform.Transform.Position.Y -= (offset);
                }
                if (GameManagerObjectCanvas.objectCanvas[GameManagerObjectCanvas.objectCanvas.Count / 3].Transform.Position.Y < 0)
                {
                    player.Physics.transform.Position.Y += offset;
                }
            }
            else
            {
                timer.Stop();
                GameState.IsSceneGameOver = true;
            }
        }

        /// <summary>
        /// Перемещении камеры при "глубоком падении"
        /// </summary>
        private void FollowFallDeath()
        {
            if (player.Physics.transform.Position.Y < GameConfig.CanvasParameters.Height / 2)
            {
                GameState.IsFallDeathSceneTwo = true;
            }
            if (GameConfig.CanvasParameters.Height / 2 < player.Physics.transform.Position.Y && !GameState.IsFallDeathSceneTwo)
            {
                int offset = GameConfig.CameraSpeeds.FallDeath;
                player.Physics.transform.Position.Y -= offset;
                for (int i = 0; i < GameManagerObjectCanvas.objectCanvas.Count; i++)
                {
                    var plaform = GameManagerObjectCanvas.objectCanvas[i];
                    plaform.Transform.Position.Y -= (offset);
                }
            }
            if (GameState.IsFallDeathSceneTwo)
            {
                FollowFallDeathSceneTwo();
            }
        }

        /// <summary>
        /// Вторая сцена проигрыша при "глубоком падении"
        /// </summary>
        private void FollowFallDeathSceneTwo()
        {
            if (player.Physics.transform.Position.Y < GameConfig.CanvasParameters.Height)
            {
                int offset = GameConfig.CameraSpeeds.FallDeathSceneTwo;
                player.Physics.transform.Position.Y += offset;
                for (int i = 0; i < GameManagerObjectCanvas.objectCanvas.Count; i++)
                {
                    var plaform = GameManagerObjectCanvas.objectCanvas[i];
                    plaform.Transform.Position.Y -= (offset);
                }
            }
            else
            {
                timer.Stop();
                GameState.IsSceneGameOver = true;
            }
        }


        private void AddBallList()
        {
            int posY = (int)player.Physics.transform.Position.Y;
            int posX = (int)player.Physics.transform.Position.X;
            Ball ball = new Ball(posX, posY);
            GameManagerObjectCanvas.ball.Add(ball);
        }


        private void doodleCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (GameState.IsSceneMenu)
            {
                if (new Rectangle(GameConfig.MainMenuConfig.Positions.ButtonStartPlay, GameConfig.MainMenuConfig.Dimensions.ButtonStartPlay).Contains(e.Location))
                {
                    RestartGame();
                    GameState.IsSceneMenu = false;
                }
            }
            else
            {
                if (GameState.IsSceneGameOver)
                {
                    if (new Rectangle(GameConfig.GameOverConfig.Positions.ButtonPlayAgain, GameConfig.GameOverConfig.Dimensions.ButtonPlayAgain).Contains(e.Location))
                    {
                        RestartGame();
                        GameState.IsSceneGameOver = false;
                        GameState.IsMonsterDeath = false;
                        GameState.IsFallDeath = false;
                        GameState.IsFallDeathSceneTwo = false;
                        timer.Start();
                    }
                    if (new Rectangle(GameConfig.GameOverConfig.Positions.ButtonMenu, GameConfig.GameOverConfig.Dimensions.ButtonMenu).Contains(e.Location))
                    {
                        InitializeGameState();
                        GameManagerObjectCanvas.AllClearObject();
                        GameManagerUI.AddElementStartMenu();
                        doodleCanvas.Invalidate();
                        timer.Start();
                    }
                }
                else
                {
                    if (!GameState.isDoodleFrozen)
                    {
                        if (new Rectangle(GameConfig.GameUiConfig.Positions.PauseButton, GameConfig.GameUiConfig.Dimensions.PauseButton).Contains(e.Location))
                        {
                            ApplyPause();
                        }
                    }
                    if (GameState.isDoodleFrozen)
                    {
                        if (new Rectangle(GameConfig.PauseMenuConfig.Positions.ButtonResume, GameConfig.PauseMenuConfig.Dimensions.ButtonResume).Contains(e.Location))
                        {
                            ApplyPause();
                        }
                    }
                }
            }
        }

        private void RestartGame()
        {
            GameManagerObjectCanvas.AllClearObject();
            GameManagerObjectCanvas.GenerateStartSequence(); ///возможно нужно посмотреть на логику, то есть обнулить логику появления платформ!!!
            player = new Player();
        }

        private void ApplyPause()
        {
            if (!GameState.isDoodleFrozen)
            {
                GameState.isDoodleFrozen = true;
            }
            else
            {
                GameState.isDoodleFrozen = false;
                timer.Start();
            }
        }
    }
}
