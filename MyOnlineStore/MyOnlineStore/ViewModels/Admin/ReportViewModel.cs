using Microcharts;
using MyOnlineStore.Dashboard.DashBoardModel;
using MyOnlineStore.DataStore;
using MyOnlineStore.Interfaces.DataStore;
using MyOnlineStore.Interfaces.IViewModel;
using MyOnlineStore.Shared.Models.Purchases;
using MyOnlineStore.ViewModels.Base;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Entry = Microcharts.Entry;
using System.Linq.Expressions;
using System.Linq;
using Xamarin.Essentials;
using System.Globalization;
using MyOnlineStore.Dashboard.Filters;
using MyOnlineStore.Dashboard.Services;
using MyOnlineStore.Utilities;

namespace MyOnlineStore.ViewModels.Admin
{

    /*
     
         Todo:
        
         Validar From Date To Date from no puede ser mayor que to

        
         
         */
    public class ReportViewModel : BaseViewModel, IReportViewModel
    {
        IProductItemDataStore _productItemData;
        IOrdersDataStore _ordersDataStore;

      

        const double widthrequest = 10 * 30;
        private double request;
        public double WidthRequest
        {
            get { return request; }
            set
            {

                request = value;
                RaisePropertyChanged(() => WidthRequest);
            }
        }

        #region Orders Data chart  Region

        private double orderwidthchart;

        public double OrderWidthChart
        {
            get { return orderwidthchart; }
            set
            {
                orderwidthchart = value;
                RaisePropertyChanged(() => OrderWidthChart);
            }
        }


        private string ordertitle;

        public string OrderChartTitle
        {
            get { return ordertitle; }
            set
            {
                ordertitle = value;
                RaisePropertyChanged(() => OrderChartTitle);
            }
        }

        ObservableCollection<Order> orders;
        public ObservableCollection<Order> Orders
        {
            get => orders;

            set
            {
                orders = value;
                RaisePropertyChanged(() => Orders);
            }

        }

       private Chart orderchart;
        public Chart OrderChart
        {

            get => orderchart;

            set
            {
                orderchart = value;
                RaisePropertyChanged(() => OrderChart);
            }

        }

        int totalOrderCompleted;
        public int TotalOrdersCompleted
        {

            get => totalOrderCompleted;

            set
            {
                totalOrderCompleted = value;
                RaisePropertyChanged(() => TotalOrdersCompleted);
            }

        }

        int totalNotCompleted;
        public int TotalNotCompleted
        {

            get => totalNotCompleted;

            set
            {

                totalNotCompleted = value;
                RaisePropertyChanged(() => TotalNotCompleted);
            }
        }

        private double totalearn;

        public double TotalEarn
        {
            get { return totalearn; }
            set
            {
                totalearn = value;
                RaisePropertyChanged(() => TotalEarn);

            }
        }



        private DateTime orderdatefrom;

        public DateTime OrderDateFrom
        {
            get { return orderdatefrom; }
            set { orderdatefrom = value; }
        }

        private DateTime orderdateto;

        public DateTime OrderDateTo
        {
            get { return orderdateto; }
            set { orderdateto = value; }
        }

        private int orderscount;

        public int OrdersCount
        {
            get { return orderscount; }
            set 
            { 
                orderscount = value;
                RaisePropertyChanged(() => OrdersCount);
            }
        }


        private string ordersoptionselected;

