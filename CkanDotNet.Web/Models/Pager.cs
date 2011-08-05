using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace CkanDotNet.Web.Models
{
    public class Pager
    {
        private int page;
        private int recordsPerPage;
        private int recordCount;

        public Pager(int page, int recordsPerPage)
        {
            this.page = Math.Max(page,1);
            this.recordsPerPage = Math.Max(recordsPerPage, 1);
        }

        public Pager(int page, int recordsPerPage, int recordCount)
        {
            this.page = Math.Max(page, 1);
            this.recordsPerPage = Math.Max(recordsPerPage, 1);
            this.recordCount = recordCount;
        }

        /// <summary>
        /// Get the number of records per page
        /// </summary>
        public int RecordsPerPage
        {
            get
            {
                return this.recordsPerPage;
            }
        }

        /// <summary>
        /// Get the first page number
        /// </summary>
        public int First
        {
            get
            {
                return (this.page * this.recordsPerPage) - this.recordsPerPage + 1;
            }
        }

        /// <summary>
        /// Get the total number of pages
        /// </summary>
        public int Last
        {
            get
            {
                return (int)Math.Ceiling((double)recordCount / (double)recordsPerPage);
            }
        }

        /// <summary>
        /// Get the previous page number
        /// </summary>
        public int Previous
        {
            get
            {
                int previous = this.page - 1;
                if (previous < 1)
                {
                    previous = 1;
                }
                return previous;
            }
        }

        /// <summary>
        /// Get the next page number
        /// </summary>
        public int Next
        {
            get
            {
                int next = this.page + 1;
                if (next > Last)
                {
                    next = Last;
                }
                return next;
            }
        }

        /// <summary>
        /// Get the total number of records
        /// </summary>
        public int RecordCount
        {
            get
            {
                return this.recordCount;
            }
            set
            {
                this.recordCount = value;
            }
        }

        /// <summary>
        /// Get the current page
        /// </summary>
        public int Page
        {
            get
            {
                return this.page;
            }
        }

        /// <summary>
        /// Get the record offset for the first record on the current page
        /// </summary>
        public int RecordOffset {
            get
            {
                return (this.page * this.recordsPerPage) - recordsPerPage;
            }
        }

        /// <summary>
        /// Get the record offset for the first record on the current page
        /// </summary>
        public int FirstRecordNumberOnCurrentPage
        {
            get
            {
                return RecordOffset + 1;
            }
        }

        /// <summary>
        /// Get the record offset for the first record on the current page
        /// </summary>
        public int LastRecordNumberOnCurrentPage
        {
            get
            {
                return Math.Min(RecordOffset + RecordsPerPage, RecordCount);
            }
        }

        public override string ToString()
        {
            string result = String.Empty;

            if (FirstRecordNumberOnCurrentPage != LastRecordNumberOnCurrentPage)
            {
                result = String.Format("{0} - {1} of {2}",
                    FirstRecordNumberOnCurrentPage,
                    LastRecordNumberOnCurrentPage,
                    RecordCount);
            }
            else
            {
                result = String.Format("{0} of {1}",
                    LastRecordNumberOnCurrentPage,
                    RecordCount);
            }   

            return result;
        }
    }
}