using UnityEngine;
using System.Collections;
//[ExecuteInEditMode]
public class OnePlaneCuttingController : MonoBehaviour {

    public GameObject plane; //インスペクタから，Quad指定
    Material mat;
    public Vector3 normal;
    public Vector3 position;
    public Renderer rend; //このスクリプトがアタッチされたオブジェクトのMeshRenderer

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>(); //このスクリプトがアタッチされたオブジェクトの
        normal = plane.transform.TransformVector(new Vector3(0,0,-1));
        position = plane.transform.position;
        UpdateShaderProperties();
    }//Start



    //毎フレームShaderを書き換えてる
    void Update (){
        UpdateShaderProperties();
    }//Update



    private void UpdateShaderProperties(){
        
        //planeオブジェクトのベクトルをローカルからワールドへ変換して法線ベクトルとする
        normal = plane.transform.TransformVector(new Vector3(0, 0, -1));
        //planeオブジェクトのposition
        position = plane.transform.position;

        //このスクリプトがアタッチされたオブジェクトのレンダラの長さに合わせて計算
        for(int i=0;i<rend.materials.Length;i++){

            //レンダラ全てに，
            if(rend.materials[i].shader.name == "CrossSection/OnePlaneBSP"){
                rend.materials[i].SetVector("_PlaneNormal", normal);
                rend.materials[i].SetVector("_PlanePosition", position);
            }else{
                //Debug.Log("aaa");
            }
        }//for
        
    }//UpdateShaderProperties()

}//class
