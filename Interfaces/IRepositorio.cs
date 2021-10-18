using System.Collections.Generic;

namespace DIO.SERIES.Interfaces
{
    public interface IRepositorio<T>
    {
        List<T> Lista();
        T RetornaPorId(int id);
        void Insere(T entidade);
        void Exclui(int id, out bool status);
        void Atualiza(int id, T entidade, out bool status);
        int ProximoId();
         
    }
}