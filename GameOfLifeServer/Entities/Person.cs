namespace GameOfLifeServer.Entities
{
    public enum BloodType
    {
        Aplus,
        Aminus,
        Bplus,
        Bminus,
        ABplus,
        ABminus,
        Oplus,
        Ominus
    }
    public enum Gender
    {
        Male,
        Female
    }
    public enum Mood
    {
        Angry,
        Happy,
        Disgust,
        Sad,
        Omg,
        Scared
    }

    public record struct Vector2Pos
    {
        public int X { get; init; }
        public int Y { get; init; }
    }

    public class Person
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
    }
}
