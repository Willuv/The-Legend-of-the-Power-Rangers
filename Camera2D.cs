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
    public Camera2D(int CurrentRoomRow, int CurrentRoomColumn)
	{
        position = new Vector2(1020 * CurrentRoomColumn, 698 * CurrentRoomRow);
        destination = position;
        isMoving = false;
	}

    public static Camera2D Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Camera2D(0, 0);
            }
            return instance;
        }
    }
    public void CalculateRoomCamera(int RoomRow, int RoomColumn)
    {
        destination = new Vector2((1020 * RoomColumn), (698 * RoomRow));
    }
    public void CalculateMovement()
    {
            // Smoothly move position towards destination
            float lerpSpeed = 0.05f; // Adjust for desired speed
            position = Vector2.Lerp(position, destination, lerpSpeed);

            // Update transform matrix based on new position
            transformMatrix = Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0));

            // Stop moving if the camera is very close to the destination
            if (Vector2.Distance(position, destination) < 1f)
            {
                position = destination;
                isMoving = false;
            }
    }

    public void CalculateTransformMatrix()
	{
        transformMatrix = Matrix.CreateTranslation(new Vector3(-destination.X, -destination.Y, 0));
	}
}
