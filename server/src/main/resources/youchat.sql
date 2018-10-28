/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50719
Source Host           : localhost:3306
Source Database       : youchat

Target Server Type    : MYSQL
Target Server Version : 50719
File Encoding         : 65001

Date: 2018-02-21 11:20:52
*/

-- SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for error
-- ----------------------------
DROP TABLE IF EXISTS "error";
CREATE TABLE "error" (
  "id" varchar(32) NOT NULL,
  "num" bigint NOT NULL,
  "message" varchar(200) DEFAULT NULL,
  "detail" text,
  "create_time" time NOT NULL,
  "update_time" time DEFAULT NULL,
  PRIMARY KEY ("id")
);

COMMENT ON TABLE "error" IS '错误信息';
COMMENT ON COLUMN "error"."id" IS 'GUID';
COMMENT ON COLUMN "error"."num" IS '流水号';
COMMENT ON COLUMN "error"."message" IS '错误信息';
COMMENT ON COLUMN "error"."detail" IS '错误明细';
COMMENT ON COLUMN "error"."create_time" IS '创建时间';
COMMENT ON COLUMN "error"."update_time" IS '最后更新时间';

-- ----------------------------
-- Table structure for note_type
-- ----------------------------
DROP TABLE IF EXISTS "note_type";
CREATE TABLE "note_type" (
  "id" varchar(32) NOT NULL,
  "num" bigint NOT NULL,
  "name" varchar(20) DEFAULT NULL,
  "detail" text,
  "create_time" time NOT NULL,
  "update_time" time DEFAULT NULL,
  PRIMARY KEY ("id")
);

COMMENT ON TABLE "note_type" IS '贴子类型';
COMMENT ON COLUMN "note_type"."id" IS 'GUID';
COMMENT ON COLUMN "note_type"."num" IS '流水号';
COMMENT ON COLUMN "note_type"."name" IS '类型名称';
COMMENT ON COLUMN "note_type"."detail" IS '类型描述';
COMMENT ON COLUMN "note_type"."create_time" IS '创建时间';
COMMENT ON COLUMN "note_type"."update_time" IS '最后更新时间';

-- ----------------------------
-- Table structure for user
-- ----------------------------
DROP TABLE IF EXISTS "users";
CREATE TABLE "users" (
  "id" varchar(32) NOT NULL,
  "num" bigint NOT NULL,
  "user_name" varchar(20) NOT NULL,
  "login_name" varchar(50) NOT NULL,
  "password" varchar(32) NOT NULL,
  "create_time" time NOT NULL,
  "update_time" time DEFAULT NULL,
  PRIMARY KEY ("id")
);

CREATE UNIQUE INDEX "users_login_name" ON "users" (
  "login_name"
);

COMMENT ON TABLE "users" IS '用户信息';
COMMENT ON COLUMN "users"."id" IS 'GUID';
COMMENT ON COLUMN "users"."num" IS '流水号';
COMMENT ON COLUMN "users"."user_name" IS '用户名';
COMMENT ON COLUMN "users"."login_name" IS '登录账号';
COMMENT ON COLUMN "users"."password" IS '密码';
COMMENT ON COLUMN "users"."create_time" IS '创建时间';
COMMENT ON COLUMN "users"."update_time" IS '最后更新时间';

-- ----------------------------
-- Table structure for note
-- ----------------------------
DROP TABLE IF EXISTS "note";
CREATE TABLE "note" (
  "id" varchar(32) NOT NULL,
  "num" bigint NOT NULL,
  "title" varchar(200) DEFAULT NULL,
  "content" text,
  "type_id" varchar(32) NOT NULL,
  "create_time" time NOT NULL,
  "update_time" time DEFAULT NULL,
  PRIMARY KEY ("id"),
  CONSTRAINT "fk_note_type_id" FOREIGN KEY ("type_id") REFERENCES "note_type" ("id") ON UPDATE CASCADE
);

COMMENT ON TABLE "note" IS '贴子信息';
COMMENT ON COLUMN "note"."id" IS 'GUID';
COMMENT ON COLUMN "note"."num" IS '流水号';
COMMENT ON COLUMN "note"."title" IS '标题';
COMMENT ON COLUMN "note"."content" IS '内容';
COMMENT ON COLUMN "note"."type_id" IS '类型id';
COMMENT ON COLUMN "note"."create_time" IS '创建时间';
COMMENT ON COLUMN "note"."update_time" IS '最后更新时间';

-- ----------------------------
-- Table structure for reply
-- ----------------------------
DROP TABLE IF EXISTS "reply";
CREATE TABLE "reply" (
  "id" varchar(32) NOT NULL,
  "num" bigint NOT NULL,
  "user_id" varchar(32) NOT NULL,
  "note_id" varchar(32) NOT NULL,
  "reply_id" varchar(32) DEFAULT NULL,
  "content" text,
  "score" bigint DEFAULT NULL,
  "create_time" time NOT NULL,
  "update_time" time DEFAULT NULL,
  PRIMARY KEY ("id"),
  CONSTRAINT "fk_reply_note_id" FOREIGN KEY ("note_id") REFERENCES "note" ("id") ON UPDATE CASCADE,
  CONSTRAINT "fk_reply_user_id" FOREIGN KEY ("user_id") REFERENCES "users" ("id") ON UPDATE CASCADE
);

COMMENT ON TABLE "reply" IS '回复信息';
COMMENT ON COLUMN "reply"."id" IS 'GUID';
COMMENT ON COLUMN "reply"."num" IS '流水号';
COMMENT ON COLUMN "reply"."user_id" IS '用户id';
COMMENT ON COLUMN "reply"."note_id" IS '贴子id';
COMMENT ON COLUMN "reply"."reply_id" IS '回复id';
COMMENT ON COLUMN "reply"."content" IS '内容';
COMMENT ON COLUMN "reply"."score" IS '分数';
COMMENT ON COLUMN "reply"."create_time" IS '创建时间';
COMMENT ON COLUMN "reply"."update_time" IS '最后更新时间';

-- ----------------------------
-- Table structure for reply_score
-- ----------------------------
DROP TABLE IF EXISTS "reply_score";
CREATE TABLE "reply_score" (
  "id" varchar(32) NOT NULL,
  "num" bigint NOT NULL,
  "user_id" varchar(32) NOT NULL,
  "reply_id" varchar(32) DEFAULT NULL,
  "create_time" time NOT NULL,
  "update_time" time DEFAULT NULL,
  PRIMARY KEY ("id"),
  CONSTRAINT "reply_score_reply_id" FOREIGN KEY ("reply_id") REFERENCES "reply" ("id") ON UPDATE CASCADE,
  CONSTRAINT "reply_score_user_id" FOREIGN KEY ("user_id") REFERENCES "users" ("id") ON UPDATE CASCADE
);

COMMENT ON TABLE "reply_score" IS '回复评分记录';
COMMENT ON COLUMN "reply_score"."id" IS 'GUID';
COMMENT ON COLUMN "reply_score"."num" IS '流水号';
COMMENT ON COLUMN "reply_score"."user_id" IS '用户id';
COMMENT ON COLUMN "reply_score"."reply_id" IS '回复id';
COMMENT ON COLUMN "reply_score"."create_time" IS '创建时间';
COMMENT ON COLUMN "reply_score"."update_time" IS '最后更新时间';
