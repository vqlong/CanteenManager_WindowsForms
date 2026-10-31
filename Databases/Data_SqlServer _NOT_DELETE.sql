CREATE DATABASE CanteenManager
GO 

USE CanteenManager
GO

--Food: Các loại món ăn đồ uống, ví dụ: Nem chua rán, Cam, Quýt, Bia...
--Table: Bàn ăn có thể 'Trống' hoặc 'Có người', nếu bàn ăn 'Có người' thì nó sẽ có 1 Bill chưa thanh toán kèm theo
--FoodCategory: Danh mục, ví dụ: Hoa quả, Món ăn, Nước... mỗi loại món ăn đồ uống sẽ thuộc về 1 danh mục nào đó
--Account: Tài khoản
--Bill: Hoá đơn, bao gồm: ngày CheckIn/CheckOut, bàn phát sinh hoá đơn, trạng thái đã thanh toán/chưa thanh toán...
--BillInfo: Chi tiết cho mỗi hoá đơn: các món ăn, số lượng...	

--Thằng nào ít bị ràng buộc thì tạo trước

/*

CREATE TABLE TableFood
(
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
    TableStatus NVARCHAR(100) NOT NULL DEFAULT N'Trống', --Trống || Có người
    UsingState INT NOT NULL DEFAULT 1 --1: Đang dùng && 0: Không dùng
)
GO

CREATE TABLE Account
(	
    [Username] NVARCHAR(100) PRIMARY KEY,
    [DisplayName] NVARCHAR(100) NOT NULL DEFAULT N'CafeNo1',
    [Password] NVARCHAR(MAX) NOT NULL DEFAULT N'952362351022552001115621782120108109105108121194219194572217814518010341215583925187233',
    [Type] INT NOT NULL DEFAULT 0 --1: Admin && 0: Staff 
)
GO 

CREATE TABLE FoodCategory
(
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
    CategoryStatus INT NOT NULL DEFAULT 1 --1: Đang bán && 0: Không bán
)
GO

CREATE TABLE Food
(
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
    CategoryId INT NOT NULL,
    Price FLOAT NOT NULL DEFAULT 0,
    FoodStatus INT NOT NULL DEFAULT 1 --1: Đang bán && 0: Không bán

    FOREIGN KEY (CategoryId) REFERENCES dbo.FoodCategory(Id)
)
GO 

CREATE TABLE Bill
(
    Id INT IDENTITY PRIMARY KEY,
    DateCheckIn DATETIME NOT NULL DEFAULT GETDATE(),
    DateCheckOut DATETIME,
    TableId INT NOT NULL,
    BillStatus INT NOT NULL DEFAULT 0, --1: đã thanh toán && 0: chưa thanh toán
    Discount INT NOT NULL DEFAULT 0,
    TotalPrice FLOAT NOT NULL DEFAULT 0

    FOREIGN KEY (TableId) REFERENCES dbo.TableFood(Id)
)
GO	

CREATE TABLE BillInfo
(
    Id INT IDENTITY PRIMARY KEY,
    BillId INT NOT NULL,
    FoodId INT NOT NULL,
    FoodCount INT NOT NULL DEFAULT 0

    FOREIGN KEY (BillId) REFERENCES dbo.Bill(Id),
    FOREIGN KEY (FoodId) REFERENCES dbo.Food(Id)
)
GO	

*/
----------------------------------------------------------------------------------------

DROP TABLE Account

--proc kiểm tra thông tin đăng nhập
ALTER PROC USP_Login
@username NVARCHAR(100),
@password NVARCHAR(100)
AS
BEGIN
    SELECT Username, DisplayName, [Type] FROM dbo.Account WHERE Username = @username AND [Password] = @password
END
GO 

EXEC USP_Login admin, 123
GO 

--Tạo dữ liệu TableFood
DECLARE @i INT = 1
WHILE @i < 31
BEGIN
    INSERT dbo.TableFood ( Name ) VALUES  ( N'Bàn ' + CAST(@i AS NVARCHAR(100)))
    SET @i += 1
END
GO

--proc tạo bàn mới
CREATE PROC USP_InsertTable
AS
INSERT TableFood(Name) VALUES(N'Bàn ' + CAST((SELECT COUNT(*) FROM TableFood) + 1 AS NVARCHAR(100)))
GO

--proc lấy danh sách bàn ăn
CREATE PROC USP_GetListTable
AS
SELECT * FROM dbo.TableFood
GO

--proc lấy danh sách bàn ăn trong trạng thái còn sử dụng
CREATE PROC USP_GetListTableUsing
AS
SELECT * FROM TableFood WHERE UsingState = 1
GO

