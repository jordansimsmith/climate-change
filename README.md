# Climate Change
In this project, teams will be required to design and implement a web-based serious game on Climate Change for children in intermediate school years (aged 12-13).

## Team members (Team 10) 

| Name               | GitHub Username | UoA UPI | Role                               |
|--------------------|:---------------:|---------|------------------------------------|
| Reshad Contractor  | res550          | Rcon954 | Head of Vision Intern              |
| Sukhans Asrani     | Deagler         | sasr366 | Chief Solutions Architect Intern   |
| Andrew Hu          | andrewh318      | ahu156  | Lead Head of Test Architect Intern |
| Tony Liu           | Minus20Five     | tliu818 | The Tech Lead Intern               |
| Nisarag Bhatt      | FocalChord      | Nbha702 | Chief Dreaming Officer Intern      |
| Jed Robertson      | JedLJRobertson  | Jrob928 | Unpaid Head of Intern              |
| Jordan Sim-Smith   | jordansimsmith  | Jsim862 | Chief Intern Driver Intern         |

## How to collaborate

**Git LFS**  
Climate Change is developed using the Unity Game Engine. As such, many large assets will be included in the repository (sound, sprites etc.). To efficiently manage these assets, Git Large File Storage is used.

1. Install git lfs using your preffered package manager.
2. Run `git lfs install` inside the repository.
3. Commit as usual, the `.gitattributes` file contains the lfs configuration.

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
