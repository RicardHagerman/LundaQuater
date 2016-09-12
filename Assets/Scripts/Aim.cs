using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Aim : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public static Aim Current;

    [SerializeField]
    GameObject quaterPrefab;
    [SerializeField]
    CanvasGroup aimCanvas;
    GameObject quater;
    Vector2 aimStartPosition;

    //float startTime;
    //float endTime;

    public Vector3 quaterPosition;
    public Level currentLevel;
    List<GameObject> quatersInPlay;

    void Awake()
    {
        Current = this;
        quatersInPlay = new List<GameObject>();
        aimCanvas.alpha = 1f;
        aimCanvas.interactable = aimCanvas.blocksRaycasts = false;
        QuaterBack.FirstQuater += OnFirstQuater;
        QuaterBack.NewRound += NewQuater;
    }

    //void Start()
    //{

    //}

    void OnDisable()
    {
        QuaterBack.FirstQuater -= OnFirstQuater;
        QuaterBack.NewRound -= NewQuater;
    }

    void NewQuater()
    {
        quater = Instantiate(quaterPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        quatersInPlay.Add(quater);
        quater.GetComponent<Rigidbody>().isKinematic = true;
        quater.GetComponent<Rigidbody>().centerOfMass = Vector3.zero;
        switch (currentLevel)
        {
            case Level.SHOW:
                quaterPosition = new Vector3(0f, 2.5f, -8f);
                break;
            case Level.BEGINNER:
                quaterPosition = new Vector3(0, 2.5f, -6f);
                break;
            case Level.NORMAL:
                quaterPosition = new Vector3(0, 2.5f, Random.Range(-4.5f, -6.5f));
                break;
            case Level.HARD:
                quaterPosition = new Vector3(Random.Range(-1f, 1f), 2.5f, Random.Range(-4.5f, -6.5f));
                break;
        }
        quater.transform.position = quaterPosition;
        quater.transform.rotation = Quaternion.Euler(190, 0, 180);
        if (currentLevel == Level.SHOW)
            return;
        aimCanvas.interactable = aimCanvas.blocksRaycasts = true;
        if (QuaterBack.NewMarker != null)
            QuaterBack.NewMarker();
    }

    void OnFirstQuater()
    {
        if (quatersInPlay.Count > 0)
            foreach (var q in quatersInPlay)
                Destroy(q);
        quatersInPlay = new List<GameObject>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //startTime = Time.time;
        aimStartPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //endTime = Time.time;
        if (currentLevel == Level.HARD)
            quater.GetComponent<Quater>().x = PowerX(eventData.position);
        else
            quater.GetComponent<Quater>().x = 0;
        var y = PowerY(eventData.position);
        quater.GetComponent<Rigidbody>().isKinematic = false;
        aimCanvas.interactable = aimCanvas.blocksRaycasts = false;
        quater.GetComponent<Rigidbody>().AddForce(new Vector3(0, y, 0), ForceMode.Impulse);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    float PowerX(Vector2 currentPos)
    {
        // var t = endTime - startTime;
        var x = Mathf.Abs(currentPos.x - aimStartPosition.x);
        var power = 1f;
        if (currentPos.x < aimStartPosition.x)
            power = 0 - x / 200;
        else if (currentPos.x > aimStartPosition.x)
            power = 0 + x / 200;
        if (power > 2f)
            power = 2f;
        if (power < -2f)
            power = -2f;
        Debug.Log("POWER   " + power);
        return power;
    }

    float PowerY(Vector2 currentPos)
    {
        //var t = endTime - startTime;
        var y = currentPos.y - aimStartPosition.y;
        //var power = 1 - (Mathf.Abs(y) / t) / 150;
        var power = 1 - Mathf.Abs(y) / 20;
        if (power > -3)
            power = -3;
        if (power < -13)
            power = -13;
        return power;
    }


}
