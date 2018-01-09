using UnityEngine;
using System.Collections;

public class ItemGenerator : MonoBehaviour {

    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //conePrefabを入れる
    public GameObject conePrefab;

    //スタート地点
    private int startPos = -160;
    //ゴール地点
    private int goalPos = 120;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;

    //◆発展課題◆スタート時に見えている画面範囲の地点（50m先まで）
    private int visiblePos = -110;

    //◆発展課題◆Unityちゃんの位置取得用
    private float lastUnityChanPos = 0;

    //◆発展課題◆Unityちゃんのオブジェクト取得用
    private GameObject unitychan;

    // Use this for initialization
    //◆発展課題◆初期画面内のみにアイテムを出すように変更
    void Start () {

        //◆発展課題◆Unityちゃんのゲームオブジェクトを取得
        unitychan = GameObject.Find("unitychan");

        //◆発展課題◆UnityちゃんのZ座標を取得
        lastUnityChanPos = unitychan.transform.position.z;

        //一定の距離ごとにアイテムを生成　◆発展課題◆見える範囲に限定
        for (int i = startPos; i < visiblePos; i += 15)
        {
            //どのアイテムを出すのかをランダムに設定
            int num = Random.Range(0, 10);

            if (num <= 1)
            {
                //コーンをx軸方向に一直線に生成
                for (float j = -1; j <= 1; j += 0.4f)
                {
                    GameObject cone = Instantiate(conePrefab) as GameObject;
                    cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
                }
            }
            else
            {
                //レーンごとにアイテムを生成
                for (int j = -1; j < 2; j++)
                {
                    //アイテムの種類を決める
                    int item = Random.Range(1, 11);
                    //アイテムを置くZ座標のオフセットをランダムに設定
                    int offsetZ = Random.Range(-5, 6);
                    //60%コイン配置:30%車配置:10%何もなし
                    if (1 <= item && item <= 6)
                    {
                        //コインを生成
                        GameObject coin = Instantiate(coinPrefab) as GameObject;
                        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i + offsetZ);
                    }
                    else if (7 <= item && item <= 9)
                    {
                        //車を生成
                        GameObject car = Instantiate(carPrefab) as GameObject;
                        car.transform.position = new Vector3(posRange * j, car.transform.position.y, i + offsetZ);
                    }

                }

            }
        }

    }

    // Update is called once per frame
    //◆発展課題◆ユニティちゃんの位置に応じてアイテムを動的に生成
    void Update () {

        if(unitychan.transform.position.z > lastUnityChanPos + 15.0f)
        {
            GenerateItem(visiblePos + 15);
            visiblePos += 15;
            lastUnityChanPos += 15.0f;
        }

    }

    //◆発展課題◆アイテム生成の関数を切り出し
    void GenerateItem(int zPos)
    {
        //どのアイテムを出すのかをランダムに設定
        int num = Random.Range(0, 10);

        if (num <= 1)
        {
            //コーンをx軸方向に一直線に生成
            for (float j = -1; j <= 1; j += 0.4f)
            {
                GameObject cone = Instantiate(conePrefab) as GameObject;
                cone.transform.position = new Vector3(4 * j, cone.transform.position.y, zPos);
            }
        }
        else
        {
            //レーンごとにアイテムを生成
            for (int j = -1; j < 2; j++)
            {
                //アイテムの種類を決める
                int item = Random.Range(1, 11);
                //アイテムを置くZ座標のオフセットをランダムに設定
                int offsetZ = Random.Range(-5, 6);
                //60%コイン配置:30%車配置:10%何もなし
                if (1 <= item && item <= 6)
                {
                    //コインを生成
                    GameObject coin = Instantiate(coinPrefab) as GameObject;
                    coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, zPos + offsetZ);
                }
                else if (7 <= item && item <= 9)
                {
                    //車を生成
                    GameObject car = Instantiate(carPrefab) as GameObject;
                    car.transform.position = new Vector3(posRange * j, car.transform.position.y, zPos + offsetZ);
                }

            }

        }
    }


}
