using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.UIElements;
using System.Linq;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class MagicGame : MonoBehaviour
{  
	List<string> cards = new List<string>();
	public GameObject input,canv,output,cardHolder,proto;

	string conv (string x)
	{
		var str = x.Trim ();
		var indexS = str.IndexOf ("(");
		return indexS >= 0 ? str.Substring(0, indexS) : str;
	}

	string[] mult (string x)
	{
		var i = x.IndexOf (" ");
		var qty = 0;
		var qStr = i >= 0 ? x.Substring(0, i) : x;
		var val = i >= 0 ? x.Substring(i+1) : x;
		if (!int.TryParse (qStr, out qty)) {
			qty = 1;
		}
		return Array.ConvertAll(new string[qty], _ => val.Trim());
	}

	public void UpdateDeck ()
	{
		cards.Clear ();
		//Debug.Log ("--");
		char[] split = {'\n'};
		var text = input.GetComponent<InputField> ().text;
		var lines = text.Replace ("\r", "").Split (split);

		foreach(var line in lines){
			var x = conv(line);
			if(x.Length > 0) {
				foreach(var c in mult(x)){		
					cards.Add(c);
					//			Debug.Log (c);
				}
			}
		}
		var r = new System.Random ();
		cards.Sort((x, y) => r.NextDouble().CompareTo(r.NextDouble()));
		var debug = ""; // "\n" + string.Join (", ", cards);
		output.GetComponent<Text> ().text = "Deck size: " + cards.Count + "\nOk: " + (cards.Count == 60)+debug;
	}

	public void Submit ()
	{
		UpdateDeck ();
		canv.SetActive(false);

		foreach (Transform c in cardHolder.transform) {	
			Destroy (c.gameObject);
		}

		var i = 0;
		foreach (var c in cards) {
			var o = (GameObject)Instantiate (proto);
			o.transform.parent = cardHolder.transform;
			var p = o.transform.position;
			p.x = i++ / 100.0f;
			o.transform.position = p;
			var cardTexture = o.GetComponent<CardTexture> ();
			cardTexture.cardName = c;
			//yield return cardTexture.Start();
		}
	}

	public void Edit ()
	{
		canv.SetActive(true);
	}
}