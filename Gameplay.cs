using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class Gameplay
{
    public Camera2D playerCamera;
    public PlayerBird playerBird;
    public static bool isGameOver = false;
    public static int curScore;
    public static int curDifficulty = 0;//0easy 1moderate 2hard
    public static int highScoreEasy;
    public static int highScoreModerate;
    public static int highScoreHard;
    public static SpriteRenderer gameplayRenderer;
    public static Texture2D textureAltas;
    public static Effect spriteEffect;
    public Ground groundObject;
    public static List<CollidingGameObject> collidingGameObjects;
    public DrawableSprite backgroundSprite;
    public DrawableSprite leftBlank;
    public DrawableSprite rightBlank;
    public Tube tube1;
    public Tube tube2;
    public Tube tube3;
    public GameWindow window;
    public SpriteBatch spriteBatch;
    public ScoreDisplayer curScoreDisplayer;
    public SpriteFont scoreFont;
    public static bool isGameOverEventInvoked = false;
    public MainGame game;
    public static TouchCollection gameTouches;
    public void InitGameplay(MainGame game)
    {

        this.game = game;
        curScore = 0;
        isGameOver = false;
        isGameOverEventInvoked = false;
        scoreFont = game.Content.Load<SpriteFont>("defaultfont");
        spriteBatch = game._spriteBatch;
        collidingGameObjects = new List<CollidingGameObject>();
        spriteEffect = game.Content.Load<Effect>("spriteeffect");
        textureAltas = game.Content.Load<Texture2D>("atlas");
        window = game.Window;
        playerCamera = new Camera2D(new Vector3(0, 0, -1f), (float)window.ClientBounds.Width / (float)window.ClientBounds.Height, 5f);

        gameplayRenderer = new SpriteRenderer(game.GraphicsDevice, new List<DrawableSprite>(), spriteEffect, playerCamera, textureAltas);

        backgroundSprite = new DrawableSprite(new Vector3(0, 0, 0), 5f * (1080f / 1920f), 5f, textureAltas, new Vector2(0, 1020f / 2048f), new Vector2(576f / 2048f, 1024f / 2048f), gameplayRenderer, 0f);
        backgroundSprite.isStatic = true;
        curScoreDisplayer = new ScoreDisplayer(game._spriteBatch, new Vector2(0.5f, 0.1f), scoreFont);
        float tubeDistance;
        switch (curDifficulty)
        {
            case 0:

                tubeDistance = 2f;
                break;
            case 1:
                tubeDistance = 1.6f;
                break;
            case 2:
                tubeDistance = 1.3f;
                break;
            default:
                tubeDistance = 2f;
                break;
        }
        float halfTubeDistance = tubeDistance / 2f;
        tube1 = new Tube
        {
            position = new Vector3(-2.5f, 0f, 0f),
            boundingBoxUp = new BoundingBox(new Vector3(-3f, halfTubeDistance, -1f), new Vector3(-2f, halfTubeDistance + 6f, 1f)),
            boundingBoxDown = new BoundingBox(new Vector3(-3f, -halfTubeDistance - 6f, -1f), new Vector3(-2f, -halfTubeDistance, 1f)),
            //, 
            tubeSpriteUp = new DrawableSprite(new Vector3(-2.5f, halfTubeDistance + 3f, 0f), 1f, 6f, textureAltas, new Vector2(224f / 2048f, 112f / 2048f), new Vector2(104f / 2048f, 640f / 2048f), gameplayRenderer, -0.6f),
            tubeSpriteDown = new DrawableSprite(new Vector3(-2.5f, -halfTubeDistance - 3f, 0f), 1f, 6f, textureAltas, new Vector2(335f / 2048f, 112f / 2048f), new Vector2(104f / 2048f, 640 / 2048f), gameplayRenderer, -0.6f),
            linkedScoreAdder = new ScoreAdder { boundingBox = new BoundingBox(new Vector3(-2.75f, -1f, -1f), new Vector3(-2.25f, 1f, 1f)), gameplay = this }
        };
        tube2 = new Tube
        {
            position = new Vector3(-5.5f, 0f, 0f),
            boundingBoxUp = new BoundingBox(new Vector3(-6f, halfTubeDistance, -1f), new Vector3(-5f, halfTubeDistance + 6f, 1f)),
            boundingBoxDown = new BoundingBox(new Vector3(-6f, -halfTubeDistance - 6f, -1f), new Vector3(-5f, -halfTubeDistance, 1f)),
            //, 
            tubeSpriteUp = new DrawableSprite(new Vector3(-5.5f, halfTubeDistance + 3f, 0f), 1f, 6f, textureAltas, new Vector2(224f / 2048f, 112f / 2048f), new Vector2(104f / 2048f, 640f / 2048f), gameplayRenderer, -0.6f),
            tubeSpriteDown = new DrawableSprite(new Vector3(-5.5f, -halfTubeDistance - 3f, 0f), 1f, 6f, textureAltas, new Vector2(335f / 2048f, 112f / 2048f), new Vector2(104f / 2048f, 640 / 2048f), gameplayRenderer, -0.6f),
            linkedScoreAdder = new ScoreAdder { boundingBox = new BoundingBox(new Vector3(-5.75f, -1f, -1f), new Vector3(-5.25f, 1f, 1f)), gameplay = this }
        };

        tube3 = new Tube
        {
            position = new Vector3(-8.5f, 0f, 0f),
            boundingBoxUp = new BoundingBox(new Vector3(-9f, halfTubeDistance, -1f), new Vector3(-8f, halfTubeDistance + 6f, 1f)),
            boundingBoxDown = new BoundingBox(new Vector3(-9f, -halfTubeDistance - 6f, -1f), new Vector3(-8f, -halfTubeDistance, 1f)),
            //, 
            tubeSpriteUp = new DrawableSprite(new Vector3(-8.5f, halfTubeDistance + 3f, 0f), 1f, 6f, textureAltas, new Vector2(224f / 2048f, 112f / 2048f), new Vector2(104f / 2048f, 640f / 2048f), gameplayRenderer, -0.6f),
            tubeSpriteDown = new DrawableSprite(new Vector3(-8.5f, -halfTubeDistance - 3f, 0f), 1f, 6f, textureAltas, new Vector2(335f / 2048f, 112f / 2048f), new Vector2(104f / 2048f, 640 / 2048f), gameplayRenderer, -0.6f),
            linkedScoreAdder = new ScoreAdder { boundingBox = new BoundingBox(new Vector3(-8.75f, -1f, -1f), new Vector3(-8.25f, 1f, 1f)), gameplay = this }
        };


        groundObject = new Ground
        {
            boundingBox = new BoundingBox(new Vector3(-5f, -2.5f, -1f), new Vector3(5f, -1.5f, 1f)),
            sprite = new DrawableSprite(new Vector3(0f, -2f, 0f), 6f, 1f, textureAltas, new Vector2(352f / 1024f, 0f), new Vector2(671f / 1024f, 112f / 1024f), gameplayRenderer, -0.5f)
        };
        playerBird = new PlayerBird(new Vector3(0, 0, 0), new DrawableSprite(new Vector3(0, 0, 0), 0.4f * (34f / 24f), 0.4f, textureAltas, new Vector2(0, 0), new Vector2(34f / 1024f, 24f / 1024f), gameplayRenderer, -0.1f));
        switch (curDifficulty)
        {
            case 0:

                playerBird.flySpeed = 1f;
                break;
            case 1:
                playerBird.flySpeed = 1.2f;
                break;
            case 2:
                playerBird.flySpeed = 1.3f;
                break;
            default:
                playerBird.flySpeed = 1f;
                break;
        }
        leftBlank = new DrawableSprite(new Vector3((0f - 5f * (1080f / 1920f)) / 2f - 5f, 0f, 0f), 10f, 10f, textureAltas, new Vector2(688f / 2048f, 0f), new Vector2(16 / 2048f, 16 / 2048f), gameplayRenderer, -0.01f);
        rightBlank = new DrawableSprite(new Vector3((0f + 5f * (1080f / 1920f)) / 2f + 5f, 0f, 0f), 10f, 10f, textureAltas, new Vector2(688f / 2048f, 0f), new Vector2(16 / 2048f, 16 / 2048f), gameplayRenderer, -0.01f);
        leftBlank.isStatic = true;
        rightBlank.isStatic = true;
        collidingGameObjects.Add(groundObject);
        collidingGameObjects.Add(tube1);
        collidingGameObjects.Add(tube1.linkedScoreAdder);
        collidingGameObjects.Add(tube2);
        collidingGameObjects.Add(tube2.linkedScoreAdder);
        collidingGameObjects.Add(tube3);
        collidingGameObjects.Add(tube3.linkedScoreAdder);
        game.status = GameStatus.Started;
    }
    public void GameOverEvent()
    {
        switch (curDifficulty)
        {
            case 0:
                if (curScore > highScoreEasy) { highScoreEasy = curScore; }
                break;
            case 1:
                if (curScore > highScoreModerate) { highScoreModerate = curScore; }
                break;
            case 2:
                if (curScore > highScoreHard) { highScoreHard = curScore; }
                break;
            default:
                if (curScore > highScoreEasy) { highScoreEasy = curScore; }
                break;
        }

        game.OnResize(null, null);
        GameData.SaveGameData();
        game.status = GameStatus.GameOver;
        Debug.WriteLine("Game over!");
    }
    public void Update(float deltaTime)
    {
        if (isGameOver)
        {
            if (isGameOverEventInvoked == false)
            {
                GameOverEvent();
                isGameOverEventInvoked = true;
            }
            return;
        }

        gameTouches = TouchPanel.GetState();
        tube1.UpdateTube(playerBird.position);
        tube2.UpdateTube(playerBird.position);
        tube3.UpdateTube(playerBird.position);

        playerBird.Update(deltaTime);


        playerCamera.camPosition = new Vector3(playerBird.position.X - 0.5f, 0, 0) + new Vector3(0, 0, -1f);
        playerCamera.UpdateMatrix();
        if (MathF.Abs(groundObject.position.X - playerBird.position.X) > 1f)
        {
            groundObject.MoveDir(new Vector3(-1f, 0f, 0f));
        }
        //  Debug.WriteLine(playerCamera.camPosition+" "+groundObject.position);
    }
    public void Draw()
    {

        gameplayRenderer.Draw();
        if (!isGameOver)
        {
            curScoreDisplayer.Draw();
        }

    }
}
