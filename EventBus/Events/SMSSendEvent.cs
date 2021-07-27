namespace EventBus.Messages.Events
{
    public class SMSSendEvent : IntegrationBaseEvent
    {
        public string PhoneNumber { get; set; }
        public string SMSText { get; set; }
    }
}
