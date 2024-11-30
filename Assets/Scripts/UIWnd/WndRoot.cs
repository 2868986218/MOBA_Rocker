using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WndRoot : MonoBehaviour
{
    public virtual void SetWndState(bool isActive = true)
    {
        if (gameObject.activeSelf != isActive)
        {
            gameObject.SetActive(isActive);
        }
        if (isActive)
        {
            InitWnd();
        }
        else
        {
            UnInitWnd();
        }
    }

    /// <summary>
    /// 给每个窗口持有 服务的引用
    /// </summary>
    protected virtual void InitWnd()
    {
    }

    protected virtual void UnInitWnd()
    {
    }


    //对UI添加点击、滑动、拖拽事件，利用自研UI事件监听器
    protected void OnClick(GameObject go, Action<PointerEventData, object[]> clickCB, params object[] args)
    {
        SUListener listener = GetOrAddComponent<SUListener>(go);
        listener.onClick = clickCB;
        if (args != null)
        {
            listener.args = args;
        }
    }
    protected void OnClickDown(GameObject go, Action<PointerEventData, object[]> clickDownCB, params object[] args)
    {
        SUListener listener = GetOrAddComponent<SUListener>(go);
        listener.onClickDown = clickDownCB;
        if (args != null)
        {
            listener.args = args;
        }
    }
    protected void OnClickUp(GameObject go, Action<PointerEventData, object[]> clickUpCB, params object[] args)
    {
        SUListener listener = GetOrAddComponent<SUListener>(go);
        listener.onClickUp = clickUpCB;
        if (args != null)
        {
            listener.args = args;
        }
    }
    protected void OnDrag(GameObject go, Action<PointerEventData, object[]> dragCB, params object[] args)
    {
        SUListener listener = GetOrAddComponent<SUListener>(go);
        listener.onDrag = dragCB;
        if (args != null)
        {
            listener.args = args;
        }
    }

    /// <summary>
    /// 获取或者添加组件，获取gameobject的组件，如果没有就给它添加
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="go"></param>
    /// <returns></returns>
    private T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        T t = go.GetComponent<T>();
        if (t == null)
        {
            t = go.AddComponent<T>();
        }
        return t;
    }

    //对各种ui组件设置显示和隐藏
    protected void SetActive(GameObject go, bool state = true)
    {
        go.SetActive(state);
    }
    protected void SetActive(Transform trans, bool state = true)
    {
        trans.gameObject.SetActive(state);
    }
    protected void SetActive(RectTransform rectTrans, bool state = true)
    {
        rectTrans.gameObject.SetActive(state);
    }
    protected void SetActive(Image img, bool state = true)
    {
        img.gameObject.SetActive(state);
    }
    protected void SetActive(Text txt, bool state = true)
    {
        txt.gameObject.SetActive(state);
    }
    protected void SetActive(InputField ipt, bool state = true)
    {
        ipt.gameObject.SetActive(state);
    }

    //对文本组件设置内容
    protected void SetText(Transform trans, int num = 0)
    {
        SetText(trans.GetComponent<Text>(), num.ToString());
    }
    protected void SetText(Transform trans, string context = "")
    {
        SetText(trans.GetComponent<Text>(), context);
    }
    protected void SetText(Text txt, int num = 0)
    {
        SetText(txt, num.ToString());
    }
    protected void SetText(Text txt, string context = "")
    {
        txt.text = context;
    }


    protected Transform GetTrans(Transform trans, string name)
    {
        if (trans != null)
        {
            return trans.Find(name);
        }
        else
        {
            return transform.Find(name);
        }
    }
    protected Image GetImage(Transform trans, string path)
    {
        if (trans != null)
        {
            return trans.Find(path).GetComponent<Image>();
        }
        else
        {
            return transform.Find(path).GetComponent<Image>();
        }
    }
    protected Image GetImage(Transform trans)
    {
        if (trans != null)
        {
            return trans.GetComponent<Image>();
        }
        else
        {
            return transform.GetComponent<Image>();
        }
    }
    protected Text GetText(Transform trans, string path)
    {
        if (trans != null)
        {
            return trans.Find(path).GetComponent<Text>();
        }
        else
        {
            return transform.Find(path).GetComponent<Text>();
        }
    }
}
