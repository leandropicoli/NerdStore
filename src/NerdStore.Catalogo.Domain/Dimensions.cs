using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Domain
{
    public class Dimensions
    {
        public Dimensions(decimal height, decimal width, decimal depth)
        {
            AssertionConcern.AssertArgumentLowerThan(height, 1, "O campo Altura não pode ser igual ou menor que 0");
            AssertionConcern.AssertArgumentLowerThan(width, 1, "O campo Largura não pode ser igual ou menor que 0");
            AssertionConcern.AssertArgumentLowerThan(depth, 1, "O campo Profundidade não pode ser igual ou menor que 0");

            Height = height;
            Width = width;
            Depth = depth;
        }

        public decimal Height { get; private set; }

        public decimal Width { get; private set; }

        public decimal Depth { get; private set; }

        public string FormatedDescription()
        {
            return $"LxAxP: {Width} x {Height} x {Depth}";
        }
    }
}