        public string OrdersOptionSelected
        {
            get { return ordersoptionselected; }
            set
            {
                ordersoptionselected = value;

                RaisePropertyChanged(() => OrdersOptionSelected);

              if(OrdersOptionSelected != string.Empty)
                {

                if (OrdersOptionSelected == "Orders Complete")
                {
                        if (OrderUnitSelected == "Days")
                        {

                            OrderChartChanged(GetOrdersCompleted(OrderServices.GetRecords(OrderDateFrom, OrderDateTo, Orders)));
                            TotalOrdersCompleted = Orders.Count(c => (c.OrderDate >= OrderDateFrom || c.OrderDate <= OrderDateFrom) && c.IsCompleted == true);
                            TotalNotCompleted = Orders.Count(c => (c.OrderDate >= OrderDateFrom || c.OrderDate <= OrderDateFrom) && c.IsCompleted == false);
                            OrdersCount = Orders.Where(x => (x.OrderDate >= OrderDateFrom || x.OrderDate <= OrderDateFrom)).Count();
                            OrderWidthChart = widthrequest;
                            OrderChartTitle = "Orders Complete";
                            OrdersOptionSelected = string.Empty;
                            OrderUnitSelected = string.Empty;

                        }
                        if (OrderUnitSelected == "Weeks")
                        {

                            var weekorders = OrderServices.GetOrdersFromDateToDate(OrderDateFrom, OrderDateTo, Orders);


                            OrderChartChanged(GetOrdersCompletedByUnitTime(UnitTimeFilter.GetRecordByWeek(OrderDateFrom, OrderDateTo, weekorders, true)));

                            OrderChartTitle = "Orders Complete By Week";
                            TotalOrdersCompleted = weekorders.Count(c => (c.OrderDate >= OrderDateFrom || c.OrderDate <= OrderDateFrom) && c.IsCompleted == true);
                            TotalNotCompleted = weekorders.Count(c => (c.OrderDate >= OrderDateFrom || c.OrderDate <= OrderDateFrom) && c.IsCompleted == false);
                            OrdersCount = Orders.Where(x => (x.OrderDate >= OrderDateFrom || x.OrderDate <= OrderDateFrom)).Count();
                            OrderWidthChart = widthrequest;
                            OrdersOptionSelected = string.Empty;
                            OrderUnitSelected = string.Empty;

                        }
                        if (OrderUnitSelected == "Months")
                        {
                            var orderbymonth = OrderServices.GetOrdersFromDateToDate(OrderDateFrom, OrderDateTo, Orders);

                            OrderChartChanged(GetOrdersCompletedByUnitTime(UnitTimeFilter.GetRecordByMonths(OrderDateFrom, OrderDateTo, orderbymonth, true)));
                            OrderChartTitle = "Orders Complete By Months";
                            TotalOrdersCompleted = orderbymonth.Count(c => (c.OrderDate >= OrderDateFrom || c.OrderDate <= OrderDateFrom) && c.IsCompleted == true);
                            TotalNotCompleted = orderbymonth.Count(c => (c.OrderDate >= OrderDateFrom || c.OrderDate <= OrderDateFrom) && c.IsCompleted == false);
                            OrderWidthChart = widthrequest;
                            OrdersOptionSelected = string.Empty;
                            OrderUnitSelected = string.Empty;
                        }

                        if (OrderUnitSelected == "Years")
                        {
                            var yearorders = OrderServices.GetOrdersFromDateToDate(OrderDateFrom, OrderDateTo, Orders);

                            OrderChartChanged(GetOrdersCompletedByUnitTime(UnitTimeFilter.GetRecordByYear(OrderDateFrom, OrderDateTo, yearorders, true)));

                            OrderWidthChart = widthrequest;
                            OrderChartTitle = "Orders Complete By Year";
                            TotalOrdersCompleted = Orders.Count(c => (c.OrderDate >= OrderDateFrom || c.OrderDate <= OrderDateFrom) && c.IsCompleted == true);
                            TotalNotCompleted = Orders.Count(c => (c.OrderDate >= OrderDateFrom || c.OrderDate <= OrderDateFrom) && c.IsCompleted == false);
                            OrdersOptionSelected = string.Empty;
                            OrderUnitSelected = string.Empty;
                        }




                }

                    if (OrdersOptionSelected == "Orders Not Completed")
                    {

                        if (OrderUnitSelected == "Days")
                    {

                        OrderChartChanged(GetOrdersNotCompleted(OrderServices.GetRecordsNotCompleted(OrderDateFrom, OrderDateTo, Orders)));
                        TotalOrdersCompleted = Orders.Count(c => (c.OrderDate >= OrderDateFrom || c.OrderDate <= OrderDateFrom) && c.IsCompleted == true);
                        TotalNotCompleted = Orders.Count(c => (c.OrderDate >= OrderDateFrom || c.OrderDate <= OrderDateFrom) && c.IsCompleted == false);
                        OrdersCount = Orders.Where(x => (x.OrderDate >= OrderDateFrom || x.OrderDate <= OrderDateFrom)).Count();
                        OrderWidthChart = widthrequest;
                        OrderChartTitle = "Orders Complete";
                        OrdersOptionSelected = string.Empty;
                        OrderUnitSelected = string.Empty;
                    }
                    if (OrderUnitSelected == "Weeks")
                    {

                       
                        OrderChartChanged(GetOrdersNotCompletedByUnitTime(UnitTimeFilter.GetRecordByWeek(OrderDateFrom, OrderDateTo, Orders, false), "Weeks"));
                        TotalOrdersCompleted = Orders.Count(c => (c.OrderDate >= OrderDateFrom || c.OrderDate <= OrderDateFrom) && c.IsCompleted == true);
                        TotalNotCompleted = Orders.Count(c => (c.OrderDate >= OrderDateFrom || c.OrderDate <= OrderDateFrom) && c.IsCompleted == false);

                        OrdersOptionSelected = string.Empty;
                        OrderUnitSelected = string.Empty;
                        OrderWidthChart = widthrequest;
                        OrderChartTitle = "Order Not completed";
                    }
                        if (OrderUnitSelected == "Months")
                        {
                          
                            OrderChartChanged(GetOrdersNotCompletedByUnitTime(UnitTimeFilter.GetRecordByMonths(OrderDateFrom, OrderDateTo, Orders, false), "Months"));
                            TotalOrdersCompleted = Orders.Count(c => (c.OrderDate >= OrderDateFrom || c.OrderDate <= OrderDateFrom) && c.IsCompleted == true);
                            TotalNotCompleted = Orders.Count(c => (c.OrderDate >= OrderDateFrom || c.OrderDate <= OrderDateFrom) && c.IsCompleted == false);

                            OrdersOptionSelected = string.Empty;
                            OrderUnitSelected = string.Empty;
                            OrderWidthChart = widthrequest;
                            OrderChartTitle = "Order Not completed";
                        }
                        if (OrderUnitSelected == "Years")
                        {
                           
                            OrderChartChanged(GetOrdersNotCompletedByUnitTime(UnitTimeFilter.GetRecordByYear(OrderDateFrom, OrderDateTo, Orders, false), "Years"));
                            OrdersOptionSelected = string.Empty;
                            OrderUnitSelected = string.Empty;
                            OrderWidthChart = widthrequest;
                            OrderChartTitle = "Order Not completed";
                        }


                    }
                    if (OrdersOptionSelected == "Show All Orders")
                    {
                       
                        OrderChartChanged(GetOrders(Orders.OrderBy(o=>o.OrderDate)));
                        OrdersOptionSelected = string.Empty;
                        OrderWidthChart = widthrequest;
                        OrderChartTitle = "All Orders";
                    }



                }

              

            }
        }

