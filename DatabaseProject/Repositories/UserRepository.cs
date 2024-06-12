using DatabaseProject;
using DatabaseProject.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class UserRepository
{
    public static User GetUserById(Database pdb, int id)
    {

        SqlCommand command = pdb.CreateCommand(@"
            SELECT id_user, trainer_id_trainer, name, surname, email, phone, role, password, deleted_at
            FROM [user]
            WHERE id_user = @id");
        command.Parameters.AddWithValue("@id", id);

        User user = null;
        using (SqlDataReader reader = pdb.Select(command))
        {
            if (reader.Read())
            {
                user = new User
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id_user")),
                    TrainerId = reader.GetInt32(reader.GetOrdinal("trainer_id_trainer")),
                    Name = reader["name"].ToString(),
                    Surname = reader["surname"].ToString(),
                    Email = reader["email"].ToString(),
                    Phone = reader["phone"].ToString(),
                    Role = reader["role"].ToString()[0],
                    Password = reader["password"].ToString(),
                    DeletedAt = reader.IsDBNull(reader.GetOrdinal("deleted_at")) ? default(DateTime?) : reader.GetDateTime(reader.GetOrdinal("deleted_at"))
                };
            }
        }


        return user;
    }

    public static List<User> GetUsersByTrainer(Database pdb, int trainerId)
    {
        string query = @"SELECT id_user, trainer_id_trainer, name, surname, email, phone, role, password, deleted_at
                     FROM[user]
                     WHERE trainer_id_trainer = @trainerId";

        SqlCommand command = pdb.CreateCommand(query);
        command.Parameters.AddWithValue("@trainerId", trainerId);

        List<User> users = new List<User>();
        using (SqlDataReader reader = pdb.Select(command))
        {
            while (reader.Read())
            {
                User user = new User
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id_user")),
                    TrainerId = reader.GetInt32(reader.GetOrdinal("trainer_id_trainer")),
                    Name = reader["name"].ToString(),
                    Surname = reader["surname"].ToString(),
                    Email = reader["email"].ToString(),
                    Phone = reader["phone"].ToString(),
                    Role = reader["role"].ToString()[0],
                    Password = reader["password"].ToString(),
                    DeletedAt = reader.IsDBNull(reader.GetOrdinal("deleted_at")) ? null : reader.GetDateTime(reader.GetOrdinal("deleted_at"))
                };
                users.Add(user);
            }
        }

        return users;
    }
}





