CREATE DATABASE QL_DONHANG;
GO
USE QL_DONHANG;
GO

CREATE TABLE CHUDE (
    MaChuDe VARCHAR(10) PRIMARY KEY NOT NULL,
    TenChuDe NVARCHAR(100)
);

CREATE TABLE NHAXUATBAN (
    MaNXB VARCHAR(10) PRIMARY KEY NOT NULL,
    TenNXB NVARCHAR(100),
    DiaChi NVARCHAR(100),
    DienThoai CHAR(12)
);

CREATE TABLE TACGIA (
    MaTacGia VARCHAR(10) PRIMARY KEY NOT NULL,
    TenTacGia NVARCHAR(100),
    DiaChi NVARCHAR(100),
    TieuSu NVARCHAR(MAX),
    DienThoai CHAR(12)
);

CREATE TABLE KHACHHANG (
    MaKH VARCHAR(10) PRIMARY KEY NOT NULL,
    HoTen NVARCHAR(100),
    NgaySinh DATE,
    GioiTinh NVARCHAR(10),
    DienThoai CHAR(12),
    TaiKhoan NVARCHAR(100),
    MatKhau NVARCHAR(255),
    Email NVARCHAR(255),
    DiaChi NVARCHAR(100)
);

CREATE TABLE DONHANG (
    MaDonHang VARCHAR(10) PRIMARY KEY NOT NULL,
    NgayGiao DATE,
    NgayDat DATE,
    DaThanhToan BIT,
    TinhTrangGiaoHang NVARCHAR(100),
    MaKH VARCHAR(10) NOT NULL,
    FOREIGN KEY (MaKH) REFERENCES KHACHHANG(MaKH)
);

CREATE TABLE SACH (
    MaSach VARCHAR(10) PRIMARY KEY NOT NULL,
    TenSach NVARCHAR(100),
    GiaBan DECIMAL(18,2),
    MoTa NVARCHAR(MAX),
    NgayCapNhat DATE,
    AnhBia VARCHAR(255),
    SoLuongTon INT,
    MaChuDe VARCHAR(10) NOT NULL,
    MaNXB VARCHAR(10) NOT NULL,
    Moi BIT,
    FOREIGN KEY (MaChuDe) REFERENCES CHUDE(MaChuDe),
    FOREIGN KEY (MaNXB) REFERENCES NHAXUATBAN(MaNXB)
);

CREATE TABLE THAMGIA (
    MaSach VARCHAR(10) NOT NULL,
    MaTacGia VARCHAR(10) NOT NULL,
    VaiTro NVARCHAR(50),
    ViTri NVARCHAR(50),
    PRIMARY KEY (MaSach, MaTacGia),
    FOREIGN KEY (MaSach) REFERENCES SACH(MaSach),
    FOREIGN KEY (MaTacGia) REFERENCES TACGIA(MaTacGia)
);

CREATE TABLE ChiTietDonHang (
    MaDonHang VARCHAR(10) NOT NULL,
    MaSach VARCHAR(10) NOT NULL,
    SoLuong INT,
    DonGia DECIMAL(18,2),
    PRIMARY KEY (MaDonHang, MaSach),
    FOREIGN KEY (MaDonHang) REFERENCES DONHANG(MaDonHang),
    FOREIGN KEY (MaSach) REFERENCES SACH(MaSach)
);

CREATE TABLE KHACHHANG (
    MaKH INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100),
    TaiKhoan NVARCHAR(50) UNIQUE,
    MatKhau NVARCHAR(50),
    DiaChi NVARCHAR(200),
    DienThoai NVARCHAR(12),
    Email NVARCHAR(100)
);

-- Dữ liệu mẫu
INSERT INTO CHUDE VALUES
('CD01', N'Sách Triết Học'),
('CD02', N'Sách Thiếu Nhi'),
('CD03', N'Sách Truyện Tranh');

INSERT INTO NHAXUATBAN VALUES
('NXB01', N'Triết Học Đổi Mới', N'140 Lê Trọng Tấn', '0123456789'),
('NXB02', N'Sản Xuất Truyện Thiếu Nhi', N'140 Lê Trọng Tấn', '0123498756'),
('NXB03', N'Truyện Tranh Manga', N'140 Lê Trọng Tấn', '0987654321'),
('NXB04', N'Xuất Bản Hành Động', N'140 Lê Trọng Tấn', '0112233445');

