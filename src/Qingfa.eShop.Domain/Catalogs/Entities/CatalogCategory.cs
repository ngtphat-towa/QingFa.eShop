using QingFa.EShop.Domain.Catalogs.ValueObjects;
using QingFa.EShop.Domain.DomainModels.Exceptions;

using System;
using System.Collections.Generic;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    /// <summary>
    /// Represents a category in the catalog.
    /// </summary>
    public class CatalogCategory : Entity<CatalogCategoryId>
    {
        /// <summary>
        /// Gets the name of the category.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the description of the category.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the list of sub-categories under this category.
        /// </summary>
        public IReadOnlyCollection<CatalogSubCategory> SubCategories => _subCategories.AsReadOnly();
        private readonly List<CatalogSubCategory> _subCategories;

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogCategory"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the category.</param>
        /// <param name="name">The name of the category.</param>
        /// <param name="description">The description of the category.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="name"/> or <paramref name="description"/> is null or whitespace.</exception>
        public CatalogCategory(CatalogCategoryId id, string name, string description)
            : base(id)
        {
            if (string.IsNullOrWhiteSpace(name)) throw CoreException.NullOrEmptyArgument(nameof(name));
            if (string.IsNullOrWhiteSpace(description)) throw CoreException.NullOrEmptyArgument(nameof(description));

            Name = name;
            Description = description;
            _subCategories = new List<CatalogSubCategory>();
        }

        /// <summary>
        /// Adds a sub-category to this category.
        /// </summary>
        /// <param name="subCategory">The sub-category to add.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="subCategory"/> is null.</exception>
        public void AddSubCategory(CatalogSubCategory subCategory)
        {
            if (subCategory == null) throw new ArgumentNullException(nameof(subCategory));
            _subCategories.Add(subCategory);
        }

        /// <summary>
        /// Updates the name of the category.
        /// </summary>
        /// <param name="name">The new name of the category.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> is null or whitespace.</exception>
        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw CoreException.NullOrEmptyArgument(nameof(name));
            Name = name;
        }

        /// <summary>
        /// Updates the description of the category.
        /// </summary>
        /// <param name="description">The new description of the category.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="description"/> is null or whitespace.</exception>
        public void UpdateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description)) throw CoreException.NullOrEmptyArgument(nameof(description));
            Description = description;
        }
    }
}
