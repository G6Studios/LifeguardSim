using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Animator menuAnims;

    public GameObject title;
    public GameObject buttons;
    public GameObject options;
    public GameObject optionButtons;
    private Toggle toggle;

    private bool optionsVisible = false;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    private void Start()
    {
        toggle = optionButtons.GetComponentInChildren<Toggle>();
    }

    // Update is called once per frame
    private void Update()
    {
        PlayerPrefs.SetInt("toggleWarning", Converters.BoolToInt(true));
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Swimming Pool");
    }

    public void ShowOptions()
    {
        if (optionsVisible == false)
        {
            menuAnims.Play("ShowOptions");
            optionsVisible = true;
        }
    }

    public void HideOptions()
    {
        if (optionsVisible == true)
        {
            menuAnims.Play("HideOptions");
            optionsVisible = false;
        }
    }
}