/* Tạo dữ liệu ban đầu

--Tạo dữ liệu FoodCategory
INSERT FoodCategory(Name) VALUES(N'Món ăn')
INSERT FoodCategory(Name) VALUES(N'Hoa quả')
INSERT FoodCategory(Name) VALUES(N'Nước')

--Tạo dữ liệu Food
INSERT Food VALUES(N'Tiết luộc măng chua', 1, 15000, 1)
INSERT Food VALUES(N'Cánh gà nướng', 1, 25000, 1) 
INSERT Food VALUES(N'Ốc xào xả ớt', 1, 18000, 1)
INSERT Food VALUES(N'Nem chua rán', 1, 19000, 1)
INSERT Food VALUES(N'Cam', 2, 8000, 1)
INSERT Food VALUES(N'Quýt', 2, 7000, 1) 
INSERT Food VALUES(N'Mít', 2, 6000, 1)
INSERT Food VALUES(N'Dừa', 2, 9000, 1)
INSERT Food VALUES(N'Dưa', 2, 5000, 1)
INSERT Food VALUES(N'Dứa', 2, 2000, 1) 
INSERT Food VALUES(N'Ổi', 2, 3000, 1)
INSERT Food VALUES(N'Cocacola', 3, 10000, 1)
INSERT Food VALUES(N'Pepsi', 3, 11000, 1)
INSERT Food VALUES(N'Bia', 3, 12000, 1) 
INSERT Food VALUES(N'Sữa chua mít', 3, 13000, 1)

--Tạo dữ liệu Bill
INSERT Bill VALUES(GETDATE(), NULL, 16, 0)
INSERT Bill VALUES(GETDATE(), NULL, 18, 0)
INSERT Bill VALUES(GETDATE(), NULL, 20, 0)

--Tạo dữ liệu BillInfo
INSERT BillInfo VALUES(1, 1, 3)
INSERT BillInfo VALUES(1, 2, 3)
INSERT BillInfo VALUES(1, 15, 3)
INSERT BillInfo VALUES(2, 3, 5)
INSERT BillInfo VALUES(2, 8, 6)
INSERT BillInfo VALUES(3, 14, 7)
INSERT BillInfo VALUES(3, 4, 10)

*/

GO


--proc thêm Bill mới
--  ID: tự động thêm do ràng buộc IDENTITY
--	DateCheckIn: luôn là ngày hôm nay
--	DateCheckOut: luôn là NULL do hoá đơn mới tạo, chưa thanh toán
--	TableID: ID của bàn phát sinh hoá đơn
--	BillStatus: luôn là 0 - chưa thanh toán
--  Discount: luôn là 0 khi thêm Bill mới, sau khi thanh toán mới tính vào
CREATE PROC USP_InsertBill
@tableId INT
AS
BEGIN
    --Thêm 1 Bill mới
    INSERT Bill VALUES(GETDATE(), NULL, @tableId, 0, 0, 0)
    --Khi 1 bàn có Bill mới, nó sẽ chuyển sang trạng thái 'Có người'
    --UPDATE TableFood SET TableStatus = N'Có người' WHERE TableFood.ID = @tableID
END
GO

--proc thanh toán Bill
CREATE PROC USP_CheckOut
@billId INT,
@discount INT,
@totalPrice FLOAT
AS
BEGIN
    --Khi Bill được thanh toán => BillStatus = 1
    UPDATE Bill SET BillStatus = 1, DateCheckOut = GETDATE(), Discount = @discount, TotalPrice = @totalPrice  WHERE Id = @billId

    --DECLARE @tableID INT
    --Theo thiết kế mỗi Bill chỉ có 1 ID duy nhất => kết quả trả về luôn chỉ có 1 hàng => Dùng hàm MAX/MIN để lấy được TableID của Bill
    --SELECT @tableID = MAX(Bill.TableID) FROM Bill WHERE Bill.ID = @billID
    --Khi 1 bàn được thanh toán Bill, nó sẽ chuyển sang trạng thái 'Trống'
    --UPDATE TableFood SET TableStatus = N'Trống' WHERE TableFood.ID = @tableID
END
GO

