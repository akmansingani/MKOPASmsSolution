namespace EventBus.Messages.Events
{
    public class SMSSendCompletedEvent : IntegrationBaseEvent
    {
        public string PhoneNumber { get; set; }
        public string SMSText { get; set; }
    }
}
