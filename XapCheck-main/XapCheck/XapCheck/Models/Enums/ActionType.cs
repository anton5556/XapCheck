using System;

namespace XapCheck.Models
{
    public enum ActionType
    {
        Add = 0,
        Update = 1,
        Delete = 2,
        IncreaseQuantity = 3,
        DecreaseQuantity = 4,
        ExpiryWarning = 5,
        PurchaseSuggested = 6,
        PurchaseCompleted = 7
    }
}
