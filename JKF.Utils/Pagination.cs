using System;

namespace JKF.Commons
{
    public class Pagination
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int SkipCount 
        { 
            get 
            {
                return PageSize * PageIndex;
            }
        }

        public long TotalCount { get; set; }

        public int PageCount 
        { 
            get 
            {
                int count = (int)(TotalCount / PageSize);
                return TotalCount % PageSize == 0 ? count : count + 1; 
            } 
        }
    }
    public class Sort
    {
        public string sortField {get;set;}
        public string sortOrder { get; set; }
    }
}
