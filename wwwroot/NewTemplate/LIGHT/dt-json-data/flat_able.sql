-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Aug 24, 2017 at 06:07 AM
-- Server version: 10.1.13-MariaDB
-- PHP Version: 5.6.21

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `flat_able`
--

-- --------------------------------------------------------

--
-- Table structure for table `main_table`
--

CREATE TABLE `main_table` (
  `id` int(20) NOT NULL,
  `first_name` varchar(20) NOT NULL,
  `last_name` varchar(20) NOT NULL,
  `position` varchar(50) NOT NULL,
  `office` varchar(20) NOT NULL,
  `start_date` varchar(20) NOT NULL,
  `salary` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `main_table`
--

INSERT INTO `main_table` (`id`, `first_name`, `last_name`, `position`, `office`, `start_date`, `salary`) VALUES
(1, 'Airi', 'Satou', 'Accountant', 'Tokyo', '28th Nov 08', '162700'),
(2, 'Angelica', 'Ramos', 'Chief Executive ', 'London', '9th Oct 09', '1200000'),
(3, 'Ashton', 'Cox', 'Junior Technical Author', 'San Francisco', '12th Jan 09', '86000'),
(4, 'Bradley', 'Greer', 'Chief Executive Officer (CEO)', 'London', '13th Oct 12', '132000'),
(5, 'Brielle', 'Williamson', 'Integration Specialist', 'New York', '2nd Dec 12', '372000'),
(6, 'Bruno', 'Nash', 'Software Engineer', 'London', '3rd May 11', '163500'),
(7, 'Caesar', 'Vance', 'Pre-Sales Support', 'New York', '12th Dec 11', '106450'),
(8, 'Cara', 'Stevens', 'Sales Assistant', 'New York', '6th Dec 11', '145600'),
(9, 'Cedric', 'Kelly', 'Senior Javascript Developer', 'Edinburgh', '29th Mar 12', '433060');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `main_table`
--
ALTER TABLE `main_table`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `main_table`
--
ALTER TABLE `main_table`
  MODIFY `id` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
