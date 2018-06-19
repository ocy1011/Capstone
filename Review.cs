using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Review{

    public int id;
    public string email;
    public int productID;
    public int score;
    public int first;
    public int second;
    public int third;
    public int fourth;
    public int fifth;

    public Review()
    {

    }

    public string firstText(int number)
    {
        string text = "선택 안 함";
        if (number == 1)
            text = "대형마트";
        if (number == 2)
            text = "중 / 소형마트";
        if (number == 3)
            text = "편의점 / 동네슈퍼마켓";
        if (number == 4)
            text = "인터넷";
        return text;
    }

    public string secondText(int number)
    {
        string text = "선택 안 함";
        if (number == 1)
            text = "2주 미만";
        if (number == 2)
            text = "1개월 미만";
        if (number == 3)
            text = "1개월 이상 ~ 3개월 미만";
        if (number == 4)
            text = "3개월 이상 ~ 6개월 미만";
        if (number == 5)
            text = "6개월 이상";
        return text;
    }

    public string thirdText(int number)
    {
        string text = "선택 안 함";
        if (number == 1)
            text = "있다";
        if (number == 2)
            text = "없다";
        return text;
    }

    public string fourthText(int number)
    {
        string text = "선택 안 함";
        if (number == 1)
            text = "0회";
        if (number == 2)
            text = "1회";
        if (number == 3)
            text = "2회";
        if (number == 4)
            text = "3회";
        if (number == 5)
            text = "4회 이상";
        return text;
    }

    public string fifthText(int number)
    {
        string text = "선택 안 함";
        if (number == 1)
            text = "지인의 추천";
        if (number == 2)
            text = "원래 사용";
        if (number == 3)
            text = "광고를 통해서";
        if (number == 4)
            text = "이슈가 되어서";
        return text;
    }

}
	
