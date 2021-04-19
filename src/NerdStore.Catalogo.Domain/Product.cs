using NerdStore.Core.DomainObjects;
using System;

namespace NerdStore.Catalog.Domain
{
    public class Product : Entity, IAggregateRoot
    {
        public Product(string name, string description, bool active, decimal value, Guid categoryId, DateTime registerDate, string image, Dimensions dimensions)
        {
            CategoryId = categoryId;
            Name = name;
            Description = description;
            Active = active;
            Value = value;
            RegisterDate = registerDate;
            Image = image;
            Dimensions = dimensions;

            Validate();
        }

        public Guid CategoryId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Active { get; private set; }

        public decimal Value { get; private set; }
        public DateTime RegisterDate { get; private set; }
        public string Image { get; private set; }
        public int InventoryAmount{ get; private set; }
        public Dimensions Dimensions { get; private set; }
        public Category Category { get; private set; }

        public void Actice() => Active = true;
        public void Deactive() => Active = false;

        public void ChangeCategory(Category category)
        {
            Category = category;
            CategoryId = category.Id;
        }

        public void ChangeDescription(string description)
        {
            AssertionConcern.AssertArgumentNotEmpty(description, "O campo Descricao do produto não pode estar vazio");
            Description = description;
        }

        public void DecreaseInventory(int amount)
        {
            if (amount < 0) amount *= -1;
            if (!HasInventory(amount)) throw new DomainException("Estoque insuficiente");
            InventoryAmount -= amount;
        }

        public void IncreaseInventory(int amount)
        {
            InventoryAmount += amount;
        }

        public bool HasInventory(int amount)
        {
            return InventoryAmount >= amount;
        }

        public void Validate()
        {
            AssertionConcern.AssertArgumentNotEmpty(Name, "O campo Nome do produto não pode estar vazio");
            AssertionConcern.AssertArgumentNotEmpty(Description, "O campo Descricao do produto não pode estar vazio");
            AssertionConcern.AssertArgumentEquals(CategoryId, Guid.Empty, "O campo CategoriaId do produto não pode estar vazio");
            AssertionConcern.AssertArgumentLowerThan(Value, 1, "O campo Valor do produto não pode se menor igual a 0");
            AssertionConcern.AssertArgumentNotEmpty(Image, "O campo Imagem do produto não pode estar vazio");
        }
    }
}
