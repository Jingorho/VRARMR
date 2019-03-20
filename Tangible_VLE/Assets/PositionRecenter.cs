using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRecenter : MonoBehaviour{

	//必要な GameObjectをグラブします。
	public GameObject avatar; //カメラリッグ
	public GameObject eye; //カメラ

	//ゲーム内のデフォルトポジションとスケールを設定します。
	Vector3 defaultPos;
	public float defaultZ = 0f;
	public float defaultY = 0f;
	public float defaultX = 0f;

	//ポジションOffsetを0に設定します。
	float yDis=0f;
	float xDis=0f;
	float zDis=0f;

	void Start (){

		/*ゲームスタートのとき defaultPosを決めた
		ゲーム内のスタート位置のデフォルト座標をX,Y,Zとして与えます。
		パブリックフロートなのでインスペクターから変更可能です。
		最後にSetPosの関数でゲームスタートのときポジションを
		ゲーム内の初期ポジションにリセンターします。*/

		defaultPos = new Vector3 (defaultX, defaultY, defaultZ);
		SetPos ();
	}

	void Update (){

		//In Gameでリセンターする場合矢印「↑」
		if (Input.GetKey (KeyCode.UpArrow)){
			SetPos ();
		}
	}

	public void SetPos(){

		/*プレイヤーの頭のポジションと デフォルトポジション（ゲーム内での初期位置）の
		 * Offsetから計算しAvatarのポジションにoffsetの値を加えるとプレイヤーの位置と
		 * ゲーム内での初期位置を合わせることができます.
		 * Avatarのポジションはプレイヤーの現在の頭の位置の座標。*/

		yDis = defaultPos.y - eye.transform.position.y;
		xDis = defaultPos.x - eye.transform.position.x;
		zDis = defaultPos.z - eye.transform.position.z;
		avatar.transform.position = avatar.transform.position + new  Vector3 (xDis, yDis, zDis);

	}
}