        private string orderunitselected;

        public string OrderUnitSelected
        {
            get { return orderunitselected; }
            set
            {
                orderunitselected = value;
                RaisePropertyChanged(() => OrderUnitSelected);
            }
        }


        public static int NumberOfWeeks(DateTime dateFrom, DateTime dateTo)
        {
            TimeSpan Span = dateTo.Subtract(dateFrom);

            if (Span.Days <= 7)
            {
                if (dateFrom.DayOfWeek > dateTo.DayOfWeek)
                {
                    return 2;
                }

                return 1;
            }

            int Days = Span.Days - 7 + (int)dateFrom.DayOfWeek;
            int WeekCount = 1;
            int DayCount = 0;

            for (WeekCount = 1; DayCount < Days; WeekCount++)
            {
                DayCount += 7;
            }

            return WeekCount;
        }



        #endregion

        #region Inventory Chart and Properties Region

        ObservableCollection<ProductItem> inventoryitems;
        public ObservableCollection<ProductItem> InventoryItems
        {

            get => inventoryitems;

            set
            {
                inventoryitems = value;
                RaisePropertyChanged(() => InventoryItems);
            }

        }

     private   Chart inventorychart;
        public Chart InventoryChart
        {

            get
            {
                return inventorychart;
            }

            set
            {
                inventorychart = value;
                RaisePropertyChanged(() => InventoryChart);
            }

        }

        private Chart donutinventorychart;

        public Chart DonutInvnentoryChart
        {
            get { return donutinventorychart; }
            set
            {
                donutinventorychart = value;
                RaisePropertyChanged(() => DonutInvnentoryChart);
            }
        }

        private string invnetorytitle;

        public string InventoryTitle
        {
            get { return invnetorytitle; }
            set
            {
                invnetorytitle = value;

                RaisePropertyChanged(() => InventoryTitle);

            }
        }


        string inventoryselectedoption;
        public string InventorySelecteOption
        {

            get => inventoryselectedoption;

            set
            {
                inventoryselectedoption = value;

                RaisePropertyChanged(() => InventorySelecteOption);
                OptionSelected(InventorySelecteOption,TypeFilterSelected);


            }

        }

       public ObservableCollection<string> Types { get; set; }

        private string typefilterselected;

        public string TypeFilterSelected
        {
            get { return typefilterselected; }
            set 
            { 
                typefilterselected = value;

                RaisePropertyChanged(() => TypeFilterSelected);

                OptionSelectedFilterbyType(InventorySelecteOption,TypeFilterSelected);


            }
        }



        double inventoryvalue;
        public double InventoryValue
        {
            get => inventoryvalue;

            set
            {
                inventoryvalue = value;
                RaisePropertyChanged(() => InventoryValue);
            }
        }

        private double itemsvalue;

        private double inventorychartwidthrequest;

        public double InventoryChartWidthRequest
        {
            get { return inventorychartwidthrequest; }
            set
            {
                inventorychartwidthrequest = value;
                RaisePropertyChanged(() => InventoryChartWidthRequest);
            }
        }


        public double Value
        {
            get { return itemsvalue; }
            set
            {
                itemsvalue = value;
                RaisePropertyChanged(() => Value);


            }
        }

        private int totalitems;
        public int TotalItems
        {
            get { return totalitems; }
            set
            {
                totalitems = value;
                RaisePropertyChanged(() => TotalItems);

            }
        }

        private int qty;

        public int Quantity
        {
            get { return qty; }
            set
            {
                qty = value;
                RaisePropertyChanged(() => Quantity);

            }
        }




        #endregion

        #region Goal Charts Region

        private string goalunittime;

        public string GoalUnitTime
        {
            get { return goalunittime; }
            set
            {
                goalunittime = value;
                RaisePropertyChanged(() => GoalUnitTime);
            }
        }

        private Chart goalprogresschart;

        public Chart GoalProgressChart
        {
            get { return goalprogresschart; }
            set
            {
                goalprogresschart = value;
                RaisePropertyChanged(() => GoalProgressChart);
            }
        }

        private string goalcharttitle;

        public string GoalChartTitle
        {
            get { return goalcharttitle; }
            set
            {
                goalcharttitle = value;
                RaisePropertyChanged(() => GoalChartTitle);
            }
        }

