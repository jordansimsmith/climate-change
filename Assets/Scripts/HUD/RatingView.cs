using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatingView : MonoBehaviour
{
    public Sprite fullStar;
    public Sprite halfStar;
    public Sprite noStar;

    private int rating; // Rating out of ten

    private Image[] stars;

    // Start is called before the first frame update
    void Start()
    {
        stars = new Image[5];
        var childrenC = gameObject.GetComponentsInChildren<Image>();

        foreach (var component in childrenC)    {
            switch (component.gameObject.name)  {
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
                default:
                    break;
            }
        }

        SetRating(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Sets the rating out of ten (10 = 5 stars, 5 = 2.5 stars)
    public void SetRating(int rating)   {
        this.rating = rating;

        this.RenderRating();
    }

    public void RenderRating()  {
        int fullStars = rating/2;
        bool halfStars = (rating % 2) == 1;

        for (var i = 0; i < 5; i++) {
            if (i < fullStars)  {
                stars[i].sprite = fullStar;
            } else if (i == fullStars && halfStars)   {
                stars[i].sprite = halfStar;
            } else {
                stars[i].sprite = noStar;
            }
        }
    }
}
