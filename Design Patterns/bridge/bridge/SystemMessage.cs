using System;
namespace bridge
{
    abstract public class SystemMessage
    {
        protected Language language;

        public SystemMessage(Language language)
        {
            this.language = language;
        }
        abstract public void ShowMessage();
    }
}
