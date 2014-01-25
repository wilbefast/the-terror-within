using UnityEngine;
using System.Collections;

public class QualifierTexture : MonoBehaviour 
{
	public string bodyPart = "Head";
	
	void Start()
	{
		var qualifier = GetComponent<MonsterQualifier>();
		renderer.material.mainTexture = (Texture)Resources.Load(bodyPart + "/" + qualifier.qualifierName);
	}
}
