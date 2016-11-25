-- MySQL dump 10.13  Distrib 5.6.24, for Win64 (x86_64)
--
-- Host: localhost    Database: labelingframework
-- ------------------------------------------------------
-- Server version	5.6.25-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `configuration`
--

DROP TABLE IF EXISTS `configuration`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `configuration` (
  `id_path` int(11) NOT NULL AUTO_INCREMENT,
  `path_label` varchar(500) NOT NULL,
  PRIMARY KEY (`id_path`),
  UNIQUE KEY `id_path_UNIQUE` (`id_path`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `configuration`
--

LOCK TABLES `configuration` WRITE;
/*!40000 ALTER TABLE `configuration` DISABLE KEYS */;
INSERT INTO `configuration` VALUES (11,'\\\\DATA\\\\ImageDatabaseA');
/*!40000 ALTER TABLE `configuration` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `globalreferencepathsmax`
--

DROP TABLE IF EXISTS `globalreferencepathsmax`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `globalreferencepathsmax` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `pathmax` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `globalreferencepathsmax`
--

LOCK TABLES `globalreferencepathsmax` WRITE;
/*!40000 ALTER TABLE `globalreferencepathsmax` DISABLE KEYS */;
/*!40000 ALTER TABLE `globalreferencepathsmax` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `globalreferencepathsmin`
--

DROP TABLE IF EXISTS `globalreferencepathsmin`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `globalreferencepathsmin` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `pathmin` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `globalreferencepathsmin`
--

LOCK TABLES `globalreferencepathsmin` WRITE;
/*!40000 ALTER TABLE `globalreferencepathsmin` DISABLE KEYS */;
/*!40000 ALTER TABLE `globalreferencepathsmin` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `group`
--

DROP TABLE IF EXISTS `group`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `group` (
  `IdGroup` bigint(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  `IdTestCase` bigint(20) NOT NULL,
  `ReferenceIsGlobal` int(1) DEFAULT NULL,
  `IsPatientChosen` int(1) DEFAULT NULL,
  `PageStyle` int(11) DEFAULT NULL,
  `hasReference` int(1) DEFAULT NULL,
  `ImagesPerPage` int(11) DEFAULT NULL,
  `pathmin` int(11) DEFAULT NULL,
  `pathmax` int(11) DEFAULT NULL,
  PRIMARY KEY (`IdGroup`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `group`
--

LOCK TABLES `group` WRITE;
/*!40000 ALTER TABLE `group` DISABLE KEYS */;
/*!40000 ALTER TABLE `group` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `group_has_image`
--

DROP TABLE IF EXISTS `group_has_image`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `group_has_image` (
  `IdGroup` bigint(20) NOT NULL,
  `PathImage` varchar(1024) NOT NULL,
  `IsReference` bit(1) DEFAULT NULL,
  `IdGroupHasImage` bigint(20) NOT NULL AUTO_INCREMENT,
  `NamePatiente` varchar(50) NOT NULL,
  PRIMARY KEY (`IdGroupHasImage`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `group_has_image`
--

LOCK TABLES `group_has_image` WRITE;
/*!40000 ALTER TABLE `group_has_image` DISABLE KEYS */;
/*!40000 ALTER TABLE `group_has_image` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `image_old`
--

DROP TABLE IF EXISTS `image_old`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `image_old` (
  `ID_Image` bigint(20) NOT NULL AUTO_INCREMENT,
  `Name_Image` varchar(100) NOT NULL,
  `Label` int(11) DEFAULT NULL,
  `Repository_ID_Repository` bigint(20) NOT NULL,
  PRIMARY KEY (`ID_Image`,`Repository_ID_Repository`),
  KEY `fk_Image_Repository1_idx` (`Repository_ID_Repository`),
  CONSTRAINT `fk_Image_Repository1` FOREIGN KEY (`Repository_ID_Repository`) REFERENCES `repository` (`ID_Repository`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `image_old`
--

LOCK TABLES `image_old` WRITE;
/*!40000 ALTER TABLE `image_old` DISABLE KEYS */;
/*!40000 ALTER TABLE `image_old` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lablescalecontinuous`
--

DROP TABLE IF EXISTS `lablescalecontinuous`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `lablescalecontinuous` (
  `IdLable` bigint(20) NOT NULL AUTO_INCREMENT,
  `Lable` decimal(18,2) NOT NULL,
  `IdUser` bigint(20) NOT NULL,
  `IdGroupImage` bigint(20) NOT NULL,
  `IdScaleContinuous` bigint(20) NOT NULL,
  PRIMARY KEY (`IdLable`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lablescalecontinuous`
--

LOCK TABLES `lablescalecontinuous` WRITE;
/*!40000 ALTER TABLE `lablescalecontinuous` DISABLE KEYS */;
/*!40000 ALTER TABLE `lablescalecontinuous` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lablescalediskrete`
--

DROP TABLE IF EXISTS `lablescalediskrete`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `lablescalediskrete` (
  `IdLable` bigint(20) NOT NULL AUTO_INCREMENT,
  `Lable` int(11) NOT NULL,
  `IdUser` bigint(20) NOT NULL,
  `IdGroupImage` bigint(20) NOT NULL,
  PRIMARY KEY (`IdLable`),
  KEY `IdUser` (`IdUser`,`IdGroupImage`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lablescalediskrete`
--

LOCK TABLES `lablescalediskrete` WRITE;
/*!40000 ALTER TABLE `lablescalediskrete` DISABLE KEYS */;
/*!40000 ALTER TABLE `lablescalediskrete` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `repository`
--

DROP TABLE IF EXISTS `repository`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `repository` (
  `ID_Repository` bigint(20) NOT NULL AUTO_INCREMENT,
  `Path` varchar(100) NOT NULL,
  `ImageNumber` int(11) DEFAULT NULL,
  `LastChange` datetime NOT NULL,
  PRIMARY KEY (`ID_Repository`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `repository`
--

LOCK TABLES `repository` WRITE;
/*!40000 ALTER TABLE `repository` DISABLE KEYS */;
/*!40000 ALTER TABLE `repository` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `statetestcase`
--

DROP TABLE IF EXISTS `statetestcase`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `statetestcase` (
  `ID_StateTestCase` int(11) NOT NULL AUTO_INCREMENT,
  `DescriptionState` varchar(100) NOT NULL,
  PRIMARY KEY (`ID_StateTestCase`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `statetestcase`
--

LOCK TABLES `statetestcase` WRITE;
/*!40000 ALTER TABLE `statetestcase` DISABLE KEYS */;
INSERT INTO `statetestcase` VALUES (1,'DoWork');
/*!40000 ALTER TABLE `statetestcase` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `testcase`
--

DROP TABLE IF EXISTS `testcase`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `testcase` (
  `IDTestcase` bigint(20) NOT NULL AUTO_INCREMENT,
  `GeneralInfo` varchar(200) DEFAULT NULL,
  `Testquestion` varchar(100) DEFAULT NULL,
  `DiskreteScala` bit(1) DEFAULT NULL,
  `StateTestCase_ID_StateTestCase` int(11) NOT NULL,
  `NameTestCase` varchar(45) NOT NULL,
  `MinDiskreteScale` int(11) DEFAULT NULL,
  `MaxDiskreteScale` int(11) DEFAULT NULL,
  `ActiveLearning` bit(1) DEFAULT b'0',
  `userThreshold` int(11) DEFAULT NULL,
  `initialThreshold` int(11) DEFAULT NULL,
  `dbPath` varchar(500) NOT NULL DEFAULT 'S:\\Rohdaten\\ImageSimilarity',
  `iSeed` int(11) NOT NULL,
  `isActive` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`IDTestcase`,`StateTestCase_ID_StateTestCase`),
  KEY `fk_testcase_StatoTestCase1_idx` (`StateTestCase_ID_StateTestCase`),
  CONSTRAINT `fk_testcase_StatoTestCase1` FOREIGN KEY (`StateTestCase_ID_StateTestCase`) REFERENCES `statetestcase` (`ID_StateTestCase`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=133 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `testcase`
--

LOCK TABLES `testcase` WRITE;
/*!40000 ALTER TABLE `testcase` DISABLE KEYS */;
INSERT INTO `testcase` VALUES (127,'dummy testcase (not working)','Image quality?','\0',1,'Dummy Testcase',1,5,'\0',0,0,'\\\\DATA\\\\ImageDatabaseA',356010671,'\0');
/*!40000 ALTER TABLE `testcase` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `testcase_has_scale`
--

DROP TABLE IF EXISTS `testcase_has_scale`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `testcase_has_scale` (
  `IdTestCaseTypeScale` bigint(20) NOT NULL AUTO_INCREMENT,
  `testcase_IDTestcase` bigint(20) NOT NULL,
  `scale_IDScale` bigint(20) NOT NULL,
  `scale_IsDiscrete` tinyint(1) NOT NULL,
  PRIMARY KEY (`IdTestCaseTypeScale`),
  KEY `fk_testcase_has_TipologiaScalaContinua_testcase1_idx` (`testcase_IDTestcase`),
  CONSTRAINT `fk_testcase_has_TipologiaScalaContinua_testcase1` FOREIGN KEY (`testcase_IDTestcase`) REFERENCES `testcase` (`IDTestcase`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `testcase_has_scale`
--

LOCK TABLES `testcase_has_scale` WRITE;
/*!40000 ALTER TABLE `testcase_has_scale` DISABLE KEYS */;
/*!40000 ALTER TABLE `testcase_has_scale` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `testcase_has_typscalediskrete`
--

DROP TABLE IF EXISTS `testcase_has_typscalediskrete`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `testcase_has_typscalediskrete` (
  `testcase_IDTestcase` bigint(20) NOT NULL,
  `TypScaleDiskrete_ID_TypScaleDiskrete` bigint(20) NOT NULL,
  `IdTestCaseTypeScaleDiskrete` varchar(45) NOT NULL,
  PRIMARY KEY (`IdTestCaseTypeScaleDiskrete`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `testcase_has_typscalediskrete`
--

LOCK TABLES `testcase_has_typscalediskrete` WRITE;
/*!40000 ALTER TABLE `testcase_has_typscalediskrete` DISABLE KEYS */;
/*!40000 ALTER TABLE `testcase_has_typscalediskrete` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `testcase_has_typuser`
--

DROP TABLE IF EXISTS `testcase_has_typuser`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `testcase_has_typuser` (
  `testcase_IDTestcase` bigint(20) NOT NULL,
  `TypUser_ID_Typ` int(11) NOT NULL,
  PRIMARY KEY (`testcase_IDTestcase`,`TypUser_ID_Typ`),
  KEY `fk_testcase_has_TipologiaUser_TipologiaUser1_idx` (`TypUser_ID_Typ`),
  KEY `fk_testcase_has_TipologiaUser_testcase1_idx` (`testcase_IDTestcase`),
  CONSTRAINT `fk_testcase_has_TipologiaUser_TipologiaUser1` FOREIGN KEY (`TypUser_ID_Typ`) REFERENCES `typuser` (`ID_Typ`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_testcase_has_TipologiaUser_testcase1` FOREIGN KEY (`testcase_IDTestcase`) REFERENCES `testcase` (`IDTestcase`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `testcase_has_typuser`
--

LOCK TABLES `testcase_has_typuser` WRITE;
/*!40000 ALTER TABLE `testcase_has_typuser` DISABLE KEYS */;
/*!40000 ALTER TABLE `testcase_has_typuser` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `typetitle`
--

DROP TABLE IF EXISTS `typetitle`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `typetitle` (
  `Title_Id` int(2) NOT NULL AUTO_INCREMENT,
  `Title` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`Title_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `typetitle`
--

LOCK TABLES `typetitle` WRITE;
/*!40000 ALTER TABLE `typetitle` DISABLE KEYS */;
INSERT INTO `typetitle` VALUES (2,'Mrs'),(3,'Mr'),(4,'Dr.'),(5,'Professor');
/*!40000 ALTER TABLE `typetitle` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `typscalecontinuous`
--

DROP TABLE IF EXISTS `typscalecontinuous`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `typscalecontinuous` (
  `ID_TypScaleContinuous` int(11) NOT NULL AUTO_INCREMENT,
  `DescriptionScaleContinuous` varchar(100) NOT NULL,
  `VersionScaleCont` int(11) NOT NULL,
  `PathImageMin` varchar(1024) NOT NULL,
  `PathImageMax` varchar(1024) NOT NULL,
  PRIMARY KEY (`ID_TypScaleContinuous`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `typscalecontinuous`
--

LOCK TABLES `typscalecontinuous` WRITE;
/*!40000 ALTER TABLE `typscalecontinuous` DISABLE KEYS */;
INSERT INTO `typscalecontinuous` VALUES (1,'ContScale',1,'/DATA/imageMin.dcm','/DATA/imageMax.dcm');
/*!40000 ALTER TABLE `typscalecontinuous` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `typscalediskrete`
--

DROP TABLE IF EXISTS `typscalediskrete`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `typscalediskrete` (
  `ID_TypScaleDiskrete` int(11) NOT NULL AUTO_INCREMENT,
  `DescriptionScaleDiscrete` varchar(45) NOT NULL,
  `PathImageMin` varchar(1024) NOT NULL,
  `PathImageMax` varchar(1024) NOT NULL,
  PRIMARY KEY (`ID_TypScaleDiskrete`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `typscalediskrete`
--

LOCK TABLES `typscalediskrete` WRITE;
/*!40000 ALTER TABLE `typscalediskrete` DISABLE KEYS */;
INSERT INTO `typscalediskrete` VALUES (1,'DiscreteScale','/DATA/discreteMin.dcm','/DATA/discreteMax.dcm');
/*!40000 ALTER TABLE `typscalediskrete` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `typuser`
--

DROP TABLE IF EXISTS `typuser`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `typuser` (
  `ID_Typ` int(11) NOT NULL AUTO_INCREMENT,
  `DescriptionTypUser` varchar(50) NOT NULL,
  PRIMARY KEY (`ID_Typ`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `typuser`
--

LOCK TABLES `typuser` WRITE;
/*!40000 ALTER TABLE `typuser` DISABLE KEYS */;
INSERT INTO `typuser` VALUES (2,'administrator'),(3,'radiologist'),(4,'physicist'),(6,'student');
/*!40000 ALTER TABLE `typuser` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `id_user` bigint(20) NOT NULL AUTO_INCREMENT,
  `Name` varchar(30) NOT NULL,
  `Surname` varchar(30) NOT NULL,
  `title_Id` int(11) DEFAULT NULL,
  `PW` varchar(1024) DEFAULT NULL,
  `salt` varchar(1024) DEFAULT NULL,
  `username` varchar(20) NOT NULL,
  `email` varchar(100) NOT NULL,
  `ID_TypeUser` int(11) NOT NULL,
  `YearsofExperience` int(11) DEFAULT NULL,
  `DateInsert` datetime DEFAULT NULL,
  PRIMARY KEY (`id_user`,`ID_TypeUser`),
  UNIQUE KEY `id_user_UNIQUE` (`id_user`),
  KEY `fk_user_Tipologia_idx` (`ID_TypeUser`),
  CONSTRAINT `fk_user_Tipologia` FOREIGN KEY (`ID_TypeUser`) REFERENCES `typuser` (`ID_Typ`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=42 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (24,'Test','User',2,'QUuvoprbPMst+pSd7AljsJsg2CpJhK1ftDSxZpMAaS4=','GIZKNE1I+ODVTmnVv0FlzkVl8e3LjRoP60fjvT4dKhg=','1','test@mail.com',3,3,'2015-05-26 12:27:36'),(30,'Master','Admin',3,'0ALZquXmXv3zb7wC6qB7tXbVCkaCrbeSwBPEP71FWlY=','6R4e+yfYQIe1XVuzScZG6RDGLnPwYQ2KKStSMOW77Ac=','admin','admin@mail.com',2,10,'2016-06-13 10:15:20');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'labelingframework'
--
/*!50003 DROP PROCEDURE IF EXISTS `ChangePassword` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `ChangePassword`(IN PW varchar(1024), IN salt varchar(1024),
                               IN Email varchar(100))
BEGIN


UPDATE`user`

SET `labelingframework`.`user`.`PW` = PW,
	`labelingframework`.`user`.`salt` = salt
    
where (`labelingframework`.`user`.`email` = Email);

select
`labelingframework`.`user`.`email` 
from user where (`labelingframework`.`user`.`email` = Email);

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `deleteGroup` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteGroup`(
                                in IdGroup int
                                )
BEGIN

DELETE FROM `group` WHERE (`group`.IdGroup = IdGroup);


END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DeletePath` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `DeletePath`(IN idpath int)
BEGIN
	
	delete from configuration  where (id_path=idpath);
		

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DeleteScaleContinuous` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `DeleteScaleContinuous`(IN TypScaleContinuous bigint )
BEGIN

DELETE FROM typscalecontinuous
WHERE ID_TypScaleContinuous=TypScaleContinuous;


END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DeleteScaleDiscrete` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `DeleteScaleDiscrete`(IN IDScaleDisc int )
BEGIN

DELETE FROM typscalediskrete
WHERE ID_TypScaleDiskrete=IDScaleDisc;


END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DeleteTypeUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `DeleteTypeUser`(IN idtype bigint)
begin

DELETE FROM typuser
WHERE ID_Typ=idtype;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DeleteUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `DeleteUser`(IN iduser int)
BEGIN
	
	delete from user  where (id_user=iduser);
		

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getAllImagesOfTestcase` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllImagesOfTestcase`(in testcaseId bigint)
BEGIN
SELECT  
		`group_has_image`.`IdGroup`,
		 `PathImage`,
         `IsReference`,
         `IdGroupHasImage`,
         `NamePatiente`
         

	
from `group_has_image` 
inner join `group` on `group`.`IdGroup` = `group_has_image`.`IdGroup`
inner join `testcase` on `testcase`.`IDTestcase` = `group`.`IdTestCase`
where `group`.`IdTestCase` = testcaseId;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetDatabasePath` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetDatabasePath`(IN idpath int)
BEGIN
	
	select id_path,
		   path_label
           
    from configuration
	where ((idpath IS NULL) or (id_path=idpath));
		


END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetDiscreteLabel` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetDiscreteLabel`(IN iduser bigint,
								 IN IdGroupImage bigint)
BEGIN
	select Lable
    from lablescalediskrete 
    where lablescalediskrete.iduser=iduser and lablescalediskrete.IdGroupImage=IdGroupImage;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetGroupFromIdTestCase` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetGroupFromIdTestCase`( IN IdTestCase BIGINT)
BEGIN

SELECT `group`.`IdGroup`,
       `group`.`Name`,
       `group`.`IdTestCase`,
       `group`.`ReferenceIsGlobal` ,
       `group`.`IsPatientChosen`, 
       `group`.`PageStyle`, 
       `group`.`HasReference`, 
       `group`.`ImagesPerPage` 
FROM `group`
where `group`.`IdTestCase`=IdTestCase;


END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetGrouphasImageFromGroup` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetGrouphasImageFromGroup`(IN id_group BIGINT,in iduser BIGINT)
BEGIN


SELECT  
		 `IdGroup`,
		 `PathImage`,
         `IsReference`,
         `IdGroupHasImage`,
         `NamePatiente`,
         `lablescalediskrete`.`Lable`  as VoteDiscrete
	
from `group_has_image` 
LEFT OUTER JOIN `lablescalediskrete` on `group_has_image`.`IdGroupHasImage`=`lablescalediskrete`.`IdGroupImage` and `lablescalediskrete`.`IdUser`=iduser
where `group_has_image`.`IdGroup` = id_group;


END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetLabelsforIdTestcase` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetLabelsforIdTestcase`(IN idtestcase bigint)
BEGIN



SELECT distinct
	
    `lablescalediskrete`.`IdLable`,
    `lablescalediskrete`.`Lable`,
    `group_has_image`.`PathImage`,
    "Discrete Scale" as Type,
    testcase.NameTestCase,    
    user.Name,
    user.Surname,
    user.email,
    testcase.Testquestion,
	testcase.MaxDiskreteScale,
    ref.IsReference,
    REPLACE(ref.`PathImage`,CONCAT(ref.`NamePatiente`,"/"),"") as RefImage,
    user.id_user,
    0
    
    from testcase
	inner join   `group`  on `group`.`IdTestCase` =testcase.IDTestcase
    inner join  `group_has_image` on `group_has_image`.IdGroup = `group`.IdGroup
    left outer join  `group_has_image` as ref on `group_has_image`.IdGroup = ref.IdGroup and ref.IsReference=true
    inner join   `lablescalediskrete`on  `lablescalediskrete`.`IdGroupImage` =`group_has_image`.`IdGroupHasImage`
    inner join  user on user.id_user = `lablescalediskrete`.`IdUser`
    where testcase.IDTestcase=idtestcase
    
      
    union 

    
    
SELECT distinct
    `lablescalecontinuous`.`IdLable`,
    `lablescalecontinuous`.`Lable`,
    `group_has_image`.`PathImage`,    
    `typscalecontinuous`.`DescriptionScaleContinuous`,
    testcase.NameTestCase,
	user.Name,
    user.Surname,
    user.email,
	testcase.Testquestion,
      "NaN",
	ref.IsReference,
	REPLACE(ref.`PathImage`,CONCAT(ref.`NamePatiente`,"/"),"") as RefImage,
    user.id_user,
    `lablescalecontinuous`.`IdScaleContinuous`
    
   
    from testcase
	inner join   `group`  on `group`.`IdTestCase` =testcase.IDTestcase
    inner join  `group_has_image` on `group_has_image`.IdGroup = `group`.IdGroup
	left outer  join  `group_has_image` as ref on `group_has_image`.IdGroup = ref.IdGroup and ref.IsReference=true
    inner join  `lablescalecontinuous` on  `lablescalecontinuous`.`IdGroupImage` =`group_has_image`.`IdGroupHasImage`
    inner join  `typscalecontinuous` on  `typscalecontinuous`.`ID_TypScaleContinuous` =`lablescalecontinuous`.`IdScaleContinuous` 
	inner join  user on user.id_user = `lablescalecontinuous`.`IdUser`
	
    where testcase.IDTestcase=idtestcase;



END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetLableforIdUserIdTestcase` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetLableforIdUserIdTestcase`(IN idtestcase bigint,
								  IN iduser bigint)
BEGIN



SELECT distinct
	
    `lablescalediskrete`.`IdLable`,
    `lablescalediskrete`.`Lable`,
    `group_has_image`.`PathImage`,
    "Discrete Scale" as Type,
    testcase.NameTestCase,    
    user.Name,
    user.Surname,
    user.email,
    testcase.Testquestion,
	testcase.MaxDiskreteScale,
    ref.IsReference,
    REPLACE(ref.`PathImage`,CONCAT(ref.`NamePatiente`,"/"),"") as RefImage,
    0
    
    
    from testcase
	inner join   `group`  on `group`.`IdTestCase` =testcase.IDTestcase
    inner join  `group_has_image` on `group_has_image`.IdGroup = `group`.IdGroup
    left outer join  `group_has_image` as ref on `group_has_image`.IdGroup = ref.IdGroup and ref.IsReference=true
    inner join   `lablescalediskrete`on  `lablescalediskrete`.`IdGroupImage` =`group_has_image`.`IdGroupHasImage`
    inner join  user on user.id_user = `lablescalediskrete`.`IdUser`
    where testcase.IDTestcase=idtestcase
    and `lablescalediskrete`.`IdUser`=iduser
    -- or ref.`IdGroupHasImage` IS NOT NULL
    

      
      
    union 

    
    
SELECT distinct
    `lablescalecontinuous`.`IdLable`,
    `lablescalecontinuous`.`Lable`,
    `group_has_image`.`PathImage`,    
    `typscalecontinuous`.`DescriptionScaleContinuous`,
    testcase.NameTestCase,
	user.Name,
    user.Surname,
    user.email,
	testcase.Testquestion,
      "NaN",
	ref.IsReference,
	REPLACE(ref.`PathImage`,CONCAT(ref.`NamePatiente`,"/"),"") as RefImage,
    `lablescalecontinuous`.`IdScaleContinuous`
   
    from testcase
	inner join   `group`  on `group`.`IdTestCase` =testcase.IDTestcase
    inner join  `group_has_image` on `group_has_image`.IdGroup = `group`.IdGroup
	left outer  join  `group_has_image` as ref on `group_has_image`.IdGroup = ref.IdGroup and ref.IsReference=true
    inner join  `lablescalecontinuous` on  `lablescalecontinuous`.`IdGroupImage` =`group_has_image`.`IdGroupHasImage`
    inner join  `typscalecontinuous` on  `typscalecontinuous`.`ID_TypScaleContinuous` =`lablescalecontinuous`.`IdScaleContinuous` 
	inner join  user on user.id_user = `lablescalecontinuous`.`IdUser`
	
    where testcase.IDTestcase=idtestcase
    and `lablescalecontinuous`.`IdUser`=iduser;
    -- or ref.`IdGroupHasImage` IS NOT NULL;
   
   
   
   

    
    


END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getListOfUsers` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getListOfUsers`()
BEGIN
	select id_user,
		   Name,
		   Surname,
           Email,
           DescriptionTypUser,
		   YearsOfExperience,
           typuser.ID_Typ,
           username,
           Title
           
    from user
    inner join typuser on user.ID_TypeUser=typuser.ID_Typ
    inner join typetitle on user.title_Id=typetitle.title_Id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetlistUserByIdTestCase` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetlistUserByIdTestCase`(IN idTestCase BIGINT)
BEGIN

SELECT distinct
    Name,
    Surname,
    DescriptionTypUser,
    email,
    testcase.DiskreteScala,
    user.id_user
    from testcase
	inner join testcase_has_typuser on testcase_IDTestcase=IDTestcase
    inner join typuser on typuser.ID_Typ=testcase_has_typuser.TypUser_ID_Typ
    inner join user on user.ID_TypeUser=testcase_has_typuser.TypUser_ID_Typ
    where testcase.IDTestcase=idTestCase;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetNamePatiente` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetNamePatiente`(IN IdGroup BIGINT)
BEGIN

SELECT DISTINCT
    `group_has_image`.`NamePatiente`
FROM `group_has_image`
WHERE `group_has_image`.`IdGroup`=IdGroup;


END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetNumberOfPictures` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetNumberOfPictures`(in idtestcase bigint)
begin

SELECT 
	count(*) as CountImage 
  
    from testcase
    inner join `group` on `group`.`IdTestCase`=testcase.IDTestcase
    inner join group_has_image on group_has_image.IdGroup=`group`.`IdGroup`
    
    where testcase.IDTestcase=idtestcase and group_has_image.IsReference = 0;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getOverallNumberOfLabelled` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getOverallNumberOfLabelled`(IN idtestcase bigint)
BEGIN

	-- get discrete or continuous (one value is 0)
SELECT MAX(fullyLabelledCONTI) FROM

	-- continuous values
(select count(*) as fullyLabelledCONTI from
(SELECT IdUser, IdGroupImage, COUNT(*) AS count
FROM 
	testcase
	inner join   `group`  on `group`.`IdTestCase` = testcase.IDTestcase
    inner join  `group_has_image` on `group_has_image`.IdGroup = `group`.IdGroup
	left outer join  `group_has_image` as ref on `group_has_image`.IdGroup = ref.IdGroup and ref.IsReference=true
	inner join   `lablescalecontinuous`on  `lablescalecontinuous`.`IdGroupImage` =`group_has_image`.`IdGroupHasImage`
    where testcase.IDTestcase=idtestcase
GROUP BY IdUser, IdGroupImage) as overallNumber
Where count = (
	 -- get the amount of labels on one image
	select 
	count(*) as amount
	from testcase_has_scale
	where labelingframework.testcase_has_scale.testcase_IDTestcase = idtestcase
     -- -------------------------------------
    )
    
    -- combine discrete and continous values to a table
        union 
        
    -- discrete values
SELECT distinct
    count(distinct `lablescalediskrete`.`IdGroupImage`) as fullyLabelledDISCR
    
	from testcase
	inner join   `group`  on `group`.`IdTestCase` = testcase.IDTestcase
    inner join  `group_has_image` on `group_has_image`.IdGroup = `group`.IdGroup
	left outer join  `group_has_image` as ref on `group_has_image`.IdGroup = ref.IdGroup and ref.IsReference=true
	inner join   `lablescalediskrete`on  `lablescalediskrete`.`IdGroupImage` =`group_has_image`.`IdGroupHasImage`
    where testcase.IDTestcase=idtestcase) as overall;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getOverallNumberOfLabelledByUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getOverallNumberOfLabelledByUser`(IN idtestcase bigint, in UserID bigint)
BEGIN

	-- get discrete or continuous (one value is 0)
SELECT MAX(fullyLabelledCONTI) FROM

	-- continuous values
(select count(*) as fullyLabelledCONTI from
(SELECT IdUser, IdGroupImage, COUNT(*) AS count
FROM 
	testcase
	inner join   `group`  on `group`.`IdTestCase` = testcase.IDTestcase
    inner join  `group_has_image` on `group_has_image`.IdGroup = `group`.IdGroup
	left outer join  `group_has_image` as ref on `group_has_image`.IdGroup = ref.IdGroup and ref.IsReference=true
	inner join   `lablescalecontinuous`on  `lablescalecontinuous`.`IdGroupImage` =`group_has_image`.`IdGroupHasImage`
    where (`testcase`.`IDTestcase` = idtestcase and IdUser = UserID) 
GROUP BY IdUser, IdGroupImage) as overallNumber
Where count = (
	 -- get the amount of labels on one image
	select 
	count(*) as amount
	from testcase_has_scale
	where labelingframework.testcase_has_scale.testcase_IDTestcase = idtestcase
     -- -------------------------------------
    )
    
    -- combine discrete and continous values to a table
        union 
        
    -- discrete values
SELECT distinct
    count(*) as fullyLabelledDISCR
    
	from testcase
	inner join   `group`  on `group`.`IdTestCase` = testcase.IDTestcase
    inner join  `group_has_image` on `group_has_image`.IdGroup = `group`.IdGroup
	left outer join  `group_has_image` as ref on `group_has_image`.IdGroup = ref.IdGroup and ref.IsReference=true
	inner join   `lablescalediskrete`on  `lablescalediskrete`.`IdGroupImage` =`group_has_image`.`IdGroupHasImage`
    where (`testcase`.`IDTestcase` = idtestcase and IdUser = UserID)) as overall;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getPassword` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getPassword`(IN inusername varchar (20))
BEGIN
	
	select PW,
		   salt
            
    from user 
    where username=inusername;
		

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetPercentualViewTestCase` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetPercentualViewTestCase`(in IdUser BIGINT,
																		in idtestcase bigint)
begin

SELECT distinct
    testcase.IDTestcase,	   
	COUNT(lablescalecontinuous.IdLable) as LableContinuous,
	-- (select count(*) from group_has_image where IdGroup =`group`.`IdGroup` and IsReference=false) as CountImage ,
    (select count(*) from testcase_has_scale where testcase_IDTestcase=testcase.IDTestcase) as CountScaleContinue,
    ((COUNT(lablescalediskrete.IdLable)/COUNT(group_has_image.IdGroupHasImage))*100) as PercDiscrete
  
    from testcase
    inner join `group` on `group`.`IdTestCase`=testcase.IDTestcase
    inner join group_has_image on group_has_image.IdGroup=`group`.`IdGroup`
    left outer join lablescalediskrete on lablescalediskrete.IdUser=IdUser and lablescalediskrete.IdGroupImage=group_has_image.IdGroupHasImage
	left outer join lablescalecontinuous on lablescalecontinuous.IdUser=IdUser and lablescalecontinuous.IdGroupImage=group_has_image.IdGroupHasImage 
    
    where testcase.IDTestcase=idtestcase and group_has_image.IsReference = 0
    group by testcase.IDTestcase;
    

    

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetScaleContinuous` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetScaleContinuous`(in idScaleCont int)
BEGIN

	SELECT ID_TypScaleContinuous,
		   DescriptionScaleContinuous,
           VersionScaleCont,
           PathImageMin,
           PathImageMax
	FROM   typscalecontinuous
    where ((idScaleCont IS NULL) or (ID_TypScaleContinuous=idScaleCont));

    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetScaleDiscrete` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetScaleDiscrete`(in idScaleDisc int)
BEGIN

	SELECT ID_TypScaleDiskrete,
		   DescriptionScaleDiscrete,
           PathImageMin,
           PathImageMax
	FROM   typscalediskrete
    where ((idScaleDisc IS NULL) or (ID_TypScaleDiskrete=idScaleDisc));

    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetTestCase` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetTestCase`(IN idTypeUser int, IN idTestCaseIn bigint)
BEGIN
SELECT distinct
    IDTestcase,	   
	GeneralInfo,
    Testquestion,
    DiskreteScala,
    statetestcase.DescriptionState,
    NameTestCase,
    MinDiskreteScale,
    MaxDiskreteScale,
    ActiveLearning,
    dbPath,
    isActive,
    initialThreshold,
    userThreshold
    
    from testcase
    inner join testcase_has_typuser on testcase_IDTestcase=IDTestcase
    inner join statetestcase on StateTestCase_ID_StateTestCase=ID_StateTestCase
	where (((idTypeUser IS NULL) or (TypUser_ID_Typ=idTypeUser)) and ((idTestCaseIn IS NULL) or (IDTestcase=idTestCaseIn))); 
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetTitles` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetTitles`()
BEGIN
	
	select Title_ID,
		   Title         
    from typetitle;
   
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetTypeUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetTypeUser`(in idtypeuser int)
BEGIN
	
	select ID_Typ,
		   DescriptionTypUser          
    from typuser
    where  (ID_Typ = idtypeuser);
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetTypeUserForTestCase` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetTypeUserForTestCase`(IN Idtestcase bigint)
BEGIN


SELECT testcase_IDTestcase,
	   TypUser_ID_Typ
  FROM testcase_has_typuser 
where testcase_IDTestcase=Idtestcase;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetTypeUsers` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetTypeUsers`()
BEGIN
	
	select ID_Typ,
		   DescriptionTypUser          
    from typuser;
   
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getUniqueLabelsforIdTestcase` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getUniqueLabelsforIdTestcase`(IN idtestcase bigint)
BEGIN



SELECT distinct
	
    `lablescalediskrete`.`IdLable`,
    `lablescalediskrete`.`Lable`,
    `group_has_image`.`PathImage`,
    "Discrete Scale" as Type,
    testcase.NameTestCase,    
    user.Name,
    user.Surname,
    user.email,
    testcase.Testquestion,
	testcase.MaxDiskreteScale,
    ref.IsReference,
    REPLACE(ref.`PathImage`,CONCAT(ref.`NamePatiente`,"/"),"") as RefImage,
    user.id_user,
    0
    
    from testcase
	inner join   `group`  on `group`.`IdTestCase` =testcase.IDTestcase
    inner join  `group_has_image` on `group_has_image`.IdGroup = `group`.IdGroup
    left outer join  `group_has_image` as ref on `group_has_image`.IdGroup = ref.IdGroup and ref.IsReference=true
    inner join   `lablescalediskrete`on  `lablescalediskrete`.`IdGroupImage` =`group_has_image`.`IdGroupHasImage`
    inner join  user on user.id_user = `lablescalediskrete`.`IdUser`
    where testcase.IDTestcase=idtestcase
    
      
    union 

    
    
SELECT distinct
    `lablescalecontinuous`.`IdLable`,
    `lablescalecontinuous`.`Lable`,
    `group_has_image`.`PathImage`,    
    `typscalecontinuous`.`DescriptionScaleContinuous`,
    testcase.NameTestCase,
	user.Name,
    user.Surname,
    user.email,
	testcase.Testquestion,
      "NaN",
	ref.IsReference,
	REPLACE(ref.`PathImage`,CONCAT(ref.`NamePatiente`,"/"),"") as RefImage,
    user.id_user,
    `lablescalecontinuous`.`IdScaleContinuous`
    
   
    from testcase
	inner join   `group`  on `group`.`IdTestCase` =testcase.IDTestcase
    inner join  `group_has_image` on `group_has_image`.IdGroup = `group`.IdGroup
	left outer  join  `group_has_image` as ref on `group_has_image`.IdGroup = ref.IdGroup and ref.IsReference=true
    inner join  `lablescalecontinuous` on  `lablescalecontinuous`.`IdGroupImage` =`group_has_image`.`IdGroupHasImage`
    inner join  `typscalecontinuous` on  `typscalecontinuous`.`ID_TypScaleContinuous` =`lablescalecontinuous`.`IdScaleContinuous` 
	inner join  user on user.id_user = `lablescalecontinuous`.`IdUser`
	
    where testcase.IDTestcase=idtestcase;



END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getUser`(IN iduser int)
BEGIN
	
	select id_user,
		   Name,
		   Surname,
           Email,
           DescriptionTypUser,
		   YearsOfExperience,
           typuser.ID_Typ,
           DateInsert,
           PW,
           username
           
    from user
	inner join typuser on user.ID_TypeUser=ID_Typ
    where ((iduser IS NULL) or (id_user=iduser));
		

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getUserWithEmail` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getUserWithEmail`(in in_email varchar(1024))
BEGIN

select distinct
typetitle.Title,
surname,
username


from user
inner join typetitle on user.title_Id=typetitle.title_Id

where user.email = in_email;


END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetVoteLableContinuous` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetVoteLableContinuous`(IN idgroupimage BIGINT, IN iduser BIGINT)
begin
	SELECT
    `lablescalecontinuous`.`Lable`,   
    `lablescalecontinuous`.`IdScaleContinuous`,
    `lablescalecontinuous`.`IdGroupImage`
	FROM `lablescalecontinuous`
    where (`lablescalecontinuous`.`IdGroupImage`=idgroupimage
      AND `lablescalecontinuous`.`IdUser`=iduser);

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `InsertConfiguration` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertConfiguration`(IN Labeling varchar(500))
BEGIN

DELETE FROM configuration;

	INSERT INTO `configuration`
(Labeling)
VALUES
(Labeling);



END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `InsertGroup` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertGroup`(IN Name varchar(50),
								IN IdTestCase bigint,
                                IN ReferenceIsGlobal BIT,
                                IN IsPatientChosen BIT,
								IN PageStyle int,
                                in HasReference bool,
                                in ImagesPerPage int,
                                OUT IdGroup BIGINT
                                )
BEGIN


	INSERT INTO `group`
(`Name`,`IdTestCase`,`ReferenceIsGlobal`,`IsPatientChosen`, `PageStyle`, `HasReference`,`ImagesPerPage`)
VALUES
(Name,
IdTestCase,
ReferenceIsGlobal,
IsPatientChosen,
PageStyle,
HasReference,
ImagesPerPage);

SELECT LAST_INSERT_ID()  INTO IdGroup;



END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `InsertImage` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertImage`(IN Name_Image VARCHAR(100),
								  IN label INT,
								  IN ID_Repository bigint)
BEGIN

INSERT INTO `image`
(`Name_Image`,
`Label`,
`Repository_ID_Repository`)
VALUES
(Name_Image,
label,
ID_Repository);



END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `InsertLableScaleContinuous` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertLableScaleContinuous`(IN Lable decimal(18,2),
                                               IN IdUser BIGINT,
                                               IN IdGroupImage BIGINT,
                                               IN IdScaleContinuous BIGINT
                                                )
BEGIN

DELETE FROM lablescalecontinuous WHERE ((lablescalecontinuous.IdUser = IdUser) and (lablescalecontinuous.IdGroupImage=IdGroupImage) and (lablescalecontinuous.IdScaleContinuous=IdScaleContinuous));
INSERT INTO `lablescalecontinuous`

(
    `Lable`,
    `IdUser`,
    `IdGroupImage`,
    `IdScaleContinuous`)
    
    VALUES
  ( Lable,
    IdUser,
    IdGroupImage,
    IdScaleContinuous);
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `InsertLableScaleDiskrete` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertLableScaleDiskrete`( IN Lable INT,
											  IN IdUser BIGINT,
											  IN IdGroupImage BIGINT)
BEGIN

DELETE FROM lablescalediskrete WHERE (lablescalediskrete.IdUser = IdUser and lablescalediskrete.IdGroupImage=IdGroupImage);
INSERT INTO lablescalediskrete (`Lable`,`IdUser`,`IdGroupImage`) VALUES (Lable,IdUser,IdGroupImage);
  
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `InsertPath` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertPath`(IN pathlabel varchar(500))
BEGIN
   
   INSERT INTO `configuration`
(`path_label`)
VALUES
(pathlabel);


END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `InsertRelationshipImageTestCase` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertRelationshipImageTestCase`( IN IdGroup bigint ,
								   IN PathImage varchar(1024),
                                   IN NamePatiente varchar(50),
                                   IN IsReference bool)
BEGIN


	INSERT INTO `group_has_image`
(`IdGroup`,
`PathImage`,
`IsReference`,
`NamePatiente`)
VALUES
(IdGroup,
PathImage,
IsReference,
NamePatiente);

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `InsertRepository` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertRepository`(IN Path VARCHAR(100),
					 IN ImageNumber int,
                     IN LastChange Datetime,
                     OUT Id_Repository bigint)
BEGIN
INSERT INTO `repository`
(
`Path`,
`ImageNumber`,
`LastChange`)
VALUES
(Path,
ImageNumber,
LastChange);


SELECT LAST_INSERT_ID()  INTO Id_Repository;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `InsertTestCase` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertTestCase`(IN GeneralInfo VARCHAR(200),
								   IN Testquestion VARCHAR(100),
								                                   IN DiskreteScala BOOL,
                                   IN StateTestCase INT,
                                   IN NameTestCase varchar(45),
                                   IN MinDiskreteScale INT,
                                   IN MaxDiskreteScale INT,
                                   IN ActiveLearning BOOl,
                                   IN DBPathIn varchar(500),
                                   IN SeedIn INT,
                                   IN userThreshold int,
                                   IN initialThreshold int,
                                   OUT IDTestCase BIGINT)
BEGIN
INSERT INTO `testcase`
(
`GeneralInfo`,
`Testquestion`,
`DiskreteScala`,
`StateTestCase_ID_StateTestCase`,
`NameTestCase`,
`MinDiskreteScale`,
`MaxDiskreteScale`,
`ActiveLearning`,
`dbPath`,
`iSeed`,
`userThreshold`,
`initialThreshold`
)
VALUES
(
GeneralInfo,
Testquestion,
DiskreteScala,
StateTestCase,
NameTestCase,
MinDiskreteScale,
MaxDiskreteScale,
ActiveLearning,
DBPathIn,
SeedIn,
userThreshold,
initialThreshold

);


SELECT LAST_INSERT_ID()  INTO IDTestCase;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `InsertTestCasetoScale` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertTestCasetoScale`(IN IdTestCase bigint,
								  IN IdScale bigint, 
                                  IN lIsDiscrete tinyint)
BEGIN

INSERT INTO `testcase_has_scale`
(`testcase_IDTestcase`,
`scale_IDScale`,
`scale_IsDiscrete`)
VALUES
(IdTestCase,
IdScale,
lIsDiscrete);




END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `InsertTestCasetoTypeUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertTestCasetoTypeUser`(IN IdTestCase bigint,
					 IN IdTypeuser bigint)
BEGIN
INSERT INTO `testcase_has_typuser`
(`testcase_IDTestcase`,
`TypUser_ID_Typ`)
VALUES
(IdTestCase,
 IdTypeuser);

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `InsertTypeScaleContinuous` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertTypeScaleContinuous`(
																		IN DescriptionScaleContinuous varchar(50),
                                                                        IN VersionScaleContinuous int(11),
                                                                        IN PathImageMin varchar(1024),
                                                                        IN PathImageMax varchar(1024)
                                                                        
                                                                        )
begin


INSERT INTO typscalecontinuous
(`DescriptionScaleContinuous`,
`VersionScaleCont`,
`PathImageMin`,
`PathImageMax`)
VALUES
(DescriptionScaleContinuous,
VersionScaleContinuous,
PathImageMin,
PathImageMax);

				  
end ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `InsertTypeScaleDiscrete` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertTypeScaleDiscrete`(
																		IN DescriptionScaleDiscrete varchar(50),
                                                                        IN PathImageMin varchar(1024),
                                                                        IN PathImageMax varchar(1024)
                                                                        
                                                                        )
begin


INSERT INTO typscalediskrete
(`DescriptionScaleDiscrete`,
`PathImageMin`,
`PathImageMax`)
VALUES
(DescriptionScaleDiscrete,
PathImageMin,
PathImageMax);

				  
end ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `InsertTypeUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertTypeUser`(IN DescriptionType varchar(50))
BEGIN
   
   INSERT INTO `typuser`
(`DescriptionTypUser`)
VALUES
(DescriptionType);


END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `LoginUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `LoginUser`(IN inusername varchar (20),IN password varchar(1024))
BEGIN
	
	select Name,
		   Surname,
           Email,
           ID_TypeUser,
		   YearsOfExperience,
           id_user,
           Title,
           username,
           DescriptionTypUser
           
    from user 
    inner join typuser on user.ID_TypeUser=typuser.ID_Typ
    inner join typetitle on user.title_Id=typetitle.title_Id
    where username=inusername and PW=password;
		

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `RegisterUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `RegisterUser`(IN name varchar(30),
							   IN Surname varchar(30),
                               in title_Id int,
                               IN Password varchar(1024),
                               IN Salt varchar(1024),
							   IN Username varchar(20),
                               IN Email varchar(100),
							   IN Type int,
							   IN YearOfExperience int,
                               IN DateInsert Datetime)
begin

insert into user (Name,
				  Surname,
                  title_Id,
				  PW,
                  salt,
				  username,
				  email,
                  ID_TypeUser,
                  YearsOfExperience,
                  DateInsert
                  )
                  values 
                  (name,
                   Surname,
                   title_Id,
                   Password,
                   Salt,
                   Username,
                   Email,
                   Type,
                   YearOfExperience,
                   DateInsert
                   );
				  

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `removeTestcase` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `removeTestcase`(IN testcaseID bigint(20))
BEGIN

delete from labelingframework.testcase_has_typuser
where labelingframework.testcase_has_typuser.testcase_IDTestcase = testcaseID;

delete from labelingframework.testcase_has_scale
where labelingframework.testcase_has_scale.testcase_IDTestcase = testcaseID;

delete from labelingframework.testcase
where labelingframework.testcase.IDTestcase = testcaseID;


END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `test` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `test`()
BEGIN

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `ToggleTestCase` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `ToggleTestCase`(IN IDTestcaseIn bigint)
BEGIN
	
	update testcase
    set `isActive`= NOT `isActive`
    where (IDTestcase=IDTestcaseIn);
	
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `TypeScaleContinuousFromIdTestCase` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `TypeScaleContinuousFromIdTestCase`(IN IdTestCase BIGINT)
BEGIN
		
        	SELECT     `testcase_has_scale`.`testcase_IDTestcase`,
				`testcase_has_scale`.`scale_IDScale`,
				`typscalecontinuous`.`DescriptionScaleContinuous`,
                `typscalecontinuous`.`VersionScaleCont`,
				`typscalecontinuous`.`PathImageMin`,
				`typscalecontinuous`.`PathImageMax`
                
FROM            `testcase_has_scale`
INNER JOIN      `typscalecontinuous` on `typscalecontinuous`.`ID_TypScaleContinuous`=`testcase_has_scale`.`scale_IDScale`
where  (`testcase_has_scale`.`testcase_IDTestcase` = IdTestCase) AND (`testcase_has_scale`.`scale_IsDiscrete` = 0);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `TypeScaleDiscreteFromIdTestCase` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `TypeScaleDiscreteFromIdTestCase`(IN IdTestCase BIGINT)
BEGIN
		
        	SELECT     `testcase_has_scale`.`testcase_IDTestcase`,
				`testcase_has_scale`.`scale_IDScale`,
				`typscalediskrete`.`DescriptionScaleDiscrete`,
				`typscalediskrete`.`PathImageMin`,
				`typscalediskrete`.`PathImageMax`
                
FROM            `testcase_has_scale`
INNER JOIN      `typscalediskrete` on `typscalediskrete`.`ID_TypScaleDiskrete`=`testcase_has_scale`.`scale_IDScale`
where  (`testcase_has_scale`.`testcase_IDTestcase` = IdTestCase) AND (`testcase_has_scale`.`scale_IsDiscrete` = 1);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `updateGroup` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `updateGroup`(IN Name varchar(50),
								IN IdTestCase bigint,
                                IN ReferenceIsGlobal BIT,
                                IN IsPatientChosen BIT,
								IN PageStyle int,
                                in HasReference bool,
                                in ImagesPerPage int,
                                in IdGroup int
                                )
BEGIN

DELETE FROM `group` WHERE `group`.IdTestCase = IdTestCase;
	INSERT INTO `group`
(`Name`,`IdTestCase`,`ReferenceIsGlobal`,`IsPatientChosen`, `PageStyle`, `HasReference`,`ImagesPerPage`)
VALUES
(Name,
IdTestCase,
ReferenceIsGlobal,
IsPatientChosen,
PageStyle,
HasReference,
ImagesPerPage,
IdGroup);



END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UpdatePath` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `UpdatePath`(IN idpath int,
														 IN pathLabel varchar(500))
BEGIN
	
	update configuration
    set path_label=pathLabel
    where (id_path=idpath);
		

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UpdateProfileView` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `UpdateProfileView`(IN name varchar(30),
							   IN Surname varchar(30),
                               IN PW varchar(1024),
                               IN Salt varchar(1024),
							   IN Username varchar(20),
                               IN Email varchar(100),
                               IN iduser bigint)
begin


UPDATE`user`
SET

`Name` = name,
`Surname` =Surname,
`PW` = PW,
`salt` = Salt,
`username` = Username,
`email` = Email

where (id_user=iduser);


END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UpdateScaleContinuous` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `UpdateScaleContinuous`(IN idtypescalecontinuous int,
                                           IN TypScaleContinuous varchar(100),
                                           IN VersionScaleContIn int,
                                           IN ImageMin varchar(1024),
                                           IN ImageMax varchar(1024)
                                           )
BEGIN

   update typscalecontinuous
   set DescriptionScaleContinuous=TypScaleContinuous,
	   VersionScaleCont=VersionScaleContIn,
       PathImageMin=ImageMin,
       PathImageMax=ImageMax
   where (ID_TypScaleContinuous = idtypescalecontinuous);



END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UpdateScaleContinuousMax` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `UpdateScaleContinuousMax`(IN idtypescalecontinuous int,
                                           IN TypScaleContinuous varchar(100),
                                           IN VersionScaleContIn int,
                                           IN ImageMax varchar(1024)
                                           )
BEGIN

   update typscalecontinuous
   set DescriptionScaleContinuous=TypScaleContinuous,
	   VersionScaleCont=VersionScaleContIn,
       PathImageMax=ImageMax
   where (ID_TypScaleContinuous = idtypescalecontinuous);



END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UpdateScaleContinuousMin` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `UpdateScaleContinuousMin`(IN idtypescalecontinuous int,
                                           IN TypScaleContinuous varchar(100),
                                           IN VersionScaleContIn int,
                                           IN ImageMin varchar(1024)
                                           )
BEGIN

   update typscalecontinuous
   set DescriptionScaleContinuous=TypScaleContinuous,
	   VersionScaleCont=VersionScaleContIn,
       PathImageMin=ImageMin
   where (ID_TypScaleContinuous = idtypescalecontinuous);



END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UpdateScaleContinuousNone` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `UpdateScaleContinuousNone`(IN idtypescalecontinuous int,
                                           IN TypScaleContinuous varchar(100),
                                           IN VersionScaleContIn int
                                           )
BEGIN

   update typscalecontinuous
   set DescriptionScaleContinuous=TypScaleContinuous,
	   VersionScaleCont=VersionScaleContIn
   where (ID_TypScaleContinuous = idtypescalecontinuous);



END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UpdateScaleDiscrete` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `UpdateScaleDiscrete`(IN idtypescalediscrete int,
                                           IN TypScaleDiscrete varchar(100),
                                           IN ImageMin varchar(1024),
                                           IN ImageMax varchar(1024)
                                           )
BEGIN

   update typscalediskrete
   set DescriptionScaleDiscrete=TypScaleDiscrete,
       PathImageMin=ImageMin,
       PathImageMax=ImageMax
   where (ID_TypScaleDiskrete = idtypescalediscrete);



END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UpdateScaleDiscreteMax` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `UpdateScaleDiscreteMax`(IN idtypescalediscrete int,
                                           IN TypScaleDiscrete varchar(100),
                                           IN ImageMax varchar(1024)
                                           )
BEGIN

   update typscalediskrete
   set DescriptionScaleDiscrete=TypScaleDiscrete,
       PathImageMax=ImageMax
   where (ID_TypScaleDiskrete = idtypescalediscrete);



END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UpdateScaleDiscreteMin` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `UpdateScaleDiscreteMin`(IN idtypescalediscrete int,
                                           IN TypScaleDiscrete varchar(100),
                                           IN ImageMin varchar(1024)
                                           )
BEGIN

   update typscalediskrete
   set DescriptionScaleDiscrete=TypScaleDiscrete,
       PathImageMin=ImageMin
   where (ID_TypScaleDiskrete = idtypescalediscrete);



END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UpdateScaleDiscreteNone` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `UpdateScaleDiscreteNone`(IN idtypescalediscrete int,
                                           IN TypScaleDiscrete varchar(100)
                                           )
BEGIN

   update typscalediskrete
   set DescriptionScaleDiscrete=TypScaleDiscrete
   where (ID_TypScaleDiskrete = idtypescalediscrete);



END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UpdateTestCase` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `UpdateTestCase`(IN idtestcase BIGINT,
								   IN activelearning bool,
                                   in generalinfo varchar(200),
                                   in userThreshold int,
                                   in initalThreshold int)
BEGIN


UPDATE `testcase`
SET `GeneralInfo` = generalinfo,
    `ActiveLearning` = activelearning,
    `userThreshold` = userThreshold,
    `initalThreshold` = initalThreshold
WHERE `IDTestcase` = idtestcase;



END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UpdateTypeUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `UpdateTypeUser`(IN idtypeuser int,
															 IN TypeUser varchar(50))
BEGIN
	
	update typuser
    set DescriptionTypUser=TypeUser
    where (ID_Typ=idtypeuser);
		

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UpdateUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `UpdateUser`(IN iduser int,
														 IN TypeUser int)
BEGIN
	
	update user
    set ID_TypeUser=TypeUser
    where (id_user=iduser);
		

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-11-21 18:19:18
