namespace InvitationManagerAPI.Dtos.User
{
    public class AddPersonDto
    {
        public string email { get; set; }
        public string telefonszam { get; set; }
        public string erzekenysegek { get; set; }
        public string vallas { get; set; }
        public string fogyatekossag { get; set; }
        public string titulus { get; set; }
        public int userType { get; set; }
        public string ProfilePic { get; set; }
        public string utolsoesemeny { get; set; }
        public string name { get; set; }
    }
}
