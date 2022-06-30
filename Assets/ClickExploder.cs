using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickExploder : MonoBehaviour
{
    //ParticleSystem型を変数psで宣言します。
    public GameObject ps;
    //GameObject型で変数objを宣言します。
    GameObject obj;
    //マウスでクリックされた位置が格納されます。
    private Vector3 mousePosition;

    
    void Start()
    {
        //FindメソッドでexplodeのGameObjectにアクセスして
        //変数objで参照します。
        //obj = GameObject.Find("explode");
        //GetComponentInChildrenで子要素も含めた
        //ParticleSystemにアクセスして変数psで参照します。
        //ps = obj.GetComponentInChildren<ParticleSystem>();
        //変数objを非表示にしてパーティクルの再生を止めます。
        //obj.SetActive(false);
        //ps.Stop();
        
    }

    
    void Update()
    {
        //マウスの左クリックされた時の処理。
        if (Input.GetMouseButtonDown(0))
        {
            //マウスカーソルの位置を取得。
            mousePosition = Input.mousePosition;
            mousePosition.z = 3f;
           Instantiate(ps, Camera.main.ScreenToWorldPoint(mousePosition),
                Quaternion.identity);
            //クリックされた位置にパーティクルを再生します。
            //obj.SetActive(true);
            //ps.Play();
           
        }
        
    }
}