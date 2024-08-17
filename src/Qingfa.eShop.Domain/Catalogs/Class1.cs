//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace YourNamespace
//{
//    public class Product
//    {
//        [Key]
//        public int ProductID { get; set; }

//        [Required]
//        [StringLength(255)]
//        public string Name { get; set; }

//        public string Description { get; set; }

//        [ForeignKey("Category")]
//        public int CategoryID { get; set; }
//        public Category Category { get; set; }

//        [ForeignKey("Brand")]
//        public int BrandID { get; set; }
//        public Brand Brand { get; set; }

//        [Column(TypeName = "decimal(18,2)")]
//        public decimal Price { get; set; }

//        [StringLength(100)]
//        public string SKU { get; set; }

//        public int StockLevel { get; set; }

//        public ProductStatus Status { get; set; }

//        public DateTime CreatedAt { get; set; }
//        public DateTime UpdatedAt { get; set; }

//        public ICollection<ProductVariant> ProductVariants { get; set; }
//        public ICollection<ProductAttributeValue> ProductAttributeValues { get; set; }
//        public ICollection<ProductPromotion> ProductPromotions { get; set; }
//    }

//    public class ProductVariant
//    {
//        [Key]
//        public int VariantID { get; set; }

//        [ForeignKey("Product")]
//        public int ProductID { get; set; }
//        public Product Product { get; set; }

//        [StringLength(100)]
//        public string SKU { get; set; }

//        [Column(TypeName = "decimal(18,2)")]
//        public decimal Price { get; set; }

//        public int StockLevel { get; set; }

//        public ProductStatus Status { get; set; }

//        public DateTime CreatedAt { get; set; }
//        public DateTime UpdatedAt { get; set; }

//        public ICollection<ProductAttributeValue> ProductAttributeValues { get; set; }
//        public ICollection<VariantAttributeGroup> VariantAttributeGroups { get; set; }
//        public ICollection<VariantAttribute> VariantAttributes { get; set; }
//        public ICollection<InventoryTracking> InventoryTrackings { get; set; }
//    }

//    public class Attribute
//    {
//        [Key]
//        public int AttributeID { get; set; }

//        [Required]
//        [StringLength(255)]
//        public string Name { get; set; }

//        [StringLength(100)]
//        public string AttributeCode { get; set; }

//        public AttributeType Type { get; set; }
//        public bool IsRequired { get; set; }
//        public bool IsFilterable { get; set; }
//        public bool ShowToCustomers { get; set; }
//        public int SortOrder { get; set; }

//        public ICollection<AttributeOption> AttributeOptions { get; set; }
//        public ICollection<ProductAttributeValue> ProductAttributeValues { get; set; }
//        public ICollection<VariantAttribute> VariantAttributes { get; set; }
//    }

//    public class AttributeOption
//    {
//        [Key]
//        public int OptionID { get; set; }

//        [ForeignKey("Attribute")]
//        public int AttributeID { get; set; }
//        public Attribute Attribute { get; set; }

//        [Required]
//        [StringLength(255)]
//        public string OptionValue { get; set; }

//        public string Description { get; set; }

//        public bool IsDefault { get; set; }
//        public int SortOrder { get; set; }
//    }

//    public class ProductAttributeValue
//    {
//        [Key]
//        public int ProductAttributeValueID { get; set; }

//        [ForeignKey("Product")]
//        public int ProductID { get; set; }
//        public Product Product { get; set; }

//        [ForeignKey("Variant")]
//        public int VariantID { get; set; }
//        public ProductVariant Variant { get; set; }

//        [ForeignKey("Attribute")]
//        public int AttributeID { get; set; }
//        public Attribute Attribute { get; set; }

//        [ForeignKey("Option")]
//        public int OptionID { get; set; }
//        public AttributeOption Option { get; set; }

//        public string Value { get; set; }
//        public bool IsVisibleToCustomer { get; set; }
//    }

//    public class Category
//    {
//        [Key]
//        public int CategoryID { get; set; }

//        [Required]
//        [StringLength(255)]
//        public string Name { get; set; }

//        public string Description { get; set; }

//        [ForeignKey("ParentCategory")]
//        public int? ParentCategoryID { get; set; }
//        public Category ParentCategory { get; set; }

//        public int SortOrder { get; set; }

