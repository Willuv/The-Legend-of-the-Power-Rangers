using Legend_of_the_Power_Rangers;
using Microsoft.Xna.Framework;
using System;

public class Camera2D
{
    private static Camera2D instance;
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
    public void CalculateMovement()
    {
       /* switch (direction) 
        {
            case "Left":
                movement.X = -1;
                break;
            case "Right":
                movement.X = 1;
                break;
            case "Up":
                movement.X = -1;
                break;
            case "Down":
                movement.X = -1;
                break;
            default:
                movement = Vector2.Zero;
                break;
        }*/
    }
    public void MoveCamera()
    {
        CalculateMovement();
        Vector2 newPosition = position + movement;
        position = newPosition;
    }

    public void CalculateTransformMatrix(int RoomRow, int RoomColumn)
	{
        position = new Vector2 ((1020 * RoomColumn), (698 * RoomRow));
        transformMatrix = Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0));
	}
}
