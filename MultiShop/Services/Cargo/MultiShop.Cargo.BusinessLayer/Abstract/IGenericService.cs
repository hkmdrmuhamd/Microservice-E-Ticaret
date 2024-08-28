using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Abstract
{
    public interface IGenericService<T> where T : class
    {
        void TInsert(T entity); //Başına T koymamızın sebebi Business katmanından geldiğini belirtmek içindir. Başında T olmayan Data Access katmanından gelendir.
        void TUpdate(T entity);
        void TDelete(int id);
        T TGetById(int id);
        List<T> TGetAll();
    }
}
