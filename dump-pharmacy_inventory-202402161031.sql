-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: localhost    Database: pharmacy_inventory
-- ------------------------------------------------------
-- Server version	8.0.33

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Dumping data for table `account`
--

LOCK TABLES `account` WRITE;
/*!40000 ALTER TABLE `account` DISABLE KEYS */;
INSERT INTO `account` VALUES (1,'owner','Nguyen Van A','','HDXZ0GBHWLsufRCIfuTXmg==;lbHuU3EhioSYGVNPAYLgDc/R28rgIvyDzwRWLBm9ZGc=','OWNER','ACTIVE','0123456789',0),(3,'staff','Tran Thi B','','s235k7+AlclIqhtmJMAdoA==;vTSe9MBmw6+nYq2KdOSFd+GABQIGiD022/aNzRxt+ow=','Staff','ACTIVE','0192837465',0);
/*!40000 ALTER TABLE `account` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `category`
--

LOCK TABLES `category` WRITE;
/*!40000 ALTER TABLE `category` DISABLE KEYS */;
INSERT INTO `category` VALUES (1,'Thuốc',1,NULL,0),(2,'Hàng hóa',1,NULL,0),(3,'Thuốc kháng sinh',0,1,0),(4,'Thuốc hạ sốt - giảm đau',0,1,0),(5,'Thuốc Chống viêm',0,1,0),(6,'Thuốc chống dị ứng - Kháng histamin',0,1,0),(7,'Thuốc Kháng virus',0,1,0),(8,'Thuốc Ho và Long đờm',0,1,0),(9,'Thuốc Dạ dày',0,1,0),(10,'Thuốc Tiêu hóa',0,1,0),(11,'Thuốc trị rối loạn kinh nguyệt',0,1,0),(12,'Thuốc Huyết áp tim mạch',0,1,0),(13,'Thuốc điều trị mỡ máu',0,1,0),(14,'Tránh thai',0,1,0),(15,'Thuốc kháng nấm',0,1,0),(16,'Thuốc vitamin – khoáng chất',0,2,0),(17,'Thuốc trị tuần hoàn máu não , chóng mặt',0,1,0),(18,'Thuốc điều trị các bệnh về gan Gan',0,1,0),(19,'Thuốc trị sỏi thận',0,1,0),(20,'Thuốc trị giun',0,1,0),(21,'Thuốc nhỏ mắt',0,1,0),(22,'Thuốc bôi ngoài da',0,1,0),(23,'Vật tư y tế',0,2,0),(24,'Dầu bôi',0,2,0),(25,'Thuốc Phụ khoa',0,1,0),(26,'Thực phẩm chức năng',0,2,0);
/*!40000 ALTER TABLE `category` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-02-16 10:31:48
