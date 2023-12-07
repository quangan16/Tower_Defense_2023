using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardPowerUpMove : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public Vector3 prePos;
    public RectTransform rectTransform;
    public Image image;
    public Camera cam;
    public string typeBuff;
    public float valueBuff;
    public LayerMask layerMask;
    public Transform oriParent;
    private void Awake()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    private void OnEnable()
    {
        oriParent = transform.parent;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        prePos = rectTransform.anchoredPosition;
        transform.SetParent(transform.root);
        image.raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        BuffPower(eventData);
        rectTransform.anchoredPosition = prePos;
        image.raycastTarget = true;
        transform.SetParent(oriParent);
    }

    public void BuffPower(PointerEventData eventData)
    {
        Ray ray = cam.ScreenPointToRay(eventData.position);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, layerMask))
        {
            if (hit.collider.CompareTag("Hero"))
            {
                hit.collider.gameObject.GetComponent<PowerUp>().Buff(valueBuff, typeBuff);
                Destroy(gameObject);
            }
        }
    }
}
