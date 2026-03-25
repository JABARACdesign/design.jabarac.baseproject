using System.Collections.Generic;
using JABARACdesign.Base.Application.Manager;
using JABARACdesign.Base.Domain.Entity;
using NUnit.Framework;

namespace JABARACdesign.Base.Tests.Editor
{
    /// <summary>
    /// DomainDataStoreのテスト。
    /// </summary>
    [TestFixture]
    public class DomainDataStoreTest
    {
        #region Test Data
        
        /// <summary>
        /// テスト用のドメインデータ。
        /// </summary>
        private class TestDomainData : IDomainData
        {
            public string Id { get; }
            public string Name { get; }
            
            public TestDomainData(string id, string name)
            {
                Id = id;
                Name = name;
            }
        }
        
        /// <summary>
        /// テスト用のソート可能なドメインデータ。
        /// </summary>
        private class TestSortableDomainData : IDomainData, ISortable
        {
            public string Id { get; }
            public string Name { get; }
            public int SortOrder { get; }
            
            public TestSortableDomainData(string id, string name, int sortOrder)
            {
                Id = id;
                Name = name;
                SortOrder = sortOrder;
            }
        }
        
        /// <summary>
        /// テスト用のintキードメインデータ。
        /// </summary>
        private class TestIntKeyDomainData : IDomainData
        {
            public int Id { get; }
            public string Name { get; }
            
            public TestIntKeyDomainData(int id, string name)
            {
                Id = id;
                Name = name;
            }
        }
        
        #endregion
        
        [Test]
        public void GetByKey_登録済みデータを取得できる()
        {
            // Arrange
            var dataList = new List<TestDomainData>
            {
                new(id: "1", name: "Item1"),
                new(id: "2", name: "Item2"),
            };
            var store = new DomainDataStore<TestDomainData, string>(
                domainDataList: dataList,
                getKeyFunc: d => d.Id);
            
            // Act
            var result = store.GetByKey(key: "1");
            
            // Assert
            Assert.AreEqual(expected: "Item1", actual: result.Name);
        }
        
        [Test]
        public void GetByKey_存在しないキーでdefaultが返る()
        {
            // Arrange
            var store = new DomainDataStore<TestDomainData, string>(
                domainDataList: new List<TestDomainData>(),
                getKeyFunc: d => d.Id);
            
            // Act
            var result = store.GetByKey(key: "nonexistent");
            
            // Assert
            Assert.IsNull(anObject: result);
        }
        
        [Test]
        public void Add_追加したデータをキーで取得できる()
        {
            // Arrange
            var store = new DomainDataStore<TestDomainData, string>(
                domainDataList: new List<TestDomainData>(),
                getKeyFunc: d => d.Id);
            
            // Act
            store.Add(data: new TestDomainData(id: "3", name: "Item3"));
            var result = store.GetByKey(key: "3");
            
            // Assert
            Assert.IsNotNull(anObject: result);
            Assert.AreEqual(expected: "Item3", actual: result.Name);
        }
        
        [Test]
        public void GetAll_全データがリストで返る()
        {
            // Arrange
            var dataList = new List<TestDomainData>
            {
                new(id: "1", name: "Item1"),
                new(id: "2", name: "Item2"),
                new(id: "3", name: "Item3"),
            };
            var store = new DomainDataStore<TestDomainData, string>(
                domainDataList: dataList,
                getKeyFunc: d => d.Id);
            
            // Act
            var result = store.GetAll();
            
            // Assert
            Assert.AreEqual(expected: 3, actual: result.Count);
        }
        
        [Test]
        public void GetAll_ISortable実装データはSortOrder順で返る()
        {
            // Arrange
            var dataList = new List<TestSortableDomainData>
            {
                new(id: "1", name: "Third", sortOrder: 3),
                new(id: "2", name: "First", sortOrder: 1),
                new(id: "3", name: "Second", sortOrder: 2),
            };
            var store = new DomainDataStore<TestSortableDomainData, string>(
                domainDataList: dataList,
                getKeyFunc: d => d.Id);
            
            // Act
            var result = store.GetAll();
            
            // Assert
            Assert.AreEqual(expected: "First", actual: result[0].Name);
            Assert.AreEqual(expected: "Second", actual: result[1].Name);
            Assert.AreEqual(expected: "Third", actual: result[2].Name);
        }
        
        [Test]
        public void IntKey_intキーでデータを正しく管理できる()
        {
            // Arrange
            var dataList = new List<TestIntKeyDomainData>
            {
                new(id: 100, name: "ItemA"),
                new(id: 200, name: "ItemB"),
            };
            var store = new DomainDataStore<TestIntKeyDomainData, int>(
                domainDataList: dataList,
                getKeyFunc: d => d.Id);
            
            // Act
            var result = store.GetByKey(key: 200);
            
            // Assert
            Assert.AreEqual(expected: "ItemB", actual: result.Name);
        }
        
        [Test]
        public void IDomainDataStore_非ジェネリックインターフェース経由でGetAllできる()
        {
            // Arrange — TKeyがintでもIDomainDataStore<T>経由でGetAllできることを確認
            var dataList = new List<TestIntKeyDomainData>
            {
                new(id: 1, name: "A"),
                new(id: 2, name: "B"),
            };
            var store = new DomainDataStore<TestIntKeyDomainData, int>(
                domainDataList: dataList,
                getKeyFunc: d => d.Id);
            
            // Act — IDomainDataStore<T>にキャストしてGetAll（DomainDataManagerBaseと同じ経路）
            var nonGenericStore = (IDomainDataStore<TestIntKeyDomainData>)store;
            var result = nonGenericStore.GetAll();
            
            // Assert
            Assert.AreEqual(expected: 2, actual: result.Count);
        }
        
        [Test]
        public void AddRange_複数データを一括追加できる()
        {
            // Arrange
            var store = new DomainDataStore<TestDomainData, string>(
                domainDataList: new List<TestDomainData>(),
                getKeyFunc: d => d.Id);
            
            var newDataList = new List<TestDomainData>
            {
                new(id: "a", name: "Alpha"),
                new(id: "b", name: "Bravo"),
            };
            
            // Act
            store.AddRange(dataList: newDataList);
            
            // Assert
            Assert.AreEqual(expected: 2, actual: store.GetAll().Count);
            Assert.AreEqual(expected: "Alpha", actual: store.GetByKey(key: "a").Name);
            Assert.AreEqual(expected: "Bravo", actual: store.GetByKey(key: "b").Name);
        }
    }
}
