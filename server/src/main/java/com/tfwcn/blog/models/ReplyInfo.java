package com.tfwcn.blog.models;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.Table;
import java.util.Date;

/**
 * 回复信息 类
 */
@Entity
@Table(name = "reply")
public class ReplyInfo {
    public ReplyInfo() {
        
    }

    /**
     * GUID
     */
    @Id
    @Column(name = "id", nullable = false, length = 32)
    private String id;

    /**
     * 流水号
     */
    @Column(name = "num", nullable = false, length = 20)
    private Long num;

    /**
     * 用户id
     */
    @Column(name = "user_id", nullable = false, length = 32)
    private String userId;

    /**
     * 贴子id
     */
    @Column(name = "note_id", nullable = false, length = 32)
    private String noteId;

    /**
     * 回复id
     */
    @Column(name = "reply_id", length = 32)
    private String replyId;

    /**
     * 内容
     */
    @Column(name = "content")
    private String content;

    /**
     * 分数
     */
    @Column(name = "score", length = 20)
    private Long score;

    /**
     * 创建时间
     */
    @Column(name = "create_time", nullable = false)
    private Date createTime;

    /**
     * 最后更新时间
     */
    @Column(name = "update_time")
    private Date updateTime;

    /**
     * 设置 GUID
     */
    public String getId() {
        return id;
    }

    /**
     * 获取 GUID
     */
    public void setId(String id) {
        this.id = id;
    }

    /**
     * 设置 流水号
     */
    public Long getNum() {
        return num;
    }

    /**
     * 获取 流水号
     */
    public void setNum(Long num) {
        this.num = num;
    }

    /**
     * 设置 用户id
     */
    public String getUserId() {
        return userId;
    }

    /**
     * 获取 用户id
     */
    public void setUserId(String userId) {
        this.userId = userId;
    }

    /**
     * 设置 贴子id
     */
    public String getNoteId() {
        return noteId;
    }

    /**
     * 获取 贴子id
     */
    public void setNoteId(String noteId) {
        this.noteId = noteId;
    }

    /**
     * 设置 回复id
     */
    public String getReplyId() {
        return replyId;
    }

    /**
     * 获取 回复id
     */
    public void setReplyId(String replyId) {
        this.replyId = replyId;
    }

    /**
     * 设置 内容
     */
    public String getContent() {
        return content;
    }

    /**
     * 获取 内容
     */
    public void setContent(String content) {
        this.content = content;
    }

    /**
     * 设置 分数
     */
    public Long getScore() {
        return score;
    }

    /**
     * 获取 分数
     */
    public void setScore(Long score) {
        this.score = score;
    }

    /**
     * 设置 创建时间
     */
    public Date getCreateTime() {
        return createTime;
    }

    /**
     * 获取 创建时间
     */
    public void setCreateTime(Date createTime) {
        this.createTime = createTime;
    }

    /**
     * 设置 最后更新时间
     */
    public Date getUpdateTime() {
        return updateTime;
    }

    /**
     * 获取 最后更新时间
     */
    public void setUpdateTime(Date updateTime) {
        this.updateTime = updateTime;
    }
}