--proc thêm BillInfo mới
CREATE PROC USP_InsertBillInfo
@billId INT,
@foodId INT,
@foodCount INT
AS
BEGIN
    DECLARE @isExistBillInfo INT
    DECLARE @currentFoodCount INT

    --Kiểm tra xem cái BillInfo này có tồn tại không (cái Bill này đã có BillInfo nào chưa, nếu có thì đã có món ăn này chưa)
    --Nếu tồn tại thì kết quả trả về cũng chỉ có 1 hàng duy nhất => Dùng hàm MAX hoặc MIN để lấy được FoodCount của món ăn
    SELECT @isExistBillInfo = COUNT(*), @currentFoodCount = MAX(FoodCount) FROM BillInfo WHERE BillInfo.BillId = @billId AND BillInfo.FoodId = @foodId

    --Nếu đã tồn tại thì update số lượng món đã gọi
    --Nếu không thì thêm mới
    IF (@isExistBillInfo > 0)
        BEGIN
            --Theo thiết kế @foodCount truyền vào có thể âm, nếu @newFoodCount <= 0 thì xoá món đó khỏi hoá đơn
            DECLARE @newFoodCount INT = @currentFoodCount + @foodCount
            IF (@newFoodCount <= 0)
                DELETE BillInfo WHERE BillInfo.BillId = @billId AND BillInfo.FoodId = @foodId
            ELSE
                UPDATE BillInfo SET FoodCount = @newFoodCount WHERE BillInfo.BillId = @billId AND BillInfo.FoodId = @foodId
        END
    ELSE
        --Nếu @foodCount > 0 (người dùng chọn số món > 0) mới thực hiện thêm
        IF (@foodCount > 0)
            BEGIN
                INSERT BillInfo VALUES(@billId, @foodId, @foodCount)
            END
    
END
GO

--Proc chuyển bàn
--(bằng cách tráo đổi TableID của 2 Bill)
CREATE PROC USP_SwapTable
@firstTableId INT,
@secondTableId INT
AS
BEGIN
    DECLARE @firstBillId INT = 0
    DECLARE @secondBillId INT = 0
    --Lấy ra BillID của Bill chưa thanh toán trên 2 bàn cần chuyển chỗ cho nhau
    SELECT @firstBillId = Bill.Id FROM Bill WHERE TableId = @firstTableId AND BillStatus = 0
    SELECT @secondBillId = Bill.Id FROM Bill WHERE TableId = @secondTableId AND BillStatus = 0
    
    IF(@firstBillId != 0) 
    --Nếu bàn thứ nhất được chọn có người thì mới tiến hành chuyển
        IF(@secondBillId !=0) 
        --Nếu bàn thứ hai được chọn cũng có người => tráo đổi TableID của 2 Bill cho nhau
            BEGIN
                UPDATE Bill SET TableId = @firstTableId WHERE Id = @secondBillId
                UPDATE Bill SET TableId = @secondTableId WHERE Id = @firstBillId
            END
        ELSE
        --Nếu bàn thứ hai được chọn không có người => chỉ cần thay đổi TableID của Bill
            BEGIN
                UPDATE Bill SET TableId = @secondTableId WHERE Id = @firstBillId
            END
    
END
GO
  
--Proc gộp bàn thứ 1 vào bàn thứ 2
CREATE PROC USP_CombineTable
@firstTableId INT,
@secondTableId INT
AS
BEGIN
    DECLARE @firstBillId INT = 0
    DECLARE @secondBillId INT = 0
    --Lấy ra BillID của Bill chưa thanh toán trên 2 bàn cần gộp vào nhau
    SELECT @firstBillId = Bill.Id FROM Bill WHERE TableId = @firstTableId AND BillStatus = 0
    SELECT @secondBillId = Bill.Id FROM Bill WHERE TableId = @secondTableId AND BillStatus = 0

    --Nếu 2 bàn đều có người thì mới tiến hành gộp
    IF(@firstBillId != 0 AND @secondBillId != 0)
        BEGIN
            --Gộp bàn thứ 1 vào bàn thứ 2 bằng cách thay BillID trong bảng BillInfo
            UPDATE BillInfo SET BillId = @secondBillId WHERE BillId = @firstBillId

            --Khi gộp bàn sẽ xuất hiện các món trùng lặp với nhau
            --Tạo con trỏ và lấy ra các FoodID với số lần trùng lặp
            DECLARE BillInfoCursor CURSOR FOR SELECT FoodId, count(*) AS 'Count' FROM BillInfo WHERE BillId = @secondBillId GROUP BY FoodId
            OPEN BillInfoCursor

            DECLARE @foodId INT
            DECLARE @count INT

            FETCH NEXT FROM BillInfoCursor INTO @foodId, @count

            WHILE @@FETCH_STATUS = 0
                BEGIN
                    --Trường hợp @count > 1 tức là món này bị trùng nhau, xuất hiện hơn 1 lần
                    IF(@count > 1) 
                        BEGIN
                            DECLARE @finalFoodCount INT = 0
                            --Tính gộp tổng các FoodCount của món này
                            SELECT @finalFoodCount = SUM(FoodCount) FROM BillInfo WHERE BillId = @secondBillId AND FoodId = @foodId
            
                            DECLARE @maxId INT = 0
                            --Lấy ra max ID của món này để tí nữa giữ lại, các ID khác xoá hết cho khỏi trùng nhau
                            SELECT @maxId = MAX(Id) FROM BillInfo WHERE BillId = @secondBillId AND FoodId = @foodId

                            --update số lượng món ăn cho ID này
                            UPDATE BillInfo SET FoodCount = @finalFoodCount WHERE BillId = @secondBillId AND FoodId = @foodId AND Id = @maxId --Cài nhiều điều kiện cho chắc kèo

                            --Xoá các ID còn lại
                            DELETE BillInfo WHERE BillId = @secondBillId AND FoodId = @foodId AND Id != @maxId
            
                        END

                    FETCH NEXT FROM BillInfoCursor INTO @foodId, @count
                END

            CLOSE BillInfoCursor
            DEALLOCATE BillInfoCursor

        END
