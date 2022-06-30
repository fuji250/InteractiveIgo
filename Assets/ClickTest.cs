using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickTest : MonoBehaviour
{
    public GameObject BeamPrefab;
    public List<Vector3> PointList;

    // Start is called before the first frame update
    void Start()
    {
        GameObject clickedGameObject = null;

        Vector3 sensorPos = new Vector3(0,0,0);
 
            // 光線を飛ばす
            Ray ray = Camera.main.ScreenPointToRay(sensorPos);
            Debug.DrawRay (ray.origin, ray.direction * 20f, Color.red, 1f, true);
            RaycastHit hit = new RaycastHit();
 
            // もし何かに当たったら
            if (Physics.Raycast(ray, out hit))
            {
                // オブジェクトを取得する
                clickedGameObject = hit.collider.gameObject;
                Debug.Log("switch clicked");
            
            }
            else {
                return;
            }
 
            // スイッチだったら処理を行う。
            if (clickedGameObject.layer == 5)
            {
                clickedGameObject.GetComponent<Image>().enabled = false;
                Debug.Log(clickedGameObject.layer);
            }
            Line();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Line()
    {
        // Lineオブジェクトの生成
    GameObject beam = Instantiate(BeamPrefab,
                        new Vector3(0, 0, 0),
                        Quaternion.identity) as GameObject;
                    
    // LineRenderer取得
    LineRenderer line = beam.GetComponent<LineRenderer>();

        // 頂点数の設定
    line.positionCount = PointList.Count;

    // 各頂点の座標設定
    for (int iCnt = 0; iCnt < PointList.Count; iCnt++)
    {
        line.SetPosition(iCnt, PointList[iCnt]);
    }
    }
}
