
--Food: Các loại món ăn đồ uống, ví dụ: Nem chua rán, Cam, Quýt, Bia...
--Table: Bàn ăn có thể 'Trống' hoặc 'Có người', nếu bàn ăn 'Có người' thì nó sẽ có 1 Bill chưa thanh toán kèm theo
--FoodCategory: Danh mục, ví dụ: Hoa quả, Món ăn, Nước... mỗi loại món ăn đồ uống sẽ thuộc về 1 danh mục nào đó
--Account: Tài khoản
--Bill: Hoá đơn, bao gồm: ngày CheckIn/CheckOut, bàn phát sinh hoá đơn, trạng thái đã thanh toán/chưa thanh toán...
--BillInfo: Chi tiết cho mỗi hoá đơn: các món ăn, số lượng...	

--Thằng nào ít bị ràng buộc thì tạo trước


CREATE TABLE [TableFood] (
    [ID]          [INTEGER]        PRIMARY KEY AUTOINCREMENT,
    [Name]        [NVARCHAR] (100) NOT NULL
                                   DEFAULT 'Chưa đặt tên',
    [TableStatus] [NVARCHAR] (100) NOT NULL
                                   DEFAULT 'Trống',
    [UsingState]  [INT]            NOT NULL
                                   DEFAULT 1
);

CREATE TABLE Account
(	
    UserName NVARCHAR(100) PRIMARY KEY,
    DisplayName NVARCHAR(100) NOT NULL DEFAULT 'CafeNo1',
    PassWord NVARCHAR(1000) NOT NULL DEFAULT '952362351022552001115621782120108109105108121194219194572217814518010341215583925187233', --Pass mặc định: 0
    AccType INT NOT NULL DEFAULT 0 --1: Admin && 0: Staff 
); 

CREATE TABLE FoodCategory
(
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    Name NVARCHAR(100) NOT NULL DEFAULT 'Chưa đặt tên',
    CategoryStatus INT NOT NULL DEFAULT 1 --1: Đang bán && 0: Không bán
);

CREATE TABLE Food
(
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    Name NVARCHAR(100) NOT NULL DEFAULT 'Chưa đặt tên',
    CategoryID INT NOT NULL,
    Price DOUBLE NOT NULL DEFAULT 0,
    FoodStatus INT NOT NULL DEFAULT 1, --1: Đang bán && 0: Không bán

    FOREIGN KEY (CategoryID) REFERENCES FoodCategory(ID)
); 

CREATE TABLE Bill
(
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    DateCheckIn NVARCHAR(100) NOT NULL DEFAULT (datetime('now', 'localtime')),
    DateCheckOut NVARCHAR(100),
    TableID INT NOT NULL,
    BillStatus INT NOT NULL DEFAULT 0, --1: đã thanh toán && 0: chưa thanh toán
    Discount INT NOT NULL DEFAULT 0,
    TotalPrice DOUBLE NOT NULL DEFAULT 0,

    FOREIGN KEY (TableID) REFERENCES TableFood(ID)
);	

CREATE TABLE BillInfo
(
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    BillID INT NOT NULL,
    FoodID INT NOT NULL,
    FoodCount INT NOT NULL DEFAULT 0,

    FOREIGN KEY (BillID) REFERENCES Bill(ID),
    FOREIGN KEY (FoodID) REFERENCES Food(ID)
);	


----------------------------------------------------------------------------------------
INSERT INTO Account
        ( UserName ,
          DisplayName ,
          --PassWord ,
          AccType
        )
VALUES  ( 'admin' , -- UserName - nvarchar(100)
          'Quán chủ' , -- DisplayName - nvarchar(100)
          --'123' , -- PassWord - nvarchar(1000)
          1  -- AccType - int
        );

--Proc update DisplayName, PassWord và AccType cho Account
UPDATE Account SET DisplayName = 'lão bản' WHERE UserName = 'admin' AND '' IS NOT NULL AND ' ' != '';
UPDATE Account SET PassWord = '' WHERE UserName = '' AND '' IS NOT NULL AND '' != '';
UPDATE Account SET AccType = 1 WHERE UserName = '' AND '' IS NOT NULL;

--proc kiểm tra thông tin đăng nhập
SELECT UserName, DisplayName, AccType FROM Account;

-------------------------------------------------------------------
--TableFood--------------------------------------------------------

--proc tạo bàn mới
INSERT INTO TableFood(Name) VALUES('Bàn ' || CAST((SELECT COUNT(*) FROM TableFood) + 1 AS NVARCHAR(100)));

SELECT * FROM TableFood;

--proc lấy danh sách bàn ăn trong trạng thái còn sử dụng
SELECT * FROM TableFood WHERE UsingState = 1;




