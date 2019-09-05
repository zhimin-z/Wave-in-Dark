using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class TutorialVisualClueController : MonoBehaviour {
    
	public Image leftButtonSprite;
    public Image rightButtonSprite;
    public Image handSprite;
	public Sprite clickSprite;
	public Sprite releaseClickSprite;
    public Text tutorialText;
    public Transform leftHandPosition;
    private bool isShowingRightHand;
    private bool isDelay;
    public float destroyDelay;
    private FirstPersonController fpc;
    private PlayerCharacter pc;
    
	public GameObject tutorial;

	void Start()
	{
        isShowingRightHand = true;
        fpc = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
        pc = GameObject.Find("FPSController").GetComponent<PlayerCharacter>();
        if (fpc.isTutorial)
        {
            pc.isRightHandTutorial = true;
            pc.isLeftHandTutorial = false;
        }
        StartCoroutine(playRightHandAnimation());
        isDelay = false;
	}
	
	void Update()
	{
        if (!isDelay && isShowingRightHand && (Input.GetMouseButtonDown(1)))
        {
            StartCoroutine(activateLeftHandTutorial());
        }
        if (!isDelay && !isShowingRightHand && (Input.GetMouseButtonDown(0)))
		{
            StartCoroutine(destroyTutorial());
        }

    }

    IEnumerator activateLeftHandTutorial()
    {
        isDelay = true;
        isShowingRightHand = false;
        yield return new WaitForSeconds(destroyDelay);
        isDelay = false;
        pc.isRightHandTutorial = false;
        pc.isLeftHandTutorial = true;
        StartCoroutine(playLeftHandAnimation());
        yield return true;
    }

    IEnumerator destroyTutorial()
    {
        isDelay = true;
        yield return new WaitForSeconds(destroyDelay);
        isDelay = false;
        fpc.isTutorial = false;
        pc.isLeftHandTutorial = false;
        Destroy(tutorial);
        yield return true;
    }

    IEnumerator playRightHandAnimation()
	{
		while (isShowingRightHand){
			yield return new WaitForSeconds(0.5f);
			handSprite.sprite = clickSprite;
			rightButtonSprite.color = Color.cyan;
			yield return new WaitForSeconds(0.5f);
			handSprite.sprite = releaseClickSprite;
            rightButtonSprite.color = Color.white;
			yield return new WaitForSeconds(0.5f);
		}
		
		yield return true;
	}
    
    IEnumerator playLeftHandAnimation()
	{
		while (true){
            handSprite.transform.position = leftHandPosition.position;
            tutorialText.text = "Left click to shoot your bullet";
            yield return new WaitForSeconds(0.5f);
			handSprite.sprite = clickSprite;
			leftButtonSprite.color = Color.cyan;
			yield return new WaitForSeconds(0.5f);
			handSprite.sprite = releaseClickSprite;
			leftButtonSprite.color = Color.white;
			yield return new WaitForSeconds(0.5f);
		}
		
		yield return true;
	}
	
}
