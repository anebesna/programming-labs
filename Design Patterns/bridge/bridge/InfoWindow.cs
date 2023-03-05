using System;
namespace bridge
{
    public class InfoWindow : SystemMessage
    {
        public InfoWindow(Language language) : base(language) { }

        public override void ShowMessage()
        {
            language.translate("infoMessage");
        }
    }
}
