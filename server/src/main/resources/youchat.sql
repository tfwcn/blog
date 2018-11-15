/*
 Navicat PostgreSQL Data Transfer

 Source Server         : localhost
 Source Server Type    : PostgreSQL
 Source Server Version : 110000
 Source Host           : localhost:5432
 Source Catalog        : blog
 Source Schema         : public

 Target Server Type    : PostgreSQL
 Target Server Version : 110000
 File Encoding         : 65001

 Date: 30/10/2018 23:08:15
*/


-- ----------------------------
-- Table structure for errors
-- ----------------------------
DROP TABLE IF EXISTS "public"."errors";
CREATE TABLE "public"."errors" (
  "id" varchar(32) COLLATE "pg_catalog"."default" NOT NULL,
  "num" int8 NOT NULL,
  "message" varchar(200) COLLATE "pg_catalog"."default" DEFAULT NULL::character varying,
  "detail" text COLLATE "pg_catalog"."default",
  "create_time" time(6) NOT NULL,
  "update_time" time(6)
)
;
COMMENT ON COLUMN "public"."errors"."id" IS 'GUID';
COMMENT ON COLUMN "public"."errors"."num" IS '流水号';
COMMENT ON COLUMN "public"."errors"."message" IS '错误信息';
COMMENT ON COLUMN "public"."errors"."detail" IS '错误明细';
COMMENT ON COLUMN "public"."errors"."create_time" IS '创建时间';
COMMENT ON COLUMN "public"."errors"."update_time" IS '最后更新时间';
COMMENT ON TABLE "public"."errors" IS '错误信息';

-- ----------------------------
-- Table structure for notes
-- ----------------------------
DROP TABLE IF EXISTS "public"."notes";
CREATE TABLE "public"."notes" (
  "id" varchar(32) COLLATE "pg_catalog"."default" NOT NULL,
  "num" int8 NOT NULL,
  "title" varchar(200) COLLATE "pg_catalog"."default" DEFAULT NULL::character varying,
  "content" text COLLATE "pg_catalog"."default",
  "type_id" varchar(32) COLLATE "pg_catalog"."default" NOT NULL,
  "create_time" time(6) NOT NULL,
  "update_time" time(6)
)
;
COMMENT ON COLUMN "public"."notes"."id" IS 'GUID';
COMMENT ON COLUMN "public"."notes"."num" IS '流水号';
COMMENT ON COLUMN "public"."notes"."title" IS '标题';
COMMENT ON COLUMN "public"."notes"."content" IS '内容';
COMMENT ON COLUMN "public"."notes"."type_id" IS '类型id';
COMMENT ON COLUMN "public"."notes"."create_time" IS '创建时间';
COMMENT ON COLUMN "public"."notes"."update_time" IS '最后更新时间';
COMMENT ON TABLE "public"."notes" IS '贴子信息';

-- ----------------------------
-- Table structure for notes_type
-- ----------------------------
DROP TABLE IF EXISTS "public"."notes_type";
CREATE TABLE "public"."notes_type" (
  "id" varchar(32) COLLATE "pg_catalog"."default" NOT NULL,
  "num" int8 NOT NULL,
  "name" varchar(20) COLLATE "pg_catalog"."default" DEFAULT NULL::character varying,
  "detail" text COLLATE "pg_catalog"."default",
  "create_time" time(6) NOT NULL,
  "update_time" time(6),
  "path" varchar(255) COLLATE "pg_catalog"."default"
)
;
COMMENT ON COLUMN "public"."notes_type"."id" IS 'GUID';
COMMENT ON COLUMN "public"."notes_type"."num" IS '流水号';
COMMENT ON COLUMN "public"."notes_type"."name" IS '类型名称';
COMMENT ON COLUMN "public"."notes_type"."detail" IS '类型描述';
COMMENT ON COLUMN "public"."notes_type"."create_time" IS '创建时间';
COMMENT ON COLUMN "public"."notes_type"."update_time" IS '最后更新时间';
COMMENT ON COLUMN "public"."notes_type"."path" IS '跳转地址';
COMMENT ON TABLE "public"."notes_type" IS '贴子类型';

-- ----------------------------
-- Table structure for replies
-- ----------------------------
DROP TABLE IF EXISTS "public"."replies";
CREATE TABLE "public"."replies" (
  "id" varchar(32) COLLATE "pg_catalog"."default" NOT NULL,
  "num" int8 NOT NULL,
  "user_id" varchar(32) COLLATE "pg_catalog"."default" NOT NULL,
  "note_id" varchar(32) COLLATE "pg_catalog"."default" NOT NULL,
  "reply_id" varchar(32) COLLATE "pg_catalog"."default" DEFAULT NULL::character varying,
  "content" text COLLATE "pg_catalog"."default",
  "score" int8,
  "create_time" time(6) NOT NULL,
  "update_time" time(6)
)
;
COMMENT ON COLUMN "public"."replies"."id" IS 'GUID';
COMMENT ON COLUMN "public"."replies"."num" IS '流水号';
COMMENT ON COLUMN "public"."replies"."user_id" IS '用户id';
COMMENT ON COLUMN "public"."replies"."note_id" IS '贴子id';
COMMENT ON COLUMN "public"."replies"."reply_id" IS '回复id';
COMMENT ON COLUMN "public"."replies"."content" IS '内容';
COMMENT ON COLUMN "public"."replies"."score" IS '分数';
COMMENT ON COLUMN "public"."replies"."create_time" IS '创建时间';
COMMENT ON COLUMN "public"."replies"."update_time" IS '最后更新时间';
COMMENT ON TABLE "public"."replies" IS '回复信息';

