using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CollidingGameObject
{
    public Vector3 position;
    //     public BoundingBox boundingBox;
    public virtual bool isColliding(BoundingBox box) { return false; }
    public virtual void CollidingEvent(PlayerBird bird) { }
}


public class Tube : CollidingGameObject
{
    public DrawableSprite tubeSpriteUp;
    public DrawableSprite tubeSpriteDown;
    public BoundingBox boundingBoxUp;
    public BoundingBox boundingBoxDown;
    public ScoreAdder linkedScoreAdder;
    public void UpdateTube(Vector3 playerPos)
    {
        // Debug.WriteLine(position);
        if (playerPos.X - position.X < -4.5f)
        {
            float randomCenterY = new Random().NextSingle() * 1.5f;
            float YPosOffset = randomCenterY - position.Y;

            linkedScoreAdder.isCollided = false;
            MoveDir(new Vector3(-9f, YPosOffset, 0f));
        }
    }
    public override bool isColliding(BoundingBox box)
    {
        return box.Intersects(boundingBoxUp) || box.Intersects(boundingBoxDown);
    }
    public override void CollidingEvent(PlayerBird bird)
    {
        if (bird.isDied == false)
        {
            AudioManager.PlaySound("hit");
            bird.isDied = true;
        }

    }
    public void MoveDir(Vector3 dir)
    {
        boundingBoxUp.Min += dir;
        boundingBoxUp.Max += dir;
        boundingBoxDown.Max += dir;
        boundingBoxDown.Min += dir;
        tubeSpriteUp.centerPosition += dir;
        tubeSpriteDown.centerPosition += dir;
        position += dir;

        linkedScoreAdder.MoveDir(dir);
    }
}



public class ScoreAdder : CollidingGameObject
{
    public BoundingBox boundingBox;
    public bool isCollided = false;
    public Gameplay gameplay;
    public override bool isColliding(BoundingBox box)
    {
        return box.Intersects(boundingBox);
    }
    public override void CollidingEvent(PlayerBird bird)
    {
        if (isCollided == false)
        {
            Gameplay.curScore++;
            Debug.WriteLine("collideScoreAdder");
            AudioManager.PlaySound("point");
            isCollided = true;
        }

    }
    public void MoveDir(Vector3 dir)
    {
        boundingBox.Min += dir;
        boundingBox.Max += dir;

        position += dir;
    }
}
public class Ground : CollidingGameObject
{
    public DrawableSprite sprite;
    public BoundingBox boundingBox;
    public override void CollidingEvent(PlayerBird bird)
    {
        AudioManager.PlaySound("die");
        Gameplay.isGameOver = true;
        //   Debug.WriteLine("collideGround");
    }
    public override bool isColliding(BoundingBox box)
    {
        return box.Intersects(boundingBox);
    }
    public void MoveDir(Vector3 dir)
    {
        boundingBox.Min += dir;
        boundingBox.Max += dir;
        sprite.centerPosition += dir;
        position += dir;
    }
}