--------------------------------------------------------------------
--FoodCategory - Food-----------------------------------------------

--Tạo dữ liệu FoodCategory
INSERT INTO FoodCategory(Name) VALUES('Món ăn');
INSERT INTO FoodCategory(Name) VALUES('Hoa quả');
INSERT INTO FoodCategory(Name) VALUES('Nước');



SELECT * FROM Food ORDER BY CategoryID ASC;
--Tạo dữ liệu Food
INSERT INTO Food(Name, CategoryID, Price) VALUES('Tiết luộc măng chua', 1, 15000);
INSERT INTO Food(Name, CategoryID, Price) VALUES('Cánh gà nướng', 1, 25000) ;
INSERT INTO Food(Name, CategoryID, Price) VALUES('Ốc xào xả ớt', 1, 18000);
INSERT INTO Food(Name, CategoryID, Price) VALUES('Nem chua rán', 1, 19000);
INSERT INTO Food(Name, CategoryID, Price) VALUES('Cam', 2, 8000);
INSERT INTO Food(Name, CategoryID, Price) VALUES('Quýt', 2, 7000); 
INSERT INTO Food(Name, CategoryID, Price) VALUES('Mít', 2, 6000);
INSERT INTO Food(Name, CategoryID, Price) VALUES('Dừa', 2, 9000);
INSERT INTO Food(Name, CategoryID, Price) VALUES('Dưa', 2, 5000);
INSERT INTO Food(Name, CategoryID, Price) VALUES('Dứa', 2, 2000) ;
INSERT INTO Food(Name, CategoryID, Price) VALUES('Ổi', 2, 3000);
INSERT INTO Food(Name, CategoryID, Price) VALUES('Cocacola', 3, 10000);
INSERT INTO Food(Name, CategoryID, Price) VALUES('Pepsi', 3, 11000);
INSERT INTO Food(Name, CategoryID, Price) VALUES('Bia', 3, 12000) ;
INSERT INTO Food(Name, CategoryID, Price) VALUES('Sữa chua mít', 3, 13000);

SELECT * FROM Food;

--Proc thêm món ăn cho Food
INSERT INTO Food(Name, CategoryID, Price) VALUES(@name, @categoryID, @price);


--Proc update món ăn cho Food
UPDATE [Food]
   SET [Name] = @name,
       [CategoryID] = @categoryID,
       [Price] = @price,
       [FoodStatus] = @foodStatus
 WHERE [Food].[ID] = @id;

--Tìm kiếm
SELECT *
  FROM [Food]
 WHERE [Name] LIKE '%c%'
 ORDER BY [CategoryID] ASC;
 
SELECT lower('A Ă Â Đ E Ê O Ô Ơ U Ư');

SELECT *
  FROM [food]
 WHERE [Name] GLOB '*Ổi*'
 ORDER BY [CategoryID] ASC;

--Proc lấy tổng doanh thu (chưa tính giảm giá) của từng món ăn dựa theo ngày truyền vào

WITH [temp] AS (
    SELECT [BillInfo].[ID],
           [BillID],
           [FoodID],
           [Name],
           [FoodCount],
           [Price],
           [FoodCount] * [Price] AS [TotalPrice],
           [DateCheckIn],
           [DateCheckOut]
      FROM [BillInfo]
           JOIN
           [Food] ON [Food].[ID] = [BillInfo].[FoodID]
           JOIN
           [Bill] ON [Bill].[ID] = [BillInfo].[BillID] AND 
                           [BillStatus] = 1 AND 
                           [DateCheckIn] >= '2022-06-01 10:37:15' AND 
                           [DateCheckOut] <= '2022-09-05 10:37:15'
)
SELECT [temp].[Name],
       SUM([temp].[FoodCount]) AS [TotalFoodCount],
       SUM([temp].[TotalPrice]) AS [Revenue]
  FROM [temp]
 GROUP BY [temp].[Name];






----------------------------------------------------------------------
--Bill----------------------------------------------------------------

DELETE FROM Bill WHERE ID = 41;

UPDATE [Bill]
   SET [TableID] = 2
 WHERE [ID] = 56;

SELECT TableID FROM BILL; 
SELECT * FROM BILL;

--proc thêm Bill mới
--  ID: tự động thêm do ràng buộc IDENTITY
--	DateCheckIn: luôn là ngày hôm nay
--	DateCheckOut: luôn là NULL do hoá đơn mới tạo, chưa thanh toán
--	TableID: ID của bàn phát sinh hoá đơn
--	BillStatus: luôn là 0 - chưa thanh toán
--  Discount: luôn là 0 khi thêm Bill mới, sau khi thanh toán mới tính vào

    --Thêm 1 Bill mới
