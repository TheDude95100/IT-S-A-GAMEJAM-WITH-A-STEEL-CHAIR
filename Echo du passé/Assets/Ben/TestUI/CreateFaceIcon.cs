using UnityEngine;
using UnityEngine.UI;

public class CreateFaceIcon : MonoBehaviour
{
    [SerializeField]
    private GameObject entityPrefab;
    GameObject headPrefab;

    [SerializeField]
    private GameObject faceUI;
    [SerializeField]
    private GameObject hairUI;
    [SerializeField]
    private GameObject facialHairUI;
    [SerializeField]
    private GameObject leftEyeUI;
    [SerializeField]
    private GameObject rightEyeUI;

    private GameObject facePrefab;
    private GameObject hairPrefab;
    private GameObject facialHairPrefab;
    private GameObject leftEyePrefab;
    private GameObject rightEyePrefab;

    private void Start()
    {
        // ===================== Begin Find Head =====================
        headPrefab = entityPrefab.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).gameObject;
        // ====================== End Find Head ======================

        ChangeFace();
        ChangeHair();
        ChangeFacialHair();
        //ChangeLeftEye();
        //ChangeRightEye();
    }

    private void ChangeFace()
    {
        facePrefab = headPrefab.transform.GetChild(1).transform.GetChild(0).gameObject;
        Image faceUiImage = faceUI.transform.GetComponent<Image>();

        faceUiImage.sprite = facePrefab.transform.GetComponent<SpriteRenderer>().sprite;
        faceUiImage.color = facePrefab.transform.GetComponent<SpriteRenderer>().color;
        faceUiImage.material = facePrefab.transform.GetComponent<SpriteRenderer>().sharedMaterial;
    }

    private void ChangeHair()
    {
        hairPrefab = headPrefab.transform.GetChild(0).transform.GetChild(0).gameObject;
        Image hairUiImage = hairUI.transform.GetComponent<Image>();
        if (hairPrefab.transform.GetComponent<SpriteRenderer>().sprite is null)
        {
            hairUI.SetActive(false);
            return;
        }

        hairUiImage.sprite = hairPrefab.transform.GetComponent<SpriteRenderer>().sprite;
        hairUiImage.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(hairUiImage.sprite.texture.width * 0.025f, hairUiImage.sprite.texture.height * 0.025f);
        hairUiImage.color = hairPrefab.transform.GetComponent<SpriteRenderer>().color;
        hairUiImage.material = hairPrefab.transform.GetComponent<SpriteRenderer>().sharedMaterial;
    }

    private void ChangeFacialHair()
    {
        facialHairPrefab = headPrefab.transform.GetChild(2).transform.GetChild(0).gameObject;

        Image facialHairUiImage = facialHairUI.transform.GetComponent<Image>();
        if (facialHairPrefab.transform.GetComponent<SpriteRenderer>().sprite is null)
        {
            facialHairUI.SetActive(false);
            return;
        }

        Debug.Log(facialHairPrefab);

        facialHairUiImage.sprite = facialHairPrefab.transform.GetComponent<SpriteRenderer>().sprite;
        facialHairUiImage.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(facialHairUiImage.sprite.texture.width * 0.03f, facialHairUiImage.sprite.texture.height * 0.03f);
        facialHairUiImage.color = facialHairPrefab.transform.GetComponent<SpriteRenderer>().color;
        facialHairUiImage.material = facialHairPrefab.transform.GetComponent<SpriteRenderer>().sharedMaterial;
    }
    private void ChangeRightEye()
    {
        rightEyePrefab = headPrefab.transform.GetChild(3).transform.GetChild(0).gameObject;
        Image rightEyeUiBackImage = rightEyeUI.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Image>();
        Image rightEyeUiFrontImage = rightEyeUI.transform.GetChild(1).transform.GetChild(0).transform.GetComponent<Image>();
        
        rightEyeUiBackImage.sprite = rightEyePrefab.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<SpriteRenderer>().sprite;
        rightEyeUiBackImage.color = rightEyePrefab.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color;
        rightEyeUiBackImage.material = rightEyePrefab.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<SpriteRenderer>().sharedMaterial;
        rightEyeUiBackImage.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(rightEyeUiBackImage.sprite.texture.width * 0.03f, rightEyeUiBackImage.sprite.texture.height * 0.03f);

        rightEyeUiFrontImage.sprite = rightEyePrefab.transform.GetChild(1).transform.GetChild(0).transform.GetComponent<SpriteRenderer>().sprite;
        rightEyeUiFrontImage.color = rightEyePrefab.transform.GetChild(1).transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color;
        rightEyeUiFrontImage.material = rightEyePrefab.transform.GetChild(1).transform.GetChild(0).transform.GetComponent<SpriteRenderer>().sharedMaterial;
        rightEyeUiFrontImage.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(rightEyeUiFrontImage.sprite.texture.width * 0.03f, rightEyeUiFrontImage.sprite.texture.height * 0.03f);
    }
    private void ChangeLeftEye()
    {
        leftEyePrefab = headPrefab.transform.GetChild(3).transform.GetChild(1).gameObject;
        Image leftEyeUiBackImage = leftEyeUI.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Image>();
        Image leftEyeUiFrontImage = leftEyeUI.transform.GetChild(1).transform.GetChild(0).transform.GetComponent<Image>();        

        
        leftEyeUiBackImage.sprite = leftEyePrefab.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<SpriteRenderer>().sprite;
        leftEyeUiBackImage.color = leftEyePrefab.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color;
        leftEyeUiBackImage.material = leftEyePrefab.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<SpriteRenderer>().sharedMaterial;
        leftEyeUiBackImage.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(leftEyeUiBackImage.sprite.texture.width * 0.03f, leftEyeUiBackImage.sprite.texture.height* 0.03f);

        leftEyeUiFrontImage.sprite = leftEyePrefab.transform.GetChild(1).transform.GetChild(0).transform.GetComponent<SpriteRenderer>().sprite;
        leftEyeUiFrontImage.color = leftEyePrefab.transform.GetChild(1).transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color;
        leftEyeUiFrontImage.material = leftEyePrefab.transform.GetChild(1).transform.GetChild(0).transform.GetComponent<SpriteRenderer>().sharedMaterial;
        leftEyeUiFrontImage.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(leftEyeUiFrontImage.sprite.texture.width* 0.03f, leftEyeUiFrontImage.sprite.texture.height * 0.03f);
        Debug.LogWarning(leftEyeUiFrontImage.sprite.texture);
        Debug.LogWarning(leftEyeUiBackImage.sprite.texture.width + " : " + leftEyeUiBackImage.sprite.texture.height);
    }


}
