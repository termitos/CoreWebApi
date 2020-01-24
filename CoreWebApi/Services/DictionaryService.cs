using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using CoreWebApi.Model;

namespace CoreWebApi.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class DictionaryService
    {
        /// <summary>
        /// The dynamo dynamoContext
        /// </summary>
        private readonly DynamoDBContext _dynamoContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryService" /> class.
        /// </summary>
        /// <param name="dynamoDbClient">The dynamo database client.</param>
        public DictionaryService(IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoContext = new DynamoDBContext(dynamoDbClient);
        }

        /// <summary>
        /// Get items
        /// </summary>
        /// <returns></returns>
        public async Task<List<Item>> GetItems()
        {
            return await _dynamoContext.ScanAsync<Item>(new List<ScanCondition>()).GetRemainingAsync();
        }

        /// <summary>
        /// Get filtered items
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<List<Item>> GetItemsWithFilter(FilterRequest filter)
        {
            var scanFilter = new ScanFilter();
            scanFilter.AddCondition(filter.Attribute, ScanOperator.Equal, filter.Value);

            var config = new ScanOperationConfig
            {
                Filter = scanFilter,
                Select = SelectValues.SpecificAttributes,
                AttributesToGet = new List<string> {"Id", "Name", "Key", "Value", "Description"}
            };

            var searchItems = _dynamoContext.FromScanAsync<Item>(config);

            var items = new List<Item>();

            do
            {
                var searchResults = await searchItems.GetNextSetAsync();
                items.AddRange(searchResults);
            } while (!searchItems.IsDone);

            return items;
        }

        /// <summary>
        /// Save item
        /// </summary>
        /// <param name="name"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task<string> SaveItem(string name, string key, string value, string description)
        {
            var item = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Key = key,
                Value = value,
                Description = description
            };

            await _dynamoContext.SaveAsync(item);

            return item.Id;
        }

        /// <summary>
        /// Update item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task<string> UpdateItem(string id, string name, string key, string value, string description)
        {
            var item = await _dynamoContext.LoadAsync<Item>(id);
            item.Name = name;
            item.Key = key;
            item.Value = value;
            item.Description = description;

            await _dynamoContext.SaveAsync(item);

            return item.Id;
        }
    }
}