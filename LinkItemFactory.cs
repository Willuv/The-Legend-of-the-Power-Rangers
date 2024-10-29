using Legend_of_the_Power_Rangers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static Legend_of_the_Power_Rangers.LinkItem;
using static Legend_of_the_Power_Rangers.LinkStateMachine;

public class LinkItemFactory : IDamaging
{
	private readonly Texture2D itemSpriteSheet;
    private readonly Texture2D projectileSpriteSheet;
    private readonly Texture2D blockSpriteSheet;
    private readonly List<LinkItem> ActiveItems;
	private readonly List<int> toRemove;
	private readonly int toRemoveIndex;
	private Rectangle position;
	private LinkDirection direction;

	//for collision
    public event ObjectEventHandler OnObjectCreated = delegate { };
    public event ObjectEventHandler OnObjectRemoved = delegate { };
    public LinkItemFactory(Texture2D itemSpriteSheet, Texture2D projectileSpriteSheet, Texture2D blockSpriteSheet)
	{
		this.ActiveItems = new List<LinkItem>();
		this.toRemove = new List<int>();
		this.itemSpriteSheet = itemSpriteSheet;
		this.projectileSpriteSheet = projectileSpriteSheet;
		this.blockSpriteSheet = blockSpriteSheet;
	}
	public void CreateItem(CreationLinkItemType type)
	{
        LinkItem item = null;
		switch (type)
		{
			case CreationLinkItemType.Bomb:
				item = new(CreationLinkItemType.Bomb, position, direction, itemSpriteSheet, projectileSpriteSheet, blockSpriteSheet);
				break;
			case CreationLinkItemType.Arrow:
				item = new(CreationLinkItemType.Arrow, position, direction, itemSpriteSheet, projectileSpriteSheet, blockSpriteSheet);
				break;
			case CreationLinkItemType.Sword:
                item = new(CreationLinkItemType.Sword, position, direction, itemSpriteSheet, projectileSpriteSheet, blockSpriteSheet);
				break;
			case CreationLinkItemType.Boomerang:
                item = new(CreationLinkItemType.Boomerang, position, direction, itemSpriteSheet, projectileSpriteSheet, blockSpriteSheet);
				break;
			case CreationLinkItemType.Candle:
                item = new(CreationLinkItemType.Candle, position, direction, itemSpriteSheet, projectileSpriteSheet, blockSpriteSheet);
				break;
        }

        ActiveItems.Add(item);
		ICollision collidable = item.CollisionObject;
		if (item == null) Debug.WriteLine("item is null");
		if (collidable == null) Debug.WriteLine("collidable is null");
        DelegateManager.RaiseObjectCreated(collidable);
		Debug.WriteLine("Object creation invoked");
    }
	public void Update(GameTime gametime, Rectangle position, LinkDirection linkDirection)
	{
		this.position = position;
		this.direction = linkDirection;
		foreach (LinkItem item in ActiveItems)
		{
			item.Update(gametime);
			if (item.GetState())
			{
				toRemove.Add(ActiveItems.IndexOf(item));
			}
		}
		foreach (int removeIndex in toRemove)
		{
			if (removeIndex < ActiveItems.Count)
			{
				ICollision collidable = ActiveItems[removeIndex] as ICollision;
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
