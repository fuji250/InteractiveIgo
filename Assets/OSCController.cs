using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uOSC;


public class OSCController : MonoBehaviour
{
    // Start is called before the first frame update

    Camera cam;

    //ParticleSystem型を変数psで宣言します。
    public GameObject ps;
    //GameObject型で変数objを宣言します。
    GameObject obj;
    //マウスでクリックされた位置が格納されます。
    private Vector3 mousePosition;
    private Vector3 sensorPosition;

    void Start()
    {
        var server = GetComponent<uOscServer>();
        server.onDataReceived.AddListener(OnDataReceived);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDataReceived(Message message)
    {
//        Debug.Log(message.ToString());

        if(message.address == "/pos") {

            if(float.Parse(message.values[0].ToString()) > 0.04 || float.Parse(message.values[0].ToString()) < -0.25
                || float.Parse(message.values[1].ToString()) > 0.9 || float.Parse(message.values[1].ToString()) < 0.01)
            {
                return;
            }
            float X = float.Parse(message.values[0].ToString()) ;
            float Y = float.Parse(message.values[1].ToString());
            //Debug.Log(X);

            float a, b, c, d, e, f, g, h;
            a = -6.89655172f;
            b = -2.08640961f;
            c = -1.72413793f;
            d = 5.96908144f;
            e = 1.14942529f;
            f = -2.00000000f;
            g = -4.44089210f;
            h = 8.32667268f;

            float HomoX = a * X + b * Y + c;
            float HomoY = d * X + e * Y + f;
            float HomoZ = g * X + h * Y + 1;

            //Debug.Log(HomoZ);

            X = HomoX / HomoZ;
            Y = HomoY / HomoZ;

            //Debug.Log(X + ", " + Y);
            //マウスカーソルの位置を取得。
            mousePosition = Input.mousePosition;
            mousePosition.z = 3f;
            sensorPosition = new Vector3(X,Y,0);
            //Debug.Log(sensorPosition);
            //Instantiate(ps, Camera.main.ScreenToWorldPoint(sensorPosition),
            //    Quaternion.identity);
            Instantiate(ps, sensorPosition, Quaternion.identity);

            PosCheck(sensorPosition, X, Y);
        }
        //var msg = message.address + ": ";

        //// arguments (object array)
        //foreach (var value in message.values)
        //{
        //    if (value is int) msg += (int)value;
        //    else if (value is float) msg += (float)value;
        //    else if (value is string) msg += (string)value;
        //    else if (value is bool) msg += (bool)value;
        //    else if (value is byte[]) msg += "byte[" + ((byte[])value).Length + "]";
        //}

        //Debug.Log(msg);
    }

    public void PosCheck(Vector3 Pos,float X, float Y)
    {
        GameObject clickedGameObject = null;

        Vector3 vec3 = new Vector3(0,2.86f,-10);
        Vector3 dir = new Vector3(X,Y,1);

 
            // 光線を飛ばす
            //Ray ray = Camera.main.ScreenPointToRay(Pos);
            Ray ray = new Ray(vec3,dir);
            Debug.DrawRay (ray.origin, ray.direction * 1000f, Color.red, 1f, false);
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
    }
}