        private string goaloptionselected;
        public string GoalOptionSelected
        {
            get { return goaloptionselected; }
            set
            {
                goaloptionselected = value;
                RaisePropertyChanged(() => GoalOptionSelected);

                if (goaloptionselected == "Show Progess")
                {
                    if(GoalUnitTime != null)
                    {

                    if (GoalUnitTime == "Days")
                    {

                            TotalToReached = Calculator.GetTotalToReached(50, UnitTimeFilter.GetDaysFromTo(FromDate, ToDate));
                            var record = new List<Record>(OrderServices.GetRecords(FromDate, ToDate,Orders));


                           

                            double val;
                            GoalChartCompared(GoalServices.GetgoalpastDays(record, out val));
                          
                            ValueReached = val;
                            PercentCompleted = Calculator.GetPercent(ValueReached, TotalToReached);


                            GoalChartTitle = "Pogress Past Days";
                            GoalOptionSelected = string.Empty;
                            WidthRequest = widthrequest;
                        }

                    if(GoalUnitTime == "Weeks")
                    {
                        var record = new List<Record>(OrderServices.GetRecords(FromDate, ToDate, Orders));
                            TotalToReached = Calculator.GetTotalToReached(50, UnitTimeFilter.GetDaysFromTo(FromDate, ToDate));

                            double val = 0;
                        GoalChartChangedWeek(GoalServices.Getgoalweek(UnitTimeFilter.GetRecordByWeek(FromDate, ToDate, _ordersDataStore.GetAll(),true),out val));

                           ValueReached = val;
                            PercentCompleted = Calculator.GetPercent(ValueReached, TotalToReached);


                            GoalChartTitle = "Pogress Past Weeks";
                        GoalOptionSelected = string.Empty;
                        GoalUnitTime = string.Empty;
                        WidthRequest = widthrequest;
                    }

                    if(GoalUnitTime == "Months")
                    {
                            var record = new List<Record>(OrderServices.GetRecords(FromDate, ToDate, Orders));

                            //Crear conteo de dias de un mes al otro

                            TotalToReached = Calculator.GetTotalToReached(50, UnitTimeFilter.GetDaysFromTo(FromDate, ToDate));

                            double val = 0;
                            GoalChartChangedWeek(GoalServices.Getgoalbymonths(UnitTimeFilter.GetRecordByMonths(FromDate, ToDate, _ordersDataStore.GetAll(), true), out val));

                            ValueReached = val;
                            PercentCompleted = Calculator.GetPercent(ValueReached, TotalToReached);


                            GoalChartTitle = "Pogress Past Months";
                            GoalOptionSelected = string.Empty;
                            GoalUnitTime = string.Empty;
                            WidthRequest = widthrequest;
                        }
                    if(GoalUnitTime == "Years")
                    {
                        var record = new List<Record>(OrderServices.GetRecords(FromDate, ToDate, Orders));
                            TotalToReached = Calculator.GetTotalToReached(50, UnitTimeFilter.GetDaysFromTo(FromDate, ToDate));

                            double val = 0;
                            GoalChartChangedWeek(GoalServices.Getgoalbyyear(UnitTimeFilter.GetRecordByYear(FromDate, ToDate, _ordersDataStore.GetAll(), true),out val));

                            ValueReached = val;
                            PercentCompleted = Calculator.GetPercent(ValueReached, TotalToReached);


                            GoalChartTitle = "Pogress Past Years";
                            GoalOptionSelected = string.Empty;
                            GoalUnitTime = string.Empty;
                            WidthRequest = widthrequest;
                    }
                    }
                    else
                    {
                        var record = new List<Record>(_ordersDataStore.GetRecords(FromDate.AddDays(-6), ToDate));


                        TotalToReached = Calculator.GetTotalToReached(50, UnitTimeFilter.GetDaysFromTo(FromDate.AddDays(-6), ToDate));
                        
                        double val;
                        
                        GoalChartCompared(GoalServices.GetgoalpastDays(record, out val));

                        ValueReached = val;
                       
                        PercentCompleted = Calculator.GetPercent(ValueReached, TotalToReached);

                        GoalChartTitle = "Pogress Past Days";
                        GoalOptionSelected = string.Empty;
                        WidthRequest = 10 * 100;
                    }


                }

            }
        }

        private double percentcompleted;

        public double PercentCompleted
        {
            get { return percentcompleted; }
            set            
            { 
                percentcompleted = value;
                RaisePropertyChanged(() => PercentCompleted);
            
            }
        }

       

        private DateTime fromdate;

        public DateTime FromDate
        {
            get { return fromdate; }
            set
            {
                fromdate = value;
                RaisePropertyChanged(() => FromDate);

            }
        }

        private DateTime todate;

        public DateTime ToDate
        {
            get { return todate; }
            set
            {
                todate = value;
                RaisePropertyChanged(() => ToDate);
            }
        }

        private double valuereached;

        public double ValueReached
        {
            get { return valuereached; }
            set
            {
                valuereached = value;
                RaisePropertyChanged(() => ValueReached);
            }
        }

        private double toreached;

        public double ToReached
        {
            get { return toreached; }
            set
            {
                toreached = value;
                RaisePropertyChanged(() => ToReached);
            }
        }

        private double totaltoreached;

        public double TotalToReached
        {
            get { return totaltoreached; }
            set
            {
                totaltoreached = value;

                RaisePropertyChanged(() => TotalToReached);

            }
        }


        #endregion

