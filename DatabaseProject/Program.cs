using DatabaseProject.Models;
using DatabaseProject.Repositories;
using System;

namespace DatabaseProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database db = new Database();
            db.Connect();

            // 3
            Console.WriteLine("Funkcia získa záznam z tabuľky user podľa id_user");
            User user = UserRepository.GetUserById(db, 1);
            Console.WriteLine($"User Name: {user.Name}, Surname: {user.Surname}, Email: {user.Email}");

            Console.WriteLine();
            // 1
            Console.WriteLine("Funkcia získa záznam z tabulky Trainer podla trainer_id ");
            Trainer trainer = TrainerRepository.GetTrainerById(db, 1);
            Console.WriteLine($"Trainer Name: {trainer.Name}, Surname: {trainer.Surname}");

            Console.WriteLine();
            //2
            Console.WriteLine("Funkcia získa záznamy z tabuľky user podľa trénera");
            int trainerId = 1;

            List<User> users = UserRepository.GetUsersByTrainer(db, trainerId);

            foreach (User user2 in users)
            {
                Console.WriteLine($"User ID: {user2.Id}, Name: {user2.Name}, Surname: {user2.Surname}, Email: {user2.Email}, Phone: {user2.Phone}");
            }

            TrainingPlan trainingPlan = TrainingPlanRepository.GetTrainingPlanById(db, 1);
            Console.WriteLine($"TrainingPlan for: {trainingPlan.UserId},Name : {user.Name}, Description: {trainingPlan.Description}");

            Console.WriteLine();
            //5
            Console.WriteLine("Funkcia vráti zoznam Exercises z tabuľky exercises ");
            List<Exercise> exercises = ExerciseRepository.GetAllExercises(db);

            foreach (Exercise exercise5 in exercises)
            {
                Console.WriteLine($"ID: {exercise5.Id}, Name: {exercise5.Name}, Description: {exercise5.Description}, Type: {exercise5.Type}");
            }
            Console.WriteLine();
            //6
            Console.WriteLine("Funkcia filtruje Exercices podľa ex_type");
            string ex_type = "Back";
            List<Exercise> exercises2 = ExerciseRepository.GetExercisesByType(db, ex_type);

            foreach (Exercise exercise4 in exercises2)
            {
                Console.WriteLine($"ID: {exercise4.Id}, Name: {exercise4.Name}, Description: {exercise4.Description}, Type: {exercise4.Type}");
            }

            Console.WriteLine();
            //7
            Console.WriteLine("Funkcia filtruje Exercises podľa názvu ex_name ");
            Exercise exercise = ExerciseRepository.GetExerciseByName(db, "Bench press");
            Console.WriteLine($"Exercise id: {exercise.Id},Nazov: {exercise.Name},Typ: {exercise.Type}");

            Console.WriteLine();
            // Test vytvorenie planu
            TrainingPlan newPlan = new TrainingPlan
            {
                UserId = 50,
                CreatedAt = DateTime.Now,
                Description = "New training plan",
                Active = true
            };
            Console.WriteLine($"TrainingPlan for: {newPlan.UserId}, Description: {newPlan.Description}");

            Console.WriteLine();
            // exercise podla id test
            Exercise exercise2 = ExerciseRepository.GetExerciseById(db, 1);
            Console.WriteLine($"Exercise Name: {exercise2.Name}, Type: {exercise2.Type}");

            bool result = Transactions.CreateTrainingPlan(db, 12, 12, 11, 3);

            if (result)
            {
                Console.WriteLine("Treningovy plan bol vytvoreny");
            }
            else
            {
                Console.WriteLine("Nepodarilo sa cosi");
            }

            db.Close();
        }
    }
}
