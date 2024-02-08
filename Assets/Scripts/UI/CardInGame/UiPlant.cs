using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

public class UiPlant : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    private Vector3 pos;
    [SerializeField]
    public int index;
    public Transform parentAfterDrag;
    public Image image;
    public bool createdPlant = false;
    private GameObject _newPlant;
    public bool ismerged;
    public bool realease;
    public string nameUiPlant;
    public int level;
    public bool triggerStay;
    public GameObject otherGameObject;
    public GameObject uiPlantPrefab;
    private FollowMousePos followMousePos;
    private void OnEnable()
    {
        realease = false;
        ismerged = false;
        image.enabled = true;
        //parentAfterDrag = transform.parent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        if (_newPlant != null)
        {
            followMousePos = _newPlant.GetComponent<FollowMousePos>();
            if (followMousePos.inBound)
            {
                image.enabled = true;
            }
            else
            {
                image.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "UiPlant")
        {
            UiPlant thisPlantBase = gameObject.GetComponent<UiPlant>();
            UiPlant otherPlantBase = collision.gameObject.GetComponent<UiPlant>();

            if (thisPlantBase.level == otherPlantBase.level && thisPlantBase.index == otherPlantBase.index)
            {
                ismerged = true;
            }
            otherGameObject = collision.gameObject;
        }
        if (collision.gameObject.tag == "NavBackground" && !triggerStay && !ismerged)
        {
            image.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NavBackground" && !triggerStay && !ismerged)
        {
            if (!createdPlant)
            {
                GameObject newPlant = ObjectPool.instance.GetFromObjectPool(ObjectPool.instance.plants[index].array[level], pos);
                _newPlant = newPlant;
                FollowMousePos newFolMouPos = newPlant.GetComponent<FollowMousePos>();
                newFolMouPos.plantBase.level = level;
                newFolMouPos.merge = false;
                newFolMouPos.rightPlace = false;
                newFolMouPos.putted = false;
                createdPlant = true;
            }
            image.enabled = false;
        }
        if (collision.gameObject.tag == "UiPlant")
        {
            triggerStay = false;
            ismerged = false;
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "UiPlant")
        {
            triggerStay = true;
        }
    }
    private void Update()
    {
        if (triggerStay && ismerged && realease)
        {
            GameObject ui = Instantiate(uiPlantPrefab, transform.position, Quaternion.identity);
            ui.transform.SetParent(otherGameObject.transform.parent);
            ui.transform.localScale = Vector3.one;
            gameObject.transform.SetParent(transform.root);
            otherGameObject.transform.SetParent(transform.root);
            realease = false;
            Destroy(otherGameObject);
            Destroy(gameObject);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        FollowMousePos.offHolding?.Invoke();
        image.raycastTarget = true;
        if (_newPlant != null)
        {
            followMousePos = _newPlant.GetComponent<FollowMousePos>();
            if (followMousePos.rightPlace && !followMousePos.inBound)
            {
                Destroy(gameObject);
            }
            else if (!followMousePos.rightPlace || followMousePos.inBound)
            {
                gameObject.transform.SetParent(parentAfterDrag);
                image.enabled = true;
                createdPlant = false;
                _newPlant = null;
            }
        }
        else
        {
            gameObject.transform.SetParent(parentAfterDrag);
            image.enabled = true;
            createdPlant = false;
            _newPlant = null;
        }
        realease = true;
        DOVirtual.DelayedCall(0.1f, () => { realease = false; });
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        FollowMousePos.nameID = nameUiPlant;
        //gameObject.transform.SetParent(parentAfterDrag);
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        FollowMousePos.onHolding?.Invoke();
        image.raycastTarget = false;
        if (triggerStay == false && ismerged)
        {
            ismerged = false;
        }
    }
}
