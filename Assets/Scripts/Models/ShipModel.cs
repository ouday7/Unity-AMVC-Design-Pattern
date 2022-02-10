using AMVC.Core;
using UnityEngine.UIElements;

namespace AMVC.Models
{
    [System.Serializable]
    public class ShipModel
    {
       
        public string ship_name;
        public string ship_id;
        public string ship_model;
        public string ship_type;
        public string active;
        public string imo;
        public string mmsi;
        public int abs;
        public int speed_kn;
        public int weight_lbs;
        public int weight_kg;
        public string year_built;
        public string url;
        public string home_port;
        public string status;
        public string image;
    }
}