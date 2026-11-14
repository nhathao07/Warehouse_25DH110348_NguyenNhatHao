# Trình tự kiểm thử và chụp ảnh

Phiếu có 22 mã test. Chỉ đánh dấu **X** khi đã chạy thật và có ảnh cho đúng mã; nếu chưa chạy hoặc không đạt thì đánh dấu **O**. Ảnh chụp bằng Snipping Tool/Print Screen, giữ nguyên nội dung trình duyệt, Visual Studio hoặc SSMS và dán vào ô cùng mã trong `25DH110348_PhieuKiemThu.docx`.

## Trước khi chạy test

Mở SSMS, kết nối cùng instance đã cấu hình trong `Web.config`, rồi chạy lệnh chuẩn bị ngay trước từng test dưới đây. Mỗi truy vấn `UPDATE` chỉ đổi dữ liệu kiểm thử; không đổi cấu trúc bảng. Chạy lệnh `SELECT` đối chiếu sau các test ghi dữ liệu để có ảnh SQL thật.

```sql
USE Warehouse_25DH110348;
SELECT ProductID, ProductName, Category, Price, Quantity FROM dbo.Products ORDER BY ProductID;
```

Nếu đã xóa ID 4 ở test 2.2 thì dùng script SQL tạo lại bảng trước khi cần kiểm thử lại câu 2; hoặc thêm sản phẩm hàng hóa tồn 0 để thay thế.

## Câu 1

- **1.1:** Chạy script SQL. Chụp SSMS thể hiện database `Warehouse_25DH110348`, bảng `dbo.Products` và các cột/data mẫu. Có thể chụp riêng Object Explorer và kết quả SELECT.
- **1.2:** Chạy web, chụp trang danh sách với header (Nguyễn Nhật Hào + MSSV), sidebar, dữ liệu thật và footer (20/02/2007).

## Câu 2

- **2.1:** Dữ liệu mẫu ban đầu đã có Quantity 0, 3, 5. Trên danh sách, chụp các dòng và trạng thái Hết hàng, Sắp hết hàng, Còn hàng.
- **2.2:** Mở Delete cho sản phẩm tồn 5, gửi thao tác và chụp thông báo bị từ chối; sau đó mở sản phẩm tồn 0, xác nhận xóa và chụp danh sách sau xóa. Đối chiếu bản ghi trong SQL nếu cần.

## Câu 3

- **3.1:** Chạy `UPDATE dbo.Products SET Quantity=2 WHERE ProductID=1; UPDATE dbo.Products SET Quantity=5 WHERE ProductID=2;` Lọc 2–5; chụp thấy cả hai biên.
- **3.2:** Đặt ID 1,2 thuộc danh mục `Điện tử Nguyễn Nhật Hào 25DH110348` và ID 3 thuộc `Phụ kiện Nguyễn Nhật Hào 25DH110348`; đặt tồn lần lượt 2,4,3. Chọn danh mục Điện tử và khoảng 2–5; chụp kết quả chỉ có ID 1,2.
- **3.3:** Chuẩn bị ID 1–3 có tồn 2,5,3. Chụp một ảnh thứ tự giảm dần và một ảnh tăng dần trong cùng ô minh chứng.
- **3.4:** Nhập Từ=8, Đến=3 (và/hoặc số âm); chụp thông báo lỗi.

## Câu 4

- **4.1:** SSMS chạy `UPDATE dbo.Products SET Quantity=10 WHERE ProductID=1;`; nhập thêm 5; chụp trang có Quantity=15 và SSMS xác nhận.
- **4.2:** Chạy `UPDATE dbo.Products SET Quantity=10 WHERE ProductID=1;`. Nhập thêm 0 rồi -2; chụp thông báo validation và SSMS xác nhận tồn vẫn 10.
- **4.3:** Chạy `UPDATE dbo.Products SET Quantity=8 WHERE ProductID=1;`; ghi nhận hỏng 5; chụp kết quả còn 3 và trạng thái Sắp hết hàng.
- **4.4:** Chạy `UPDATE dbo.Products SET Quantity=5 WHERE ProductID=1;`; ghi nhận hỏng 8; chụp lỗi, rồi SSMS xác nhận vẫn 5.
- **4.5:** Chạy `UPDATE dbo.Products SET Quantity=5 WHERE ProductID=1;`; ghi nhận hỏng 5; chụp kết quả tồn 0 và trạng thái Hết hàng.

## Câu 5

- **5.1:** Chạy `UPDATE dbo.Products SET Quantity=10 WHERE ProductID=1; UPDATE dbo.Products SET Quantity=4 WHERE ProductID=2;`; chuyển 12; chụp lỗi và đối chiếu SQL A=10, B=4.
- **5.2:** Đặt lại `UPDATE dbo.Products SET Quantity=10 WHERE ProductID=1; UPDATE dbo.Products SET Quantity=4 WHERE ProductID=2;`; chuyển 3; chụp kết quả 7/7 và SQL.
- **5.3:** Thử chọn A làm cả nguồn và đích, rồi thử khác danh mục; chụp lỗi. Mở `TransfersController.cs` tại đoạn transaction và chụp cùng điều kiện từ chối.

## Câu 6

- **6.1:** Trang Thêm sản phẩm: gửi thiếu ProductName, rồi thiếu Category; chụp validation và không có sản phẩm mới.
- **6.2:** Gửi Price=0, rồi Quantity=-1; chụp validation và không có sản phẩm mới.
- **6.3:** Thêm `USB Nguyễn Nhật Hào 25DH110348`, danh mục `Phụ kiện Nguyễn Nhật Hào 25DH110348`, Price=75000, Quantity=3; chụp danh sách có ProductID tự tăng. Đối chiếu thêm bằng SELECT trong SSMS.

## Câu 7

- **7.1:** Mở Thống kê; chụp tổng số sản phẩm và số Quantity 1–4, đối chiếu với SQL.
- **7.2:** Chạy `UPDATE dbo.Products SET Quantity=0; UPDATE dbo.Products SET Quantity=2 WHERE ProductID=1; UPDATE dbo.Products SET Quantity=5 WHERE ProductID=2;`; chụp tổng số đơn vị tồn kho là 7.
- **7.3:** Sau khi thiết lập tổng 7 ở test 7.2, nhập thêm 3 vào ID 1 rồi tải lại thống kê; chụp tổng tăng từ 7 lên 10.

> Các test có thay đổi dữ liệu nên chuẩn bị lại trạng thái trước mỗi lần chạy. Ghi ProductID/tên hàng vào phiếu để ảnh dễ đối chiếu.
