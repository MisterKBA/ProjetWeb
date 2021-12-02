using System;
using System.Collections.Generic;

namespace NasaImage.Model
{
    public class TodoDate
    {

        public string date { get; set; }
        public List<TodoDate> todoDates{get; set;}
        public List <TodoResult> list { get; set; }
    }
}
