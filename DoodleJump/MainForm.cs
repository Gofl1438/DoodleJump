using DoodleJump.Classes;
using Microsoft.Win32;
using System.Diagnostics;
using System.Reflection;
using System.Timers;
using System.Windows.Forms;
using static System.Formats.Asn1.AsnWriter;

namespace DoodleJump
{
    public partial class MainForm : Form
    {
        Player player;
        Rectangle workingArea;
        System.Windows.Forms.Timer timer;

        /// <summary>
        /// Конструктор главной формы.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            СalibrationSize();
            InitializeGameState();
            GameManagerUI.AddElementStartMenu();
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 14;
            timer.Tick += Update;
            timer.Start();
        }

        /// <summary>
        /// Выполняет калибровку размеров главного окна в соответствии с рабочей областью экрана.
        /// </summary>
        private void СalibrationSize() 
        {
            workingArea = Screen.FromControl(this).WorkingArea;
            this.Height = workingArea.Height;
            this.Width = workingArea.Width;
            this.MinimumSize = new Size(workingArea.Width, workingArea.Height);
            GameConfig.Initialize(new Size(doodleCanvas.Size.Width, doodleCanvas.Size.Height));
        }

        /// <summary>
        /// Обрабатывает отпускание клавиши, останавливает движение и меняет спрайт игрока.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyBoardUp(object sender, KeyEventArgs e)
        {
            if (player != null)
            {
                player.Physics.Dx = 0;
                GameState.IsWasShoot = false;
                if (e.KeyCode == Keys.Up)
                {
                    player.Sprite = GameConfig.Player.Sprites.Right;
                }
            }
        }

        /// <summary>
        /// Осуществляет обработку и реагирование на события нажатия клавиш.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyBoardPressed(object sender, KeyEventArgs e)
        {
            if (player != null)
            {
                switch (e.KeyCode.ToString())
                {
                    case "Right":
                    case "D":
                        player.Physics.Dx = GameConfig.Player.MovingOx;
                        if (player.Sprite != GameConfig.Player.Sprites.Right)
                        {
                            player.Sprite = GameConfig.Player.Sprites.Right;
                        }
                        break;
                    case "Left":
                    case "A":
                        player.Physics.Dx = -GameConfig.Player.MovingOx;
                        if (player.Sprite != GameConfig.Player.Sprites.Left)
                        {
                            player.Sprite = GameConfig.Player.Sprites.Left;
                        }
                        break;
                    case "Up":
                    case "W":
                        if (!GameState.IsWasShoot && !GameState.IsFallDeath && !GameState.IsDoodleFrozen && !GameState.IsMonsterDeath)
                        {
                            GameState.IsWasShoot = true;
                            if (player.Sprite != GameConfig.Player.Sprites.Up)
                            {
                                player.Sprite = GameConfig.Player.Sprites.Up;
                            }
                            AddBallList();
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Обрабатывает обновление игровой логики и физики.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    player.Physics.ApplyPhysics();
                    GameManagerObjectCanvas.DeleteTouchObject();
                    GameManagerObjectCanvas.MaintainPlatformsCount();
                    GameManagerObjectCanvas.RemoveOffScreenObjects();
                    FollowPlayer();
                }
                player.Physics.WrapHorizontalPosition(doodleCanvas);
                GameManagerObjectCanvas.ApplyPhysicsObject();
                GameManagerObjectCanvas.ApplyPhysicsBall();
                GameManagerObjectCanvas.ClearBall();
            }
            doodleCanvas.Invalidate();
        }

        /// <summary>
        /// Реализует сброс всех значений состояния до значений по умолчанию.
        /// </summary>
        private void InitializeGameState()
        {
            GameState gameState = new GameState();
        }

        /// <summary>
        /// Реализует отрисовку всех игровых объектов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRepaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (GameState.IsSceneMenu)
            {
                for (int i = 0; i < GameManagerUI.MainMenuElements.Count; i++)
                {
                    GameManagerUI.MainMenuElements[i].DrawSprite(g);
                }
            }
            else
            {
                for (int i = 0; i < GameManagerObjectCanvas.Objects.Count; i++)
                {
                    GameManagerObjectCanvas.Objects[i].DrawSprite(g);
                }
                for (int i = 0; i < GameManagerObjectCanvas.ActiveBalls.Count; i++)
                {
                    GameManagerObjectCanvas.ActiveBalls[i].DrawSprite(g);
                }
                player.DrawSprite(g);
                for (int i = 0; i < GameManagerUI.InterfaceElements.Count; i++)
                {
                    GameManagerUI.InterfaceElements[i].DrawSprite(g);
                }
                DrawScore(g);
                if (GameState.IsDoodleFrozen)
                {
                    for (int i = 0; i < GameManagerUI.PauseMenuElements.Count; i++)
                    {
                        GameManagerUI.PauseMenuElements[i].DrawSprite(g);
                    }
                    timer.Stop();
                    GameState.IsScenePaused = true;
                }
                if (GameState.IsSceneGameOver)
                {
                    GameManagerObjectCanvas.AllClearObject();
                    doodleCanvas.Invalidate();
                    for (int i = 0; i < GameManagerUI.GameOverElements.Count; i++)
                    {
                        GameManagerUI.GameOverElements[i].DrawSprite(g);
                    }
                }
            }
        }

