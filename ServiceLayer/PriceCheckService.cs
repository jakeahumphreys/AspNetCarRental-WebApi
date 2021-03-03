using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using HtmlAgilityPack;

namespace EIRLSS_Data_API.ServiceLayer
{
    public class PriceCheckService
    {
        public double GetAffordPrice(string requestVehicleType, DateTime startDate, DateTime endDate)
        {
            var affordVehicleType = 0;

            switch (requestVehicleType)
            {
                case "Small Town Car":
                    affordVehicleType = 1;
                    break;
                case "Small Family Hatchback":
                    affordVehicleType = 2;
                    break;
                case "Large Family Saloon":
                    affordVehicleType = 3;
                    break;
                case "Large Family Estate":
                    affordVehicleType = 3;
                    break;
                case "Medium Van":
                    affordVehicleType = 5;
                    break;
            }

            var startDateString = startDate.ToString("dd-MM-yyyy");
            var endDateString = endDate.ToString("dd-MM-yyyy");
            var affordUrl =
                $"https://www.affordrentacar.co.uk/booking/vehicle?SearchForm[sub_category]={affordVehicleType}&SearchForm[date_from]={startDateString}&SearchForm[date_from_time]={startDate.ToShortTimeString()}&SearchForm[date_return]={endDateString}&SearchForm[date_return_time]={endDate.ToShortTimeString()}";
            var web = new HtmlWeb();
            var doc = web.Load(affordUrl);
            var nodes = doc.DocumentNode.SelectNodes("//span[@class='recommend-price']");
            var nodeContent = Regex.Replace(nodes[0].InnerHtml, @"( |\t|\r|\n)+", string.Empty);
            var totalCost = double.Parse(nodeContent, CultureInfo.InvariantCulture);
            

            return Math.Round(totalCost, 2);
        }
    }
}