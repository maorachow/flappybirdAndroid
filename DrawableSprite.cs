using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//0,1          1,1
//
//
//     center
//
//
//0,0          1,0
//sizeX

public class DrawableSprite
{
    public Camera2D camera;
    public Vector3 centerPosition;
    public float sizeX;
    public float sizeY;
    public List<VertexPositionNormalTexture> vertices;
    public Texture atlas;
    public Vector2 uvCorner;
    public Vector2 uvSize;
    public float rotationZ;
    public Matrix rotationMat;
    public bool isStatic = false;
    public bool isEnabled = true;
    public float depth = 0f;
    public DrawableSprite(Vector3 centerPosition, float sizeX, float sizeY, Texture atlas, Vector2 uvCorner, Vector2 uvSize, SpriteRenderer sr, float depth)
    {
        this.centerPosition = centerPosition;
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        this.atlas = atlas;
        this.uvCorner = uvCorner;
        this.uvSize = uvSize;
        this.vertices = new List<VertexPositionNormalTexture>();
        sr.sprites.Add(this);
        this.depth = depth;
    }
    public Vector3 RotateAngle(float angle, Vector3 inVec, Vector3 o)
    {
        Vector3 b = new Vector3();
        b.X = (inVec.X - o.X) * MathF.Cos(angle) - (inVec.Y - o.Y) * MathF.Sin(angle) + o.X;

        b.Y = (inVec.X - o.X) * MathF.Sin(angle) + (inVec.Y - o.Y) * MathF.Cos(angle) + o.Y;
        return b;
    }
    public void SetupSprite()
    {
        vertices.Clear();
        //  rotationMat =Matrix.CreateFromYawPitchRoll(0,0,this.rotationZ);
        Matrix transMat = Matrix.CreateTranslation(centerPosition);
        VertexPositionNormalTexture vert00 = new VertexPositionNormalTexture();
        VertexPositionNormalTexture vert01 = new VertexPositionNormalTexture();
        VertexPositionNormalTexture vert11 = new VertexPositionNormalTexture();
        VertexPositionNormalTexture vert10 = new VertexPositionNormalTexture();
        vert00.Position = new Vector3(centerPosition.X - sizeX / 2f, centerPosition.Y - sizeY / 2f, 0f);
        vert10.Position = new Vector3(centerPosition.X + sizeX / 2f, centerPosition.Y - sizeY / 2f, 0f);
        vert11.Position = new Vector3(centerPosition.X + sizeX / 2f, centerPosition.Y + sizeY / 2f, 0f);
        vert01.Position = new Vector3(centerPosition.X - sizeX / 2f, centerPosition.Y + sizeY / 2f, 0f);

        vert00.Position = RotateAngle((rotationZ), vert00.Position, centerPosition);
        vert10.Position = RotateAngle((rotationZ), vert10.Position, centerPosition);
        vert11.Position = RotateAngle((rotationZ), vert11.Position, centerPosition);
        vert01.Position = RotateAngle((rotationZ), vert01.Position, centerPosition);

        if (isStatic == false)
        {
            Vector3.Transform(ref vert00.Position, ref camera.viewMatrix, out vert00.Position);
            Vector3.Transform(ref vert10.Position, ref camera.viewMatrix, out vert10.Position);
            Vector3.Transform(ref vert11.Position, ref camera.viewMatrix, out vert11.Position);
            Vector3.Transform(ref vert01.Position, ref camera.viewMatrix, out vert01.Position);
            vert00.Position.Z = depth;
            vert10.Position.Z = depth;
            vert11.Position.Z = depth;
            vert01.Position.Z = depth;
            Vector3.Transform(ref vert00.Position, ref camera.projectionMatrix, out vert00.Position);
            Vector3.Transform(ref vert10.Position, ref camera.projectionMatrix, out vert10.Position);
            Vector3.Transform(ref vert11.Position, ref camera.projectionMatrix, out vert11.Position);
            Vector3.Transform(ref vert01.Position, ref camera.projectionMatrix, out vert01.Position);

        }
        else
        {
            Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, -1f), new Vector3(0, 0, 0), Vector3.Up);
            vert00.Position.Z = depth;
            vert10.Position.Z = depth;
            vert11.Position.Z = depth;
            vert01.Position.Z = depth;
            Vector3.Transform(ref vert00.Position, ref view, out vert00.Position);
            Vector3.Transform(ref vert10.Position, ref view, out vert10.Position);
            Vector3.Transform(ref vert11.Position, ref view, out vert11.Position);
            Vector3.Transform(ref vert01.Position, ref view, out vert01.Position);

            Vector3.Transform(ref vert00.Position, ref camera.projectionMatrix, out vert00.Position);
            Vector3.Transform(ref vert10.Position, ref camera.projectionMatrix, out vert10.Position);
            Vector3.Transform(ref vert11.Position, ref camera.projectionMatrix, out vert11.Position);
            Vector3.Transform(ref vert01.Position, ref camera.projectionMatrix, out vert01.Position);
        }


        vert00.TextureCoordinate = uvCorner;
        vert01.TextureCoordinate = uvCorner + new Vector2(0, uvSize.Y);
        vert11.TextureCoordinate = uvCorner + new Vector2(uvSize.X, uvSize.Y);
        vert10.TextureCoordinate = uvCorner + new Vector2(uvSize.X, 0);
        vertices.Add(vert10);
        vertices.Add(vert01);
        vertices.Add(vert00);
        vertices.Add(vert11);
        vertices.Add(vert01);
        vertices.Add(vert10);
    }

}

