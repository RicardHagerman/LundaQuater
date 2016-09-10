using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Aim : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public static Aim Current;
    public Action NewRound;

    [SerializeField]
    GameObject quater;
    [SerializeField]
    CanvasGroup aimCanvas;
    Vector2 aimStartPosition;

    float startTime;
    float endTime;

    Vector3 quaterPosition;

    void Awake()
    {
        Current = this;
    }

    void Start()
    {
        NewQuater();
    }

    public void NewQuater()
    {
        quater.GetComponent<Rigidbody>().isKinematic = true;
        quater.GetComponent<Rigidbody>().centerOfMass = Vector3.zero;
        quaterPosition = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0.18f, UnityEngine.Random.Range(-4.8f, -6f));
        quater.transform.position = quaterPosition;
        quater.transform.rotation = Quaternion.Euler(210, 0, 180);
        aimCanvas.alpha = 1f;
        aimCanvas.interactable = aimCanvas.blocksRaycasts = true;
        if (NewRound != null)
            NewRound();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        quater.transform.position = quaterPosition + new Vector3(0, 2.5f, 0);
        quater.transform.rotation = Quaternion.Euler(195, 0, 180);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startTime = Time.time;
        aimStartPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        endTime = Time.time;
        quater.GetComponent<Quater>().x = PowerX(eventData.position);
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
        var t = endTime - startTime;
        var x = Mathf.Abs(currentPos.x - aimStartPosition.x);
        var power = 1f;
        if (x < 20)
            power = 0;
        else if (currentPos.x < aimStartPosition.x)
            power = 0 - (x / t) / 400;
        else if (currentPos.x > aimStartPosition.x)
            power = 0 + (x / t) / 400;
        if (power > 1.5f)
            power = 1.5f;
        if (power < -1.5f)
            power = -1.5f;
        return power;
    }

    float PowerY(Vector2 currentPos)
    {
        var t = endTime - startTime;
        var y = currentPos.y - aimStartPosition.y;
        var power = 1 - (Mathf.Abs(y) / t) / 200;
        if (y > 0)
            power = 0;
        return power;
    }


}
