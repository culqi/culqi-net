using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi.Constants
{
    public static class Events
    {
        public const string TokenCreationSucceeded = "token.creation.succeeded";
        public const string TokenCreationFailed = "token.creation.failed";
        public const string TokenExpired = "token.expired";
        public const string TokenUpdateSucceeded = "token.update.succeeded";
        public const string TokenUpdateFailed = "token.update.failed";

        public const string ChargeCreationSucceeded = "charge.creation.succeeded";
        public const string ChargeCreationFailed = "charge.creation.failed";
        public const string ChargeExpired = "charge.expired";
        public const string ChargeUpdateFail = "charge.update.fail";
        public const string ChargeCaptureSucceeded = "charge.capture.succeeded";
        public const string ChargeCaptureFailed = "charge.capture.failed";

        public const string RefundCreationSucceeded = "refund.creation.succeeded";
        public const string RefundCreationFailed = "refund.creation.failed";
        public const string RefundUpdateSucceeded	 = "refund.update.succeeded	";
        public const string RefundUpdateFailed = "refund.update.failed";

        public const string CustomerCreationSucceeded = "customer.creation.succeeded";
        public const string CustomerCreationFailed = "customer.creation.failed";
        public const string CustomerDeleteSucceeded = "customer.delete.succeeded";
        public const string CustomerDeleteFailed = "customer.delete.failed";

        public const string CardCreationSucceeded = "card.creation.succeeded";
        public const string CardCreationFailed = "card.creation.failed";
        public const string CardUpdatedSucceeded = "card.updated.succeeded";
        public const string CardUpdatedFailed = "card.updated.failed";
        public const string CardDeleteSucceeded = "card.delete.succeeded";
        public const string CardDeleteFailed = "card.delete.failed";

        public const string PlanCreationSucceeded = "plan.creation.succeeded";
        public const string PlanCreationFailed = "plan.creation.failed";
        public const string PlanUpdateSucceeded = "plan.update.succeeded";
        public const string PlanUpdateFailed = "plan.update.failed";
        public const string PlanDeleteSucceeded = "plan.delete.succeeded";
        public const string PlanDeleteFailed = "plan.delete.failed";

        public const string SubscriptionCreationSucceeded = "subscription.creation.succeeded";
        public const string SubscriptionCreationFailed = "subscription.creation.failed";
        public const string SubscriptionUpdateSucceeded = "subscription.update.succeeded";
        public const string SubscriptionUpdateFailed = "subscription.update.failed";
        public const string SubscriptionCancelSucceeded = "subscription.cancel.succeeded";
        public const string SubscriptionCancelFailed = "subscription.cancel.failed";

        public const string OrderStatusChanged = "order.status.changed";
        public const string OrderCreationSucceeded = "order.creation.succeeded";
    }
}
