using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Newtonsoft.Json;
using PainLogUWP.Interfaces;

namespace PainLogUWP.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : IElement
    {
        private Task<List<T>> _elementsList;

        private Task<List<T>> ElementsList
        {
            get
            {
                if (_elementsList == null)
                {
                    return GetAll();
                }
                else
                    return _elementsList;
            }
            set { _elementsList = value; }
        }

        public StorageFolder LocalFolder => ApplicationData.Current.LocalFolder;

        public virtual async Task AddNew(T element)
        {
            List<T> list = await ElementsList;
            if (await IsExistst(element) == false)
            {
                list?.Add(element);
                await WriteFile(list);
            }
        }

        public virtual async Task Delete(T element)
        {
            List<T> list = await ElementsList;
            if (await IsExistst(element) == true)
            {
                list?.Remove(element);
                await WriteFile(list);
            }
        }

        public virtual async Task<List<T>> GetAll()
        {
            if (await LocalFolder.TryGetItemAsync(typeof(T).ToString()) == null)
                return new List<T>();

            StorageFile textFile = await LocalFolder.GetFileAsync(typeof(T).ToString());
            string content = await FileIO.ReadTextAsync(textFile);

            return JsonConvert.DeserializeObject<List<T>>(content);
        }

        //public virtual T GetOne(Guid id)
        //{
        //    throw new NotImplementedException();
        //}

        public virtual async Task<bool> IsExistst(T element)
        {
            List<T> list = await ElementsList;
            return list.Any(x => x.Id == element.Id);
        }

        private async Task WriteFile(List<T> list)
        {
            StorageFile textFile = await LocalFolder.CreateFileAsync(typeof(T).ToString(),
                                                                     CreationCollisionOption.ReplaceExisting);
            string jsonContents = JsonConvert.SerializeObject(list);
            using (IRandomAccessStream textStream = await textFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (DataWriter textWriter = new DataWriter(textStream))
                {
                    textWriter.WriteString(jsonContents);
                    await textWriter.StoreAsync();
                }
            }
        }

        //}
        //    throw new NotImplementedException();
        //{

        //public virtual void Update(T element)
    }
}