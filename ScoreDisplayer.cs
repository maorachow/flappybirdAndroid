using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ScoreDisplayer
{
    public SpriteBatch spriteBatch;
    public SpriteFont font;
    public Vector2 position;

    public ScoreDisplayer(SpriteBatch spriteBatch, Vector2 position, SpriteFont sf)
    {
        this.spriteBatch = spriteBatch;
        this.position = position;
        this.font = sf;

    }
    public void Draw()
    {

        Vector2 stringSize = font.MeasureString(Gameplay.curScore.ToString());
        float textScaling = ((float)UIElement.ScreenRect.Height / (float)UIElement.ScreenRectInital.Height) * UIElement.globalUIScaling * 3f;
        stringSize *= textScaling;
        Vector2 screenSpacePos = new Vector2(position.X * (float)UIElement.ScreenRect.Width, position.Y * (float)UIElement.ScreenRect.Height);
        spriteBatch.Begin(samplerState: SamplerState.PointWrap);
        spriteBatch.DrawString(font, Gameplay.curScore.ToString(), screenSpacePos - new Vector2(stringSize.X / 2f, stringSize.Y / 2f), Color.White, 0f, new Vector2(0, 0), textScaling, SpriteEffects.None, 1);
        spriteBatch.End();
    }
}

