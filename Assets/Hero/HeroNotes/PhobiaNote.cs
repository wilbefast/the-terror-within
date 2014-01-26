using UnityEngine;
using System.Collections;

public class PhobiaNote : HeroNote 
{
	private static readonly string[] qualifiers =
	{
		"UnknownPhobia", "Fish", "Hairy", "Horned", "Human", "Lion", "Lizard", "Octopus", "Spider", "Swan"
	};
	
	private static readonly string[] flavourText =
	{
		"Unknown phobia\nEveryone is afraid of something...", 
		
		"Ichthyophobia\nJust can't stand fish: the scales, those dead, dead eyes!", 
				
		"Ommetaphobia\nHorrified by the very idea of being watched.\nWhile you sleep. Every night.",
		
		"Aichmophobia\nA perhaps not-so-irrational fear of sharp objects.", 
		
		"Anthropophobia\nThe irrational fear of David Bowie...\nand people in general",
		
		"Ailurophobia\nTerrified of cats, or perhaps just allergic...", 
		
		"Herpetophobia\nAn adject fear of cold-blooded reptiles, especially bankers.",
		
		"Octophobia\nAfraid of the number 8. Not great for mathematicians.",
		
		"Arachnophobia\nThere's one crawling up your arm as we speak.",
		
		"Ornithophobia\nA swan can break a body's arm with its wings!"
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
