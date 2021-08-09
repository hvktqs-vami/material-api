-- 
-- Set character set the client will use to send SQL statements to the server
--
SET NAMES 'utf8';

--
-- Set default database
--
USE vami_dev;

--
-- Create table `Material_Groups`
--
CREATE TABLE Material_Groups (
  Id int NOT NULL AUTO_INCREMENT,
  Name varchar(255) NOT NULL,
  CreatedTime datetime(6) NOT NULL,
  CreatedUser varchar(64) DEFAULT NULL,
  UpdatedTime datetime(6) DEFAULT NULL,
  UpdatedUser varchar(64) DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 4,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci;

--
-- Create table `Material_Sub_Groups`
--
CREATE TABLE Material_Sub_Groups (
  Id int NOT NULL AUTO_INCREMENT,
  Name varchar(255) NOT NULL,
  GroupId int NOT NULL,
  CreatedTime datetime(6) NOT NULL,
  CreatedUser varchar(64) DEFAULT NULL,
  UpdatedTime datetime(6) DEFAULT NULL,
  UpdatedUser varchar(64) DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 2,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci;

--
-- Create index `IX_Material_Sub_Groups_GroupId` on table `Material_Sub_Groups`
--
ALTER TABLE Material_Sub_Groups
ADD INDEX IX_Material_Sub_Groups_GroupId (GroupId);

--
-- Create foreign key
--
ALTER TABLE Material_Sub_Groups
ADD CONSTRAINT FK_Material_Sub_Groups_Material_Groups_GroupId FOREIGN KEY (GroupId)
REFERENCES Material_Groups (Id) ON DELETE CASCADE;

--
-- Create table `Countries`
--
CREATE TABLE Countries (
  Id varchar(64) NOT NULL,
  Name varchar(255) NOT NULL,
  CreatedTime datetime(6) NOT NULL,
  CreatedUser varchar(64) DEFAULT NULL,
  UpdatedTime datetime(6) DEFAULT NULL,
  UpdatedUser varchar(64) DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AVG_ROW_LENGTH = 63,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci;

--
-- Create table `Material_Standards`
--
CREATE TABLE Material_Standards (
  Id int NOT NULL AUTO_INCREMENT,
  Name varchar(255) NOT NULL,
  CountryId varchar(64) DEFAULT NULL,
  CreatedTime datetime(6) NOT NULL,
  CreatedUser varchar(64) DEFAULT NULL,
  UpdatedTime datetime(6) DEFAULT NULL,
  UpdatedUser varchar(64) DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 4,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci;

--
-- Create index `IX_Material_Standards_CountryId` on table `Material_Standards`
--
ALTER TABLE Material_Standards
ADD INDEX IX_Material_Standards_CountryId (CountryId);

--
-- Create foreign key
--
ALTER TABLE Material_Standards
ADD CONSTRAINT FK_Material_Standards_Country_CountryId FOREIGN KEY (CountryId)
REFERENCES Countries (Id);

--
-- Create table `Materials`
--
CREATE TABLE Materials (
  Id bigint NOT NULL AUTO_INCREMENT,
  Name varchar(255) NOT NULL,
  CountryId varchar(64) DEFAULT NULL,
  HeatTreatment varchar(1024) DEFAULT NULL,
  SubGroupId int DEFAULT NULL,
  GroupId int DEFAULT NULL,
  StandardId int DEFAULT NULL,
  CreatedTime datetime(6) NOT NULL,
  CreatedUser varchar(64) DEFAULT NULL,
  UpdatedTime datetime(6) DEFAULT NULL,
  UpdatedUser varchar(64) DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 3,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci;

--
-- Create index `IX_Materials_SubGroupId1` on table `Materials`
--
ALTER TABLE Materials
ADD INDEX IX_Materials_SubGroupId1 (SubGroupId);

--
-- Create index `IX_Materials_CountryId` on table `Materials`
--
ALTER TABLE Materials
ADD INDEX IX_Materials_CountryId (CountryId);

--
-- Create index `IX_Materials_GroupId1` on table `Materials`
--
ALTER TABLE Materials
ADD INDEX IX_Materials_GroupId1 (GroupId);

--
-- Create index `IX_Materials_StandardId` on table `Materials`
--
ALTER TABLE Materials
ADD INDEX IX_Materials_StandardId (StandardId);

--
-- Create foreign key
--
ALTER TABLE Materials
ADD CONSTRAINT FK_Materials_Countries_CountryId FOREIGN KEY (CountryId)
REFERENCES Countries (Id);

--
-- Create foreign key
--
ALTER TABLE Materials
ADD CONSTRAINT FK_Materials_Groups_GroupId FOREIGN KEY (GroupId)
REFERENCES Material_Groups (Id);

--
-- Create foreign key
--
ALTER TABLE Materials
ADD CONSTRAINT FK_Materials_Standards_StandardId FOREIGN KEY (StandardId)
REFERENCES Material_Standards (Id);

--
-- Create foreign key
--
ALTER TABLE Materials
ADD CONSTRAINT FK_Materials_SubGroups_SubGroupId FOREIGN KEY (SubGroupId)
REFERENCES Material_Sub_Groups (Id);

--
-- Create table `Material_Usages`
--
CREATE TABLE Material_Usages (
  Id bigint NOT NULL AUTO_INCREMENT,
  Name varchar(255) NOT NULL,
  MaterialId bigint NOT NULL,
  CreatedTime datetime(6) NOT NULL,
  CreatedUser varchar(64) DEFAULT NULL,
  UpdatedTime datetime(6) DEFAULT NULL,
  UpdatedUser varchar(64) DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 3,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci;

--
-- Create foreign key
--
ALTER TABLE Material_Usages
ADD CONSTRAINT FK_Material_Usges_MaterialId FOREIGN KEY (MaterialId)
REFERENCES Materials (Id);

--
-- Create table `Material_Low_Fatigues`
--
CREATE TABLE Material_Low_Fatigues (
  Id bigint NOT NULL AUTO_INCREMENT,
  MaterialId bigint DEFAULT NULL,
  `Condition` varchar(512) DEFAULT NULL,
  Direction varchar(128) DEFAULT NULL,
  CYieldStrength decimal(65, 30) NOT NULL,
  CStrengthExp decimal(65, 30) NOT NULL,
  CStrengthCoef decimal(65, 30) NOT NULL,
  FStrengthExp decimal(65, 30) NOT NULL,
  FStrengthCoef decimal(65, 30) NOT NULL,
  FDuctilityExp decimal(65, 30) NOT NULL,
  FDuctilityCoef decimal(65, 30) NOT NULL,
  CreatedTime datetime(6) NOT NULL,
  CreatedUser varchar(64) DEFAULT NULL,
  UpdatedTime datetime(6) DEFAULT NULL,
  UpdatedUser varchar(64) DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 3,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci;

--
-- Create index `IX_Material_Low_Fatigues_MaterialId1` on table `Material_Low_Fatigues`
--
ALTER TABLE Material_Low_Fatigues
ADD INDEX IX_Material_Low_Fatigues_MaterialId1 (MaterialId);

--
-- Create foreign key
--
ALTER TABLE Material_Low_Fatigues
ADD CONSTRAINT FK_Material_Low_Fatigues_Materials_MaterialId1 FOREIGN KEY (MaterialId)
REFERENCES Materials (Id);

--
-- Create table `Material_High_Temp_Mec_Props`
--
CREATE TABLE Material_High_Temp_Mec_Props (
  Id bigint NOT NULL AUTO_INCREMENT,
  MaterialId bigint DEFAULT NULL,
  Temperature decimal(65, 30) NOT NULL,
  YieldRp02 decimal(65, 30) NOT NULL,
  YieldRp1 decimal(65, 30) NOT NULL,
  TensileRm decimal(65, 30) NOT NULL,
  Hardness decimal(65, 30) NOT NULL,
  H1k decimal(65, 30) NOT NULL,
  H10k decimal(65, 30) NOT NULL,
  H100k decimal(65, 30) NOT NULL,
  CreatedTime datetime(6) NOT NULL,
  CreatedUser varchar(64) DEFAULT NULL,
  UpdatedTime datetime(6) DEFAULT NULL,
  UpdatedUser varchar(64) DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 3,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci;

--
-- Create index `IX_Material_High_Temp_Mec_Props_MaterialId1` on table `Material_High_Temp_Mec_Props`
--
ALTER TABLE Material_High_Temp_Mec_Props
ADD INDEX IX_Material_High_Temp_Mec_Props_MaterialId1 (MaterialId);

--
-- Create foreign key
--
ALTER TABLE Material_High_Temp_Mec_Props
ADD CONSTRAINT FK_Material_High_Temp_Mec_Props_Materials_MaterialId1 FOREIGN KEY (MaterialId)
REFERENCES Materials (Id);

--
-- Create table `Material_High_Fatigues`
--
CREATE TABLE Material_High_Fatigues (
  Id bigint NOT NULL AUTO_INCREMENT,
  MaterialId bigint DEFAULT NULL,
  `Condition` varchar(512) DEFAULT NULL,
  Value decimal(65, 30) NOT NULL,
  CreatedTime datetime(6) NOT NULL,
  CreatedUser varchar(64) DEFAULT NULL,
  UpdatedTime datetime(6) DEFAULT NULL,
  UpdatedUser varchar(64) DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 3,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci;

--
-- Create index `IX_Material_High_Fatigues_MaterialId1` on table `Material_High_Fatigues`
--
ALTER TABLE Material_High_Fatigues
ADD INDEX IX_Material_High_Fatigues_MaterialId1 (MaterialId);

--
-- Create foreign key
--
ALTER TABLE Material_High_Fatigues
ADD CONSTRAINT FK_Material_High_Fatigues_Materials_MaterialId1 FOREIGN KEY (MaterialId)
REFERENCES Materials (Id);

--
-- Create table `Material_Equivalents`
--
CREATE TABLE Material_Equivalents (
  Id bigint NOT NULL AUTO_INCREMENT,
  MaterialId bigint DEFAULT NULL,
  EquivMaterialId bigint DEFAULT NULL,
  CreatedTime datetime(6) NOT NULL,
  CreatedUser varchar(64) DEFAULT NULL,
  UpdatedTime datetime(6) DEFAULT NULL,
  UpdatedUser varchar(64) DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci;

--
-- Create index `IX_Material_Equivalents_EquivMaterialId1` on table `Material_Equivalents`
--
ALTER TABLE Material_Equivalents
ADD INDEX IX_Material_Equivalents_EquivMaterialId1 (EquivMaterialId);

--
-- Create index `IX_Material_Equivalents_MaterialId1` on table `Material_Equivalents`
--
ALTER TABLE Material_Equivalents
ADD INDEX IX_Material_Equivalents_MaterialId1 (MaterialId);

--
-- Create foreign key
--
ALTER TABLE Material_Equivalents
ADD CONSTRAINT FK_Material_Equivalents_Materials_EquivMaterialId1 FOREIGN KEY (EquivMaterialId)
REFERENCES Materials (Id);

--
-- Create foreign key
--
ALTER TABLE Material_Equivalents
ADD CONSTRAINT FK_Material_Equivalents_Materials_MaterialId1 FOREIGN KEY (MaterialId)
REFERENCES Materials (Id);

--
-- Create table `Material_Contents`
--
CREATE TABLE Material_Contents (
  Id bigint NOT NULL AUTO_INCREMENT,
  HtmlContent longtext DEFAULT NULL,
  PlainTextContent longtext DEFAULT NULL,
  MaterialId bigint NOT NULL,
  CreatedTime datetime(6) NOT NULL,
  CreatedUser varchar(63) DEFAULT NULL,
  UpdatedTime datetime(6) DEFAULT NULL,
  UpdatedUser varchar(63) DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 3,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci;

--
-- Create foreign key
--
ALTER TABLE Material_Contents
ADD CONSTRAINT FK_Material_Contents_MaterialId FOREIGN KEY (MaterialId)
REFERENCES Materials (Id);

--
-- Create table `Material_Mechanical_Prop_Groups`
--
CREATE TABLE Material_Mechanical_Prop_Groups (
  Id int NOT NULL AUTO_INCREMENT,
  Name varchar(255) NOT NULL,
  Description varchar(1000) DEFAULT NULL,
  CreatedTime datetime(6) NOT NULL,
  CreatedUser varchar(64) DEFAULT NULL,
  UpdatedTime datetime(6) DEFAULT NULL,
  UpdatedUser varchar(64) DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 2,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci;

--
-- Create table `Material_Mechanical_Prop_Types`
--
CREATE TABLE Material_Mechanical_Prop_Types (
  Id int NOT NULL AUTO_INCREMENT,
  Name varchar(255) NOT NULL,
  GroupId int NOT NULL,
  SortOrder int NOT NULL,
  CreatedTime datetime(6) NOT NULL,
  CreatedUser varchar(64) DEFAULT NULL,
  UpdatedTime datetime(6) DEFAULT NULL,
  UpdatedUser varchar(64) DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 3,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci;

--
-- Create index `IX_Material_Mechanical_Prop_Types_GroupId` on table `Material_Mechanical_Prop_Types`
--
ALTER TABLE Material_Mechanical_Prop_Types
ADD INDEX IX_Material_Mechanical_Prop_Types_GroupId (GroupId);

--
-- Create foreign key
--
ALTER TABLE Material_Mechanical_Prop_Types
ADD CONSTRAINT `FK_Material_Mechanical_Prop_Types_Material_Mechanical_Prop_Grou~` FOREIGN KEY (GroupId)
REFERENCES Material_Mechanical_Prop_Groups (Id) ON DELETE CASCADE;

--
-- Create table `Material_Mechanical_Props`
--
CREATE TABLE Material_Mechanical_Props (
  Id bigint NOT NULL AUTO_INCREMENT,
  MaterialId bigint NOT NULL,
  TypeId int NOT NULL,
  DimensionHeat varchar(512) DEFAULT NULL,
  Min decimal(65, 30) NOT NULL,
  Max decimal(65, 30) NOT NULL,
  Approx decimal(65, 30) NOT NULL,
  Unit varchar(64) DEFAULT NULL,
  Comment varchar(512) DEFAULT NULL,
  CreatedTime datetime(6) NOT NULL,
  CreatedUser varchar(64) DEFAULT NULL,
  UpdatedTime datetime(6) DEFAULT NULL,
  UpdatedUser varchar(64) DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 3,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci;

--
-- Create index `IX_Material_Mechanical_Props_TypeId` on table `Material_Mechanical_Props`
--
ALTER TABLE Material_Mechanical_Props
ADD INDEX IX_Material_Mechanical_Props_TypeId (TypeId);

--
-- Create foreign key
--
ALTER TABLE Material_Mechanical_Props
ADD CONSTRAINT FK_Material_Mechanical_Props_Materials_MaterialId FOREIGN KEY (MaterialId)
REFERENCES Materials (Id);

--
-- Create foreign key
--
ALTER TABLE Material_Mechanical_Props
ADD CONSTRAINT FK_Material_Mechanical_Props_PropTypes_TypeId FOREIGN KEY (TypeId)
REFERENCES Material_Mechanical_Prop_Types (Id);

--
-- Create table `Material_Chemical_Types`
--
CREATE TABLE Material_Chemical_Types (
  Id int NOT NULL AUTO_INCREMENT,
  Code varchar(255) NOT NULL,
  Name varchar(255) NOT NULL,
  CreatedTime datetime(6) NOT NULL,
  CreatedUser varchar(64) DEFAULT NULL,
  UpdatedTime datetime(6) DEFAULT NULL,
  UpdatedUser varchar(64) DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 3,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci;

--
-- Create table `Material_Chemicals`
--
CREATE TABLE Material_Chemicals (
  Id bigint NOT NULL AUTO_INCREMENT,
  MaterialId bigint DEFAULT NULL,
  ElementId int NOT NULL,
  Min decimal(65, 30) NOT NULL,
  Max decimal(65, 30) NOT NULL,
  Approx decimal(65, 30) NOT NULL,
  CreatedTime datetime(6) NOT NULL,
  CreatedUser varchar(64) DEFAULT NULL,
  UpdatedTime datetime(6) DEFAULT NULL,
  UpdatedUser varchar(64) DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 3,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci;

--
-- Create index `IX_Material_Chemicals_ElementId` on table `Material_Chemicals`
--
ALTER TABLE Material_Chemicals
ADD INDEX IX_Material_Chemicals_ElementId (ElementId);

--
-- Create index `IX_Material_Chemicals_MaterialId1` on table `Material_Chemicals`
--
ALTER TABLE Material_Chemicals
ADD INDEX IX_Material_Chemicals_MaterialId1 (MaterialId);

--
-- Create foreign key
--
ALTER TABLE Material_Chemicals
ADD CONSTRAINT FK_Material_Chemicals_Material_Chemical_Types_ElementId FOREIGN KEY (ElementId)
REFERENCES Material_Chemical_Types (Id) ON DELETE CASCADE;

--
-- Create foreign key
--
ALTER TABLE Material_Chemicals
ADD CONSTRAINT FK_Material_Chemicals_Materials_MaterialId1 FOREIGN KEY (MaterialId)
REFERENCES Materials (Id);