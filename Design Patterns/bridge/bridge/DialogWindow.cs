using System;
namespace bridge
{
    public class DialogWindow : SystemMessage
    {
        public DialogWindow(Language language) : base(language) { }

        public override void ShowMessage()
        {
            language.translate("dialogMessage");
        }
    }
}
