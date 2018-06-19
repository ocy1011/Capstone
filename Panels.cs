using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panels : MonoBehaviour {

    public static Panels instance;
    public SignInPanel signInPanel;
    public ProductInfoPanel productInfoPanel;
    public HomePanel homePanel;
    public BookmarkPanel bookmarkPanel;
    public LogInPanel logInPanel;
    public UserPanel userPanel;
    public MemberInfo memberInfo;
    public ReviewPanel reviewPanel;
    public WriteReviewPanel writeReviewPanel;
    public IngredientPanel ingredientPanel;
    public MyReviewPanel myReviewPanel;
    public SuggestLogInPanel suggestLogInPanel;
    public BarcodePanel barcodePanel;
    public ProductSearchPanel productSearchPanel;
    public TestIngredientPanel testIngredientPanel;

	// Use this for initialization
	void Start () {
        instance = this;
	}
}
