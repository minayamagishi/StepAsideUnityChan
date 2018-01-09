using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //◆課題◆不要になったアイテムを順次破棄（画面外に出たアイテムを直ちに破棄）
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

}
