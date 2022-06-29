
---Xoa CSDL
use master
Drop Database phong
-----Tao CSDL 
create database phong
GO
use phong


GO
CREATE TABLE KHACHHANG
(
	MaKH INT IDENTITY(1,1),
	HoTen nVarchar(50) NOT NULL,
	Taikhoan Varchar(50) UNIQUE,
	Matkhau Varchar(50) NOT NULL,
	Email Varchar(100) UNIQUE,
	DiachiKH nVarchar(200),
	DienthoaiKH Varchar(50),	
	Ngaysinh DATETIME
	CONSTRAINT PK_Khachhang PRIMARY KEY(MaKH)
)


GO
Create Table LOAISP
(
	MaLoai int Identity(1,1),
	TenLoai nvarchar(50) NOT NULL,
	CONSTRAINT PK_LoaiSP PRIMARY KEY(MaLoai)
)

Create Table THUONGHIEU
(
	MaTH int identity(1,1),
	TenTH nvarchar(50) NOT NULL,
	Diachi NVARCHAR(200),
	DienThoai VARCHAR(50),
	CONSTRAINT PK_ThuongHieu PRIMARY KEY(MaTH)
)


Go
CREATE TABLE DONGHO
(
	Madongho INT IDENTITY(1,1),
	Tendongho NVARCHAR(100) NOT NULL,
	Giaban Decimal(18,0) CHECK (Giaban>=0),
	Mota NVarchar(Max),
	Anhbia VARCHAR(50),
	Ngaycapnhat DATETIME,
	Soluongton INT,
	MaLoai INT,
	MaTH INT,
	Constraint PK_Dongho Primary Key(Madongho),
	Constraint FK_LoaiSP Foreign Key(MaLoai) References LOAISP(MaLoai),
	Constraint FK_ThuongHieu Foreign Key(MaTH)References 
THUONGHIEU(MaTH)
)



GO
CREATE TABLE DONDATHANG
(
	MaDonHang INT IDENTITY(1,1),
	Dathanhtoan bit,
	Tinhtranggiaohang  bit,
	Ngaydat Datetime,
	Ngaygiao Datetime,	
	MaKH INT,
	CONSTRAINT FK_Khachhang FOREIGN KEY (MaKH) REFERENCES Khachhang(MaKH),
	CONSTRAINT PK_DonDatHang PRIMARY KEY (MaDonHang),
)


Go
CREATE TABLE CHITIETDONTHANG
(
	MaDonHang INT,
	MaDongHo INT,
	Soluong Int Check(Soluong>0),
	Dongia Decimal(18,0) Check(Dongia>=0),	
	CONSTRAINT PK_CTDatHang PRIMARY KEY(MaDonHang,Madongho),
	CONSTRAINT FK_Donhang FOREIGN KEY (Madonhang) REFERENCES DONDATHANG(Madonhang),
	CONSTRAINT FK_DongHo FOREIGN KEY (Madongho) REFERENCES DONGHO(Madongho),	
)

CREATE TABLE ADMIN
(
	UserAdmin varchar(30) primary key,
	PassAdmin varchar(30) not null,
	Hoten	nvarchar(50)
)
Insert into ADMIN values('admin', '123456', 'Trương Hoài Phong')
Insert into ADMIN values('user', '1234567', 'Phong Trio')