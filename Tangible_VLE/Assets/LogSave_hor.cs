using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class LogSave_hor : MonoBehaviour{
	public StreamWriter sw;
	public FileInfo fi;
	public DateTime thisDay;
	public string fileName;
    public Vector3 pos;
    public Vector3 rot;
    public String tempData;

    void Start(){
        pos = transform.position;
        rot = transform.eulerAngles;
        tempData = "";

        startLogSave();
    }

    void Update(){
        pos = transform.position;
        rot = transform.eulerAngles;
        tempData = (pos.x).ToString() + "," + (pos.y).ToString() + "," + (pos.z).ToString()
            + "," + (rot.x).ToString() + "," + (rot.y).ToString() + "," + (rot.z).ToString();

        logSave(tempData);

    }


	public void startLogSave(){
		thisDay = DateTime.Now;
		fileName = thisDay.ToString("yyMMddHHmm") + "_" + gameObject.name;
		fi = new FileInfo(Application.dataPath + "/Data_hor/" + fileName + ".csv");
		sw = fi.AppendText();
		sw.WriteLine("time" + "," + "X" + "," + "Y" + "," + "Z" + "," + "X" + "," + "Y" + "," + "Z");
		sw.Flush();
		sw.Close();
	}

	public void logSave(string data){
		sw = fi.AppendText();
		sw.WriteLine(data);
		sw.Flush();
		sw.Close();
	}
}