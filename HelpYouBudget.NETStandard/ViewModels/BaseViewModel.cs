#define FIX
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
//using Babyfied.NETStandard.Data.Entities;
using HelpYouBudget.NETStandard.Data;
using HelpYouBudget.NETStandard.Data.Entities;
using Newtonsoft.Json;
using PCLStorage;

namespace HelpYouBudget.NETStandard.ViewModels
{
    public class NotifyProp : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class BaseViewModel : NotifyProp
    {

        private bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (IsBusy == value) return;
                isBusy = value;
                RaisePropertyChanged();
            }
        }


        public async Task CheckLastUpdated()
        {

            try
            {
                var lastUpdated = await RetrieveFile<string>(Constants.LastSavedFileName, false);

                if (string.IsNullOrEmpty(lastUpdated)) return;

              DateTime.TryParse(lastUpdated, out var dt);
                var nw = DateTime.Now;
                var cr = nw - dt;
               

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }





        public async Task<bool> SaveAnyString(string dataToSave, string fileName)
        {

            try
            {
                IsBusy = true;
#if FIX
                var rootFolder = FileSystem.Current.LocalStorage;

                //create the shoppingList file
                var file = await rootFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

                //var data = JsonConvert.SerializeObject(dataToSave);

                await file.WriteAllTextAsync(dataToSave);

                IsBusy = false;
                return true;
#endif
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            isBusy = false;
            return false;
        }

        public async Task<bool> SaveAnySession<T>(T dataToSave, string fileName)
        {

            try
            {
                IsBusy = true;
#if FIX
                var rootFolder = FileSystem.Current.LocalStorage;

                //create the shoppingList file
                var file = await rootFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

                var data = JsonConvert.SerializeObject(dataToSave);

                await file.WriteAllTextAsync(data);

                IsBusy = false;
                return true;
#endif
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            isBusy = false;
            return false;
        }

        public async Task SaveWatchList(List<BudgetData> dataToSave)
        {

            try
            {
                IsBusy = true;

                var rootFolder = FileSystem.Current.LocalStorage;

                //create the shoppingList file
                var file = await rootFolder.CreateFileAsync(Constants.WatchListFileName, CreationCollisionOption.ReplaceExisting);

                var data = JsonConvert.SerializeObject(dataToSave);

                await file.WriteAllTextAsync(data);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            IsBusy = false;
        }

        public async Task RecordSaveSuccessful(bool wassuccessful)
        {

            try
            {
                IsBusy = true;

                var rootFolder = FileSystem.Current.LocalStorage;

                //create the shoppingList file
                var file = await rootFolder.CreateFileAsync(Constants.SessionSavedFile, CreationCollisionOption.ReplaceExisting);

                var data = JsonConvert.SerializeObject(wassuccessful);

                await file.WriteAllTextAsync(data);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        public async Task<bool> CheckIfLastSaveSuccessful()
        {

            try
            {

                var rootFolder = FileSystem.Current.LocalStorage;
                var fileexists = await rootFolder.CheckExistsAsync(Constants.SessionSavedFile);

                if (fileexists == ExistenceCheckResult.FileExists)
                {
                    var file = await rootFolder.GetFileAsync(Constants.SessionSavedFile);
                    var data = await file.ReadAllTextAsync();
                    var kaitaia = JsonConvert.DeserializeObject<bool>(data);

                    if (kaitaia)
                    {
                        //file was saved successfully
                        Debug.WriteLine("file was saved successfully last time");
                        return true;
                    }
                    //try sync and save again
                    //file wasn't saved successfully last time
                    Debug.WriteLine("trying to sync file");
                    return false;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }


        //var sync = await CurrentViewModel.SaveAnyString(DateTime.Now.ToString("F"), Constants.SyncTime);
        public async Task<T> RetrieveFile<T>(string filename, bool deserialize = true)
        {


            try
            {
#if FIX
                var rootFolder = FileSystem.Current.LocalStorage;
                var fileexists = await rootFolder.CheckExistsAsync(filename);

                if (fileexists == ExistenceCheckResult.FileExists)
                {
                    //create the highscores file
                    var file = await rootFolder.GetFileAsync(filename);
                    //, CreationCollisionOption.ReplaceExisting);
                    var data = await file.ReadAllTextAsync();

                    if (!deserialize)
                    {
                        return (T) Convert.ChangeType(data, typeof(T));
                    }


                    var kaitaia = JsonConvert.DeserializeObject<T>(data);

                    return kaitaia;
                }

                return default(T);
#endif
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return default(T);
        }
        public async Task GetSavedFile()
        {

            try
            {
#if FIX
                var rootFolder = FileSystem.Current.LocalStorage;
                var fileexists = await rootFolder.CheckExistsAsync(Constants.AllProductsFileName);

                if (fileexists == ExistenceCheckResult.FileExists)
                {
                    //create the highscores file
                    var file = await rootFolder.GetFileAsync(Constants.AllProductsFileName);
                    //, CreationCollisionOption.ReplaceExisting);
                    var data = await file.ReadAllTextAsync();
                    //var kaitaia = JsonConvert.DeserializeObject<KaitaiaData>(data);
                    
                }
                else
                {
                    Debug.WriteLine("Nothing to retrieve");
                }
#endif
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task<List<BudgetData>> GetSavedWatchlist()
        {

            try
            {

                var rootFolder = FileSystem.Current.LocalStorage;
                var fileexists = await rootFolder.CheckExistsAsync(Constants.WatchListFileName);

                if (fileexists == ExistenceCheckResult.FileExists)
                {
                    var file = await rootFolder.GetFileAsync(Constants.WatchListFileName);
                    var data = await file.ReadAllTextAsync();
                    var kaitaia = JsonConvert.DeserializeObject<List<BudgetData>>(data);

                    if (kaitaia != null && kaitaia.Any())
                    {
                        return kaitaia;
                    }
                }
                else
                {
                    Debug.WriteLine("Nothing to retrieve");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new List<BudgetData>();
        }


        public T DeserializeDataBase<T>(T tt, string serializedData)
        {


            try
            {
                var dataToSerialize =
                    JsonConvert.DeserializeObject<T>(serializedData);
                tt = dataToSerialize;
                return tt;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return tt;

        }
        public BudgetData ConvertToAssessment(string categoryItem)
        {

            try
            {
                if (string.IsNullOrEmpty(categoryItem)) return new BudgetData();
                var de = DeserializeDataBase(new BudgetData(), categoryItem);
                return de;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new BudgetData();
        }
        

        public string SerializeDataBase<T>(T tt)
        {
            try
            {
                var dataToSerialize =
                    JsonConvert.SerializeObject(tt);

                return dataToSerialize;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return String.Empty;
        }






















    }
}