//        public ICollection<Product> Products { get; set; }
//    }

//    public class Brand
//    {
//        [Key]
//        public int BrandID { get; set; }

//        [Required]
//        [StringLength(255)]
//        public string Name { get; set; }

//        public string Description { get; set; }

//        public ICollection<Product> Products { get; set; }
//    }

//    public class Order
//    {
//        [Key]
//        public int OrderID { get; set; }

//        [ForeignKey("Customer")]
//        public int CustomerID { get; set; }
//        public Customer Customer { get; set; }

//        public DateTime OrderDate { get; set; }

//        [Column(TypeName = "decimal(18,2)")]
//        public decimal TotalAmount { get; set; }

//        public OrderStatus Status { get; set; }

//        [ForeignKey("ShippingAddress")]
//        public int ShippingAddressID { get; set; }
//        public Address ShippingAddress { get; set; }

//        [ForeignKey("ShippingMethod")]
//        public int ShippingMethodID { get; set; }
//        public ShippingMethod ShippingMethod { get; set; }

//        [ForeignKey("PaymentMethod")]
//        public int PaymentMethodID { get; set; }
//        public PaymentMethod PaymentMethod { get; set; }

//        public ICollection<OrderItem> OrderItems { get; set; }
//        public ICollection<ShippingTracking> ShippingTrackings { get; set; }
//    }

//    public class OrderItem
//    {
//        [Key]
//        public int OrderItemID { get; set; }

//        [ForeignKey("Order")]
//        public int OrderID { get; set; }
//        public Order Order { get; set; }

//        [ForeignKey("Product")]
//        public int ProductID { get; set; }
//        public Product Product { get; set; }

//        [ForeignKey("Variant")]
//        public int VariantID { get; set; }
//        public ProductVariant Variant { get; set; }

//        public int Quantity { get; set; }

//        [Column(TypeName = "decimal(18,2)")]
//        public decimal Price { get; set; }
//    }

//    public class Address
//    {
//        [Key]
//        public int AddressID { get; set; }

//        [ForeignKey("Customer")]
//        public int CustomerID { get; set; }
//        public Customer Customer { get; set; }

//        [Required]
//        [StringLength(255)]
//        public string AddressLine1 { get; set; }

//        [StringLength(255)]
//        public string AddressLine2 { get; set; }

//        [Required]
//        [StringLength(100)]
//        public string City { get; set; }

//        [StringLength(100)]
//        public string State { get; set; }

//        [Required]
//        [StringLength(20)]
//        public string PostalCode { get; set; }

//        [Required]
//        [StringLength(100)]
//        public string Country { get; set; }
//    }

//    public class Customer
//    {
//        [Key]
//        public int CustomerID { get; set; }

//        [Required]
//        [StringLength(255)]
//        public string Name { get; set; }

//        [Required]
//        [StringLength(255)]
//        public string Email { get; set; }

//        [StringLength(20)]
//        public string Phone { get; set; }

//        public DateTime CreatedAt { get; set; }
//        public DateTime UpdatedAt { get; set; }

//        public ICollection<Order> Orders { get; set; }
//        public ICollection<Address> Addresses { get; set; }
//        public ICollection<Review> Reviews { get; set; }
//        public ICollection<Wishlist> Wishlists { get; set; }
//        public ICollection<Notification> Notifications { get; set; }
//        public ICollection<Cart> Carts { get; set; }
//    }

//    public class ShippingTracking
//    {
//        [Key]
//        public int TrackingID { get; set; }

//        [ForeignKey("Order")]
//        public int OrderID { get; set; }
//        public Order Order { get; set; }

//        [Required]
//        [StringLength(255)]
//        public string TrackingNumber { get; set; }

//        [Required]
//        [StringLength(255)]
//        public string Carrier { get; set; }

//        [StringLength(255)]
//        public string Status { get; set; }

//        public DateTime EstimatedDeliveryDate { get; set; }
//    }

//    public class InventoryTracking
//    {
//        [Key]
//        public int InventoryTrackingID { get; set; }

//        [ForeignKey("Product")]
//        public int ProductID { get; set; }
//        public Product Product { get; set; }

//        [ForeignKey("Variant")]
//        public int VariantID { get; set; }
//        public ProductVariant Variant { get; set; }