        public ReportViewModel(IProductItemDataStore productItemDataStore, IOrdersDataStore ordersDataStore)
        {
            _productItemData = productItemDataStore;

            var inventoryInfo = _productItemData.GetAll();

           // InventoryItems = new ObservableCollection<ProductItem>(productItemDataStore.GetAll());

            _ordersDataStore = ordersDataStore;

            //Escoje metas a presentar de una fechas a otra
            FromDate = DateTime.Today;
            ToDate = DateTime.Today;

            OrderDateFrom = DateTime.Today;
            OrderDateTo = DateTime.Today;


            Orders = new ObservableCollection<Order>(_ordersDataStore.GetAll());
           


            InventorySelecteOption = "Low Quantity";
            GoalOptionSelected = "Show Progress";
           

           

            //Presentar meta ha alcanzar
            ToReached = 50;

           

        }

       

        #region Method utilities 
        List<Entry> GetComplete(IEnumerable<Record> records)
        {
            List<Record> recordData = new List<Record>(records);
            List<Entry> entries = new List<Entry>();

            if (ValueReached != 0)
            {
                ValueReached = 0;
            }


            foreach (var item in recordData)
            {



                if (item.RecordValue >= 50)
                {
                    ValueReached += item.RecordValue;
                    entries.Add(new Entry((float)item.RecordValue)
                    {
                        Label = item.RecordDate.ToString("MM / dd / yyyy"),
                        ValueLabel = item.RecordValue.ToString(),
                        Color = SKColor.Parse("#ffcc66"),
                        TextColor = SKColor.Parse("#ffcc66"),
                    });
                }
            }

            return entries;

        }
        List<Entry> GetOrdersCompletedByUnitTime(IEnumerable<Record> records)
        {
            List<Record> completed = new List<Record>(records);
            List<Entry> entries = new List<Entry>();

            foreach (var item in completed)
            {

                entries.Add(new Entry((float)item.RecordValue)
                {
                    ValueLabel = "$" + item.RecordValue,
                    Label = item.RecordTitle,
                    Color = SKColor.Parse("#00DC7D"),
                    TextColor = SKColor.Parse("#00DC7D"),




                });

            }

            return entries;
        }
        List<Entry> GetOrdersCompleted(IEnumerable<Record> orders)
        {
            List<Record> completed = new List<Record>(orders);
            List<Entry> entries = new List<Entry>();

            foreach (var item in completed)
            {

                if (item.RecordValue > 50)
                {

                    entries.Add(new Entry((float)item.RecordValue)
                    {
                        ValueLabel = "$" + item.RecordValue.ToString(),
                        Label = item.RecordDate.DayOfWeek.ToString() + item.RecordDate.Day.ToString(),
                        Color = SKColor.Parse("#00DC7D"),
                        TextColor = SKColor.Parse("#00DC7D"),



                    });
                }
                else
                {

                    entries.Add(new Entry((float)item.RecordValue)
                    {
                        ValueLabel = "$" + item.RecordValue.ToString(),
                        Label = item.RecordDate.DayOfWeek.ToString()+item.RecordDate.Day.ToString(),
                        Color = SKColor.Parse("#F85C50"),
                        TextColor = SKColor.Parse("#F85C50"),



                    });
                    
                }
            }

            return entries;
        }
        List<Entry> GetOrdersNotCompleted(IEnumerable<Record> orders)
        {
            List<Record> notcompleted = new List<Record>(orders);
            List<Entry> entries = new List<Entry>();

            foreach (var item in notcompleted)
            {

                if (item.RecordValue > 50)
                {

                    entries.Add(new Entry((float)item.RecordValue)
                    {
                        ValueLabel = "$" + item.RecordValue.ToString(),
                        Label = item.RecordDate.DayOfWeek.ToString() + item.RecordDate.Day.ToString(),
                        Color = SKColor.Parse("#00DC7D"),
                        TextColor = SKColor.Parse("#00DC7D"),



                    });
                }
                else
                {

                    entries.Add(new Entry((float)item.RecordValue)
                    {
                        ValueLabel = "$" + item.RecordValue.ToString(),
                        Label = item.RecordDate.DayOfWeek.ToString() + item.RecordDate.Day.ToString(),
                        Color = SKColor.Parse("#F85C50"),
                        TextColor = SKColor.Parse("#F85C50"),



                    });

                }
            }

            return entries;
        }
        List<Entry> GetOrdersNotCompletedByUnitTime(IEnumerable<Record> orders,string unitselected)
        {
            List<Record> completed = new List<Record>(orders);
            List<Entry> entries = new List<Entry>();

            string show=string.Empty;
            if(unitselected == "Years")
            {
                show = "yyyy";
            }
            if (unitselected == "Months")
            {
                show = "MMMM";
            }

            foreach (var item in completed)
            {
                entries.Add(new Entry((float)item.RecordValue)
                {
                    ValueLabel = "$" + item.RecordValue,
                    Label=item.RecordDate.ToString(show),                    
                    Color = SKColor.Parse("#F6522E"),
                    TextColor = SKColor.Parse("#F6522E"),
                });
                
            }

            return entries;
        }

        List<Entry> GetOrders(IEnumerable<Order> orders)
        {
            List<Order> recordData = new List<Order>(orders);
            List<Entry> entries = new List<Entry>();

            foreach (var item in recordData)
            {

                entries.Add(new Entry((float)item.OrderItems.Sum(i => i.Price))
                {
                    Label = item.OrderDate.ToString("MM / dd / yyyy"),
                    ValueLabel = item.OrderItems.Sum(i => i.Price).ToString(),

                    Color = SKColor.Parse("#FF6B00"),
                    TextColor = SKColor.Parse("#FF6B00"),



                });
            }

            return entries;

        }

       
      

