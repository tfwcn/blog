package com.tfwcn.blog.models;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.Table;
import java.util.Date;

/**
 * 贴子信息 类
 */
@Entity
@Table(name = "note")
public class NoteInfo {
    public NoteInfo() {
        
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
     * 标题
     */
    @Column(name = "title", length = 200)
    private String title;

    /**
     * 内容
     */
    @Column(name = "content")
    private String content;

    /**
     * 类型id
     */
    @Column(name = "type_id", nullable = false, length = 32)
    private String typeId;

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
     * 设置 标题
     */
    public String getTitle() {
        return title;
    }

    /**
     * 获取 标题
     */
    public void setTitle(String title) {
        this.title = title;
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
     * 设置 类型id
     */
    public String getTypeId() {
        return typeId;
    }

    /**
     * 获取 类型id
     */
    public void setTypeId(String typeId) {
        this.typeId = typeId;
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