//        public DateTime ChangeDate { get; set; }
//        public ChangeType ChangeType { get; set; }
//        public int QuantityChange { get; set; }
//    }

//    public class SalesReport
//    {
//        [Key]
//        public int ReportID { get; set; }

//        public DateTime ReportDate { get; set; }

//        [Column(TypeName = "decimal(18,2)")]
//        public decimal TotalSales { get; set; }
//        public int TotalOrders { get; set; }
//        public int TotalProductsSold { get; set; }
//    }

//    public class Log
//    {
//        [Key]
//        public int LogID { get; set; }

//        public LogType LogType { get; set; }
//        public string Message { get; set; }
//        public DateTime Timestamp { get; set; }
//    }

//    public class AuditTable
//    {
//        [Key]
//        public int AuditID { get; set; }

//        [Required]
//        [StringLength(255)]
//        public string TableName { get; set; }

//        public int RecordID { get; set; }
//        public ActionType ActionType { get; set; }

//        [ForeignKey("User")]
//        public int ChangedBy { get; set; }
//        public User User { get; set; }

//        public DateTime ChangeDate { get; set; }

//        public string OldValue { get; set; }
//        public string NewValue { get; set; }
//    }

//    public class User
//    {
//        [Key]
//        public int UserID { get; set; }

//        [Required]
//        [StringLength(255)]
//        public string Username { get; set; }

//        [Required]
//        [StringLength(255)]
//        public string PasswordHash { get; set; }

//        [StringLength(255)]
//        public string Email { get; set; }

//        public UserRole Role { get; set; }

//        public DateTime CreatedAt { get; set; }
//        public DateTime UpdatedAt { get; set; }
//    }

//    public class Promotion
//    {
//        [Key]
//        public int PromotionID { get; set; }

//        [Required]
//        [StringLength(100)]
//        public string Code { get; set; }

//        public string Description { get; set; }

//        [Column(TypeName = "decimal(18,2)")]
//        public decimal DiscountAmount { get; set; }

//        [Column(TypeName = "decimal(5,2)")]
//        public decimal DiscountPercentage { get; set; }

//        public DateTime StartDate { get; set; }
//        public DateTime EndDate { get; set; }

//        public PromotionStatus Status { get; set; }

//        public ICollection<ProductPromotion> ProductPromotions { get; set; }
//    }

//    public class ProductPromotion
//    {
//        [Key]
//        public int ProductPromotionID { get; set; }

//        [ForeignKey("Product")]
//        public int ProductID { get; set; }
//        public Product Product { get; set; }

//        [ForeignKey("Promotion")]
//        public int PromotionID { get; set; }
//        public Promotion Promotion { get; set; }
//    }

//    public class VariantAttributeGroup
//    {
//        [Key]
//        public int GroupID { get; set; }

//        [Required]
//        [StringLength(255)]
//        public string GroupName { get; set; }

//        public ICollection<ProductVariant> ProductVariants { get; set; }
//        public ICollection<VariantAttribute> VariantAttributes { get; set; }
//    }

//    public class VariantAttribute
//    {
//        [Key]
//        public int VariantAttributeID { get; set; }

//        [ForeignKey("Group")]
//        public int GroupID { get; set; }
//        public VariantAttributeGroup Group { get; set; }

//        [ForeignKey("Attribute")]
//        public int AttributeID { get; set; }
//        public Attribute Attribute { get; set; }

//        [Required]
//        [StringLength(255)]
//        public string Value { get; set; }

//        public bool IsRequired { get; set; }
//        public bool IsVisibleToCustomer { get; set; }
//    }

//    public class Review
//    {
//        [Key]
//        public int ReviewID { get; set; }

//        [ForeignKey("Product")]
//        public int ProductID { get; set; }
//        public Product Product { get; set; }

//        [ForeignKey("Customer")]
//        public int CustomerID { get; set; }
//        public Customer Customer { get; set; }

//        public int Rating { get; set; }
//        public string Comment { get; set; }
//        public DateTime ReviewDate { get; set; }
//    }

//    public class Wishlist
//    {
//        [Key]
//        public int WishlistID { get; set; }

//        [ForeignKey("Customer")]
//        public int CustomerID { get; set; }
//        public Customer Customer { get; set; }

