namespace ClearWpf.ViewModels.Base
{
    public abstract class TittledViewModel : ViewModel
    {
        #region Tittle : string - Заголовок

        /// <summary>Заголовок</summary>
        private string _Title;

        /// <summary>Заголовок</summary>
        public string VmTitle { get => _Title; set => Set(ref _Title, value); }

        #endregion
    }
}
