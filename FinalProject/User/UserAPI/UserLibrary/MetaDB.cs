using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLibrary
{
    [JsonObject(MemberSerialization.OptIn)]
    [MetadataType(typeof(AccountBuddy))]
    public partial class Account
    {
    }
    public class AccountBuddy
    {
        [JsonProperty]
        public int AccountId { get; set; }
        [JsonProperty]
        public string AccountsName { get; set; }
        [JsonProperty]
        public string AccountDetail { get; set; }
        [JsonProperty]
        public int AccountTypeId { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    [MetadataType(typeof(AccountingSubjectBuddy))]
    public partial class AccountingSubject
    {
    }
    public class AccountingSubjectBuddy
    {
        [JsonProperty]
        public int AccountingSubjectId { get; set; }
        [JsonProperty] 
        public string AccountingSubjectDescription { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    [MetadataType(typeof(AccountTypeBuddy))]
    public partial class AccountType
    {
    }
    public class AccountTypeBuddy
    {
        [JsonProperty]
        public int AccountTypeId { get; set; }
        [JsonProperty] 
        public string AccountTypesDescription { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    [MetadataType(typeof(CustomerBuddy))]
    public partial class Customer
    {
    }
    public class CustomerBuddy
    {
        [JsonProperty]
        public int CustomerId { get; set; }
        [JsonProperty]
        public string CustomerName { get; set; }
        [JsonProperty]
        public string CustomerPhone { get; set; }
        [JsonProperty]
        public string CustomerEmail { get; set; }
        [JsonProperty]
        public System.DateTime RegistrationDate { get; set; }
        [JsonProperty]
        public string LoginId { get; set; }
        [JsonProperty]
        public string Password { get; set; }
        [JsonProperty]
        public string OtherDetails { get; set; }
        [JsonProperty]
        public int CustomerTypeId { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    [MetadataType(typeof(CustomerTypeBuddy))]
    public partial class CustomerType
    {
    }
    public class CustomerTypeBuddy
    {
        [JsonProperty]
        public int CustomerTypeId { get; set; }
        [JsonProperty]
        public string CustomerTypeDescription { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    [MetadataType(typeof(EmployeeBuddy))]
    public partial class Employee
    {
    }
    public class EmployeeBuddy
    {
        [JsonProperty]
        public int EmployeeId { get; set; }
        [JsonProperty]
        public string Name { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    [MetadataType(typeof(FacilityBuddy))]
    public partial class Facility
    {
    }
    public class FacilityBuddy
    {
        [JsonProperty]
        public int FacilityId { get; set; }
        [JsonProperty]
        public string FacilitySpecDescription { get; set; }
        [JsonProperty]
        public int RegionId { get; set; }
        [JsonProperty]
        public int EmployeeId { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    [MetadataType(typeof(FakeAccountInfoBuddy))]
    public partial class FakeAccountInfo
    {
    }
    public class FakeAccountInfoBuddy
    {
        [JsonProperty]
        public int InfoId { get; set; }
        [JsonProperty]
        public System.DateTime Date { get; set; }
        [JsonProperty]
        public int Sales { get; set; }
        [JsonProperty]
        public int OtherRevenues { get; set; }
        [JsonProperty]
        public int SalesReturns { get; set; }
        [JsonProperty]
        public int GrossProfit { get; set; }
        [JsonProperty]
        public int Wages { get; set; }
        [JsonProperty]
        public int Depreciation { get; set; }
        [JsonProperty]
        public int Rent { get; set; }
        [JsonProperty]
        public int OtherSupplies { get; set; }
        [JsonProperty]
        public int Utilities { get; set; }
        [JsonProperty]
        public int Insurance { get; set; }
        [JsonProperty]
        public int Maintenance { get; set; }
        [JsonProperty]
        public int Advertising { get; set; }
        [JsonProperty]
        public int OtherExpenses { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    [MetadataType(typeof(FareBuddy))]
    public partial class Fare
    {
    }
    public class FareBuddy
    {
        [JsonProperty]
        public int FareId { get; set; }
        [JsonProperty]
        public int StorageTypeId { get; set; }
        [JsonProperty]
        public int FarePerMinute { get; set; }
        [JsonProperty]
        public int StorageSizeId { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    [MetadataType(typeof(PurchaseBuddy))]
    public partial class Purchase
    {
    }
    public class PurchaseBuddy
    {
        [JsonProperty]
        public int PurchaseId { get; set; }
        [JsonProperty]
        public System.DateTime PurchaseTime { get; set; }
        [JsonProperty]
        public string OtherDetails { get; set; }
        [JsonProperty]
        public int CustomerId { get; set; }
        [JsonProperty]
        public int PurchaseAmount { get; set; }
        [JsonProperty]
        public int TransactionId { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    [MetadataType(typeof(PurchaseItemBuddy))]
    public partial class PurchaseItem
    {
    }
    public class PurchaseItemBuddy
    {
        [JsonProperty]
        public int PurchaseItemId { get; set; }
        [JsonProperty]
        public System.DateTime InTime { get; set; }
        [JsonProperty]
        public System.DateTime OutTime { get; set; }
        [JsonProperty]
        public int StorageId { get; set; }
        [JsonProperty]
        public int PurchaseId { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    [MetadataType(typeof(RecieptBuddy))]
    public partial class Reciept
    {
    }
    public class RecieptBuddy
    {
        [JsonProperty]
        public int RecieptId { get; set; }
        [JsonProperty]
        public int TransactionId { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    [MetadataType(typeof(RegionBuddy))]
    public partial class Region
    {
    }
    public class RegionBuddy
    {
        [JsonProperty]
        public int RegionId { get; set; }
        [JsonProperty]
        public string City { get; set; }
        [JsonProperty]
        public string District { get; set; }
        [JsonProperty]
        public string Town { get; set; }
        [JsonProperty]
        public Nullable<decimal> Latitude { get; set; }
        [JsonProperty]
        public Nullable<decimal> Longitude { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    [MetadataType(typeof(StorageBuddy))]
    public partial class Storage
    {
    }
    public class StorageBuddy
    {
        [JsonProperty]
        public int StorageId { get; set; }
        [JsonProperty]
        public int FacilityId { get; set; }
        [JsonProperty]
        public bool Activated { get; set; }
        [JsonProperty]
        public int FareId { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    [MetadataType(typeof(StorageSizeBuddy))]
    public partial class StorageSize
    {
    }
    public class StorageSizeBuddy
    {
        [JsonProperty]
        public int StorageSizeId { get; set; }
        [JsonProperty]
        public int Width { get; set; }
        [JsonProperty]
        public int Height { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    [MetadataType(typeof(StorageTypeBuddy))]
    public partial class StorageType
    {
    }
    public class StorageTypeBuddy
    {
        [JsonProperty]
        public int StorageTypeId { get; set; }
        [JsonProperty]
        public string StorageTypeName { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    [MetadataType(typeof(TransactionBuddy))]
    public partial class Transaction
    {
    }
    public class TransactionBuddy
    {
        [JsonProperty]
        public int TransactionId { get; set; }
        [JsonProperty]
        public System.DateTime Date { get; set; }
        [JsonProperty]
        public int AmountofTransaction { get; set; }
        [JsonProperty]
        public int AccountId { get; set; }
        [JsonProperty]
        public int TransactionTypeId { get; set; }
        [JsonProperty]
        public int AccountingSubjectId { get; set; }
        [JsonProperty]
        public string OtherDetails { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    [MetadataType(typeof(TransactionTypeBuddy))]
    public partial class TransactionType
    {
    }
    public class TransactionTypeBuddy
    {
        [JsonProperty]
        public int TransactionTypesId { get; set; }
        [JsonProperty]
        public string TransactionTypesDescription { get; set; }
    }


}