using System;
using System.Collections.Generic;
using DIO.SERIES.Interfaces;

namespace DIO.SERIES
{
    public class SerieRepositorio : IRepositorio<Serie>
    {
        private List<Serie> listaSerie = new List<Serie>();
        public void Atualiza(int id, Serie objeto, out bool status)
        {
            if(listaSerie.Count>id)
            {
                listaSerie[id]=objeto;
                status = true;
            }
            else
                status = false;
        }

        public void Exclui(int id, out bool status)
        {
             if(listaSerie.Count>id)
            {
                listaSerie[id].Excluir();
                status = true;
            }
            else
                status = false;
        }

        public void Insere(Serie objeto)
        {
            listaSerie.Add(objeto);
        }

        public List<Serie> Lista()
        {
            return listaSerie;
        }

        public int ProximoId()
        {
            return listaSerie.Count;
        }

        public Serie RetornaPorId(int id)
        {
            if(listaSerie.Count>id)
                return listaSerie[id];
            else
                return null;
        }
    }
}