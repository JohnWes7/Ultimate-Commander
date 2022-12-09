using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRTSController : MonoBehaviour
{
    [SerializeField]
    private Vector3 startSelectPos;
    private Vector3 startMousePos;
    [SerializeField]
    private Vector3 endSelectPos;
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

                    //设置所有单位去该去的位置
                    foreach (var item in selectedUnits)
                    {
                        IMove move;
                        if (item.gameObject.TryGetComponent<IMove>(out move))
                        {
                            move.Move(dest);
                        }
                    }
                }
            }
        }
    }

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
            startSelectPos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, Camera.main.transform.position.y));
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            // 更改选择框的位置大小
            endSelectPos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, Camera.main.transform.position.y));
            float tempx = Mathf.Abs(startMousePos.x - Input.mousePosition.x);
            float tempy = Mathf.Abs(startMousePos.y - Input.mousePosition.y);

            selectRect.transform.position = (startMousePos + Input.mousePosition) / 2;
            (selectRect.transform as RectTransform).sizeDelta = new Vector2(tempx, tempy);

            // 碰撞检测选择单位
            endSelectPos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, Camera.main.transform.position.y));
            tempx = Mathf.Abs(startSelectPos.x - endSelectPos.x);
            float tempz = Mathf.Abs(startSelectPos.z - endSelectPos.z);

            Vector3 startV = startSelectPos - Camera.main.transform.position;
            Vector3 endV = endSelectPos - Camera.main.transform.position;
            Vector3 middir = Vector3.Lerp(startV, endV, 0.5f);

            RaycastHit[] raycastHits = Physics.BoxCastAll(Camera.main.transform.position, new Vector3(tempx, 1, tempz), middir, Quaternion.identity, 20);

            foreach (UnitController item in selectedUnits)
            {
                item.SetSelectedAni(false);
            }
            selectedUnits.Clear();

            foreach (RaycastHit item in raycastHits)
            {
                UnitController unitController = null;
                if (item.collider.gameObject.TryGetComponent<UnitController>(out unitController))
                {
                    unitController.SetSelectedAni(true);
                    selectedUnits.Add(unitController);
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            // 关闭选择框
            selectRect.SetActive(false);

            // 碰撞检测选择单位
            endSelectPos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, Camera.main.transform.position.y));
            float tempx = Mathf.Abs(startSelectPos.x - endSelectPos.x);
            float tempz = Mathf.Abs(startSelectPos.z - endSelectPos.z);

            Vector3 startV = startSelectPos - Camera.main.transform.position;
            Vector3 endV = endSelectPos - Camera.main.transform.position;
            Vector3 middir = Vector3.Lerp(startV, endV, 0.5f);

            RaycastHit[] raycastHits = Physics.BoxCastAll(Camera.main.transform.position, new Vector3(tempx, 1, tempz), middir, Quaternion.identity, 20);

            foreach (UnitController item in selectedUnits)
            {
                item.SetSelectedAni(false);
            }
            selectedUnits.Clear();

            selectedUnits.Clear();
            foreach (RaycastHit item in raycastHits)
            {
                UnitController unitController = null;
                if (item.collider.gameObject.TryGetComponent<UnitController>(out unitController))
                {
                    selectedUnits.Add(unitController);
                    unitController.SetSelectedAni(true);
                }
            }
            
        }
    }

    public void debugselectline()
    {
        Debug.DrawLine(Camera.main.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 100)), Color.red);
    }
}
