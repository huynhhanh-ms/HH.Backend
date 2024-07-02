namespace PI.Domain.Enums
{
    public enum SignalRType
    {
        ReceiveNotification
    }
    public enum NotificationType
    {
        STOCKCHECK_CREATE,
        STOCKCHECK_ASSIGN,
        STOCKCHECK_ACCEPT_ASSIGN,
        STOCKCHECK_REJECT_ASSIGN,
        STOCKCHECK_SUBMIT,
        STOCKCHECK_CONFIRM,
        STOCKCHECK_REJECT,
        STOCKCHECK_COMPLETE
    }

    public enum  StockCheckNotificationType
    {
        CREATE,
        ASSIGN,
        ACCEPT_ASSIGN,
        REJECT_ASSIGN,
        SUBMIT,
        CONFIRM,
        REJECT,
        COMPLETE
    }
}