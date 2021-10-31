namespace JobApplication.Entities
{
    public class JobPostingDTO
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string Employer { get; set; }
        public string SkillName { get; set; }
    }
}
