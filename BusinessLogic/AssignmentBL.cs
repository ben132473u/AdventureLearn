using System;
using System.Collections.Generic;

/// <summary>
/// Class to handle Business Logic for Assignment
/// </summary>
public class AssignmentBL
{
    /// <summary>
    /// Get selected Assignment
    /// </summary>
    /// <param name="assignmentId"></param>
    /// <returns>Return Assignment object</returns>
    public Assignment GetAssignment(int assignmentId)
    {
        AssignmentDaoImpl assignmentDao = new AssignmentDaoImpl();
        return assignmentDao.GetAssignment(assignmentId);
    }
    /// <summary>
    /// Get all Student's published Assignments
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns>Return list of PublishedAssignment object</returns>
    public List<PublishedAssignment> GetStudentAssignment(int studentId)
    {
        AssignmentDaoImpl assignmentDao = new AssignmentDaoImpl();
        return assignmentDao.GetStudentAssignment(studentId);
    }
    /// <summary>
    /// Get Monster that belongs to selected Assignment
    /// </summary>
    /// <param name="assignmentId"></param>
    /// <returns>Return Monster object</returns>
    public Monster GetAssignmentMonster(int assignmentId)
    {
        AssignmentDaoImpl assignmentDao = new AssignmentDaoImpl();
        return assignmentDao.GetAssignmentMonster(assignmentId);
    }

}
