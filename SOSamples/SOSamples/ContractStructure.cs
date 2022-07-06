using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOSamples
{
    /// <summary>
    /// OP was using inheritance while the structure needs composition.
    /// </summary>

    public class Contract
    {
        public int ContractId
        {
            get;
            set;
        }
        public DateTime StartDate
        {
            get;
            set;
        }
        public int Months
        {
            get;
            set;
        }
        public decimal Charge
        {
            get;
            set;
        }

        public bool IsMultiContract
        {
            get
            {
                return SubContracts.GroupBy(x => x.ContractType).Count() > 0;
            }
        }

        /// <summary>
        /// Optional unique check for subcontract types may be added
        /// </summary>
        public List<SubContract> SubContracts
        {
            get;
            set;
        }
    }

    public enum ContractType
    {
        Broadband,
        Mobile,
        TV
    }


    public class SubContract
    {
        public ContractType ContractType
        {
            get;
            set;
        }
    }

    public class MobileContract : SubContract
    {
        public string MobileNumber
        {
            get;
            set;
        }
    }

    public class TvContract : SubContract
    {
        public PackageType PackageType
        {
            get;
            set;
        }
    }

    public class BroadBandContract : SubContract
    {
        public int DownloadSpeed
        {
            get;
            set;
        }
    }

    public enum PackageType
    {
        S,
        M,
        L,
        XL
    }
}