END
GO

/*  Trigger

    Theo thiết kế, người dùng có thể nhập số lượng món < 0 để trừ bớt số món ăn trên bàn
    => người dùng có thể trừ hết sạch các món ăn trên bàn
    => lúc này có thể coi là bàn đã trống mặc dù chưa thanh toán (chưa chạy USP_CheckOut)
    => Dùng Trigger để chuyển trạng thái của bàn thay vì chạy kèm UPDATE TableFood trong 2 proc USP_InsertBill và USP_CheckOut
    => Trigger sẽ chạy bất cứ khi nào có INSERT, UPDATE, DELETE trên bảng, phải cài vào đầy đủ các điều kiện ràng buộc để 
       Trigger chạy đúng vào hàng cần thay đổi

    Trigger cũng chuyển trạng thái của bàn khi tiến hành chuyển bàn, gộp bàn
    -Chuyển bàn:
     +Trường hợp 2 bàn đều có người: chỉ cần tráo đổi TableID của Bill (chưa thanh toán) trên mỗi bàn => proc USP_SwapTable
     +Trường hợp bàn có người chuyển sang 1 bàn trống: vừa tráo đổi TableID của Bill vừa chuyển trạng thái mỗi bàn => proc USP_SwapTable + Trigger UTG_UpdateBill
    -Gộp bàn, gồm các bước:
     +Kiểm tra 2 bàn cần gộp đều phải có người
     +Gộp bàn 1 vào bàn 2 => trong bảng BillInfo, thay các BillID (Bill chưa thanh toán) của bàn 1 thành BillID của bàn 2 => proc USP_CombineTable
     +Xoá Bill (chưa thanh toán) của bàn 1 (lúc này không còn món nào), bàn 1 chuyển thành trống => Trigger UTG_UpdateBillInfo
*/
GO

--Trigger chuyển trạng thái của bàn sang 'Có người' mỗi khi nó được insert thêm BillInfo (thêm món)
CREATE TRIGGER UTG_InsertBillInfo
ON BillInfo
FOR INSERT
AS
BEGIN
    DECLARE @billId INT
    --Lấy ra BillID của BillInfo vừa được thêm
    SELECT @billId = BillId FROM inserted
    
    DECLARE @tableId INT
    --Lấy ra TableID của Bill có BillInfo vừa được thêm (phải là Bill chưa thanh toán)
    SELECT @tableId = TableId FROM Bill WHERE ID = @billId AND BillStatus = 0

    --Chuyển trạng thái của bàn mà Bill của nó vừa được thêm BillInfo
    UPDATE TableFood SET TableStatus = N'Có người' WHERE TableFood.Id = @tableId

END
GO 