        List<Entry> TotalProductWithQty(IEnumerable<ProductItem> items)
        {
            var toShow = new ObservableCollection<ProductItem>(items);
            List<Entry> entries = new List<Entry>();

            foreach (var item in toShow)
            {
                if (item.Quantity <= 5)
                {
                    entries.Add(new Entry(item.Quantity)
                    {
                        Label = item.Name,
                        ValueLabel = item.Quantity.ToString(),
                        Color = SKColor.Parse("#ff0000"),
                        TextColor = SKColor.Parse("#ff0000"),
                    });
                }
                else
                {

                    entries.Add(new Entry(item.Quantity)
                    {
                        Label = item.Name,
                        ValueLabel = item.Quantity.ToString(),
                        Color = SKColor.Parse("#ff8533"),
                        TextColor = SKColor.Parse("#C13100"),
                    });
                }
            }
            return entries;
        }

        List<Entry> TotalEarnBySaleEntries(IEnumerable<ProductItem> items)
        {
            var toShow = new ObservableCollection<ProductItem>(items);

            List<Entry> entries = new List<Entry>();
            float earn = 0.00f;

            foreach (var item in toShow)
            {
                earn = (float)item.Price * item.TotalPurchased;

                entries.Add(new Entry(earn)
                {

                    Label = item.Name + earn.ToString("$0.##"),
                    ValueLabel = item.TotalPurchased.ToString(),
                    Color = SKColor.Parse("#ff8533"),
                    TextColor = SKColor.Parse("#C13100"),


                }) ;

            }

            return entries;
        }
        int GetDays(DateTime From, DateTime To)
        {

            int days = 0;
            if (From.Day < To.Day)
            {
                days = To.Day - From.Day;
            }
            else
            {
                days = From.Day - To.Day;
            }

            return days;

        }
        void GoalChartCompared(List<Entry> entries)
        {
            GoalProgressChart = new BarChart()
            {
                Entries = entries,
                LabelTextSize = 30f,



            };
        }

        List<Entry> ProductEarning(IEnumerable<ProductItem> items)
        {
            var products = new List<ProductItem>(items);
            List<Entry> entries = new List<Entry>();

            foreach (var item in products)
            {

                float value = (float)item.Price * item.Quantity;
                entries.Add(new Entry(value)
                {
                    Label = item.Name,
                    ValueLabel = "$" + value.ToString(),
                    Color = SKColor.Parse("#ff8533"),
                    TextColor = SKColor.Parse("#ff8533"),

                });
            }

            return entries;
        }

        void GoalCharChanged(double progress, double reached)
        {
            GoalProgressChart = new DonutChart()
            {
                Entries = new[]
                {
                    new Entry((float)progress)
                    {
                        Color = SKColor.Parse("#ffcc66"),
                        TextColor = SKColor.Parse("#ffcc66"),
                        Label = "Completed",
                        ValueLabel = progress.ToString()+"%"
                    },
                     new Entry((float)reached)
                    {
                        Color = SKColor.Parse("#ff8533"),
                        TextColor = SKColor.Parse("#ff8533"),
                        Label = "Goal",
                        ValueLabel = reached.ToString()+"%"
                    }
                },
                LabelTextSize = 38,
                HoleRadius = 0.10f,
            };
        }

        void GoalChartChangedWeek(List<Entry> entries)
        {
            GoalProgressChart = new BarChart()
            {
                Entries = entries,
                 LabelTextSize=38,
                  
                 
                   
            
            };

        }
        void InventoryChartChanged(List<Entry> entries)
        {
            InventoryChart = new BarChart()
            {
                Entries = entries,
                LabelTextSize = 30f,


            };
        }
        void OrderChartChanged(List<Entry> data)
        {

            OrderChart = new BarChart()
            {
                Entries = data,
                LabelTextSize = 30f,
               
            };
        }



        double GetProgress(DateTime date)
        {
            var totalearnday = _ordersDataStore.GetValueOfItemsPurchaseWithOrder(date);



            double progress = (totalearnday / 50) * 100;

            return progress;

        }


        int GetNotCompleted(IEnumerable<Order> _orders)
        {

            var getter = new ObservableCollection<Order>(_orders);
            int count = 0;
            foreach (var item in getter)
            {
                if (item.IsCompleted == false)
                {
                    count += 1;
                }
            }

            return count;

        }
        float OrdersCompletedPercentCalculation(int totalOrders, int totalCompleted)
        {
            float percentCompleted;
            if (totalCompleted != 0 && totalOrders != 0)
            {

                percentCompleted = ((float)totalCompleted / totalOrders);
            }
            else
            {
                percentCompleted = 0.00f;
            }

            return percentCompleted;
        }
        float OrderNotCompletedPercentCalculation(int totalOrders, int notCompleted)
        {
            float percentCompleted;
            if (notCompleted != 0 && totalOrders != 0)
            {

                percentCompleted = ((float)notCompleted / totalOrders);
            }
            else
            {
                percentCompleted = 0.00f;
            }

            return percentCompleted;
        }

