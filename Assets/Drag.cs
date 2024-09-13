using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour,IBeginDragHandler, IDragHandler,IEndDragHandler, IPointerDownHandler
{
    Transform parentToReturnTo=null;
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    public void Awake(){
        //rectTransform=GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData){
        parentToReturnTo=this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);
        GetComponent<CanvasGroup>().alpha=.8f;
        GetComponent<CanvasGroup>().blocksRaycasts=false;
    }
    public void OnDrag(PointerEventData eventData){
        this.transform.position=eventData.position;
        //rectTransform.anchoredPosition+=eventData.delta/canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData){
        GameObject PlayArea=GameObject.Find("PlayArea");
        if((PlayArea.transform.position.y>this.transform.position.y || PlayArea.transform.position.y<this.transform.position.y)){
            this.transform.SetParent(parentToReturnTo);                  
            Debug.Log(this.transform.position.y);
        }
             
        GetComponent<CanvasGroup>().alpha=1f;
        GetComponent<CanvasGroup>().blocksRaycasts=true;
    }
    public void OnPointerDown(PointerEventData eventData){

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