INSERT INTO KHACHHANG VALUES
('KH01', N'Nguyễn Thị Thu', '1990-01-15', N'Nữ', '0912345678', 'thunt', 'mkthunt', 'thunt@example.com', N'123 Nguyễn Huệ, Q1, TP.HCM'),
('KH02', N'Lê Văn An', '1985-03-22', N'Nam', '0987654321', 'anlv', 'mkanlv', 'anlv@example.com', N'456 Trần Hưng Đạo, Q5, TP.HCM'),
('KH03', N'Phạm Minh Duy', '1995-07-01', N'Nam', '0901122334', 'duypm', 'mkduypm', 'duypm@example.com', N'789 Cách Mạng Tháng 8, Q3, TP.HCM'),
('KH04', N'Trần Thị Mai', '1998-11-10', N'Nữ', '0909887766', 'maitt', 'mkmaitt', 'maitt@example.com', N'101 Nguyễn Trãi, Q1, TP.HCM');

INSERT INTO TACGIA VALUES
('TG01', N'Nguyễn Nhật Ánh', N'TP.HCM', N'Nhà văn chuyên viết truyện thiếu nhi', '0903112233'),
('TG02', N'Adam Smith', N'Edinburgh, Scotland', N'Nhà kinh tế học và triết gia', '0011223344'),
('TG03', N'Gosho Aoyama', N'Tỉnh Tottori, Nhật Bản', N'Họa sĩ truyện tranh', '0055667788'),
('TG04', N'Yuval Noah Harari', N'Israel', N'Nhà sử học và tác giả', '0099887766');

INSERT INTO SACH VALUES
('S001', N'Tôi Thấy Hoa Vàng Trên Cỏ Xanh', 120000, N'Truyện dài của Nguyễn Nhật Ánh', '2023-01-10', 'hoavang.jfif', 150, 'CD02', 'NXB02', 0),
('S002', N'Của Cải Các Dân Tộc', 250000, N'Tác phẩm kinh điển về kinh tế học', '2022-11-20', 'cuacaivdt.jfif', 80, 'CD01', 'NXB01', 1),
('S003', N'Thám Tử Lừng Danh Conan Tập 1', 95000, N'Truyện tranh trinh thám nổi tiếng', '2023-02-05', 'conan01.jfif', 200, 'CD03', 'NXB03', 0),
('S004', N'Sapiens: Lược Sử Loài Người', 200000, N'Cuốn sách lịch sử loài người', '2023-03-15', 'sapiens.png', 120, 'CD01', 'NXB01', 0),
('S005', N'Mắt Biếc', 110000, N'Một câu chuyện tình yêu lãng mạn', '2023-04-01', 'matbiec.jfif', 100, 'CD02', 'NXB02', 0);

INSERT INTO THAMGIA VALUES
('S001', 'TG01', N'Tác giả chính', N'Chủ biên'),
('S002', 'TG02', N'Tác giả chính', N'Chủ biên'),
('S003', 'TG03', N'Tác giả chính', N'Họa sĩ và cốt truyện'),
('S004', 'TG04', N'Tác giả chính', N'Chủ biên'),
('S005', 'TG01', N'Tác giả chính', N'Chủ biên');

INSERT INTO DONHANG VALUES
('DH001', '2023-05-10', '2023-05-08', 0, N'Đã giao', 'KH01'),
('DH002', '2023-05-12', '2023-05-09', 1, N'Đang vận chuyển', 'KH02'),
('DH003', '2023-05-15', '2023-05-11', 0, N'Đang xử lý', 'KH03'),
('DH004', '2023-05-16', '2023-05-12', 0, N'Đã giao', 'KH01');

INSERT INTO ChiTietDonHang VALUES
('DH001', 'S001', 1, 120000),
('DH001', 'S003', 2, 95000),
('DH002', 'S002', 1, 250000),
('DH003', 'S004', 1, 200000),
('DH004', 'S005', 1, 110000),
('DH004', 'S001', 1, 120000);

DROP TABLE ChiTietDonHang;
DROP TABLE THAMGIA;
DROP TABLE DONHANG;
DROP TABLE SACH;
DROP TABLE KHACHHANG;
DROP TABLE NHAXUATBAN;
DROP TABLE CHUDE;
DROP TABLE TACGIA;