--Hiện tại sử dụng lệnh UPDATE Bill cho 2 mục đích:
--  UPDATE để chuyển BillStatus = 1 khi thanh toán => Trigger chuyển trạng thái của bàn sang 'Trống' mỗi khi nó được thanh toán
--  UPDATE để thay đổi TableID khi chuyển bàn => Trigger lấy ra TableID cũ và mới, đếm số lượng Bill chưa được thanh toán trên bàn và chuyển trạng thái tương ứng
CREATE TRIGGER UTG_UpdateBill
ON Bill
FOR UPDATE
AS
BEGIN
    DECLARE @id INT
    --Lấy ra ID của Bill vừa được update
    SELECT @id = Id FROM inserted
    
    DECLARE @tableId INT
    --Lấy ra TableID của Bill vừa được update
    SELECT @tableId = TableId FROM Bill WHERE Id = @id

    DECLARE @countBill INT = 0
    --Đếm số lượng Bill chưa được thanh toán trên bàn
    SELECT @countBill = COUNT(*) FROM Bill WHERE TableId = @tableId AND BillStatus = 0

    IF (@countBill = 0)
        --Bàn mà không có Bill nào là chưa thanh toán => 'Trống'
        UPDATE TableFood SET TableStatus = N'Trống' WHERE TableFood.Id = @tableId
    ELSE
        UPDATE TableFood SET TableStatus = N'Có người' WHERE TableFood.Id = @tableId
    ----------------------------------------------------------------------------------------
    
    DECLARE @oldTableId INT
    --Lấy ra TableID cũ sau khi UPDATE (khi chuyển bàn)
    SELECT @oldTableId = TableId FROM deleted

    --Dùng lại biến @countBill
    SET @countBill = 0
    --Đếm số lượng Bill chưa được thanh toán trên bàn
    SELECT @countBill = COUNT(*) FROM Bill WHERE TableId = @oldTableId AND BillStatus = 0

    IF (@countBill = 0)
        --Bàn mà không có Bill nào là chưa thanh toán => 'Trống'
        UPDATE TableFood SET TableStatus = N'Trống' WHERE TableFood.Id = @oldTableId
        
END
GO 

--Trigger xoá Bill và chuyển trạng thái của bàn sang 'Trống' khi người dùng trừ hết sạch các món ăn trên bàn
CREATE TRIGGER UTG_DeleteBillInfo
ON BillInfo
FOR DELETE
AS
BEGIN
    --Lấy ra BillID của BillInfo vừa bị xoá
    DECLARE @billId INT
    SELECT @billId = BillId FROM deleted

    --Đếm số lượng BillInfo (số lượng món ăn) còn lại của Bill đó, nếu = 0 => xoá Bill và chuyển trạng thái của bàn sang 'Trống'
    DECLARE @countBillInfo INT = 0
    SELECT @countBillInfo = COUNT(BillId) FROM BillInfo WHERE BillId = @billId

    IF (@countBillInfo = 0)
        BEGIN
            DECLARE @tableId INT
            SELECT @tableId = TableId FROM Bill WHERE Id = @billId AND BillStatus = 0
            DELETE Bill WHERE Id = @billId
            UPDATE TableFood SET TableStatus = N'Trống' WHERE TableFood.Id = @tableId
        END
END
GO

--Trigger xoá Bill và chuyển trạng thái của bàn thứ 1 sang 'Trống' khi gộp bàn thứ 1 vào bàn thứ 2
CREATE TRIGGER UTG_UpdateBillInfo
ON BillInfo
FOR UPDATE
AS
BEGIN
    --Lấy ra BillID cũ của BillInfo vừa update
    DECLARE @billId INT
    SELECT @billId = BillId FROM deleted

    --Đếm số lượng BillInfo (số lượng món ăn) còn lại của Bill đó, nếu = 0 => xoá Bill và chuyển trạng thái của bàn sang 'Trống'
    DECLARE @countBillInfo INT = 0
    SELECT @countBillInfo = COUNT(BillId) FROM BillInfo WHERE BillId = @billId

    IF (@countBillInfo = 0)
        BEGIN
            DECLARE @tableId INT
            SELECT @tableId = TableId FROM Bill WHERE Id = @billId AND BillStatus = 0
            DELETE Bill WHERE Id = @billId
            UPDATE TableFood SET TableStatus = N'Trống' WHERE TableFood.Id = @tableId
        END
END
GO

--Test Cursor xoá các món trùng lặp khi gộp bàn
--Khi gộp bàn sẽ xuất hiện các món trùng lặp với nhau
--Tạo con trỏ và lấy ra các FoodID với số lần trùng lặp
DECLARE BillInfoCursor CURSOR FOR SELECT FoodId, count(*) AS 'Count' FROM BillInfo WHERE BillId = 17 GROUP BY FoodId
OPEN BillInfoCursor

DECLARE @foodId INT
DECLARE @count INT

FETCH NEXT FROM BillInfoCursor INTO @foodId, @count

