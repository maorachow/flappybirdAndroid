using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;


public class Camera2D
{
    public Vector3 camPosition;
    public Matrix viewMatrix;
    public Matrix projectionMatrix;
    public float sizeX;
    public float sizeY;
    public float aspectRatio;
    public float camViewSize = 20f;
    public Camera2D(Vector3 camPosition, float aspectRatio, float camSize)
    {
        this.camPosition = camPosition;
        this.viewMatrix = Matrix.CreateLookAt(camPosition, camPosition + new Vector3(0, 0, 1), Vector3.Up);

        this.aspectRatio = aspectRatio;
        this.camViewSize = camSize;
        this.projectionMatrix = Matrix.CreateOrthographic(aspectRatio * camViewSize, 1 * camViewSize, 0.1f, 100f);

    }
    public void UpdateMatrix()
    {
        viewMatrix = Matrix.CreateLookAt(camPosition, camPosition + new Vector3(0, 0, 1), Vector3.Up);
        //   aspectRatio = sizeX / sizeY;
        this.projectionMatrix = Matrix.CreateOrthographic(aspectRatio * camViewSize, 1 * camViewSize, 0.1f, 100f);
    }
    public Matrix GetViewMatrix()
    {
        viewMatrix = Matrix.CreateLookAt(camPosition, camPosition + new Vector3(0, 0, 1), Vector3.Up);
        return viewMatrix;
    }
    public void SetSize(float camViewSize)
    {
        this.camViewSize = camViewSize;
        this.projectionMatrix = Matrix.CreateOrthographic(aspectRatio * camViewSize, 1 * camViewSize, 0.1f, 10f);
    }

}

