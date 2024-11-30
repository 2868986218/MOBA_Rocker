
using System;
using UnityEngine;
using UnityEngine.EventSystems;

//接口隔离原则，需要什么功能实现对应接口即可

/// <summary>
/// 将此脚本挂载在UI物体上实现UI监听 并分配UI监听委托
/// </summary>
public class SUListener :
    MonoBehaviour,
    IPointerClickHandler,
    IPointerDownHandler,
    IPointerUpHandler,
    IDragHandler {
    public Action<PointerEventData, object[]> onClick;
    public Action<PointerEventData, object[]> onClickDown;
    public Action<PointerEventData, object[]> onClickUp;
    public Action<PointerEventData, object[]> onDrag;

    public object[] args = null;

    public void OnPointerClick(PointerEventData eventData) {
        onClick?.Invoke(eventData, args);
    }
    public void OnPointerDown(PointerEventData eventData) {
        onClickDown?.Invoke(eventData, args);
    }
    public void OnPointerUp(PointerEventData eventData) {
        onClickUp?.Invoke(eventData, args);
    }
    public void OnDrag(PointerEventData eventData) {
        onDrag?.Invoke(eventData, args);
    }
}
