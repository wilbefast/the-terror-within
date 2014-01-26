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
		"UnknownPredisposition", "Bipolar", "Optimistic", "Pessimistic", "PrimaDonna", "Realist"
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
