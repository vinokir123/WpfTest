using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearWpf.ViewModels.Base
{
    public abstract class TittledViewModel : ViewModel
    {
        #region Tittle : string - Заголовок

        /// <summary>Заголовок</summary>
        private string _Tittle;

        /// <summary>Заголовок</summary>
        public string Tittle { get => _Tittle; set => Set(ref _Tittle, value); }

        #endregion
    }
}
