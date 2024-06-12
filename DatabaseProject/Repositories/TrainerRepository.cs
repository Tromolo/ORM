using DatabaseProject;
using DatabaseProject.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public static class TrainerRepository
{
    public static Trainer GetTrainerById(Database pdb, int id)
    {

        string query = @"SELECT id_trainer, name, surname, email, phone, specialisation, skills, deleted_at
                         FROM trainer
                         WHERE id_trainer = @id";

        SqlCommand command = pdb.CreateCommand(query);
        command.Parameters.AddWithValue("@id", id);

        Trainer trainer = null;
        using (SqlDataReader reader = pdb.Select(command))
        {
            if (reader.Read())
            {
                trainer = new Trainer
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id_trainer")),
                    Name = reader["name"].ToString(),
                    Surname = reader["surname"].ToString(),
                    Email = reader["email"].ToString(),
                    Phone = reader["phone"].ToString(),
                    Specialisation = reader["specialisation"].ToString(),
                    Skills = reader["skills"].ToString(),
                    DeletedAt = reader.IsDBNull(reader.GetOrdinal("deleted_at")) ? default(DateTime?) : reader.GetDateTime(reader.GetOrdinal("deleted_at"))
                };
            }
        }

        return trainer;
    }
}

