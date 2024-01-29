using flappybird;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;

public enum GameStatus
{
    Menu,
    Started,
    GameOver,
    HallOfFame
}
public class MainGame : Game
{
    private GraphicsDeviceManager _graphics;
    public SpriteBatch _spriteBatch;
    public Gameplay gameplay;
    public GameStatus status;
    public MainGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.AllowUserResizing = true;
        Window.ClientSizeChanged += OnResize;
        this.IsFixedTimeStep = false;
    }
    public void OnResize(object sender, EventArgs e)
    {
        switch (status)
        {

            case GameStatus.Menu:
                UIElement.ScreenRect = Window.ClientBounds;
                foreach (UIElement element in UIElement.menuUIs)
                {
                    element.OnResize();
                }
                foreach (UIElement element in UIElement.hallOfFameUIs)
                {
                    element.OnResize();
                }
                break;

            case GameStatus.Started:
                gameplay.playerCamera.aspectRatio = (float)Window.ClientBounds.Width / (float)Window.ClientBounds.Height;

                gameplay.playerCamera.UpdateMatrix();
                UIElement.ScreenRect = Window.ClientBounds;
                foreach (UIElement element in UIElement.gameOverUIs)
                {
                    element.OnResize();
                }
                foreach (UIElement element in UIElement.menuUIs)
                {
                    element.OnResize();
                }
                break;

            case GameStatus.GameOver:
                gameplay.playerCamera.aspectRatio = (float)Window.ClientBounds.Width / (float)Window.ClientBounds.Height;

                gameplay.playerCamera.UpdateMatrix();
                UIElement.ScreenRect = Window.ClientBounds;
                foreach (UIElement element in UIElement.gameOverUIs)
                {
                    element.OnResize();
                }
                foreach (UIElement element in UIElement.menuUIs)
                {
                    element.OnResize();
                }
                break;


            case GameStatus.HallOfFame:
                UIElement.ScreenRect = Window.ClientBounds;
                foreach (UIElement element in UIElement.menuUIs)
                {
                    element.OnResize();
                }
                foreach (UIElement element in UIElement.gameOverUIs)
                {
                    element.OnResize();
                }
                foreach (UIElement element in UIElement.hallOfFameUIs)
                {
                    element.OnResize();
                }
                break;
        }

    }
    protected override void Initialize()
    {
        GameData.ReadJson();
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        gameplay = new Gameplay();
        //    gameplay.InitGameplay(this);
        UIUtility.InitUI(this);
        AudioManager.Init(this);
        base.Initialize();
    }

    protected override void LoadContent()
    {



    }
    public void ReturnToMenu()
    {
        status = GameStatus.Menu;

    }
    public void GoToHallOfFame()
    {
        status = GameStatus.HallOfFame;

    }
    public void ChangeDifficulty()
    {
        Gameplay.curDifficulty++;
        if (Gameplay.curDifficulty == 3)
        {
            Gameplay.curDifficulty = 0;
        }
        switch (Gameplay.curDifficulty)
        {
            case 0:
                UIElement.menuUIs[4].text = "Easy";
                break;
            case 1:
                UIElement.menuUIs[4].text = "Moderate";
                break;
            case 2:
                UIElement.menuUIs[4].text = "Hard";
                break;
            default:
                UIElement.menuUIs[4].text = "Default";
                break;

        }

    }
    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        if (!IsActive)
        {
            return;
        }
        switch (status)
        {
            case GameStatus.Menu:
                UIElement.GetTouches();
                foreach (var el in UIElement.menuUIs)
                {
                    el.Update();
                }
                break;
            case GameStatus.Started:

                gameplay.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

                break;
            case GameStatus.GameOver:
                UIElement.GetTouches();
                foreach (var el in UIElement.gameOverUIs)
                {
                    el.Update();
                }

                break;
            case GameStatus.HallOfFame:
                UIElement.GetTouches();
                foreach (var el in UIElement.hallOfFameUIs)
                {
                    el.Update();
                }
                break;
        }




        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        switch (status)
        {
            case GameStatus.Menu:
                _spriteBatch.Begin(samplerState: SamplerState.PointWrap);
                foreach (var el in UIElement.menuUIs)
                {
                    el.DrawString(el.text);
                }
                _spriteBatch.End();
                break;
            case GameStatus.Started:
                GraphicsDevice.BlendState = BlendState.AlphaBlend;

                gameplay.Draw();
                break;
            case GameStatus.GameOver:
                GraphicsDevice.BlendState = BlendState.AlphaBlend;

                gameplay.Draw();
                _spriteBatch.Begin(samplerState: SamplerState.PointWrap);
                foreach (var el in UIElement.gameOverUIs)
                {
                    el.DrawString(el.text);
                }
                _spriteBatch.End();
                break;

            case GameStatus.HallOfFame:
                _spriteBatch.Begin(samplerState: SamplerState.PointWrap);
                foreach (var el in UIElement.hallOfFameUIs)
                {
                    el.DrawString(el.text);
                }
                _spriteBatch.End();
                break;
        }


        base.Draw(gameTime);
    }
}
