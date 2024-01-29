using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface UIElement
{
    public static Dictionary<string, Texture2D> UITextures = new Dictionary<string, Texture2D>();
    public static Rectangle ScreenRect = new Rectangle(0, 0, 800, 480);
    public static Rectangle ScreenRectInital = new Rectangle(0, 0, 1080, 1920);
    public static float globalUIScaling = 2.5f;
    public void GetScreenSpaceRect();
    public void Draw();
    public void DrawString(string text);
    public void Update();
    public void OnResize();
    public string text { get; set; }
    public static List<UIElement> menuUIs = new List<UIElement>();
    public static List<UIElement> gameOverUIs = new List<UIElement>();
    public static List<UIElement> hallOfFameUIs = new List<UIElement>();
    public static TouchCollection touchCollection;
    public static void GetTouches()
    {
        touchCollection = TouchPanel.GetState();
    }
}

