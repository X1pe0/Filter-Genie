CREATE TABLE `orgs` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`org_key` VARCHAR(32),
	`max_agents` INT,
	PRIMARY KEY (`id`), UNIQUE (`org_key`)
);

CREATE TABLE `agents` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`org_key` VARCHAR(32),
	`agent_key` VARCHAR(40),
	`host_name` VARCHAR(32),
	`personal_hash` VARCHAR(64),
	PRIMARY KEY (`id`), UNIQUE (`agent_key`)
);

CREATE TABLE `global_hosts` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`org_key` VARCHAR(32),
	`host` VARCHAR(250),
	PRIMARY KEY (`id`)
);

CREATE TABLE `agent_hosts` (
	`id` INT NOT NULL AUTO_INCREMENT,
    `agent_key` VARCHAR(40),
	`host` VARCHAR(250),
	PRIMARY KEY (`id`)
);