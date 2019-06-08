using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Use InputFieldM instead of InputField
public class InputFieldM : InputField
{
	
	protected override void Append (string input)
	{ 
		if (Input.GetKeyDown (KeyCode.V)) { 
			TextEditor te = new TextEditor (); 
			te.multiline = multiLine; 
			te.Paste (); 
			input = te.text; 
		} 

		if (TouchScreenKeyboard.isSupported)
			return; 

		for (int i = 0, imax = input.Length; i < imax; ++i) { 
			char c = input [i]; 

			Append (c); 
		} 
	}
}