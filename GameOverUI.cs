using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GameOverUI : UIElement
{
    public string curScoreText = "";
    public string highScoreText = "";
    public SpriteFont font;
    public Vector2 curScoreTextPosition;
    public Vector2 highScoreTextPosition;
    public Vector2 titlePosition;
    public SpriteBatch spriteBatch;

    public GameOverUI(SpriteFont font, Vector2 curScoreTextPosition, Vector2 highScoreTextPosition, Vector2 titlePosition, SpriteBatch spriteBatch)
    {
        this.font = font;
        this.curScoreTextPosition = curScoreTextPosition;
        this.highScoreTextPosition = highScoreTextPosition;
        this.titlePosition = titlePosition;
        this.spriteBatch = spriteBatch;
    }

    public string text { get; set; }

    public void Draw()
    {
        Vector2 stringSize = font.MeasureString(curScoreText.ToString());
        float textScaling = ((float)UIElement.ScreenRect.Height / (float)UIElement.ScreenRectInital.Height) * UIElement.globalUIScaling * 3f;
        stringSize *= textScaling;
        Vector2 screenSpacePos = new Vector2(curScoreTextPosition.X * (float)UIElement.ScreenRect.Width, curScoreTextPosition.Y * (float)UIElement.ScreenRect.Height);


        Vector2 stringSize2 = font.MeasureString(highScoreText.ToString());

        stringSize2 *= textScaling;
        Vector2 screenSpacePos2 = new Vector2(highScoreTextPosition.X * (float)UIElement.ScreenRect.Width, highScoreTextPosition.Y * (float)UIElement.ScreenRect.Height);


        Vector2 stringSize3 = font.MeasureString("Game Over!");

        stringSize3 *= textScaling;
        Vector2 screenSpacePos3 = new Vector2(titlePosition.X * (float)UIElement.ScreenRect.Width, titlePosition.Y * (float)UIElement.ScreenRect.Height);

        spriteBatch.DrawString(font, curScoreText, screenSpacePos - new Vector2(stringSize.X / 2f, stringSize.Y / 2f), Color.White, 0f, new Vector2(0, 0), textScaling, SpriteEffects.None, 1);
        spriteBatch.DrawString(font, highScoreText, screenSpacePos2 - new Vector2(stringSize2.X / 2f, stringSize2.Y / 2f), Color.White, 0f, new Vector2(0, 0), textScaling, SpriteEffects.None, 1);
        spriteBatch.DrawString(font, "Game Over!", screenSpacePos3 - new Vector2(stringSize3.X / 2f, stringSize3.Y / 2f), Color.White, 0f, new Vector2(0, 0), textScaling, SpriteEffects.None, 1);

    }
    public void DrawString(string text)
    {
        Draw();
    }
    public void GetScreenSpaceRect()
    {

    }

    public void OnResize()
    {

    }

    public void Update()
    {
        curScoreText = "Your Score:" + Gameplay.curScore;
        switch (Gameplay.curDifficulty)
        {
            case 0:
                highScoreText = "Highest Score:" + Gameplay.highScoreEasy;
                break;
            case 1:
                highScoreText = "Highest Score:" + Gameplay.highScoreModerate;
                break;
            case 2:
                highScoreText = "Highest Score:" + Gameplay.highScoreHard;
                break;
            default:
                highScoreText = "Highest Score:" + Gameplay.highScoreEasy;
                break;
        }

    }
}
