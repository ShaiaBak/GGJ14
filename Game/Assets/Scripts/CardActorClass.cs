using UnityEngine;
using System.Collections;

public class CardActorClass : MonoBehaviour {

	
	public GameObject FirstCommand;
	public GameObject SecondCommand;

	public Material[] BaseMaterials;
	public Texture[] CommandTextures; // Preset in order of the enum

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetupUserInterface(CardCommand FirstCom, CardCommand SecondCom)
	{
		// Dynamically generates action icons based on card info passed in
		int ChooseBase = 0;

		// Use blue card base if both actions are moves, red otherwise
		if ( (FirstCom == CardCommand.CC_MoveLeft || 
		      FirstCom == CardCommand.CC_MoveRight || 
		      FirstCom == CardCommand.CC_MoveUp || 
		      FirstCom == CardCommand.CC_MoveDown ) 
			 && 
		    (SecondCom == CardCommand.CC_MoveLeft || 
		 	 SecondCom == CardCommand.CC_MoveRight || 
		 	 SecondCom == CardCommand.CC_MoveUp || 
		 	 SecondCom == CardCommand.CC_MoveDown) )
		{
			ChooseBase = 1;
		}

		renderer.material = BaseMaterials[ChooseBase];


		if ((int) FirstCom < CommandTextures.Length && FirstCommand != null)
		{
			FirstCommand.renderer.material.mainTexture = CommandTextures[(int) FirstCom];
		}

		if ((int) SecondCom < CommandTextures.Length && SecondCommand != null)
		{
			SecondCommand.renderer.material.mainTexture = CommandTextures[(int) SecondCom];
		}
	}

}
