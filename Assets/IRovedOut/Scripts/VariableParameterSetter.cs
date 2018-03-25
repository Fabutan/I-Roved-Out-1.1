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

		AC.GlobalVariables.GetIntegerValue (5);
		AC.GlobalVariables.GetIntegerValue (6);
		AC.GlobalVariables.GetIntegerValue (7);
		AC.GlobalVariables.GetIntegerValue (8);
		AC.GlobalVariables.GetIntegerValue (9);

		float topSetterValue = AC.GlobalVariables.GetIntegerValue (5);
		float legSetterValue = AC.GlobalVariables.GetIntegerValue (6);
		float feetSetterValue = AC.GlobalVariables.GetIntegerValue (7);
		float hatSetterValue = AC.GlobalVariables.GetIntegerValue (8);
		float underSetterValue = AC.GlobalVariables.GetIntegerValue (9);

		Animator myAnimator = GetComponent <Animator>();

		myAnimator.SetFloat ("TopSetter", topSetterValue);
		myAnimator.SetFloat ("LegSetter", legSetterValue);
		myAnimator.SetFloat ("FeetSetter", feetSetterValue);
		myAnimator.SetFloat ("HatSetter", hatSetterValue);
		myAnimator.SetFloat ("UnderSetter", underSetterValue);
		}
}
