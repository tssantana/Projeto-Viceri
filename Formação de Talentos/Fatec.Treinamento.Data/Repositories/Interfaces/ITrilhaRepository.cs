﻿using Fatec.Treinamento.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fatec.Treinamento.Data.Repositories.Interfaces
{
    public interface ITrilhaRepository : 
        ICrudRepository<Trilha>
    {
        void AtualizarCursos(int idTrilha, int[] cursos);
    }
}
