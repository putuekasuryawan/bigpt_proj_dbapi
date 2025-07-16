# DBAPI

A lightweight and flexible ASP.NET Core Web API for executing raw SQL queries across multiple database systems. Whether you're developing internal tools, testing queries, or building database utilities, **DBAPI** offers a simple and unified interface to interact with:

- ✅ Microsoft SQL Server  
- ✅ PostgreSQL  
- ✅ MySQL  

---

## 🚀 Features

- 🔗 Accepts raw SQL queries and connection strings via HTTP GET
- 📦 Returns query results in clean JSON format
- ⚙️ Supports multiple RDBMS engines
- 🛡️ API Key protection for endpoint access
- 🧪 Ideal for rapid testing and internal use

---

## 📥 API Endpoints

### `GET /BiGptApi/SqlServerQuery`
Execute a SQL Server query.

**Query Parameters:**
- `conn`: SQL Server connection string
- `q`: Raw SQL query

---

### `GET /BiGptApi/PostgreQuery`
Execute a PostgreSQL query.

**Query Parameters:**
- `conn`: PostgreSQL connection string
- `q`: Raw SQL query

---

### `GET /BiGptApi/MySqlQuery`
Execute a MySQL query.

**Query Parameters:**
- `conn`: MySQL connection string
- `q`: Raw SQL query

---
