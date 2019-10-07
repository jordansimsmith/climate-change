using System.Globalization;
using Tweets;
using UnityEngine;
using UnityEngine.AI;
using World.Resource;

public class NonPlayingCharacter : MonoBehaviour
{
    public Color highlightColour;
    public Sprite avatar;
    public ResourceSingleton resourceSingleton;

    private string firstName;
    private string lastName;
    private string occupation;

    private Color[] colours;
    private Renderer[] renderers;

    private TweetGenerator tweetGenerator;
    private NPCPanelController panelController;
    private NavMeshAgent agent;

    public string FirstName
    {
        get => firstName;
        set => firstName = value;
    }

    public string LastName
    {
        get => lastName;
        set => lastName = value;
    }

    public string Occupation
    {
        get => occupation;
        set => occupation = value;
    }

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // get singleton instances
        tweetGenerator = TweetGenerator.Instance;
        panelController = NPCPanelController.Instance;

        // get renderers of npc model components
        renderers = GetComponentsInChildren<Renderer>();
        colours = new Color[renderers.Length];

        // cache original color
        for (int i = 0; i < renderers.Length; i++)
        {
            colours[i] = renderers[i].material.color;
        }
        
        // update pathing destination every 10 seconds
        InvokeRepeating("ChangeDestination", 0, 10f);
    }

    private void ChangeDestination()
    {
        float x = Random.Range(0, 190f);
        float y = transform.position.y;
        float z = Random.Range(0, 190f);
        
        Vector3 destination = new Vector3(x,y,z);
        agent.destination = destination;
    }

    private void OnMouseEnter()
    {
        // highlight npc
        foreach (Renderer rend in renderers)
        {
            rend.material.color = highlightColour;
        }
    }

    private void OnMouseExit()
    {
        // restore original colours
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = colours[i];
        }
    }

    void OnMouseDown()
    {
        string fullName = firstName + " " + lastName;

        float food = resourceSingleton.Food.CurAmount;
        float power = resourceSingleton.Power.CurAmount;
        float shelter = resourceSingleton.Shelter.CurAmount;
        float environment = resourceSingleton.Environment.CurAmount;
        
        string tweet = tweetGenerator.GenerateTweet(food, power, shelter, environment);
        tweet = "\"" + tweet + "\"";
        TextInfo info = new CultureInfo("en-US", false).TextInfo;
        string occupationTitleCase = info.ToTitleCase(occupation);

        panelController.Show(fullName, occupationTitleCase, tweet, avatar);
    }
}