using ControllerForCookieClicker.Different;
using CookieClicker.ClassLibrary.Models;
using System.Linq;

namespace ControllerForCookieClicker.Qiwi
{
    public class QiwiRequest
    {
        static readonly int koefDonationOnRuble = 150000;

        public static void SaveInDataBaseQiwiPayments(QiwiData item)
        {
            DataBaseCookieContext db = new DataBaseCookieContext();
            if (db.Donation.Where(obj => obj.IdQiwi == item.txnId).FirstOrDefault() == null)
            {
                db.Donation.Add(new Donation
                {
                    IdQiwi = item.txnId,
                    Comment = item.comment,
                    Date = item.date,
                    PaymentAmount = item.sum.amount,
                    IdCurrency = item.sum.currency,
                    IdDonateStatus = 1,
                });
            }
            db.SaveChanges();
        }

        public static void DistributionOfCurrencyAmongAccounts()
        {
            DataBaseCookieContext db = new DataBaseCookieContext();
            for (int i = 0; i < db.Donation.ToList().Count; i++)
            {
                var item = db.Donation.ToList()[i];
                if (item.IdDonateStatus == 1)
                {
                    var answer = db.Account.Where(obj => obj.Login == item.Comment).FirstOrDefault();
                    if (answer != null)
                    {
                        var coefCurrency = db.Currency.Where(obj => obj.IdCurrency == item.IdCurrency).FirstOrDefault().RublesToOneCurrency;
                        answer.Cookies += koefDonationOnRuble * item.PaymentAmount * coefCurrency;
                        item.IdDonateStatus = 2;
                    }
                    else
                    {
                        item.IdDonateStatus = 3;
                    }
                }
            }

            db.SaveChanges();
        }
    }
}
