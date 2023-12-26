using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{

    public GameObject targetItem;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;



    private void Awake()
    {

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

    }


    public void OnBeginDrag(PointerEventData eventData)
    {

        Debug.Log("OnBeginDrag");
        canvasGroup.alpha = .6f;
        //So the ray cast will ignore the item itself.
        canvasGroup.blocksRaycasts = false;
        startPosition = transform.position;
        startParent = transform.parent;
        transform.SetParent(transform.root);
        itemBeingDragged = gameObject;

    }

    public void OnDrag(PointerEventData eventData)
    {
        //So the item will move with our mouse (at same speed)  and so it will be consistant if the canvas has a different scale (other then 1);
        rectTransform.anchoredPosition += eventData.delta;

    }



    public void OnEndDrag(PointerEventData eventData)
    {

        itemBeingDragged = null;

        if (transform.parent == startParent || transform.parent == transform.root)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);

        }

        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //önce þuanki itemin tagini al
            string tag = gameObject.tag;
           
            
            // uygun poziyonaa spwawn et
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            Debug.Log(player.transform.position + new Vector3(0, 1, 0)+ "pozisyonuna " + tag + " tagine sahib bir obje olusturulcak");
            Instantiate(targetItem, player.transform.position + new Vector3(0, 2, 0), Quaternion.identity);
            Destroy(gameObject);
        }
    }



}