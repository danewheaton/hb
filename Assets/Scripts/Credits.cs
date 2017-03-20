using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Credits : MonoBehaviour
{
    public static bool Won;
    public Image titleScreenPanel;
    public Text creditsText;
    public GameObject flamingoAmbush;
    public float titleScreenFadetime = 1.5f;
	public float flashFadeTime = 2f;
    public float adequateAmountOfTimeToTakeAGoodLongGanderAtTheToughGangOfMercilessAviansSurroundingYou = 2;
	public SoundFeedback soundFeed;
    public Material textColor;

    Image panelImage;
    GameObject player;
    bool clicked;

    void Start()
    {
        panelImage = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(FadeIn());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) clicked = true;

        if (Won) StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        float timer = 1;
        float elapsedTime = 0;
        while (elapsedTime < timer)
        {
            panelImage.color = Color.Lerp(Color.white, Color.clear, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        panelImage.color = Color.clear;

        while (!clicked) yield return null;

        elapsedTime = 0;
        while (elapsedTime < timer)
        {
            titleScreenPanel.color = Color.Lerp(Color.white, Color.clear, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        titleScreenPanel.color = Color.clear;

    }

    IEnumerator FadeOut()
    {
        Won = false;

        player.GetComponent<vp_FPController>().MotorAcceleration = 0;
        flamingoAmbush.SetActive(true);
        GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(adequateAmountOfTimeToTakeAGoodLongGanderAtTheToughGangOfMercilessAviansSurroundingYou);

        panelImage.color = Color.black;
        creditsText.color = textColor.color;

        yield return new WaitForSeconds(2);
        creditsText.text = "HEALTHY BREAKFAST";
        yield return new WaitForSeconds(2);
        creditsText.text = "";
        yield return new WaitForSeconds(1.5f);
        creditsText.text = "ART                 Joakim Saldamando\n\nDESIGN         Aidan Walsh\n                          Dane Wheaton\n\nMUSIC           Aidan Walsh";
        yield return new WaitForSeconds(4);
        creditsText.text = "";
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }

    public IEnumerator FlashWhite()
    {
		soundFeed.Vwoop ();

		Color[] randomColors = new Color[7];
		randomColors[0] = Color.cyan;
        randomColors[1] = Color.cyan;
		randomColors[2] = Color.magenta;
        randomColors[3] = Color.magenta;
        randomColors[4] = Color.red;
        randomColors[5] = Color.white;
        randomColors[6] = Color.yellow;

		Color newColor = randomColors[Random.Range(0, 6)];
		panelImage.color = newColor;

		float timer = flashFadeTime;
        float elapsedTime = 0;
        while (elapsedTime < timer)
        {
			panelImage.color = Color.Lerp(newColor, Color.clear, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        panelImage.color = Color.clear;
    }

    public IEnumerator FlashWhite2()
    {
        panelImage.color = Color.white;

        float timer = flashFadeTime;
        float elapsedTime = 0;
        while (elapsedTime < timer)
        {
            panelImage.color = Color.Lerp(Color.white, Color.clear, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        panelImage.color = Color.clear;
    }
}
