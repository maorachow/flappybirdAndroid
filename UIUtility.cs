using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flappybird
{
    public class UIUtility
    {

        // public MainGame game;
        public static void InitUI(MainGame game)
        {
            SpriteFont sf = game.Content.Load<SpriteFont>("defaultfont");

            UIElement.UITextures = new Dictionary<string, Microsoft.Xna.Framework.Graphics.Texture2D>
            {
                {"startbutton", game.Content.Load<Texture2D>("startbutton")},
                {"title", game.Content.Load<Texture2D>("title")},
                {"menubutton",game.Content.Load<Texture2D>("menubutton") },
                {"highsocrebutton",game.Content.Load<Texture2D>("highscorebutton") },
                {"menubackground",game.Content.Load<Texture2D>("menubackground") },
                {"changedifficultybutton",game.Content.Load<Texture2D>("difficultyselectionbutton") }
            };
            UIElement.menuUIs = new List<UIElement>
            {
                new UIImage(new Vector2(0.4f,0.4f),0.2f,0.2f,UIElement.UITextures["menubackground"],game._spriteBatch),
               new UIImage(new Vector2(0.3f,0.05f),0.4f,0.2f,UIElement.UITextures["title"],game._spriteBatch),
               new UIButton(new Vector2(0.3f, 0.3f), 0.4f, 0.2f, UIElement.UITextures["startbutton"],new Vector2(0.4f,0.55f),sf,game._spriteBatch,game.Window, (()=>game.gameplay.InitGameplay(game)) ," "),
               new UIButton(new Vector2(0.3f, 0.5f), 0.4f, 0.2f, UIElement.UITextures["highsocrebutton"],new Vector2(0.4f,0.55f),sf,game._spriteBatch,game.Window, (()=>game.GoToHallOfFame()) ," "),
               new UIButton(new Vector2(0.3f, 0.7f), 0.4f, 0.2f, UIElement.UITextures["changedifficultybutton"],new Vector2(0.4f,0.55f),sf,game._spriteBatch,game.Window, (()=>game.ChangeDifficulty()) ,"Easy"),
            };
            UIElement.gameOverUIs = new List<UIElement>
            {
                new GameOverUI(sf,new Vector2(0.5f,0.2f),new Vector2(0.5f,0.3f),new Vector2(0.5f,0.1f),game._spriteBatch),
                 new UIButton(new Vector2(0.3f, 0.5f), 0.4f, 0.2f, UIElement.UITextures["menubutton"],new Vector2(0.4f,0.55f),sf,game._spriteBatch,game.Window, (()=>game.ReturnToMenu()) ," ")
                };

            UIElement.hallOfFameUIs = new List<UIElement>
            {
                 new UIImage(new Vector2(0.4f,0.4f),0.2f,0.2f,UIElement.UITextures["menubackground"],game._spriteBatch),
                new HallOfFameUI(sf,new Vector2(0.5f,0.2f),new Vector2(0.5f,0.3f),new Vector2(0.5f,0.4f),game._spriteBatch),
                new UIButton(new Vector2(0.3f, 0.5f), 0.4f, 0.2f, UIElement.UITextures["menubutton"],new Vector2(0.4f,0.55f),sf,game._spriteBatch,game.Window, (()=>game.ReturnToMenu()) ," ")
             };
            game.status = GameStatus.Menu;
        }
    }
}
