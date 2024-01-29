using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UIImage : UIElement
{
    public Rectangle ImageRect;


    public Vector2 element00Pos;
    public Vector2 element01Pos;
    public Vector2 element11Pos;
    public Vector2 element10Pos;
    public Vector2 centerPos;
    public string text { get; set; }
    SpriteBatch spriteBatch;
    public Texture2D texture;

    public GameWindow window;
    public UIImage(Vector2 position, float width, float height, Texture2D tex, SpriteBatch sb)
    {
        element00Pos = position;
        element10Pos = new Vector2(position.X + width, position.Y);
        element11Pos = new Vector2(position.X + width, position.Y + height);
        element01Pos = new Vector2(position.X, position.Y + height);
        centerPos = new Vector2(position.X + width / 2f, position.Y + width / 2f);
        this.texture = tex;


        this.spriteBatch = sb;
        OnResize();
    }
    public void OnResize()
    {
        GetScreenSpaceRect();
    }
    public void DrawString(string text)
    {
        //   this.text = text;
        //     text = text == null ? " " : text;

        spriteBatch.Draw(texture, ImageRect, Color.White);
    }
    public void Update() { }
    public void Draw()
    {
        DrawString(null);
    }
    public void GetScreenSpaceRect()
    {
        //  Vector2 transformedP00 = new Vector2((element00Pos.X * UIElement.ScreenRect.Width), (element00Pos.Y * UIElement.ScreenRect.Height));
        Vector2 transformedCenter = new Vector2((centerPos.X * UIElement.ScreenRect.Width), (centerPos.Y * UIElement.ScreenRect.Height));
        float imageScaling = ((float)UIElement.ScreenRect.Height / (float)UIElement.ScreenRectInital.Height) * UIElement.globalUIScaling;
        float transformedImageWidth = texture.Width * imageScaling;
        float transformedImageHeight = texture.Height * imageScaling;
        //   float width = (element10Pos - element00Pos).X * UIElement.ScreenRect.Width;

        float height = (element01Pos - element00Pos).Y * UIElement.ScreenRect.Height;
        this.ImageRect = new Rectangle((int)(transformedCenter.X - transformedImageWidth / 2f), (int)(transformedCenter.Y - transformedImageHeight / 2f), (int)(transformedImageWidth), (int)transformedImageHeight);


    }
}

