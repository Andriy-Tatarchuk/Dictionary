using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewLayer.Models
{
    public enum Command
    {
        None,
        Save,
        Cancel,
        AddEditWord,
        AddEditDic,
        AddWordToDic,
        AddDicToWord,
        Delete
    } 
}
