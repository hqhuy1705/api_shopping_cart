using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualShopping.API.SignalR
{
    public class GroupsTracker
    {
        private static readonly SortedDictionary<string, List<string>> UsersToResponseOfOrders = 
            new SortedDictionary<string, List<string>>();
        private static readonly SortedDictionary<string, List<string>> UsersToResponseOfCarts =
            new SortedDictionary<string, List<string>>();
        private static readonly SortedDictionary<string, List<string>> UsersToResponseOfShops =
            new SortedDictionary<string, List<string>>();

        #region Methods for order
        public Task UserConnectedToOrder(string orderId, string connectionId)
        {
            AddConnectionIdsToStore(UsersToResponseOfOrders, orderId, connectionId);
            return Task.FromResult(true);
        }

        public Task UserDisconnectedToOrder(string orderId, string connectionId)
        {
            RemoveConnectionIdsFromStore(UsersToResponseOfOrders, orderId, connectionId);
            return Task.FromResult(true);
        }

        public Task<List<string>> GetUserConnectionsOfOrder(string orderId)
        {
            var connectionIds = GetUserConnectionIdsFromStore(UsersToResponseOfOrders, orderId);
            return Task.FromResult(connectionIds);
        }
        #endregion

        #region Methods for cart
        public Task UserConnectedToCart(string cartId, string connectionId)
        {
            AddConnectionIdsToStore(UsersToResponseOfCarts, cartId, connectionId);
            return Task.FromResult(true);
        }

        public Task UserDisconnectedToCart(string cartId, string connectionId)
        {
            RemoveConnectionIdsFromStore(UsersToResponseOfCarts, cartId, connectionId);
            return Task.FromResult(true);
        }

        public Task<List<string>> GetUserConnectionsOfCart(string cartId)
        {
            var connectionIds = GetUserConnectionIdsFromStore(UsersToResponseOfCarts, cartId);
            return Task.FromResult(connectionIds);
        }
        #endregion

        #region Methods for shop
        public Task UserConnectedToShop(string shopId, string connectionId)
        {
            AddConnectionIdsToStore(UsersToResponseOfShops, shopId, connectionId);
            return Task.FromResult(true);
        }

        public Task UserDisconnectedToShop(string shopId, string connectionId)
        {
            RemoveConnectionIdsFromStore(UsersToResponseOfShops, shopId, connectionId);
            return Task.FromResult(true);
        }

        public Task<List<string>> GetUserConnectionsOfShop(string shopId)
        {
            var connectionIds = GetUserConnectionIdsFromStore(UsersToResponseOfShops, shopId);
            return Task.FromResult(connectionIds);
        }
        #endregion

        #region Private methods
        private void AddConnectionIdsToStore(SortedDictionary<string, List<string>> store,
                                                string key,
                                                string connectionId)
        {
            lock (store)
            {
                if (store.ContainsKey(key))
                {
                    store[key].Add(connectionId);
                }
                else
                {
                    store.Add(key, new List<string>() { connectionId });
                };
            }
        }

        private void RemoveConnectionIdsFromStore(SortedDictionary<string, List<string>> store,
                                                    string key,
                                                    string connectionId)
        {
            lock (store)
            {
                if (store.ContainsKey(key))
                {
                    store[key].Remove(connectionId);
                    if (store[key].Count == 0)
                    {
                        store.Remove(key);
                    }
                }
            }
        }

        private List<string> GetUserConnectionIdsFromStore(SortedDictionary<string, List<string>> store,
                                                                string key)
        {
            List<string> connectionIds;
            lock (store)
            {
                connectionIds = store.GetValueOrDefault(key);
            }
            return connectionIds;
        }
        #endregion
    }
}
