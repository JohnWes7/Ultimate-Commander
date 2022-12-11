using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 负责选取物体移动
/// </summary>
public class GameRTSController : MonoBehaviour
{
    [SerializeField]
    private Vector3 startMousePos;
    [SerializeField]
    private Vector3 endMousePos;
    [SerializeField]
    private List<UnitController> selectedUnits = new List<UnitController>();
    private GameObject selectRect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SelectUnit();
        MoveUnit();
    }

    private void MoveUnit()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (selectedUnits.Count != 0)
            {
                // 通过射线找到 目标位置
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // 射线只监测ground层
                int layer = 1 << LayerMask.NameToLayer("Ground");
                RaycastHit raycastHit;

                if (Physics.Raycast(ray, out raycastHit, 1000, layer))
                {
                    Vector3 dest = raycastHit.point;
                    List<Vector3> poss = GetSquareDestPos(dest, selectedUnits.Count, 2);

                    //设置所有单位去该去的位置
                    int i = 0;
                    foreach (var item in selectedUnits)
                    {
                        IMove move;
                        if (item.gameObject.TryGetComponent<IMove>(out move))
                        {
                            move.SetMoveDest(poss[i]);
                            i++;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 创造一个正方形的位置矩阵
    /// </summary>
    /// <param name="dest"></param>
    /// <param name="number"></param>
    /// <param name="interval"></param>
    /// <returns></returns>
    private List<Vector3> GetSquareDestPos(Vector3 dest, int number, float interval)
    {
        int sqrt = Mathf.CeilToInt(Mathf.Sqrt(number));
        int n = 0;
        float halfsquare = (sqrt - 1) * interval / 2;
        //Debug.Log(sqrt);
        //Debug.Log(halfsquare);
        List<Vector3> positions = new List<Vector3>();
        for (int i = sqrt - 1; i >= 0; i--)
        {
            for (int j = sqrt - 1; j >= 0; j--)
            {
                Vector3 pos = new Vector3(j * interval - halfsquare + dest.x, dest.y, i * interval - halfsquare + dest.z);
                //Debug.Log(pos);
                //Debug.Log(dest);
                positions.Add(pos);
                n++;

                if (n >= number)
                {
                    break;
                }
            }

            if (n >= number)
            {
                break;
            }
        }

        return positions;
    }

    #region 框选方法
    public void SelectUnit()
    {
        // 监测有没有选择框ui
        if (selectRect == null)
        {
            GameObject prefab = Resources.Load<GameObject>(ResourcesPath.GameRTSController_selectRect_PATH);
            selectRect = Instantiate<GameObject>(prefab, GameObject.Find("Canvas").transform);
            selectRect.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // 打开选择框ui 记录一开始鼠标位置和指向的向量
            selectRect.SetActive(true);
            startMousePos = Input.mousePosition;
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            // 更改选择框的位置大小
            float tempx = Mathf.Abs(startMousePos.x - Input.mousePosition.x);
            float tempy = Mathf.Abs(startMousePos.y - Input.mousePosition.y);

            selectRect.transform.position = (startMousePos + Input.mousePosition) / 2;
            (selectRect.transform as RectTransform).sizeDelta = new Vector2(tempx, tempy);

            // 碰撞检测选择单位
            endMousePos = Input.mousePosition;
            RaycastHit[] raycastHits = SelectRectCast(startMousePos, Input.mousePosition);

            // 用raycast更新unitslist
            UpdateSelectedUnitsList(raycastHits);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            // 关闭选择框
            selectRect.SetActive(false);

            // 碰撞检测选择单位
            endMousePos = Input.mousePosition;
            RaycastHit[] raycastHits = SelectRectCast(startMousePos, Input.mousePosition);

            // 用raycast更新unitslist
            UpdateSelectedUnitsList(raycastHits);
        }
    }

    private RaycastHit[] SelectRectCast(Vector3 startMousePos, Vector3 endMousePos)
    {
        Vector3 startSelectPos = Camera.main.ScreenToWorldPoint(startMousePos + Camera.main.transform.position.y * Vector3.forward);
        Vector3 endSelectPos = Camera.main.ScreenToWorldPoint(endMousePos + Camera.main.transform.position.y * Vector3.forward);
        
        float tempx = Mathf.Abs(startSelectPos.x - endSelectPos.x);
        float tempz = Mathf.Abs(startSelectPos.z - endSelectPos.z);

        Vector3 startV = startSelectPos - Camera.main.transform.position;
        Vector3 endV = endSelectPos - Camera.main.transform.position;
        Vector3 middir = Vector3.Lerp(startV, endV, 0.5f);

        RaycastHit[] raycastHits = Physics.BoxCastAll(Camera.main.transform.position, new Vector3(tempx, 1, tempz), middir, Quaternion.identity, middir.magnitude + 10);

        return raycastHits;
    }

    private void UpdateSelectedUnitsList(RaycastHit[] raycastHits)
    {
        foreach (UnitController item in selectedUnits)
        {
            item.SetSelectedAni(false);
        }
        selectedUnits.Clear();

        foreach (RaycastHit item in raycastHits)
        {
            // 二次判断是否在框选框中被选中
            Vector3 spos = Camera.main.WorldToScreenPoint(item.transform.position);
            if ((spos.x < startMousePos.x && spos.x < Input.mousePosition.x) || (spos.x > startMousePos.x && spos.x > Input.mousePosition.x))
            {
                continue;
            }
            if ((spos.y < startMousePos.y && spos.y < Input.mousePosition.y) || (spos.y > startMousePos.y && spos.y > Input.mousePosition.y))
            {
                continue;
            }

            UnitController unitController = null;
            if (item.collider.gameObject.TryGetComponent<UnitController>(out unitController))
            {
                unitController.SetSelectedAni(true);
                selectedUnits.Add(unitController);
            }
        }
    }
    #endregion

    public void debugselectline()
    {
        Debug.DrawLine(Camera.main.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 100)), Color.red);
    }
}
