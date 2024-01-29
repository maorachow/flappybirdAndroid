using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct Vector2Int : IEquatable<Vector2Int>
{

    public int x;

    public int y;

    public Vector2Int(int a, int b)
    {
        x = a;
        y = b;
    }
    public float magnitude { get { return MathF.Sqrt((float)(x * x + y * y)); } }

    // Returns the squared length of this vector (RO).

    public int sqrMagnitude { get { return x * x + y * y; } }
    public override bool Equals(object other)
    {
        if (!(other is Vector2Int)) return false;

        return Equals((Vector2Int)other);
    }
    public bool Equals(Vector2Int other)
    {
        return x == other.x && y == other.y;
    }

    public override int GetHashCode()
    {
        return x.GetHashCode() ^ (y.GetHashCode() << 2);
    }


    public static Vector2Int operator -(Vector2Int v)
    {
        return new Vector2Int(-v.x, -v.y);
    }


    public static Vector2Int operator +(Vector2Int a, Vector2Int b)
    {
        return new Vector2Int(a.x + b.x, a.y + b.y);
    }

    public static Vector2Int operator -(Vector2Int a, Vector2Int b)
    {
        return new Vector2Int(a.x - b.x, a.y - b.y);
    }


    public static Vector2Int operator *(Vector2Int a, Vector2Int b)
    {
        return new Vector2Int(a.x * b.x, a.y * b.y);
    }


    public static Vector2Int operator *(int a, Vector2Int b)
    {
        return new Vector2Int(a * b.x, a * b.y);
    }


    public static Vector2Int operator *(Vector2Int a, int b)
    {
        return new Vector2Int(a.x * b, a.y * b);
    }


    public static Vector2Int operator /(Vector2Int a, int b)
    {
        return new Vector2Int(a.x / b, a.y / b);
    }


    public static bool operator ==(Vector2Int lhs, Vector2Int rhs)
    {
        return lhs.x == rhs.x && lhs.y == rhs.y;
    }

    public static bool operator !=(Vector2Int lhs, Vector2Int rhs)
    {
        return !(lhs == rhs);
    }

}

/*
 0,0               1,0





0,1                1,1
 */
public class UIButton : UIElement
{
    public Rectangle ButtonRect;
    public Action ButtonAction;
    public Vector2Int textPixelPos;
    public Vector2 textPos;
    public Vector2 textWH;
    public float textHeight;
    public Vector2 element00Pos;
    public Vector2 element01Pos;
    public Vector2 element11Pos;
    public Vector2 element10Pos;
    public Vector2 centerPos;
    // public string text="123";
    SpriteBatch spriteBatch;
    public Texture2D texture;
    public SpriteFont font;
    public GameWindow window;
    public string text { get; set; }
    public UIButton(Vector2 position, float width, float height, Texture2D tex, Vector2 tPos, SpriteFont font, SpriteBatch sb, GameWindow window, Action action, string text)
    {
        element00Pos = position;
        element10Pos = new Vector2(position.X + width, position.Y);
        element11Pos = new Vector2(position.X + width, position.Y + height);
        element01Pos = new Vector2(position.X, position.Y + height);
        centerPos = new Vector2(position.X + width / 2f, position.Y + height / 2f);
        //    Debug.WriteLine(element00Pos + " " + element10Pos + " " + element11Pos + " " + element01Pos);
        this.texture = tex;
        this.textPos = tPos;
        this.font = font;

        this.spriteBatch = sb;
        this.window = window;
        this.ButtonAction = action;
        this.text = text;
        OnResize();
    }
    public void Draw()
    {
        DrawString(null);
    }

