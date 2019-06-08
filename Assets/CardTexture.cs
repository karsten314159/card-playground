using UnityEngine;
using System.Collections;
using System;


public class CardTexture : MonoBehaviour
{  
	public string cardName;

	bool init;
	public IEnumerator Start ()
	{
		if (!init) {
			init = true;
			return initTex ();
		}
		return null;
	}

	public void Update ()
	{
		if (!init) {
			init = true;
			initTex ();
		}
	}

	IEnumerator initTex ()
	{
		var pref = "https://cors-anywhere.herokuapp.com/";
		var nameUrl = cardName;
		var url = pref + "http://gatherer.wizards.com/Handlers/Image.ashx?type=card&name=" + nameUrl;

		// Start a download of the given URL
		using (WWW www = new WWW(url))
		{
			// Wait for download to complete
			yield return www;

			// assign texture
			Renderer renderer = GetComponent<Renderer>();
			renderer.material.mainTexture = www.texture;
		}
	}
	
}