INSERT INTO Bill(TableID) VALUES(30);
    --Khi 1 bàn có Bill mới, nó sẽ chuyển sang trạng thái 'Có người'
    --UPDATE TableFood SET TableStatus = N'Có người' WHERE TableFood.ID = @tableID

--Proc lấy tổng số danh sách các hoá đơn dựa theo ngày truyền vào    
SELECT COUNT( * ) 
  FROM [Bill]
 WHERE strftime('%s', [DateCheckIn]) >= strftime('%s', '{fromDate.ToString("o")}') AND 
       strftime('%s', [DateCheckOut]) <= strftime('%s', '{toDate.ToString("o")}') AND 
       [BillStatus] = 1;


--Proc lấy danh sách các hoá đơn dựa theo ngày truyền vào
SELECT [Bill].[ID],
       [Bill].[DateCheckIn] AS [Ngày phát sinh],
       [Bill].[DateCheckOut] AS [Ngày thanh toán],
       [TableFood].[Name] AS [Tên bàn],
       [Bill].[Discount] AS [Giảm giá (%)],
       [Bill].[TotalPrice] AS [Tiền thanh toán (Vnđ)]
  FROM [Bill],
       [TableFood]
 WHERE strftime('%s', [DateCheckIn]) >= strftime('%s', '{fromDate.ToString("o")}') AND 
       strftime('%s', [DateCheckOut]) <= strftime('%s', '{toDate.ToString("o")}') AND 
       [BillStatus] = 1 AND 
       [TableFood].[ID] = [Bill].[TableID];


--Proc lấy danh sách các hoá đơn dựa theo ngày và số trang truyền vào    
WITH [temp] 
AS (
    SELECT [Bill].[ID],
           [Bill].[DateCheckIn] AS [Ngày phát sinh],
           [Bill].[DateCheckOut] AS [Ngày thanh toán],
           [TableFood].[Name] AS [Tên bàn],
           [Bill].[Discount] AS [Giảm giá (%)],
           [Bill].[TotalPrice] AS [Tiền thanh toán (Vnđ)]
      FROM [Bill],
           [TableFood]
     WHERE strftime('%s', [DateCheckIn]) >= strftime('%s', '2022-06-01') AND 
           strftime('%s', [DateCheckOut]) <= strftime('%s', '2022-09-16') AND 
           [BillStatus] = 1 AND 
           [TableFood].[ID] = [Bill].[TableID]
)
SELECT * FROM (SELECT * FROM [temp] LIMIT 10 * 3)
EXCEPT
SELECT *  FROM (SELECT * FROM [temp] LIMIT 10 * 0);

SELECT * FROM Bill WHERE Bill.BillStatus = 1 AND Bill.TableID = 1;
SELECT * FROM Bill WHERE Bill.ID = 1;

--proc thanh toán Bill

    --Khi Bill được thanh toán => BillStatus = 1
UPDATE [Bill]
   SET [BillStatus] = 1,
       [DateCheckOut] = strftime('%Y-%m-%d %H:%M:%f', 'now', 'localtime'),
       [Discount] = 99,
       [TotalPrice] = 88
 WHERE [ID] = 41;


    --DECLARE @tableID INT
    --Theo thiết kế mỗi Bill chỉ có 1 ID duy nhất => kết quả trả về luôn chỉ có 1 hàng => Dùng hàm MAX/MIN để lấy được TableID của Bill
    --SELECT @tableID = MAX(Bill.TableID) FROM Bill WHERE Bill.ID = @billID
    --Khi 1 bàn được thanh toán Bill, nó sẽ chuyển sang trạng thái 'Trống'
    --UPDATE TableFood SET TableStatus = N'Trống' WHERE TableFood.ID = @tableID


--Lấy tổng doanh thu (đã tính giảm giá) của từng tháng dựa theo ngày truyền vào
WITH [temp] AS (
    SELECT *,
           strftime('%m-%Y', [DateCheckOut]) AS [Month]
      FROM [Bill]
     WHERE [BillStatus] = 1 AND 
           strftime('%s', [DateCheckIn]) >= strftime('%s', '2022-06-01') AND 
           strftime('%s', [DateCheckOut]) <= strftime('%s', '2022-09-16') 
)
SELECT [temp].[Month],
       SUM([temp].[TotalPrice]) AS [Revenue]
  FROM [temp]
 GROUP BY [temp].[Month];



--------------------------------------------------------------------
--BillDetail--------------------------------------------------------

