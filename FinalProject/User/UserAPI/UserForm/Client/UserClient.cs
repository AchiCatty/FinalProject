using MyNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserForm.Client
{
    public static class UserClient
    {
        public static AccountingSubjectsClient AccountingSubjectsClient = new AccountingSubjectsClient("http://localhost:58549/", new System.Net.Http.HttpClient());
        public static AccountsClient AccountsClient = new AccountsClient("http://localhost:58549/", new System.Net.Http.HttpClient());
        public static AccountTypesClient AccountTypesClient = new AccountTypesClient("http://localhost:58549/", new System.Net.Http.HttpClient());
        public static AccountsClient accountsClient = new AccountsClient("http://localhost:58549/", new System.Net.Http.HttpClient());
        public static CustomersClient CustomersClient = new CustomersClient("http://localhost:58549/", new System.Net.Http.HttpClient());
        public static CustomerTypesClient CustomerTypesClient = new CustomerTypesClient("http://localhost:58549/", new System.Net.Http.HttpClient());
        public static EmployeesClient EmployeesClient = new EmployeesClient("http://localhost:58549/", new System.Net.Http.HttpClient());
        public static FacilitiesClient FacilitiesClient = new FacilitiesClient("http://localhost:58549/", new System.Net.Http.HttpClient());
        public static FakeAccountInfoesClient FakeAccountInfoesClient = new FakeAccountInfoesClient("http://localhost:58549/", new System.Net.Http.HttpClient());
        public static FaresClient FaresClient = new FaresClient("http://localhost:58549/", new System.Net.Http.HttpClient());
        public static PurchaseItemsClient PurchaseItemsClient = new PurchaseItemsClient("http://localhost:58549/", new System.Net.Http.HttpClient());
        public static PurchasesClient PurchasesClient = new PurchasesClient("http://localhost:58549/", new System.Net.Http.HttpClient());
        public static RecieptsClient RecieptsClient = new RecieptsClient("http://localhost:58549/", new System.Net.Http.HttpClient());
        public static RegionsClient RegionsClient = new RegionsClient("http://localhost:58549/", new System.Net.Http.HttpClient());
        public static StoragesClient StoragesClient = new StoragesClient("http://localhost:58549/", new System.Net.Http.HttpClient());
        public static StorageSizesClient StorageSizesClient = new StorageSizesClient("http://localhost:58549/", new System.Net.Http.HttpClient());
        public static StorageTypesClient StorageTypesClient = new StorageTypesClient("http://localhost:58549/", new System.Net.Http.HttpClient());
        public static TransactionsClient TransactionsClient = new TransactionsClient("http://localhost:58549/", new System.Net.Http.HttpClient());
        public static TransactionTypesClient TransactionTypesClient = new TransactionTypesClient("http://localhost:58549/", new System.Net.Http.HttpClient());
    }
}
