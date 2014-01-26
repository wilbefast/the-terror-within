using UnityEngine;
using System.Collections;

public class PredispositionNote : HeroNote 
{
	private static readonly string[] predispositions =
	{
		"UnknownPredisposition", "Bipolar", "Optimistic", "Pessimistic", "PrimaDonna", "Realist"
	};
	
	private static readonly string[] flavourText =
	{
		"Unknown temperement\nWe don't see things as they are,\nwe see them as we are...",
		"Bipolar\nLife is full of ups and down,\nsome bigger than others.",
		"Optimistic\nSometimes it can be a bad thing to be too optimistic",
		"Pessimistic\nBetter safe than sorry.\nSome people like to be very safe.",
		"Prima donna\nMaking a mountain out of every molehill since 1982!",
		"Realist\nAccurately evaluates the situation"
	};
	
	private int currentIndex = 0;
	
	void Start()
	{
		resetTexture();
	}
	
	private void resetTexture()
	{
		renderer.material.mainTexture = (Texture)Resources.Load("Note" + predispositions[currentIndex]);
	}
	
	protected override void switchNote()
	{
		currentIndex = (currentIndex + 1) % predispositions.Length;
		resetTexture();
	}
	
	protected override void resetNote()
	{
		currentIndex = 0;
		resetTexture();
	}
	
	protected override string getFlavourText()
	{
		return flavourText[currentIndex];
	}
}
