using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Aim : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{


    [SerializeField]
    GameObject quater;
    [SerializeField]
    CanvasGroup aimCanvas;
    Vector2 aimStartPosition;
    Vector3 quaterPosition;
    bool quaterIsMoving;


    void Start()
    {
        quater.GetComponent<Rigidbody>().isKinematic = true;
        quaterPosition = quater.transform.position;
    }

    void Update()
    {

    }

    public void NewQuater()
    {
        quater.GetComponent<Rigidbody>().isKinematic = true;
        quaterPosition = new Vector3(0, 8, -5);
        quater.transform.rotation = Quaternion.Euler(115, 0, 0);
        quater.transform.position = quaterPosition;
        aimCanvas.alpha = 1f;
        aimCanvas.interactable = aimCanvas.blocksRaycasts = true;
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        quaterIsMoving = true;

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");

    }



    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        aimStartPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        MoveQuater(eventData.position);
        aimStartPosition = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp");
        quater.GetComponent<Rigidbody>().isKinematic = false;
        aimCanvas.alpha = 0f;
        aimCanvas.interactable = aimCanvas.blocksRaycasts = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");
        quaterIsMoving = false;
    }


    void MoveQuater(Vector2 currentAimPosition)
    {
        if (quaterIsMoving == false)
            return;
        var newPos = Vector3.zero;
        bool moveLeftRight = false;
        if (aimStartPosition.x - currentAimPosition.x < -5 || aimStartPosition.x - currentAimPosition.x > 5)
            moveLeftRight = true;

        if (moveLeftRight)
        {
            if (aimStartPosition.x - currentAimPosition.x < -1)
                newPos.x = 0.05f;
            else if (aimStartPosition.x - currentAimPosition.x > 1)
                newPos.x = -0.05f;
        }
        else
        {
            if (aimStartPosition.y - currentAimPosition.y < -2)
                newPos.z = -0.05f;
            else if (aimStartPosition.y - currentAimPosition.y > 2)
                newPos.z = 0.05f;
        }

        quater.transform.position = quaterPosition + newPos;
        quaterPosition = quater.transform.position;
    }

}
