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
		// TODO: Stretch goal, don't choose colours randomly, choose based on what actions on card

		// Dynamically generates action icons based on card info passed in
		int ChooseBase = Random.Range(0,2);
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