WHILE @@FETCH_STATUS = 0
BEGIN
    --Trường hợp @count > 1 tức là món này bị trùng nhau, xuất hiện hơn 1 lần
    IF(@count > 1) 
        BEGIN
            DECLARE @finalFoodCount INT = 0
            --Tính gộp tổng các FoodCount của món này
            SELECT @finalFoodCount = SUM(FoodCount) FROM BillInfo WHERE BillId = 30 AND FoodId = @foodId
            
            DECLARE @maxId INT = 0
            --Lấy ra max ID của món này để tí nữa giữ lại, các ID khác xoá hết cho khỏi trùng nhau
            SELECT @maxId = MAX(Id) FROM BillInfo WHERE BillId = 30 AND FoodId = @foodId

            --update số lượng món ăn cho ID này
            UPDATE BillInfo SET FoodCount = @finalFoodCount WHERE BillId = 30 AND FoodId = @foodId AND Id = @maxId --Cài nhiều điều kiện cho chắc kèo

            --Xoá các ID còn lại
            DELETE BillInfo WHERE BillId = 30 AND FoodId = @foodId AND Id != @maxId
            
        END

    FETCH NEXT FROM BillInfoCursor INTO @foodId, @count
END

CLOSE BillInfoCursor
DEALLOCATE BillInfoCursor
GO 

--Proc lấy danh sách các hoá đơn dựa theo ngày truyền vào
CREATE PROC USP_GetListBillByDate
@fromDate DATETIME,
@toDate DATETIME
AS
BEGIN
    SELECT 
    Bill.Id, 
    Bill.DateCheckIn AS [Ngày phát sinh], 
    Bill.DateCheckOut AS [Ngày thanh toán], 
    TableFood.Name AS [Tên bàn], 
    Bill.Discount AS [Giảm giá (%)], 
    Bill.TotalPrice AS [Tiền thanh toán (Vnđ)]
    FROM Bill, TableFood 
    WHERE DateCheckIn >= @fromDate AND DateCheckOut <= @toDate AND BillStatus = 1 AND TableFood.Id = Bill.TableId
END
GO

--Proc lấy danh sách các hoá đơn dựa theo ngày và số trang truyền vào
CREATE PROC USP_GetListBillByDateAndPage
@fromDate DATETIME,
@toDate DATETIME,
@pageNumber INT = 1, --Page muốn lấy về
@pageSize INT = 10 --Số dòng 1 page
AS
BEGIN
    DECLARE @totalRow INT = (SELECT COUNT(*) FROM Bill WHERE DateCheckIn >= @fromDate AND DateCheckOut <= @toDate AND BillStatus = 1) --Lấy tổng số dòng thoả mãn ngày truyền vào
    DECLARE @selectRow INT = @pageNumber * @pageSize --Lấy số dòng chứa page cần lấy
    DECLARE @exceptRow INT = (@pageNumber - 1) * @pageSize; --Lấy số dòng sẽ loại trừ ra bằng EXCEPT
    
    --IF @exceptRow < @totalRow
        --BEGIN
            WITH temp AS
            (
                SELECT 
                Bill.Id, 
                Bill.DateCheckIn AS [Ngày phát sinh], 
                Bill.DateCheckOut AS [Ngày thanh toán], 
                TableFood.Name AS [Tên bàn], 
                Bill.Discount AS [Giảm giá (%)], 
                Bill.TotalPrice AS [Tiền thanh toán (Vnđ)]
                FROM Bill, TableFood 
                WHERE DateCheckIn >= @fromDate AND DateCheckOut <= @toDate AND BillStatus = 1 AND TableFood.Id = Bill.TableId
            )
            SELECT TOP (@selectRow) * FROM temp 
            EXCEPT 
            SELECT TOP (@exceptRow) * FROM temp
        --END
END
GO

--Proc update DisplayName, PassWord và AccType cho Account
CREATE PROC [dbo].[USP_UpdateAccount]
@username NVARCHAR(100),
@displayname NVARCHAR(100) = NULL,
@password NVARCHAR(MAX) = NULL,
@type INT = NULL
AS
BEGIN
	UPDATE Account SET Account.DisplayName = @displayname WHERE Username = @username AND @displayname IS NOT NULL AND @displayname != ''
	UPDATE Account SET Account.[Password] = @password WHERE Username = @username AND @password IS NOT NULL AND @password != ''
	UPDATE Account SET Account.[Type] = @type WHERE Username = @username AND @type IS NOT NULL

	DECLARE @resultDisplayName int = 0
	DECLARE @resultPassword int = 0
	DECLARE @resultType int = 0

	SELECT @resultDisplayName = (SELECT COUNT(*) FROM Account WHERE Username = @username AND DisplayName = @displayname)
	SELECT @resultPassword = (SELECT COUNT(*) FROM Account WHERE Username = @username AND [Password] = @password)
	SELECT @resultType = (SELECT COUNT(*) FROM Account WHERE Username = @username AND [Type] = @type)

	SELECT @resultDisplayName AS ResultDisplayName, @resultPassword AS ResultPassword, @resultType AS ResultType
END
GO

EXEC USP_UpdateAccount 'nv01', N'', '123'
GO

