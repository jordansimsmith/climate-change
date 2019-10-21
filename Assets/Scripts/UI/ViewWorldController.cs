using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Persistence;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ViewWorldController : MonoBehaviour
{
    public InputField shareCodeInput;

    public Button viewWorldBtn;
    private PersistenceManager persistenceManager;

    // Start is called before the first frame update
    void Start()
    {
        persistenceManager = FindObjectOfType<PersistenceManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void CodeTextChanged()
    {
        if (shareCodeInput.text.Trim().Equals(""))
        {
            viewWorldBtn.interactable = false;
        }
        else
        {
            viewWorldBtn.interactable = true;
        }
    }

    public void ViewWorldClicked()
    {
        shareCodeInput.interactable = false;
        viewWorldBtn.interactable = false;
        viewWorldBtn.GetComponentInChildren<Text>().text = "Retrieving...";
        APIService.Instance.GetSharedWorld(shareCodeInput.text, (serverWorld) =>
        {
            if (serverWorld == null)
            {
                shareCodeInput.text = "";
                shareCodeInput.interactable = true;
                viewWorldBtn.GetComponentInChildren<Text>().text = "Invalid Code, Try Again!";
            }
            else
            {
                shareCodeInput.interactable = true;
                persistenceManager.SelectedWorld = serverWorld;
                SceneManager.LoadScene("ObserverScene", LoadSceneMode.Single);
            }
        });
    }

    public void CloseButtonClicked()
    {
        gameObject.SetActive(false);
    }
}