    public void DrawString(string text)
    {
        this.text = text;
        text = text == null ? " " : text;
        // ButtonRect.Center;

        spriteBatch.Draw(texture, ButtonRect, Color.White);
        textHeight = (element01Pos - element00Pos).Y;

        this.textPixelPos = new Vector2Int(ButtonRect.Center.X, ButtonRect.Center.Y);
        Vector2 textSize = font.MeasureString(text) / 2f;
        float textSizeScaling = ((float)UIElement.ScreenRect.Height / (float)UIElement.ScreenRectInital.Height) * UIElement.globalUIScaling * 2f;
        textSize *= textSizeScaling;
        //   Debug.WriteLine(textSize/2f);
        // textSize.Y = 0;
        // spriteBatch.DrawString(font, text, new Vector2(textPixelPos.x,textPixelPos.y), Color.White);
        spriteBatch.DrawString(font, text, new Vector2(textPixelPos.x - textSize.X, textPixelPos.y - textSize.Y), Color.White, 0f, new Vector2(0f, 0f), textSizeScaling, SpriteEffects.None, 1);
    }

    public void OnResize()
    {
        this.GetScreenSpaceRect();
    }
    MouseState mouseState;
    MouseState lastMouseState;
    //   TouchCollection touches;
    //  TouchCollection lastTouches;
    public bool isHovered
    {
        get
        {
            //     Debug.WriteLine(ButtonRect.
            //     X+" "+ ButtonRect.Y + " "+ ButtonRect.Width + " "+ ButtonRect.Height);
            //     Debug.WriteLine(UIElement.ScreenRect.X + " " + UIElement.ScreenRect.Y + " " + UIElement.ScreenRect.Width + " " + UIElement.ScreenRect.Height);
            foreach (var tc in UIElement.touchCollection)
            {
                //   Debug.WriteLine(tc.Position);
                if (this.ButtonRect.Contains(tc.Position))
                {
                    //       Debug.WriteLine("hovered");
                    return true;
                }
            }
            if (this.ButtonRect.Contains(new Vector2(mouseState.X, mouseState.Y)))
            {
                return true;

            }
            else
            {
                return false;
            }
        }

    }
    public UIButton() { }
    public void Update()
    {
        mouseState = Mouse.GetState();

        bool isTouched = false;
        foreach (var tc in UIElement.touchCollection)
        {

            if (tc.State == TouchLocationState.Released && isHovered)
            {
                isTouched = true;
                //   Debug.WriteLine("touched");
            }
        }
        if (mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released || isTouched)
        {
            //Debug.WriteLine("pressed");
            ButtonAction();
        }


        lastMouseState = mouseState;
    }
    public void GetScreenSpaceRect()
    {
        //      Debug.WriteLine(element00Pos + " " + element01Pos + " " + element10Pos + " " + element11Pos);
        /*   Vector2 transformedP00 = new Vector2((element00Pos.X * UIElement.ScreenRect.Width) , (element00Pos.Y * UIElement.ScreenRect.Height));
           float width=(element10Pos - element00Pos).X * UIElement.ScreenRect.Width;
           float height=(element01Pos - element00Pos).Y * UIElement.ScreenRect.Height;
           this.ButtonRect = new Rectangle((int)transformedP00.X, (int)transformedP00.Y, (int)width, (int)height);*/
        Vector2 transformedCenter = new Vector2((centerPos.X * UIElement.ScreenRect.Width), (centerPos.Y * UIElement.ScreenRect.Height));
        float imageScaling = ((float)UIElement.ScreenRect.Height / (float)UIElement.ScreenRectInital.Height) * UIElement.globalUIScaling;
        float transformedImageWidth = texture.Width * imageScaling;
        float transformedImageHeight = texture.Height * imageScaling;
        //   float width = (element10Pos - element00Pos).X * UIElement.ScreenRect.Width;


        this.ButtonRect = new Rectangle((int)(transformedCenter.X - transformedImageWidth / 2f), (int)(transformedCenter.Y - transformedImageHeight / 2f), (int)(transformedImageWidth), (int)transformedImageHeight);
        //   Debug.WriteLine(ButtonRect.X + " " + ButtonRect.Y + " " + ButtonRect.Width + " " + ButtonRect.Height);

        //      this.textPixelPos = new Vector2Int((int)(textPos.X * UIElement.ScreenRect.Width), (int)(textPos.Y * UIElement.ScreenRect.Height));
        //   Debug.WriteLine(this.textPixelPos.x+" "+this.textPixelPos.y);
        //      Debug.WriteLine(this.ButtonRect );
    }
}
