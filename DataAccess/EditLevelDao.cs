using Godot;
using MySql.Data.MySqlClient;
using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;

public class EditLevelDao : Node
{
	CustomLevel levelInfo;

	/// <summary>
	/// Update Question into database
	/// </summary>
	/// <param name="string option1"></param>
	/// <param name="string option2"></param>
	/// <param name="string option3"></param>
	/// <param name="string option4"></param>
	/// <param name="int correctOption"></param>
	/// <param name="string questionTitle"></param>
	/// <param name="int questionId"></param>
	/// <returns></returns>
	public void updateQuestion(string option1, string option2, string option3, string option4, int correctOptionInt, string questionTitle, int questionId)
	{
		switch (correctOptionInt)
		{
			case 1:
				formatForDatabaseInsertion(ref option4, ref option1);
				break;
			case 2:
				formatForDatabaseInsertion(ref option4, ref option2);
				break;
			case 3:
				formatForDatabaseInsertion(ref option4, ref option3);
				break;
			case 4:
				//formatForDatabaseInsertion(ref option4, ref option4);
				break;
		}

		//GD.Print("Question id: " + questionList[questionId].QuestionId);

		string query = String.Format("UPDATE Question SET Option1 = '{0}', Option2 = '{1}', " +
			"Option3 = '{2}', CorrectOption = '{3}', QuestionTitle = '{4}' " +
			"WHERE QuestionId = {5}", option1, option2, option3, option4, questionTitle, levelInfo.Question[questionId].QuestionId);

		BaseDao<int> baseDao = new BaseDao<int>();
		int result = baseDao.ExecuteQuery(query);   

		if (result <= 0)
			GD.Print("Error updating question into database.");
		else
			GD.Print("Question updated into database successfully.");
	}

	/// <summary>
	/// Format for database insertion
	/// </summary>
	/// <param name="ref string option4"></param>
	/// <param name="ref string correctOption"></param>
	/// <returns></returns>
	private void formatForDatabaseInsertion(ref string option4, ref string correctOption)
	{
		option4 = option4 + correctOption;
		correctOption = option4.Substring(0, (option4.Length - correctOption.Length));
		option4 = option4.Substring(correctOption.Length);
	}

	/// <summary>
	/// Check database for existing level name
	/// Return -1 if there is existing level name, else return 1
	/// </summary>
	/// <param name="string oldName"></param>
	/// <param name="string newName"></param>
	/// <returns></returns>
	public static int checkValidLevelName(string oldName,string newName)
	{
		//student
		string query = String.Format("SELECT CustomLevelName FROM(SELECT * FROM CustomLevel c2 WHERE c2.CustomLevelName <> '{0}') AS c " +
			"WHERE c.StudentId = {1} AND c.CustomLevelName = '{2}'; ", oldName, Global.StudentId, newName);

		//teacher
		/*string query = String.Format("SELECT AssignmentName FROM(SELECT * FROM `Assignment` a2 WHERE a2.AssignmentName <> '{1}') AS a " +
			"WHERE TeacherId = {0} AND AssignmentName = '{1}'; ", Global.TeacherId, name);*/

		BaseDao<string> baseDao = new BaseDao<string>();
		string name = baseDao.RetrieveQuery(query);

		if (name == null)
		{
			GD.Print("Did not find any");
			return 1;
		}
		GD.Print("Found matching");
		return -1;
	}

	/// <summary>
	/// Load all information on CustomLevel
	/// </summary>
	/// <returns></returns>
	public CustomLevel getLevelInfo() 
	{
		string query = String.Format("SELECT CustomLevelId, CustomLevelName, TimeLimit, StudentId, MonsterId, QuestionId, Option1, Option2, Option3, " +
			"CorrectOption, QuestionTitle FROM CustomLevel NATURAL JOIN StudentCustomQuestion " +
			"NATURAL JOIN Question WHERE CustomLevelId = {0};", Global.CustomLevelId);

		Dictionary<int, CustomLevel> clDict;
		using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
		{
			clDict = new Dictionary<int, CustomLevel>();
			var list = conn.Query<CustomLevel, Student,Monster, Question, CustomLevel>(
				query,
				(cl,s, m, q) =>
				{
					CustomLevel customLevel;
					if (!clDict.TryGetValue(cl.CustomLevelId, out customLevel))
					{
						customLevel = cl;
						customLevel.Question = new List<Question>();
						clDict.Add(customLevel.CustomLevelId, customLevel);
					}
					customLevel.Question.Add(q);
					customLevel.Monster = m;
					customLevel.Student = s;
					return customLevel;
				}, splitOn: "CustomLevelId, StudentId, MonsterId, QuestionId").Distinct().ToList();
		}

		levelInfo = clDict[Global.CustomLevelId];
		return levelInfo;
	}
}


