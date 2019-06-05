namespace Lab10
{
    public class Student
    {
        public override string ToString()
        {
            return $"Студент: {Name}, {Age} лет из {Group} группы";
        }

        public override bool Equals(object obj)
        {
            var student = obj as Student;
            if (student == null)
                return false;
            return this.Group == student.Group &&
                   this.Age == student.Age &&
                   this.Name == student.Name;
        }

        public override int GetHashCode()
        {
            return Age << 1 * Name.GetHashCode() * 11 + Group ^ 5;
        }

        public int Age { get; set; }
        public string Name { get; set; } = "";
        public int Group { get; set; }
    }
}