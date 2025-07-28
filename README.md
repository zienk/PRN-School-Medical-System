# 🏥 Hệ Thống Quản Lý Y Tế Trường Học (School Medical Management System)

[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/)
[![WPF](https://img.shields.io/badge/WPF-Windows-lightblue.svg)](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-green.svg)](https://docs.microsoft.com/en-us/ef/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-red.svg)](https://www.microsoft.com/en-us/sql-server)

> **Final Report - Basic Cross-Platform Application Programming With .NET**  
> **Group:** Brainrot | **Class:** SE1852 | **Course ID:** PRN212 | **Semester:** SUMMER2025

## 📋 Mục Lục
- [Giới Thiệu](#-giới-thiệu)
- [Nhóm Phát Triển](#-nhóm-phát-triển)
- [Mục Tiêu và Kết Quả Mong Đợi](#-mục-tiêu-và-kết-quả-mong-đợi)
- [Phạm Vi và Yêu Cầu Kỹ Thuật](#-phạm-vi-và-yêu-cầu-kỹ-thuật)
- [Tính Năng](#-tính-năng)
- [Kiến Trúc Hệ Thống](#-kiến-trúc-hệ-thống)
- [Công Nghệ Sử Dụng](#-công-nghệ-sử-dụng)
- [Kế Hoạch Triển Khai](#-kế-hoạch-triển-khai)
- [Tài Nguyên và Công Cụ](#-tài-nguyên-và-công-cụ)
- [Đánh Giá Rủi Ro](#-đánh-giá-rủi-ro)
- [Cài Đặt](#-cài-đặt)
- [Cấu Hình](#-cấu-hình)
- [Sử Dụng](#-sử-dụng)
- [Cấu Trúc Dự Án](#-cấu-trúc-dự-án)
- [Tài Liệu Tham Khảo](#-tài-liệu-tham-khảo)
- [Đóng Góp](#-đóng-góp)
- [Giấy Phép](#-giấy-phép)

## 🎯 Giới Thiệu

**Hệ Thống Quản Lý Y Tế Trường Học (School Medical Management System)** là một ứng dụng desktop được phát triển bằng Windows Presentation Foundation (WPF) với .NET 8, C#, SQL Server Database và Entity Framework Core. 

Trong bối cảnh giáo dục hiện đại, sức khỏe và phúc lợi của học sinh là điều tối quan trọng. Việc quản lý sức khỏe học sinh hiệu quả là rất quan trọng để tạo ra một môi trường học tập an toàn và thuận lợi. Các phương pháp truyền thống trong việc quản lý hồ sơ sức khỏe học sinh, theo dõi sự cố và tổ chức các chiến dịch sức khỏe thường liên quan đến các quy trình thủ công, dẫn đến sự kém hiệu quả, không nhất quán về dữ liệu và phản ứng chậm trễ đối với các tình huống sức khỏe quan trọng.

Ứng dụng này giúp y tá trường học quản lý hồ sơ sức khỏe học sinh, xử lý các sự cố sức khỏe bất ngờ như sốt và chấn thương, đồng thời tổ chức các chiến dịch tiêm chủng và kiểm tra sức khỏe định kỳ. Hệ thống cung cấp các tính năng cốt lõi như đăng nhập, quản lý hồ sơ sức khỏe học sinh, theo dõi sự cố và ghi lại các sự kiện sức khỏe.

## 👥 Nhóm Phát Triển

**Group:** Brainrot  
**Instructor:** Tran Duy Thanh, Ph.D  
**Class:** SE1852  
**Course ID:** PRN212  
**Semester:** SUMMER2025  

| STT | Mã Sinh Viên | Họ và Tên | Đóng Góp |
|-----|--------------|-----------|----------|
| 1   | SE182672     | Nguyễn Ngọc Viên | 100% |
| 2   | SE184745     | Mai Văn Thành | 100% |
| 3   | SE182117     | Lại Thành Nhật Thiên | 100% |

**Địa điểm:** Thành phố Hồ Chí Minh  
**Ngày hoàn thành:** 25 tháng 7, 2025

## 🎯 Mục Tiêu và Kết Quả Mong Đợi

### Mục Tiêu Chính
- **Số hóa quy trình quản lý y tế:** Chuyển đổi từ quản lý thủ công sang hệ thống số hóa hiện đại
- **Tăng hiệu quả quản lý:** Giảm thời gian xử lý và tăng độ chính xác trong quản lý thông tin sức khỏe
- **Cải thiện chất lượng chăm sóc:** Cung cấp thông tin kịp thời và chính xác cho việc ra quyết định y tế
- **Tăng cường bảo mật:** Đảm bảo thông tin y tế được bảo vệ an toàn và tuân thủ quy định

### Kết Quả Mong Đợi
- ✅ **Hệ thống hoàn chỉnh:** Ứng dụng WPF với đầy đủ chức năng quản lý y tế trường học
- ✅ **Cơ sở dữ liệu:** Database SQL Server được thiết kế tối ưu với các bảng và quan hệ phù hợp
- ✅ **Giao diện người dùng:** UI/UX thân thiện, dễ sử dụng cho các vai trò khác nhau
- ✅ **Báo cáo và thống kê:** Hệ thống báo cáo chi tiết và trực quan
- ✅ **Tài liệu kỹ thuật:** Documentation đầy đủ cho việc triển khai và bảo trì
- ✅ **Kiểm thử:** Test cases và quality assurance đảm bảo chất lượng sản phẩm

## 🔍 Phạm Vi và Yêu Cầu Kỹ Thuật

### Phạm Vi Dự Án

#### Bao Gồm:
- 🏥 **Quản lý thông tin học sinh:** Hồ sơ cá nhân, thông tin liên hệ, lịch sử y tế
- 👩‍⚕️ **Quản lý nhân viên y tế:** Thông tin y tá, bác sĩ, phân quyền truy cập
- 📋 **Khám sức khỏe định kỳ:** Lập lịch, thực hiện và lưu trữ kết quả khám
- 💉 **Quản lý tiêm chủng:** Theo dõi lịch tiêm, vaccine, phản ứng phụ
- 🚨 **Xử lý sự cố y tế:** Ghi nhận, xử lý và theo dõi các trường hợp khẩn cấp
- 📊 **Báo cáo và thống kê:** Dashboard, báo cáo định kỳ, xuất dữ liệu
- 🔐 **Bảo mật và phân quyền:** Xác thực người dùng, phân quyền theo vai trò

#### Không Bao Gồm:
- ❌ **Hệ thống thanh toán:** Không xử lý các giao dịch tài chính
- ❌ **Tích hợp với thiết bị y tế:** Không kết nối trực tiếp với máy đo, thiết bị chẩn đoán
- ❌ **Telemedicine:** Không hỗ trợ khám bệnh từ xa
- ❌ **Quản lý thuốc:** Không quản lý kho thuốc và đơn thuốc chi tiết

### Yêu Cầu Kỹ Thuật

#### Yêu Cầu Chức Năng:
- **Authentication & Authorization:** Đăng nhập an toàn với phân quyền theo vai trò
- **Data Management:** CRUD operations cho tất cả entities
- **Search & Filter:** Tìm kiếm và lọc dữ liệu nhanh chóng
- **Report Generation:** Tạo báo cáo PDF/Excel tự động
- **Data Validation:** Kiểm tra tính hợp lệ của dữ liệu đầu vào
- **Audit Trail:** Ghi log các thao tác quan trọng

#### Yêu Cầu Phi Chức Năng:
- **Performance:** Thời gian phản hồi < 2 giây cho các truy vấn thông thường
- **Scalability:** Hỗ trợ tối thiểu 1000 học sinh và 50 người dùng đồng thời
- **Security:** Mã hóa dữ liệu nhạy cảm, backup tự động
- **Usability:** Giao diện trực quan, dễ học và sử dụng
- **Reliability:** Uptime 99.5%, xử lý lỗi graceful
- **Maintainability:** Code clean, có documentation, dễ bảo trì

## ✨ Tính Năng Chính

###  Quản Lý Sự  Cố Y Tế
- Ghi nhận các sự cố như chấn thương học sinh, bệnh tật hoặc ngất xỉu xảy ra trong khuôn viên trường
- Bao gồm các trường như thời gian, triệu chứng, hành động được thực hiện và nhân viên liên quan
- Theo dõi và xử lý kịp thời các tình huống khẩn cấp

###  Quản Lý Hồ Sơ Tiêm Chủng
- Theo dõi lịch sử tiêm chủng của từng học sinh
- Hỗ trợ tên vaccine, ngày tiêm, nhà cung cấp và theo dõi phản ứng
- Tạo chương trình tiêm chủng, lập danh sách học sinh, ghi nhận kết quả tiêm chủng và theo dõi học sinh sau tiêm chủng

###  Quản Lý Hồ Sơ Sức Khỏe Học Sinh
- Duy trì dữ liệu như chiều cao, cân nặng, BMI, dị ứng, bệnh mãn tính
- Hỗ trợ cập nhật và xem xét hồ sơ sức khỏe định kỳ
- Quản lý thông tin nhân khẩu học, tiền sử bệnh, dị ứng đã biết và thông tin liên hệ khẩn cấp

### Dashboard Quản Trị
- Hiển thị báo cáo chi tiết và phân tích dữ liệu
- Cung cấp thông tin hữu ích về các sự cố sức khỏe, tỷ lệ tiêm chủng và kết quả kiểm tra sức khỏe
- Biểu đồ dễ đọc và trực quan

### 🔐 Xác Thực và Phân Quyền
- Cơ chế đăng nhập an toàn cho các vai trò người dùng khác nhau (y tá, quản trị viên)
- Phân quyền truy cập theo vai trò

## 🏗️ Kiến Trúc Hệ Thống

Hệ thống được thiết kế theo mô hình **N-Layer Architecture** với các tầng rõ ràng:

```
┌─────────────────────────────────────┐
│         Presentation Layer          │
│            (WPF Views)              │
├─────────────────────────────────────┤
│          Service Layer              │
│        (Business Logic)             │
├─────────────────────────────────────┤
│         Repository Layer            │
│       (Data Access Logic)           │
├─────────────────────────────────────┤
│       Data Access Layer             │
│      (Entity Framework)             │
├─────────────────────────────────────┤
│        Business Objects             │
│          (Entities)                 │
├─────────────────────────────────────┤
│          Database Layer             │
│        (SQL Server)                 │
└─────────────────────────────────────┘
```

## 🛠️ Công Nghệ Sử Dụng

- **Framework:** .NET 8 với Windows Presentation Foundation (WPF)
- **Programming Language:** C#
- **Database:** SQL Server (bảng: Students, HealthRecords, Vaccines, MedicalEvents)
- **ORM:** Entity Framework Core
- **Architecture Pattern:** Repository Pattern, Dependency Injection
- **Design Pattern:** MVVM (Model-View-ViewModel)
- **Development Tools:**
  - Visual Studio 2022
  - SQL Server Management Studio
  - Git for version control
- **Optional:** ADO.NET trong các phần quan trọng về hiệu suất

## 📅 Kế Hoạch Triển Khai

### Phase 1: Khởi Tạo và Phân Tích (Tuần 1 - 2)
- **Milestone 1.1:** Hoàn thành Project Proposal Report
- **Milestone 1.2:** Phân tích yêu cầu chi tiết và thiết kế database
- **Milestone 1.3:** Thiết kế UI/UX mockups và wireframes
- **Trách nhiệm:** Toàn bộ team tham gia phân tích và thiết kế

### Phase 2: Thiết Kế và Chuẩn Bị (Tuần 3 - 4)
- **Milestone 2.1:** Hoàn thành thiết kế database và tạo ERD
- **Milestone 2.2:** Setup môi trường phát triển WPF và repository
- **Milestone 2.3:** Tạo WPF project structure và MVVM base classes
- **Trách nhiệm:** Database Developer, WPF UI Developer

### Phase 3: Phát Triển Core Features (Tuần 5 - 6)
- **Milestone 3.1:** Implement Authentication & User Management với WPF
- **Milestone 3.2:** Phát triển Student Management WPF Views và ViewModels
- **Milestone 3.3:** Xây dựng Health Checkup WPF Module
- **Trách nhiệm:** WPF UI Developer, Business Logic Developer

### Phase 4: Phát Triển Advanced Features (Tuần 7 - 8)
- **Milestone 4.1:** Implement Vaccination Management WPF Interface
- **Milestone 4.2:** Phát triển Incident Management WPF System
- **Milestone 4.3:** Xây dựng Reporting & Analytics WPF Dashboard
- **Trách nhiệm:** Toàn bộ team WPF development

### Phase 5: Testing và Tối Ưu (Tuần 9 - 10)
- **Milestone 5.1:** Unit Testing cho ViewModels và Business Logic
- **Milestone 5.2:** WPF UI Testing và User Acceptance Testing
- **Milestone 5.3:** Performance optimization và WPF bug fixes
- **Trách nhiệm:** Toàn bộ team

### Phase 6: Triển Khai và Bàn Giao (Tuần 11)
- **Milestone 6.1:** WPF Application deployment và configuration
- **Milestone 6.2:** User training và WPF documentation
- **Milestone 6.3:** Final presentation và Project Report Document
- **Trách nhiệm:** Team Leader, Documentation

## 🛠️ Tài Nguyên và Công Cụ

### Công Cụ Phát Triển
- **IDE:** Visual Studio 2022 Community/Professional
- **Database Management:** SQL Server Management Studio (SSMS)
- **Version Control:** Git + GitHub
- **Project Management:** GitHub Issues / Excel
- **Communication:** Google Meet / Discord

### Frameworks và Libraries
- **.NET Framework:** .NET 8
- **UI Framework:** WPF (Windows Presentation Foundation)
- **Design Pattern:** MVVM (Model-View-ViewModel)
- **ORM:** Entity Framework Core
- **Database:** Microsoft SQL Server
- **Architecture:** Multiple-layer architecture với separation of concerns
- **Testing:** Manual testing cho tất cả các chức năng chính
- **Optional:** ADO.NET cho các phần quan trọng về hiệu suất

### Môi Trường và Infrastructure
- **Development Environment:** Windows 10/11 (64-bit)
- **Database Server:** SQL Server LocalDB (development), SQL Server Express (testing)
- **Documentation:** Markdown, GitHub Wiki, Google Docs, Microsoft Word
- **Design Tools:** Figma (UI/UX), Draw.io (diagrams)

### Yêu Cầu Hệ Thống

#### Phần Mềm
- **OS:** Windows 10/11 (64-bit)
- **Runtime:** .NET 8.0 Runtime
- **Database:** SQL Server 2019 hoặc mới hơn
- **Development:** Visual Studio 2022 (để phát triển)

#### Phần Cứng
- **RAM:** Tối thiểu 4GB (khuyến nghị 8GB)
- **Storage:** 500MB dung lượng trống
- **Processor:** Intel Core i3 hoặc tương đương
- **Display:** 1366x768 (khuyến nghị 1920x1080)

### Setup Ban Đầu
1. **GitHub Repository:** Tạo private repository với branch protection
2. **Project Structure:** Setup solution với multiple projects
3. **Database:** Tạo database schema và seed data
4. **Documentation:** Wiki setup và README templates

## ⚠️ Đánh Giá Rủi Ro

### Rủi Ro Kỹ Thuật

| Rủi Ro | Mức Độ | Tác Động | Biện Pháp Giảm Thiểu |
|---------|---------|----------|---------------------|
| **Thiếu kinh nghiệm với WPF** | Cao | Cao | - Học tập qua documentation và tutorials<br>- Pair programming với thành viên có kinh nghiệm<br>- Prototype sớm để test feasibility |
| **Database design phức tạp** | Trung bình | Cao | - Review thiết kế với mentor<br>- Sử dụng ERD tools<br>- Implement từng phần nhỏ |
| **Performance issues** | Trung bình | Trung bình | - Load testing sớm<br>- Database indexing<br>- Code optimization |
| **Security vulnerabilities** | Cao | Cao | - Follow security best practices<br>- Input validation<br>- Regular security reviews |

### Rủi Ro Dự Án

| Rủi Ro | Mức Độ | Tác Động | Biện Pháp Giảm Thiểu |
|---------|---------|----------|---------------------|
| **Thành viên nghỉ học/bỏ nhóm** | Trung bình | Cao | - Cross-training giữa các thành viên<br>- Documentation chi tiết<br>- Regular check-ins |
| **Scope creep** | Trung bình | Trung bình | - Định nghĩa scope rõ ràng<br>- Change control process<br>- Regular stakeholder communication |
| **Timeline delays** | Cao | Cao | - Buffer time trong schedule<br>- Agile methodology<br>- Regular progress tracking |
| **Technical debt** | Trung bình | Trung bình | - Code reviews<br>- Refactoring sprints<br>- Technical documentation |

### Rủi Ro Môi Trường

| Rủi Ro | Mức Độ | Tác Động | Biện Pháp Giảm Thiểu |
|---------|---------|----------|---------------------|
| **Hardware/Software failures** | Thấp | Cao | - Regular backups<br>- Cloud storage<br>- Multiple development environments |
| **Network connectivity issues** | Thấp | Trung bình | - Offline development capability<br>- Local repositories<br>- Alternative communication channels |

### Kế Hoạch Ứng Phó
1. **Weekly Risk Assessment:** Đánh giá rủi ro hàng tuần trong team meetings
2. **Contingency Planning:** Có kế hoạch dự phòng cho các rủi ro cao
3. **Escalation Process:** Quy trình báo cáo và xử lý khi rủi ro xảy ra
4. **Risk Monitoring:** Theo dõi và cập nhật trạng thái rủi ro thường xuyên

## 🚀 Cài Đặt

### 1. Clone Repository
```bash
git clone https://github.com/zienk/PRN-School-Medical-System.git
cd PRN-School-Medical-System
```

### 2. Cài Đặt Dependencies
```bash
dotnet restore
```

### 3. Thiết Lập Database
1. Mở SQL Server Management Studio
2. Tạo database mới với tên `PRN_EduHealth`
3. Chạy script tạo bảng từ ERD: [Database ERD](https://dbdiagram.io/d/PRN_ERD_ReportProject-6882d7facca18e685ca56364)

### 4. Build Solution
```bash
dotnet build
```

### 5. Xem Demo và Screenshots
- **Source Code:** [GitHub Repository](https://github.com/zienk/PRN-School-Medical-System.git)
- **UI Screenshots:** [Google Drive](https://drive.google.com/drive/folders/1MEwM9vxw1GRGEqqfF-pZ89ODsd4OXLSH?usp=drive_link)

## ⚙️ Cấu Hình

### Connection String
Cập nhật connection string trong file `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=(local); database=PRN_EduHealth; uid=sa; pwd=your_password; TrustServerCertificate=True;"
  }
}
```

### Tài Khoản Mặc Định
- **Quản lý:** manager1 / 123
- **Y tá:** nurse1 / 123
- **Phụ huynh:** parent01 / 123

## 🎮 Sử Dụng

### Khởi Chạy Ứng Dụng
```bash
dotnet run --project WPF.SchoolMedicalManagementSystem
```

### Đăng Nhập
1. Mở ứng dụng
2. Nhập username và password
3. Chọn vai trò phù hợp
4. Nhấn "Đăng Nhập"

### Các Chức Năng Chính
- **Quản lý học sinh:** Thêm, sửa, xóa thông tin học sinh
- **Khám sức khỏe:** Tạo lịch khám, ghi nhận kết quả
- **Tiêm chủng:** Quản lý chương trình và thực hiện tiêm chủng
- **Báo cáo:** Xuất báo cáo thống kê theo nhiều tiêu chí

## 📁 Cấu Trúc Dự Án

```
SchoolMedicalSystem/
├── BusinessObjects/              # Entities và Models
│   ├── Entities/                # Database Entities
│   └── BusinessObjects.csproj
├── DataAccessLayer/             # Entity Framework Context
│   ├── PrnEduHealthContext.cs   # Database Context
│   └── DataAccessLayer.csproj
├── Repositories/                # Repository Pattern Implementation
│   ├── Interfaces/              # Repository Interfaces
│   ├── Implementations/         # Repository Implementations
│   └── Repositories.csproj
├── Services/                    # Business Logic Layer
│   ├── Interfaces/              # Service Interfaces
│   ├── Implementations/         # Service Implementations
│   └── Services.csproj
├── WPF.SchoolMedicalManagementSystem/  # Presentation Layer
│   ├── ManagerView/             # Views cho Quản lý
│   ├── NurseView/              # Views cho Y tá
│   ├── ParentView/             # Views cho Phụ huynh
│   ├── LoginWindow.xaml        # Màn hình đăng nhập
│   ├── App.xaml                # Application Configuration
│   └── appsettings.json        # Configuration File
└── README.md
```

##  Kết Quả Đạt Được

### Đánh Giá Hệ Thống

| Tiêu Chí Đánh Giá | Kết Quả |
|-------------------|---------|
| **Hoàn thành các tính năng cốt lõi** | 100%: Sự kiện Y tế, Hồ sơ Tiêm chủng, Hồ sơ Sức khỏe |
| **Thiết kế giao diện** | Sạch sẽ, trực quan, dễ sử dụng |
| **Tốc độ xử lý dữ liệu** | Nhanh và phản hồi tốt |
| **Tính ổn định hệ thống** | Ổn định trong điều kiện sử dụng bình thường |
| **Khả năng bảo trì & mở rộng** | Thiết kế 4 tầng hỗ trợ mở rộng tính năng trong tương lai |

### Điểm Mạnh của Dự Án
- **Phạm vi tập trung:** Chỉ triển khai các tính năng thiết yếu, làm cho hệ thống sạch sẽ và dễ sử dụng
- **Giao diện thân thiện:** Được xây dựng bằng WPF để kiểm soát UI tốt hơn và dễ sử dụng
- **Codebase có cấu trúc tốt:** Kiến trúc phân tầng đơn giản hóa việc bảo trì và nâng cấp
- **Hỗ trợ offline:** Ứng dụng hoạt động mà không cần internet, phù hợp với môi trường địa phương

### Hạn Chế
- **Không có quản lý vai trò người dùng:** Mọi người đều có quyền truy cập đầy đủ
- **Thiếu đồng bộ hóa nhiều máy:** Không có dữ liệu chia sẻ nếu cài đặt trên nhiều thiết bị
- **Thiếu tìm kiếm/lọc nâng cao:** Tìm kiếm hồ sơ theo ngày hoặc điều kiện cần cải thiện
- **Sao lưu/khôi phục thủ công:** Hiện tại dựa vào công cụ SQL Server thay vì chức năng trong ứng dụng

## 📚 Tài Liệu Tham Khảo

1. **WHO guideline on school health services (2021):** https://www.who.int/publications/i/item/9789240029392

2. **Chăm sóc 'Sức khỏe học đường giai đoạn 2021 – 2025' toàn diện cả thể chất và tinh thần:** https://bvquan5.medinet.gov.vn/tin-moi/cham-soc-suc-khoe-hoc-duong-giai-doan-2021-2025-toan-dien-ca-the-chat-va-tinh-t-cmobile14475-54813.aspx

3. **Schools & Health: Our Nation's Investment:** https://www.ncbi.nlm.nih.gov/books/NBK232689

---

## 🤝 Đóng Góp

Chúng tôi hoan nghênh mọi đóng góp cho dự án! Để đóng góp:

### Quy Trình Đóng Góp
1. **Fork repository** và tạo branch mới từ `main`
2. **Tạo feature branch** (`git checkout -b feature/AmazingFeature`)
3. **Commit changes** với message rõ ràng (`git commit -m 'Add some AmazingFeature'`)
4. **Push to branch** (`git push origin feature/AmazingFeature`)
5. **Tạo Pull Request** với mô tả chi tiết

### Coding Standards
- Tuân thủ C# Coding Conventions
- Sử dụng meaningful names cho variables và methods
- Viết unit tests cho các chức năng mới
- Cập nhật documentation khi cần thiết

### Code Review Process
- Mọi PR cần ít nhất 2 approvals
- Chạy tất cả tests trước khi merge
- Kiểm tra code quality và performance
- Đảm bảo không có security vulnerabilities

## 📞 Liên Hệ và Hỗ Trợ

### Thông Tin Liên Hệ
- **Project Repository:** [[GitHub Repository Link]](https://github.com/zienk/PRN-School-Medical-System)
- **Team Email:** zienkdev@gmail.com
- **Project Manager:** ZienK - zienkdev@gmail.com

### Báo Cáo Lỗi và Yêu Cầu Tính Năng
- **GitHub Issues:** [Link to issues page]
- **Bug Reports:** Sử dụng bug report template
- **Feature Requests:** Sử dụng feature request template

### Hỗ Trợ Kỹ Thuật
- **Documentation:** Xem Wiki của project
- **FAQ:** Câu hỏi thường gặp trong Issues
- **Community:** Discussions tab trên GitHub

## 📝 Giấy Phép

Dự án này được phát hành dưới **Giấy phép MIT**. Xem file [LICENSE](LICENSE) để biết thêm chi tiết.

```
MIT License

Copyright (c) 2024 School Medical Management System Team

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

---

## 🏆 Lời Cảm Ơn

Trong suốt hành trình khám phá và học tập về phát triển ứng dụng đa nền tảng với .NET, chúng tôi vô cùng biết ơn vì trải nghiệm học tập quý báu mà khóa học này đã mang lại. Nó không chỉ làm phong phú thêm kiến thức kỹ thuật của chúng tôi mà còn tăng cường khả năng quản lý công việc độc lập, giải quyết các vấn đề thực tế và tư duy sáng tạo như một đội nhóm.

Trước hết, chúng tôi muốn bày tỏ lòng biết ơn chân thành đến **ThS. Trần Duy Thanh**, giảng viên của khóa học, vì sự hướng dẫn tận tâm và kiến thức sâu rộng của thầy. Sự hướng dẫn kiên nhẫn và những hiểu biết thực tế của thầy đã giúp chúng tôi áp dụng các khái niệm lý thuyết vào các dự án thực tế. Sự khuyến khích và hỗ trợ của thầy là điều cần thiết để giúp chúng tôi vượt qua những thách thức và duy trì động lực trong suốt dự án.

Ngoài ra, chúng tôi cũng muốn cảm ơn các tài nguyên trực tuyến và công cụ phát triển đã đóng vai trò quan trọng trong quá trình nghiên cứu và triển khai của chúng tôi. Khả năng tiếp cận và hiệu quả của những công cụ này đã nâng cao đáng kể việc thực hành và sáng tạo của chúng tôi trong quá trình phát triển.

Khóa học này đã là một hành trình ý nghĩa và bổ ích. Lời nói của chúng tôi có thể không đủ để diễn tả sự biết ơn đối với sự cố vấn và hỗ trợ mà chúng tôi đã nhận được. Một lần nữa, chúng tôi muốn gửi lời cảm ơn chân thành đến giảng viên của chúng tôi vì đã đồng hành với chúng tôi trong từng bước của hành trình này.

**Trân trọng,**  
**Team Brainrot**

---

## 🔗 Liên Kết Quan Trọng

- **📁 Source Code:** [GitHub Repository](https://github.com/zienk/PRN-School-Medical-System.git)
- **🖼️ UI Screenshots:** [Google Drive](https://drive.google.com/drive/folders/1MEwM9vxw1GRGEqqfF-pZ89ODsd4OXLSH?usp=drive_link)
- **🗄️ Database ERD:** [DBDiagram](https://dbdiagram.io/d/PRN_ERD_ReportProject-6882d7facca18e685ca56364)

---

⭐ **Nếu dự án này hữu ích cho việc học tập và nghiên cứu của bạn, hãy cho chúng tôi một star!** ⭐

**📊 Project Status:** `Completed` | **🎯 Final Report:** `SUMMER2025` | **📅 Completed:** `July 25, 2025`
