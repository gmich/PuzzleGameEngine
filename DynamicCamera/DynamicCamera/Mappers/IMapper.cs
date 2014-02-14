using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicCamera.Mappers
{
    interface IMapper
    {
        void Load(object entity);

        void Save(object entity);
    }
}
