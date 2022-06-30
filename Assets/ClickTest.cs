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
 
            // �������΂�
            Ray ray = Camera.main.ScreenPointToRay(sensorPos);
            Debug.DrawRay (ray.origin, ray.direction * 20f, Color.red, 1f, true);
            RaycastHit hit = new RaycastHit();
 
            // ���������ɓ���������
            if (Physics.Raycast(ray, out hit))
            {
                // �I�u�W�F�N�g���擾����
                clickedGameObject = hit.collider.gameObject;
                Debug.Log("switch clicked");
            
            }
            else {
                return;
            }
 
            // �X�C�b�`�������珈�����s���B
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
        // Line�I�u�W�F�N�g�̐���
    GameObject beam = Instantiate(BeamPrefab,
                        new Vector3(0, 0, 0),
                        Quaternion.identity) as GameObject;
                    
    // LineRenderer�擾
    LineRenderer line = beam.GetComponent<LineRenderer>();

        // ���_���̐ݒ�
    line.positionCount = PointList.Count;

    // �e���_�̍��W�ݒ�
    for (int iCnt = 0; iCnt < PointList.Count; iCnt++)
    {
        line.SetPosition(iCnt, PointList[iCnt]);
    }
    }
}