--Proc thêm món ăn cho Food
CREATE PROC USP_InsertFood
@name NVARCHAR(100),
@categoryId INT,
@price FLOAT
AS
BEGIN
    INSERT Food(Name, CategoryId, Price) VALUES(@name, @categoryId, @price)
END
GO

EXEC USP_InsertFood N'Nhặng rang', 1, 99999
GO

--Proc update món ăn cho Food
CREATE PROC USP_UpdateFood
@id INT,
@name NVARCHAR(100),
@categoryId INT,
@price FLOAT,
@foodStatus INT
AS
BEGIN
    UPDATE Food SET Food.Name = @name, Food.CategoryId = @categoryId, Food.Price = @price, Food.FoodStatus = @foodStatus WHERE Food.Id = @id
END
GO

DECLARE @foodName NVARCHAR(100)
SET @foodName = N'ốc'
SELECT * FROM Food WHERE Name LIKE '%' + @foodName + '%' ORDER BY CategoryId ASC
SELECT * FROM Food WHERE dbo.UF_ConvertToUnsigned(Name) LIKE '%' + dbo.UF_ConvertToUnsigned(@foodName) + '%' ORDER BY CategoryId ASC
go

select lower(N'ỐUD')
select UPPER(N'uế')
select TRIM(N' n_n     ')

--By default, LIKE operator performs case-insensitive pattern match.
SELECT * FROM Food WHERE Name LIKE N'%ốc%' ORDER BY CategoryId ASC

SELECT SERVERPROPERTY('COLLATION')

SELECT collation_name
FROM sys.databases
WHERE name = 'CanteenManager'

--Proc search món ăn trong bảng Food
ALTER PROC USP_SearchFood
@foodName NVARCHAR(100),
@option INT = 0
AS
BEGIN
    IF(@option = 0) 
        SELECT * FROM Food WHERE Name LIKE '%' + TRIM(@foodName) + '%' ORDER BY CategoryId ASC
    ELSE
        SELECT * FROM Food WHERE dbo.UF_ConvertToUnsigned(LOWER(TRIM(Name))) LIKE '%' + dbo.UF_ConvertToUnsigned(LOWER(TRIM(@foodName))) + '%' ORDER BY CategoryId ASC
END
GO

exec USP_SearchFood N'Ốc   '
exec USP_SearchFood N'ốc  ', 1
GO

--Convert có dấu thành không dấu
CREATE FUNCTION UF_ConvertToUnsigned ( @strInput NVARCHAR(4000) ) 
RETURNS NVARCHAR(4000) 
AS 
BEGIN 
    IF @strInput IS NULL RETURN @strInput 
    IF @strInput = '' RETURN @strInput 
    DECLARE @RT NVARCHAR(4000) 
    DECLARE @SIGN_CHARS NCHAR(136) 
    DECLARE @UNSIGN_CHARS NCHAR (136) 
    SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' +NCHAR(272)+ NCHAR(208) 
    SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD' 
    DECLARE @COUNTER int 
    DECLARE @COUNTER1 int 
    SET @COUNTER = 1 
    WHILE (@COUNTER <=LEN(@strInput)) 
        BEGIN 
            SET @COUNTER1 = 1 
            WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1) 
                BEGIN 
                    IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) ) 
                        BEGIN 
                            IF @COUNTER=1 
                                SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) 
                            ELSE 
                                SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER) 
                            BREAK 
                        END 
                    SET @COUNTER1 = @COUNTER1 +1 
                END 
            SET @COUNTER = @COUNTER +1 
        END 
        SET @strInput = replace(@strInput,' ','-') 
        RETURN @strInput 
END
GO

------------------------------------------------------------------------------------------------------------------------------

------------Nháp--------------------------------------------------------------------------------------------------------------

--Proc lấy tổng doanh thu (chưa tính giảm giá) của từng món ăn dựa theo ngày truyền vào
ALTER PROCEDURE USP_GetRevenueByFoodAndDate
@fromDate DATETIME,
@toDate DATETIME
AS
BEGIN
	WITH temp AS
	(
		SELECT BillInfo.Id, BillId, FoodId, Name, FoodCount, Price, FoodCount * Price AS [TotalPrice], DateCheckIn, DateCheckOut 
		FROM dbo.BillInfo 
		JOIN dbo.Food ON Food.Id = BillInfo.FoodId
		JOIN dbo.Bill ON Bill.Id = BillInfo.BillId AND BillStatus = 1 AND DateCheckIn >= @fromDate AND DateCheckOut <= @toDate
	
	)
	SELECT temp.Name, SUM(temp.FoodCount) AS [TotalFoodCount], SUM(temp.TotalPrice) AS [Revenue] FROM temp GROUP BY temp.Name
