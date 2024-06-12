using DatabaseProject;
using DatabaseProject.Models;
using System;
using System.Data.SqlClient;

public static class TrainingPlanRepository
{
    public static TrainingPlan GetTrainingPlanById(Database pdb, int id)
    {

        string query = @"SELECT id_training_plan, user_id_user, created_at, description, active
                         FROM training_plan
                         WHERE id_training_plan = @id";

        SqlCommand command = pdb.CreateCommand(query);
        command.Parameters.AddWithValue("@id", id);

        TrainingPlan trainingPlan = null;
        using (SqlDataReader reader = pdb.Select(command))
        {
            if (reader.Read())
            {
                trainingPlan = new TrainingPlan
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id_training_plan")),
                    UserId = reader.GetInt32(reader.GetOrdinal("user_id_user")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
                    Description = reader["description"].ToString(),
                    Active = reader.GetBoolean(reader.GetOrdinal("Active"))
                };
            }
        }

        return trainingPlan;
    }

    public static void UpdateTrainingPlan(Database pdb, TrainingPlan plan)
    {

        string query = @"UPDATE training_plan SET active = @active WHERE id_training_plan = @id";

        SqlCommand command = pdb.CreateCommand(query);
        command.Parameters.AddWithValue("@active", plan.Active);
        command.Parameters.AddWithValue("@id", plan.Id);

        pdb.ExecuteNonQuery(command);


    }

    public static void InsertTrainingPlan(Database pdb, TrainingPlan plan)
    {

        string query = @"INSERT INTO training_plan (user_id_user, created_at, description, active)
                         VALUES (@userId, @createdAt, @description, @active)";

        SqlCommand command = pdb.CreateCommand(query);
        command.Parameters.AddWithValue("@userId", plan.UserId);
        command.Parameters.AddWithValue("@createdAt", plan.CreatedAt);
        command.Parameters.AddWithValue("@description", plan.Description);
        command.Parameters.AddWithValue("@active", plan.Active);

        pdb.ExecuteNonQuery(command);


    }
}
