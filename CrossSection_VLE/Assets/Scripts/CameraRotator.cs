using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CameraRotator : MonoBehaviour{
    // カメラオブジェクトを格納する変数
    public Camera mainCamera;
    // カメラの回転速度を格納する変数
    public Vector2 rotationSpeed;
    // マウス移動方向とカメラ回転方向を反転する判定フラグ
    public bool reverse;
    // マウス座標を格納する変数
    private Vector2 lastMousePosition;
    // カメラの角度を格納する変数（初期値に0,0を代入）
    private Vector2 newAngle = new Vector2(0, 0);


    public Vector3 velocity;
    public float moveSpeed;

	private void Start(){
        rotationSpeed = new Vector2(0.1f, 0.2f);

        // WASD入力から、XZ平面(水平な地面)を移動する方向(velocity)を得ます
        //velocity = transform.position;
        moveSpeed = 0.3f;

	}

	// ゲーム実行中の繰り返し処理
	void Update()
    {
        // 左クリックした時
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("左クリック");

            // カメラの角度を変数"newAngle"に格納
            newAngle = mainCamera.transform.localEulerAngles;
            // マウス座標を変数"lastMousePosition"に格納
            lastMousePosition = Input.mousePosition;
        }

        // 左ドラッグしている間
        else if (Input.GetMouseButton(0))
        {
            //カメラ回転方向の判定フラグが"true"の場合
            if (!reverse)
            {
                //Debug.Log("左ドラッグ");

                // Y軸の回転：マウスドラッグ方向に視点回転
                // マウスの水平移動値に変数"rotationSpeed"を掛ける
                //（クリック時の座標とマウス座標の現在値の差分値）
                newAngle.y -= (lastMousePosition.x - Input.mousePosition.x) * rotationSpeed.y;
                // X軸の回転：マウスドラッグ方向に視点回転
                // マウスの垂直移動値に変数"rotationSpeed"を掛ける
                //（クリック時の座標とマウス座標の現在値の差分値）
                newAngle.x -= (Input.mousePosition.y - lastMousePosition.y) * rotationSpeed.x;
                // "newAngle"の角度をカメラ角度に格納
                mainCamera.transform.localEulerAngles = newAngle;
                // マウス座標を変数"lastMousePosition"に格納
                lastMousePosition = Input.mousePosition;
            }
            // カメラ回転方向の判定フラグが"reverse"の場合
            else if (reverse)
            {
                Debug.Log("左ドラッグ");

                // Y軸の回転：マウスドラッグと逆方向に視点回転
                newAngle.y -= (Input.mousePosition.x - lastMousePosition.x) * rotationSpeed.y;
                // X軸の回転：マウスドラッグと逆方向に視点回転
                newAngle.x -= (lastMousePosition.y - Input.mousePosition.y) * rotationSpeed.x;
                // "newAngle"の角度をカメラ角度に格納
                mainCamera.transform.localEulerAngles = newAngle;
                // マウス座標を変数"lastMousePosition"に格納
                lastMousePosition = Input.mousePosition;
            }//if(reverse)

        }//if (Input.GetMouseButton(0) )

        else if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * moveSpeed;
            //velocity.x -= 1;

        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * moveSpeed;
            //velocity.z -= 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * moveSpeed;
            //velocity.x += 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * moveSpeed;
            //velocity.z += 1;
        }

        // 速度ベクトルの長さを1秒でmoveSpeedだけ進むように調整します
        velocity = velocity.normalized * moveSpeed * Time.deltaTime;

        // いずれかの方向に移動している場合
        if (velocity.magnitude > 0)
        {
            // プレイヤーの位置(transform.position)の更新
            // 移動方向ベクトル(velocity)を足し込みます
            transform.localPosition += velocity;
        }




    }//Update()
 
    // マウスドラッグ方向と視点回転方向を反転する処理
    public void DirectionChange()
    {
        // 判定フラグ変数"reverse"が"false"であれば
        if (!reverse)
        {
            // 判定フラグ変数"reverse"に"true"を代入
            reverse = true;
        }
        // でなければ（判定フラグ変数"reverse"が"true"であれば）
        else
        {
            // 判定フラグ変数"reverse"に"false"を代入
            reverse = false;
        }
    }
}