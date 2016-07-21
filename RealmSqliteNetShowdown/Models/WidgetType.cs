using System;
using System.Collections.Generic;
using PropertyChanged;
using Realms;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace WidgetList.Abstract.Widgets
{
    [ImplementPropertyChanged]
    public class WidgetType : RealmObject
    {
        [Realms.ObjectId]
        public int Id { get; set; }
        [Realms.Indexed]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public RealmList<WidgetListing> WidgetListings {get;}
        public string UPCCode { get; set; } //barcode
    }

   
}




