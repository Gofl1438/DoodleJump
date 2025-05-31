using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    public static class GameManagerUI
    {
        public static List<ElementUI> elementInterface = new List<ElementUI>();
        public static List<ElementUI> elementPause = new List<ElementUI>();
        public static List<ElementUI> elementGameOver = new List<ElementUI>();
        public static List<ElementUI> elementStartMenu = new List<ElementUI>();
        public static void AppendElementInterface()
        {
            ClearElementInterface();
            AddElementInterface();
            AddElementPause();
            AddElementGameOver();
            AddElementStartMenu();
        }

        public static void ClearElementInterface()
        {
            elementInterface.Clear();
            elementGameOver.Clear();
            elementPause.Clear();
            elementStartMenu.Clear();
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

            elementInterface.Add(Panel);
            elementInterface.Add(PauseButton);
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
            elementPause.Add(BackgroundPause);
            elementPause.Add(ButtonResume);
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
            elementGameOver.Add(ButtonPlayAgain);
            elementGameOver.Add(ButtonMenu);
            elementGameOver.Add(TitleGameOver);
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
            elementStartMenu.Add(TitleDoodleJump);
            elementStartMenu.Add(ButtonStartPlay);
            elementStartMenu.Add(BackgroundError);
        }
    }
}
