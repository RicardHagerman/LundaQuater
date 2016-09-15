using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class Aim : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
	public static Aim Current;

	[SerializeField]
	GameObject quaterPrefab;
	[SerializeField]
	CanvasGroup aimCanvas;
	[SerializeField]
	Transform loadingBar;
	GameObject quater;
	Vector2 aimStartPosition;

	//float startTime;
	//float endTime;

	public Vector3 quaterPosition;
	public Level currentLevel;
	List<GameObject> quatersInPlay;

	float bouncePower;

	void Awake ()
	{
		Current = this;
		quatersInPlay = new List<GameObject> ();
		aimCanvas.alpha = 1f;
		aimCanvas.interactable = aimCanvas.blocksRaycasts = false;
		QuaterBack.FirstQuater += OnFirstQuater;
		QuaterBack.NewRound += NewQuater;
	}

	void Start ()
	{
		bouncePower = (Screen.height / 10);
		loadingBar.GetComponent<Image> ().fillAmount = 0;
	}

	void OnDisable ()
	{
		QuaterBack.FirstQuater -= OnFirstQuater;
		QuaterBack.NewRound -= NewQuater;
	}

	void NewQuater ()
	{
		quater = Instantiate (quaterPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		quatersInPlay.Add (quater);
		quater.GetComponent<Rigidbody> ().isKinematic = true;
		switch (currentLevel)
		{
		case Level.SHOW:
			quaterPosition = new Vector3 (0f, 3.7f, -8f);
			break;
		case Level.BEGINNER:	
			quaterPosition = new Vector3 (0, 4f, -6.3f);
			break;
		case Level.NORMAL:
			quaterPosition = new Vector3 (0, 4f, Random.Range (-4.5f, -6.5f));
			break;
		case Level.HARD:
			quaterPosition = new Vector3 (Random.Range (-1f, 1f), 2.5f, Random.Range (-4.5f, -6.5f));
			break;
		}
		quater.transform.position = quaterPosition;
		quater.transform.rotation = Quaternion.Euler (205, 0, 180);
		if (currentLevel == Level.SHOW)
			return;
		aimCanvas.interactable = aimCanvas.blocksRaycasts = true;
		if (QuaterBack.NewMarker != null)
			QuaterBack.NewMarker ();
	}

	void OnFirstQuater ()
	{
		if (quatersInPlay.Count > 0)
			foreach (var q in quatersInPlay)
				Destroy (q);
		quatersInPlay = new List<GameObject> ();
	}

	public void OnPointerEnter (PointerEventData eventData)
	{
	}

	public void OnPointerDown (PointerEventData eventData)
	{
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		//startTime = Time.time;
		aimStartPosition = eventData.position;
	}

	public void OnDrag (PointerEventData eventData)
	{
		loadingBar.GetComponent<Image> ().fillAmount = Fill (eventData.position) / 100f;
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		//endTime = Time.time;
		if (currentLevel == Level.HARD)
			quater.GetComponent<Quater> ().x = PowerX (eventData.position);
		else
			quater.GetComponent<Quater> ().x = 0;
		var y = PowerY (eventData.position);
		quater.transform.rotation = Quaternion.Euler (190, 0, 180);
		quater.GetComponent<Rigidbody> ().isKinematic = false;
		quater.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
		aimCanvas.interactable = aimCanvas.blocksRaycasts = false;
		quater.GetComponent<Rigidbody> ().AddForce (new Vector3 (0, y, 0), ForceMode.Impulse);
		loadingBar.GetComponent<Image> ().fillAmount = 0;

	}

	public void OnPointerUp (PointerEventData eventData)
	{
	}

	public void OnPointerExit (PointerEventData eventData)
	{
	}

	float PowerX (Vector2 currentPos)
	{
		// var t = endTime - startTime;
		var x = Mathf.Abs (currentPos.x - aimStartPosition.x);
		var power = 1f;
		if (currentPos.x < aimStartPosition.x)
			power = 0 - x / 200;
		else if (currentPos.x > aimStartPosition.x)
			power = 0 + x / 200;
		if (power > 2f)
			power = 2f;
		if (power < -2f)
			power = -2f;
		Debug.Log ("POWER   " + power);
		return power;
	}

	float PowerY (Vector2 currentPos)
	{
		//var t = endTime - startTime;
		var y = currentPos.y - aimStartPosition.y;
		//var power = 1 - (Mathf.Abs(y) / t) / 150;
		float power = -8.5f + (y / bouncePower);
		if (power < -12.5f)
			power = -12.5f;
		Debug.Log (power);
		return power;
	}


	float Fill (Vector2 currentPos)
	{
		var f = (Mathf.Abs (PowerY (currentPos)) - 8.5f) * 26f;
		return f;
	}

}
