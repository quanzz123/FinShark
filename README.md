# FinShark - Ứng Dụng Quản Lý Và Theo Dõi Cổ Phiếu

**FinShark** là một ứng dụng web tài chính cho phép người dùng theo dõi thông tin cổ phiếu, các chỉ số tài chính cơ bản và tương tác thảo luận thông qua các bình luận. Dự án được phát triển với mô hình phân tách rõ ràng giữa **Backend (ASP.NET Core Web API)** và **Frontend**.

---

## 📂 Cấu Trúc Thư Mục Dự Án

Dự án được chia thành hai phần chính:

*   **[`api/`](file:///z:/Workspace2026/WEB%20PROJECT/finshark/api)**: Mã nguồn của Backend, được xây dựng bằng ASP.NET Core Web API (.NET 9.0).
*   **[`fontend/`](file:///z:/Workspace2026/WEB%20PROJECT/finshark/fontend)**: Thư mục chứa mã nguồn Frontend (React, Vue, hoặc framework khác).

---

## 🛠️ Công Nghệ Sử Dụng

### Backend
*   **Framework**: .NET 9.0 (ASP.NET Core Web API)
*   **Database ORM**: Entity Framework Core
*   **Database**: PostgreSQL (thông qua package `Npgsql.EntityFrameworkCore.PostgreSQL`)
*   **API Documentation**: Swagger / OpenAPI (Swashbuckle)

### Frontend
*   *(Sẽ được cập nhật khi khởi tạo mã nguồn Frontend)*

---

## 🚀 Hướng Dẫn Cài Đặt & Chạy Dự Án

### Yêu Cầu Hệ Thống
*   Đã cài đặt [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
*   Cơ sở dữ liệu [PostgreSQL](https://www.postgresql.org/) đang chạy cục bộ hoặc trên máy chủ.
*   Cài đặt công cụ EF Core CLI (nếu muốn quản lý migration):
    ```bash
    dotnet tool install --global dotnet-ef
    ```

### 1. Cấu Hình Cơ Sở Dữ Liệu
Mở file [appsettings.json](file:///z:/Workspace2026/WEB%20PROJECT/finshark/api/appsettings.json) và chỉnh sửa chuỗi kết nối (`ConnectionString`) sao cho phù hợp với tài khoản PostgreSQL của bạn:

```json
"ConnectionStrings": {
  "DefaultConnection" : "Host=localhost;Port=5432;Database=finshark;Username=<YOUR_USERNAME>;Password=<YOUR_PASSWORD>"
}
```

### 2. Khởi Chạy Backend API
Di chuyển vào thư mục `api` và thực hiện các lệnh sau:

*   **Khôi phục các package NuGet:**
    ```bash
    dotnet restore
    ```

*   **Cập nhật Database (Áp dụng các Migration có sẵn vào PostgreSQL):**
    ```bash
    dotnet ef database update
    ```

*   **Chạy ứng dụng:**
    ```bash
    dotnet run
    ```

Ứng dụng sẽ chạy mặc định tại cổng HTTP/HTTPS (ví dụ: `http://localhost:5000` hoặc cổng ngẫu nhiên do dotnet chỉ định). Bạn có thể truy cập tài liệu Swagger tại đường dẫn:
`http://localhost:<PORT>/swagger/index.html` để kiểm tra và thử nghiệm các API.

---

## 🔗 Các API Endpoints Chính (Cổ Phiếu)

Tất cả các API của Stock nằm dưới tiền tố `/api/stock`:

| Phương thức | Endpoint | Mô tả |
| :--- | :--- | :--- |
| **GET** | `/api/stock` | Lấy danh sách tất cả các cổ phiếu |
| **GET** | `/api/stock/{id}` | Lấy chi tiết thông tin của một cổ phiếu theo ID |
| **POST** | `/api/stock` | Thêm mới một mã cổ phiếu |
| **PUT** | `/api/stock/{id}` | Cập nhật thông tin cổ phiếu theo ID |
| **DELETE** | `/api/stock/{id}` | Xóa một mã cổ phiếu theo ID |

---

## 📝 Giấy Phép (License)
Dự án được phát triển cho mục đích học tập và tham khảo.
