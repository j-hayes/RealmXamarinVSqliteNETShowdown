using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using PropertyChanged;
using Xamarin.Forms;
using Realms;
using System.Collections.Generic;
using WidgetList.Abstract.Widgets;
using RandomStringGenerator;
using System.Linq;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;

namespace RealmSqliteNetShowdown
{

    /* Disclaimer This code is not archetected for anything other than throw away code. 
     * Please do not use this as an example of how to build an Xamarin Forms App 
     * This code is written to stress test Realm and Sqlite.NET and and display the results.
     * 
     *                                                                                                      
     *                                                                                                      
     *           |@                                                                                                                                                                               
     *           |@                                                                                          
     *           |@ @                   @                                                                      
     *           |@  @                 @@                                                                     
     *           |@    @              @@@                                                                        
     *           |@      @          @@  @                                                                      
     *           |@       @   @@@@@@@   @                                                                              
     *           |@        @@@@@@@@     @                                                                            
     *           |@           @@@@@     @                                                                          
     *           |@                     @                                                                     

    */

    [ImplementPropertyChanged]
    public class RealmSqliteNetShowdownViewModel : INotifyPropertyChanged
    {
        List<string> _barCodes;
        List<WidgetTypeModel> _models;

        public event PropertyChangedEventHandler PropertyChanged;

        SQLiteConnection _connection;

        public RealmSqliteNetShowdownViewModel()
        {
            ISqliteConnectionProvider connectionProvieder = DependencyService.Get<ISqliteConnectionProvider>();
            _connection = connectionProvieder.Connection;
            _realm = Realm.GetInstance();
            _barCodes = new List<string>(20000);
            _models = new List<WidgetTypeModel>(20000);
           CreateModels();
        }

        private void CreateModels()
        {
            int nextWidgetListingId=0;
            for (int i = 0; i < 10000; i++) 
            {
                Debug.WriteLine(i);
                var widget = new WidgetTypeModel();
                widget.Description = RandomString(1000);
                widget.Name = RandomString(15);
                widget.Id = i;
                string barcode = (i + 2000000).ToString();
                _barCodes.Add(barcode);
                widget.UPCCode = barcode;
                if (i % 2 == 0)
                {
                    widget.Category = "bowtie";
                }
                else 
                {
                    widget.Category = RandomString(15);
                }

                for (int j = 0; j < 5; j++) 
                {
                    nextWidgetListingId++;
                    widget.WidgetListings.Add(new WidgetListingModel()
                    {

                        Id = nextWidgetListingId,
                        WidgetTypeId = widget.Id,
                        Price = i * 329.23,
                        QuantityOnHand = i,
                        WidgetType = widget,
                        WidgetTypeName = widget.Name
                    });
                }

                _models.Add(widget);
            }
        }

        public ICommand StartRealmCommand { get { return new Command(StartRealm); } }
        public ICommand StartSQLiteCommand { get { return new Command(StartSqlite); } }
        private const int _oneThousand = 1000;
        private const int _tenThousand = 10000;
        private const int _oneHundredThousand = 100000;

        private Realm _realm;



        #region RealmDisplayStrings
     
        public string RealmInsert1000Time{get;set;}
        public string RealmDelete1000Time { get; set; }
        public string RealmInsert10000Time { get; set; }
        public string RealmDelete10000Time { get; set; }
        public string RealmInsert100000Time { get; set; }
        public string RealmCurrentWidgetName{ get;set; }
        public string RealmTotalTestTime { get; set; }

        #endregion


        #region SqliteDisplayStrings

        public string SqliteInsert1000Time { get; set; }
        public string SqliteDelete1000Time { get; set; }
        public string SqliteInsert10000Time { get; set; }
        public string SqliteDelete10000Time { get; set; }
        public string SqliteInsert100000Time { get; set; }
        public string SqliteCurrentWidgetName { get; set; }
        public string SqliteTotalTestTime { get; set; }

        #endregion


