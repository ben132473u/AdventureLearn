using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class LevelSelection : Node2D
{
    //Asset path
    string starsImagePath = "res://Assets/Level/";

    SectionBL sectionBL;
    List<Section> sectionList;
    StudentScoreBL studentScoreBL;
    TextureButton forwardBtn;
    TextureButton backWardBtn;
    Label sectionLbl;
    int count = 1;

    TextureButton level1;
    TextureButton level2;
    TextureButton level3;
    TextureButton level4;
    TextureButton level5;
    TextureButton level6;
    Sprite bg;
    List<string> bgList;

    public override void _Ready()
    {
        //Intializing nodes
        sectionBL = new SectionBL();
        studentScoreBL = new StudentScoreBL();
        forwardBtn = GetNode<TextureButton>("ArrowNavigation/ForwardButton");
        backWardBtn = GetNode<TextureButton>("ArrowNavigation/BackwardButton");
        sectionLbl = GetNode<Label>("Title/SectionName");

        level1 = GetNode<TextureButton>("Levels/Level1");
        level2 = GetNode<TextureButton>("Levels/Level2");
        level3 = GetNode<TextureButton>("Levels/Level3");
        level4 = GetNode<TextureButton>("Levels/Level4");
        level5 = GetNode<TextureButton>("Levels/Level5");
        level6 = GetNode<TextureButton>("Levels/Level6");
        bg = GetNode<Sprite>("Bg");
        bgList = new List<string>();

        bgList.Add("res://Assets/LevelUI/LvlMap03.png");
        bgList.Add("res://Assets/LevelUI/LvlMap01.png");
        bgList.Add("res://Assets/LevelUI/LvlMap02.png");
        //On load set section id to 1 to display section 1
        Global.SectionId = 1;
        sectionList = sectionBL.GetWorldSections(Global.WorldId);
        sectionLbl.Text = sectionList[count - 1].SectionName;

        //Star path
        string[] starArray = new string[3];
        starArray[0] = "Level/1 stars.png";
        starArray[1] = "Level/2 stars.png";
        starArray[2] = "Level/3 stars.png";

        //Disable forward n back btns if only have 1 level
        DisableBothButtons();
        backWardBtn.Disabled = true;
        HideLevels();

        DisplayLevelScore();

    }
    private void HideLevels()
    {
        Node levelNode = GetNode<Node>("Levels");
        foreach (Node n in levelNode.GetChildren())
        {
            if (n is TextureButton)
            {
                TextureButton levelTexture = (TextureButton)n;
                levelTexture.Visible = false;
                foreach (Node no in levelTexture.GetChildren())
                {
                    if (no is Sprite)
                    {
                        Sprite starSprite = (Sprite)no;
                        starSprite.Visible = false;
                    }
                }
            }
        }
    }

    private void DisplayLevelScore()
    {
        Node levelNode = GetNode<Node>("Levels");

        //If student has not cleared a single level in the section
        if (studentScoreBL.GetStudentScores(Global.WorldId, Global.SectionId, Global.StudentId) == null)
        {

            Node level;
            Section section = sectionBL.GetSectionLevels(Global.WorldId, Global.SectionId);

            if (Global.SectionId == 1)
            {

                //Unlock first level of section 1 by default
                UnlockFirstLevel();
                GD.Print(section.Level.Count());
                //Lock all levels other than 1
                for (int i = 1; i < section.Level.Count(); i++)
                {
                    level = levelNode.GetChild(i);
                    TextureButton levelTexture = level as TextureButton;
                    levelTexture.Visible = true;
                    levelTexture.Disabled = true;
                }

            }
            else
            {
                //Lock all levels
                for (int i = 0; i < section.Level.Count(); i++)
                {
                    level = levelNode.GetChild(i);
                    TextureButton levelTexture = level as TextureButton;
                    levelTexture.Visible = true;
                    levelTexture.Disabled = true;
                }
                CheckSectionCleared();

            }

        }
        else
        {
            GD.Print("da");

            Student student = studentScoreBL.GetStudentScores(Global.WorldId, Global.SectionId, Global.StudentId);
            int totalLevels = GetNode("Levels").GetChildCount();
            Section section = sectionBL.GetSectionLevels(Global.WorldId, Global.SectionId);
            Node level;
            int i = 0;
            Node levelParent = GetNode("Levels");

            //Display all section's levels
            foreach (Level lvl in section.Level)
            {
                level = levelNode.GetChild(i);
                TextureButton levelTexture = level as TextureButton;
                levelTexture.Visible = true;
                levelTexture.Disabled = true;
                i++;
            }

            //Load student scores
            int count = 0;
            foreach (StudentScore score in student.StudentScore)
            {
                string starAssetPath = starsImagePath;

                TextureButton levelTexture = levelParent.GetChild(count) as TextureButton;
                levelTexture.Disabled = false;
                levelTexture.Visible = true;

                Sprite starNode = levelTexture.GetChild(0) as Sprite;

                switch (score.LevelScore)
                {
                    case int ls when (ls >= 1 && ls <= 50):
                        starAssetPath += "1 stars.png";
                        break;
                    case int ls when (ls >= 51 && ls <= 70):
                        starAssetPath += "2 stars.png";
                        break;
                    case int ls when (ls >= 71 && ls <= 100):
                        starAssetPath += "3 stars.png";
                        break;
                }
                var texture = ResourceLoader.Load(starAssetPath) as Texture;
                starNode.Texture = texture;
                starNode.Visible = true;

                count++;
            }
            //Unlock nxt lvl
            TextureButton nextLevel = levelParent.GetChild(count) as TextureButton;
            nextLevel.Disabled = false;
        }
    }
    private void CheckSectionCleared()
    {
        int result = sectionBL.CheckSectionCleared(Global.WorldId, Global.SectionId, Global.StudentId);
        Student student = studentScoreBL.GetStudentScores(Global.WorldId, Global.SectionId, Global.StudentId);
        //Student cleared previous section
        if (result == 0)
        {
            UnlockFirstLevel();
        }
    }

    private void UnlockFirstLevel()
    {
        Node levelNode = GetNode<Node>("Levels");
        //Unlock first level
        TextureButton level1Texture = levelNode.GetChild(0) as TextureButton;
        level1Texture.Disabled = false;
        level1Texture.Visible = true;

        //Lock rest of the levels
        for (int i = 1; i < levelNode.GetChildren().Count; i++)
        {
            TextureButton levelTexture = levelNode.GetChild(i) as TextureButton;
            levelTexture.Disabled = true;
            Sprite starSprite = levelTexture.GetChild(0) as Sprite;
            starSprite.Visible = false;
        }
    }
    private void LoadBg()
    {
        switch (Global.SectionId)
        {
            case 1:
                var texture = ResourceLoader.Load(bgList[0]) as Texture;
                bg.Texture = texture;
                LoadFirstSection();
                break;
            case 2:
                var texture2 = ResourceLoader.Load(bgList[1]) as Texture;
                bg.Texture = texture2;
                LoadSecondSection();
                break;
            case 3:
                var texture3 = ResourceLoader.Load(bgList[2]) as Texture;
                bg.Texture = texture3;
                LoadThirdSection();
                break;
        }
    }
    private void LoadFirstSection()
    {
        level1.SetPosition(new Vector2(896, 352));
        level2.SetPosition(new Vector2(678, 418));
        level3.SetPosition(new Vector2(540, 212));
        level4.SetPosition(new Vector2(337, 331));
        level5.SetPosition(new Vector2(192, 249));
    }
    private void LoadSecondSection()
    {
        level1.SetPosition(new Vector2(821, 326));
        level2.SetPosition(new Vector2(674, 390));
        level3.SetPosition(new Vector2(487, 216));
        level4.SetPosition(new Vector2(336, 202));
        level5.SetPosition(new Vector2(99, 196));
    }
    private void LoadThirdSection()
    {
        level1.SetPosition(new Vector2(870, 332));
        level2.SetPosition(new Vector2(671, 387));
        level3.SetPosition(new Vector2(540, 212));
        level4.SetPosition(new Vector2(383, 70));
        level5.SetPosition(new Vector2(227, 234));
    }
    private void _on_ForwardButton_pressed()
    {
        count++;
        if (count >= sectionList.Count())
            forwardBtn.Disabled = true;
        if (backWardBtn.Disabled == true)
            backWardBtn.Disabled = false;
        Global.SectionId++;
        LoadBg();
        DisplaySectionName();
        HideLevels();
        DisplayLevelScore();
    }
    private void _on_BackwardButton_pressed()
    {
        count--;
        if (count <= 1)
            backWardBtn.Disabled = true;
        if (forwardBtn.Disabled == true)
            forwardBtn.Disabled = false;
        Global.SectionId--;
        LoadBg();
        DisplaySectionName();
        HideLevels();
        DisplayLevelScore();
    }

    private void DisplaySectionName()
    {
        sectionLbl.Text = sectionList[count - 1].SectionName;
    }

    private void DisableBothButtons()
    {
        if (sectionList.Count == 1)
        {
            forwardBtn.Disabled = true;
            backWardBtn.Disabled = true;
        }
    }
    private void RedirectGamePlay()
    {
        GetTree().ChangeScene("res://Presentation/GamePlay/Campaign.tscn");

    }
    //Buttons signal
    private void _on_Level1Btn_pressed()
    {
        RedirectGamePlay();
        Global.LevelId = 1;
    }
    private void _on_Level2Btn_pressed()
    {
        RedirectGamePlay();
        Global.LevelId = 2;
    }
    private void _on_Level3Btn_pressed()
    {
        RedirectGamePlay();
        Global.LevelId = 3;
    }
    private void _on_Level4Btn_pressed()
    {
        RedirectGamePlay();
        Global.LevelId = 4;
    }
    private void _on_Level5Btn_pressed()
    {
        RedirectGamePlay();
        Global.LevelId = 5;
    }
    private void _on_Level6Btn_pressed()
    {
        RedirectGamePlay();
        Global.LevelId = 6;
    }
    private void _on_Level1_pressed()
    {
        RedirectGamePlay();
        Global.LevelId = 1;
    }
    private void _on_Level2_pressed()
    {
        RedirectGamePlay();
        Global.LevelId = 2;
    }
    private void _on_Level3_pressed()
    {
        RedirectGamePlay();
        Global.LevelId = 3;
    }
    private void _on_Level4_pressed()
    {
        RedirectGamePlay();
        Global.LevelId = 4;
    }
    private void _on_Level5_pressed()
    {
        RedirectGamePlay();
        Global.LevelId = 5;
    }
    private void _on_Level6_pressed()
    {
        RedirectGamePlay();
        Global.LevelId = 6;
    }



}
