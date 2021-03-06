﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Blog.Data
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="eouw0o83hf")]
	public partial class BlogDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertBlog(Blog instance);
    partial void UpdateBlog(Blog instance);
    partial void DeleteBlog(Blog instance);
    partial void InsertUserPermission(UserPermission instance);
    partial void UpdateUserPermission(UserPermission instance);
    partial void DeleteUserPermission(UserPermission instance);
    partial void InsertIdentityProvider(IdentityProvider instance);
    partial void UpdateIdentityProvider(IdentityProvider instance);
    partial void DeleteIdentityProvider(IdentityProvider instance);
    partial void InsertPermission(Permission instance);
    partial void UpdatePermission(Permission instance);
    partial void DeletePermission(Permission instance);
    partial void InsertEmailVerification(EmailVerification instance);
    partial void UpdateEmailVerification(EmailVerification instance);
    partial void DeleteEmailVerification(EmailVerification instance);
    partial void InsertPost(Post instance);
    partial void UpdatePost(Post instance);
    partial void DeletePost(Post instance);
    partial void InsertUser(User instance);
    partial void UpdateUser(User instance);
    partial void DeleteUser(User instance);
    #endregion
		
		public BlogDataContext() : 
				base(global::Blog.Data.Properties.Settings.Default.eouw0o83hfConnectionString1, mappingSource)
		{
			OnCreated();
		}
		
		public BlogDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BlogDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BlogDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BlogDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Blog> Blogs
		{
			get
			{
				return this.GetTable<Blog>();
			}
		}
		
		public System.Data.Linq.Table<UserPermission> UserPermissions
		{
			get
			{
				return this.GetTable<UserPermission>();
			}
		}
		
		public System.Data.Linq.Table<IdentityProvider> IdentityProviders
		{
			get
			{
				return this.GetTable<IdentityProvider>();
			}
		}
		
		public System.Data.Linq.Table<Permission> Permissions
		{
			get
			{
				return this.GetTable<Permission>();
			}
		}
		
		public System.Data.Linq.Table<EmailVerification> EmailVerifications
		{
			get
			{
				return this.GetTable<EmailVerification>();
			}
		}
		
		public System.Data.Linq.Table<Post> Posts
		{
			get
			{
				return this.GetTable<Post>();
			}
		}
		
		public System.Data.Linq.Table<User> Users
		{
			get
			{
				return this.GetTable<User>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Blogs")]
	public partial class Blog : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _BlogId;
		
		private string _UrlName;
		
		private string _DisplayName;
		
		private string _Description;
		
		private EntitySet<Post> _Posts;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnBlogIdChanging(int value);
    partial void OnBlogIdChanged();
    partial void OnUrlNameChanging(string value);
    partial void OnUrlNameChanged();
    partial void OnDisplayNameChanging(string value);
    partial void OnDisplayNameChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    #endregion
		
		public Blog()
		{
			this._Posts = new EntitySet<Post>(new Action<Post>(this.attach_Posts), new Action<Post>(this.detach_Posts));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BlogId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int BlogId
		{
			get
			{
				return this._BlogId;
			}
			set
			{
				if ((this._BlogId != value))
				{
					this.OnBlogIdChanging(value);
					this.SendPropertyChanging();
					this._BlogId = value;
					this.SendPropertyChanged("BlogId");
					this.OnBlogIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UrlName", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string UrlName
		{
			get
			{
				return this._UrlName;
			}
			set
			{
				if ((this._UrlName != value))
				{
					this.OnUrlNameChanging(value);
					this.SendPropertyChanging();
					this._UrlName = value;
					this.SendPropertyChanged("UrlName");
					this.OnUrlNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DisplayName", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string DisplayName
		{
			get
			{
				return this._DisplayName;
			}
			set
			{
				if ((this._DisplayName != value))
				{
					this.OnDisplayNameChanging(value);
					this.SendPropertyChanging();
					this._DisplayName = value;
					this.SendPropertyChanged("DisplayName");
					this.OnDisplayNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Blog_Post", Storage="_Posts", ThisKey="BlogId", OtherKey="BlogId")]
		public EntitySet<Post> Posts
		{
			get
			{
				return this._Posts;
			}
			set
			{
				this._Posts.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Posts(Post entity)
		{
			this.SendPropertyChanging();
			entity.Blog = this;
		}
		
		private void detach_Posts(Post entity)
		{
			this.SendPropertyChanging();
			entity.Blog = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.UserPermissions")]
	public partial class UserPermission : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _UserId;
		
		private int _PermissionId;
		
		private EntityRef<Permission> _Permission;
		
		private EntityRef<User> _User;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUserIdChanging(int value);
    partial void OnUserIdChanged();
    partial void OnPermissionIdChanging(int value);
    partial void OnPermissionIdChanged();
    #endregion
		
		public UserPermission()
		{
			this._Permission = default(EntityRef<Permission>);
			this._User = default(EntityRef<User>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					if (this._User.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PermissionId", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int PermissionId
		{
			get
			{
				return this._PermissionId;
			}
			set
			{
				if ((this._PermissionId != value))
				{
					if (this._Permission.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnPermissionIdChanging(value);
					this.SendPropertyChanging();
					this._PermissionId = value;
					this.SendPropertyChanged("PermissionId");
					this.OnPermissionIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Permission_UserPermission", Storage="_Permission", ThisKey="PermissionId", OtherKey="PermissionId", IsForeignKey=true)]
		public Permission Permission
		{
			get
			{
				return this._Permission.Entity;
			}
			set
			{
				Permission previousValue = this._Permission.Entity;
				if (((previousValue != value) 
							|| (this._Permission.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Permission.Entity = null;
						previousValue.UserPermissions.Remove(this);
					}
					this._Permission.Entity = value;
					if ((value != null))
					{
						value.UserPermissions.Add(this);
						this._PermissionId = value.PermissionId;
					}
					else
					{
						this._PermissionId = default(int);
					}
					this.SendPropertyChanged("Permission");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_UserPermission", Storage="_User", ThisKey="UserId", OtherKey="UserId", IsForeignKey=true)]
		public User User
		{
			get
			{
				return this._User.Entity;
			}
			set
			{
				User previousValue = this._User.Entity;
				if (((previousValue != value) 
							|| (this._User.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._User.Entity = null;
						previousValue.UserPermissions.Remove(this);
					}
					this._User.Entity = value;
					if ((value != null))
					{
						value.UserPermissions.Add(this);
						this._UserId = value.UserId;
					}
					else
					{
						this._UserId = default(int);
					}
					this.SendPropertyChanged("User");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.IdentityProviders")]
	public partial class IdentityProvider : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _IdentityProviderId;
		
		private string _Uri;
		
		private EntitySet<User> _Users;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdentityProviderIdChanging(int value);
    partial void OnIdentityProviderIdChanged();
    partial void OnUriChanging(string value);
    partial void OnUriChanged();
    #endregion
		
		public IdentityProvider()
		{
			this._Users = new EntitySet<User>(new Action<User>(this.attach_Users), new Action<User>(this.detach_Users));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdentityProviderId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int IdentityProviderId
		{
			get
			{
				return this._IdentityProviderId;
			}
			set
			{
				if ((this._IdentityProviderId != value))
				{
					this.OnIdentityProviderIdChanging(value);
					this.SendPropertyChanging();
					this._IdentityProviderId = value;
					this.SendPropertyChanged("IdentityProviderId");
					this.OnIdentityProviderIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Uri", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Uri
		{
			get
			{
				return this._Uri;
			}
			set
			{
				if ((this._Uri != value))
				{
					this.OnUriChanging(value);
					this.SendPropertyChanging();
					this._Uri = value;
					this.SendPropertyChanged("Uri");
					this.OnUriChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="IdentityProvider_User", Storage="_Users", ThisKey="IdentityProviderId", OtherKey="IdentityProviderId")]
		public EntitySet<User> Users
		{
			get
			{
				return this._Users;
			}
			set
			{
				this._Users.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Users(User entity)
		{
			this.SendPropertyChanging();
			entity.IdentityProvider = this;
		}
		
		private void detach_Users(User entity)
		{
			this.SendPropertyChanging();
			entity.IdentityProvider = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Permissions")]
	public partial class Permission : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _PermissionId;
		
		private string _Name;
		
		private EntitySet<UserPermission> _UserPermissions;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnPermissionIdChanging(int value);
    partial void OnPermissionIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    #endregion
		
		public Permission()
		{
			this._UserPermissions = new EntitySet<UserPermission>(new Action<UserPermission>(this.attach_UserPermissions), new Action<UserPermission>(this.detach_UserPermissions));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PermissionId", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int PermissionId
		{
			get
			{
				return this._PermissionId;
			}
			set
			{
				if ((this._PermissionId != value))
				{
					this.OnPermissionIdChanging(value);
					this.SendPropertyChanging();
					this._PermissionId = value;
					this.SendPropertyChanged("PermissionId");
					this.OnPermissionIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Permission_UserPermission", Storage="_UserPermissions", ThisKey="PermissionId", OtherKey="PermissionId")]
		public EntitySet<UserPermission> UserPermissions
		{
			get
			{
				return this._UserPermissions;
			}
			set
			{
				this._UserPermissions.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_UserPermissions(UserPermission entity)
		{
			this.SendPropertyChanging();
			entity.Permission = this;
		}
		
		private void detach_UserPermissions(UserPermission entity)
		{
			this.SendPropertyChanging();
			entity.Permission = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.EmailVerifications")]
	public partial class EmailVerification : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _Id;
		
		private int _UserId;
		
		private System.DateTime _Created;
		
		private System.DateTime _Expires;
		
		private EntityRef<User> _User;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(System.Guid value);
    partial void OnIdChanged();
    partial void OnUserIdChanging(int value);
    partial void OnUserIdChanged();
    partial void OnCreatedChanging(System.DateTime value);
    partial void OnCreatedChanged();
    partial void OnExpiresChanging(System.DateTime value);
    partial void OnExpiresChanged();
    #endregion
		
		public EmailVerification()
		{
			this._User = default(EntityRef<User>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", DbType="Int NOT NULL")]
		public int UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					if (this._User.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Created", DbType="DateTime NOT NULL")]
		public System.DateTime Created
		{
			get
			{
				return this._Created;
			}
			set
			{
				if ((this._Created != value))
				{
					this.OnCreatedChanging(value);
					this.SendPropertyChanging();
					this._Created = value;
					this.SendPropertyChanged("Created");
					this.OnCreatedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Expires", DbType="DateTime NOT NULL")]
		public System.DateTime Expires
		{
			get
			{
				return this._Expires;
			}
			set
			{
				if ((this._Expires != value))
				{
					this.OnExpiresChanging(value);
					this.SendPropertyChanging();
					this._Expires = value;
					this.SendPropertyChanged("Expires");
					this.OnExpiresChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_EmailVerification", Storage="_User", ThisKey="UserId", OtherKey="UserId", IsForeignKey=true)]
		public User User
		{
			get
			{
				return this._User.Entity;
			}
			set
			{
				User previousValue = this._User.Entity;
				if (((previousValue != value) 
							|| (this._User.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._User.Entity = null;
						previousValue.EmailVerifications.Remove(this);
					}
					this._User.Entity = value;
					if ((value != null))
					{
						value.EmailVerifications.Add(this);
						this._UserId = value.UserId;
					}
					else
					{
						this._UserId = default(int);
					}
					this.SendPropertyChanged("User");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Posts")]
	public partial class Post : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _PostId;
		
		private int _BlogId;
		
		private System.Guid _PermalinkGuid;
		
		private string _UrlTitle;
		
		private string _Title;
		
		private string _Body;
		
		private System.DateTime _CreatedOn;
		
		private string _CreatedBy;
		
		private System.Nullable<System.DateTime> _PublishDate;
		
		private bool _IsDraft;
		
		private EntityRef<Blog> _Blog;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnPostIdChanging(int value);
    partial void OnPostIdChanged();
    partial void OnBlogIdChanging(int value);
    partial void OnBlogIdChanged();
    partial void OnPermalinkGuidChanging(System.Guid value);
    partial void OnPermalinkGuidChanged();
    partial void OnUrlTitleChanging(string value);
    partial void OnUrlTitleChanged();
    partial void OnTitleChanging(string value);
    partial void OnTitleChanged();
    partial void OnBodyChanging(string value);
    partial void OnBodyChanged();
    partial void OnCreatedOnChanging(System.DateTime value);
    partial void OnCreatedOnChanged();
    partial void OnCreatedByChanging(string value);
    partial void OnCreatedByChanged();
    partial void OnPublishDateChanging(System.Nullable<System.DateTime> value);
    partial void OnPublishDateChanged();
    partial void OnIsDraftChanging(bool value);
    partial void OnIsDraftChanged();
    #endregion
		
		public Post()
		{
			this._Blog = default(EntityRef<Blog>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PostId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int PostId
		{
			get
			{
				return this._PostId;
			}
			set
			{
				if ((this._PostId != value))
				{
					this.OnPostIdChanging(value);
					this.SendPropertyChanging();
					this._PostId = value;
					this.SendPropertyChanged("PostId");
					this.OnPostIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BlogId", DbType="Int NOT NULL")]
		public int BlogId
		{
			get
			{
				return this._BlogId;
			}
			set
			{
				if ((this._BlogId != value))
				{
					if (this._Blog.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnBlogIdChanging(value);
					this.SendPropertyChanging();
					this._BlogId = value;
					this.SendPropertyChanged("BlogId");
					this.OnBlogIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PermalinkGuid", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid PermalinkGuid
		{
			get
			{
				return this._PermalinkGuid;
			}
			set
			{
				if ((this._PermalinkGuid != value))
				{
					this.OnPermalinkGuidChanging(value);
					this.SendPropertyChanging();
					this._PermalinkGuid = value;
					this.SendPropertyChanged("PermalinkGuid");
					this.OnPermalinkGuidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UrlTitle", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string UrlTitle
		{
			get
			{
				return this._UrlTitle;
			}
			set
			{
				if ((this._UrlTitle != value))
				{
					this.OnUrlTitleChanging(value);
					this.SendPropertyChanging();
					this._UrlTitle = value;
					this.SendPropertyChanged("UrlTitle");
					this.OnUrlTitleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Title", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				if ((this._Title != value))
				{
					this.OnTitleChanging(value);
					this.SendPropertyChanging();
					this._Title = value;
					this.SendPropertyChanged("Title");
					this.OnTitleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Body", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Body
		{
			get
			{
				return this._Body;
			}
			set
			{
				if ((this._Body != value))
				{
					this.OnBodyChanging(value);
					this.SendPropertyChanging();
					this._Body = value;
					this.SendPropertyChanged("Body");
					this.OnBodyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreatedOn", DbType="DateTime NOT NULL")]
		public System.DateTime CreatedOn
		{
			get
			{
				return this._CreatedOn;
			}
			set
			{
				if ((this._CreatedOn != value))
				{
					this.OnCreatedOnChanging(value);
					this.SendPropertyChanging();
					this._CreatedOn = value;
					this.SendPropertyChanged("CreatedOn");
					this.OnCreatedOnChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreatedBy", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string CreatedBy
		{
			get
			{
				return this._CreatedBy;
			}
			set
			{
				if ((this._CreatedBy != value))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._CreatedBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PublishDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> PublishDate
		{
			get
			{
				return this._PublishDate;
			}
			set
			{
				if ((this._PublishDate != value))
				{
					this.OnPublishDateChanging(value);
					this.SendPropertyChanging();
					this._PublishDate = value;
					this.SendPropertyChanged("PublishDate");
					this.OnPublishDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsDraft", DbType="Bit NOT NULL")]
		public bool IsDraft
		{
			get
			{
				return this._IsDraft;
			}
			set
			{
				if ((this._IsDraft != value))
				{
					this.OnIsDraftChanging(value);
					this.SendPropertyChanging();
					this._IsDraft = value;
					this.SendPropertyChanged("IsDraft");
					this.OnIsDraftChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Blog_Post", Storage="_Blog", ThisKey="BlogId", OtherKey="BlogId", IsForeignKey=true)]
		public Blog Blog
		{
			get
			{
				return this._Blog.Entity;
			}
			set
			{
				Blog previousValue = this._Blog.Entity;
				if (((previousValue != value) 
							|| (this._Blog.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Blog.Entity = null;
						previousValue.Posts.Remove(this);
					}
					this._Blog.Entity = value;
					if ((value != null))
					{
						value.Posts.Add(this);
						this._BlogId = value.BlogId;
					}
					else
					{
						this._BlogId = default(int);
					}
					this.SendPropertyChanged("Blog");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Users")]
	public partial class User : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _UserId;
		
		private int _IdentityProviderId;
		
		private string _Upn;
		
		private string _Email;
		
		private string _Handle;
		
		private System.Nullable<System.DateTimeOffset> _TimeZoneOffset;
		
		private System.Nullable<bool> _EmailIsVerified;
		
		private string _PasswordSalt;
		
		private string _PasswordHash;
		
		private EntitySet<UserPermission> _UserPermissions;
		
		private EntitySet<EmailVerification> _EmailVerifications;
		
		private EntityRef<IdentityProvider> _IdentityProvider;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUserIdChanging(int value);
    partial void OnUserIdChanged();
    partial void OnIdentityProviderIdChanging(int value);
    partial void OnIdentityProviderIdChanged();
    partial void OnUpnChanging(string value);
    partial void OnUpnChanged();
    partial void OnEmailChanging(string value);
    partial void OnEmailChanged();
    partial void OnHandleChanging(string value);
    partial void OnHandleChanged();
    partial void OnTimeZoneOffsetChanging(System.Nullable<System.DateTimeOffset> value);
    partial void OnTimeZoneOffsetChanged();
    partial void OnEmailIsVerifiedChanging(System.Nullable<bool> value);
    partial void OnEmailIsVerifiedChanged();
    partial void OnPasswordSaltChanging(string value);
    partial void OnPasswordSaltChanged();
    partial void OnPasswordHashChanging(string value);
    partial void OnPasswordHashChanged();
    #endregion
		
		public User()
		{
			this._UserPermissions = new EntitySet<UserPermission>(new Action<UserPermission>(this.attach_UserPermissions), new Action<UserPermission>(this.detach_UserPermissions));
			this._EmailVerifications = new EntitySet<EmailVerification>(new Action<EmailVerification>(this.attach_EmailVerifications), new Action<EmailVerification>(this.detach_EmailVerifications));
			this._IdentityProvider = default(EntityRef<IdentityProvider>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdentityProviderId", DbType="Int NOT NULL")]
		public int IdentityProviderId
		{
			get
			{
				return this._IdentityProviderId;
			}
			set
			{
				if ((this._IdentityProviderId != value))
				{
					if (this._IdentityProvider.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnIdentityProviderIdChanging(value);
					this.SendPropertyChanging();
					this._IdentityProviderId = value;
					this.SendPropertyChanged("IdentityProviderId");
					this.OnIdentityProviderIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Upn", DbType="NVarChar(200) NOT NULL", CanBeNull=false)]
		public string Upn
		{
			get
			{
				return this._Upn;
			}
			set
			{
				if ((this._Upn != value))
				{
					this.OnUpnChanging(value);
					this.SendPropertyChanging();
					this._Upn = value;
					this.SendPropertyChanged("Upn");
					this.OnUpnChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Email", DbType="NVarChar(MAX)")]
		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				if ((this._Email != value))
				{
					this.OnEmailChanging(value);
					this.SendPropertyChanging();
					this._Email = value;
					this.SendPropertyChanged("Email");
					this.OnEmailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Handle", DbType="NVarChar(MAX)")]
		public string Handle
		{
			get
			{
				return this._Handle;
			}
			set
			{
				if ((this._Handle != value))
				{
					this.OnHandleChanging(value);
					this.SendPropertyChanging();
					this._Handle = value;
					this.SendPropertyChanged("Handle");
					this.OnHandleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TimeZoneOffset", DbType="DateTimeOffset")]
		public System.Nullable<System.DateTimeOffset> TimeZoneOffset
		{
			get
			{
				return this._TimeZoneOffset;
			}
			set
			{
				if ((this._TimeZoneOffset != value))
				{
					this.OnTimeZoneOffsetChanging(value);
					this.SendPropertyChanging();
					this._TimeZoneOffset = value;
					this.SendPropertyChanged("TimeZoneOffset");
					this.OnTimeZoneOffsetChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EmailIsVerified", DbType="Bit")]
		public System.Nullable<bool> EmailIsVerified
		{
			get
			{
				return this._EmailIsVerified;
			}
			set
			{
				if ((this._EmailIsVerified != value))
				{
					this.OnEmailIsVerifiedChanging(value);
					this.SendPropertyChanging();
					this._EmailIsVerified = value;
					this.SendPropertyChanged("EmailIsVerified");
					this.OnEmailIsVerifiedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PasswordSalt", DbType="NVarChar(MAX)")]
		public string PasswordSalt
		{
			get
			{
				return this._PasswordSalt;
			}
			set
			{
				if ((this._PasswordSalt != value))
				{
					this.OnPasswordSaltChanging(value);
					this.SendPropertyChanging();
					this._PasswordSalt = value;
					this.SendPropertyChanged("PasswordSalt");
					this.OnPasswordSaltChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PasswordHash", DbType="NVarChar(MAX)")]
		public string PasswordHash
		{
			get
			{
				return this._PasswordHash;
			}
			set
			{
				if ((this._PasswordHash != value))
				{
					this.OnPasswordHashChanging(value);
					this.SendPropertyChanging();
					this._PasswordHash = value;
					this.SendPropertyChanged("PasswordHash");
					this.OnPasswordHashChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_UserPermission", Storage="_UserPermissions", ThisKey="UserId", OtherKey="UserId")]
		public EntitySet<UserPermission> UserPermissions
		{
			get
			{
				return this._UserPermissions;
			}
			set
			{
				this._UserPermissions.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_EmailVerification", Storage="_EmailVerifications", ThisKey="UserId", OtherKey="UserId")]
		public EntitySet<EmailVerification> EmailVerifications
		{
			get
			{
				return this._EmailVerifications;
			}
			set
			{
				this._EmailVerifications.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="IdentityProvider_User", Storage="_IdentityProvider", ThisKey="IdentityProviderId", OtherKey="IdentityProviderId", IsForeignKey=true)]
		public IdentityProvider IdentityProvider
		{
			get
			{
				return this._IdentityProvider.Entity;
			}
			set
			{
				IdentityProvider previousValue = this._IdentityProvider.Entity;
				if (((previousValue != value) 
							|| (this._IdentityProvider.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._IdentityProvider.Entity = null;
						previousValue.Users.Remove(this);
					}
					this._IdentityProvider.Entity = value;
					if ((value != null))
					{
						value.Users.Add(this);
						this._IdentityProviderId = value.IdentityProviderId;
					}
					else
					{
						this._IdentityProviderId = default(int);
					}
					this.SendPropertyChanged("IdentityProvider");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_UserPermissions(UserPermission entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}
		
		private void detach_UserPermissions(UserPermission entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}
		
		private void attach_EmailVerifications(EmailVerification entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}
		
		private void detach_EmailVerifications(EmailVerification entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}
	}
}
#pragma warning restore 1591
