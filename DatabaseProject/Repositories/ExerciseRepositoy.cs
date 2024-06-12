using DatabaseProject.Models;
using System;
using System.Data.SqlClient;

namespace DatabaseProject.Repositories
{
    public static class ExerciseRepository
    {
        public static Exercise GetExerciseById(Database pdb, int id)
        {

            string query = @"SELECT id, name, description, type
                             FROM exercise
                             WHERE Id = @id";

            SqlCommand command = pdb.CreateCommand(query);
            command.Parameters.AddWithValue("@id", id);

            Exercise exercise = null;
            using (SqlDataReader reader = pdb.Select(command))
            {
                if (reader.Read())
                {
                    exercise = new Exercise
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        Name = reader["name"].ToString(),
                        Description = reader["description"].ToString(),
                        Type = reader["type"].ToString()
                    };
                }
            }
            return exercise;
        }

        public static Exercise GetExerciseByName(Database pdb, string ex_name)
        {

            string query = @"SELECT id, name, description, type
                     FROM exercise
                     WHERE name = @name";

            SqlCommand command = pdb.CreateCommand(query);
            command.Parameters.AddWithValue("@name", ex_name);

            Exercise exercise = null;
            using (SqlDataReader reader = pdb.Select(command))
            {
                if (reader.Read())
                {
                    exercise = new Exercise
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        Name = reader["name"].ToString(),
                        Description = reader["description"].ToString(),
                        Type = reader["type"].ToString()
                    };
                }
            }

            return exercise;
        }

        public static List<Exercise> GetExercisesByType(Database pdb, string ex_type)
        {

            string query = @"SELECT id, name, description, type
                     FROM exercise
                     WHERE type = @type";

            SqlCommand command = pdb.CreateCommand(query);
            command.Parameters.AddWithValue("@type", ex_type);

            List<Exercise> exercises = new List<Exercise>();
            using (SqlDataReader reader = pdb.Select(command))
            {
                while (reader.Read())
                {
                    Exercise exercise = new Exercise
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        Name = reader["name"].ToString(),
                        Description = reader["description"].ToString(),
                        Type = reader["type"].ToString()
                    };
                    exercises.Add(exercise);
                }
            }

            return exercises;
        }

        public static List<Exercise> GetAllExercises(Database pdb)
        {

            string query = @"SELECT id, name, description, type
                     FROM exercise";

            SqlCommand command = pdb.CreateCommand(query);

            List<Exercise> exercises = new List<Exercise>();
            using (SqlDataReader reader = pdb.Select(command))
            {
                while (reader.Read())
                {
                    Exercise exercise = new Exercise
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        Name = reader["name"].ToString(),
                        Description = reader["description"].ToString(),
                        Type = reader["type"].ToString()
                    };
                    exercises.Add(exercise);
                }
            }

            return exercises;
        }


    }

}