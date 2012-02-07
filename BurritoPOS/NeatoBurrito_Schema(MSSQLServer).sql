delimiter $$

CREATE DATABASE `neatoburrito` /*!40100 DEFAULT CHARACTER SET latin1 */$$

delimiter $$

CREATE TABLE `burrito` (
  `id` char(36) DEFAULT NULL,
  `flourTortilla` tinyint(1) DEFAULT NULL,
  `chiliTortilla` tinyint(1) DEFAULT NULL,
  `jalapenoCheddarTortilla` tinyint(1) DEFAULT NULL,
  `tomatoBasilTortilla` tinyint(1) DEFAULT NULL,
  `herbGarlicTortilla` tinyint(1) DEFAULT NULL,
  `wheatTortilla` tinyint(1) DEFAULT NULL,
  `whiteRice` tinyint(1) DEFAULT NULL,
  `brownRice` tinyint(1) DEFAULT NULL,
  `blackBeans` tinyint(1) DEFAULT NULL,
  `pintoBeans` tinyint(1) DEFAULT NULL,
  `chicken` tinyint(1) DEFAULT NULL,
  `beef` tinyint(1) DEFAULT NULL,
  `hummus` tinyint(1) DEFAULT NULL,
  `salsaPico` tinyint(1) DEFAULT NULL,
  `salsaVerde` tinyint(1) DEFAULT NULL,
  `salsaSpecial` tinyint(1) DEFAULT NULL,
  `guacamole` tinyint(1) DEFAULT NULL,
  `lettuce` tinyint(1) DEFAULT NULL,
  `jalapenos` tinyint(1) DEFAULT NULL,
  `tomatoes` tinyint(1) DEFAULT NULL,
  `cucumber` tinyint(1) DEFAULT NULL,
  `onion` tinyint(1) DEFAULT NULL,
  `price` decimal(10,2) DEFAULT NULL,
  `orderID` char(36) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1$$

delimiter $$

CREATE TABLE `customer` (
  `id` char(36) DEFAULT NULL,
  `firstName` varchar(128) DEFAULT NULL,
  `lastName` varchar(128) DEFAULT NULL,
  `emailaddress` varchar(256) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1$$

delimiter $$

CREATE TABLE `employee` (
  `employeeID` char(36) DEFAULT NULL,
  `firstName` varchar(128) DEFAULT NULL,
  `lastName` varchar(128) DEFAULT NULL,
  `isManager` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1$$

delimiter $$

CREATE TABLE `inventory` (
  `id` char(36) DEFAULT NULL,
  `flourTortillaQty` int(11) DEFAULT NULL,
  `chiliTortillaQty` int(11) DEFAULT NULL,
  `jalapenoCheddarTortillaQty` int(11) DEFAULT NULL,
  `tomatoBasilTortillaQty` int(11) DEFAULT NULL,
  `herbGarlicTortillaQty` int(11) DEFAULT NULL,
  `wheatTortillaQty` int(11) DEFAULT NULL,
  `whiteRiceQty` int(11) DEFAULT NULL,
  `brownRiceQty` int(11) DEFAULT NULL,
  `blackBeansQty` int(11) DEFAULT NULL,
  `pintoBeansQty` int(11) DEFAULT NULL,
  `chickenQty` int(11) DEFAULT NULL,
  `beefQty` int(11) DEFAULT NULL,
  `hummusQty` int(11) DEFAULT NULL,
  `salsaPicoQty` int(11) DEFAULT NULL,
  `salsaVerdeQty` int(11) DEFAULT NULL,
  `salsaSpecialQty` int(11) DEFAULT NULL,
  `guacamoleQty` int(11) DEFAULT NULL,
  `lettuceQty` int(11) DEFAULT NULL,
  `jalapenosQty` int(11) DEFAULT NULL,
  `tomatoesQty` int(11) DEFAULT NULL,
  `cucumberQty` int(11) DEFAULT NULL,
  `onionQty` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1$$

delimiter $$

CREATE TABLE `orders` (
  `orderID` char(36) DEFAULT NULL,
  `orderDate` datetime DEFAULT NULL,
  `submitted` tinyint(1) DEFAULT NULL,
  `complete` tinyint(1) DEFAULT NULL,
  `totalCost` decimal(10,2) DEFAULT NULL,
  `inventoryID` char(32) DEFAULT NULL,
  `isComplete` tinyint(1) DEFAULT NULL,
  `isSubmitted` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1$$