END
GO


--Proc lấy tổng doanh thu (đã tính giảm giá) của từng tháng dựa theo ngày truyền vào
ALTER PROCEDURE USP_GetRevenueByMonth
@fromDate DATETIME,
@toDate DATETIME
AS
BEGIN
	WITH temp AS
	(
		SELECT *, 
			Convert(datetime, CONCAT(YEAR(DateCheckOut), '-', MONTH(DateCheckOut), '-01')) AS [FirstDayInMonth], 
			CONCAT(Month(DateCheckOut), '-', Year(DateCheckOut)) AS [Month] 
		FROM dbo.Bill 
		WHERE BillStatus = 1 AND DateCheckIn >= @fromDate AND DateCheckOut <= @toDate	
	)
	SELECT temp.FirstDayInMonth, temp.Month, SUM(temp.TotalPrice) AS [Revenue] FROM temp GROUP BY temp.FirstDayInMonth, temp.Month ORDER BY temp.FirstDayInMonth
END
GO

select DATEFROMPARTS(2022, 11, 2)
select DATETIMEFROMPARTS(2022, 11, 2, 1, 2, 3, 4)
select ISDATE(Convert(nvarchar(100), DATEFROMPARTS(2022, 11, 2)))
select Convert(datetime, CONCAT(2022, '-', 11, '-01'))
select Convert(datetime, CONCAT(2022, 11, 1))


DECLARE	@return_value int
EXEC	@return_value = [dbo].[USP_GetRevenueByMonth]
		@fromDate = N'2022-06-01',
		@toDate = N'2022-10-14'
SELECT	'Return Value' = @return_value


WITH temp AS 
(
SELECT *, CONCAT(MONTH(DateCheckOut),'-',YEAR(DateCheckOut)) AS [Month] 
FROM dbo.Bill 
WHERE BillStatus = 1 AND DateCheckIn >= '2022-06-01 10:37:15' AND DateCheckOut <= '2022-09-02 10:37:15'
)
SELECT temp.Month, SUM(temp.TotalPrice) AS [Revenue] FROM temp GROUP BY temp.Month
SELECT SUM(TotalPrice) FROM dbo.Bill WHERE MONTH(DateCheckOut) = 9

SELECT CONCAT(GETDATE(),' 1')

DELETE dbo.Bill WHERE ID = 18
DELETE FROM dbo.BillInfo WHERE BillID = 1888

DECLARE @id INT
SELECT @id = Id FROM Bill WHERE Id = 49
SELECT * FROM BillInfo --Where BillID = 17 group by FoodID
select BillID, count(FoodID) from BillInfo group by BillID

SELECT * FROM TableFood
DECLARE @name NVARCHAR(100) 
SELECT TOP 3 @name = Name FROM TableFood WHERE UsingState = 0
select @name

SELECT * FROM Food ORDER BY CategoryID ASC
SELECT * FROM Food ORDER BY CategoryID DESC
SELECT * FROM FoodCategory
SELECT * FROM account where username = 'admin'
SELECT UserName AS [Tên], DisplayName, AccType FROM Account
SELECT * ,(SELECT COUNT(*) FROM BillInfo WHERE FoodID = Food.ID) FROM Food

INSERT Food(Name, CategoryID, Price) VALUES(N'Đỉa luộc', 1, 99999)
INSERT FoodCategory(Name) VALUES(N'Chè')

exec USP_UpdateAccount N'nv01', ''
INSERT Account(UserName) VALUES(N'anan')

alter table account drop DF__Account__PassWor__4AB81AF0
alter table account add default N'952362351022552001115621782120108109105108121194219194572217814518010341215583925187233' for password

alter table tablefood add UsingState INT default 1
update TableFood set UsingState = 1
alter table tablefood alter column usingstate int not null

alter table foodcategory add CategoryStatus INT default 1
update  foodcategory set categorystatus = 1
alter table foodcategory alter column categorystatus INT NOT NULL


select Food.Name as FoodName, FoodCategory.Name as CategoryName, BillInfo.FoodCount as FoodCount, Food.Price as Price, FoodCount*Price as TotalPrice 
from BillInfo, Food, FoodCategory 
where BillInfo.FoodID = Food.ID and Food.CategoryID = FoodCategory.ID and BillInfo.BillID = 4

select * from sysdatabases where dbid > 6


--intersect và except
select top 4 * from FoodCategory
intersect
(select top 12 * from FoodCategory
except
select top 10 * from FoodCategory)
go

drop table billinfo

SELECT * FROM dbo.Account