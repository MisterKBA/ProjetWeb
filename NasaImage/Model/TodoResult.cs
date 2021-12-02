using System;
using System.Collections.Generic;
using System.Text;

namespace NasaImage.Model
{
    public class TodoResult
    {
        public string identifier { get; set; }
        public string caption { get; set; }
        public string image { get; set; }
        public string version { get; set; }
        public string adresse { get; set; }
        public Centroid centroid_coordinates { get; set; }

        public List<TodoResult> todoResultlist { get; set; }
    }
}
