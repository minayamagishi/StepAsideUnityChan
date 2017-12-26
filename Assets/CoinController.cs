﻿using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {

	// Use this for initialization
	void Start () {

        //回転を開始する確度を設定
        //すべてのコインの回転が揃わないように、Random.Range関数を使って回転を開始する角度をランダムに設定
        this.transform.Rotate(0, Random.Range(0, 360), 0);
		
	}
	
	// Update is called once per frame
	void Update () {

        //回転
        //Rotate関数を使ってY軸を中心にコインが回転し続けるように設定
        this.transform.Rotate(0, 3, 0);

	}
}
