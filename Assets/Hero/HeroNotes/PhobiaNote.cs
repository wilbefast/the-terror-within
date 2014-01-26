using UnityEngine;
using System.Collections;

public class PhobiaNote : HeroNote 
{
	private static readonly string[] qualifiers =
	{
		"UnknownPhobia", "Fish", "Hairy", "Horned", "Lion", "Lizard", "Octopus", "Spider", "Swan"
	};
	
	private static readonly string[] flavourText =
	{
		"UnknownPhobia", 
		
		"Fish", 
		
		"Hairy", 
		
		"Horned", 
		
		"Lion", 
		
		"Lizard", 
		
		"Octopus", 
		
		"Spider", 
		
		"Swan"
	};
	
	private int currentIndex = 0;
	
	void Start()
	{
		resetTexture();
	}
	
	private void resetTexture()
	{
		renderer.material.mainTexture = (Texture)Resources.Load("Note" + qualifiers[currentIndex]);
	}
	
	protected override void switchNote()
	{
		currentIndex = (currentIndex + 1) % qualifiers.Length;
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
