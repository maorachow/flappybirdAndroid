using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SpriteRenderer
{
    public List<VertexPositionNormalTexture> vertices;
    public List<DrawableSprite> sprites;
    public Effect spriteEffect;
    public GraphicsDevice device;
    public DynamicVertexBuffer vertexBuffer;
    public Camera2D camera;
    public RenderTarget2D renderTarget;
    public Texture2D textureAtlas;

    public SpriteRenderer(GraphicsDevice device, List<DrawableSprite> sprites, Effect spriteEffect, Camera2D cam, Texture2D atlas)
    {
        this.device = device;
        this.sprites = sprites;
        vertexBuffer = new DynamicVertexBuffer(device, typeof(VertexPositionNormalTexture), (int)2e4, BufferUsage.WriteOnly);
        vertices = new List<VertexPositionNormalTexture>();
        this.spriteEffect = spriteEffect;
        this.camera = cam;
        this.textureAtlas = atlas;
        //   this.spriteBatch = sb;
        //   this.game = game;
    }
    public void Draw()
    {

        vertices.Clear();
        spriteEffect.Parameters["Texture"].SetValue(textureAtlas);

        foreach (var sprite in sprites)
        {
            //    Debug.WriteLine(sprite.centerPosition);
            //    Debug.WriteLine(camera.camPosition);
            if (sprite.isEnabled == true)
            {
                sprite.camera = this.camera;
                sprite.SetupSprite();
                vertices.AddRange(sprite.vertices);
            }

        }
        vertexBuffer.SetData(vertices.ToArray());
        device.SetVertexBuffer(vertexBuffer);
        foreach (var pass in spriteEffect.CurrentTechnique.Passes)
        {
            pass.Apply();
            device.DrawPrimitives(PrimitiveType.TriangleList, 0, vertices.Count);
        }

        /* 

         float aspectRatio=renderTarget.Width/renderTarget.Height;
         if(game.Window.ClientBounds.Width>)
         spriteBatch.Draw(renderTarget,*)*/
    }
}

