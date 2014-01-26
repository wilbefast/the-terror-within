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
		renderer.material.mainTexture = (Texture)Resources.Load("Note" + predispositions[currentIndex]);
	}
	
	void OnMouseDown()
	{
		currentIndex = (currentIndex + 1) % predispositions.Length;
		renderer.material.mainTexture = (Texture)Resources.Load("Note" + predispositions[currentIndex]);
	}
}
