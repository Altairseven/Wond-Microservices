# create databases
CREATE DATABASE IF NOT EXISTS `authdb`;
CREATE DATABASE IF NOT EXISTS `notifdb`;
CREATE DATABASE IF NOT EXISTS `paramsdb`;
CREATE DATABASE IF NOT EXISTS `sellsdb`;
CREATE DATABASE IF NOT EXISTS `stockdb`;

# create root user and grant rights
CREATE USER 'root'@'localhost' IDENTIFIED BY 'local';
GRANT ALL PRIVILEGES ON *.* TO 'root'@'%';
