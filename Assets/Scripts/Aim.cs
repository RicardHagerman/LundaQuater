using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Aim : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{


    [SerializeField]
    GameObject quater;
    [SerializeField]
    CanvasGroup aimCanvas;
    Vector2 aimStartPosition;
    Vector3 quaterPosition;
    Vector3 fromPos;
    Vector3 toPos;
    float lerpTime;

    bool quaterIsMoving;
    bool newRandomPosition;

    Vector3 newPos;

    void Start()
    {
        quater.GetComponent<Rigidbody>().isKinematic = true;
        quaterPosition = quater.transform.position;
        newRandomPosition = true;
    }

    void Update()
    {
        RandomQuaterMovement(quater.transform.position);
    }

    public void NewQuater()
    {
        quater.GetComponent<Rigidbody>().isKinematic = true;
        quaterPosition = new Vector3(0, 3, -6.5f);
        quater.transform.position = quaterPosition;
        newRandomPosition = true;
        quater.transform.rotation = Quaternion.Euler(115, 0, 0);
        aimCanvas.alpha = 1f;
        aimCanvas.interactable = aimCanvas.blocksRaycasts = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        quaterIsMoving = true;
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
        quater.GetComponent<Rigidbody>().isKinematic = false;
        quater.GetComponent<Rigidbody>().AddForce(new Vector3(0, -1200, 0));
        aimCanvas.alpha = 0f;
        aimCanvas.interactable = aimCanvas.blocksRaycasts = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        quaterIsMoving = false;
    }


    void MoveQuater(Vector2 currentAimPosition, PointerEventData data)
    {
        if (quaterIsMoving == false)
            return;
        newPos = Vector3.zero;
        if (Mathf.Abs(data.delta.x) > 2)
        {
            if (aimStartPosition.x - currentAimPosition.x < 0)
                newPos.x = 0.1f;
            else if (aimStartPosition.x - currentAimPosition.x > 0)
                newPos.x = -0.1f;
        }
        if (Mathf.Abs(data.delta.y) > 2)
        {
            if (aimStartPosition.y - currentAimPosition.y < 0)
                newPos.z = 0.1f;
            else if (aimStartPosition.y - currentAimPosition.y > 0)
                newPos.z = -0.1f;
        }
    }


    void RandomQuaterMovement(Vector3 quaterPos)
    {
        if (quaterIsMoving == false)
            return;
        lerpTime += Time.deltaTime / 2;
        if (newRandomPosition)
        {
            var randomPos = new Vector3(Random.Range(-.6f, .6f), 0, Random.Range(-.2f, .2f));
            toPos = quaterPos + randomPos;
            fromPos = quaterPos;
            lerpTime = 0;
            newRandomPosition = false;
        }
        toPos += newPos;
        Bounds();
        quaterPosition = Vector3.Lerp(fromPos, toPos, lerpTime);
        quater.transform.position = quaterPosition;
        newRandomPosition |= lerpTime > 1;
    }

    void Bounds()
    {
        if (toPos.x < -1.3f)
            toPos.x = -1.3f;
        if (toPos.x > 1.3f)
            toPos.x = 1.3f;
        if (toPos.z < -8f)
            toPos.z = -8f;
        if (toPos.z > -5f)
            toPos.z = -5f;
    }

}
