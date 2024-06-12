using DatabaseProject.Models;
using DatabaseProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseProject
{
    public static class Transactions
    {
        public static bool CreateTrainingPlan(Database pdb, int trainerId, int userId, int planId, int exerciseId)
        {


            // transakce
            pdb.BeginTransaction();

            try
            {
                // Kontrola ci trener ma uzivatela
                Trainer trainer = TrainerRepository.GetTrainerById(pdb, trainerId);
                User user = UserRepository.GetUserById(pdb, userId);
                bool isAssigned = trainer != null && user != null;

                // kontrola ci pouzivatel existuje
                bool userExists = user != null;

                // Ci uz ma pouzivatel treningovy plan
                TrainingPlan existingPlan = TrainingPlanRepository.GetTrainingPlanById(pdb, planId);
                bool planExists = existingPlan != null;

                // Ci cvik existuje 
                Exercise exercise = ExerciseRepository.GetExerciseById(pdb, exerciseId);
                bool exerciseExists = exercise != null;

                // Ak ma uz uzivatel plan oznacime ho ako neaktivny
                if (planExists)
                {
                    existingPlan.Active = false;
                    TrainingPlanRepository.UpdateTrainingPlan(pdb, existingPlan);
                }

                // Vytvorenie planu ked vsetko prebehne pohode
                if (isAssigned && userExists && exerciseExists)
                {
                    TrainingPlan newPlan = new TrainingPlan
                    {
                        Id = planId,
                        UserId = userId,
                        CreatedAt = DateTime.Now,
                        Description = "New training plan",
                        Active = true
                    };
                    TrainingPlanRepository.InsertTrainingPlan(pdb, newPlan);

                    //Console.WriteLine("Ok");
                    pdb.EndTransaction();
                    return true;
                }
                else
                {
                    //Console.WriteLine("Not ok");
                    pdb.Rollback();
                    return false;
                }
            }
            catch (Exception)
            {
                // zrusenie tranzakcie ak sa cosi pokazi
                pdb.Rollback();
                throw;
            }
        }
    }
}