using System;
using System.Collections.Generic;
using PropertyChanged;
using Realms;
using SQLiteNetExtensions.Attributes;

namespace WidgetList.Abstract.Widgets
{
    [ImplementPropertyChanged]
    public class WidgetType : RealmObject
    {
        [Indexed]
        public int Id { get; set; }
        [Indexed]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public RealmList<WidgetListing> WidgetListings {get;}
        public string UPCCode { get; set; } //barcode
    }

   public class WidgetTypeModel
    {
        public WidgetTypeModel() 
        {
            WidgetListings = new List<WidgetListingModel>(4);
        }

        [Indexed]
        public int Id { get; set; }
        public string Description { get;  set; }
        public string Name { get; set; }
        public string Category { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<WidgetListingModel> WidgetListings { get; set; }
        public string UPCCode { get; set; } //barcode
    }
}




