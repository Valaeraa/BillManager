﻿using BM.Library.Models;
using System.Collections.Generic;

namespace BM.Library.DataAccess
{
    public interface IPaymentData
    {
        List<PaymentModel> GetPayments(int clientId);
        void InsertPaymentData(PaymentModel parameters);
        void UpdatePaymentData(PaymentModel parameters);
    }
}