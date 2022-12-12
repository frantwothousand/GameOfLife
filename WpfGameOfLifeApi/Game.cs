using Bogus;
using GameOfLifeApi.Enums;
using GameOfLifeApi.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace GameOfLifeApi
{  
    public class FakeData
    {
        public static List<Person> persons = new List<Person>();

        public void Startup()
        {
            Thread _bgStart = new Thread(new ThreadStart(GenerateFakeData));
            _bgStart.Priority = ThreadPriority.Highest;
            _bgStart.Start();
            //GenerateFakeData();
        }

        
        public static void GenerateFakeData()
        {
            var _fakeData = new Faker<Person>() 
                .RuleFor(u => u.Gender, f => f.PickRandom<Gender>())
                .RuleFor(u => u.Name, (f, u) => f.Name.FirstName(u.Gender == Gender.Male ? Bogus.DataSets.Name.Gender.Male : Bogus.DataSets.Name.Gender.Female))
                .RuleFor(p => p.LastName, f => f.Name.LastName() + " " + f.Name.LastName())
                .RuleFor(p => p.Avatar, f => f.Internet.Avatar())
                .RuleFor(p => p.Age, f => f.Random.Int(18, 40))
                .RuleFor(p => p.IsEmployed, f => f.Random.Bool())
                .RuleFor(p => p.Salary, f => f.Random.Int(1000, 10000))
                .RuleFor(p => p.IsMarried, f => false)
                .RuleFor(p => p.IsAlive, f => true)
                .RuleFor(p => p.Mood, f => f.PickRandom<Mood>())
                .RuleFor(p => p.Position, f => new Vector2Pos { X = f.Random.Int(0, 48), Y = f.Random.Int(0, 84) })
                .RuleFor(p => p.Blood, f => f.PickRandom<BloodType>());

            var _fakeDataList = _fakeData.Generate(100);

            foreach(var item in _fakeDataList)
            {
                //persons.Add(item);
                if (!item.IsEmployed) // (?) Se supone que si no tiene empleo, no tiene un sueldo.
                    item.Salary = 0;
            }

            JsonFileUtils.PrettyWrite(_fakeDataList, "fakeData.json");
        }

        public static void GetGeneratedPersons(string filename) 
        {
            JsonFileUtils.ReadFromFile(filename, ref persons);

            int count = new int();
            foreach(var item in persons)
            {
                count++;
            }
            Debug.WriteLine($"Se cargaron {count} objetos desde un JSON.");
        }

        public static void GenerateRelationship(List<Person> ListPerson)
        {
            List<Person> WomanSingle = new List<Person>(); //Agregar a todas las solteras a casar
            List<Person> ManSingle = new List<Person>(); //Agregar a todas los solteros a casar

            foreach (Person person in ListPerson)
            {
                if ((person.Gender == Gender.Female) && (person.IsMarried == false)) WomanSingle.Add(person);
                else if ((person.Gender == Gender.Male) && (person.IsMarried == false)) ManSingle.Add(person);
            }

            int counter = 0;
            foreach(Person woman in WomanSingle)
            {
                do
                {
                    foreach (Person man in ManSingle)
                    {
                        counter++;
                        if ((woman.Age + 5) <= man.Age) //Comprueba si el hombre es mayor o igual a la edad de la mujer
                        {
                            if((woman.IsEmployed == true) &&  (woman.Salary <= man.Salary)) //Valida si la mujer trabaj y si lo hace que su slario sea igual o menor al del hombre
                            {
                                woman.IsMarried = true;
                                man.IsMarried = true;

                                // Según la data analizada en base a la lógica, los que se casen, deben ser actualizados en la lista principal.
                                
                                ListPerson.Find(x => x.Name == man.Name)!.ChangeCivilState(true);
                                ListPerson.Find(x => x.Name == woman.Name)!.ChangeCivilState(true);

                                WomanSingle.Remove(woman);
                                ManSingle.Remove(man);
                            }
                            else if(man.IsEmployed == true)
                            {
                                woman.IsMarried = true;
                                man.IsMarried = true;

                                ListPerson.Find(x => x.Name == man.Name)!.ChangeCivilState(true); // If false, not married.
                                ListPerson.Find(x => x.Name == woman.Name)!.ChangeCivilState(true); // True if married

                                WomanSingle.Remove(woman);
                                ManSingle.Remove(man);
                            }

                        }
                    }
                }
                while((woman.IsMarried == false) || (counter == ManSingle.Count));
            }
        }

        public static void ValidateIfGenerateChildren(List<Person> ListPerson)
        {
            List<Person> ManMarried = new List<Person>(); //Agregar a todos los casados
            List<Person> GenerateChilds = new List<Person>();

            foreach (Person person in ListPerson)
            {
                if ((person.Gender == Gender.Male) && (person.IsMarried == true)) ManMarried.Add(person);
            }

            foreach (Person husband in ManMarried)
            {
                if ((husband.Blood == BloodType.Aplus || husband.Blood == BloodType.Bplus || husband.Blood == BloodType.Oplus || husband.Blood == BloodType.ABplus) && (husband.Pareja.Blood == BloodType.Aplus || husband.Pareja.Blood == BloodType.Bplus || husband.Pareja.Blood == BloodType.Oplus || husband.Pareja.Blood == BloodType.ABplus)) GenerateChildren(ListPerson, ManMarried, GenerateChilds);
                if ((husband.Blood == BloodType.Aminus || husband.Blood == BloodType.Bminus || husband.Blood == BloodType.Ominus || husband.Blood == BloodType.ABminus) && (husband.Pareja.Blood == BloodType.Aminus || husband.Pareja.Blood == BloodType.Bminus || husband.Pareja.Blood == BloodType.Ominus || husband.Pareja.Blood == BloodType.ABminus)) GenerateChildren(ListPerson, ManMarried, GenerateChilds);
            }
        }

        public static void GenerateChildren(List<Person> ListPerson, List<Person> GenerateChilds)
        {
            Random random = new Random();
            Gender genderBaby;

            int partosPosibles = 1; //Dice que por parto solo puede tener 1 hijo, pero no dice cuanto partos asi que por ahora solo pondré 1 parto

            foreach (Person person in GenerateChilds)
            {
                // Generar con Faker. //Lo unico que hay que generar con FAKER aqui creo que sería el estado de animo, las deas cosas las heredan del padre o son logica como si esta vivo
                Person hijo = new Person();
                genderBaby = (Gender)random.Next(0, 2);
                var _person = new Faker<Person>();
                if (person.Blood == person.Pareja.Blood)
                {
                    if (genderBaby == Gender.Male)
                    {
                        /*_person
                            .RuleFor(u => u.Gender, f => f.PickRandom<Gender>())
                            .RuleFor(u => u.Name, (f, u) => f.Name.FirstName(u.Gender == Gender.Male ? Bogus.DataSets.Name.Gender.Male : Bogus.DataSets.Name.Gender.Female))
                            .RuleFor(u => u.LastName, person.LastName + " " + person.Pareja.LastName)
                            .RuleFor(p => p.IsMarried, f => false)
                            .RuleFor(p => p.IsAlive, f => true)
                            .RuleFor(p => p.Mood, f => f.PickRandom<Mood>())
                            .RuleFor(p => p.Position, f => new Vector2Pos { X = f.Random.Int(0, 48), Y = f.Random.Int(0, 84) })
                            .RuleFor(p => p.Age, 0)
                            .RuleFor(p => p.IsMarried, false)
                            .RuleFor(p => p.Salary, 0)
                            .RuleFor(p => p.Avatar, f => f.Internet.Avatar())
                            .RuleFor(p => p.Blood, person.Blood)
                            .RuleFor(p => p.IsEmployed, false);

                        var child = _person.Generate(1);*/
                            hijo.Gender = genderBaby;
                        hijo.Name = person.Name;
                        //hijo.Avatar = ;
                        hijo.Age = 0;
                        hijo.Salary = 0;
                        hijo.Blood = person.Blood;
                        hijo.Mood = (Mood)random.Next(0, 6);
                        hijo.IsMarried = false;
                        hijo.IsEmployed = false;
                        hijo.IsAlive = true;

                        //Agregarle los hijos
                        person.Hijo.Add(hijo);
                    }
                }

                ListPerson.Add(hijo);
                UpdateList(person, ListPerson, hijo);
            }            
        }

        public static void UpdateList(Person person, List<Person> ListPerson, Person hijo)
        {
            foreach(Person oldPerson in ListPerson)
            {
                if(oldPerson == person.Pareja)
                {
                    ListPerson.Remove(oldPerson); //Remover a la mamá ya que no contiene al hijo y agregarla a Lista con el hijo

                    //Agregarle el hijo a la pareja
                    person.Pareja.Hijo.Add(hijo);
                    ListPerson.Add(person.Pareja);
                }
            }
        }

        public static void GenerateCovid(List<Person> ListPerson, double infections) //Infections es el % de la población que el usuario quiere que se infecte de covid
        {
            Random random = new Random();
            int countListPerson = ListPerson.Count;
            int randomPerson;
            List<Person> PeopleWithCovid = new List<Person>();

            //Infectarlo
            for (double i = 0; i < (countListPerson * (infections/100)); i++)
            {
                do
                {
                    randomPerson = random.Next(0, countListPerson);
                }
                while (ListPerson[randomPerson].HasCovid != false); //Si no tiene covid entonces si la podemso infectar, si ya tiene convid no la puedo infectar si ya tiene

                ListPerson[randomPerson].HasCovid = true; //Las infecto
                PeopleWithCovid.Add(ListPerson[randomPerson]);
            }

            //Matarlo
            for (double i = 0; i < (PeopleWithCovid.Count * (20/100)) ; i++)
            {
                do
                {
                    randomPerson = random.Next(0, PeopleWithCovid.Count);
                }
                while ((ListPerson[randomPerson].Age <= 40) && (ListPerson[randomPerson].Gender == Gender.Female) && (ListPerson[randomPerson].IsMarried == true));

                ListPerson.Remove(ListPerson[randomPerson]); //Murió
            }
        }

        public static void GetAttributes(List<Person> ListPerson)
        {
            Random random = new Random();

            foreach(Person person in ListPerson)
            {
                person.Life = random.Next(50, 70);
                //person.Ataque = random.Next(30, 75);
                person.Defense = random.Next(20, 50);
                person.Weapon = random.Next(5, 10);
                person.TotalDamage = person.Attack + person.Weapon;
                person.TotalLife = person.Life + person.Defense;
            }
        }

        public static void FightOfTheStrongest(List<Person> ListPerson)
        {
            int cantListPerson = ListPerson.Count;
            if  (cantListPerson % 2 == 0) cantListPerson = cantListPerson / 2;
            else cantListPerson = (cantListPerson - 1) / 2;


            for (int i = 0; i < cantListPerson; i++) //Es pelea de 2, de la lsita completa se pelea el primero con el segundo, el tercero con el cuarto y asi sucesivamente...
            {
                do
                {
                    ListPerson[i].TotalLife -= ListPerson[i + 1].TotalDamage;
                    ListPerson[i + 1].TotalLife -= ListPerson[i].TotalDamage;
                }
                while (ListPerson[i].TotalLife >= 1 || ListPerson[i+1].TotalLife > 1); //Si uno de los dos muere entonces dejan de pelear

                if (ListPerson[i].TotalLife <= 0) ListPerson.Remove(ListPerson[i]); //Si murió lo elimino al jugador 1
                else //Aumento la resistencia y el poder del arma si es que gano la batalla
                {
                    ListPerson[i].Resistence += 1;
                    ListPerson[i].Weapon += 1;
                }

                if (ListPerson[i+1].TotalLife <= 0) ListPerson.Remove(ListPerson[i+1]); //Si murió lo elimino al jugador 2
                else //Aumento la resistencia y el poder del arma si es que gano la batalla
                {
                    ListPerson[i+1].Resistence += 1;
                    ListPerson[i + 1].Weapon += 1;
                }
            }
        } 

        public static void AdvanceOfTime(List<Person> ListPerson, int advanceYears) //Hay que pedirle al usuario cuantos años quiere adelantar
        {
            foreach (Person person in ListPerson)
            {
                person.Age = person.Age + advanceYears;
                if (person.Age >= 65) ListPerson.Remove(person);
            }

        }

    }
}


