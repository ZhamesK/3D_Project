using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;      // 현재 값
    public float startValue;    // 초기 값
    public float maxValue;      // 최대 값
    public float passiveValue;  // 자연 회복
    public Image UI_image;      // UI 이미지


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
        // 두 값을 비교하고 더 작은 값 가져오기
        curValue = Mathf.Min(curValue += value, maxValue);
    }

    public void Subtract(float value)
    {
        // curValue -= value;
        // 두 값을 비교하고 더 큰 값을 가져오기
        curValue = Mathf.Max(curValue -= value, 0);
    }
}
