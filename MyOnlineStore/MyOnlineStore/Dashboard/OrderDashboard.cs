using Microcharts;
using MyOnlineStore.Dashboard.DashBoardModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Dashboard
{
    public class OrderDashboard : IDashboard
    {
        public Chart Dashboard 
        {
            get  ; set  ;
        }
        public List<Entry> Entries
        { 
            get ; set ; 
        }
        public string OptionSelected 
        { 
            get ; 
            set ;
        }
        public string Title { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime From { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime To { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public OrderDashboard()
        {

        }
    }
}