//        [ForeignKey("Product")]
//        public int ProductID { get; set; }
//        public Product Product { get; set; }

//        public DateTime AddedDate { get; set; }
//    }

//    public class Cart
//    {
//        [Key]
//        public int CartID { get; set; }

//        [ForeignKey("Customer")]
//        public int CustomerID { get; set; }
//        public Customer Customer { get; set; }

//        public DateTime CreatedDate { get; set; }
//        public DateTime UpdatedDate { get; set; }

//        public ICollection<CartItem> CartItems { get; set; }
//    }

//    public class CartItem
//    {
//        [Key]
//        public int CartItemID { get; set; }

//        [ForeignKey("Cart")]
//        public int CartID { get; set; }
//        public Cart Cart { get; set; }

//        [ForeignKey("Product")]
//        public int ProductID { get; set; }
//        public Product Product { get; set; }

//        [ForeignKey("Variant")]
//        public int VariantID { get; set; }
//        public ProductVariant Variant { get; set; }

//        public int Quantity { get; set; }
//    }

//    public class Coupon
//    {
//        [Key]
//        public int CouponID { get; set; }

//        [Required]
//        [StringLength(100)]
//        public string Code { get; set; }

//        public string Description { get; set; }

//        [Column(TypeName = "decimal(18,2)")]
//        public decimal DiscountAmount { get; set; }

//        [Column(TypeName = "decimal(5,2)")]
//        public decimal DiscountPercentage { get; set; }

//        public DateTime StartDate { get; set; }
//        public DateTime EndDate { get; set; }

//        public int UsageLimit { get; set; }

//        public ICollection<CouponUsage> CouponUsages { get; set; }
//    }

//    public class CouponUsage
//    {
//        [Key]
//        public int CouponUsageID { get; set; }

//        [ForeignKey("Coupon")]
//        public int CouponID { get; set; }
//        public Coupon Coupon { get; set; }

//        [ForeignKey("Customer")]
//        public int CustomerID { get; set; }
//        public Customer Customer { get; set; }

//        public DateTime UsageDate { get; set; }
//    }

//    public class Return
//    {
//        [Key]
//        public int ReturnID { get; set; }

//        [ForeignKey("Order")]
//        public int OrderID { get; set; }
//        public Order Order { get; set; }

//        [ForeignKey("Product")]
//        public int ProductID { get; set; }
//        public Product Product { get; set; }

//        [ForeignKey("Variant")]
//        public int VariantID { get; set; }
//        public ProductVariant Variant { get; set; }

//        public DateTime ReturnDate { get; set; }
//        public string Reason { get; set; }
//        public ReturnStatus Status { get; set; }

//        public ICollection<ReturnItem> ReturnItems { get; set; }
//    }

//    public class ReturnItem
//    {
//        [Key]
//        public int ReturnItemID { get; set; }

//        [ForeignKey("Return")]
//        public int ReturnID { get; set; }
//        public Return Return { get; set; }

//        [ForeignKey("OrderItem")]
//        public int OrderItemID { get; set; }
//        public OrderItem OrderItem { get; set; }

//        public int Quantity { get; set; }
//    }

//    public class Supplier
//    {
//        [Key]
//        public int SupplierID { get; set; }

//        [Required]
//        [StringLength(255)]
//        public string Name { get; set; }

//        public string ContactName { get; set; }

//        [StringLength(255)]
//        public string ContactEmail { get; set; }

//        [StringLength(20)]
//        public string ContactPhone { get; set; }

//        public string Address { get; set; }

//        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
//    }

//    public class PurchaseOrder
//    {
//        [Key]
//        public int PurchaseOrderID { get; set; }

//        [ForeignKey("Supplier")]
//        public int SupplierID { get; set; }
//        public Supplier Supplier { get; set; }

//        public DateTime OrderDate { get; set; }
//        public PurchaseOrderStatus Status { get; set; }

//        public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
//    }

//    public class PurchaseOrderItem
//    {
//        [Key]
//        public int PurchaseOrderItemID { get; set; }

//        [ForeignKey("PurchaseOrder")]
//        public int PurchaseOrderID { get; set; }
//        public PurchaseOrder PurchaseOrder { get; set; }

//        [ForeignKey("Product")]
//        public int ProductID { get; set; }
//        public Product Product { get; set; }

