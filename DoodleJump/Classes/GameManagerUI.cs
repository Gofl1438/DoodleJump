using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    /// <summary>
    /// Менеджер пользовательского интерфейса игры.
    /// </summary>
    public static class GameManagerUI
    {
        public static List<ElementUI> InterfaceElements { get; } = new List<ElementUI>();
        public static List<ElementUI> PauseMenuElements { get; } = new List<ElementUI>();
        public static List<ElementUI> GameOverElements { get; } = new List<ElementUI>();
        public static List<ElementUI> MainMenuElements { get; } = new List<ElementUI>();
        /// <summary>
        /// Инициализирует все UI элементы игры
        /// </summary>
        public static void InitializeAllUI()
        {
            ClearAllElements();
            AddElementInterface();
            AddElementPause();
            AddElementGameOver();
            AddElementStartMenu();
        }
        /// <summary>
        /// Очищает все списки UI элементов
        /// </summary>
        public static void ClearAllElements()
        {
            InterfaceElements.Clear();
            GameOverElements.Clear();
            PauseMenuElements.Clear();
            MainMenuElements.Clear();
        }

        private static void AddElementInterface()
        {
            ElementUI PauseButton = new ElementUI(
                GameConfig.GameUiConfig.PauseButton,
                GameConfig.GameUiConfig.Dimensions.PauseButton,
                GameConfig.GameUiConfig.Positions.PauseButton
                );
            ElementUI Panel = new ElementUI(
                GameConfig.GameUiConfig.Panel,
                GameConfig.GameUiConfig.Dimensions.Panel,
                GameConfig.GameUiConfig.Positions.Panel
                );

            InterfaceElements.Add(Panel);
            InterfaceElements.Add(PauseButton);
        }

        private static void AddElementPause()
        {
            ElementUI ButtonResume = new ElementUI(
                GameConfig.PauseMenuConfig.ButtonResume,
                GameConfig.PauseMenuConfig.Dimensions.ButtonResume,
                GameConfig.PauseMenuConfig.Positions.ButtonResume
                );
            ElementUI BackgroundPause = new ElementUI(
                GameConfig.PauseMenuConfig.BackgroundPause,
                GameConfig.PauseMenuConfig.Dimensions.Background,
                GameConfig.PauseMenuConfig.Positions.Background
                );
            PauseMenuElements.Add(BackgroundPause);
            PauseMenuElements.Add(ButtonResume);
        }
        private static void AddElementGameOver()
        {
            ElementUI ButtonMenu = new ElementUI(
                GameConfig.GameOverConfig.ButtonMenu,
                GameConfig.GameOverConfig.Dimensions.ButtonMenu,
                GameConfig.GameOverConfig.Positions.ButtonMenu
                );
            ElementUI ButtonPlayAgain = new ElementUI(
                GameConfig.GameOverConfig.ButtonPlayAgain,
                GameConfig.GameOverConfig.Dimensions.ButtonPlayAgain,
                GameConfig.GameOverConfig.Positions.ButtonPlayAgain
                );
            ElementUI TitleGameOver = new ElementUI(
                GameConfig.GameOverConfig.TitleGameOver,
                GameConfig.GameOverConfig.Dimensions.TitleGameOver,
                GameConfig.GameOverConfig.Positions.TitleGameOver
                );
            GameOverElements.Add(ButtonPlayAgain);
            GameOverElements.Add(ButtonMenu);
            GameOverElements.Add(TitleGameOver);
        }
        public static void AddElementStartMenu()
        {
            ElementUI TitleDoodleJump = new ElementUI(
                GameConfig.MainMenuConfig.TitleDoodleJump,
                GameConfig.MainMenuConfig.Dimensions.TitleDoodleJump,
                GameConfig.MainMenuConfig.Positions.TitleDoodleJump
                );
            ElementUI ButtonStartPlay = new ElementUI(
                GameConfig.MainMenuConfig.ButtonStartPlay,
                GameConfig.MainMenuConfig.Dimensions.ButtonStartPlay,
                GameConfig.MainMenuConfig.Positions.ButtonStartPlay
                );
            ElementUI BackgroundError = new ElementUI(
                GameConfig.MainMenuConfig.BackgroundError,
                GameConfig.MainMenuConfig.Dimensions.BackgroundError,
                GameConfig.MainMenuConfig.Positions.BackgroundError
                );
            MainMenuElements.Add(TitleDoodleJump);
            MainMenuElements.Add(ButtonStartPlay);
            MainMenuElements.Add(BackgroundError);
        }
    }
}