        List<Record> GetRecordByWeek(IEnumerable<Order> orders)
        {
            List<Record> records = new List<Record>();
            List<Order> orderbyweek = new List<Order>(orders);

            for (var day = OrderDateFrom.Date; day.Date <= OrderDateTo.Date; day = day.AddDays(1))
            {
                Record record = new Record();

                record.RecordValue = orderbyweek.Where(c => c.OrderDate == day && c.IsCompleted == true).Sum(i => i.OrderItems.Sum(s => s.Price));

                record.RecordDate = day;

                records.Add(record);



            }

            double totalweek = 0;

            List<Record> recordsweeks = new List<Record>();

            for (var day = OrderDateFrom.Date; day.Date <= OrderDateTo.Date; day = day.AddDays(1))
            {


                if (day.DayOfWeek > DayOfWeek.Sunday)
                {
                    totalweek += records.Where(d => d.RecordDate == day).Sum(i => i.RecordValue);



                }
                if (day.DayOfWeek == DayOfWeek.Sunday)
                {
                    recordsweeks.Add(new Record()
                    {
                        RecordValue = totalweek,
                        RecordDate = day

                    });
                    totalweek = 0;
                }
                if (day == OrderDateTo.Date)
                {
                    recordsweeks.Add(new Record()
                    {
                        RecordValue = totalweek,
                        RecordDate = day

                    });
                    totalweek = 0;
                }





            }

            return recordsweeks;
        }


        List<Record> GetRecordByYear(IEnumerable<Order> orders)
        {
            List<Record> records = new List<Record>();
            List<Order> orderbyweek = new List<Order>(orders);

            for (var day = OrderDateFrom.Date; day.Date <= OrderDateTo.Date; day = day.AddDays(1))
            {
                Record record = new Record();

                record.RecordValue = orderbyweek.Where(c => c.OrderDate == day && c.IsCompleted == true).Sum(i => i.OrderItems.Sum(s => s.Price));

                record.RecordDate = day;

                records.Add(record);



            }

            double totalweek = 0;

            List<Record> recordyears = new List<Record>();

            for (var day = OrderDateFrom.Date; day.Date.Year <= OrderDateTo.Date.Year; day = day.AddYears(1))
            {


                totalweek += records.Where(d => d.RecordDate.Year == day.Year).Sum(i => i.RecordValue);

                recordyears.Add(new Record()
                {
                    RecordValue = totalweek,
                    RecordTitle = day.Year.ToString()

                });
                totalweek = 0;

            }

            return recordyears;
        }

        List<Record> GetRecordByMonths(IEnumerable<Order> orders)
        {
            List<Record> records = new List<Record>();
            List<Order> orderbyweek = new List<Order>(orders);



            for (var day = OrderDateFrom.Date; day.Date <= OrderDateTo.Date; day = day.AddDays(1))
            {
                Record record = new Record();

                record.RecordValue = orderbyweek.Where(c => c.OrderDate == day && c.IsCompleted == true).Sum(i => i.OrderItems.Sum(s => s.Price));

                record.RecordDate = day;

                records.Add(record);



            }

            double totalweek = 0;

            List<Record> recordyears = new List<Record>();

            for (var day = OrderDateFrom.Date; day.Date.Month <= OrderDateTo.Date.Month; day = day.AddMonths(1))
            {


                totalweek += records.Where(d => d.RecordDate.Month == day.Month).Sum(i => i.RecordValue);

                recordyears.Add(new Record()
                {
                    RecordValue = totalweek,
                    RecordTitle = day.Month.ToString()

                });
                totalweek = 0;

            }

            return recordyears;
        }

