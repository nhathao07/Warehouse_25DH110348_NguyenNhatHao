# Warehouse_25DH110348

Project MVC 5 (.NET Framework 4.8) dùng Entity Framework 6 Database First, theo đề Ca 3. Tên sinh viên trong dữ liệu mẫu: **Nguyễn Nhật Hào**, MSSV **25DH110348**, ngày sinh **20/02/2007**.

## Bắt đầu trên máy có Visual Studio và SQL Server

1. Mở SQL Server Management Studio, kết nối tới `localhost\SQLEXPRESS` (hoặc instance SQL Server đang dùng).
2. Mở và chạy `Database/Warehouse_25DH110348.sql`. Lưu lại ảnh kết quả gồm bảng `Products` và 5 dòng dữ liệu; đây là minh chứng 1.1.
3. Nếu dùng instance khác SQLEXPRESS, sửa `data source` trong `Warehouse_25DH110348/Web.config` cho khớp.
4. Mở `Warehouse_25DH110348.sln` bằng Visual Studio 2022. Restore NuGet packages, build project.
5. Chạy bằng IIS Express. Trước khi bắt đầu test, chụp trang danh sách có header, sidebar, ngày sinh ở footer và dữ liệu lấy từ DB làm minh chứng 1.2.
6. Làm các mã test theo thứ tự trong `PhieuKiemThu_Guide.md`. Sau mỗi mã, chụp ảnh màn hình thật (không chỉnh sửa/nội suy), dán vào đúng khung mã đó trong phiếu Word và đánh dấu X chỉ khi kết quả đạt, có ảnh.

## Quy ước dữ liệu test

Script SQL nạp sẵn 5 dòng: Laptop (ID 1, tồn 10) và Màn hình (ID 2, tồn 4) cùng danh mục; Chuột (ID 3, tồn 3), Bàn phím (ID 4, tồn 0), Tai nghe (ID 5, tồn 5) ở danh mục phụ kiện. Tên hàng và danh mục có đủ họ tên, MSSV. Có thể dùng các lệnh `UPDATE dbo.Products SET Quantity = ... WHERE ProductID = ...` trong hướng dẫn để chuẩn bị lại dữ liệu cho từng test.

Project sử dụng EF6 EDMX với connection string `WarehouseEntities`. EDMX và mã lớp/context Database First nằm trong `Models/WarehouseModel.edmx` và các file sinh kèm. Script tạo cơ sở dữ liệu là `Database/Warehouse_25DH110348.sql`.

## GitHub

Chưa có repository GitHub hoặc link remote trong phiếu. Sau khi mở project, tạo repository GitHub riêng rồi chạy trong thư mục `outputs/Warehouse_25DH110348`:

```powershell
git init
git add .
git commit -m "Hoan thanh bai thi quan ly kho"
git branch -M main
git remote add origin <URL-repository-cua-ban>
git push -u origin main
```

Sau đó dán URL repository vào dòng Link GitHub của phiếu.

## Giới hạn bàn giao hiện tại

Máy chạy Codex không có Visual Studio hoặc dịch vụ SQL Server, nên project chưa được chạy end-to-end và các mã test chưa được xác nhận ở runtime. Vì vậy phiếu chưa được đánh dấu X và không có ảnh minh chứng giả. Chạy và chụp trên máy có SQL Server/Visual Studio để hoàn thiện bài nộp.
