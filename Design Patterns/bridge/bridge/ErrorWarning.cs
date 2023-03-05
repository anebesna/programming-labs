using System;

namespace bridge
{
    public class ErrorWarning : SystemMessage
    {
        public ErrorWarning(Language language) : base(language) { }

        public override void ShowMessage()
        {
            language.translate("errorMessage");
        }
    }
}
