using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickExploder : MonoBehaviour
{
    //ParticleSystem�^��ϐ�ps�Ő錾���܂��B
    public GameObject ps;
    //GameObject�^�ŕϐ�obj��錾���܂��B
    GameObject obj;
    //�}�E�X�ŃN���b�N���ꂽ�ʒu���i�[����܂��B
    private Vector3 mousePosition;

    
    void Start()
    {
        //Find���\�b�h��explode��GameObject�ɃA�N�Z�X����
        //�ϐ�obj�ŎQ�Ƃ��܂��B
        //obj = GameObject.Find("explode");
        //GetComponentInChildren�Ŏq�v�f���܂߂�
        //ParticleSystem�ɃA�N�Z�X���ĕϐ�ps�ŎQ�Ƃ��܂��B
        //ps = obj.GetComponentInChildren<ParticleSystem>();
        //�ϐ�obj���\���ɂ��ăp�[�e�B�N���̍Đ����~�߂܂��B
        //obj.SetActive(false);
        //ps.Stop();
        
    }

    
    void Update()
    {
        //�}�E�X�̍��N���b�N���ꂽ���̏����B
        if (Input.GetMouseButtonDown(0))
        {
            //�}�E�X�J�[�\���̈ʒu���擾�B
            mousePosition = Input.mousePosition;
            mousePosition.z = 3f;
           Instantiate(ps, Camera.main.ScreenToWorldPoint(mousePosition),
                Quaternion.identity);
            //�N���b�N���ꂽ�ʒu�Ƀp�[�e�B�N�����Đ����܂��B
            //obj.SetActive(true);
            //ps.Play();
           
        }
        
    }
}