        void OptionSelected(string selected,string type)
        {
            if (selected == "Show Top Sales")
            {
               if(type != null)
                {
                    var top5 = _productItemData.GetBestSaleInventoryOf();


                    InventoryItems = new ObservableCollection<ProductItem>(top5);
                    
                    InventoryChartWidthRequest = InventoryItems.Count * 30;
                    Value = _productItemData.GetValueOfProductsBySales(top5.Where(s => s.Type == type));

                    List<Entry> totalEntries = new List<Entry>(TotalEarnBySaleEntries(InventoryItems.Where(s=>s.Type == type)));
                    InventoryChartChanged(totalEntries);

                    InventoryTitle = selected;

                    TotalItems = InventoryItems.Where(s => s.Type == type).Count();
                    InventorySelecteOption = string.Empty;
                    TypeFilterSelected = string.Empty;
                }
               else
                {

                var top5 = _productItemData.GetBestSaleInventoryOf();


                InventoryItems = new ObservableCollection<ProductItem>(top5);
                TotalItems = InventoryItems.Count;
                InventoryChartWidthRequest = TotalItems * 30;
                Value = _productItemData.GetValueOfProductsBySales(top5);

                List<Entry> totalEntries = new List<Entry>(TotalEarnBySaleEntries(InventoryItems));
                InventoryChartChanged(totalEntries);

                InventoryTitle = selected;


                InventorySelecteOption = string.Empty;
                }

                
               

            }
            if (selected == "Low Quantity")
            {
                if (type != null)
                {
                    InventoryTitle = selected;
                    var lowqty = _productItemData.GetProductItemsWithQty(5);
                    InventoryItems = new ObservableCollection<ProductItem>(lowqty);

                    InventoryChartChanged(TotalProductWithQty(InventoryItems.Where(s => s.Type == type)));

                    InventoryChartWidthRequest = TotalItems * 30;
                    TotalItems = lowqty.Count();

                    Value = InventoryItems.Where(s => s.Type == type && s.Quantity <=5).Sum(s=>s.Price*s.Quantity);
                    InventorySelecteOption = string.Empty;
                    TypeFilterSelected = string.Empty;
                }
                else
                {

                InventoryTitle = selected;
                var lowqty = _productItemData.GetProductItemsWithQty(5);
                InventoryItems = new ObservableCollection<ProductItem>(lowqty);

                InventoryChartChanged(TotalProductWithQty(InventoryItems));

                InventoryChartWidthRequest = TotalItems * 30;
                TotalItems = lowqty.Count();

                Value = lowqty.Sum(x => x.Price * x.Quantity);
                InventorySelecteOption = string.Empty;
                }

            }

            if (selected == "High Quantity")
            {

                if (type != null)
                {
                    InventoryTitle = selected;
                    var highqty = _productItemData.GetHighQty(10);
                    InventoryItems = new ObservableCollection<ProductItem>(highqty);

                    InventoryChartChanged(TotalProductWithQty(InventoryItems.Where(s => s.Type == type)));
                    TotalItems = InventoryItems.Where(i=> i.Type == type).Count();
                    InventoryChartWidthRequest = TotalItems * 30;
                    Value = InventoryItems.Where(s => s.Type == type && s.Quantity > 5).Sum(s => s.Price * s.Quantity);
                    InventorySelecteOption = string.Empty;
                    TypeFilterSelected = string.Empty;
                }
                else
                {

                InventoryTitle = selected;
                var highqty = _productItemData.GetHighQty(10);
                InventoryItems = new ObservableCollection<ProductItem>(highqty);

                InventoryChartChanged(TotalProductWithQty(InventoryItems));
                TotalItems = highqty.Count();
                InventoryChartWidthRequest = TotalItems * 30;
                Value = highqty.Sum(x => x.Price * x.Quantity);
                InventorySelecteOption = string.Empty;

                }

            }
            if (selected == "Inventory Information")
            {

                if (type != null)
                {
                    InventoryTitle = selected;

                    var items = _productItemData.GetAll();
                    InventoryItems = new ObservableCollection<ProductItem>(items.Where(s => s.Type == type));

                    Value = _productItemData.GetValueOfProductsByQty(InventoryItems.Where(s => s.Type == type));
                    InventoryChartChanged(TotalProductWithQty(InventoryItems.Where(s => s.Type == type)));

                    TotalItems = InventoryItems.Where(s => s.Type == type).Count();
                    InventoryChartWidthRequest = TotalItems * 30;
                    Value = items.Sum(x => x.Price * x.Quantity);

                    InventorySelecteOption = string.Empty;
                    TypeFilterSelected = string.Empty;
                }
                else
                {

                InventoryTitle = selected;

                var items = _productItemData.GetAll();
                InventoryItems = new ObservableCollection<ProductItem>(items);

                Value = _productItemData.GetValueOfProductsByQty(items);

                TotalItems = items.Count();
                InventoryChartWidthRequest = TotalItems * 30;
                Value = items.Sum(x => x.Price * x.Quantity);


                InventoryChartChanged(TotalProductWithQty(_productItemData.GetAll()));
                InventorySelecteOption = string.Empty;
                }
            }
            if (selected == "Products Earning")
            {

                if (type != null)
                {
                    InventoryTitle = selected;
                    var items = _productItemData.GetAll();
                    InventoryItems = new ObservableCollection<ProductItem>(items);

                    InventoryChartChanged(ProductEarning(InventoryItems.Where(s => s.Type == type)));
                    InventorySelecteOption = string.Empty;
                    TypeFilterSelected = string.Empty;
                }
                else
                {

                InventoryTitle = selected;
                var items = _productItemData.GetAll();
                InventoryItems = new ObservableCollection<ProductItem>(items);

                InventoryChartChanged(ProductEarning(InventoryItems));
                InventorySelecteOption = string.Empty;
                }

            }

        }

        void OptionSelectedFilterbyType(string selected,string type)
        {
           
            if (selected == "Low Quantity")
            {
                InventoryTitle = selected;
                var lowqty = _productItemData.GetProductItemsWithQty(5);
                InventoryItems = new ObservableCollection<ProductItem>(lowqty.Where(s => s.Type == type));

                InventoryChartChanged(TotalProductWithQty(InventoryItems));

                InventoryChartWidthRequest = TotalItems * 30;
                TotalItems = lowqty.Count();

                Value = lowqty.Sum(x => x.Price * x.Quantity);
                InventorySelecteOption = string.Empty;

            }
            if (selected == "High Quantity")
            {
              


            }
            if (selected == "Inventory Information")
            {
               


                InventoryChartChanged(TotalProductWithQty(_productItemData.GetAll()));
                InventorySelecteOption = string.Empty;
            }
            if (selected == "Products Earning")
            {
               

            }

        }


        #endregion
    }
}
