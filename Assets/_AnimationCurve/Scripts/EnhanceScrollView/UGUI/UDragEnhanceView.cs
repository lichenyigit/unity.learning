using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UDragEnhanceView : EventTrigger
{
    private EnhanceScrollView enhanceScrollView;
    public void SetScrollView(EnhanceScrollView view)
    {
        enhanceScrollView = view;
    }

    
    public void OnBeginDrag(PointerEventData eventData)
    {
        //base.OnBeginDrag(eventData);
    }
    

    public void OnDrag(PointerEventData eventData)
    {
        //base.OnDrag(eventData);
        if (enhanceScrollView != null)
            enhanceScrollView.OnDragEnhanceViewMove(eventData.delta);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //base.OnEndDrag(eventData);
        if (enhanceScrollView != null)
            enhanceScrollView.OnDragEnhanceViewEnd();
    }
}
