using UnityEngine;
using System.Collections;


public class Texture : MonoBehaviour
{  
	public string name;

	// Use this for initialization
	IEnumerator Start ()
	{
		var url = "http://gatherer.wizards.com/Handlers/Image.ashx?type=card&name=" + name;
		
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