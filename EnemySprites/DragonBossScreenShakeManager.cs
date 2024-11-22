using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Legend_of_the_Power_Rangers
{
public static class ScreenShakeManager
{
    private static float intensity = 0f;
    private static float duration = 0f;
    private static Random random = new Random();

    public static Vector2 Offset { get; private set; } = Vector2.Zero;

    public static void TriggerShake(float intensity, float duration)
    {
        ScreenShakeManager.intensity = intensity;
        ScreenShakeManager.duration = duration;
    }

    public static void Update(GameTime gameTime)
    {
        if (duration > 0)
        {
            duration -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            Offset = new Vector2(
                (float)(random.NextDouble() * 2 - 1) * intensity,
                (float)(random.NextDouble() * 2 - 1) * intensity
            );
        }
        else
        {
            Offset = Vector2.Zero;
        }
    }
}
}