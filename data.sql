use sapdb

Create Table Hierarch_Chart
(
      CompanyCode nvarchar(MAX),
      CompanyName nvarchar(MAX),
      ShCode nvarchar(MAX),
      ShName nvarchar(MAX),
      Percentage Numeric(10,4),
      ShCapital  Numeric(10,2),
      ShType nvarchar(20),
      Flag nvarchar(1)
)

INSERT INTO Hierarch_Chart VALUES('HC0001','Holding Company 1','FM0001',N'الأخ/ فتحي عبد الواسع هائل سعيد',50,150000,'Family Member','Y');
INSERT INTO Hierarch_Chart VALUES('HC0001','Holding Company 1','FM0002',N'الأخ/ فؤاد هائل سعيد',30,150000,'Family Member','Y');
INSERT INTO Hierarch_Chart VALUES('HC0001','Holding Company 1','HC0002',N'الأخ/ عبدالله عبده سعيد',5,150000,'Holding Company','Y');
INSERT INTO Hierarch_Chart VALUES('HC0001','Holding Company 1','FM0004',N'ايمن رشاد هائل سعيد',10,150000,'Family Member','Y');
INSERT INTO Hierarch_Chart VALUES('HC0001','Holding Company 1','FM0003',N'الأخت/ ام شوقي - رحمة عبد الواسع هائل سعيد',5,150000,'Family Member','Y');
INSERT INTO Hierarch_Chart VALUES('HC0002','Holding Company 2','FM0001',N'الأخ/ فتحي عبد الواسع هائل سعيد',80,150000,'Family Member','N');
INSERT INTO Hierarch_Chart VALUES('HC0002','Holding Company 2','FM0003',N'الأخت/ ام شوقي - رحمة عبد الواسع هائل سعيد',10,150000,'Family Member','N');
INSERT INTO Hierarch_Chart VALUES('HC0002','Holding Company 2','TP0001',N'أحمد شوقي احمد هائل سعيد',5,150000,'Third Party','N');
INSERT INTO Hierarch_Chart VALUES('HC0002','Holding Company 2','FM0004',N'ايمن رشاد هائل سعيد',5,150000,'Family Member','N');
INSERT INTO Hierarch_Chart VALUES('SC0001','Subsidiary Company I','HC0001',N'ابراهيم خالد احمد هائل سعيد',50,1500000,'Holding Company','N');
INSERT INTO Hierarch_Chart VALUES('SC0001','Subsidiary Company I','HC0003',N'أحمد شوقي احمد هائل سعيد',10,1500000,'Holding Company','N');
INSERT INTO Hierarch_Chart VALUES('SC0001','Subsidiary Company I','TP0001',N'أحمد شوقي احمد هائل سعيد',20,1500000,'Third Party','N');
INSERT INTO Hierarch_Chart VALUES('SC0001','Subsidiary Company I','FM0004',N'ايمن رشاد هائل سعيد',20,1500000,'Family Member','N');
INSERT INTO Hierarch_Chart VALUES('SC0002','Subsidiary Company II','SC0001',N'الأخ/ أبراهيم هائل سعيد',50,20000,'Subsidiary','N');
INSERT INTO Hierarch_Chart VALUES('SC0002','Subsidiary Company II','HC0001',N'ابراهيم خالد احمد هائل سعيد',20,20000,'Holding Company','N');
INSERT INTO Hierarch_Chart VALUES('SC0002','Subsidiary Company II','HC0003',N'أحمد شوقي احمد هائل سعيد',30,20000,'Holding Company','N');
INSERT INTO Hierarch_Chart VALUES('SC0003','Subsidiary Company III','SC0001','Sample Company',50,110000,'Subsidiary','N');
INSERT INTO Hierarch_Chart VALUES('SC0003','Subsidiary Company III','SC0002','Sample Company',50,110000,'Subsidiary','N');



CREATE TABLE CompMaster (
	CompCode nvarchar(MAX),
	CompName nvarchar(MAX),
	CapAmount numeric(10, 2),
	flag nvarchar(MAX)
);

INSERT INTO CompMaster VALUES('HC0001','Holding Company 1',150000,'Y');
INSERT INTO CompMaster VALUES('HC0002','Holding Company 2',150000,'N');
INSERT INTO CompMaster VALUES('HC0003','Holding Company 3',1539950,'N');
INSERT INTO CompMaster VALUES('SC0001','Subsidiary Company I',1500000,'N');
INSERT INTO CompMaster VALUES('SC0002','Subsidiary Company II',20000,'N');
INSERT INTO CompMaster VALUES('SC0003','Subsidiary Company III',110000,'N');
INSERT INTO CompMaster VALUES('TP0001','Third Party I',0,'N');
INSERT INTO CompMaster VALUES('SC0004','Subsidiary Company IV',0,'N');
