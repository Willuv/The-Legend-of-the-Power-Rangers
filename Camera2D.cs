using Legend_of_the_Power_Rangers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

public class Camera2D
{
    private static Camera2D instance;
    private bool isMoving;
    public bool IsMoving
    {
        get { return isMoving; }
        set { isMoving = value; }
    }
    private Vector2 destination;
    private Vector2 position;
    private Vector2 movement;
    private Matrix transformMatrix;
    public Matrix TransformMatrix
    {
        get { return transformMatrix; }
    }
    public Camera2D()
	{
        position = new Vector2(/*1020*/0, /*698*/0);
        destination = new Vector2(0, 0);
        isMoving = true;
	}

    public static Camera2D Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Camera2D();
            }
            return instance;
        }
    }
    public void CalculateRoomCamera(int RoomRow, int RoomColumn)
    {
        destination = new Vector2((1020 * RoomColumn), (698 * RoomRow));
    }
    public void CalculateMovement(String direction)
    {
         switch (direction)
         {
             case "Left":
                 position.X += -1;
                 break;
             case "Right":
                 position.X += 1;
                 break;
             case "Up":
                 position.Y += -1;
                 break;
             case "Down":
                 position.Y += 1;
                 break;
             default:
                break;
         }
        transformMatrix = Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0));
        Debug.Print(position.X.ToString() + destination.X.ToString());
        if (destination == position)
        {
            IsMoving = false;
        }
    }

    public void CalculateTransformMatrix()
	{
        transformMatrix = Matrix.CreateTranslation(new Vector3(-destination.X, -destination.Y, 0));
        position = destination;
	}
}
