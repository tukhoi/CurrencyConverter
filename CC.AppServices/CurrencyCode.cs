using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CC.AppServices
{
    public class CurrencyCode : Dictionary<string, string>
    {
        static CurrencyCode _instance;

        public static CurrencyCode GetInstance()
        {
            if (_instance == null)
                _instance = new CurrencyCode();

            return _instance;
        }

        private CurrencyCode()
        {
            Initialize();
        }

        private void Initialize() 
        {
            using (var fileStream = new FileStream("Code.txt", FileMode.Open, FileAccess.Read))
            {
                var reader = new StreamReader(fileStream);
                while (!reader.EndOfStream)
                {
                    var code = reader.ReadLine();
                    var codeName = code.Split(new string[]{"\t"}, StringSplitOptions.RemoveEmptyEntries);
                    if (codeName.Length > 1)
                        this.Add(codeName[0], codeName[1]);
                }
            }
        }
    }
}
