﻿using UnityEngine;
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
    Vector3 quaterPosition;
    Vector3 fromPos;
    Vector3 toPos;
    float dist;
    float lerpTime;

    bool quaterIsMoving;
    bool newRandomPosition;

    Vector3 newPos;


    void Awake()
    {
        Current = this;
    }

    void Start()
    {
        NewQuater();
    }

    void Update()
    {
        RandomQuaterMovement(quater.transform.position);
    }

    public void NewQuater()
    {
        quater.GetComponent<Rigidbody>().isKinematic = true;
        quaterPosition = new Vector3(UnityEngine.Random.Range(-.5f, .5f), 3, UnityEngine.Random.Range(-4.8f, -6f));
        quater.transform.position = quaterPosition;
        newRandomPosition = true;
        lerpTime = 0;
        quater.transform.rotation = Quaternion.Euler(195, 0, 180);
        aimCanvas.alpha = 1f;
        aimCanvas.interactable = aimCanvas.blocksRaycasts = true;
        quaterIsMoving = true;
        if (NewRound != null)
            NewRound();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        aimStartPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        MoveQuater(eventData.position, eventData);
        aimStartPosition = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        quaterIsMoving = false;
        quater.GetComponent<Rigidbody>().isKinematic = false;
        quater.GetComponent<Rigidbody>().AddForce(new Vector3(-1f, -11, 0), ForceMode.Impulse);
        if (quater.transform.position.x > 0.15f)
            quater.GetComponent<Rigidbody>().AddForce(new Vector3(1, 0, 0), ForceMode.Impulse);
        if (quater.transform.position.x < -0.15f)
            quater.GetComponent<Rigidbody>().AddForce(new Vector3(-1, 0, 0), ForceMode.Impulse);
        aimCanvas.alpha = 0f;
        aimCanvas.interactable = aimCanvas.blocksRaycasts = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }


    void MoveQuater(Vector2 currentAimPosition, PointerEventData data)
    {
        if (quaterIsMoving == false)
            return;
        newPos = Vector3.zero;
        if (Mathf.Abs(data.delta.x) > 2)
        {
            if (aimStartPosition.x - currentAimPosition.x < 0)
                newPos.x = 0.05f;
            else if (aimStartPosition.x - currentAimPosition.x > 0)
                newPos.x = -0.05f;
        }
        if (Mathf.Abs(data.delta.y) > 2)
        {
            if (aimStartPosition.y - currentAimPosition.y < 0)
                newPos.z = 0.05f;
            else if (aimStartPosition.y - currentAimPosition.y > 0)
                newPos.z = -0.05f;
        }
    }


    void RandomQuaterMovement(Vector3 quaterPos)
    {
        if (quaterIsMoving == false)
            return;
        if (newRandomPosition)
        {
            var randomPos = new Vector3(UnityEngine.Random.Range(-.05f, .05f), 0, UnityEngine.Random.Range(-.05f, .05f));
            toPos = quaterPos + randomPos;
            fromPos = quaterPos;
            lerpTime = 0;
            dist = Vector3.Distance(fromPos, toPos);
            newRandomPosition = false;
        }
        toPos += newPos;
        Bounds();
        lerpTime += Time.deltaTime / dist / 5;
        quaterPosition = Vector3.Lerp(fromPos, toPos, lerpTime);
        quater.transform.position = quaterPosition;
        newRandomPosition |= lerpTime > 1;
    }

    void Bounds()
    {
        if (toPos.x < -0.50f)
            toPos.x = -0.50f;
        if (toPos.x > 0.55f)
            toPos.x = 0.55f;
        if (toPos.z < -7f)
            toPos.z = -7f;
        if (toPos.z > -4.8f)
            toPos.z = -4.8f;
    }

}
