using UnityEngine;
using System.Collections;

public class PredispositionNote : HeroNote 
{
	private static readonly string[] predispositions =
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
	
	void Update()
	{
		if(!hovering)
			return;
		
		if(Input.GetMouseButtonDown(0))
		{
			currentIndex = (currentIndex + 1) % predispositions.Length;
			resetTexture();
		}
		else if(Input.GetMouseButtonDown(1))
		{
			currentIndex = 0;
			resetTexture();
		}
	}
}