-- ----------------------------
-- Table structure for replies_score
-- ----------------------------
DROP TABLE IF EXISTS "public"."replies_score";
CREATE TABLE "public"."replies_score" (
  "id" varchar(32) COLLATE "pg_catalog"."default" NOT NULL,
  "num" int8 NOT NULL,
  "user_id" varchar(32) COLLATE "pg_catalog"."default" NOT NULL,
  "reply_id" varchar(32) COLLATE "pg_catalog"."default" DEFAULT NULL::character varying,
  "create_time" time(6) NOT NULL,
  "update_time" time(6)
)
;
COMMENT ON COLUMN "public"."replies_score"."id" IS 'GUID';
COMMENT ON COLUMN "public"."replies_score"."num" IS '流水号';
COMMENT ON COLUMN "public"."replies_score"."user_id" IS '用户id';
COMMENT ON COLUMN "public"."replies_score"."reply_id" IS '回复id';
COMMENT ON COLUMN "public"."replies_score"."create_time" IS '创建时间';
COMMENT ON COLUMN "public"."replies_score"."update_time" IS '最后更新时间';
COMMENT ON TABLE "public"."replies_score" IS '回复评分记录';

-- ----------------------------
-- Table structure for users
-- ----------------------------
DROP TABLE IF EXISTS "public"."users";
CREATE TABLE "public"."users" (
  "id" varchar(32) COLLATE "pg_catalog"."default" NOT NULL,
  "num" int8 NOT NULL,
  "user_name" varchar(20) COLLATE "pg_catalog"."default" NOT NULL,
  "login_name" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "password" varchar(32) COLLATE "pg_catalog"."default" NOT NULL,
  "create_time" time(6) NOT NULL,
  "update_time" time(6)
)
;
COMMENT ON COLUMN "public"."users"."id" IS 'GUID';
COMMENT ON COLUMN "public"."users"."num" IS '流水号';
COMMENT ON COLUMN "public"."users"."user_name" IS '用户名';
COMMENT ON COLUMN "public"."users"."login_name" IS '登录账号';
COMMENT ON COLUMN "public"."users"."password" IS '密码';
COMMENT ON COLUMN "public"."users"."create_time" IS '创建时间';
COMMENT ON COLUMN "public"."users"."update_time" IS '最后更新时间';
COMMENT ON TABLE "public"."users" IS '用户信息';

-- ----------------------------
-- Primary Key structure for table errors
-- ----------------------------
ALTER TABLE "public"."errors" ADD CONSTRAINT "error_pkey" PRIMARY KEY ("id");

-- ----------------------------
-- Primary Key structure for table notes
-- ----------------------------
ALTER TABLE "public"."notes" ADD CONSTRAINT "note_pkey" PRIMARY KEY ("id");

-- ----------------------------
-- Primary Key structure for table notes_type
-- ----------------------------
ALTER TABLE "public"."notes_type" ADD CONSTRAINT "note_type_pkey" PRIMARY KEY ("id");

-- ----------------------------
-- Primary Key structure for table replies
-- ----------------------------
ALTER TABLE "public"."replies" ADD CONSTRAINT "reply_pkey" PRIMARY KEY ("id");

-- ----------------------------
-- Primary Key structure for table replies_score
-- ----------------------------
ALTER TABLE "public"."replies_score" ADD CONSTRAINT "reply_score_pkey" PRIMARY KEY ("id");

-- ----------------------------
-- Indexes structure for table users
-- ----------------------------
CREATE UNIQUE INDEX "users_login_name" ON "public"."users" USING btree (
  "login_name" COLLATE "pg_catalog"."default" "pg_catalog"."text_ops" ASC NULLS LAST
);

-- ----------------------------
-- Primary Key structure for table users
-- ----------------------------
ALTER TABLE "public"."users" ADD CONSTRAINT "users_pkey" PRIMARY KEY ("id");

-- ----------------------------
-- Foreign Keys structure for table notes
-- ----------------------------
ALTER TABLE "public"."notes" ADD CONSTRAINT "fk_note_type_id" FOREIGN KEY ("type_id") REFERENCES "public"."notes_type" ("id") ON DELETE NO ACTION ON UPDATE CASCADE;

-- ----------------------------
-- Foreign Keys structure for table replies
-- ----------------------------
ALTER TABLE "public"."replies" ADD CONSTRAINT "fk_reply_note_id" FOREIGN KEY ("note_id") REFERENCES "public"."notes" ("id") ON DELETE NO ACTION ON UPDATE CASCADE;
ALTER TABLE "public"."replies" ADD CONSTRAINT "fk_reply_user_id" FOREIGN KEY ("user_id") REFERENCES "public"."users" ("id") ON DELETE NO ACTION ON UPDATE CASCADE;

-- ----------------------------
-- Foreign Keys structure for table replies_score
-- ----------------------------
ALTER TABLE "public"."replies_score" ADD CONSTRAINT "reply_score_reply_id" FOREIGN KEY ("reply_id") REFERENCES "public"."replies" ("id") ON DELETE NO ACTION ON UPDATE CASCADE;
ALTER TABLE "public"."replies_score" ADD CONSTRAINT "reply_score_user_id" FOREIGN KEY ("user_id") REFERENCES "public"."users" ("id") ON DELETE NO ACTION ON UPDATE CASCADE;
