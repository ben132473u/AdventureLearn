using System;
using System.Collections.Generic;

/// <summary>
/// Class to handle Business Logic for CustomLevel
/// </summary>
public class CustomLevelBL
{
    /// <summary>
    /// Get selected CustomLevel
    /// </summary>
    /// <param name="customLevelId"></param>
    /// <returns>Return CustomLevel object</returns>
    public CustomLevel GetCustomLevel(int customLevelId)
    {
        CustomLevelDao customLevelDao = new CustomLevelDao();
        return customLevelDao.GetCustomLevel(customLevelId);
    }
    /// <summary>
    /// Get all CustomLevels
    /// </summary>
    /// <returns>Return list of CustomLevel object</returns>
    public List<CustomLevel> GetCustomLevels()
    {
        CustomLevelDao customLevelDao = new CustomLevelDao();
        return customLevelDao.GetCustomLevels();
    }
    /// <summary>
    /// Get all Student's created CustomLevels
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns>Return list of CustomLevel object</returns>
    public List<CustomLevel> GetStudentCustomLevel(int studentId)
    {
        CustomLevelDao customLevelDao = new CustomLevelDao();
        return customLevelDao.GetStudentCustomLevel(studentId);
    }
    /// <summary>
    /// Get all of Student's CustomLevelScore 
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns>Return list of CustomLevelScore</returns>
    public List<CustomLevelScore> GetClearedCustomLevels(int studentId)
    {
        CustomLevelDao customLevelDao = new CustomLevelDao();
        return customLevelDao.GetClearedCustomLevels(studentId);
    }
    /// <summary>
    /// Delete CustomLevel
    /// </summary>
    /// <param name="customLevelId"></param>
    /// <returns>Return 1 if delete query has executed successfully</returns>
    public int DeleteCustomLevel(int customLevelId)
    {
        CustomLevelDao customLevelDao = new CustomLevelDao();
        return customLevelDao.DeleteCustomLevel(customLevelId);
    }
    /// <summary>
    /// Get Monster that belongs to selected CustomLevel
    /// </summary>
    /// <param name="customLevelId"></param>
    /// <returns>Return Monster object</returns>
    public Monster GetCustomLevelMonster(int customLevelId)
    {
        CustomLevelDao customLevelDao = new CustomLevelDao();
        return customLevelDao.GetCustomLevelMonster(customLevelId);
    }

}
