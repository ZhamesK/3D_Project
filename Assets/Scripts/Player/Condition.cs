using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;      // ���� ��
    public float startValue;    // �ʱ� ��
    public float maxValue;      // �ִ� ��
    public float passiveValue;  // �ڿ� ȸ��
    public Image UI_image;      // UI �̹���


    // Start is called before the first frame update
    void Start()
    {
        curValue = startValue;
    }

    // Update is called once per frame
    void Update()
    {
        UI_image.fillAmount = UIPercentage();
    }

    private float UIPercentage()
    {
        return curValue / startValue;
    }

    public void Add(float value)
    {
        // curValue += value;
        // �� ���� ���ϰ� �� ���� �� ��������
        curValue = Mathf.Min(curValue += value, maxValue);
    }

    public void Subtract(float value)
    {
        // curValue -= value;
        // �� ���� ���ϰ� �� ū ���� ��������
        curValue = Mathf.Max(curValue -= value, 0);
    }
}
