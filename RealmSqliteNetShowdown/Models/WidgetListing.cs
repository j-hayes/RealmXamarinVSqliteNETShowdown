using PropertyChanged;
using Realms;

namespace WidgetList.Abstract.Widgets
{
    [ImplementPropertyChanged]
    public class WidgetListing : RealmObject
    {
        [ObjectId]
        public int Id { get; set; }
        public WidgetType WidgetType { get; set; }
        [Indexed]
        public string WidgetTypeName { get; set; }
        public double Price { get; set; }
        public int QuantityOnHand { get; set; }
        [Ignored]
        public string PriceStr { 
            get
            {
                return Price.ToString();
            }
        }
     
    }

    
}