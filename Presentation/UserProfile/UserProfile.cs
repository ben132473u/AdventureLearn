using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Class to handle Presentation for UserProfile
/// </summary>
public class UserProfile : Node2D
{
    StudentBL studentBL;

    /// <summary>
    /// Initialization
    /// </summary>
    public override void _Ready()
    {
        studentBL = new StudentBL();
        Student student = studentBL.GetStudentCharacter(Global.StudentId);
        Label lbl = GetNode<Label>("NameLbl");
        lbl.Text = student.StudentName;
        AnimatedSprite character = GetNode<AnimatedSprite>("Character");
        Label charName = GetNode<Label>("CharacterName");
        Label charSkill = GetNode<Label>("CharacterSkill");
        charName.Text = student.Character.CharName;
        charSkill.Text = student.Character.CharSkill;
        string spritePath = "res://CharSprites/";
        string charPath = String.Format(spritePath + "{0}/", student.Character.CharName);
        List<string> animationList = new List<string>();
        animationList.Add("Idle");
        Global.LoadSprite(charPath, character, animationList);
        StudentScoreBL studentScoreBL = new StudentScoreBL();
        Label avgScoreLbl = GetNode<Label>("AvgScoreLbl");
        Label campaignLbl = GetNode<Label>("RankLbl");

        if(studentScoreBL.GetCampaignRanking(Global.StudentId) != 0)
            campaignLbl.Text = studentScoreBL.GetCampaignRanking(Global.StudentId).ToString();
        else
            campaignLbl.Text = "-";
        
        if(studentScoreBL.GetAvgWorldScores(Global.StudentId) != null)
            avgScoreLbl.Text = studentScoreBL.GetAvgWorldScores(Global.StudentId).LevelScore.ToString();

    }

    /// <summary>
    /// Change scene to CharSelect.tscn whenever the ChangeCharacter button is pressed
    /// </summary>
    private void _on_ChangeCharacter_pressed()
    {
        GetTree().ChangeScene("res://Presentation/CharSelect/CharSelect.tscn");
    }

}



