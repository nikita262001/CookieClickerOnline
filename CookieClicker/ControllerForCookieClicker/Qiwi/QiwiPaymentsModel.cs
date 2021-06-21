using System;
using System.Collections.Generic;

namespace ControllerForCookieClicker.Qiwi
{
    public class WebhookQiwiPaymentsModel
    {
        public string hash { get; set; }
        public string hookId { get; set; }
        public string messageId { get; set; }
        public QiwiData payment { get; set; }
    }
    public class QiwiPaymentsModel
    {
        public List<QiwiData> data { get; set; }
    }
    public class QiwiData
    {
        public string txnId { get; set; } // Id 
        public DateTime date { get; set; } // Дата
        public string status { get; set; } // SUCCESS or ERROR
        public string type { get; set; } // IN or OUT
        public SumDonation sum { get; set; } // Сумма
        public string comment { get; set; } // Коментарий

    }
    public class SumDonation
    {
        public decimal amount { get; set; } // Кол-во доната
        public int currency { get; set; } // Номер валюты
    }
}
