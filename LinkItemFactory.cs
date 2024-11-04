using Legend_of_the_Power_Rangers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using static Legend_of_the_Power_Rangers.Item;
using static Legend_of_the_Power_Rangers.LinkStateMachine;

public class LinkItemFactory
{
	private readonly Texture2D itemSpriteSheet;
    private readonly Texture2D projectileSpriteSheet;
    private readonly Texture2D blockSpriteSheet;
    private readonly List<Item> ActiveItems;
	private readonly List<int> toRemove;
	private readonly int toRemoveIndex;
	private Rectangle position;
	private LinkDirection direction;
	public LinkItemFactory(Texture2D itemSpriteSheet, Texture2D projectileSpriteSheet, Texture2D blockSpriteSheet)
	{
		this.ActiveItems = new List<Item>();
		this.toRemove = new List<int>();
		this.itemSpriteSheet = itemSpriteSheet;
		this.projectileSpriteSheet = projectileSpriteSheet;
		this.blockSpriteSheet = blockSpriteSheet;
	}
	public void CreateItem(CreationLinkItemType type)
	{
		//if (ActiveItems.Count == 0)
		//{
			switch (type)
			{
				case CreationLinkItemType.Bomb:
					ActiveItems.Add(new Item(CreationLinkItemType.Bomb, position, direction, itemSpriteSheet, projectileSpriteSheet, blockSpriteSheet));
					break;
				case CreationLinkItemType.Arrow:
					ActiveItems.Add(new Item(CreationLinkItemType.Arrow, position, direction, itemSpriteSheet, projectileSpriteSheet, blockSpriteSheet));
					break;
				case CreationLinkItemType.Sword:
					ActiveItems.Add(new Item(CreationLinkItemType.Sword, position, direction, itemSpriteSheet, projectileSpriteSheet, blockSpriteSheet));
					break;
				case CreationLinkItemType.Boomerang:
					ActiveItems.Add(new Item(CreationLinkItemType.Boomerang, position, direction, itemSpriteSheet, projectileSpriteSheet, blockSpriteSheet));
					break;
				case CreationLinkItemType.Candle:
					ActiveItems.Add(new Item(CreationLinkItemType.Candle, position, direction, itemSpriteSheet, projectileSpriteSheet, blockSpriteSheet));
					break;
			}
		//}
	}
	public void Update(GameTime gametime, Rectangle position, LinkDirection linkDirection)
	{
		this.position = position;
		this.direction = linkDirection;
		foreach (Item item in ActiveItems)
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
				ActiveItems.RemoveAt(removeIndex);
			}
		}
		toRemove.Clear();
	}
	public void Draw(SpriteBatch spritebatch)
	{
		foreach (Item item in ActiveItems)
		{
			item.Draw(spritebatch);
		}
	}
}
