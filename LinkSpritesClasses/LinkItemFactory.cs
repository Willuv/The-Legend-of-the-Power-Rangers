using Legend_of_the_Power_Rangers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static Legend_of_the_Power_Rangers.LinkItem;
using static Legend_of_the_Power_Rangers.LinkStateMachine;

public class LinkItemFactory
{
	private readonly Texture2D itemSpriteSheet;
    private readonly Texture2D projectileSpriteSheet;
    private readonly Texture2D blockSpriteSheet;
    private readonly List<LinkItem> ActiveItems;
	private readonly List<int> toRemove;
	private readonly int toRemoveIndex;
	private Rectangle position;
	private LinkDirection direction;

    private readonly Dictionary<CreationLinkItemType, float> cooldownDurations = new()
    {
        { CreationLinkItemType.Bomb, 0.5f },
        { CreationLinkItemType.Arrow, 0.5f },
        { CreationLinkItemType.Sword, 1.5f },
        { CreationLinkItemType.Boomerang, 1.6f },
        { CreationLinkItemType.Candle, 1.2f }
    };

    private readonly Dictionary<CreationLinkItemType, float> cooldownTimers = new();


    public LinkItemFactory(Texture2D itemSpriteSheet, Texture2D projectileSpriteSheet, Texture2D blockSpriteSheet)
	{
		this.ActiveItems = new List<LinkItem>();
		this.toRemove = new List<int>();
		this.itemSpriteSheet = itemSpriteSheet;
		this.projectileSpriteSheet = projectileSpriteSheet;
		this.blockSpriteSheet = blockSpriteSheet;
        foreach (var itemType in cooldownDurations.Keys)
        {
            cooldownTimers[itemType] = 0;
        }
    }
	public void CreateItem(CreationLinkItemType type)
	{
        if (cooldownTimers[type] > 0) return;

        LinkItem item = null;
		switch (type)
		{

			case CreationLinkItemType.Bomb:
                if (!AudioManager.Instance.IsMuted()) AudioManager.Instance.PlaySound("Bomb_Drop");
                item = new(CreationLinkItemType.Bomb, position, direction, itemSpriteSheet, projectileSpriteSheet, blockSpriteSheet);
				break;
			case CreationLinkItemType.Arrow:
                if (!AudioManager.Instance.IsMuted()) AudioManager.Instance.PlaySound("Arrow_Boomerang");
                item = new(CreationLinkItemType.Arrow, position, direction, itemSpriteSheet, projectileSpriteSheet, blockSpriteSheet);
				break;
			case CreationLinkItemType.Sword:
                if (!AudioManager.Instance.IsMuted()) AudioManager.Instance.PlaySound("Sword_Combined");
                item = new(CreationLinkItemType.Sword, position, direction, itemSpriteSheet, projectileSpriteSheet, blockSpriteSheet);
				break;
			case CreationLinkItemType.Boomerang:
                item = new(CreationLinkItemType.Boomerang, position, direction, itemSpriteSheet, projectileSpriteSheet, blockSpriteSheet);
				break;
			case CreationLinkItemType.Candle:
                if (!AudioManager.Instance.IsMuted()) AudioManager.Instance.PlaySound("Candle");
                item = new(CreationLinkItemType.Candle, position, direction, itemSpriteSheet, projectileSpriteSheet, blockSpriteSheet);
				break;
        }

        ActiveItems.Add(item);
		ICollision collidable = item.CollisionObject;
		//if (item == null) Debug.WriteLine("item is null");
		//if (collidable == null) Debug.WriteLine("collidable is null");
        DelegateManager.RaiseObjectCreated(collidable);
        //Debug.WriteLine("Object creation invoked");
        cooldownTimers[type] = cooldownDurations[type];
    }

    public void Update(GameTime gametime, Rectangle position, LinkDirection linkDirection)
	{
		this.position = position;
		this.direction = linkDirection;

        foreach (var itemType in cooldownTimers.Keys)
        {
            if (cooldownTimers[itemType] > 0)
            {
                cooldownTimers[itemType] -= (float)gametime.ElapsedGameTime.TotalSeconds;
                if (cooldownTimers[itemType] < 0)
                {
                    cooldownTimers[itemType] = 0;
                }
            }
        }

        foreach (LinkItem item in ActiveItems)
		{
			item.Update(gametime);
			if (item.GetState() || item.DamagingObject.HasHitWall)
			{
				toRemove.Add(ActiveItems.IndexOf(item));
			}
		}
		foreach (int removeIndex in toRemove)
		{
			if (removeIndex < ActiveItems.Count)
			{
				ICollision collidable = ActiveItems[removeIndex].CollisionObject;
				DelegateManager.RaiseObjectRemoved(collidable);
                ActiveItems.RemoveAt(removeIndex);
				Debug.WriteLine("Object removal invoked");
            }
		}
		toRemove.Clear();
	}
	public void Draw(SpriteBatch spritebatch)
	{
		foreach (LinkItem item in ActiveItems)
		{
			item.Draw(spritebatch);
		}
	}
}
