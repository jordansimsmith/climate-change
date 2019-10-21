# Climate Change
In this project, teams will be required to design and implement a web-based serious game on Climate Change for children in intermediate school years (aged 12-13). 

Note that the game is intended to be run at a 16:9 aspect ratio.

![caeli](https://user-images.githubusercontent.com/31643423/67184473-55700600-f440-11e9-8195-fed95f4bcd37.png)


## How to run
Open https://tinyurl.com/caeli-final in google chrome.

## Team members (Team 10) 

| Name               | GitHub Username | UoA UPI |
|--------------------|:---------------:|---------|
| Reshad Contractor  | res550          | Rcon954 |
| Sukhans Asrani     | Deagler         | sasr366 |
| Andrew Hu          | andrewh318      | ahu156  |
| Tony Liu           | Minus20Five     | tliu818 |
| Nisarag Bhatt      | FocalChord      | Nbha702 |
| Jed Robertson      | JedLJRobertson  | Jrob928 |
| Jordan Sim-Smith   | jordansimsmith  | Jsim862 |

### A team photo:

![TeamPhoto](https://user-images.githubusercontent.com/31643423/67136196-7a694b00-f27f-11e9-8434-dca8ebf5494a.jpeg)

## How to collaborate

**Git LFS**  
Climate Change is developed using the Unity Game Engine. As such, many large assets will be included in the repository (sound, sprites etc.). To efficiently manage these assets, Git Large File Storage is used.

1. Install git lfs using your preffered package manager.
2. Run `git lfs install` inside the repository.
3. Commit as usual, the `.gitattributes` file contains the lfs configuration.

**Unity Version**  
Please make sure when you run the project you are using unity version 2019.2.6f1

**Configure Unity**  
Unity, by default, uses binary files for project configuration. However, it can be configured to use text based files instead so that they can be managed with version control.

1. Open the editory settings window.  
`Edit > Project Settings > Editor`
2. Make `.meta` fiels visible to avoid broken object references.  
`Version Control / Mode: "Visible Meta Files"`
3. Use plain text serialisation to avoid unresolvable merge conflicts.  
`Asset Serialisation / Mode: "Force Text"`
4. Save your changes.  
`File > Save Project`

> Refer to this article for more information: [how-to-git-with-unity](https://thoughtbot.com/blog/how-to-git-with-unity)
