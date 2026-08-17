# 🦈 FinShark - Web API Quản Lý & Theo Dõi Cổ Phiếu

**FinShark** là một ứng dụng Web API tài chính mạnh mẽ, được xây dựng trên nền tảng **.NET 9.0**, cho phép người dùng theo dõi thông tin cổ phiếu, quản lý danh mục đầu tư cá nhân (Portfolio), và tương tác, thảo luận thông qua hệ thống bình luận (Comments). Dự án hỗ trợ bảo mật chặt chẽ bằng cơ chế xác thực JWT và phân quyền người dùng (ASP.NET Core Identity).

---

## 📂 Cấu Trúc Mã Nguồn Dự Án

Mã nguồn Backend được tổ chức khoa học theo mô hình Repository Pattern kết hợp với DTOs (Data Transfer Objects) và Mappers:

*   **[api/](file:///d:/WORKSPACE/Project_WEB/FinShark/api)**: Thư mục gốc chứa toàn bộ mã nguồn ASP.NET Core API.
    *   **[Controllers/](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Controllers)**: Chứa các bộ điều hướng (Controllers) xử lý yêu cầu HTTP:
        *   [AccountController.cs](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Controllers/AccountController.cs): Đăng ký, đăng nhập và cấp phát JWT Token.
        *   [StockController.cs](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Controllers/StockController.cs): CRUD thông tin cổ phiếu kèm bộ lọc nâng cao.
        *   [CommentController.cs](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Controllers/CommentController.cs): CRUD bình luận theo từng mã cổ phiếu.
        *   [PortfolioController.cs](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Controllers/PortfolioController.cs): Quản lý danh mục cổ phiếu của từng tài khoản.
    *   **[Models/](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Models)**: Định nghĩa các thực thể (Entities) ánh xạ trực tiếp xuống cơ sở dữ liệu:
        *   [AppUser.cs](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Models/AppUser.cs): Thông tin người dùng (kế thừa từ `IdentityUser`).
        *   [Stock.cs](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Models/Stock.cs): Thông tin mã cổ phiếu (Symbol, CompanyName, Purchase, MarketCap,...).
        *   [Comment.cs](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Models/Comment.cs): Nội dung bình luận, thời gian tạo và liên kết tới User & Stock.
        *   [Portfolio.cs](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Models/Portfolio.cs): Bảng liên kết trung gian (Many-to-Many) giữa User và Stock.
    *   **[Data/](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Data)**: Khởi tạo DBContext và thiết lập cấu hình cơ sở dữ liệu:
        *   [ApplicationDBContext.cs](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Data/ApplicationDBContext.cs): Định nghĩa các DbSet, thiết lập Composite Key cho Portfolio, và cấu hình Seeding dữ liệu (Role Admin & User).
    *   **[Repository/](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Repository)**: Lớp xử lý dữ liệu tương tác trực tiếp với Database:
        *   [StockRepository.cs](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Repository/StockRepository.cs)
        *   [CommentRepository.cs](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Repository/CommentRepository.cs)
        *   [PortfolioRepository.cs](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Repository/PortfolioRepository.cs)
    *   **[Interfaces/](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Interfaces)**: Định nghĩa các hợp đồng (Interfaces) để áp dụng cơ chế Dependency Injection:
        *   [IStockRepository.cs](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Interfaces/IStockRepository.cs) | [ICommentRepository.cs](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Interfaces/ICommentRepository.cs) | [IPortfolioRepository.cs](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Interfaces/IPortfolioRepository.cs) | [ITokenServices.cs](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Interfaces/ITokenServices.cs)
    *   **[Dtos/](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Dtos)**: Các đối tượng vận chuyển dữ liệu giúp che giấu thông tin nhạy cảm của Model gốc và kiểm soát định dạng dữ liệu đầu vào/ra.
    *   **[Mappers/](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Mappers)**: Lớp chuyển đổi qua lại giữa Models và DTOs.
    *   **[Services/](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Services)**: Xử lý các logic nghiệp vụ phụ trợ (ví dụ: [TokenServices.cs](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Services/TokenServices.cs) để tạo mã JWT Token).
    *   **[Helpers/](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Helpers)**: Các lớp bổ trợ như [QueryObject.cs](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Helpers/QueryObject.cs) định nghĩa tham số lọc, phân trang, và sắp xếp.
    *   **[Extensions/](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Extensions)**: Các hàm mở rộng tiện ích như [ClaimsExtentions.cs](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Extensions/ClaimsExtentions.cs) để lấy tên người dùng từ Claims.

---

## 🛠️ Công Nghệ & Thư Viện Sử Dụng

*   **Runtime & Framework**: `.NET 9.0` (ASP.NET Core Web API)
*   **Database Engine**: `PostgreSQL`
*   **ORM**: `Entity Framework Core 9`
*   **Bảo mật & Xác thực**: `ASP.NET Core Identity` + `Microsoft.AspNetCore.Authentication.JwtBearer`
*   **API Documentation**: `Swagger / OpenAPI` (được cấu hình hỗ trợ Authorize header chứa JWT)
*   **JSON Serialization**: `Microsoft.AspNetCore.Mvc.NewtonsoftJson` (bỏ qua lỗi lặp vòng tham chiếu giữa các thực thể liên kết)

---

## 🚀 Hướng Dẫn Cài Đặt & Chạy Dự Án

### Yêu Cầu Hệ Thống
*   Cài đặt [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
*   Hệ quản trị cơ sở dữ liệu [PostgreSQL](https://www.postgresql.org/) đã được cài đặt và đang chạy.
*   Cài đặt công cụ Entity Framework Core CLI:
    ```bash
    dotnet tool install --global dotnet-ef
    ```

### 1. Cấu Hình Dự Án
Mở file cấu hình **[appsettings.json](file:///d:/WORKSPACE/Project_WEB/FinShark/api/appsettings.json)** và điều chỉnh các thông số sau:

*   **Chuỗi kết nối cơ sở dữ liệu (`DefaultConnection`)**: Cập nhật Host, Port, Username, Password phù hợp với tài khoản PostgreSQL của bạn.
*   **JWT Configuration**: Cấu hình các tham số phát hành và xác thực Token:
    ```json
    "JWT": {
      "Issuer": "http://localhost:5115",
      "Audience": "http://localhost:5115",
      "SigningKey": "d82a25aa1cfd6d1301637167beaa5f08223536d91db00a09bd1ea487cf37b8b4"
    }
    ```

### 2. Khởi Tạo Cơ Sở Dữ Liệu
Di chuyển vào thư mục chứa mã nguồn backend:
```bash
cd api
```

Thực thi lệnh sau để cập nhật cấu trúc bảng (chạy các tệp Migration) và nạp dữ liệu phân quyền ban đầu (Roles: Admin, User) vào PostgreSQL:
```bash
dotnet ef database update
```

### 3. Khởi Chạy API
Từ thư mục `api/`, chạy dự án:
```bash
dotnet run
```
Hoặc chạy chế độ nhà phát triển tự động tải lại mã nguồn khi có thay đổi:
```bash
dotnet watch
```

API sẽ khởi chạy. Bạn có thể mở tài liệu trực quan của hệ thống Swagger UI tại đường dẫn mặc định:
`http://localhost:5115/swagger/index.html` (hoặc cổng ngẫu nhiên do hệ thống chỉ định khi chạy).

---

## 🔐 Cơ Chế Xác Thực & Phân Quyền

*   Dự án sử dụng cơ chế bảo mật **JWT (Json Web Token)**. Để truy cập các API yêu cầu quyền, bạn cần đính kèm JWT Token vào Header của yêu cầu:
    ```http
    Authorization: Bearer <YOUR_JWT_TOKEN>
    ```
*   **Chính sách mật khẩu (Password Policy)** mặc định được quy định tại [Program.cs](file:///d:/WORKSPACE/Project_WEB/FinShark/api/Program.cs#L29-L36):
    *   Độ dài tối thiểu: **12 ký tự**
    *   Yêu cầu ít nhất **1 ký tự số** (Digit)
    *   Yêu cầu ít nhất **1 ký tự viết thường** (Lowercase)
    *   Yêu cầu ít nhất **1 ký tự viết hoa** (Uppercase)
    *   Yêu cầu ít nhất **1 ký tự đặc biệt** (Non-Alphanumeric)

---

## 🔗 Danh Sách Các Endpoints API Chi Tiết

### 1. Tài Khoản & Xác Thực (`api/account`)
| Phương thức | Endpoint | Yêu Cầu Auth | Mô tả |
| :--- | :--- | :---: | :--- |
| **POST** | `/api/account/register` | ❌ Không | Đăng ký tài khoản mới (Mặc định gắn Role `User`). |
| **POST** | `/api/account/login` | ❌ Không | Đăng nhập tài khoản, nhận về JWT Token. |

### 2. Quản Lý Cổ Phiếu (`api/stock`)
| Phương thức | Endpoint | Yêu Cầu Auth | Mô tả |
| :--- | :--- | :---: | :--- |
| **GET** | `/api/stock` | ❌ Không | Lấy danh sách tất cả các cổ phiếu hỗ trợ bộ lọc nâng cao. |
| **GET** | `/api/stock/{id}` | ❌ Không | Lấy chi tiết thông tin và danh sách bình luận của một mã cổ phiếu. |
| **POST** | `/api/stock` | ❌ Không | Tạo mới một mã cổ phiếu. |
| **PUT** | `/api/stock/{id}` | ❌ Không | Cập nhật thông tin mã cổ phiếu theo ID. |
| **DELETE** | `/api/stock/{id}` | ❌ Không | Xóa mã cổ phiếu theo ID. |

> [!NOTE]
> **Bộ lọc nâng cao cho API lấy danh sách cổ phiếu (`GET /api/stock`)**:
> Bạn có thể truyền các Query Parameters sau:
> *   `Symbol`: Tìm kiếm theo ký hiệu cổ phiếu (tìm kiếm gần đúng không phân biệt chữ hoa thường).
> *   `CompanyName`: Tìm kiếm theo tên công ty.
> *   `SortBy`: Tên cột cần sắp xếp (ví dụ: `Symbol`).
> *   `IsDecending`: Sắp xếp giảm dần (`true`) hoặc tăng dần (`false`).
> *   `PageNumber`: Trang dữ liệu hiện tại (Mặc định: `1`).
> *   `PageSize`: Số lượng bản ghi trên một trang (Mặc định: `20`).

### 3. Quản Lý Bình Luận (`api/comment`)
| Phương thức | Endpoint | Yêu Cầu Auth | Mô tả |
| :--- | :--- | :---: | :--- |
| **GET** | `/api/comment` | ❌ Không | Lấy danh sách tất cả bình luận trong hệ thống. |
| **GET** | `/api/comment/{id}` | ❌ Không | Lấy chi tiết bình luận kèm thông tin người tạo. |
| **POST** | `/api/comment/{stockId}` | ✔️ Có (JWT) | Đăng bình luận mới cho mã cổ phiếu tương ứng với `stockId`. |
| **PUT** | `/api/comment/{id}` | ❌ Không | Cập nhật nội dung tiêu đề và bình luận của bạn. |
| **DELETE** | `/api/comment/{id}` | ❌ Không | Xóa một bình luận theo ID. |

### 4. Danh Mục Đầu Tư Cá Nhân (`api/portfolio`)
| Phương thức | Endpoint | Yêu Cầu Auth | Mô tả |
| :--- | :--- | :---: | :--- |
| **GET** | `/api/portfolio` | ✔️ Có (JWT) | Lấy danh sách tất cả cổ phiếu có trong danh mục của người dùng hiện tại. |
| **POST** | `/api/portfolio` | ✔️ Có (JWT) | Thêm một cổ phiếu vào danh mục bằng Query Parameter `symbol`. |
| **DELETE** | `/api/portfolio` | ✔️ Có (JWT) | Xóa cổ phiếu khỏi danh mục bằng Query Parameter `symbol`. |

---

## 📝 Giấy Phép
Dự án được phát triển nhằm mục đích học tập và xây dựng ứng dụng mẫu cho nền tảng web tài chính.
