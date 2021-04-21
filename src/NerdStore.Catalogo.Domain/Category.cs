using NerdStore.Core.DomainObjects;
using System.Collections.Generic;

namespace NerdStore.Catalog.Domain
{
    public class Category : Entity
    {
        public Category(string name, int code)
        {
            Name = name;
            Code = code;

            Validate();
        }

        public string Name { get; private set; }
        public int Code { get; private set; }
        public ICollection<Product> Products { get; set; }
        protected Category() { }
        public override string ToString()
        {
            return $"{Name} - {Code}";
        }

        public void Validate()
        {
            AssertionConcern.AssertArgumentNotEmpty(Name, "O campo Nome da categoria não pode estar vazio");
            AssertionConcern.AssertArgumentEquals(Code, 0, "O campo Codigo não pode ser 0");
        }
    }
}