        /// <summary>
        /// Реализует отрисовку очков.
        /// </summary>
        /// <param name="g"></param>
        private void DrawScore(Graphics g)
        {
            g.DrawString(GameState.Score.ToString(),
                GameConfig.GameUiConfig.Dimensions.ScoreFont,
                GameConfig.GameUiConfig.CustomBrush,
                GameConfig.GameUiConfig.Positions.Score
                );
        }

        /// <summary>
        /// Реализует стандартное перемещение камеры при прыжке главного героя.
        /// </summary>
        private void FollowPlayer()
        {
            float playerY = player.Physics.Transform.Position.Y;
            float halfScreenHeight = GameConfig.CanvasParameters.Height / 2f;

            if (playerY < halfScreenHeight)
            {
                float offset = halfScreenHeight - playerY;
                MoveAllObjects(offset);
                player.Physics.Transform.Position.Y += offset;
                GameState.Score += (int)offset;
            }
        }
        /// <summary>
        /// Реализует перемещение каждого объекта при прыжке главного героя.
        /// </summary>
        /// <param name="offset"></param>
        private void MoveAllObjects(float offset)
        {
            for (int i = GameManagerObjectCanvas.Objects.Count - 1; i >= 0; i--)
            {
                var obj = GameManagerObjectCanvas.Objects[i];
                obj.Transform.Position.Y += offset;
            }
        }

        /// <summary>
        /// Реализует проигрышное перемещение камеры при столкновении с монстром.
        /// </summary>
        private void FollowMonsterDeath()
        {
            if (player.Physics.Transform.Position.Y < GameConfig.CanvasParameters.Height)
            {
                int offset = GameConfig.CameraSpeeds.MonsterDeath;
                for (int i = 0; i < GameManagerObjectCanvas.Objects.Count; i++)
                {
                    var plaform = GameManagerObjectCanvas.Objects[i];
                    plaform.Transform.Position.Y -= (offset);
                }
                if (GameManagerObjectCanvas.Objects[GameManagerObjectCanvas.Objects.Count / 3].Transform.Position.Y < 0)
                {
                    player.Physics.Transform.Position.Y += offset;
                }
            }
            else
            {
                timer.Stop();
                GameState.IsSceneGameOver = true;
                GameManagerUI.AddElementGameOver();
            }
        }

        /// <summary>
        /// Реализует первичное проигрышное перемещение камеры при глубоком падении за пределы экрана.
        /// </summary>
        private void FollowFallDeath()
        {
            if (player.Physics.Transform.Position.Y < GameConfig.CanvasParameters.Height / 2)
            {
                GameState.IsFallDeathSceneTwo = true;
            }
            if (GameConfig.CanvasParameters.Height / 2 < player.Physics.Transform.Position.Y && !GameState.IsFallDeathSceneTwo)
            {
                int offset = GameConfig.CameraSpeeds.FallDeath;
                player.Physics.Transform.Position.Y -= offset;
                for (int i = 0; i < GameManagerObjectCanvas.Objects.Count; i++)
                {
                    var plaform = GameManagerObjectCanvas.Objects[i];
                    plaform.Transform.Position.Y -= (offset);
                }
            }
            if (GameState.IsFallDeathSceneTwo)
            {
                FollowFallDeathSceneTwo();
            }
        }

