
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RockerWnd : WndRoot
{
    public Image imgTouch;//遥感可点击范围图片
    public Image imgDirBg;//整个遥感根节点
    public Image imgDirPoint;//遥感中心点
    public Transform ArrowRoot;//指引方向箭头

    float pointDis = 135;
    Vector2 startPos = Vector2.zero;//开始点击位置为开始位置为中心
    Vector2 defaultPos = Vector2.zero;//还原遥感位置

    private void OnEnable()
    {
        InitWnd();
    }
    private void OnDisable()
    {
        UnInitWnd();
    }

    protected override void InitWnd()
    {
        base.InitWnd();
        //pointDis = Screen.height * 1.0f / ClientConfig.ScreenStandardHeight * ClientConfig.ScreenOPDis;
        pointDis = 135;
        defaultPos = imgDirBg.transform.position;

        RegisterMoveEvts();
    }
    protected override void UnInitWnd()
    {

    }

    /// <summary>
    /// 对移动遥感的事件监听器
    /// </summary>
    void RegisterMoveEvts()
    {
        SetActive(ArrowRoot, false);

        OnClickDown(imgTouch.gameObject, (PointerEventData evt, object[] args) => {
            startPos = evt.position;
            imgDirPoint.color = new Color(1, 1, 1, 1f);//高亮
            imgDirBg.transform.position = evt.position;
        });
        OnClickUp(imgTouch.gameObject, (PointerEventData evt, object[] args) => {
            imgDirBg.transform.position = defaultPos;
            imgDirPoint.color = new Color(1, 1, 1, 0.5f);//取消高亮 增加透明度
            imgDirPoint.transform.localPosition = Vector2.zero;
            SetActive(ArrowRoot, false);
        });
        OnDrag(imgTouch.gameObject, (PointerEventData evt, object[] args) => {
            Vector2 dir = evt.position - startPos;
            float len = dir.magnitude;
            //摇杆滑动位置超出范围
            if (len > pointDis)
            {
                //向量模长钳制范围，最大模长为pointDis，防止遥感中心点imgDirPoint超出范围
                Vector2 clampDir = Vector2.ClampMagnitude(dir, pointDis);
                imgDirPoint.transform.position = startPos + clampDir;//这才是更新摇杆位置
            }
            else
            {
                imgDirPoint.transform.position = evt.position;//没有超出范围，就是手滑动的位置
            }
            //说明有移动方向箭头显示，更新逻辑层数据
            //产生了拖动
            if (dir != Vector2.zero)
            {
                SetActive(ArrowRoot);
                float angle = Vector2.SignedAngle(new Vector2(1, 0), dir);//获得方向角度 以x轴正方向偏转的角度
                ArrowRoot.localEulerAngles = new Vector3(0, 0, angle);//以z轴为中心做旋转
            }
        });
    }
}
