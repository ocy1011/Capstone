using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignInPanel : MonoBehaviour {

    char splitChar = ';';
    GameObject exPanel;
    public InputField emailInputField;
    public InputField passwordInputField;
    public Transform textPanel;
    public Transform toggle;
    public Transform errorPanel;
    public ToggleGroupInfo genderGroup;
    public ToggleGroupInfo ageGroup;
    public ToggleGroupInfo childGroup;
    float titleHeight = 0.07f;
    float toggleHeight = 0.07f;
    string tempEmail = "";

    public void SelectBasicInfo()
    {
        Transform body = transform.GetChild(2);
        Transform basicInfo = body.GetChild(0);
        Transform additionalInfo = body.GetChild(1);
        Transform selectInfo = transform.GetChild(1);
        Image basicImage = selectInfo.GetChild(0).GetComponent<Image>();
        Image additionalImage = selectInfo.GetChild(1).GetComponent<Image>();
        basicInfo.gameObject.SetActive(true);
        additionalInfo.gameObject.SetActive(false);
        basicImage.color = new Color(0, 150, 255, 255) / 255f;
        additionalImage.color = new Color(255, 255, 255, 255) / 255f;
    }

    public void SelectAdditionalInfo()
    {
        if (tempEmail == Email())
        {
            Transform body = transform.GetChild(2);
            Transform basicInfo = body.GetChild(0);
            Transform additionalInfo = body.GetChild(1);
            Transform selectInfo = transform.GetChild(1);
            Image basicImage = selectInfo.GetChild(0).GetComponent<Image>();
            Image additionalImage = selectInfo.GetChild(1).GetComponent<Image>();
            basicInfo.gameObject.SetActive(false);
            additionalInfo.gameObject.SetActive(true);
            basicImage.color = new Color(255, 255, 255, 255) / 255f;
            additionalImage.color = new Color(0, 150, 255, 255) / 255f;
        }
        else
            OpenErrorPanel("중복 검사를 진행하십시오");
    }

    void MakeAdditionalInfo()
    {
        Transform parent = transform.GetChild(2).GetChild(1);
        float yMax = 0.99f;
        yMax = GenderGroup(parent, yMax) - 0.01f;
        yMax = AgeGroup(parent, yMax) - 0.01f;
        yMax = ChildGroup(parent, yMax) - 0.01f;
    }

    ToggleGroupInfo GroupInfo(Transform parent, string text, float yMax)
    {
        Transform newTextPanel = Instantiate(textPanel);
        newTextPanel.SetParent(parent);
        RectTransform rectTransform = newTextPanel.GetComponent<RectTransform>();
        rectTransform = AnchorChange(rectTransform, new Vector2(0, yMax - titleHeight), new Vector2(1, yMax));
        newTextPanel.GetChild(0).GetComponent<Text>().text = text;
        ToggleGroupInfo toggleGroupInfo = newTextPanel.gameObject.AddComponent<ToggleGroupInfo>();
        return toggleGroupInfo;
    }

    float GenderGroup(Transform parent, float yMax)
    {
        genderGroup = GroupInfo(parent, "성별", yMax);
        yMax -= titleHeight;
        for (int i=0; i<3; i++)
        {
            string text = "선택 안 함";
            if (i == 1)
                text = "여성";
            if (i == 2)
                text = "남성";
            ToggleInfo toggleInfo = Toggle(parent, text);
            toggleInfo.toggleGroup = genderGroup;
            toggleInfo.id = i;
            RectTransform rectTransform = toggleInfo.GetComponent<RectTransform>();
            rectTransform = AnchorChange(rectTransform, new Vector2(i * 1/3f,yMax - toggleHeight), new Vector2((i+1) * 1 / 3f, yMax));
        }
        return yMax - toggleHeight;
    }

    float AgeGroup(Transform parent, float yMax)
    {
        ageGroup = GroupInfo(parent, "연령대", yMax);
        yMax -= titleHeight;
        for (int i = 0; i < 7; i++)
        {
            string text = "선택 안 함";
            if (i == 1)
                text = "20대 미만";
            if (i == 2)
                text = "20대";
            if(i==3)
                text = "30대";
            if (i == 4)
                text = "40대";
            if (i == 5)
                text = "50대";
            if (i == 6)
                text = "60대 이상";
            ToggleInfo toggleInfo = Toggle(parent, text);
            toggleInfo.toggleGroup = ageGroup;
            toggleInfo.id = i;
            RectTransform rectTransform = toggleInfo.GetComponent<RectTransform>();
            float yMin = yMax - toggleHeight;
            rectTransform = AnchorChange(rectTransform, new Vector2(0, yMin), new Vector2(1/3f, yMax));
            yMax = yMin;
        }
        return yMax;
    }

    float ChildGroup(Transform parent, float yMax)
    {
        childGroup = GroupInfo(parent, "자녀 유무", yMax);
        yMax -= titleHeight;
        for (int i = 0; i < 3; i++)
        {
            string text = "선택 안 함";
            if (i == 1)
                text = "아니오";
            if (i == 2)
                text = "예";
            ToggleInfo toggleInfo = Toggle(parent, text);
            toggleInfo.toggleGroup = childGroup;
            toggleInfo.id = i;
            RectTransform rectTransform = toggleInfo.GetComponent<RectTransform>();
            rectTransform = AnchorChange(rectTransform, new Vector2(i * 1 / 3f, yMax - toggleHeight), new Vector2((i + 1) * 1 / 3f, yMax));
        }
        return yMax - toggleHeight;
    }

    RectTransform AnchorChange(RectTransform rectTransform, Vector2 min, Vector2 max)
    {
        rectTransform.anchorMin = min;
        rectTransform.anchorMax = max;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
        return rectTransform;
    }

    ToggleInfo Toggle(Transform parent, string text)
    {
        Transform newToggle = Instantiate(toggle);
        newToggle.SetParent(parent);
        newToggle.GetChild(1).GetComponent<Text>().text = text;
        ToggleInfo toggleInfo = newToggle.GetComponent<ToggleInfo>();
        return toggleInfo;
    }

    void RandomSignIn(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            string email = "changyeong" + i;
            string password = "1234";
            int age = Random.Range(0, 7);
            int child = Random.Range(0, 3);
            string sql = "INSERT INTO member(email, password, age, child) VALUES(" + "'" + email + "', '" + password + "', " + age + ", " + child + ");";
            DbConnecter.instance.Insert(sql);
            Debug.Log(i + "complete");
        }
    }

    public void OnPanel(GameObject exPanel)
    {
        this.exPanel = exPanel;
        exPanel.gameObject.SetActive(false);
        SelectBasicInfo();
        AllReset();
    }

    void AllReset()
    {
        emailInputField.text = "";
        passwordInputField.text = "";
        CloseErrorPanel();
        genderGroup.SelectToggle(0);
        ageGroup.SelectToggle(0);
        childGroup.SelectToggle(0);
        tempEmail = "";
    }

    public void BackButton()
    {
        exPanel.SetActive(true);
        if (exPanel.GetComponent<ProductInfoPanel>())
            Panels.instance.productInfoPanel.CheckBookmark();
        if (exPanel.GetComponent<IngredientPanel>())
            exPanel.GetComponent<IngredientPanel>().CheckBookmark();
        gameObject.SetActive(false);
        TextPanel[] textPanels = exPanel.transform.GetComponentsInChildren<TextPanel>();
        for (int i = 0; i < textPanels.Length; i++)
            textPanels[i].ChangeTextSize();
    }

    //id int, email varchar(50), password varchar(20), nickname varchar(16)
	string Email()
    {
        string text = emailInputField.text;
        int textLength = text.Length;
        if (textLength < 8)
            return null;
        return text;
    }

    string Password()
    {
        string text = passwordInputField.text;
        int textLength = text.Length;
        if (textLength < 4 || textLength > 20)
            return null;
        return text;
    }

    public void CheckBasicInfo()
    {
        string email = Email();
        string password = Password();
        if (email == null || password == null)
        {
            OpenErrorPanel("이메일 또는 비밀번호를 다시 한번 확인하십시오");
        }
        else
        {
            if(Duplication(email)==false)
            {
                tempEmail = email;
                SelectAdditionalInfo();
            }
            else
            {
                OpenErrorPanel("중복된 이메일이 존재합니다");
            }
        }
    }

    public void SignIn()
    {
        string email = Email();
        string password = Password();
        int gender = genderGroup.ActiveToggle();
        int age = ageGroup.ActiveToggle();
        int child = childGroup.ActiveToggle();
        string sql = "INSERT INTO member VALUES(" + "'" + email + "', '" + password + "', " + gender + ", " + age + ", " + child +", '', ''"  +  ");";
        DbConnecter.instance.Insert(sql);
        Debug.Log("email : " + email + " Success!");
        Panels.instance.memberInfo.Login(email, password);
        Panels.instance.userPanel.CheckLog();
        if (exPanel.GetComponent<LogInPanel>())
        {
            exPanel.GetComponent<LogInPanel>().BackButton();
            gameObject.SetActive(false);
        }  
        else
            BackButton();
    }

    bool Duplication(string email)
    {
        int emailCount = DbConnecter.instance.Count("email", "member", "email = "+"'"+email+"';");
        if (emailCount == 0)
        {
            Debug.Log("생성가능");
            return false;
        }
            
        else
        {
            Debug.Log("중복  - 생성 불가능");
            return true;
        }  
    }

    void OpenErrorPanel(string text)
    {
        errorPanel.gameObject.SetActive(true);
        Text textInfo = errorPanel.transform.GetChild(0).GetChild(1).GetComponent<Text>();
        textInfo.text = text;
    }

    public void CloseErrorPanel()
    {
        errorPanel.gameObject.SetActive(false);
    }
}