        /// <summary>
        /// Реализует вторичное проигрышное перемещение камеры при глубоком падении за пределы экрана.
        /// </summary>
        private void FollowFallDeathSceneTwo()
        {
            if (player.Physics.Transform.Position.Y < GameConfig.CanvasParameters.Height)
            {
                int offset = GameConfig.CameraSpeeds.FallDeathSceneTwo;
                player.Physics.Transform.Position.Y += offset;
                for (int i = 0; i < GameManagerObjectCanvas.Objects.Count; i++)
                {
                    var plaform = GameManagerObjectCanvas.Objects[i];
                    plaform.Transform.Position.Y -= (offset);
                }
            }
            else
            {
                timer.Stop();
                GameState.IsSceneGameOver = true;
                GameManagerUI.AddElementGameOver();
            }
        }

        /// <summary>
        /// Выполняет расчёт и добавление объектов (мячей) в специализированный класс.
        /// </summary>
        private void AddBallList()
        {
            int posY = (int)player.Physics.Transform.Position.Y;
            int posX = (int)player.Physics.Transform.Position.X;
            Ball ball = new Ball(posX, posY);
            GameManagerObjectCanvas.ActiveBalls.Add(ball);
        }

        /// <summary>
        /// Обрабатывает взаимодействие пользователя с интерфейсом посредством нажатий мыши.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void doodleCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (GameState.IsSceneMenu)
            {
                if (new Rectangle(GameConfig.MainMenuConfig.Positions.ButtonStartPlay, GameConfig.MainMenuConfig.Dimensions.ButtonStartPlay).Contains(e.Location))
                {
                    RestartGame();
                    GameState.IsSceneMenu = false;
                    GameManagerUI.ClearAllElements();
                    GameManagerUI.AddElementInterface();
                }
            }
            else
            {
                if (GameState.IsSceneGameOver)
                {
                    if (new Rectangle(GameConfig.GameOverConfig.Positions.ButtonPlayAgain, GameConfig.GameOverConfig.Dimensions.ButtonPlayAgain).Contains(e.Location))
                    {
                        InitializeGameState();
                        RestartGame();
                        GameState.IsSceneMenu = false;
                        GameState.Score = 0;
                        timer.Start();
                    }
                    if (new Rectangle(GameConfig.GameOverConfig.Positions.ButtonMenu, GameConfig.GameOverConfig.Dimensions.ButtonMenu).Contains(e.Location))
                    {
                        InitializeGameState();
                        GameManagerObjectCanvas.AllClearObject();
                        GameManagerUI.ClearAllElements();
                        GameManagerUI.AddElementStartMenu();
                        doodleCanvas.Invalidate();
                        timer.Start();
                    }
                }
                else
                {
                    if (!GameState.IsDoodleFrozen)
                    {
                        if (new Rectangle(GameConfig.GameUiConfig.Positions.PauseButton, GameConfig.GameUiConfig.Dimensions.PauseButton).Contains(e.Location))
                        {
                            ApplyPause();
                        }
                    }
                    if (GameState.IsDoodleFrozen)
                    {
                        if (new Rectangle(GameConfig.PauseMenuConfig.Positions.ButtonResume, GameConfig.PauseMenuConfig.Dimensions.ButtonResume).Contains(e.Location))
                        {
                            ApplyPause();
                        }
                    }
                }
            }
        }
        

        /// <summary>
        /// Реализиует рестарт игры.
        /// </summary>
        private void RestartGame()
        {
            GameManagerObjectCanvas.AllClearObject();
            GameManagerObjectCanvas.GenerateStartSequence();
            player = new Player();
        }

        /// <summary>
        /// Реализует остановку игрового процесса.
        /// </summary>
        private void ApplyPause()
        {
            if (!GameState.IsDoodleFrozen)
            {
                GameState.IsDoodleFrozen = true;
                GameManagerUI.AddElementPause();
            }
            else
            {
                GameState.IsDoodleFrozen = false;
                GameManagerUI.ClearAllElements();
                GameManagerUI.AddElementInterface();
                timer.Start();
            }
        }
    }
}
