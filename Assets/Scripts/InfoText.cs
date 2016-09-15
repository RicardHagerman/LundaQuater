using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoText : MonoBehaviour
{
	Coroutine co;

	[SerializeField]
	Text infoText1;
	[SerializeField]
	Text infoText2;
	[SerializeField]
	GameObject swipeIcon;


	void OnEnable ()
	{
		swipeIcon.SetActive (false);
		if (co != null)
		{
			StopCoroutine (co);
			co = null;
		}
		co = StartCoroutine (InfoCO ());
	}


	void OnDisable ()
	{
		if (co != null)
		{
			StopCoroutine (co);
			co = null;
		}
	}


	IEnumerator InfoCO ()
	{
		var text1 = "Bounce the coin off the table, into the beer.\nPut your finger on the coin and swipe down.\nThe longer the swipe the greater the bounce.";
		for (int i = 0; i < text1.Length + 1; i++)
		{
			infoText1.text = "" + text1.Substring (0, i) + "";
			infoText2.text = "" + text1.Substring (0, i) + "";
			if (i == 67)
				swipeIcon.SetActive (true);
			if (i == 120)
				swipeIcon.SetActive (false);
			yield return new WaitForSeconds (.08f);
		}
		yield return null;
	}

}
