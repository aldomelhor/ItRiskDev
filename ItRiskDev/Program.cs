
using ItRiskDev.Implementacao;
using ItRiskDev.Interface;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace ItRiskDev
{
    class Program
    {
        static void Main(string[] args)
        {
            string iniValor = @"12/11/2020 
                                4 
                                2000000 Private 12/29/2025 
                                400000 Public 07/01/2020 
                                5000000 Public 01/02/2024 
                                3000000 Public 10/26/2023";

            List<ITrade> portfolio = new List<ITrade>();
            var conValor = iniValor.Split(Environment.NewLine);
            DateTime referenceDate = DateTime.ParseExact(conValor[0].ToString().Trim(), "MM/dd/yyyy", new CultureInfo("en-US"), DateTimeStyles.None);  
            int numberOfTrades = int.Parse(conValor[1].ToString()) ;

            for (int i = 2; i < conValor.Length; i++)
            {
                var lnPorti = conValor[i].Trim().Split();
                Trade trade = new Trade();
                trade.Value = double.Parse( lnPorti[0].ToString());
                trade.ClientSector = lnPorti[1].ToString();
                trade.ReferenceDate = referenceDate;
                trade.NextPaymentDate = DateTime.ParseExact(lnPorti[2].ToString(), "MM/dd/yyyy", new CultureInfo("en-US"), DateTimeStyles.None);

                portfolio.Add(trade);
            }

            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.Register<IRisk, Risk>(Lifestyle.Transient);
            container.Verify();

            using (var scope = new Scope(container))
            {
                var riskNegocio = scope.GetInstance<IRisk>();
                foreach (var item in portfolio)
                {
                    var retorno = riskNegocio.CalculateRisk(item);
                    Console.WriteLine($"Portfolio {item.Value} {item.ClientSector} {item.NextPaymentDate.ToShortDateString()}. risk: {retorno}");
                }
                
            }
            Thread.Sleep(30000);
        }

    }
}
