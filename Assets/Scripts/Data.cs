using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[Serializable]
public struct CircleData
{
    public string name;
    public float radius;
    public bool rolling;
    public string prefabName;
};


public class Data
{

    //保存文件夹
    private string fileName="aaa.txt";

    private string FolderName
    {
        get
        {
            string path = Application.dataPath;
            int num = path.LastIndexOf("/");
            path = path.Substring(0, num);

            return path;
        }
    }

    private string FileName
    {
        get
        {
            return FolderName + "/" + fileName;
        }
        set
        {
            fileName = value;
        }
    }

    //将Dictionary数据转成json保存到本地文件  
    public void Save(CircleData[] array)
    {
        if (!Directory.Exists(FolderName))
        {
            Directory.CreateDirectory(FolderName);
        }
        Debug.Log(FolderName + FileName);
        FileStream file = new FileStream(FileName, FileMode.Create);
        StreamWriter m_streamWriter = new StreamWriter(file);
        m_streamWriter.Flush();
        // 使用StreamWriter来往文件中写入内容
        m_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);
        // 把richTextBox1中的内容写入文件
        for (int i = 0; i < array.Length; i++)
        {
            m_streamWriter.WriteLine(JsonUtility.ToJson(array[i]));
        }
        //关闭此文件
        m_streamWriter.Flush();
        m_streamWriter.Close();
    }

    ////读取文件及json数据加载到Dictionary中  
    //public List<CircleData> Read()
    //{

    //    if (!Directory.Exists(FolderName))
    //    {
    //        Directory.CreateDirectory(FolderName);
    //        return null;
    //    }
    //    Read1();
    //    if (File.Exists(FileName))
    //    {
    //        List<CircleData> list = new List<CircleData>();
    //        FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
    //        StreamReader m_streamReader = new StreamReader(fs);//中文乱码加上System.Text.Encoding.Default,或则 System.Text.Encoding.GetEncoding("GB2312")
    //        //使用StreamReader类来读取文件
    //        m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
    //        // 从数据流中读取每一行，直到文件的最后一行，并在richTextBox1中显示出内容


    //        string strLine = m_streamReader.ReadLine();
    //        while (strLine != null)
    //        {
    //            list.Add(JsonUtility.FromJson<CircleData>(strLine));
    //            strLine = m_streamReader.ReadLine();
    //        }
    //        //关闭此StreamReader对象
    //        m_streamReader.Close();
    //        return list;
    //    }
    //    return null;
    //}

    public List<CircleData> Read()
    {
        TextAsset textAsset = Resources.Load("aaa") as TextAsset;
        string[] s=textAsset.text.Split('\n');
        List<CircleData> list = new List<CircleData>();
        foreach (string str in s)
        {
            list.Add(JsonUtility.FromJson<CircleData>(str));
        }
        return list;
    }
}

