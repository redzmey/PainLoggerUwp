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
        private readonly StorageFolder _localFolder = ApplicationData.Current.LocalFolder;

        public virtual async void AddNew(T element)
        {
            List<T> list = await GetAll() ?? new List<T>();

            StorageFile textFile = await _localFolder.CreateFileAsync(typeof (T).ToString(),
                
                                                                      CreationCollisionOption.ReplaceExisting);
            list.Add(element);
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

        public virtual async Task<List<T>> GetAll()
        {
            StorageFile textFile = await _localFolder.GetFileAsync(typeof (T).ToString());
            string content = await FileIO.ReadTextAsync(textFile);
            return JsonConvert.DeserializeObject<List<T>>(content);
        }

        public virtual T GetOne(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> IsExistst(T element)
        {
            List<T> all = await GetAll();

            return all.Any(x => x.Id == element.Id);
        }

        public virtual void Update(T element)
        {
            throw new NotImplementedException();
        }
    }
}