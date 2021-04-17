using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Domain
{
    public class Product : Entity, IAggregateRoot
    {
        public Product(string name, string description, bool active, decimal value, DateTime registerDate, string image, Guid categoryId)
        {
            CategoryId = categoryId;
            Name = name;
            Description = description;
            Active = active;
            Value = value;
            RegisterDate = registerDate;
            Image = image;
        }

        public Guid CategoryId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Active { get; private set; }

        public decimal Value { get; private set; }
        public DateTime RegisterDate { get; private set; }
        public string Image { get; private set; }
        public int InventoryAmount{ get; private set; }
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
            Description = description;
        }

        public void DecreaseInventory(int amount)
        {
            if (amount < 0) amount *= -1;
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

        }
    }

    public class Category : Entity
    {
        public Category(string name, int code)
        {
            Name = name;
            Code = code;
        }

        public string Name { get; private set; }
        public int Code { get; private set; }

        public override string ToString()
        {
            return $"{Name} - {Code}";
        }
    }
}
