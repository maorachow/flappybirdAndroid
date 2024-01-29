using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


public class PlayerBird
{

    public DrawableSprite sprite1;


    public bool isDied = false;
    public Vector3 position;
    public Vector2 moveVec;
    public BoundingBox boundingBox;
    public float timeSurvived = 0f;
    public float flySpeed = 1f;
    public PlayerBird(Vector3 pos, DrawableSprite sprite1)
    {
        this.position = pos;

        this.boundingBox = new BoundingBox(new Vector3(-0.2f, -0.2f, -0.2f), new Vector3(0.2f, 0.2f, 0.2f));
        this.sprite1 = sprite1;

        // this.sprite = sprite1;
    }
    public void HandleInput(MouseState state, MouseState preState)
    {
        bool isTapped = false;
        foreach (var tc in Gameplay.gameTouches)
        {
            if (tc.State == Microsoft.Xna.Framework.Input.Touch.TouchLocationState.Pressed)
            {
                isTapped = true;
            }
        }
        if ((state.LeftButton == ButtonState.Pressed && preState.LeftButton == ButtonState.Released) || isTapped)
        {
            AudioManager.PlaySound("wing");
            moveVec = new Vector2(1f, 3f);
        }
    }
    public MouseState preState;
    public void Update(float deltaTime)
    {
        if (!isDied)
        {
            MouseState state = Mouse.GetState();
            HandleInput(state, preState);
            preState = state;
            timeSurvived += deltaTime;
            switch ((int)((timeSurvived * 4f) % 3))
            {
                case 0:

                    sprite1.uvCorner = new Vector2(0, 0);

                    break;
                case 1:
                    sprite1.uvCorner = new Vector2(34f / 1024f, 0);

                    break;
                case 2:
                    sprite1.uvCorner = new Vector2(68f / 1024f, 0);

                    break;
            }

            moveVec.Y += deltaTime * -5.8f;
            moveVec.X = -flySpeed;
            float rotValue = MathF.Atan(MathF.Abs(moveVec.Y) / MathF.Abs(moveVec.X)) - MathHelper.ToRadians(35f);
            //   Debug.WriteLine(MathHelper.ToDegrees(rotValue));
            sprite1.rotationZ = rotValue;

            Vector3 finalMoveDir = new Vector3(moveVec.X * deltaTime, moveVec.Y * deltaTime, 0);
            position += finalMoveDir;
            boundingBox.Min += finalMoveDir;
            boundingBox.Max += finalMoveDir;
            sprite1.centerPosition = position;

            //   Debug.WriteLine(sprite.centerPosition);
            foreach (var cl in Gameplay.collidingGameObjects)
            {
                if (cl.isColliding(this.boundingBox))
                {
                    cl.CollidingEvent(this);
                }
            }
        }
        else
        {
            moveVec.Y = -3f;
            moveVec.X = 0f;
            float rotValue = -80f;
            sprite1.rotationZ = rotValue;
            Vector3 finalMoveDir = new Vector3(moveVec.X * deltaTime, moveVec.Y * deltaTime, 0);
            position += finalMoveDir;
            boundingBox.Min += finalMoveDir;
            boundingBox.Max += finalMoveDir;
            sprite1.centerPosition = position;
            foreach (var cl in Gameplay.collidingGameObjects)
            {
                if (cl.isColliding(this.boundingBox))
                {
                    cl.CollidingEvent(this);
                }
            }
        }

    }
}

