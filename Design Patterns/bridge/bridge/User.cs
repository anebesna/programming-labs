using System;
namespace bridge
{
    public class User
    {
        public Language language = new English();
        public string username;

        public User(string username)
        {
            this.username = username;
        }
        public void ChangeLanguage(Language language)
        {
            this.language = language;
        }
    }
}
