using UnityEngine;
using UnityEngine.UI;
using World.Resource;

public class RatingView : MonoBehaviour
{
    public Sprite fullStar;
    public Sprite halfStar;
    public Sprite noStar;

    public ResourceSingleton resourceSingleton;

    // If you have a good rating for a while, it gets a positive boost (0 no boost, 10 full boost)
    private int ratingBoost;

    // Rating out of ten
    private int rating;

    private Image[] stars;

    // Start is called before the first frame update
    void Start()
    {
        stars = new Image[5];
        var childrenC = gameObject.GetComponentsInChildren<Image>();

        foreach (var component in childrenC)
        {
            switch (component.gameObject.name)
            {
                case "Star1":
                    stars[0] = component;
                    break;
                case "Star2":
                    stars[1] = component;
                    break;
                case "Star3":
                    stars[2] = component;
                    break;
                case "Star4":
                    stars[3] = component;
                    break;
                case "Star5":
                    stars[4] = component;
                    break;
            }
        }

        SetRating(5);

        InvokeRepeating("FourSecondTick", 4f, 4f);
    }

// Update is called once per frame
    void Update()
    {
        var pts = 2;

        var envBalance = resourceSingleton.totalSupply.environment - resourceSingleton.totalDemand.environment;
        var powerBalance = resourceSingleton.totalSupply.power - resourceSingleton.totalDemand.power;
        var foodBalance = resourceSingleton.totalSupply.food - resourceSingleton.totalDemand.food;
        var shelterBalance = resourceSingleton.totalSupply.shelter - resourceSingleton.totalDemand.shelter;

        if (0 < envBalance)
        {
            pts++;
        }

        if (0 < powerBalance)
        {
            pts++;
        }

        if (0 < foodBalance)
        {
            pts++;
        }

        if (0 < shelterBalance)
        {
            pts++;
        }

        if (-20 > envBalance)
        {
            pts--;
        }

        if (-20 > powerBalance)
        {
            pts--;
        }

        if (-20 > foodBalance)
        {
            pts--;
        }

        if (-20 > shelterBalance)
        {
            pts--;
        }

        if (resourceSingleton.Money > 200)
        {
            pts++;
        }

        if (resourceSingleton.MoneyRate > 10)
        {
            pts++;
        }

        if (ratingBoost > 6)
        {
            pts++;
        }

        if (ratingBoost > 9)
        {
            pts++;
        }

        this.SetRating(pts);
    }

    // Sets the rating out of ten (10 = 5 stars, 5 = 2.5 stars)
    public void SetRating(int rating)
    {
        this.rating = rating;

        this.RenderRating();
    }

    public void RenderRating()
    {
        int fullStars = rating / 2;
        bool halfStars = (rating % 2) == 1;

        for (var i = 0; i < 5; i++)
        {
            if (i < fullStars)
            {
                stars[i].sprite = fullStar;
            }
            else if (i == fullStars && halfStars)
            {
                stars[i].sprite = halfStar;
            }
            else
            {
                stars[i].sprite = noStar;
            }
        }
    }

    // Used to update the rating boost, from having a streak of good rating
    private void FourSecondTick()
    {
        if (rating > 6)
        {
            ratingBoost++;
            if (ratingBoost > 10)
            {
                ratingBoost = 10;
            }
        }
        else
        {
            ratingBoost--;
            if (ratingBoost < 0)
            {
                ratingBoost = 0;
            }
        }
    }
}