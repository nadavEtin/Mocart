using Assets.Scripts.Utility;

namespace Assets.GameCore.Events
{
    public class ErrorEvent : BaseEventParams
    {
        public string ErrorMessage;

        public ErrorEvent(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}