using UnityEngine;
using System.Collections;

public class PhobiaNote : HeroNote 
{
	private static readonly string[] qualifiers =
	{
		"UnknownPhobia", "Fish", "Hairy", "Horned", "Lion", "Lizard", "Octopus", "Spider", "Swan"
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
	
	void Update()
	{
		if(!hovering)
			return;
		
		if(Input.GetMouseButtonDown(0))
		{
			currentIndex = (currentIndex + 1) % qualifiers.Length;
			resetTexture();
		}
		else if(Input.GetMouseButtonDown(1))
		{
			currentIndex = 0;
			resetTexture();
		}
	}
}
