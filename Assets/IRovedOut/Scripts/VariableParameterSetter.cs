using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AC;


public class VariableParameterSetter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		AC.GlobalVariables.GetFloatValue (5);
		AC.GlobalVariables.GetFloatValue (6);
		AC.GlobalVariables.GetFloatValue (7);
		AC.GlobalVariables.GetFloatValue (8);
		AC.GlobalVariables.GetFloatValue (9);

		float topSetterValue = AC.GlobalVariables.GetFloatValue (5);
		float legSetterValue = AC.GlobalVariables.GetFloatValue (6);
		float feetSetterValue = AC.GlobalVariables.GetFloatValue (7);
		float hatSetterValue = AC.GlobalVariables.GetFloatValue (8);
		float underSetterValue = AC.GlobalVariables.GetFloatValue (9);

		Animator myAnimator = GetComponent <Animator>();

		myAnimator.SetFloat ("TopSetter", topSetterValue);
		myAnimator.SetFloat ("LegSetter", legSetterValue);
		myAnimator.SetFloat ("FeetSetter", feetSetterValue);
		myAnimator.SetFloat ("HatSetter", hatSetterValue);
		myAnimator.SetFloat ("UnderSetter", underSetterValue);
		}
}
