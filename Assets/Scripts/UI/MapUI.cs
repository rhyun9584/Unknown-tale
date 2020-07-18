using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    public static MapUI inst;

    public GameObject mapUI;
    public GameObject exitButton, moveButton, detailButton;
    public GameObject warningPage;

    // 상세히 창 이전의 정보 창 -> simple
    // 상세히 창                -> detail
    public GameObject simpleUI, simpleName, simpleExplain, simpleImage;
    public GameObject detailUI, detailName, detailExplain, detailImage;

    private Text simpleNameText, simpleExplainText;
    private Text detailNameText, detailExplainText;

    private Image simpleImageSprite;
    private Image detailImageSprite;

    private LocationCode nextLocation;

    private bool isActive = false;
    private bool isSimpleActive = false;
    private bool isDetailActive = false;

    void Awake()
    {
        if (inst == null)
            inst = this;
    }

    private void Start()
    {
        simpleNameText = simpleName.GetComponent<Text>();
        simpleExplainText = simpleExplain.GetComponent<Text>();
        simpleImageSprite = simpleImage.GetComponent<Image>();
        
        detailNameText = detailName.GetComponent<Text>();
        detailExplainText = detailExplain.GetComponent<Text>();
        detailImageSprite = detailImage.GetComponent<Image>();

        warningPage.SetActive(false);
    }

    public void OpenMapUI()
    {
        StartCoroutine(warning()); // MAP UI를 켜지않고 사용불가하단 팝업창 짧게 띄우기

        /*
        if (!isActive)
        {
            isActive = true;

            PhoneUI.inst.HideMain();
            PhoneUI.inst.HideBackground();
            mapUI.SetActive(isActive);

            GameManager.inst.ChangeState(State.Map);
        }
        */
    }

    IEnumerator warning()
    {
        warningPage.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        warningPage.SetActive(false);

        yield return null;
    }

    public void CloseMapUI()
    {
        if (isActive)
        {
            isActive = false;

            PhoneUI.inst.ShowMain();
            PhoneUI.inst.ShowBackground();

            OffDetailUI();
            OffSimpleUI();
            mapUI.SetActive(isActive);

            GameManager.inst.ChangeState(State.Phone);
        }
    }

    public void OnSimpleUI(LocationCode locationCode)
    {
        if (!isSimpleActive)
        {
            isSimpleActive = true;

            simpleNameText.text = locationCode.ToString();
            //simpleExplainText.text = ;
            //simpleImageSprite = Resources.Load("");

            nextLocation = locationCode;

            simpleUI.SetActive(isSimpleActive);
        }
    }

    public void OffSimpleUI()
    {
        if (isSimpleActive)
        {
            isSimpleActive = false;

            simpleUI.SetActive(isSimpleActive);
        }
    }

    public void OnDetailUI()
    {
        if (!isDetailActive)
        {
            isDetailActive = true;

            detailNameText.text = nextLocation.ToString();
            // Explain과 Image 변경 필요

            detailUI.SetActive(isDetailActive);
        }
    }

    public void OffDetailUI()
    {
        if (isDetailActive)
        {
            isDetailActive = false;

            detailUI.SetActive(isDetailActive);
        }
    }

    /// <summary>
    /// Map UI 내부의 Exit Button이 어떤 동작을 할 것인지 결정
    /// 가장 상단의 창을 닫음
    /// </summary>
    public void ExitButton()
    {
        if (isDetailActive)
        {
            OffDetailUI();
        }
        else if (isSimpleActive)
        {
            OffSimpleUI();
        }
        else if (isActive)
        {
            CloseMapUI();
        }
    }

    /// <summary>
    /// Simple UI에서 사용하는 이동 버튼
    /// </summary>
    public void MoveLocation()
    {
        // Debug.Log(GameManager.inst.ReturnState() + " " + LocationManager.inst.locationScript[(int)nextLocation].GetActive()
        //     + " " + LocationManager.inst.location[(int)nextLocation].GetComponent<LocationBase>().GetActive());

        if (GameManager.inst.ReturnLocation() == nextLocation) // current Location == next Location
        {
            Debug.Log(nextLocation.ToString() + " is here");
        }
        else if (GameManager.inst.ReturnState() == State.Map && LocationManager.inst.locationScript[(int)nextLocation].GetActive() == true)
        {
            GameManager.inst.ChangeLocation(nextLocation);
            PhoneUI.inst.ClosePhoneUI();
        }
        else // next Location is not activated 한 번도 방문한 적 없는 장소로 이동 시에
        {
            Debug.Log(nextLocation.ToString() + " is not activated");
        }
    }
}