--Lấy danh sách BillDetail dựa vào TableID của Bill.
--Bill này phải là Bill chưa thanh toán (BillStatus = 0).
SELECT [Food].[Name] AS [FoodName],
       [FoodCategory].[Name] AS [CategoryName],
       [BillInfo].[FoodCount] AS [FoodCount],
       [Food].[Price] AS [Price],
       [FoodCount] * [Price] AS [TotalPrice]
  FROM [Bill],
       [BillInfo],
       [Food],
       [FoodCategory]
 WHERE [BillInfo].[BillID] = [Bill].[ID] AND 
       [BillInfo].[FoodID] = [Food].[ID] AND 
       [Food].[CategoryID] = [FoodCategory].[ID] AND 
       [Bill].[BillStatus] = 0 AND 
       [Bill].[TableID] = 1;

--Lấy danh sách BillDetail dựa vào ID của Bill.       
SELECT [Food].[Name] AS [FoodName],
       [FoodCategory].[Name] AS [CategoryName],
       [BillInfo].[FoodCount] AS [FoodCount],
       [Food].[Price] AS [Price],
       [FoodCount] * [Price] AS [TotalPrice]
  FROM [BillInfo],
       [Food],
       [FoodCategory]
 WHERE [BillInfo].[FoodID] = [Food].[ID] AND 
       [Food].[CategoryID] = [FoodCategory].[ID] AND 
       [BillInfo].[BillID] = 39;



--------------------------------------------------------------------
--BillInfo----------------------------------------------------------

SELECT * FROM BillInfo;
--proc thêm BillInfo mới

    --Kiểm tra xem cái BillInfo này có tồn tại không (cái Bill này đã có BillInfo nào chưa, nếu có thì đã có món ăn này chưa)
    --Nếu tồn tại thì kết quả trả về cũng chỉ có 1 hàng duy nhất => Dùng hàm MAX hoặc MIN để lấy được FoodCount của món ăn
    SELECT COUNT(*) FROM BillInfo WHERE BillInfo.BillID = @billID AND BillInfo.FoodID = @foodID;
    SELECT FoodCount FROM BillInfo WHERE BillInfo.BillID = @billID AND BillInfo.FoodID = @foodID;

    --Nếu đã tồn tại thì update số lượng món đã gọi
    --Nếu không thì thêm mới

            --Theo thiết kế @foodCount truyền vào có thể âm, nếu @newFoodCount <= 0 thì xoá món đó khỏi hoá đơn
            --@newFoodCount  = @currentFoodCount + @foodCount;           
                DELETE FROM BillInfo WHERE BillInfo.BillID = 39 AND BillInfo.FoodID = 1;
            
                UPDATE BillInfo SET FoodCount = @newFoodCount WHERE BillInfo.BillID = @billID AND BillInfo.FoodID = @foodID;

            --Nếu @foodCount > 0 (người dùng chọn số món > 0) mới thực hiện thêm
                INSERT INTO BillInfo(BillID, FoodID, FoodCount) VALUES(@billID, @foodID, @newFoodCount);



CREATE TRIGGER [UTG_InsertBill]
         AFTER INSERT
            ON [Bill]
BEGIN
    UPDATE [TableFood]
       SET [TableStatus] = 'Có người'
     WHERE [ID] = [new].[TableID] AND 
           [new].[BillStatus] = 0;
END;

CREATE TRIGGER [UTG_DeleteBill]
         AFTER DELETE
            ON [Bill]
BEGIN
    UPDATE [TableFood]
       SET [TableStatus] = 'Trống'
     WHERE [ID] = [old].[TableID] AND 
           [old].[BillStatus] = 0;
END;

CREATE TRIGGER [UTG_UpdateBill]
         AFTER UPDATE
            ON [Bill]
BEGIN
--Khi thanh toán (thay đổi BillStatus = 1)
    UPDATE [TableFood]
       SET [TableStatus] = 'Trống'
     WHERE [ID] = [old].[TableID] AND 
           [ID] = [new].[TableID] AND 
           [old].[BillStatus] = 0 AND 
           [new].[BillStatus] = 1;
           
--Khi chuyển bản (thay đổi TableID)

    UPDATE [TableFood]
       SET [TableStatus] = 'Có người'
     WHERE [ID] = [new].[TableID] AND 
           [old].[TableID] != [new].[TableID] AND 
           [old].[BillStatus] = 0 AND 
           [new].[BillStatus] = 0;
           
UPDATE [TableFood]
   SET [TableStatus] = 'Trống'
 WHERE [ID] = [old].[TableID] AND 
       (
           SELECT count( * ) 
             FROM [Bill]
            WHERE [TableID] = [old].[TableID] AND 
                  [BillStatus] = 0
       )
=      0;

END;

SELECT count(*) FROM Bill WHERE [TableID] = 1 AND [BillStatus] = 0;
DROP TRIGGER [UTG_UpdateBill];
select * from bill
------------------------------------------------------------------------------------------------------------------------------

------------Nháp--------------------------------------------------------------------------------------------------------------

