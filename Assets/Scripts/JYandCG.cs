using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class JYandCG : MonoBehaviour
{
    public GameObject PIANDUAN;
    public GameObject CGgroup;
    public GameObject JYgroup;
    public GameObject TextPanel;
    public GameObject vp;
    public VideoClip[] videoClips;
    public bool[] CGbool = new bool[3];
    public bool[] JYbool = new bool[5];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PIANDUANinit()
    {
        Transform[] CG = CGgroup.GetComponentsInChildren<Transform>();
        Transform[] JY = JYgroup.GetComponentsInChildren<Transform>();
        for (int i = 0; i < 5; i++)
        {
            JY[i + 1].GetComponent<Button>().interactable = JYbool[i];
        }
        for (int i = 0; i < 3; i++)
        {
            CG[i + 1].GetComponent<Button>().interactable = CGbool[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeToCG()
    {
        CGgroup.SetActive(true);
        JYgroup.SetActive(false);
    }

    public void ChangeToJY()
    {
        CGgroup.SetActive(false);
        JYgroup.SetActive(true);
    }

    public void closePIANDUAN()
    {
        PIANDUAN.SetActive(false);
    }

    public void showTextPanel1()
    {
        TextPanel.SetActive(true);
        SetText(1);
    }

    public void showTextPanel2()
    {
        TextPanel.SetActive(true);
        SetText(2);
    }

    public void showTextPanel3()
    {
        TextPanel.SetActive(true);
        SetText(3);
    }

    public void showTextPanel4()
    {
        TextPanel.SetActive(true);
        SetText(4);
    }

    public void showTextPanel5()
    {
        TextPanel.SetActive(true);
        SetText(5);
    }

    //这个方法的作用是读取txt文档中的内容
    public void SetText(int index)
    {
        Text[] texts = TextPanel.GetComponentsInChildren<Text>();
        //表示的是要进行存储的所有的内容
        string m_Str = "";
        string[] strs = File.ReadAllLines(".\\Assets\\JY\\" + index+".txt");//读取文件的所有行，并将数据读取到定义好的字符数组strs中，一行存一个单元
        texts[0].text = strs[0];
        for (int i = 1; i < strs.Length; i++)
        {
            m_Str += strs[i];//读取每一行，并连起来
            m_Str += "\n";
        }
        texts[1].text = m_Str;
    }

    public void closeTextPanel()
    {
        TextPanel.SetActive(false);
    }

    public void video1()
    {
        playvideo(1);
    }

    public void video2()
    {
        playvideo(2);
    }

    public void video3()
    {
        playvideo(3);
    }

    public void playvideo(int i)
    {
        vp.GetComponent<VideoPlayer>().clip = videoClips[i-1];
        vp.SetActive(true);
    }
    public void closevideo(int i)
    {
        vp.SetActive(false);
    }
}
