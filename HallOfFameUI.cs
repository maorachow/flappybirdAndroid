using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HallOfFameUI : UIElement
{

    public string easyScoreText = "";
    public string moderateScoreText = "";
    public string hardScoreText = "";
    public SpriteFont font;
    public Vector2 easyScoreTextPosition;
    public Vector2 moderateScoreTextPosition;
    public Vector2 hardScoreTextPosition;
    public SpriteBatch spriteBatch;

    public HallOfFameUI(SpriteFont font, Vector2 easyScoreTextPosition, Vector2 moderateScoreTextPosition, Vector2 hardScoreTextPosition, SpriteBatch spriteBatch)
    {
        this.font = font;
        this.easyScoreTextPosition = easyScoreTextPosition;
        this.moderateScoreTextPosition = moderateScoreTextPosition;
        this.hardScoreTextPosition = hardScoreTextPosition;
        this.spriteBatch = spriteBatch;
    }

    public string text { get; set; }

    public void Draw()
    {
        Vector2 stringSize = font.MeasureString(easyScoreText.ToString());
        float textScaling = ((float)UIElement.ScreenRect.Height / (float)UIElement.ScreenRectInital.Height) * UIElement.globalUIScaling * 1.6f;
        stringSize *= textScaling;
        Vector2 screenSpacePos = new Vector2(easyScoreTextPosition.X * (float)UIElement.ScreenRect.Width, easyScoreTextPosition.Y * (float)UIElement.ScreenRect.Height);


        Vector2 stringSize2 = font.MeasureString(moderateScoreText.ToString());

        stringSize2 *= textScaling;
        Vector2 screenSpacePos2 = new Vector2(moderateScoreTextPosition.X * (float)UIElement.ScreenRect.Width, moderateScoreTextPosition.Y * (float)UIElement.ScreenRect.Height);


        Vector2 stringSize3 = font.MeasureString(hardScoreText);

        stringSize3 *= textScaling;
        Vector2 screenSpacePos3 = new Vector2(hardScoreTextPosition.X * (float)UIElement.ScreenRect.Width, hardScoreTextPosition.Y * (float)UIElement.ScreenRect.Height);

        spriteBatch.DrawString(font, easyScoreText, screenSpacePos - new Vector2(stringSize.X / 2f, stringSize.Y / 2f), Color.White, 0f, new Vector2(0, 0), textScaling, SpriteEffects.None, 1);
        spriteBatch.DrawString(font, moderateScoreText, screenSpacePos2 - new Vector2(stringSize2.X / 2f, stringSize2.Y / 2f), Color.White, 0f, new Vector2(0, 0), textScaling, SpriteEffects.None, 1);
        spriteBatch.DrawString(font, hardScoreText, screenSpacePos3 - new Vector2(stringSize3.X / 2f, stringSize3.Y / 2f), Color.White, 0f, new Vector2(0, 0), textScaling, SpriteEffects.None, 1);

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
        easyScoreText = "Easy Mode Highscore: " + Gameplay.highScoreEasy;
        moderateScoreText = "Moderate Mode Highscore: " + Gameplay.highScoreModerate;
        hardScoreText = "Hard Mode Highscore: " + Gameplay.highScoreHard;
    }
}