        private void StartRealm() 
        {             RealmClearAll();               var realmTotalStopWatch = Stopwatch.StartNew();              Stopwatch stopWatch = Stopwatch.StartNew();             RealmInsert(_oneThousand);             var bowties = RealmSearchForAllOfType("bowtie");             while (bowties.MoveNext())             {                 RealmCurrentWidgetName = bowties.Current.Name;             }             RealmInsert1000Time = stopWatch.ElapsedMilliseconds.ToString();             Debug.WriteLine($"RealmInsert1000Time: {RealmInsert1000Time} ");             stopWatch = Stopwatch.StartNew();             RealmUpdateAllChangeName("Super Awesome");             Debug.WriteLine($"Realm Update 1000: {stopWatch.ElapsedMilliseconds} ");             RealmClearAll();              //test 2             stopWatch = Stopwatch.StartNew();             RealmInsert(_tenThousand);             RealmInsert10000Time = stopWatch.ElapsedMilliseconds.ToString();             Debug.WriteLine($"Realm Insert 10000: {stopWatch.ElapsedMilliseconds} ");             stopWatch = Stopwatch.StartNew();             bowties = RealmSearchForAllOfType("bowtie");             while (bowties.MoveNext())
            {
                RealmCurrentWidgetName = bowties.Current.Name;
            }             Debug.WriteLine($"Realm search by bowtie 10000: {stopWatch.ElapsedMilliseconds} ");             stopWatch = Stopwatch.StartNew();             var bacodes = _barCodes.Skip(_oneThousand).Take(_oneThousand);             foreach (var barCode in bacodes)             {                 var widget = RealmSearchByBarCode(barCode);                 if (widget != null)                 {                     RealmCurrentWidgetName = widget.Name;                 }                 else                 {                     //not found?                 }             }             Debug.WriteLine($"Realm Search by UPC Code 1000 {stopWatch.ElapsedMilliseconds}");

            RealmClearAll();             RealmTotalTestTime = realmTotalStopWatch.ElapsedMilliseconds.ToString();
            Debug.WriteLine($"Realm Total Test Time {RealmTotalTestTime}");
        }

        private void StartSqlite()
        {
            try
            {
                _connection.CreateTable<WidgetTypeModel>();
                _connection.CreateTable<WidgetListingModel>();


                SqliteClearAll();


                var sqliteTotalStopWatch = Stopwatch.StartNew();

                Stopwatch stopWatch = Stopwatch.StartNew();
                SqliteInsert(_oneThousand);
                var bowties = SqliteSearchForAllOfType("bowtie");
                foreach (var bowtie in bowties)
                {
                    SqliteCurrentWidgetName = bowtie.Name;
                }
                SqliteInsert1000Time = stopWatch.ElapsedMilliseconds.ToString();
                Debug.WriteLine($"SqliteInsert1000Time: {SqliteInsert1000Time}");
                stopWatch = Stopwatch.StartNew();
                SqliteUpdateAllChangeName("Super Awesome");
                Debug.WriteLine($"Sqlite Update 1000: {stopWatch.ElapsedMilliseconds}");
                SqliteClearAll();

                //test 2
                stopWatch = Stopwatch.StartNew();
                SqliteInsert(_tenThousand);
                SqliteInsert10000Time = stopWatch.ElapsedMilliseconds.ToString();
                Debug.WriteLine($"Sqlite Insert 10000: {stopWatch.ElapsedMilliseconds}");

                stopWatch = Stopwatch.StartNew();
                bowties = SqliteSearchForAllOfType("bowtie");
                foreach (var bowtie in bowties)
                {
                    SqliteCurrentWidgetName = bowtie.Name;
                }
                Debug.WriteLine($"Sqlite search by bowtie 10000: {stopWatch.ElapsedMilliseconds}");
                stopWatch = Stopwatch.StartNew();
                var bacodes = _barCodes.Skip(_oneThousand).Take(_oneThousand);
                foreach (var barCode in bacodes)
                {
                    var widget = SqliteSearchByBarCode(barCode);
                    if (widget != null)
                    {
                        SqliteCurrentWidgetName = widget.Name;
                    }
                    else
                    {
                        //not found?
                    }
                }
                Debug.WriteLine($"SQLite Search by UPC Code 1000 {stopWatch.ElapsedMilliseconds}");
                SqliteClearAll();
                SqliteTotalTestTime = sqliteTotalStopWatch.ElapsedMilliseconds.ToString();
                Debug.WriteLine($"SQLite Total Test Time {SqliteTotalTestTime}");
            }
            catch (Exception e)
            {
                //
            }
            
        }

        private void RealmUpdateAllChangeName(string update)
        {
            var allWidgets = _realm.All<WidgetType>();
            foreach (var widget in allWidgets) 
            {
                _realm.Write(() =>
                {
                    widget.Name = widget.Name + update;
                }); 
            }
        }

        private void SqliteUpdateAllChangeName(string update)
        {
            var allWidgets = _connection.GetAllWithChildren<WidgetTypeModel>();
            foreach (var widget in allWidgets)
            {
                widget.Name = widget.Name + update;
                _connection.Update(widget);
            }
        }


        /*This is a search on a non index column*/
        private WidgetType RealmSearchByBarCode(string barcode)
        {
            try
            {
                _realm.All<WidgetType>().Where(x => x.UPCCode == barcode).First();
            }
            catch (InvalidOperationException) 
            {
            
            }
            return null;

        }

        /*This is a search on a non index column*/
        private WidgetTypeModel SqliteSearchByBarCode(string barcode)
        {
           return _connection.GetAllWithChildren<WidgetTypeModel>(x => x.UPCCode == barcode).FirstOrDefault();

        }

        void RealmClearAll()
        {
            _realm.Write(() => { _realm.RemoveAll(); });
        }

        void SqliteClearAll()
        {
            _connection.DeleteAll<WidgetTypeModel>();
            _connection.DeleteAll<WidgetListingModel>();
        }


        void RealmDeleteSearchById(int count)
        {
            using (var transaction = _realm.BeginWrite())
            {
                for (int i = 0; i < count; i++)
                {
                    WidgetType widget = new WidgetType();
                    try
                    {
                        widget = _realm.All<WidgetType>().First();//first or default not yet supported
                    }
                    catch (InvalidOperationException e)
                    {
                        continue;
                    }
                    foreach (var listing in widget.WidgetListings)
                    {
                        _realm.Remove(listing);
                    }

                    _realm.Remove(widget);
                }
                transaction.Commit();
            }
        }

        void SqliteDeleteSearchById(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var widget = _connection.FindWithChildren<WidgetTypeModel>(i);
                _connection.Delete(widget);
            }
        }

        private IEnumerator<WidgetType> RealmSearchForAllOfType(string query)
        {
           return _realm.All<WidgetType>().Where(x => x.Category == query).GetEnumerator();
        }

        private IEnumerable<WidgetTypeModel> SqliteSearchForAllOfType(string query)
        {
            return _connection.GetAllWithChildren<WidgetTypeModel>(x => x.Category == query);
        }

        void RealmInsert(int count)
        {
            using (var transaction = _realm.BeginWrite())
            {
                for (int i = 0; i < count; i++)
                {
                    var model = _models[i];
                    var widget = _realm.CreateObject<WidgetType>();
                    MapToRealmWidget(model, widget);
                }
                transaction.Commit();
            }
        }

        void SqliteInsert(int count)
        {
            var models = _models.Where(x => x.Id < count).ToList();
           // var listings = models.SelectMany(x => x.WidgetListings).ToList();
            //foreach (var model in models) 
            //{
            //    //_connection.Insert(model);
            //    //foreach (var listing in model.WidgetListings)
            //    //{
            //    //    _connection.Insert(listing);
            //    //}



            //}
            _connection.InsertAllWithChildren(models);
        }

        private void MapToRealmWidget(WidgetTypeModel model, WidgetType widget)
        {
            widget.Category = model.Category;
            widget.Description = model.Description;
            widget.Id = model.Id;
            widget.Name = model.Name;
            widget.UPCCode = model.UPCCode;
            MapWidgetListingsToRealm(model.WidgetListings, widget);
        }

        private void MapWidgetListingsToRealm(List<WidgetListingModel> widgetListings, WidgetType type)
        {
            foreach (var listingModel in widgetListings)
            {
                var widgetListing = _realm.CreateObject<WidgetListing>();
                widgetListing.Id = listingModel.Id;
                widgetListing.Price = listingModel.Price;
                widgetListing.QuantityOnHand = listingModel.QuantityOnHand;
                widgetListing.WidgetType = type;
                widgetListing.WidgetTypeName = type.Name;
                type.WidgetListings.Add(widgetListing);
            }
        }

        //http://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings-in-c
        private static Random random = new Random();

       

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNO . PQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
   }
}

