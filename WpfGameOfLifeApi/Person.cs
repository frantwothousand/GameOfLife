using GameOfLifeApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameOfLifeApi
{
    public interface IPerson
    {
        string Name { get; set; }
        string LastName { get; set; }
        string Avatar { get; set; }
        int Age { get; set; }
        bool IsEmployed { get; set; }
        int Salary { get; set; }
        bool IsMarried { get; set; }
        bool IsAlive { get; set; }
        Mood Mood { get; set; }
        Gender Gender { get; set; }
        Vector2Pos Position { get; set; }
        BloodType Blood { get; set; }
        
    }

    public class Person : IPerson
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public int Age { get; set; }
        public bool IsEmployed { get; set; }
        public int Salary { get; set; }
        public bool IsMarried { get; set; }
        public bool IsAlive { get; set; }
        public Mood Mood { get; set; }
        public Gender Gender { get; set; }
        public Vector2Pos Position { get; set; }
        public BloodType Blood { get; set; }
        public bool HasCovid { get; set; }
        public int Life { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Weapon { get; set; }
        public int TotalDamage { get; set; }
        public int Resistence { get; set; }
        public int TotalLife { get; set; }

        public List<Person> Pareja { get; set; }
        public List<Person> Hijo { get; set; }

        /// <summary>
        /// Creamos una 'persona' que tiene propiedades. Y que al crearse, se encuentra soltero.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lastName"></param>
        /// <param name="age"></param>
        /// <param name="isEmployed"></param>
        /// <param name="salary"></param>
        /// <param name="isMarried"></param>
        /// <param name="isAlive"></param>
        /// <param name="mood"></param>
        /// <param name="gender"></param>
        /// <param name="position"></param>
        /// <param name="blood"></param>

        public void Move(Vector2Pos position)
        {
            Position = position;
        }

        public void ChangeCivilState(bool gotmarried)
        {
            IsMarried = gotmarried;
        }

        public void ChangeMood(Mood mood)
        {
            Mood = mood;
        }

        public void ChangeSalary(int salary)
        {
            Salary = salary;
        }

        public void ChangeEmployment(bool isEmployed)
        {
            IsEmployed = isEmployed;
        }

        public string GetFullName()
        {
            return $"{Name} {LastName}";
        }

        public string GetShortName()
        {
            return $"{Name} {LastName[0]}";
        }

        public string GetCivilState()
        {
            return IsMarried ? "Casado" : "Soltero";
        }

        public string GetEmploymentState()
        {
            return IsEmployed ? "Trabajando" : "Desempleado";
        }

        private string TranslateMood()
        {
            return Mood switch
            {
                Mood.Angry => "Enojado",
                Mood.Happy => "Feliz",
                Mood.Disgust => "Asqueado",
                Mood.Sad => "Triste",
                Mood.Omg => "Sorprendido",
                Mood.Scared => "Asustado",
                _ => "Sin estado de animo",
            };
        }

        public string GetFullMood()
        {
            return $"{TranslateMood()} ({GetEmojiByMood()})";
        }

        private string TranslateGender()
        {
            return Gender switch
            {
                Gender.Male => "Masculino",
                Gender.Female => "Femenino",
                _ => "Sin genero"
            };
        }

        public string GetGender()
        {
            return TranslateGender();
        }

        public string GetSymbolByGender()
        {
            return Gender switch
            {
                Gender.Male => "♂️",
                Gender.Female => "♀️",
                _ => "⚥"
            };
        }

        public string GetEmojiByGender()
        {
            return Gender switch
            {
                Gender.Male => "👨🏼",
                Gender.Female => "👩🏼",
                _ => "😵"
            };
        }
        

        // Format the blood type.
        private string TranslateBloodType()
        {
            return Blood switch
            {
                BloodType.Aplus => "A+",
                BloodType.Aminus => "A-",
                BloodType.Bplus => "B+",
                BloodType.Bminus => "B-",
                BloodType.ABplus => "AB+",
                BloodType.ABminus => "AB-",
                BloodType.Oplus => "O+",
                BloodType.Ominus => "O-",
                _ => "Sin tipo de sangre"
            };
        }

        public string GetBloodType()
        {
            return TranslateBloodType();
        }

        // Estado de animo (Mood)

        // Esto es visual, para representarlo en el Box de Info de la persona en el componente Game.xaml.

        public string GetEmojiByMood()
        {
            return Mood switch {
                Mood.Angry => "😡",
                Mood.Happy => "😀",
                Mood.Disgust => "🤢",
                Mood.Sad => "😢",
                Mood.Omg => "😮",
                Mood.Scared => "😱",
                _ => "😐"
            };
        }
    }
}



