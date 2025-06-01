using DoodleJump.Classes;
using System.Reflection;
using System.Timers;
using System.Windows.Forms;

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
            GameManagerUI.AppendElementInterface();
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 15;
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
                player.Physics.dx = 0;
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
                        if (!GameState.IsWasShoot && !GameState.IsFallDeath && !GameState.isDoodleFrozen && !GameState.IsMonsterDeath)
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
                    GameManagerObjectCanvas.ClearObjectCanvas();
                    FollowPlayer();
                }
                player.Physics.WrapHorizontalPosition(doodleCanvas);
                GameManagerObjectCanvas.ApplyPhysicsObject();
                GameManagerObjectCanvas.ApplyPhysicsBall();
                GameManagerObjectCanvas.ClearBall();
                doodleCanvas.Invalidate();
            }
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
                for (int i = 0; i < GameManagerUI.elementStartMenu.Count; i++)
                {
                    GameManagerUI.elementStartMenu[i].DrawSprite(g);
                }
            }
            else
            {
                for (int i = 0; i < GameManagerObjectCanvas.objectCanvas.Count; i++)
                {
                    GameManagerObjectCanvas.objectCanvas[i].DrawSprite(g);
                }
                for (int i = 0; i < GameManagerObjectCanvas.ball.Count; i++)
                {
                    GameManagerObjectCanvas.ball[i].DrawSprite(g);
                }
                player.DrawSprite(g);
                for (int i = 0; i < GameManagerUI.elementInterface.Count; i++)
                {
                    GameManagerUI.elementInterface[i].DrawSprite(g);
                }
                DrawScore(g);
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
            float playerY = player.Physics.transform.Position.Y;
            float halfScreenHeight = GameConfig.CanvasParameters.Height / 2f;

            if (playerY < halfScreenHeight)
            {
                float offset = halfScreenHeight - playerY;
                MoveAllObjects(offset);
                player.Physics.transform.Position.Y += offset;
                GameState.Score += (int)offset;
            }
        }
        /// <summary>
        /// Реализует перемещение каждого объекта при прыжке главного героя.
        /// </summary>
        /// <param name="offset"></param>
        private void MoveAllObjects(float offset)
        {
            for (int i = GameManagerObjectCanvas.objectCanvas.Count - 1; i >= 0; i--)
            {
                var obj = GameManagerObjectCanvas.objectCanvas[i];
                obj.Transform.Position.Y += offset;
            }
        }

        /// <summary>
        /// Реализует проигрышное перемещение камеры при столкновении с монстром.
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
        /// Реализует первичное проигрышное перемещение камеры при глубоком падении за пределы экрана.
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
        /// Реализует вторичное проигрышное перемещение камеры при глубоком падении за пределы экрана.
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

        /// <summary>
        /// Выполняет расчёт и добавление объектов (мячей) в специализированный класс.
        /// </summary>
        private void AddBallList()
        {
            int posY = (int)player.Physics.transform.Position.Y;
            int posX = (int)player.Physics.transform.Position.X;
            Ball ball = new Ball(posX, posY);
            GameManagerObjectCanvas.ball.Add(ball);
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
                        GameState.Score = 0;
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
        

        /// <summary>
        /// Реализиует рестарт игры.
        /// </summary>
        private void RestartGame()
        {

            GameManagerObjectCanvas.AllClearObject();
            GameManagerObjectCanvas.GenerateStartSequence(); ///возможно нужно посмотреть на логику, то есть обнулить логику появления платформ!!!
            player = new Player();
        }

        /// <summary>
        /// Реализует остановку игрового процесса.
        /// </summary>
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