//        [ForeignKey("Variant")]
//        public int VariantID { get; set; }
//        public ProductVariant Variant { get; set; }

//        public int QuantityOrdered { get; set; }
//        public int QuantityReceived { get; set; }
//    }

//    public class Notification
//    {
//        [Key]
//        public int NotificationID { get; set; }

//        [ForeignKey("Customer")]
//        public int CustomerID { get; set; }
//        public Customer Customer { get; set; }

//        public string Message { get; set; }
//        public DateTime NotificationDate { get; set; }
//        public NotificationStatus Status { get; set; }
//    }

//    public class Setting
//    {
//        [Key]
//        public int SettingID { get; set; }

//        [Required]
//        [StringLength(255)]
//        public string SettingKey { get; set; }

//        public string SettingValue { get; set; }
//    }

//    public class TermsAndConditions
//    {
//        [Key]
//        public int TermsID { get; set; }

//        public string Content { get; set; }
//        public DateTime LastUpdated { get; set; }
//    }

//    public class PrivacyPolicy
//    {
//        [Key]
//        public int PrivacyPolicyID { get; set; }

//        public string Content { get; set; }
//        public DateTime LastUpdated { get; set; }
//    }

//    public class Refund
//    {
//        [Key]
//        public int RefundID { get; set; }

//        [ForeignKey("Order")]
//        public int OrderID { get; set; }
//        public Order Order { get; set; }

//        [Column(TypeName = "decimal(18,2)")]
//        public decimal Amount { get; set; }

//        public DateTime RefundDate { get; set; }
//        public string Reason { get; set; }
//        public RefundStatus Status { get; set; }
//    }

//    public class ShippingMethod
//    {
//        [Key]
//        public int ShippingMethodID { get; set; }

//        [Required]
//        [StringLength(255)]
//        public string Name { get; set; }

//        [Column(TypeName = "decimal(18,2)")]
//        public decimal Cost { get; set; }

//        [StringLength(255)]
//        public string DeliveryTime { get; set; }
//    }

//    public class PaymentMethod
//    {
//        [Key]
//        public int PaymentMethodID { get; set; }

//        [Required]
//        [StringLength(255)]
//        public string Name { get; set; }

//        public string Description { get; set; }
//    }

//    public class Transaction
//    {
//        [Key]
//        public int TransactionID { get; set; }

//        [ForeignKey("Order")]
//        public int OrderID { get; set; }
//        public Order Order { get; set; }

//        [ForeignKey("PaymentMethod")]
//        public int PaymentMethodID { get; set; }
//        public PaymentMethod PaymentMethod { get; set; }

//        [Column(TypeName = "decimal(18,2)")]
//        public decimal Amount { get; set; }

//        public DateTime TransactionDate { get; set; }
//        public TransactionStatus Status { get; set; }
//    }

//    // Enums
//    public enum ProductStatus
//    {
//        Active,
//        Inactive,
//        Discontinued
//    }

//    public enum AttributeType
//    {
//        Text,
//        Number,
//        Date,
//        Boolean,
//        Select
//    }

//    public enum ChangeType
//    {
//        Increase,
//        Decrease
//    }

//    public enum OrderStatus
//    {
//        Pending,
//        Processing,
//        Shipped,
//        Delivered,
//        Cancelled
//    }

//    public enum LogType
//    {
//        Info,
//        Warning,
//        Error
//    }

//    public enum ActionType
//    {
//        Create,
//        Update,
//        Delete
//    }

//    public enum UserRole
//    {
//        Admin,
//        User,
//        Guest
//    }

//    public enum PromotionStatus
//    {
//        Active,
//        Inactive,
//        Expired
//    }

//    public enum ReturnStatus
//    {
//        Requested,
//        Approved,
//        Rejected,
//        Completed
//    }

//    public enum PurchaseOrderStatus
//    {
//        Pending,
//        Approved,
//        Rejected,
//        Completed
//    }

//    public enum NotificationStatus
//    {
//        Sent,
//        Delivered,
//        Failed
//    }

//    public enum RefundStatus
//    {
//        Requested,
//        Approved,
//        Rejected,
//        Completed
//    }

//    public enum TransactionStatus
//    {
//        Pending,
//        Completed,
//        Failed
//    }
//}
